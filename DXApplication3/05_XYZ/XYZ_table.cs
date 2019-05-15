using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;              //Serielle Schnittstelle

using ATIM_GUI._2_AutoConnect;


namespace ATIM_GUI._05_XYZ
{
    public partial class XYZ_table : UserControl
    {

        /**********************************************************************************************************************
        * Klasse für den iM8 von Isel (XYZ Controller)
        * Schmid Maximlian
        * 27.07.2017
        * V3_0 (funkionsfähig)
        * 
        * Erklärung:
        * Hauptklasse (XYZ-Tabel) besitzt 
        * -die Platzhalter für alle notwendigen Verfahr-Fuktionen/Reset
        * -Alle Button Events
        * -Alle Variablen
        * -Kommunikations Funktionen
        * 
        * Die Unterklassen untterteilen sich in 3Achsen und 4Achsen
        * -Hier sind die Funktionen definiert
        * 
        * Was fehlt:
        * -Fehler-Abfrage
        * 
       *********************************************************************************************************************/


        //********************************************************************************************************************
        //                                    Eigenschaften der Klasse
        //********************************************************************************************************************

        //1. Zustands Parameter
        public Boolean IsConnected { get; internal set; } = false;          //Wenn verbindung über COM-Port besteht
        public Boolean IsReady { get; internal set; } = false;              //Wenn Init-Befehl und Referenzfahrt durchgeführt wurden

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
        public decimal Grenze_X_pos { get; set; }                    //Eingentlich 530, aber Kamera im weg
        public decimal Grenze_X_neg { get; set; }
        public decimal Grenze_Y_pos { get; set; }
        public decimal Grenze_Y_neg { get; internal set; }
        public decimal Grenze_Z_pos { get; set; }
        public decimal Grenze_Z_neg { get; set; }
        public decimal Grenze_A_pos { get; set; }
        public decimal Grenze_A_neg { get; set; }

        public decimal Anfahrt_z { get; set; } = -30;                       //HöhenPosition des Boards

        //5.Aktuelle Position
        public decimal Akt_x_Koordinate { get; internal set; }
        public decimal Akt_y_Koordinate { get; internal set; }
        public decimal Akt_z_Koordinate { get; internal set; }
        public decimal Akt_Winkel { get; internal set; }

        //6. Serielle Schnittstelle
        public SerialPort Serial_Interface { get; set; } = new SerialPort()
        {
            BaudRate = 19200,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = 0,

        };
        public string Antwort { get; internal set; }
        public string Communication_LOG { get; internal set; }


        //7. Sonstige Infos
        public string UserInfo { get; internal set; }

        //8. Signal-Ampel
        public bool Signal_green { get; internal set; } = false;
        public bool Signal_yellow { get; internal set; } = false;
        public bool Signal_red { get; internal set; } = false;


        //********************************************************************************************************************
        //                                              Konstruktoren
        //********************************************************************************************************************

        internal void Init_GUI()
        {
            InitializeComponent();

            //Alle ComPorts suchen
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                ComPort_select.Items.Add(port);
        }

        //Change Enable Status form MainForm
        public void Change_Enabled(Boolean input)
        {
            if (groupBox_XYZ.InvokeRequired)
            {
                groupBox_XYZ.Invoke((MethodInvoker)delegate
                {
                    groupBox_XYZ.Enabled = input;
                });
            }
            else
            {
                groupBox_XYZ.Enabled = input;
            }
        }


        //********************************************************************************************************************
        //                                               Buttons and DropDown
        //********************************************************************************************************************

        #region GUI Events

        internal void Button_OpenClose_Click(object sender, EventArgs e)
        {
            if (!IsConnected)
                Open();
            else
                Close();
        }

        private void BarButtonItem_Init_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Funktion in entsprechender Klasse wird aufgerufen
            Initialisieren();
        }

