using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;

using Hilfsfunktionen;
using Communication_Settings;
using AutoConnect;


namespace RthTEC_Rack
{
    public partial class RthTEC_V1 : UserControl, I_RthTEC
    {
        //********************************************************************************************************************
        //                                    Eigenschaften der Klasse
        //********************************************************************************************************************

        #region Variables

        /// <summary>
        /// Name and ID of RthTEC
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Flag if RthTEC is Connected
        /// </summary>
        public Boolean IsConnected { get; internal set; } = false;

        /// <summary>
        /// Flag if RthTEC is Enabled
        /// </summary>
        public Boolean IsEnabled { get; internal set; } = false;

        /// <summary>
        /// String mit Communication LOG
        /// </summary>
        public string Communication_LOG { get; internal set; }

        /// <summary>
        /// Number of available Slots in RthTEC Rack
        /// </summary>
        public int Slot_Count { get; internal set; } = 3;

        /// <summary>
        /// Slot-Belegung von RthTEC Rack
        /// </summary>
        public char[] SlotBelegung { get; set; } = new char[8];


        /// <summary>
        /// Standard & DPA heat pulse length in ms
        /// </summary>
        public decimal Time_Heat { get; internal set; }

        /// <summary>
        /// Standard & DPA meas pulse length in ms
        /// </summary>
        public decimal Time_Meas { get; internal set; }

        /// <summary>
        /// DPA short pulse length in ms
        /// </summary>
        public decimal DPA_Time { get; internal set; }

        /// <summary>
        /// DPA short pulse count
        /// </summary>
        public Int32 DPA_Count { get; internal set; }

        /// <summary>
        /// Measurment repitation TTA
        /// </summary>
        public int Cycles { get; internal set; } = 1;


        /// <summary>
        /// Feld mit den verwendeten Karten in den Slots
        /// </summary>
        public I_RthTEC_Card[] Cards { get; set; } = new I_RthTEC_Card[8];

        /// <summary>
        /// Serial Interface
        /// </summary>
        public SerialPort Serial_Interface { get; set; } = new SerialPort()
        {
            BaudRate = 57600,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = 0,
            ReadTimeout = 500
        };

        #endregion Variables

        Window_RthTEC_Rack detailedForm;

        private string Antwort = "";

        //********************************************************************************************************************
        //                                                Konstruktoren
        //********************************************************************************************************************

        #region Konstruktor

        public RthTEC_V1()
        {
            InitializeComponent();

            //Com Port lesen
            HelpFCT.SetComPortBox(ComPort_select);

        }

        public RthTEC_V1(Form callingForm, int x, int y)
        {
            InitializeComponent();

            //Com Port lesen
            HelpFCT.SetComPortBox(ComPort_select);


            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "RthTEC_V1";
            this.Size = new System.Drawing.Size(515, 105);
            this.TabIndex = 34;

            //Hinzufügen
            callingForm.Controls.Add(this);
            
        }

        //Change Enable Status form MainForm
        public void Change_Enabled(Boolean input)
        {
            groupBox_XYZ.Invoke((MethodInvoker)delegate
            {
                groupBox_XYZ.Enabled = input;
            });
        }

        #endregion Konstruktor

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

        private void Button_Enable_Click(object sender, EventArgs e)
        {
            if (button_Enable.Text.Contains("Enable"))
            {
                button_Enable.Text = "Disable Outputs";
                SetEnable(true);
            }
            else
            {
                button_Enable.Text = "Enable Outputs";
                SetEnable(false);
            }
        }

        private void NumericUpDown_Repetations_ValueChanged(object sender, EventArgs e)
        {
            Cycles = (int)numericUpDown_Repetations.Value;
        }
        private void BarButtonItem_Reset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reset();
        }

