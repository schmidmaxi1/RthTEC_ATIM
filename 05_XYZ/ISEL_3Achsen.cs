using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ATIM_GUI._2_AutoConnect;

namespace ATIM_GUI._05_XYZ
{
    class ISEL_3Achsen : XYZ_table
    {

        //********************************************************************************************************************
        //                                              Konstruktoren
        //********************************************************************************************************************

        public ISEL_3Achsen()
        {
            Init_GUI();

            Grenze_X_pos = 500;                    //Eingentlich 530, aber Kamera im weg
            Grenze_X_neg = 0;
            Grenze_Y_pos = 0;
            Grenze_Y_neg = -500;
            Grenze_Z_pos = 0;
            Grenze_Z_neg = -40;
            Grenze_A_neg = 0;
            Grenze_A_pos = 0;
        }

        //********************************************************************************************************************
        //                                           Functions to Override
        //********************************************************************************************************************

        #region Open/Close

        public override void Open()
        {
            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                MessageBox.Show("A COM Port has to be selected! \nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            //COMPort anpassen
            Serial_Interface.PortName = ComPort_select.Text;

            //Verbindung herstellen
            try
            {
                Serial_Interface.Open();
            }
            catch (UnauthorizedAccessException)
            {
                //Wenn es nicht funktioniert --> abbrechen
                MessageBox.Show("COM Port is allready in use!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("COM Port is not available!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Serial_Interface.WriteLine("@0U");

            //UserInfo Abfragen (ob es sich um einen XYZ Tisch handelt)
            UserInfo = this.SendMessage_withAnswer("@0U");

            //Überprüfen
            if (UserInfo.IndexOf("UserInfo") > 0)
                IsConnected = true;
            else
            {
                MessageBox.Show("COM Port represents no XYZ table!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Serial_Interface.Close();
                return;
            }

            //UI anpassen
            barButtonItem_Init.Enabled = true;
            barButtonItem_REF.Enabled = true;
            ComPort_select.Enabled = false;

            //Initalisieren
            Initialisieren();


            //Testen ob Initalisierung und Referenzfahrt notwendig sind
            //Wenn aktuelle Position != 0,0,0 ist wurde xyz nicht ausgeschalten
            Position_aktualisiern();


            if (Akt_x_Koordinate != 0 | Akt_y_Koordinate != 0 | Akt_z_Koordinate != 0)
            {
                //Voll funkionsfähirg
                IsReady = true;
                barButtonItem_Manual.Enabled = true;
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Reference drive is needed! Do you want to run one now?\n" +
                    "Make sure you have activated power on iMC8", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    ReferenceDrive();
                    IsReady = true;
                    barButtonItem_Manual.Enabled = true;
                }

            }


            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Close";

        }

        public override void Close()
        {
            //COMPort schließen
            Serial_Interface.Close();

            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Open";
            akt_Postition.Text = "Not Connected!";

            ComPort_select.Enabled = true;
            barButtonItem_Init.Enabled = false;
            barButtonItem_REF.Enabled = false;
            barButtonItem_Manual.Enabled = false;

            //Flags anpassen
            IsReady = false;
            IsConnected = false;


        }

        #endregion Open/Close

        #region Init, Ref, Axis

        public override void Initialisieren()
        {
            SendMessage_withAnswer("@07");
        }


        public override void ReferenceDrive()
        {
            SendMessage_withAnswer("@0R7");
        }

        #endregion Init, Ref, Axis

        #region Bewegungsbefehle

        //1. Fahren zu einem Punkt-----------------------------------------------------------------
        public override bool Move2Position(decimal xKoordinate, decimal yKoordinate, decimal zKoordinate, decimal winkel)
        {
            //Funktion zum Entgegennehmen des Punktes und Senden der resultierenden Befehle über den seriellen Port
            Position_aktualisiern();

            //Ist die Koordinate im erlaubten bereich
            if (this.CheckIfPointAllowed(xKoordinate, yKoordinate, zKoordinate, 0))
            {
                //Schritte Berechnen

                //Schritte = Länge * schritte pro umdrehung / spindelsteigung
                //Geschwindigkeit (mm/s) = spindelsteigung * geschwindigkeit (schritte pro sekunde) / schritte pro umdrehung
                int position_x = decimal.ToInt32(xKoordinate * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur x-KOO 
                int position_y = decimal.ToInt32(yKoordinate * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur y-KOO 
                int position_z = decimal.ToInt32(zKoordinate * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur z-KOO 


                this.SendMessage_withoutAnswer("@0M " + position_x.ToString() + "," + X_ges_schnell + ","
                                                      + position_y.ToString() + "," + Y_ges_schnell + ","
                                                      + position_z.ToString() + ',' + Z_ges_schnell + ",0,900");

                //Schritte = Länge * schritte pro umdrehung / spindelsteigung
                //Geschwindigkeit (mm/s) = spindelsteigung * geschwindigkeit (schritte pro sekunde) / schritte pro umdrehung
                int schritte_x = decimal.ToInt32((xKoordinate - Akt_x_Koordinate)* Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur x-KOO 
                int schritte_y = decimal.ToInt32((yKoordinate - Akt_y_Koordinate) * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur y-KOO 
                int schritte_z = decimal.ToInt32((zKoordinate - Akt_z_Koordinate) * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur z-KOO 

                //Warten bis am Ziel angekommen
                int wartezeit_in_ms = Calculate_Waiting_TIME(schritte_x, schritte_y, schritte_z, 0);
                System.Threading.Thread.Sleep(wartezeit_in_ms);

                //Nachricht abholen
                string test = Check_if_position_is_reached();

                //Position aktuallisieren
                Position_aktualisiern();

                return true;
            }
            else
            {
                return false;
            }

        }

        //2. Fahren einen Weg----------------------------------------------------------------------
        public override bool MoveADistance(decimal xWeg, decimal yWeg, decimal zWeg, decimal winkel)
        {
            //Funktion zum Entgegennehmen der Entfernungen und Senden der resultierenden Befehle über den seriellen Port
            //Position_aktualisiern();

            //Ist die Koordinate im erlaubten bereich
            if (this.CheckIfPointAllowed(Akt_x_Koordinate + xWeg, Akt_y_Koordinate + yWeg, Akt_z_Koordinate + zWeg, 0))
            {
                //Schritte = Länge * schritte pro umdrehung / spindelsteigung
                //Geschwindigkeit (mm/s) = spindelsteigung * geschwindigkeit (schritte pro sekunde) / schritte pro umdrehung

                int schritte_x = decimal.ToInt32(xWeg * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur x-KOO 
                int schritte_y = decimal.ToInt32(yWeg * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur y-KOO 
                int schritte_z = decimal.ToInt32(zWeg * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur z-KOO 

                //serialPort1.WriteLine("@0A 10000,800,0,900,0,900,0,900\r");
                this.SendMessage_withoutAnswer("@0A " + schritte_x.ToString() + "," + X_ges_schnell + ","
                                                      + schritte_y.ToString() + "," + Y_ges_schnell + ","
                                                      + schritte_z.ToString() + ',' + Z_ges_schnell + ",0,900");


                //Warten bis am Ziel angekommen
                int wartezeit_in_ms = Calculate_Waiting_TIME(schritte_x, schritte_y, schritte_z, 0);
                System.Threading.Thread.Sleep(wartezeit_in_ms + 100);

                //Nachricht abholen
                string test = Check_if_position_is_reached();

                //Position aktuallisieren
                Position_aktualisiern();

                return true;
            }
            else
            {
                return false;
            }
        }

        //3. Aktuelle Position abfragen-----------------------------------------------------------
        public override void Position_aktualisiern()
        {
            //Position abfragen, Antwort hat mindestens 20 Zeichen
            SendMessage_withAnswer("@0P", 25);

            //Positionen herauslösen
            if (Antwort.Length == 19)
            {
                //Teil Strings herauslösen
                String position_X_Teil = Antwort.Substring(1, 6);
                String position_Y_Teil = Antwort.Substring(7, 6);
                String position_Z_Teil = Antwort.Substring(13, 6);

                //Umrechnung in Position in mm
                Akt_x_Koordinate = HexString_to_Decimal(position_X_Teil) / (Schritt_pro_umdrehung_Linear / Spindel_steigug);
                Akt_y_Koordinate = HexString_to_Decimal(position_Y_Teil) / (Schritt_pro_umdrehung_Linear / Spindel_steigug);
                Akt_z_Koordinate = HexString_to_Decimal(position_Z_Teil) / (Schritt_pro_umdrehung_Linear / Spindel_steigug);

                if (akt_Postition.InvokeRequired)
                {
                    akt_Postition.Invoke((MethodInvoker)delegate
                    {
                        akt_Postition.Text =
                            "X: " + Akt_x_Koordinate.ToString() + "mm; " +
                            "Y: " + Akt_y_Koordinate.ToString() + "mm; " +
                            "Z: " + Akt_z_Koordinate.ToString() + "mm; ";
                    });
                }
                else
                {
                    akt_Postition.Text =
                        "X: " + Akt_x_Koordinate.ToString() + "mm; " +
                        "Y: " + Akt_y_Koordinate.ToString() + "mm; " +
                        "Z: " + Akt_z_Koordinate.ToString() + "mm; ";
                }

            }
            else
            {
                akt_Postition.Text = "Not able to get correct position!";
            }

        }

        #endregion Bewegungsbefehle

        #region AutoOpen

        public override string AutoOpen(Load_Screen myLoadScreen)
        {

            //Schritte zum Hochzählen
            int iterration = 5;

            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                return "XYZ table: No COM-Port selected!" + Environment.NewLine;
            }

            //COMPort anpassen
            Serial_Interface.PortName = ComPort_select.Text;

            //Verbindung herstellen
            try
            {
                Serial_Interface.Open();
            }
            catch (UnauthorizedAccessException)
            {
                return "XYZ table: COM Port is allready in use!" + Environment.NewLine;
            }
            catch (System.IO.IOException)
            {
                return "XYZ table: COM Port is not available!" + Environment.NewLine;
            }

            myLoadScreen.ChangeTask("Checking device ...", iterration);

            //UserInfo Abfragen (ob es sich um einen XYZ Tisch handelt)
            UserInfo = this.SendMessage_withAnswer("@0U");

            //Überprüfen
            if (UserInfo.IndexOf("UserInfo") > 0)
                IsConnected = true;
            else
            {
                Serial_Interface.Close();
                return "XYZ table: COM Port represents no XYZ table!" + Environment.NewLine;
            }

            myLoadScreen.ChangeTask("Change UI ...", iterration);
            //UI anpassen
            barButtonItem_Init.Enabled = true;
            barButtonItem_REF.Enabled = true;
            barButtonItem_Manual.Enabled = true;
            ComPort_select.Enabled = false;

            myLoadScreen.ChangeTask("Initialisation ...", iterration);

            //Initalisieren
            Initialisieren();

            myLoadScreen.ChangeTask("Referenz drive ...", iterration);

            //Testen ob Initalisierung und Referenzfahrt notwendig sind
            //Wenn aktuelle Position != 0,0,0 ist wurde xyz nicht ausgeschalten
            Position_aktualisiern();


            if (Akt_x_Koordinate != 0 | Akt_y_Koordinate != 0 | Akt_z_Koordinate != 0)
            {
                //Voll funkionsfähirg
                IsReady = true;
                barButtonItem_Init.Enabled = true;
                barButtonItem_REF.Enabled = true;
                barButtonItem_Manual.Enabled = true;
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Reference drive is needed! Do you want to run one now?\n" +
                    "Make sure you have activated power on iMC8", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    ReferenceDrive();
                    IsReady = true;
                }

            }


            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Close";



            return "";
        }

        #endregion AutoOpen

    }
}
