using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Hilfsfunktionen;

namespace XYZ_Table
{
    public partial class ISEL_3Axis : UserControl, IXYZ
    {

        //********************************************************************************************************************
        //                                    Eigenschaften der Klasse
        //********************************************************************************************************************

        #region Variables

        //1. Zustands Parameter
        public Boolean IsConnected { get; internal set; } = false;          //Wenn verbindung über COM-Port besteht
        public Boolean IsReady { get; internal set; } = false;              //Wenn Init-Befehl und Referenzfahrt durchgeführt wurden
        public string UserInfo { get; internal set; }


        //2. Geschwindigkeit
        public decimal X_ges_schnell { get; set; } = 8000;                 //Geschwindigkeit für Verfahrweg (wird nachher als String benötigt)
        public decimal Y_ges_schnell { get; set; } = 8000;
        public decimal Z_ges_schnell { get; set; } = 8000;
        public decimal A_ges_schnell { get; set; } = 8000;

        //3. Umrechnung für Verfahrweg
        internal static decimal Schritt_pro_umdrehung_Linear { get; } = 800;        //Einstellbar über DIP-Schalter an den Endstufen
        internal static decimal Schritt_pro_umdrehung_Rotation { get; } = 2000;
        internal static decimal Spindel_steigug { get; } = 4;                       //Spindelsteigung so bestellt
        internal static decimal Untersetzung_Rotation { get; } = 30;

        //4. Grenzen (Tischparameter) dürfen nicht überschritten werden
        public decimal Grenze_X_pos { get; set; } = 530;
        public decimal Grenze_X_neg { get; set; } = 0;
        public decimal Grenze_Y_pos { get; set; } = 0;
        public decimal Grenze_Y_neg { get; internal set; } = -500;
        public decimal Grenze_Z_pos { get; set; } = 0;
        public decimal Grenze_Z_neg { get; set; } = -40;
        public decimal Grenze_A_pos { get; set; } = 0;
        public decimal Grenze_A_neg { get; set; } = 0;




        //5.Aktuelle Position
        public decimal Akt_x_Koordinate { get; internal set; }
        public decimal Akt_y_Koordinate { get; internal set; }
        public decimal Akt_z_Koordinate { get; internal set; }
        public decimal Akt_Winkel { get; internal set; }

        //6. Serielle Schnittstelle
        private ISEL MyISEL { get; } = new ISEL();

        #endregion Variables

        //********************************************************************************************************************
        //                                                Konstruktoren
        //********************************************************************************************************************
        public ISEL_3Axis()
        {
            InitializeComponent();

            //Com Port lesen
            HelpFCT.SetComPortBox(ComPort_select);
        }

        public ISEL_3Axis(Form callingForm, int x, int y)
        {
            InitializeComponent();

            HelpFCT.SetComPortBox(ComPort_select);

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "ISEL_3Axis";
            this.Size = new System.Drawing.Size(515, 80);
            this.TabIndex = 33;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }

        //********************************************************************************************************************
        //                                                  GUI-Events
        //********************************************************************************************************************

        #region GUI

        private void Button_OpenClose_Click(object sender, EventArgs e)
        {
            if (!IsConnected)
                Open();
            else
                Close();
        }

        private void BarButtonItem_Init_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Initialisieren();
        }

