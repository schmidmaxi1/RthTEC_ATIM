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


using ATIM_GUI._4_Settings;

using AutoConnect;
using Power_Supply_HamegHMP;

//using Hameg_HMP_namespace;

namespace _8_Rth_TEC_Rack
{
    public partial class RthTEC_Rack : UserControl
    {
        //***********************Variablen********************************************
        public SerialPort Serial_Interface { get; set; } = new SerialPort()
        {
            BaudRate = 57600,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = 0,
            ReadTimeout = 500
        };
        public string Antwort { get; internal set; }
        public string Communication_LOG { get; internal set; }

        public Boolean IsConnected { get; internal set; } = false;
        public Boolean IsEnabled { get; internal set; } = false;

        public string DeviceType { get; internal set; }

        public decimal U_Heat { get; internal set; } = 10;
        public decimal I_Heat { get; internal set; } = 500;
        public decimal Time_Heat { get; internal set; } = 1000;
        public decimal U_Meas { get; internal set; } = 10;
        public decimal I_Meas { get; internal set; } = 20;
        public decimal Time_Meas { get; internal set; } = 1000;

        public string DUT_Type { get; internal set; }

        public uint Cycles { get; internal set; } = 1;

        public decimal U_offset { get; internal set; } = 3000;

        public decimal Gain { get; internal set; } = 2;

        private bool HMP_on_GUI = false;
        public PowerSupply_HMP usedHMP = null;

        private string str_slot = "0";
        private string str_type = "L";

        //Default Ranges
        #region Ranges
        private decimal range_LED_I_Heat_min = 0;
        private decimal range_LED_I_Heat_max = 1500;
        private decimal range_LED_I_Meas_min = 5;
        private decimal range_LED_I_Meas_max = 25;
        private decimal range_LED_U_Heat_min = 0;
        private decimal range_LED_U_Heat_max = 20;


        private decimal range_MOSFET_I_Heat_min = 0;
        private decimal range_MOSFET_I_Heat_max = 5000;
        private decimal range_MOSFET_I_Meas_min = 5;
        private decimal range_MOSFET_I_Meas_max = 25;
        private decimal range_MOSFET_U_Heat_min = 0;
        private decimal range_MOSFET_U_Heat_max = 20;
        private decimal range_MOSFET_U_Meas_min = 0;
        private decimal range_MOSFET_U_Meas_max = 20;

        private decimal range_Booster_I_Heat_min = 0;
        private decimal range_Booster_I_Heat_max = 10000;
        private decimal range_Booster_I_Meas_min = 5;
        private decimal range_Booster_I_Meas_max = 25;
        private decimal range_Booster_U_Heat_min = 0;
        private decimal range_Booster_U_Heat_max = 20;
        #endregion Ranges

        //********************************************************************************************************************
        //                                       UI - Kontext-Menü für Button
        //********************************************************************************************************************

        ContextMenuStrip cm = new ContextMenuStrip();

        ToolStripMenuItem Item_Reset = new ToolStripMenuItem()
        {
            Name = "Reset",
            Text = "Reset",
            Enabled = false,
            //Image = 
        };
        ToolStripMenuItem Item_LOG = new ToolStripMenuItem()
        {
            Name = "LOG",
            Text = "Show communication log",
            Enabled = true,
        };

        //********************************************************************************************************************
        //                                          Initalisierung
        //********************************************************************************************************************

        public RthTEC_Rack()
        {
            InitializeComponent();

            //Alle ComPorts suchen
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                ComPort_select.Items.Add(port);


            //Komponenten-Auswahl
            string[] kindOfComponents = new string[] { "LED", "MOSFET", "Booster" };
            foreach (string component in kindOfComponents)
                UI_KindOfComponent.Items.Add(component);

            //LED als default
            UI_KindOfComponent.SelectedItem = "LED";

            //Context Menü für Button Open&Close hinzufügen
            Item_Reset.Click += new System.EventHandler(this.MenuItem_Reset_Click);
            Item_LOG.Click += new System.EventHandler(this.MenuItem_LOG_Click);

            cm.Items.Add(Item_Reset);
            cm.Items.Add(Item_LOG);

            OpenClose.ContextMenuStrip = cm;
        }