        private void BarButtonItem_Detailed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Form erstellen
            detailedForm = new Window_RthTEC_Rack(this);
            //öffnen
            detailedForm.Show();
        }

        private void BarButtonItem_Log_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        #endregion GUI

        //********************************************************************************************************************
        //                                        globale Functions (Interface I_RthTEC)
        //********************************************************************************************************************

        #region 0. Reset
        public string Reset()
        {
            return Write_N_Read("RST");
        }

        #endregion 0. Reset

        #region 1. Enable

        public string SetEnable(bool input)
        {
            IsEnabled = input;

            //1 wenn angeschaltet werden soll
            if (input)
            {
                //Enable_Button.Text = "Disable";
                return Write_N_Read("SEN 1");
            }
            else
            {
                //Enable_Button.Text = "Enable";
                return Write_N_Read("SEN 0");
            }
        }

        #endregion 1. Enable

        #region 2. Start Pulse

        public string Start_std_Pulse(bool input)
        {
            if (input)
                return Write_N_Read("SPS 1");
            else
                return Write_N_Read("SPS 0");
        }

        public string Start_SEN_Pulse(bool input)
        {
            if (input)
                return Write_N_Read("SSS 1");
            else
                return Write_N_Read("SSS 0");
        }

        public string Start_DPA_Pulse(bool input)
        {
            if (input)
                return Write_N_Read("SDS 1");
            else
                return Write_N_Read("SDS 0");
        }

        public string Start_Pre_Pulse(bool input)
        {
            if (input)
                return Write_N_Read("SMS 1");
            else
                return Write_N_Read("SMS 0");
        }

        #endregion 2. Start Pulse

        #region 3.Puls-Einstellungen

        public string SetHeatTime(Decimal input)
        {
            Time_Heat = input;
            //Messzeit muss sicherheitshalber auch angepasst werden
            SetMeasTime(Time_Meas, Time_Heat);
            //Messzeit kann direkt übernommen werden
            if (IsConnected)
                return Write_N_Read("SHP " + input.ToString());
            else
                return "";
        }

        public string SetMeasTime(Decimal input_mess, Decimal input_heiz)
        {
            Time_Meas = input_mess;
            //Heiz- & Mess-Zeit müssen addiert werden
            if (IsConnected)
                return Write_N_Read("SMP " + (input_mess + input_heiz));
            else
                return "";
        }

        public string SetDPACount(Int32 input)
        {
            DPA_Count = input;
            //Messzeit kann direkt übernommen werden
            if (IsConnected)
                return Write_N_Read("SND " + input);
            else
                return "";
        }

        public string SetDPATime(Decimal input)
        {
            DPA_Time = input;

            //DPA Zeit ist in 100µs, deshalb mal 10
            if (IsConnected)
                return Write_N_Read("STD " + (input * 10).ToString("#"));
            else
                return "";
        }

        public string SetSlotActivation(bool input, int slot_nr)
        {
            string temp_str = "";
            int i = 0;

            //Aktuelle (Bereits Upgedated)
            for ( ; i < Slot_Count; i++)
            {
                if (Cards[i].IsActiv)
                    temp_str += "1";
                else
                    temp_str += "0";
            }

            //Bei berdarf mit nullen auffüllen
            for (; i < 8; i++)
            {
                temp_str += "0";
            }

            //Senden
            return Write_N_Read("SPR " + temp_str); ;
        }

        #endregion 3.Puls-Einstellungen

        //********************************************************************************************************************
        //                                              lokale Functions
        //********************************************************************************************************************

        #region Communication

        public string Write_N_Read(string message)
        {
            //Nachricht senden
            Serial_Interface.WriteLine(message);
            //Auslesen versuchen
            try
            {
                Antwort = Serial_Interface.ReadLine();
            }
            //Fehler bei Timeout schmeißen
            catch (TimeoutException)
            {
                Antwort = "TIMEOUT";
            }
            //Log Füllen
            Communication_LOG += DateTime.Now.ToString() +
                "\n     --> " + message +
                "\n     <-- " + Antwort + "\n";

            //in detail Window Status-Bar
            if (detailedForm != null)
            {
                detailedForm.statusBar_TextBox_Message.Text = message;
                detailedForm.statusBar_TextBox_Answer.Text = Antwort;
            }

            return Antwort;
        }

        public void Write_Only(string message)
        {
            //Nachricht senden
            Serial_Interface.WriteLine(message);

            //Log Füllen
            Communication_LOG += DateTime.Now.ToString() +
                "\n     --> " + message +
                "\n     <-- Was not wanted\n";
        }

        #endregion Communication

        #region Open/Close

        public void Open()
        {
            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                MessageBox.Show("A COM Port has to be selected! \n Try again.", "Warning",
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
                MessageBox.Show("COM Port is allready in use!\n Try again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("COM Port is not available!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            //Abfragen ob richtiges Gerät
            ID = Write_N_Read("GID");

            //Checken ob vom richtigen Typ
            if (!ID.Contains("RthTEC TTA-Equipment"))
            {
                MessageBox.Show("COM Port represents no Rth-Rack!\n Try again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Serial_Interface.Close();
                return;
            }

            //Ab hier zählt es als Verbunden
            IsConnected = true;

            //Reset
            Write_N_Read("RST");

            //Delay (Warten bis RESET fertig)
            Thread.Sleep(2000);

            //Slot Belegung abfragen und übernehmen und ob Aktiviert
            Get_Slot_Belegung();
            GetSlotActivation();

            //Zeiten abfragen
            GetHeatTime();
            GetMeasTime();
            GetDPACount();
            GetDPATime();

            //Enable deaktivieren
            SetEnable(false);

            //Oberfläche anpassen
            IsConnected = true;
            ComPort_select.Enabled = false;
            Button_OpenClose.Text = "Close";
            barButtonItem_Reset.Enabled = true;
            barButtonItem_Detailed.Enabled = true;
        }

        public void Close()
        {
            //COMPort schließen
            Serial_Interface.Close();

            //Flag
            IsConnected = false;

            //Oberfläche anpassen
            ComPort_select.Enabled = true;
            Button_OpenClose.Text = "Open";
            barButtonItem_Reset.Enabled = false;
            barButtonItem_Detailed.Enabled = false;
        }

        #endregion Open/Close

        #region 3.Puls-Einstellungen

        private decimal GetHeatTime()
        {
            string temp = Write_N_Read("GHP");

            Time_Heat = Convert.ToDecimal(temp.Substring(4, temp.LastIndexOf(' ') - 4));

            return Time_Heat;
        }

        private decimal GetMeasTime()
        {
            string temp = Write_N_Read("GMP");

            Time_Meas = Convert.ToDecimal(temp.Substring(4, temp.LastIndexOf(' ') - 4)) - Time_Heat;

            return Time_Meas;
        }

        private Int32 GetDPACount()
        {
            string temp = Write_N_Read("GND");

            DPA_Count = Convert.ToInt32(temp.Substring(4, temp.LastIndexOf(' ') - 4));

            return DPA_Count;
        }

        private decimal GetDPATime()
        {
            string temp = Write_N_Read("GTD");

            DPA_Time = Convert.ToDecimal(temp.Substring(4, temp.LastIndexOf(' ') - 4));

            return DPA_Time;
        }

        private bool[] GetSlotActivation()
        {
            bool[] temp_bool = new bool[8];

            string temp = Write_N_Read("GPR");
            temp = temp.Substring(4);

            for (int i = 0; i < Slot_Count; i++)
            {
                if (temp[i] == '1')
                {
                    temp_bool[i] = true;
                    Cards[i].IsActiv = true;
                }
                else
                {
                    temp_bool[i] = false;
                    Cards[i].IsActiv = false;
                }                  
            }
            return temp_bool;
        }

        #endregion 3.Puls-Einstellungen

        private void Get_Slot_Belegung()
        {
            //Nachricht senden und Antowrt empfangen (Ersten 4 Zeichen sing "GTC=")
            SlotBelegung = Write_N_Read("GCT").Substring(4).ToArray();

            //Werte übernehmen
            for (int i = 0; i < Slot_Count; i++)
            {
                switch (SlotBelegung[i])
                {
                    case '0':
                        Cards[i] = new Card_Empty(this, i + 1);
                        break;

                    case 'A':
                        Cards[i] = new Card_Amplifier(this, i + 1);
                        break;

                    case 'L':
                        Cards[i] = new Card_LED_Source(this, i + 1);
                        break;
                }
            }
        }

        //********************************************************************************************************************
        //                                              Auto Open
        //********************************************************************************************************************

        #region AutoOpen

        public void Update_settings(SerialCommunicationDivice myInput)
        {
            //Combox übernehen
            HelpFCT.SetComboBox2ComboBox(myInput.comboBox_Port, ComPort_select);

            //COM-Port eigenschaften übernehmen
            Serial_Interface = myInput.ToSerialPort();
        }

        public string AutoOpen(AutoConnect_Window myLoadScreen)
        {
            int iterration = 5;

            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                return "Rth-Rack: No COM-Port selected!" + Environment.NewLine; ;
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
                return "Rth-Rack: COM Port is allready in use!" + Environment.NewLine; ;
            }
            catch (System.IO.IOException)
            {
                return "Rth-Rack: COM Port is not available!" + Environment.NewLine; ;
            }

            myLoadScreen.ChangeTask("Checking device ...", iterration);

            //Abfragen ob richtiges Gerät
            ID = Write_N_Read("GID");

            //Checken ob vom richtigen Typ
            if (!ID.Contains("RthTEC TTA-Equipment"))
            {
                Serial_Interface.Close();
                return "Rth-Rack: COM Port represents no Rth-Rack!" + Environment.NewLine; ;
            }

            //Ab hier zählt es als Verbunden
            IsConnected = true;


            //Reset
            myLoadScreen.ChangeTask("Reset ...", iterration);
            Write_N_Read("RST");

            //Delay (Warten bis RESET fertig)
            Thread.Sleep(2000);

            myLoadScreen.ChangeTask("Load Setup ...", iterration);

            //Slot Belegung abfragen und übernehmen und ob Aktiviert
            Get_Slot_Belegung();
            GetSlotActivation();

            //Zeiten abfragen
            GetHeatTime();
            GetMeasTime();
            GetDPACount();
            GetDPATime();

            //Enable deaktivieren
            SetEnable(false);

            //Oberfläche anpassen
            IsConnected = true;
            ComPort_select.Enabled = false;
            Button_OpenClose.Text = "Close";
            barButtonItem_Reset.Enabled = true;
            barButtonItem_Detailed.Enabled = true;

            return "";
        }

        #endregion AutoOpen

        //**************************************************************************************************
        //                                    FCT für Setting-Files
        //**************************************************************************************************

        public override string ToString()
        {
            string text = "*Rth-Rack:" + Environment.NewLine;

            //text += "I_Heat[mA]: " + I_Heat.ToString() + Environment.NewLine;
            text += "t_Heat[ms]: " + Time_Heat.ToString() + Environment.NewLine;
            //text += "I_Meas[ms]: " + I_Meas.ToString() + Environment.NewLine;
            text += "t_Meas[ms]: " + Time_Meas.ToString() + Environment.NewLine;
            //text += "Repetitions: " + Cycles.ToString() + Environment.NewLine;

            return text;
        }

        public void FromString(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                /*
                if (input[i].StartsWith("I_Heat[mA]:"))
                    UI_Heat_Current.Value = Convert.ToDecimal(input[i].Substring(12));
                else if (input[i].StartsWith("t_Heat[ms]:"))
                    UI_Heat_Time.Value = Convert.ToDecimal(input[i].Substring(12));
                else if (input[i].StartsWith("I_Meas[ms]:"))
                    UI_Meas_Current.Value = Convert.ToDecimal(input[i].Substring(12));
                else if (input[i].StartsWith("t_Meas[ms]:"))
                    UI_Meas_Time.Value = Convert.ToDecimal(input[i].Substring(12));
                else if (input[i].StartsWith("Repetitions:"))
                    UI_Cycles.Value = Convert.ToDecimal(input[i].Substring(13));
                    */

            }
        }


    }
}