        private void BarButtonItem_Manual_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Window_XYZ_manuell_drive XYZ_manuall_window = new Window_XYZ_manuell_drive(this);
            XYZ_manuall_window.Show();
        }

        private void BarButtonItem_REF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Funktion in entsprechender Klasse wird aufgerufen
            ReferenceDrive();
        }

        private void BarButtonItem_LOG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("LOG:\n" + Communication_LOG, "Communication log XYZ table");
        }

        #endregion GUI Events

        //********************************************************************************************************************
        //                                  Befehle die in jedem Typ anderst sind
        //********************************************************************************************************************

        public virtual void Open() { }

        public virtual void Close() { }

        public virtual void Initialisieren(){ }

        public virtual void ReferenceDrive(){ }

        public virtual bool Move2Position(decimal xKoordinate, decimal yKoordinate, decimal zKoordinate, decimal winkel){ return false; }

        public virtual bool MoveADistance(decimal xKoordinate, decimal yKoordinate, decimal zKoordinate, decimal winkel){ return false; }

        public virtual void Position_aktualisiern() { }

        public virtual string AutoOpen(Load_Screen myLoadScreen) { return null; }

        //********************************************************************************************************************
        //                                  Hilfs-Funktionen die immer gleich sind
        //********************************************************************************************************************

        #region Allgemeine Hilfsfunktionen

        /// <summary>
        /// Calculats the time in [ms] that is necessary for movement according to single the axis steps
        /// </summary>
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

        /// <summary>
        /// Converts the HEX answer from the ISEL SYSTEM to the decimal number of steps
        /// </summary>
        internal Decimal HexString_to_Decimal(string input)
        {
            //Hex-Sting in int umwandeln
            Int32 anzahl_schritte = Convert.ToInt32(input, 16);

            //Da Rückgabewert ein 2er Komplement mit 24 Bit ist, muss bei negativen Zahlen umgerechnet werden
            if ((anzahl_schritte & 0x80000) == 0x80000)
            {
                anzahl_schritte = -1 * (((~anzahl_schritte) & 0xFFFFFF) + 1);
            }

            //Umwandlung von Schritte in mm
            return (Decimal)anzahl_schritte;
        }

        /// <summary>
        /// Checks if the next Point is allowed
        /// </summary>
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

        #endregion Allgemeine Hilfsfunktionen

        //********************************************************************************************************************
        //                                 Kommunikations-Funktionen sind immer gleich
        //********************************************************************************************************************

        #region Kommunikation
        //1. Nachricht senden und Antwort empfangen mit unbestimmter länge-------------------------
        internal string SendMessage_withAnswer(string nachricht)
        {
            //Nachricht senden
            

            Serial_Interface.WriteLine(nachricht + "\r");
            Communication_LOG += DateTime.Now.ToString() + " --> " + nachricht + "\n";

            //Pause damit iM8 die antwort vollständig gesendet hat
            System.Threading.Thread.Sleep(50);

            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }

            //Communication Log beschreiben
            Communication_LOG += DateTime.Now.ToString() +
              "\n     --> " + nachricht +
              "\n     <-- " + Antwort + "\n";

            return Antwort;
        }

        //2. Nachricht senden und Antwort empfangen mit bestimmter Länge (nicht möglich mit zu wenig Bytes)
        internal string SendMessage_withAnswer(string nachricht, UInt16 ziel_laenge)
        {
            //Nachricht senden
            Serial_Interface.WriteLine(nachricht + "\r");

            //Pause damit iM8 die antwort vollständig gesendet hat
            System.Threading.Thread.Sleep(10);

            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }

            int i = 0;
            while (Antwort.Length < ziel_laenge & i < 50)
            {
                i++;
                System.Threading.Thread.Sleep(10);
                Task.Delay(10);
                Antwort += Serial_Interface.ReadExisting();
            }

            //Communication Log beschreiben
            Communication_LOG += DateTime.Now.ToString() + " --> " + nachricht + "\n" +
              " <-- " + Antwort + "\n";

            return Antwort;
        }

        //3. Nachricht senden ohne die Antwort gleich zu wollen-------------------------------------------
        internal string SendMessage_withoutAnswer(string nachricht)
        {
            //Nachricht senden
            Serial_Interface.WriteLine(nachricht + "\r");

            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }

            //Communication Log beschreiben
            Communication_LOG += DateTime.Now.ToString() + " --> " + nachricht + "\n" +
              " <-- " + Antwort + "\n";

            return Antwort;

        }

        internal string Check_if_position_is_reached()
        {
            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }
            //0 kommt erst wenn Ziel erreicht
            int i = 0; //maximal 500ms
            while (Antwort.Length != 1 & i < 50)
            {
                i++;
                System.Threading.Thread.Sleep(10);
                Task.Delay(10);
                Antwort += Serial_Interface.ReadExisting();
            }

            //Communication Log beschreiben
            Communication_LOG += " <-- " + Antwort + "\n";

            return Antwort;
        }

        internal string Check_if_position_is_reached_REFDrive()
        {
            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }
            //0 kommt erst wenn Ziel erreicht
            int i = 0; //max. 100s
            while (Antwort.Length != 1 & i < 10000)
            {
                i++;
                System.Threading.Thread.Sleep(10);
                Task.Delay(10);
                Antwort += Serial_Interface.ReadExisting();
            }

            //Communication Log beschreiben
            Communication_LOG += " <-- " + Antwort + "\n";

            return Antwort;
        }

        #endregion Kommunikation


        //********************************************************************************************************************
        //                                      Hilfsfunktionen - Ampel
        //********************************************************************************************************************

        /// <summary>
        /// Change the Colour of the single lamp (true = ON)
        /// </summary>
        public void Signal_Change(bool green, bool yellow, bool red)
        {
            //Neue Werte übernehmen
            Signal_green = green;
            Signal_yellow = yellow;
            Signal_red = red;

            //Portwert berechnen
            Byte myByte = 0;

            if (green)
                myByte += 1;
            if (yellow)
                myByte += 2;
            if (red)
                myByte += 4;

            //Nachricht senden
            string test = "@0B0," + myByte.ToString("X2");
            SendMessage_withAnswer("@0B0," + myByte.ToString("X2"));
        }
    }
}