        //Change Enable Status form MainForm
        public void Change_Enabled(Boolean input)
        {
            groupBox_RthTEC.Invoke((MethodInvoker)delegate
            {
                groupBox_RthTEC.Enabled = input;
            });
        }


        //********************************************************************************************************************
        //                                          Button Events
        //********************************************************************************************************************

        #region Button_Events

        public void OpenClose_Click(object sender, EventArgs e)
        {
            if (!IsConnected)
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
                DeviceType = Write_N_Read("GID");

                //Checken ob vom richtigen Typ
                if (!DeviceType.Contains("RthTEC TTA-Equipment"))
                {
                    MessageBox.Show("COM Port represents no Rth-Rack!\n Try again.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Serial_Interface.Close();
                    return;
                }

                //Reset
                Write_N_Read("RST");

                //Delay
                Thread.Sleep(2000);

                //Default Einstellungen setzen
                SetEnable(false);

                //SetDefaultSetup();

                //Oberfläche anpassen
                IsConnected = true;

                //Reset_Button.Enabled = true;
                TestPulse.Enabled = true;
                Enable_Button.Enabled = true;

                UI_KindOfComponent.Enabled = true;
                UI_Heat_Time.Enabled = true;
                UI_Heat_Current.Enabled = true;
                UI_Heat_Voltage.Enabled = true;
                UI_Meas_Time.Enabled = true;
                UI_Meas_Current.Enabled = true;
                UI_Meas_Voltage.Enabled = true;
                UI_Cycles.Enabled = true;
                UI_Offset_Voltage.Enabled = true;


                ComPort_select.Enabled = false;

                OpenClose.Text = "Close";

                cm.Items["Reset"].Enabled = true;

            }

            else
            {


                //COMPort schließen
                Serial_Interface.Close();

                //Oberfläche anpassen
                IsConnected = false;

                //Reset_Button.Enabled = false;
                TestPulse.Enabled = false;
                Enable_Button.Enabled = false;

                UI_KindOfComponent.Enabled = false;
                UI_Heat_Time.Enabled = false;
                UI_Heat_Current.Enabled = false;
                UI_Heat_Voltage.Enabled = false;
                UI_Meas_Time.Enabled = false;
                UI_Meas_Current.Enabled = false;
                UI_Meas_Voltage.Enabled = false;
                UI_Cycles.Enabled = false;
                UI_Offset_Voltage.Enabled = false;

                ComPort_select.Enabled = true;

                OpenClose.Text = "Open";

                cm.Items["Reset"].Enabled = false;
            }
        }

        private void Enable_Button_Click(object sender, EventArgs e)
        {
            SetEnable(!IsEnabled);

            UI_Heat_Time_ValueChanged(sender, e);
            UI_Heat_Current_ValueChanged(sender, e);
            UI_Heat_Voltage_ValueChanged(sender, e);
            UI_Meas_Time_ValueChanged(sender, e);
            UI_Meas_Current_ValueChanged(sender, e);
            UI_Meas_Voltage_ValueChanged(sender, e);
            UI_Cycles_ValueChanged(sender, e);
            UI_Offset_Voltage_ValueChanged(sender, e);
        }


        private void TestPulse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Cycles; i++)
                SinglePuls_withDelay();

            string test = GetHeat_Voltage();
            test += GetMeas_Voltage();