        private void BarButtonItem_Manual_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Window_XYZ_manuell_drive XYZ_manuall_window = new Window_XYZ_manuell_drive(this);
            XYZ_manuall_window.Show();
        }

        private void BarButtonItem_REF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReferenceDrive();
        }

        private void BarButtonItem_LOG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("LOG:\n" + MyISEL.Communication_LOG, "Communication log XYZ table");
        }

        #endregion GUI

        //********************************************************************************************************************
        //                                        globale Functions (Interface I_XYZ)
        //********************************************************************************************************************

        #region Global_FCT

        public void Initialisieren()
        {
            MyISEL.SendMessage_withAnswer("@07");
        }

        public void ReferenceDrive()
        {
            MyISEL.SendMessage_withoutAnswer("@0R7");
            MyISEL.Check_if_position_is_reached_REFDrive();

            Position_aktualisiern();
            barButtonItem_Manual.Enabled = true;
        }

        public  bool Move2Position(decimal xKoordinate, decimal yKoordinate, decimal zKoordinate, decimal winkel)
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


                MyISEL.SendMessage_withoutAnswer("@0M " + position_x.ToString() + "," + X_ges_schnell + ","
                                                      + position_y.ToString() + "," + Y_ges_schnell + ","
                                                      + position_z.ToString() + ',' + Z_ges_schnell + ",0,900");

                //Schritte = Länge * schritte pro umdrehung / spindelsteigung
                //Geschwindigkeit (mm/s) = spindelsteigung * geschwindigkeit (schritte pro sekunde) / schritte pro umdrehung
                int schritte_x = decimal.ToInt32((xKoordinate - Akt_x_Koordinate) * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur x-KOO 
                int schritte_y = decimal.ToInt32((yKoordinate - Akt_y_Koordinate) * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur y-KOO 
                int schritte_z = decimal.ToInt32((zKoordinate - Akt_z_Koordinate) * Schritt_pro_umdrehung_Linear / Spindel_steigug);   //Schritte zur z-KOO 

                //Warten bis am Ziel angekommen
                int wartezeit_in_ms = Calculate_Waiting_TIME(schritte_x, schritte_y, schritte_z, 0);
                System.Threading.Thread.Sleep(wartezeit_in_ms);

                //Nachricht abholen
                string test = MyISEL.Check_if_position_is_reached();

                //Position aktuallisieren
                Position_aktualisiern();

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool MoveADistance(decimal xWeg, decimal yWeg, decimal zWeg, decimal winkel)
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
                MyISEL.SendMessage_withoutAnswer("@0A " + schritte_x.ToString() + "," + X_ges_schnell + ","
                                                      + schritte_y.ToString() + "," + Y_ges_schnell + ","
                                                      + schritte_z.ToString() + ',' + Z_ges_schnell + ",0,900");


                //Warten bis am Ziel angekommen
                int wartezeit_in_ms = Calculate_Waiting_TIME(schritte_x, schritte_y, schritte_z, 0);
                System.Threading.Thread.Sleep(wartezeit_in_ms + 100);

                //Nachricht abholen
                string test = MyISEL.Check_if_position_is_reached();

                //Position aktuallisieren
                Position_aktualisiern();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Position_aktualisiern()
        {
            //Position abfragen, Antwort hat mindestens 20 Zeichen
            MyISEL.SendMessage_withAnswer("@0P", 25);

            //Positionen herauslösen
            if (MyISEL.Antwort.Length == 19)
            {
                //Teil Strings herauslösen
                String position_X_Teil = MyISEL.Antwort.Substring(1, 6);
                String position_Y_Teil = MyISEL.Antwort.Substring(7, 6);
                String position_Z_Teil = MyISEL.Antwort.Substring(13, 6);

                //Umrechnung in Position in mm
                Akt_x_Koordinate = ISEL.HexString_to_Decimal(position_X_Teil) / (Schritt_pro_umdrehung_Linear / Spindel_steigug);
                Akt_y_Koordinate = ISEL.HexString_to_Decimal(position_Y_Teil) / (Schritt_pro_umdrehung_Linear / Spindel_steigug);
                Akt_z_Koordinate = ISEL.HexString_to_Decimal(position_Z_Teil) / (Schritt_pro_umdrehung_Linear / Spindel_steigug);

                if (akt_Position.InvokeRequired)
                {
                    akt_Position.Invoke((MethodInvoker)delegate
                    {
                        akt_Position.Text =
                            "X: " + Akt_x_Koordinate.ToString() + "mm; " +
                            "Y: " + Akt_y_Koordinate.ToString() + "mm; " +
                            "Z: " + Akt_z_Koordinate.ToString() + "mm; ";
                    });
                }
                else
                {
                    akt_Position.Text =
                        "X: " + Akt_x_Koordinate.ToString() + "mm; " +
                        "Y: " + Akt_y_Koordinate.ToString() + "mm; " +
                        "Z: " + Akt_z_Koordinate.ToString() + "mm; ";
                }

            }
            else
            {
                akt_Position.Text = "Not able to get correct position!";
            }

        }

        #endregion Global_FCT

        //********************************************************************************************************************
        //                                              lokale Functions
        //********************************************************************************************************************


        #region Open/Close

        public void Open()
        {
            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                MessageBox.Show("A COM Port has to be selected! \nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //COMPort anpassen
            MyISEL.Serial_Interface.PortName = ComPort_select.Text;

            //Verbindung herstellen
            try
            {
                MyISEL.Serial_Interface.Open();
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
            UserInfo = MyISEL.SendMessage_withAnswer("@0U");

            //Überprüfen
            if (UserInfo.IndexOf("UserInfo") > 0)
                IsConnected = true;
            else
            {
                MessageBox.Show("COM Port represents no XYZ table!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MyISEL.Serial_Interface.Close();
                return;
            }

            //UI anpassen
            barButtonItem_Init.Enabled = true;
            barButtonItem_REF.Enabled = true;
            ComPort_select.Enabled = false;

            //Initalisieren
            Initialisieren();

            //Achsen richtungen anpassen (z.B. Y-Vertauschen).
            AdjusteAxisDerections();

            //Testen ob Initalisierung und Referenzfahrt notwendig sind
            //Wenn aktuelle Position != 0,0,0 ist wurde xyz nicht ausgeschalten
            Position_aktualisiern();


            if (Akt_x_Koordinate != 0 | Akt_y_Koordinate != 0 | Akt_z_Koordinate != 0 | Akt_Winkel != 0)
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
            var test2 = groupBox_XYZ.Handle;
        }

        public void Close()
        {
            //COMPort schließen
            MyISEL.Serial_Interface.Close();

            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Open";
            akt_Position.Text = "Not Connected!";

            //Oberfläche anpassen                
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

        public void AdjusteAxisDerections()
        {
            //Nix
        }

        #endregion Init, Ref, Axis

        internal bool CheckIfPointAllowed(decimal xKoordinate, decimal yKoordinate, decimal zKoordinate, decimal winkel)
        {
            //Überprüfen ob eine Achse aus dem Arbeitsbereich laufen würde
            if (xKoordinate < Grenze_X_neg)
            {
                MessageBox.Show("x-Axis would leave working-area in negativ direction", "error");
            }
            else if (xKoordinate > Grenze_X_pos)
            {
                MessageBox.Show("x-Axis would leave working-area in positiv direction", "error");
            }
            else if (yKoordinate < Grenze_Y_neg)
            {
                MessageBox.Show("y-Axis would leave working-area in negativ direction", "error");
            }
            else if (yKoordinate > Grenze_Y_pos)
            {
                MessageBox.Show("y-Axis would leave working-area in positiv direction", "error");
            }
            else if (zKoordinate < Grenze_Z_neg)
            {
                MessageBox.Show("z-Axis would leave working-area in negativ direction", "error");
            }
            else if (zKoordinate > Grenze_Z_pos)
            {
                MessageBox.Show("z-Axis would leave working-area in positiv direction", "error");
            }
            else if (winkel < Grenze_A_neg)
            {
                MessageBox.Show("z-Axis would leave working-area in negativ direction", "error");
            }
            else if (winkel > Grenze_A_pos)
            {
                MessageBox.Show("z-Axis would leave working-area in positiv direction", "error");
            }
            else
            {
                return true;
            }
            return false;
        }

        internal Int32 Calculate_Waiting_TIME(Int32 xSTeps, Int32 ySTeps, Int32 zSTeps, Int32 aSTeps)
        {
            //Zeit = Steps / Geschwindigkeit[steps/s] * 1000(umrechnung ms)
            decimal time_x = Math.Abs(xSTeps) * 1000 / X_ges_schnell;
            decimal time_y = Math.Abs(ySTeps) * 1000 / Y_ges_schnell;
            decimal time_z = Math.Abs(zSTeps) * 1000 / Z_ges_schnell;
            decimal time_a = Math.Abs(aSTeps) * 1000 / A_ges_schnell;

            //Diese Funktion berechnet die Gesamtstrecke die Verfahren werden muss
            //x und y Achse werden gleichzeit gefahren (Die Längere bestimmt die gesamtStecke)
            //Anschließend wird die z-Achse verfahren (deshalb addiert)
            //Anschließend wird die a-Achse verfahren (deshalb addiert)
            //Toleranz-Faktor 1.1m
            if (time_x > time_y)
            {
                return Convert.ToInt32((time_x + time_z + time_a) * 1.1m);
            }
            else
            {
                return Convert.ToInt32((time_y + time_z + time_a) * 1.1m);
            }
        }

        #region AutoOpen

        /*public override string AutoOpen(Load_Screen myLoadScreen)
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
        */

        #endregion AutoOpen
    }
}