            MessageBox.Show(test);
        }

        private void MenuItem_Reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void MenuItem_LOG_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LOG:\n" + Communication_LOG, "Communication log Rth-Rack table");
        }


        private void UI_KindOfComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (UI_KindOfComponent.SelectedItem)
            {
                case "LED":

                    //String zum erzeugen der Nachrichten
                    str_type = "L";
                    str_slot = "0";
                    DUT_Type = "LED";

                    //Heiz- und Mess-Spannung ausbleden (nicht möglich)
                    UI_Heat_Voltage.Visible = false;
                    UI_Meas_Voltage.Visible = false;
                    UI_Lable_U_Heat.Visible = false;
                    UI_Lable_U_Meas.Visible = false;

                    //Range umstellen
                    UI_Heat_Current.Minimum = range_LED_I_Heat_min;
                    UI_Heat_Current.Maximum = range_LED_I_Heat_max;
                    UI_Meas_Current.Minimum = range_LED_I_Meas_min;
                    UI_Meas_Current.Maximum = range_LED_I_Meas_max;
                    UI_Heat_Voltage.Minimum = range_LED_U_Heat_min;
                    UI_Heat_Voltage.Maximum = range_LED_U_Heat_max;

                    //Heiz-Spannung bei Hameg wirder zugänglich
                    if (HMP_on_GUI)
                    {
                        if (usedHMP.IsConnected)
                            usedHMP.Voltage_Heat.Enabled = true;
                    }

                    break;

                case "MOSFET":
                    //Abfragen ob Hameg schon verbunden
                    if (HMP_on_GUI)
                    {
                        if (!usedHMP.IsConnected)
                        {
                            MessageBox.Show("Power Supply is not connected!");
                            UI_KindOfComponent.SelectedItem = "LED";
                            break;
                        }
                    }
                    //String zum erzeugen der Nachrichten
                    str_type = "M";
                    str_slot = "2";
                    DUT_Type = "MOSFET/IGBT";

                    //Heiz- und Mess-Spannung einblenden
                    UI_Heat_Voltage.Visible = true;
                    UI_Meas_Voltage.Visible = true;
                    UI_Lable_U_Heat.Visible = true;
                    UI_Lable_U_Meas.Visible = true;

                    //Range umstellen
                    UI_Heat_Current.Minimum = range_MOSFET_I_Heat_min;
                    UI_Heat_Current.Maximum = range_MOSFET_I_Heat_max;
                    UI_Meas_Current.Minimum = range_MOSFET_I_Meas_min;
                    UI_Meas_Current.Maximum = range_MOSFET_I_Meas_max;
                    UI_Heat_Voltage.Minimum = range_MOSFET_U_Heat_min;
                    UI_Heat_Voltage.Maximum = range_MOSFET_U_Heat_max;
                    UI_Meas_Voltage.Minimum = range_MOSFET_U_Meas_min;
                    UI_Meas_Voltage.Maximum = range_MOSFET_U_Meas_max;

                    //Heiz-Spannung bei Hameg Sperren
                    if (HMP_on_GUI)
                    {
                        usedHMP.Voltage_Heat.Enabled = false;
                    }

                    

                    break;

                case "Booster":
                    //String zum erzeugen der Nachrichten
                    str_type = "B";
                    DUT_Type = "Booster";

                    //Heiz- und Mess-Spannung ausbleden (nicht möglich)
                    UI_Heat_Voltage.Visible = false;
                    UI_Meas_Voltage.Visible = false;
                    UI_Lable_U_Heat.Visible = false;
                    UI_Lable_U_Meas.Visible = false;

                    //Range umstellen
                    UI_Heat_Current.Minimum = range_Booster_I_Heat_min;
                    UI_Heat_Current.Maximum = range_Booster_I_Heat_max;
                    UI_Meas_Current.Minimum = range_Booster_I_Meas_min;
                    UI_Meas_Current.Maximum = range_Booster_I_Meas_max;
                    UI_Heat_Voltage.Minimum = range_Booster_U_Heat_min;
                    UI_Heat_Voltage.Maximum = range_Booster_U_Heat_max;

                    //Heiz-Spannung bei Hameg wirder zugänglich
                    if (HMP_on_GUI)
                    {
                        if (usedHMP.IsConnected)
                            usedHMP.Voltage_Heat.Enabled = true;
                    }

                    break;
            }
        }

        private void UI_Heat_Time_ValueChanged(object sender, EventArgs e)
        {
            SetHeatTime(UI_Heat_Time.Value);
        }

        private void UI_Heat_Current_ValueChanged(object sender, EventArgs e)
        {
            SetHeatCurrent(UI_Heat_Current.Value);
        }

        private void UI_Heat_Voltage_ValueChanged(object sender, EventArgs e)
        {
            SetHeatVoltage(UI_Heat_Voltage.Value);
        }

        private void UI_Meas_Time_ValueChanged(object sender, EventArgs e)
        {
            SetMeasTime(UI_Meas_Time.Value, UI_Heat_Time.Value);
        }

        private void UI_Meas_Current_ValueChanged(object sender, EventArgs e)
        {
            SetMeasCurrent(UI_Meas_Current.Value);
        }

        private void UI_Meas_Voltage_ValueChanged(object sender, EventArgs e)
        {
            SetMeasVoltage(UI_Meas_Voltage.Value);
        }

        private void UI_Cycles_ValueChanged(object sender, EventArgs e)
        {
            Cycles = Convert.ToUInt16(UI_Cycles.Value);
        }

        private void UI_Offset_Voltage_ValueChanged(object sender, EventArgs e)
        {
            SetOffsetVoltage(UI_Offset_Voltage.Value);
        }

        #endregion Button_Events

        //********************************************************************************************************************
        //                                           Nachrichten - Befehle
        //********************************************************************************************************************

        private string Write_N_Read(string message)
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

            return Antwort;
        }

        private void Write_Only(string message)
        {
            //Nachricht senden
            Serial_Interface.WriteLine(message);

            //Log Füllen
            Communication_LOG += DateTime.Now.ToString() +
                "\n     --> " + message +
                "\n     <-- Was not wanted\n";
        }

        //********************************************************************************************************************
        //                                              PulsBefehle-Befehle Befehle
        //********************************************************************************************************************

        #region Haupt_Befehle

        public string SinglePuls_withDelay()
        {
            //Puls Starten
            string ausgabe = Write_N_Read("SPS 1");

            //Warten bis Puls zu ende (Umrechnung notwendig um von dezimal auf int zu kommen)
            System.Threading.Thread.Sleep((int)(Time_Heat + Time_Meas));

            //Rückgabe
            return ausgabe;
        }

        public string SinglePuls_withoutDelay()
        {
            return Write_N_Read("SPS 1");
        }

        public string MeasCurrent_on()
        {
            return Write_N_Read("SKS 1");
        }

        public string MeasCurrent_off()
        {
            return Write_N_Read("SKS 0");
        }

        public string Deterministic_Pulse_Start()
        {
            return Write_N_Read("SDS 1");
        }

        #endregion Haupt_Befehle

        //********************************************************************************************************************
        //                                             Einstell-Befehle
        //********************************************************************************************************************

        #region Hilfsbefehle
        public string SetHeatCurrent(Decimal input)
        {
            I_Heat = input;
            UI_Heat_Current.Value = input;
            //Beim Heizstrom kann direkt übernommen werden
            if (IsConnected)
                return Write_N_Read("SHC" + str_slot + str_type + " " + input);
            else
                return "";
        }

        public string SetMeasCurrent(Decimal input)
        {
            I_Meas = input;
            UI_Meas_Current.Value = input;
            //Messstrom muss mit 10 Multipliziert werden
            if (IsConnected)
                return Write_N_Read("SMC" + str_slot + str_type + " " + (input * 10));
            else
                return "";
            
        }

        public string SetHeatVoltage(Decimal input)
        {
            //Hameg HMP muss angepasst werden (keine Kommunikation mit µC
            if (HMP_on_GUI)
            {
                usedHMP.SetVoltage(input + 0.5m, 4);
            }
            return "";
        }

        public string SetMeasVoltage(Decimal input)
        {
            U_Meas = input;
            UI_Meas_Voltage.Value = input;
            //Messspannung muss mit 100 multipliziert  werden
            if (IsConnected)
                return Write_N_Read("SMV" + str_slot + str_type + " " + (input * 100).ToString("0000"));
            else
                return "";
            
        }

        public string SetHeatTime(Decimal input)
        {
            Time_Heat = input;
            UI_Heat_Time.Value = input;
            //Messzeit muss sicherheitshalber auch angepasst werden
            SetMeasTime(Time_Meas, Time_Heat);
            //Messzeit kann direkt übernommen werden
            if (IsConnected)
                return Write_N_Read("SHP " + input);
            else
                return "";
            
        }

        public string SetMeasTime(Decimal input_mess, Decimal input_heiz)
        {
            Time_Meas = input_mess;
            UI_Meas_Time.Value = input_mess;
            //Heiz- & Mess-Zeit müssen addiert werden
            if (IsConnected)
                return Write_N_Read("SMP " + (input_mess + input_heiz));
            else
                return "";           
        }

        public string SetEnable(bool input)
        {
            IsEnabled = input;

            //1 wenn angeschaltet werden soll
            if (input)
            {
                Enable_Button.Text = "Disable";
                return Write_N_Read("SEN 1");
            }
            else
            {
                Enable_Button.Text = "Enable";
                return Write_N_Read("SEN 0");
            }
        }

        public string Reset()
        {
            return Write_N_Read("RST");
        }


        //Mess-Karte
        public string SetOffsetVoltage(Decimal input)
        {
            U_offset = input;
            //Betrug am System -->Widerstände auf FrontEnd getausch
            input = input / 2;

            //Messzeit kann direkt übernommen werden
            if (IsConnected)
                return Write_N_Read("SWO" + "1" + "A " + input);
            else
                return "";            
        }

        public string GetHeat_Voltage()
        {
            if (IsConnected)
                return Write_N_Read("GHV");
            else
                return "";
        }

        public string GetMeas_Voltage()
        {
            if (IsConnected)
                return Write_N_Read("GMV");
            else
                return "";
        }

        #endregion Hilfsbefehle

        //********************************************************************************************************************
        //                                           AutoConnect
        //********************************************************************************************************************

        public string AutoOpen(AutoConnect_Window myLoadScreen)
        {
            int iterration = 5;

            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                return "Rth-Rack: No COM-Port selected!" + Environment.NewLine;
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
                return "Rth-Rack: COM Port is allready in use!" + Environment.NewLine;
            }
            catch (System.IO.IOException)
            {
                return "Rth-Rack: COM Port is not available!" + Environment.NewLine;
            }

            myLoadScreen.ChangeTask("Checking device ...", iterration);

            //Abfragen ob richtiges Gerät
            DeviceType = Write_N_Read("GID");

            //Checken ob vom richtigen Typ
            if (!DeviceType.Contains("RthTEC TTA-Equipment"))
            {
                Serial_Interface.Close();
                return "Rth-Rack: COM Port represents no Rth-Rack!" + Environment.NewLine;
            }

            myLoadScreen.ChangeTask("Reset ...", iterration);
            //Reset
            Write_N_Read("RST");

            //Delay
            Thread.Sleep(2000);

            //Default Einstellungen setzen
            SetEnable(false);

            //SetDefaultSetup();

            myLoadScreen.ChangeTask("Change GUI ...", iterration);

            //Oberfläche anpassen
            IsConnected = true;

            //Reset_Button.Enabled = true;
            TestPulse.Enabled = true;
            Enable_Button.Enabled = true;

            UI_KindOfComponent.Enabled = true;
            UI_Heat_Time.Enabled = true;
            UI_Heat_Current.Enabled = true;
            UI_Heat_Voltage.Enabled = true;
            UI_Meas_Time.Enabled = true;
            UI_Meas_Current.Enabled = true;
            UI_Meas_Voltage.Enabled = true;
            UI_Cycles.Enabled = true;
            UI_Offset_Voltage.Enabled = true;


            ComPort_select.Enabled = false;

            OpenClose.Text = "Close";

            cm.Items["Reset"].Enabled = true;

            return "";

        }

        public void AutoSettings(Settings_ATIM mySettings)
        {
            UI_KindOfComponent.SelectedItem = mySettings.Device;

            UI_Heat_Current.Value = mySettings.Heat_current;
            UI_Heat_Time.Value = mySettings.Heat_time;
            UI_Heat_Voltage.Value = mySettings.Heat_voltage;

            UI_Meas_Current.Value = mySettings.Meas_current;
            UI_Meas_Time.Value = mySettings.Meas_time;
            UI_Meas_Voltage.Value = mySettings.Meas_voltage;

            UI_Offset_Voltage.Value = mySettings.Offset_voltage;

            UI_Cycles.Value = mySettings.Cycles;          
        }

        //**************************************************************************************************
        //                                    FCT für Setting-Files
        //**************************************************************************************************

        public override string ToString()
        {
            string text = "*Rth-Rack:" + Environment.NewLine;

            text += "I_Heat[mA]: " + I_Heat.ToString() + Environment.NewLine;
            text += "t_Heat[ms]: " + Time_Heat.ToString() + Environment.NewLine;
            text += "I_Meas[ms]: " + I_Meas.ToString() + Environment.NewLine;
            text += "t_Meas[ms]: " + Time_Meas.ToString() + Environment.NewLine;
            text += "Repetitions: " + Cycles.ToString() + Environment.NewLine;

            return text;
        }

        public void FromString(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
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

            }
        }
    }
}