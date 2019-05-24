using MeSoft.MeCom.Core;
//TEC-Libaries
using MeSoft.MeCom.PhyWrapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;

using System.Windows.Forms;


//using ATIM_GUI._2_AutoConnect;

namespace TEC_Controller
{
    public partial class Meerstetter_4fach : UserControl, I_TEC_Controller
    {
        //********************************************************************************************************************
        //                                              Variables
        //********************************************************************************************************************

        #region Variables

        //***********************Interface-Variablen***********************************
        public bool IsConnected { get; internal set; } = false;
        public bool IsRunning { get; internal set; } = false;

        public bool Stable_for_30sec { get; internal set; } = false;
        public int Counter_0_5sec { get; internal set; } = 0;

        public float Target_temp_aver { get; internal set; } = 25;
        public float Meas_temp_aver { get; internal set; }


        //************************Special-Variablen*************************************
        private static float Temp_abweichung { get; } = 0.05f;

        public static uint Nr_of_channels { get; internal set; } = 4;
        public static uint Nr_of_TECs { get; internal set; } = 2;

        public bool[] TEC_on { get; internal set; } = new bool[] { false, false, false, false };

        public float[] Target_temp { get; internal set; } = new float[Nr_of_channels];
        public float[] Meas_temp { get; internal set; } = new float[Nr_of_channels];

        public float[] Meas_current { get; internal set; } = new float[Nr_of_channels];
        public float[] Meas_voltage { get; internal set; } = new float[Nr_of_channels];
        public float[] Meas_sink_temp { get; internal set; } = new float[Nr_of_channels];

        public float[] Value_P { get; internal set; } = new float[Nr_of_channels];
        public float[] Value_I { get; internal set; } = new float[Nr_of_channels];
        public float[] Value_D { get; internal set; } = new float[Nr_of_channels];


        private bool Flag_Temp_Changed { get; set; } = false;
        private float New_Temp_kFactor { get; set; } = 0;


        //Liste mit allen Registern
        private string path_Init_File = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\0_Initialisation_Files\\TEC.ini";
        private string[] initalisationFile;
        public List<Meerstetter_Registers>[] registers = { new List<Meerstetter_Registers>(), new List<Meerstetter_Registers>() };

        //Connection-Variables
        private IMeComPhy[] TEC_Connection = new IMeComPhy[2];
        public MeComPhySerialPort[] MyMeComPhySerialPort { get; set; } = new MeComPhySerialPort[2];
        
        //Timer Thread
        private Parallel_Thread_TEC threath_05sec_timer;

        //Second Window
        public Meerstetter_4fach_Detailed TEC_Detailed { get; internal set; }
        public bool WindowOpen { get; internal set; } = false;

        #endregion Variables

        //********************************************************************************************************************
        //                                          Initalisierung
        //********************************************************************************************************************

        public Meerstetter_4fach()
        {

            InitializeComponent();

            //Alle ComPorts suchen
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                ComPort_select1.Items.Add(port);
            ComPort_select1.SelectedIndex = 0;

            foreach (string port in ports)
                ComPort_select2.Items.Add(port);
            ComPort_select2.SelectedIndex = 0;

            //Read Init-File und in lokale Register einsortieren
            initalisationFile = File.ReadAllLines(path_Init_File);
            Fill_Registers_without_Values(0);
            Fill_Registers_without_Values(1);
            Fill_Register_Values(0);
            Fill_Register_Values(1);
        }

        //Einfaches einfügen in Andere Fenster
        public Meerstetter_4fach(Form callingForm, int x, int y)
        {
            InitializeComponent();

            //Alle ComPorts suchen
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                ComPort_select1.Items.Add(port);
            ComPort_select1.SelectedIndex = 0;

            foreach (string port in ports)
                ComPort_select2.Items.Add(port);
            ComPort_select2.SelectedIndex = 0;

            //Read Init-File und in lokale Register einsortieren
            initalisationFile = File.ReadAllLines(path_Init_File);
            Fill_Registers_without_Values(0);
            Fill_Registers_without_Values(1);
            Fill_Register_Values(0);
            Fill_Register_Values(1);

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "Meerstetter_4fach";
            this.Size = new System.Drawing.Size(515, 120);
            this.TabIndex = 30;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }
        
        //Change Enable Status form MainForm
        public void Change_Enabled(Boolean input)
        {
            groupBox_TEC.Invoke((MethodInvoker)delegate
            {
                groupBox_TEC.Enabled = input;
            });
        }

        //********************************************************************************************************************
        //                                            GUI-Events
        //********************************************************************************************************************

        #region GUI

        private void OpenClose_Click(object sender, EventArgs e)
        {
            if (!IsConnected)
            {
                if (TEC_Open())
                {
                    //Oberfläche anpassen
                    ComPort_select1.Enabled = false;
                    ComPort_select2.Enabled = false;

                    UI_TargetTemp.Enabled = true;

                    barButtonItem_Fan.Enabled = true;
                    barButtonItem_OnOff.Enabled = true;

                    OpenClose.Text = "Close";

                    //TEC anschalten
                    Switch_Channel_OnOff(true);
                    //Fan anschalten
                    Switch_Fan_OnOff(true);                  
                }
            }

            else
            {
                TEC_Close();

                //Oberfläche anpassen
                ComPort_select1.Enabled = true;
                ComPort_select2.Enabled = true;

                UI_TargetTemp.Enabled = false;

                barButtonItem_Fan.Enabled = false;
                barButtonItem_OnOff.Enabled = false;

                OpenClose.Text = "Open";
            }

        }

        private void UI_TargetTemp_ValueChanged(object sender, EventArgs e)
        {
            SetTemperature((float)UI_TargetTemp.Value);
        }

        private void BarButtonItem_LOG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("Not implementet yet!");
        }

        private void BarButtonItem_Detailed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Fenster erzeugen und öffnen
            TEC_Detailed = new Meerstetter_4fach_Detailed(this);
            TEC_Detailed.Show();
            //GroupBox disablen (keine änderungen hier möglich)
            groupBox_TEC.Enabled = false;
            //Flag Setzen
            WindowOpen = true;
        }

        private void BarButtonItem_OnOff_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barButtonItem_OnOff.Caption.Contains("on"))
                Switch_Channel_OnOff(true);
            else
                Switch_Channel_OnOff(false);
        }

        private void BarButtonItem_Fan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barButtonItem_Fan.Caption.Contains("on"))
                Switch_Fan_OnOff(true);
            else
                Switch_Fan_OnOff(false);
        }

        #endregion GUI

        //********************************************************************************************************************
        //                                              Interface Functions
        //********************************************************************************************************************

        public void SetTemperature_w_TimerStop(float newTemp)
        {
            //Flags müssen hier umgeschieben werden, da Timer nicht immer ausgelöst wird
            Stable_for_30sec = false;
            Counter_0_5sec = 0;

            //Flag setzen
            Flag_Temp_Changed = true;

            //Target anpassen
            New_Temp_kFactor = newTemp;

            /*  Alt
            threath_05sec_timer.timer_05sec.Stop();

            SetTemperature(newTemp);

            threath_05sec_timer.timer_05sec.Start();
            */
    }

        public void SetTemperature(float newTemp)
        {
            //Flags müssen hier umgeschieben werden, da Timer nicht immer ausgelöst wird
            Stable_for_30sec = false;
            Counter_0_5sec = 0;

            for (int i = 1; i <= Nr_of_channels; i++)
                //for(int j = 0; j < Nr_of_TECs; j++)
                    SetTemperature(newTemp, i);
        }

        public void Switch_Channel_OnOff(bool value)
        {
            //An oder Ausschalten
            for (int i = 1; i <= Nr_of_channels; i++)
                //for (int j = 0; j < Nr_of_TECs; j++)
                    Switch_Channel_OnOff(value, i);
            //Button in DropDown anpassen
            if (value)
                barButtonItem_OnOff.Caption = "Switch TEC off";
            else
                barButtonItem_OnOff.Caption = "Switch TEC on";
        }

        public void Switch_Fan_OnOff(bool value)
        {
            //An oder Ausschalten
            for (int i = 1; i <= Nr_of_channels; i++)
                //for (int j = 0; j < Nr_of_TECs; j++)
                    Switch_Fan_OnOff(value, i);
            //Button in DropDown anpassen
            if (value)
                barButtonItem_Fan.Caption = "Switch fan off";
            else
                barButtonItem_Fan.Caption = "Switch fan on";
        }

        //********************************************************************************************************************
        //                                                Open / Close
        //********************************************************************************************************************

        public Boolean TEC_Open()
        {
            if (ComPort_select1.Text == "" | ComPort_select2.Text == "")   //Wenn kein Port gewählt ist
            {
                MessageBox.Show("A COM Port has to be selected! \nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //Neuen ComPort erstellen
            MyMeComPhySerialPort[0] = new MeComPhySerialPort()
            {
                ReadTimeout = 500,
            };
            //Neuen ComPort erstellen
            MyMeComPhySerialPort[1] = new MeComPhySerialPort()
            {
                ReadTimeout = 500,
            };


            //Verbindung aufbauen
            try
            {
                MyMeComPhySerialPort[0].OpenWithDefaultSettings(ComPort_select1.Text, 57600);
                MyMeComPhySerialPort[1].OpenWithDefaultSettings(ComPort_select2.Text, 57600);
                TEC_Connection[0] = MyMeComPhySerialPort[0];
                TEC_Connection[1] = MyMeComPhySerialPort[1];
            }
            catch (UnauthorizedAccessException)
            {
                //Wenn es nicht funktioniert --> abbrechen
                MessageBox.Show("COM Port is allready in use!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("COM Port is not available!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            //Abfragen ob richtiges Gerät
            Int32 []diviceType = { 0, 0 };
            try
            {
                diviceType[0] = ReadValueInt("DeviceType", 1, 0);
                diviceType[1] = ReadValueInt("DeviceType", 1, 1);
            }
            catch (MeComPhyTimeoutException)
            {
                MessageBox.Show("COM Port represents no Meerstetter TEC Controller!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MyMeComPhySerialPort[0].Close();
                MyMeComPhySerialPort[1].Close();
                return false;
            }
            catch (ComCommandException)
            {
                MessageBox.Show("COM Port represents no Meerstetter TEC Controller!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MyMeComPhySerialPort[0].Close();
                MyMeComPhySerialPort[1].Close();
                return false;
            }

            if (diviceType[0] != 1122)
            {
                MessageBox.Show("Not Correct Device 1: TEC-" + diviceType.ToString() + "selected", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MyMeComPhySerialPort[0].Close();
                MyMeComPhySerialPort[1].Close();
                return false;
            }
            if (diviceType[1] != 1122)
            {
                MessageBox.Show("Not Correct Device 2: TEC-" + diviceType.ToString() + "selected", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MyMeComPhySerialPort[0].Close();
                MyMeComPhySerialPort[1].Close();
                return false;
            }

            //Jetzt ist verbunden
            IsConnected = true;

            //Alle Daten aus ini-File senden/////////////////////////////////////////////////////////////////////
            SendRegisters2TEC_Controller(0);
            SendRegisters2TEC_Controller(1);

            //Eingestellte Temperatur auslesen
            Target_temp_aver = (GetTargetTemperature(1) + GetTargetTemperature(2) + GetTargetTemperature(3) + GetTargetTemperature(4)) / 4;
            UI_TargetTemp.Value = (decimal)Target_temp_aver;

            //Timer Starten
            threath_05sec_timer = new Parallel_Thread_TEC();
            threath_05sec_timer.Event_OnParallelThread += new OnParaThreadHandler(OnThreath_05sec_timer);
            threath_05sec_timer.timer_05sec.Start();

            return true;
        }

        public void TEC_Close()
        {
            //Timer Stoppen
            threath_05sec_timer.timer_05sec.Stop();

            //COMPort schließen
            MyMeComPhySerialPort[0].Close();
            MyMeComPhySerialPort[1].Close();

            //Oberfläche anpassen
            IsConnected = false;
            UI_MeasuredTemp1.BackColor = SystemColors.Control;
            UI_MeasuredTemp2.BackColor = SystemColors.Control;
        }

        //********************************************************************************************************************
        //                                              Other Functions
        //********************************************************************************************************************

        #region MainCommands

        private void New_ChannelAndTECNr(int channel_in, out int channel_new, out int tEC_Nr_new)
        {
            if (channel_in == 1)
            {
                channel_new = 1;
                tEC_Nr_new = 0;
            }
            else if (channel_in == 2)
            {
                channel_new = 2;
                tEC_Nr_new = 0;
            }
            else if (channel_in == 3)
            {
                channel_new = 1;
                tEC_Nr_new = 1;
            }
            else if (channel_in == 4)
            {
                channel_new = 2;
                tEC_Nr_new = 1;
            }
            else
            {
                //Fehler durch out of Range
                channel_new = 3;
                tEC_Nr_new = 3;
            }
        }

        public void SetTemperature(float newTemp, int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert intern übernehmen
            Target_temp[channel - 1] = newTemp;
            //Senden
            ChangeRegister_float("Target Object Temp", channel_new, newTemp, tEC_Nr);
        }

        public void Switch_Channel_OnOff(bool switch_on, int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert intern übernehmen
            TEC_on[channel - 1] = switch_on;
            //Senden
            if (switch_on)
                ChangeRegister_int("Status", channel_new, 1, tEC_Nr);
            else
                ChangeRegister_int("Status", channel_new, 0, tEC_Nr);

            //Wenn alle an dann Flag IsRunning setzen
            Boolean temp_flag = true;
            foreach (Boolean single_flag in TEC_on)
                temp_flag = temp_flag & single_flag;

            IsRunning = temp_flag;
        }

        public void Switch_Fan_OnOff(bool switch_on, int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            //Senden
            if (switch_on)
                ChangeRegister_int("FAN Control Enable", channel_new, 1, tEC_Nr);
            else
                ChangeRegister_int("FAN Control Enable", channel_new, 0, tEC_Nr);
        }

        public void Set_P(float newP, int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert intern übernehmen
            Value_P[channel - 1] = newP;
            ChangeRegister_float("Kp", channel_new, newP, tEC_Nr);
        }

        public void Set_I(float newI, int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert intern übernehmen
            Value_I[channel - 1] = newI;
            ChangeRegister_float("Ti", channel_new, newI, tEC_Nr);
        }

        public void Set_D(float newD, int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert intern übernehmen
            Value_D[channel - 1] = newD;
            ChangeRegister_float("Td", channel_new, newD, tEC_Nr);
        }



        public float GetTemperatur(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert auslesen
            Meas_temp[channel - 1] = ReadValueFloat("Object Temperature", channel_new, tEC_Nr);
            //Rückgabe
            return Meas_temp[channel - 1];
        }

        public float GetTargetTemperature(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert abhholen
            Target_temp[channel - 1] = ReadValueFloat("Target Object Temp", channel_new, tEC_Nr);
            return Target_temp[channel - 1];
        }

        public float GetCurrent(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert abhholen
            Meas_current[channel - 1] = ReadValueFloat("Actual Output Current", channel_new, tEC_Nr);
            return Meas_current[channel - 1];
        }

        public float GetVoltage(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert abhholen
            Meas_voltage[channel - 1] = ReadValueFloat("Actual Output Voltage", channel_new, tEC_Nr);
            return Meas_voltage[channel - 1];
        }

        public float GetSinkTemp(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert abhholen
            Meas_sink_temp[channel - 1] = ReadValueFloat("Sink Temperature", channel_new, tEC_Nr);
            return Meas_sink_temp[channel - 1];
        }

        public bool GetFanStatus(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert abhholen
            if (GetRegisterintValue("FAN Control Enable", channel_new, tEC_Nr) == 1)
                return true;
            else
                return false;
        }

        public float Get_P(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert abhholen
            Value_P[channel - 1] = ReadValueFloat("Kp", channel_new, tEC_Nr);
            return Value_P[channel - 1];
        }

        public float Get_I(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert abhholen
            Value_I[channel - 1] = ReadValueFloat("Ti", channel_new, tEC_Nr);
            return Value_I[channel - 1];
        }

        public float Get_D(int channel)
        {
            //Channel und TEC-Nr berechnen
            New_ChannelAndTECNr(channel, out int channel_new, out int tEC_Nr);
            // Wert abhholen
            Value_D[channel - 1] = ReadValueFloat("Td", channel_new, tEC_Nr);
            return Value_D[channel - 1];
        }

        #endregion MainCommands

        //********************************************************************************************************************
        //                                   Nachrichten senden allgemein
        //********************************************************************************************************************

        #region Messages

        private void ChangeRegister_int(string RegisterName, int channel, int newValue, int tEC_Nr)
        {
            int i = 0;

            foreach (Meerstetter_Registers selected in registers[tEC_Nr])
            {
                if (selected.registerName == RegisterName)
                {
                    if (selected.channel == channel)
                    {
                        selected.valueInt = newValue;
                        break;
                    }
                }
                i++;
            }
            this.SendIntValue(i, newValue, tEC_Nr);
        }

        private void ChangeRegister_float(string RegisterName, int channel, float newValue, int tEC_Nr)
        {
            int i = 0;

            foreach (Meerstetter_Registers selected in registers[tEC_Nr])
            {
                if (selected.registerName == RegisterName)
                {
                    if (selected.channel == channel)
                    {
                        selected.valueFloat = newValue;

                        break;
                    }

                }
                i++;
            }


            this.SendFloatValue(i, newValue, tEC_Nr);
        }

        private float ReadValueFloat(string RegisterName, int channel, int tEC_Nr)
        {
            int i = 0;

            foreach (Meerstetter_Registers selected in registers[tEC_Nr])
            {
                if (selected.registerName == RegisterName)
                {
                    if (selected.channel == channel)
                    {
                        selected.valueFloat = this.GetFloatValue(i, tEC_Nr);
                        return selected.valueFloat;
                    }
                }
                i++;
            }
            return 0;
        }

        private Int32 ReadValueInt(string RegisterName, int channel, int tEC_Nr)
        {
            int i = 0;

            foreach (Meerstetter_Registers selected in registers[tEC_Nr])
            {
                if (selected.registerName == RegisterName)
                {
                    if (selected.channel == channel)
                    {
                        selected.valueInt = this.GetIntValue(i, tEC_Nr);
                        return selected.valueInt;
                    }
                }
                i++;
            }
            return 0;
        }



        private void SendIntValue(int indexOfRegister, int newValue, int tEC_Nr)
        {            
            MeComQuerySet meComQuerySet = new MeComQuerySet(TEC_Connection[tEC_Nr]);
            meComQuerySet.SetDefaultDeviceAddress(0);
            meComQuerySet.SetIsReady(true);

            MeComBasicCmd meComBasicCmd = new MeComBasicCmd(meComQuerySet);

            byte parameterInst = registers[tEC_Nr][indexOfRegister].channel;

            meComBasicCmd.SetINT32Value(null, registers[tEC_Nr][indexOfRegister].registerNumber, parameterInst, newValue);           
        }

        private void SendFloatValue(int indexOfRegister, float newValue, int tEC_Nr)
        {
            MeComQuerySet meComQuerySet = new MeComQuerySet(TEC_Connection[tEC_Nr]);
            meComQuerySet.SetDefaultDeviceAddress(0);
            meComQuerySet.SetIsReady(true);


            MeComBasicCmd meComBasicCmd = new MeComBasicCmd(meComQuerySet);

            byte parameterInst = registers[tEC_Nr][indexOfRegister].channel;

            meComBasicCmd.SetFloatValue(null, registers[tEC_Nr][indexOfRegister].registerNumber, parameterInst, newValue);
        }

        private int GetIntValue(int indexOfRegister, int tEC_Nr)
        {
            MeComQuerySet meComQuerySet = new MeComQuerySet(TEC_Connection[tEC_Nr]);
            meComQuerySet.SetDefaultDeviceAddress(0);
            meComQuerySet.SetIsReady(true);

            MeComBasicCmd meComBasicCmd = new MeComBasicCmd(meComQuerySet);
            byte parameterInst = registers[tEC_Nr][indexOfRegister].channel;

            return meComBasicCmd.GetINT32Value(null, registers[tEC_Nr][indexOfRegister].registerNumber, parameterInst);
        }

        private float GetFloatValue(int indexOfRegister, int tEC_Nr)
        {
            MeComQuerySet meComQuerySet = new MeComQuerySet(TEC_Connection[tEC_Nr]);
            meComQuerySet.SetDefaultDeviceAddress(0);
            meComQuerySet.SetIsReady(true);


            MeComBasicCmd meComBasicCmd = new MeComBasicCmd(meComQuerySet);
            byte parameterInst = registers[tEC_Nr][indexOfRegister].channel;

            return meComBasicCmd.GetFloatValue(null, registers[tEC_Nr][indexOfRegister].registerNumber, parameterInst);
        }

        #endregion Messages

        //********************************************************************************************************************
        //                                     Register
        //********************************************************************************************************************

        #region Register

        void Fill_Registers_without_Values(int tEC_Nr)
        {

            //All registers listet with their Name, Number and int/Float indicator

            //3.3.1.1 Device Identification
            registers[tEC_Nr].Add(new Meerstetter_Registers(100, 1, "DeviceType", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(101, 1, "Hardware Version", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(102, 1, "Serial Number", "", true));     //Read only (maybe delate iniName)
            registers[tEC_Nr].Add(new Meerstetter_Registers(103, 1, "Firmware Version", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(104, 1, "Device Status", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(105, 1, "Error Number", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(106, 1, "Error Instance", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(107, 1, "Error Parameter", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(108, 1, "Serial Number", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(109, 1, "Serial Number", "", true));


            //3.3.2 Tab: Monitor (Read only)
            //3.3.2.1 CHx Temperature Measurement for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1000, 1, "Object Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1001, 1, "Sink Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1010, 1, "Target Object Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1011, 1, "(Ramp) Nominal Object Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1012, 1, "Thermal Power Model Current", "", false));
            //3.3.2.1 CHx Temperature Measurement for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1000, 2, "Object Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1001, 2, "Sink Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1010, 2, "Target Object Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1011, 2, "(Ramp) Nominal Object Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1012, 2, "Thermal Power Model Current", "", false));


            //3.3.2.3 CHx Output Stage Monitoring for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1020, 1, "Actual Output Current", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1021, 1, "Actual Output Voltage", "", false));
            //3.3.2.3 CHx Output Stage Monitoring for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1020, 2, "Actual Output Current", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1021, 2, "Actual Output Voltage", "", false));


            //3.3.2.4 CHx FAN Controller for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1100, 1, "Relative Cooling Power", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1101, 1, "Nominal FAN Speed", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1102, 1, "Actual FAN Speed", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1103, 1, "FAN PWM Level", "", false));
            //3.3.2.4 CHx FAN Controller for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1100, 2, "Relative Cooling Power", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1101, 2, "Nominal FAN Speed", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1102, 2, "Actual FAN Speed", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1103, 2, "FAN PWM Level", "", false));


            //3.3.2.5 CHx Temperature Controller PID Status for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1030, 1, "PID Lower Limitation", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1031, 1, "PID Upper Limitation", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1032, 1, "PID Control Variable", "", false));
            //3.3.2.5 CHx Temperature Controller PID Status for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1030, 2, "PID Lower Limitation", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1031, 2, "PID Upper Limitation", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1032, 2, "PID Control Variable", "", false));


            //3.3.2.6 CHx Temperature Measurement fo CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1040, 1, "Object Sensor ADC Value", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1041, 1, "Sink Sensor Raw ADC Value", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1042, 1, "Object Sensor Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1043, 1, "Sink Sensor Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1044, 1, "Sink Sensor Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1045, 1, "Object Sensor Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1046, 1, "Object Sensor Type", "", true));
            //3.3.2.6 CHx Temperature Measurement fo CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1040, 2, "Object Sensor ADC Value", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1041, 2, "Sink Sensor Raw ADC Value", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1042, 2, "Object Sensor Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1043, 2, "Sink Sensor Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1044, 2, "Sink Sensor Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1045, 2, "Object Sensor Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1046, 2, "Object Sensor Type", "", true));


            //3.3.2.7 Firmware and Hardware Versions for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1050, 1, "Firmware Version", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1051, 1, "Firmware Build Number", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1052, 1, "Hardware Version", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1053, 1, "Serial Number", "", true));
            //3.3.2.7 Firmware and Hardware Versions for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1050, 2, "Firmware Version", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1051, 2, "Firmware Build Number", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1052, 2, "Hardware Version", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1053, 2, "Serial Number", "", true));


            //3.3.2.8 Power Supplies and Temperature for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1060, 1, "Driver Input Voltage", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1061, 1, "Medium Internal Supply", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1062, 1, "3.3V Internal Supply", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1063, 1, "Base Plate Temperature", "", false));
            //3.3.2.8 Power Supplies and Temperature for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1060, 2, "Driver Input Voltage", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1061, 2, "Medium Internal Supply", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1062, 2, "3.3V Internal Supply", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1063, 2, "Base Plate Temperature", "", false));


            //3.3.2.9 Device Temperature Mode (Standard or Extended) for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1110, 1, "Maximum Device Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1111, 1, "Maximum Output Current", "", false));
            //3.3.2.9 Device Temperature Mode (Standard or Extended) for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1110, 2, "Maximum Device Temperature", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1111, 2, "Maximum Output Current", "", false));


            //3.3.2.10 Parallel Output Stage Monitoring (Common Load) for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(1090, 1, "Actual Output Current", "", false));
            //3.3.2.10 Parallel Output Stage Monitoring (Common Load) for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1090, 2, "Actual Output Current", "", false));


            //3.3.2.11 Error Status for CH1 and CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(1070, 1, "Error Number", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1071, 1, "Error Instance", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1072, 1, "Error Parameter", "", true));


            //3.3.2.12 Driver Status
            registers[tEC_Nr].Add(new Meerstetter_Registers(1080, 1, "Driver Status", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(1081, 1, "Parameter System: Flash Status", "", true));


            //3.3.2.13 Object Temperature Stability Detection
            registers[tEC_Nr].Add(new Meerstetter_Registers(1200, 1, "Temperature is Stable", "", true));


            //3.3.3 Tab: Operation
            //3.3.3.1 CHx Output Stage Control Input Selection for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(2000, 1, "Input Selection", "label_PAR_TEC1_MODE", true));
            //3.3.3.1 CHx Output Stage Control Input Selection for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(2000, 2, "Input Selection", "label_PAR_TEC2_MODE", true));


            //3.3.3.2 CHx Output Stage Enable for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(2010, 1, "Status", "label_PAR_TEC_EN1", true));
            //3.3.3.2 CHx Output Stage Enable for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(2010, 2, "Status", "label_PAR_TEC_EN2", true));


            //3.3.3.3 CHx Output Stage 'Static Current/Voltage' Control Values for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(2020, 1, "Set Current", "label_PAR_TEC1_Set_I", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2021, 1, "Set Voltage", "label_PAR_TEC1_Set_U", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2030, 1, "Current Limitation", "label_PAR_TEC1_LIMIT_I", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2031, 1, "Voltage Limitation", "label_PAR_TEC1_LIMIT_U", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2032, 1, "Current Error Threshold", "label_PAR_TEC1_ERRORLIMIT_I", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2033, 1, "Voltage Error Threshold", "label_PAR_TEC1_ERRORLIMIT_U", false));
            //3.3.3.3 CHx Output Stage 'Static Current/Voltage' Control Values for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(2020, 2, "Set Current", "label_PAR_TEC2_Set_I", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2021, 2, "Set Voltage", "label_PAR_TEC2_Set_U", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2030, 2, "Current Limitation", "label_PAR_TEC2_LIMIT_I", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2031, 2, "Voltage Limitation", "label_PAR_TEC2_LIMIT_U", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2032, 2, "Current Error Threshold", "label_PAR_TEC2_ERRORLIMIT_I", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2033, 2, "Voltage Error Threshold", "label_PAR_TEC2_ERRORLIMIT_U", false));


            //3.3.3.5 General Operating Mode for CH1 and CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(2040, 1, "General Operating Mode", "label_PAR_GENERAL_MODE", true));
            //3.3.3.6 Device Address
            registers[tEC_Nr].Add(new Meerstetter_Registers(2051, 1, "Device Address", "label_PAR_RS485_1_ADDRESS", true));
            //3.3.3.7 UART Interface Settings
            registers[tEC_Nr].Add(new Meerstetter_Registers(2050, 1, "Base Baud Rate", "", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(2052, 1, "Response Delay", "", true));
            //3.3.3.8 Communication Watchdog
            registers[tEC_Nr].Add(new Meerstetter_Registers(2060, 1, "Timeout", "label_PAR_CONECT_WATCH_DOG", false));


            //3.3.4 Tab: Temperature Control
            //3.3.4.1 CHx Nominal Temperature for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(3000, 1, "Target Object Temp", "label_PAR_TEMP1_REGUL_NOM", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3003, 1, "Coarse Temp Ramp", "label_PAR_TEMP1_NOM_RAMP_T", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3002, 1, "Proximity Width", "label_PAR_TEMP1_NOM_RAMP_S2", false));
            //3.3.4.1 CHx Nominal Temperature for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(3000, 2, "Target Object Temp", "label_PAR_TEMP2_REGUL_NOM", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3003, 2, "Coarse Temp Ramp", "label_PAR_TEMP2_NOM_RAMP_T", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3002, 2, "Proximity Width", "label_PAR_TEMP2_NOM_RAMP_S2", false));


            //3.3.4.2 CHx Temperature Controller PID Values for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(3010, 1, "Kp", "label_PAR_TEMP1_REGUL_KP", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3011, 1, "Ti", "label_PAR_TEMP1_REGUL_TI", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3012, 1, "Td", "label_PAR_TEMP1_REGUL_TD", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3013, 1, "D Part Damping PT1", "label_PAR_TEMP1_REGUL_D_PT1", false));
            //3.3.4.2 CHx Temperature Controller PID Values for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(3010, 2, "Kp", "label_PAR_TEMP2_REGUL_KP", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3011, 2, "Ti", "label_PAR_TEMP2_REGUL_TI", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3012, 2, "Td", "label_PAR_TEMP2_REGUL_TD", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3013, 2, "D Part Damping PT1", "label_PAR_TEMP2_REGUL_D_PT1", false));


            //3.3.4.3 CHx Modelization for Thermal Power Regulation for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(3020, 1, "Mode", "label_PAR_MODELIZATION_MODE1", true));
            //3.3.4.3 CHx Modelization for Thermal Power Regulation for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(3020, 2, "Mode", "label_PAR_MODELIZATION_MODE2", true));


            //3.3.4.4 CHx Peltier Characteristics for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(3030, 1, "Maximal Current Imax", "label_PAR_PELT1_MAXI", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3033, 1, "Delta Temperature dTmax", "label_PAR_PELT1_MAXDT", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3034, 1, "Positive Current is", "label_PAR_PELT_POLARITY1", true));
            //3.3.4.4 CHx Peltier Characteristics for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(3030, 2, "Maximal Current Imax", "label_PAR_PELT2_MAXI", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3033, 2, "Delta Temperature dTmax", "label_PAR_PELT2_MAXDT", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3034, 2, "Positive Current is", "label_PAR_PELT_POLARITY2", true));


            //3.3.4.5 CHx Resistor Characteristics for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(3040, 1, "Resistance", "label_PAR_RESISTOR_RESISTANCE1", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3041, 1, "Maximal Current", "label_PAR_RESISTOR_MAXI1", false));
            //3.3.4.5 CHx Resistor Characteristics for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(3040, 2, "Resistance", "label_PAR_RESISTOR_RESISTANCE2", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3041, 2, "Maximal Current", "label_PAR_RESISTOR_MAXI2", false));


            //3.3.4.6 CHx Peltier, Heat Only – Cool Only Boundaries for CH1 and CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(3051, 1, "Upper Boundary", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(3050, 1, "Lower Boundary", "", false));


            //3.3.5 Tab: Object Temperature
            //3.3.5.1 CHx Object Measurement Settings for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(4001, 1, "Temperature Offset", "label_PAR_TOBJ_TADJ_A01", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4002, 1, "Temperature Gain", "label_PAR_TOBJ_TADJ_A11", false));
            //3.3.5.1 CHx Object Measurement Settings for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(4001, 2, "Temperature Offset", "label_PAR_TOBJ_TADJ_A02", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4002, 2, "Temperature Gain", "label_PAR_TOBJ_TADJ_A12", false));


            //3.3.5.2 CHx Actual Object Temperature Error Limits for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(4011, 1, "Upper Error Threshold", "label_PAR_TOBJECT1_OVER", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4010, 1, "Lower Error Threshold", "label_PAR_TOBJECT1_UNDER", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4012, 1, "Max Temp Change", "label_PAR_TO_MAX_RAMP1", false));
            //3.3.5.2 CHx Actual Object Temperature Error Limits for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(4011, 2, "Upper Error Threshold", "label_PAR_TOBJECT2_OVER", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4010, 2, "Lower Error Threshold", "label_PAR_TOBJECT2_UNDER", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4012, 2, "Max Temp Change", "label_PAR_TO_MAX_RAMP2", false));



            //3.3.5.3 CHx Object NTC Sensor Characteristics for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(4024, 1, "Upper Point:Temperature", "label_PAR_TOBJ_NTC_T31", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4025, 1, "Upper Point:Resistance", "label_PAR_TOBJ_NTC_R31", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4022, 1, "Middle Point:Temperature", "label_PAR_TOBJ_NTC_T21", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4023, 1, "Middle Point:Resistance", "label_PAR_TOBJ_NTC_R21", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4020, 1, "Lower Point:Temperature", "label_PAR_TOBJ_NTC_T11", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4021, 1, "Lower Point:Resistance", "label_PAR_TOBJ_NTC_R11", false));
            //3.3.5.3 CHx Object NTC Sensor Characteristics for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(4024, 2, "Upper Point:Temperature", "label_PAR_TOBJ_NTC_T32", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4025, 2, "Upper Point:Resistance", "label_PAR_TOBJ_NTC_R32", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4022, 2, "Middle Point:Temperature", "label_PAR_TOBJ_NTC_T22", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4023, 2, "Middle Point:Resistance", "label_PAR_TOBJ_NTC_R22", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4020, 2, "Lower Point:Temperature", "label_PAR_TOBJ_NTC_T12", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4021, 2, "Lower Point:Resistance", "label_PAR_TOBJ_NTC_R12", false));


            //3.3.5.4 CH1 Object Temperature Stability Indicator Settings for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(4040, 1, "Temperature Deviation", "label_PAR_STABILITY_IND_TEMP1", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4041, 1, "Min Time in Window", "label_PAR_STABILITY_IND_TIME1", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4042, 1, "Max Stabilization Time", "", false));
            //3.3.5.4 CH1 Object Temperature Stability Indicator Settings for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(4040, 2, "Temperature Deviation", "label_PAR_STABILITY_IND_TEMP2", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4041, 2, "Min Time in Window", "label_PAR_STABILITY_IND_TIME2", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4042, 2, "Max Stabilization Time", "", false));


            //3.3.5.5 CHx Object Temperature Measurement Limits (Read Only) for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(4030, 1, "Lowest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4031, 1, "Highest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4032, 1, "Temperature at Lowest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4033, 1, "Temperature at Highest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4034, 1, "Object Sensor Type", "", true));
            //3.3.5.5 CHx Object Temperature Measurement Limits (Read Only) for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(4030, 2, "Lowest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4031, 2, "Highest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4032, 2, "Temperature at Lowest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4033, 2, "Temperature at Highest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(4034, 2, "Object Sensor Type", "", true));


            //3.3.6 Tab: Sink Temperature
            //3.3.6.1 CHx Sink Measurement Settings for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(5001, 1, "Temperature Offset", "label_PAR_TSINK_TADJ_A01", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5002, 1, "Temperature Gain", "label_PAR_TSINK_TADJ_A11", false));
            //3.3.6.1 CHx Sink Measurement Settings for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(5001, 2, "Temperature Offset", "label_PAR_TSINK_TADJ_A02", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5002, 2, "Temperature Gain", "label_PAR_TSINK_TADJ_A12", false));


            //3.3.6.2 CHx Actual Sink Temperature Error Limits for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(5011, 1, "Upper Error Threshold", "label_PAR_TSINK1_TEMP_OVER", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5010, 1, "Lower Error Threshold", "label_PAR_TSINK1_TEMP_UNDER", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5012, 1, "Max Temp Change", "label_PAR_TS_MAX_RAMP1", false));
            //3.3.6.2 CHx Actual Sink Temperature Error Limits for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(5011, 2, "Upper Error Threshold", "label_PAR_TSINK2_TEMP_OVER", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5010, 2, "Lower Error Threshold", "label_PAR_TSINK2_TEMP_UNDER", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5012, 2, "Max Temp Change", "label_PAR_TS_MAX_RAMP2", false));


            //3.3.6.3 CHx Sink NTC Sensor Characteristics for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(5024, 1, "Upper Point:Temperature", "label_PAR_TSINK_NTC_T31", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5025, 1, "Upper Point:Resistance", "label_PAR_TSINK_NTC_R31", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5022, 1, "Middle Point:Temperature", "label_PAR_TSINK_NTC_T21", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5023, 1, "Middle Point:Resistance", "label_PAR_TSINK_NTC_R21", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5020, 1, "Lower Point:Temperature", "label_PAR_TSINK_NTC_T11", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5021, 1, "Lower Point:Resistance", "label_PAR_TSINK_NTC_R11", false));
            //3.3.6.3 CHx Sink NTC Sensor Characteristics for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(5024, 2, "Upper Point:Temperature", "label_PAR_TSINK_NTC_T32", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5025, 2, "Upper Point:Resistance", "label_PAR_TSINK_NTC_R32", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5022, 2, "Middle Point:Temperature", "label_PAR_TSINK_NTC_T22", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5023, 2, "Middle Point:Resistance", "label_PAR_TSINK_NTC_R22", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5020, 2, "Lower Point:Temperature", "label_PAR_TSINK_NTC_T12", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5021, 2, "Lower Point:Resistance", "label_PAR_TSINK_NTC_R12", false));


            //3.3.6.4 CHx Sink Temperature General for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(5030, 1, "Sink Temperature Selection", "label_PAR_TSINK1_MODE", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5031, 1, "Fixed Temperature", "label_PAR_TSINK1_FIXED_TEMP", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5032, 1, "Upper ADC Limit Error", "", true));
            //3.3.6.4 CHx Sink Temperature General for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(5030, 2, "Sink Temperature Selection", "label_PAR_TSINK2_MODE", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5031, 2, "Fixed Temperature", "label_PAR_TSINK2_FIXED_TEMP", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5032, 2, "Upper ADC Limit Error", "", true));


            //3.3.6.5 CHx Sink Temperature Measurement Limits (Read Only) for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(5040, 1, "Lowest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5041, 1, "Highest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5042, 1, "Temperature at Lowest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5043, 1, "Temperature at Highest Resistance", "", false));
            //3.3.6.5 CHx Sink Temperature Measurement Limits (Read Only) for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(5040, 2, "Lowest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5041, 2, "Highest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5042, 2, "Temperature at Lowest Resistance", "", false));
            registers[tEC_Nr].Add(new Meerstetter_Registers(5043, 2, "Temperature at Highest Resistance", "", false));


            //3.3.7 Tab: Expert
            //3.3.7.4 Sub Tab: FAN
            //3.3.7.3.1 PBC Configuration (RES1 … RES8)
            registers[tEC_Nr].Add(new Meerstetter_Registers(6100, 1, "PBC RES1", "label_PAR_PBC_RES_FUNC1", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(6100, 2, "PBC RES2", "label_PAR_PBC_RES_FUNC2", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(6100, 3, "PBC RES3", "label_PAR_PBC_RES_FUNC3", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(6100, 4, "PBC RES4", "label_PAR_PBC_RES_FUNC4", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(6100, 5, "PBC RES5", "label_PAR_PBC_RES_FUNC5", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(6100, 6, "PBC RES6", "label_PAR_PBC_RES_FUNC6", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(6100, 7, "PBC RES7", "label_PAR_PBC_RES_FUNC7", true));
            registers[tEC_Nr].Add(new Meerstetter_Registers(6100, 8, "PBC RES8", "label_PAR_PBC_RES_FUNC8", true));

            //3.3.7.4.1 CHx FAN Control Enable for CH1
            registers[tEC_Nr].Add(new Meerstetter_Registers(6200, 1, "FAN Control Enable", "label_PAR_FAN_ENABLE1", true));
            //3.3.7.4.1 CHx FAN Control Enable for CH2
            registers[tEC_Nr].Add(new Meerstetter_Registers(6200, 2, "FAN Control Enable", "label_PAR_FAN_ENABLE2", true));


        }


        private void Fill_Register_Values(int tEC_Nr)
        {
            //Load ini File and write values in registers
            foreach (string line in initalisationFile)
            {
                int placeOf = line.IndexOf('=');

                if (placeOf != -1)
                {

                    string registerName = line.Substring(0, placeOf);
                    string registerValue = line.Substring(placeOf + 1);


                    //find index
                    foreach (Meerstetter_Registers selected in registers[tEC_Nr])
                    {


                        Meerstetter_Registers neu = selected;

                        if (string.Equals(selected.iniFileName, registerName))
                        {


                            if (selected.isInteger == true)
                            {
                                switch (registerName)
                                {
                                    case "label_PAR_TEC1_MODE":
                                        selected.valueInt = Converter_label_PAR_TEC1_MODE(registerValue);
                                        break;
                                    case "label_PAR_TEC2_MODE":
                                        selected.valueInt = Converter_label_PAR_TEC2_MODE(registerValue);
                                        break;
                                    case "label_PAR_TEC_EN1":
                                        selected.valueInt = Converter_label_PAR_TEC_EN1(registerValue);
                                        break;
                                    case "label_PAR_TEC_EN2":
                                        selected.valueInt = Converter_label_PAR_TEC_EN2(registerValue);
                                        break;
                                    case "label_PAR_GENERAL_MODE":
                                        selected.valueInt = Converter_label_PAR_GENERAL_MODE(registerValue);
                                        break;
                                    case "label_PAR_MODELIZATION_MODE1":
                                        selected.valueInt = Converter_label_PAR_MODELIZATION_MODE1(registerValue);
                                        break;
                                    case "label_PAR_MODELIZATION_MODE2":
                                        selected.valueInt = Converter_label_PAR_MODELIZATION_MODE2(registerValue);
                                        break;
                                    case "label_PAR_PELT_POLARITY1":
                                        selected.valueInt = Converter_label_PAR_PELT_POLARITY1(registerValue);
                                        break;
                                    case "label_PAR_PELT_POLARITY2":
                                        selected.valueInt = Converter_label_PAR_PELT_POLARITY2(registerValue);
                                        break;
                                    case "label_PAR_TSINK1_MODE":
                                        selected.valueInt = Converter_label_PAR_TSINK1_MODE(registerValue);
                                        break;
                                    case "label_PAR_TSINK2_MODE":
                                        selected.valueInt = Converter_label_PAR_TSINK2_MODE(registerValue);
                                        break;
                                    case "label_PAR_PBC_RES_FUNC1":
                                        selected.valueInt = Converter_label_PAR_PBC_RES_FUNC1(registerValue);
                                        break;
                                    case "label_PAR_PBC_RES_FUNC2":
                                        selected.valueInt = Converter_label_PAR_PBC_RES_FUNC2(registerValue);
                                        break;
                                    case "label_PAR_PBC_RES_FUNC3":
                                        selected.valueInt = Converter_label_PAR_PBC_RES_FUNC3(registerValue);
                                        break;
                                    case "label_PAR_PBC_RES_FUNC4":
                                        selected.valueInt = Converter_label_PAR_PBC_RES_FUNC4(registerValue);
                                        break;
                                    case "label_PAR_PBC_RES_FUNC5":
                                        selected.valueInt = Converter_label_PAR_PBC_RES_FUNC5(registerValue);
                                        break;
                                    case "label_PAR_PBC_RES_FUNC6":
                                        selected.valueInt = Converter_label_PAR_PBC_RES_FUNC6(registerValue);
                                        break;
                                    case "label_PAR_PBC_RES_FUNC7":
                                        selected.valueInt = Converter_label_PAR_PBC_RES_FUNC7(registerValue);
                                        break;
                                    case "label_PAR_PBC_RES_FUNC8":
                                        selected.valueInt = Converter_label_PAR_PBC_RES_FUNC8(registerValue);
                                        break;
                                    case "label_PAR_FAN_ENABLE1":
                                        selected.valueInt = Converter_label_PAR_FAN_ENABLE1(registerValue);
                                        break;
                                    case "label_PAR_FAN_ENABLE2":
                                        selected.valueInt = Converter_label_PAR_FAN_ENABLE2(registerValue);
                                        break;

                                    default:

                                        registerValue = registerValue.Replace(".", "");


                                        try
                                        {
                                            selected.valueInt = Convert.ToInt32(registerValue);
                                        }
                                        catch (FormatException e)
                                        {
                                            e.ToString();
                                            selected.valueInt = -1;
                                        }


                                        break;
                                }

                            }
                            if (selected.isInteger == false)
                            {

                                try
                                {
                                    selected.valueFloat = float.Parse(registerValue);

                                }
                                catch (FormatException e)
                                {
                                    e.ToString();
                                    selected.valueFloat = -1;
                                }


                                break;

                            }

                        }
                    }

                }
            }

        }

        #region Char2Int

        private Int32 Converter_label_PAR_TEC1_MODE(string input)
        {
            switch (input)
            {
                case "Static Current/Voltage":
                    return 0;
                case "Live Current/Voltage":
                    return 1;
                case "Temperature Controller":
                    return 2;
                default:
                    return -1;
            }

        }
        private Int32 Converter_label_PAR_TEC2_MODE(string input)
        {
            switch (input)
            {
                case "Static Current/Voltage":
                    return 0;
                case "Live Current/Voltage":
                    return 1;
                case "Temperature Controller":
                    return 2;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_TEC_EN1(string input)
        {
            switch (input)
            {
                case "Static OFF":
                    return 0;
                case "Static ON":
                    return 1;
                case "Live OFF/ON":
                    return 2;
                case "HW Enable":
                    return 3;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_TEC_EN2(string input)
        {
            switch (input)
            {
                case "Static OFF":
                    return 0;
                case "Static ON":
                    return 1;
                case "Live OFF/ON":
                    return 2;
                case "HW Enable":
                    return 3;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_GENERAL_MODE(string input)
        {
            switch (input)
            {
                case "Single (Independent)":
                    return 0;
                case "Parallel (CH1  CH2); Individual Loads":
                    return 1;
                case "Parallel: (CH1  CH2); Common Load":
                    return 2;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_MODELIZATION_MODE1(string input)
        {
            switch (input)
            {
                case "Peltier, Full Control":
                    return 0;
                case "Peltier, Heat Only - Cool Only":
                    return 1;
                case "Resistor, Heat Only":
                    return 2;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_MODELIZATION_MODE2(string input)
        {
            switch (input)
            {
                case "Peltier, Full Control":
                    return 0;
                case "Peltier, Heat Only - Cool Only":
                    return 1;
                case "Resistor, Heat Only":
                    return 2;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PELT_POLARITY1(string input)
        {
            switch (input)
            {
                case "Cooling":
                    return 0;
                case "Heating":
                    return 1;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PELT_POLARITY2(string input)
        {
            switch (input)
            {
                case "Cooling":
                    return 0;
                case "Heating":
                    return 1;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_TSINK1_MODE(string input)
        {
            switch (input)
            {
                case "External":
                    return 0;
                case "Fixed Value":
                    return 1;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_TSINK2_MODE(string input)
        {
            switch (input)
            {
                case "External":
                    return 0;
                case "Fixed Value":
                    return 1;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_FAN_ENABLE1(string input)
        {
            switch (input)
            {
                case "Disabled":
                    return 0;
                case "Enabled":
                    return 1;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_FAN_ENABLE2(string input)
        {
            switch (input)
            {
                case "Disabled":
                    return 0;
                case "Enabled":
                    return 1;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PBC_RES_FUNC1(string input)
        {
            switch (input)
            {
                case "No Function":
                    return 0;
                case "Data Interface":
                    return 1;
                case "TEC OK":
                    return 2;
                case "CH1 Stable":
                    return 3;
                case "CH2 Stable":
                    return 4;
                case "CH1 HW Enable":
                    return 5;
                case "CH2 HW Enable":
                    return 6;
                case "CH1 FAN PWM":
                    return 7;
                case "CH2 FAN PWM":
                    return 8;
                case "CH1 FAN Tacho":
                    return 9;
                case "CH2 FAN Tacho":
                    return 10;
                case "TEC Error":
                    return 11;
                case "CH1 Rmp/Stable":
                    return 12;
                case "CH2 Rmp/Stable":
                    return 13;
                case "TEC Run":
                    return 14;
                case "CH1 Not Stable":
                    return 15;
                case "CH2 Not Stable":
                    return 16;
                case "CH1 TempUp":
                    return 17;
                case "CH2 TempUp":
                    return 18;
                case "CH1 TempDown":
                    return 19;
                case "CH2 TempDown":
                    return 20;
                case "CH1 Pump":
                    return 21;
                case "CH2 Pump":
                    return 22;
                case "CH1 Lookup Start":
                    return 23;
                case "CH2 Lookup Start":
                    return 24;
                case "Dev Adr +1":
                    return 25;
                case "Dev Adr +2":
                    return 26;
                case "Dev Adr +4":
                    return 27;
                case "CH1 FAN Stop":
                    return 28;
                case "CH2 FAN Stop":
                    return 29;
                case "CH1 Alt Target T1":
                    return 30;
                case "CH1 Alt Target T2":
                    return 31;
                case "CH2 Alt Target T1":
                    return 32;
                case "CH2 Alt Target T2":
                    return 33;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PBC_RES_FUNC2(string input)
        {
            switch (input)
            {
                case "No Function":
                    return 0;
                case "Data Interface":
                    return 1;
                case "TEC OK":
                    return 2;
                case "CH1 Stable":
                    return 3;
                case "CH2 Stable":
                    return 4;
                case "CH1 HW Enable":
                    return 5;
                case "CH2 HW Enable":
                    return 6;
                case "CH1 FAN PWM":
                    return 7;
                case "CH2 FAN PWM":
                    return 8;
                case "CH1 FAN Tacho":
                    return 9;
                case "CH2 FAN Tacho":
                    return 10;
                case "TEC Error":
                    return 11;
                case "CH1 Rmp/Stable":
                    return 12;
                case "CH2 Rmp/Stable":
                    return 13;
                case "TEC Run":
                    return 14;
                case "CH1 Not Stable":
                    return 15;
                case "CH2 Not Stable":
                    return 16;
                case "CH1 TempUp":
                    return 17;
                case "CH2 TempUp":
                    return 18;
                case "CH1 TempDown":
                    return 19;
                case "CH2 TempDown":
                    return 20;
                case "CH1 Pump":
                    return 21;
                case "CH2 Pump":
                    return 22;
                case "CH1 Lookup Start":
                    return 23;
                case "CH2 Lookup Start":
                    return 24;
                case "Dev Adr +1":
                    return 25;
                case "Dev Adr +2":
                    return 26;
                case "Dev Adr +4":
                    return 27;
                case "CH1 FAN Stop":
                    return 28;
                case "CH2 FAN Stop":
                    return 29;
                case "CH1 Alt Target T1":
                    return 30;
                case "CH1 Alt Target T2":
                    return 31;
                case "CH2 Alt Target T1":
                    return 32;
                case "CH2 Alt Target T2":
                    return 33;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PBC_RES_FUNC3(string input)
        {
            switch (input)
            {
                case "No Function":
                    return 0;
                case "Data Interface":
                    return 1;
                case "TEC OK":
                    return 2;
                case "CH1 Stable":
                    return 3;
                case "CH2 Stable":
                    return 4;
                case "CH1 HW Enable":
                    return 5;
                case "CH2 HW Enable":
                    return 6;
                case "CH1 FAN PWM":
                    return 7;
                case "CH2 FAN PWM":
                    return 8;
                case "CH1 FAN Tacho":
                    return 9;
                case "CH2 FAN Tacho":
                    return 10;
                case "TEC Error":
                    return 11;
                case "CH1 Rmp/Stable":
                    return 12;
                case "CH2 Rmp/Stable":
                    return 13;
                case "TEC Run":
                    return 14;
                case "CH1 Not Stable":
                    return 15;
                case "CH2 Not Stable":
                    return 16;
                case "CH1 TempUp":
                    return 17;
                case "CH2 TempUp":
                    return 18;
                case "CH1 TempDown":
                    return 19;
                case "CH2 TempDown":
                    return 20;
                case "CH1 Pump":
                    return 21;
                case "CH2 Pump":
                    return 22;
                case "CH1 Lookup Start":
                    return 23;
                case "CH2 Lookup Start":
                    return 24;
                case "Dev Adr +1":
                    return 25;
                case "Dev Adr +2":
                    return 26;
                case "Dev Adr +4":
                    return 27;
                case "CH1 FAN Stop":
                    return 28;
                case "CH2 FAN Stop":
                    return 29;
                case "CH1 Alt Target T1":
                    return 30;
                case "CH1 Alt Target T2":
                    return 31;
                case "CH2 Alt Target T1":
                    return 32;
                case "CH2 Alt Target T2":
                    return 33;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PBC_RES_FUNC4(string input)
        {
            switch (input)
            {
                case "No Function":
                    return 0;
                case "Data Interface":
                    return 1;
                case "TEC OK":
                    return 2;
                case "CH1 Stable":
                    return 3;
                case "CH2 Stable":
                    return 4;
                case "CH1 HW Enable":
                    return 5;
                case "CH2 HW Enable":
                    return 6;
                case "CH1 FAN PWM":
                    return 7;
                case "CH2 FAN PWM":
                    return 8;
                case "CH1 FAN Tacho":
                    return 9;
                case "CH2 FAN Tacho":
                    return 10;
                case "TEC Error":
                    return 11;
                case "CH1 Rmp/Stable":
                    return 12;
                case "CH2 Rmp/Stable":
                    return 13;
                case "TEC Run":
                    return 14;
                case "CH1 Not Stable":
                    return 15;
                case "CH2 Not Stable":
                    return 16;
                case "CH1 TempUp":
                    return 17;
                case "CH2 TempUp":
                    return 18;
                case "CH1 TempDown":
                    return 19;
                case "CH2 TempDown":
                    return 20;
                case "CH1 Pump":
                    return 21;
                case "CH2 Pump":
                    return 22;
                case "CH1 Lookup Start":
                    return 23;
                case "CH2 Lookup Start":
                    return 24;
                case "Dev Adr +1":
                    return 25;
                case "Dev Adr +2":
                    return 26;
                case "Dev Adr +4":
                    return 27;
                case "CH1 FAN Stop":
                    return 28;
                case "CH2 FAN Stop":
                    return 29;
                case "CH1 Alt Target T1":
                    return 30;
                case "CH1 Alt Target T2":
                    return 31;
                case "CH2 Alt Target T1":
                    return 32;
                case "CH2 Alt Target T2":
                    return 33;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PBC_RES_FUNC5(string input)
        {
            switch (input)
            {
                case "No Function":
                    return 0;
                case "Data Interface":
                    return 1;
                case "TEC OK":
                    return 2;
                case "CH1 Stable":
                    return 3;
                case "CH2 Stable":
                    return 4;
                case "CH1 HW Enable":
                    return 5;
                case "CH2 HW Enable":
                    return 6;
                case "CH1 FAN PWM":
                    return 7;
                case "CH2 FAN PWM":
                    return 8;
                case "CH1 FAN Tacho":
                    return 9;
                case "CH2 FAN Tacho":
                    return 10;
                case "TEC Error":
                    return 11;
                case "CH1 Rmp/Stable":
                    return 12;
                case "CH2 Rmp/Stable":
                    return 13;
                case "TEC Run":
                    return 14;
                case "CH1 Not Stable":
                    return 15;
                case "CH2 Not Stable":
                    return 16;
                case "CH1 TempUp":
                    return 17;
                case "CH2 TempUp":
                    return 18;
                case "CH1 TempDown":
                    return 19;
                case "CH2 TempDown":
                    return 20;
                case "CH1 Pump":
                    return 21;
                case "CH2 Pump":
                    return 22;
                case "CH1 Lookup Start":
                    return 23;
                case "CH2 Lookup Start":
                    return 24;
                case "Dev Adr +1":
                    return 25;
                case "Dev Adr +2":
                    return 26;
                case "Dev Adr +4":
                    return 27;
                case "CH1 FAN Stop":
                    return 28;
                case "CH2 FAN Stop":
                    return 29;
                case "CH1 Alt Target T1":
                    return 30;
                case "CH1 Alt Target T2":
                    return 31;
                case "CH2 Alt Target T1":
                    return 32;
                case "CH2 Alt Target T2":
                    return 33;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PBC_RES_FUNC6(string input)
        {
            switch (input)
            {
                case "No Function":
                    return 0;
                case "Data Interface":
                    return 1;
                case "TEC OK":
                    return 2;
                case "CH1 Stable":
                    return 3;
                case "CH2 Stable":
                    return 4;
                case "CH1 HW Enable":
                    return 5;
                case "CH2 HW Enable":
                    return 6;
                case "CH1 FAN PWM":
                    return 7;
                case "CH2 FAN PWM":
                    return 8;
                case "CH1 FAN Tacho":
                    return 9;
                case "CH2 FAN Tacho":
                    return 10;
                case "TEC Error":
                    return 11;
                case "CH1 Rmp/Stable":
                    return 12;
                case "CH2 Rmp/Stable":
                    return 13;
                case "TEC Run":
                    return 14;
                case "CH1 Not Stable":
                    return 15;
                case "CH2 Not Stable":
                    return 16;
                case "CH1 TempUp":
                    return 17;
                case "CH2 TempUp":
                    return 18;
                case "CH1 TempDown":
                    return 19;
                case "CH2 TempDown":
                    return 20;
                case "CH1 Pump":
                    return 21;
                case "CH2 Pump":
                    return 22;
                case "CH1 Lookup Start":
                    return 23;
                case "CH2 Lookup Start":
                    return 24;
                case "Dev Adr +1":
                    return 25;
                case "Dev Adr +2":
                    return 26;
                case "Dev Adr +4":
                    return 27;
                case "CH1 FAN Stop":
                    return 28;
                case "CH2 FAN Stop":
                    return 29;
                case "CH1 Alt Target T1":
                    return 30;
                case "CH1 Alt Target T2":
                    return 31;
                case "CH2 Alt Target T1":
                    return 32;
                case "CH2 Alt Target T2":
                    return 33;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PBC_RES_FUNC7(string input)
        {
            switch (input)
            {
                case "No Function":
                    return 0;
                case "Data Interface":
                    return 1;
                case "TEC OK":
                    return 2;
                case "CH1 Stable":
                    return 3;
                case "CH2 Stable":
                    return 4;
                case "CH1 HW Enable":
                    return 5;
                case "CH2 HW Enable":
                    return 6;
                case "CH1 FAN PWM":
                    return 7;
                case "CH2 FAN PWM":
                    return 8;
                case "CH1 FAN Tacho":
                    return 9;
                case "CH2 FAN Tacho":
                    return 10;
                case "TEC Error":
                    return 11;
                case "CH1 Rmp/Stable":
                    return 12;
                case "CH2 Rmp/Stable":
                    return 13;
                case "TEC Run":
                    return 14;
                case "CH1 Not Stable":
                    return 15;
                case "CH2 Not Stable":
                    return 16;
                case "CH1 TempUp":
                    return 17;
                case "CH2 TempUp":
                    return 18;
                case "CH1 TempDown":
                    return 19;
                case "CH2 TempDown":
                    return 20;
                case "CH1 Pump":
                    return 21;
                case "CH2 Pump":
                    return 22;
                case "CH1 Lookup Start":
                    return 23;
                case "CH2 Lookup Start":
                    return 24;
                case "Dev Adr +1":
                    return 25;
                case "Dev Adr +2":
                    return 26;
                case "Dev Adr +4":
                    return 27;
                case "CH1 FAN Stop":
                    return 28;
                case "CH2 FAN Stop":
                    return 29;
                case "CH1 Alt Target T1":
                    return 30;
                case "CH1 Alt Target T2":
                    return 31;
                case "CH2 Alt Target T1":
                    return 32;
                case "CH2 Alt Target T2":
                    return 33;
                default:
                    return -1;
            }
        }
        private Int32 Converter_label_PAR_PBC_RES_FUNC8(string input)
        {
            switch (input)
            {
                case "No Function":
                    return 0;
                case "Data Interface":
                    return 1;
                case "TEC OK":
                    return 2;
                case "CH1 Stable":
                    return 3;
                case "CH2 Stable":
                    return 4;
                case "CH1 HW Enable":
                    return 5;
                case "CH2 HW Enable":
                    return 6;
                case "CH1 FAN PWM":
                    return 7;
                case "CH2 FAN PWM":
                    return 8;
                case "CH1 FAN Tacho":
                    return 9;
                case "CH2 FAN Tacho":
                    return 10;
                case "TEC Error":
                    return 11;
                case "CH1 Rmp/Stable":
                    return 12;
                case "CH2 Rmp/Stable":
                    return 13;
                case "TEC Run":
                    return 14;
                case "CH1 Not Stable":
                    return 15;
                case "CH2 Not Stable":
                    return 16;
                case "CH1 TempUp":
                    return 17;
                case "CH2 TempUp":
                    return 18;
                case "CH1 TempDown":
                    return 19;
                case "CH2 TempDown":
                    return 20;
                case "CH1 Pump":
                    return 21;
                case "CH2 Pump":
                    return 22;
                case "CH1 Lookup Start":
                    return 23;
                case "CH2 Lookup Start":
                    return 24;
                case "Dev Adr +1":
                    return 25;
                case "Dev Adr +2":
                    return 26;
                case "Dev Adr +4":
                    return 27;
                case "CH1 FAN Stop":
                    return 28;
                case "CH2 FAN Stop":
                    return 29;
                case "CH1 Alt Target T1":
                    return 30;
                case "CH1 Alt Target T2":
                    return 31;
                case "CH2 Alt Target T1":
                    return 32;
                case "CH2 Alt Target T2":
                    return 33;
                default:
                    return -1;
            }
        }

        #endregion Char2Int

        private void SendRegisters2TEC_Controller(int tEC_Nr)
        {
            foreach (Meerstetter_Registers selected in registers[tEC_Nr])
            {
                if ("" == selected.iniFileName)
                {

                }
                else
                {
                    if (selected.isInteger)
                    {
                        SendIntValue(registers[tEC_Nr].IndexOf(selected), selected.valueInt, tEC_Nr);
                    }
                    else
                    {
                        SendFloatValue(registers[tEC_Nr].IndexOf(selected), selected.valueFloat, tEC_Nr);
                    }
                }
            }
        }

        public float GetRegisterFloatValue(string registerName, int channel, int tEC_Nr)
        {
            foreach (Meerstetter_Registers selected in registers[tEC_Nr])
            {

                if (selected.registerName == registerName)
                {
                    if (selected.channel == channel)
                    {

                        return selected.valueFloat;
                    }
                }
            }

            return 0;
        }
        public float GetRegisterintValue(string registerName, int channel, int tEC_Nr)
        {
            foreach (Meerstetter_Registers selected in registers[tEC_Nr])
            {

                if (selected.registerName == registerName)
                {
                    if (selected.channel == channel)
                    {

                        return selected.valueInt;
                    }
                }
            }

            return 0;
        }


        #endregion Register

        //********************************************************************************************************************
        //                                     Timer
        //********************************************************************************************************************

        private void OnThreath_05sec_timer()
        {
            if (this.InvokeRequired)
            {
                // Wenn Invoke nötig ist, ...
                // dann rufen wir die Methode selbst per Invoke auf (Invoke verursacht Error, da Kommunikation doppelt verwendet weird)
                this.Invoke(new OnParaThreadHandler(OnThreath_05sec_timer));
                return;
            }

            #region Temperatur ändern

            if (Flag_Temp_Changed)
            {
                Flag_Temp_Changed = false;
                SetTemperature(New_Temp_kFactor);
            }

            #endregion Temperature ändern

            #region hauptfenster

            //Ausgaben Textboxen
            UI_MeasuredTemp1.Text = GetTemperatur(1).ToString("0.000") + "°C | " + GetTemperatur(2).ToString("0.000") + "°C";
            UI_MeasuredTemp2.Text = GetTemperatur(3).ToString("0.000") + "°C | " + GetTemperatur(4).ToString("0.000") + "°C";

            //Mittelwert berechnen
            Meas_temp_aver = ((Meas_temp[0] + Meas_temp[1] + Meas_temp[2] + Meas_temp[3]) / 4);

            //Farbe anpassen & Stable-Counter ************************************************************************************************

            //1. Wenn alle an
            if (TEC_on[0] & TEC_on[1] & TEC_on[2] & TEC_on[3])
            {
                //1.1 Wenn alle in Range
                if ((Math.Abs(Meas_temp[0] - Target_temp[0]) < Temp_abweichung) && (Math.Abs(Meas_temp[1] - Target_temp[1]) < Temp_abweichung)
                    && (Math.Abs(Meas_temp[2] - Target_temp[2]) < Temp_abweichung) && (Math.Abs(Meas_temp[3] - Target_temp[3]) < Temp_abweichung))
                {
                    // increasing the counter for the 30s
                    Counter_0_5sec++;
                    // change the color of the box
                    UI_MeasuredTemp1.BackColor = Color.Lime;
                    UI_MeasuredTemp2.BackColor = Color.Lime;
                }

                //1.2 Mindestens einer nicht im Range
                else
                {
                    // reset the counter to 0
                    Counter_0_5sec = 0;
                    //TEC 1 auf fehler prüfen
                    if ((Math.Abs(Meas_temp[0] - Target_temp[0]) > Temp_abweichung) | (Math.Abs(Meas_temp[1] - Target_temp[1]) > Temp_abweichung))
                        UI_MeasuredTemp1.BackColor = Color.Red;
                    else
                        UI_MeasuredTemp1.BackColor = Color.Lime;
                    //TEC 2 auf fehler prüfen
                    if ((Math.Abs(Meas_temp[2] - Target_temp[2]) > Temp_abweichung) | (Math.Abs(Meas_temp[3] - Target_temp[3]) > Temp_abweichung))
                        UI_MeasuredTemp2.BackColor = Color.Red;
                    else
                        UI_MeasuredTemp2.BackColor = Color.Lime;
                }
            }

            //2. wenn nicht alle an
            else
            {
                // reset the counter to 0
                Counter_0_5sec = 0;

                //Grau wenn keiner an, orang wenn nur einer an
                if (!TEC_on[0] & !TEC_on[1])
                    UI_MeasuredTemp1.BackColor = SystemColors.Control;
                else
                    UI_MeasuredTemp1.BackColor = Color.Orange;

                if (!TEC_on[2] & !TEC_on[3])
                    UI_MeasuredTemp1.BackColor = SystemColors.Control;
                else
                    UI_MeasuredTemp2.BackColor = Color.Orange;

            }

            //check if the counter has reached the 30s limit*************************************************************************************
            if (Counter_0_5sec >= 60)
            {
                //set the boolean to true
                Stable_for_30sec = true;
                // this will be useful to avoid overflow
                Counter_0_5sec = 70;
            }
            else
            {
                //set the boolean to true
                Stable_for_30sec = false;
            }

            /*
            //Failure: Edit Maxi 08112017
            if (my_kFactor_Measurement != null)
            {
                if (my_kFactor_Measurement.isrunning)
                {
                    kFactorChart.Series["Temperature progress"].Points.AddXY((double)my_kFactor_Measurement.counter_time, (double)TempAverage);
                    kFactorChart.Update();

                    my_kFactor_Measurement.counter_time += 0.5;
                }
            }
            */
            #endregion hauptfenster

            #region detailed

            if (WindowOpen)
            {
                //Temp
                this.TEC_Detailed.Invoke((MethodInvoker)delegate
                {
                    TEC_Detailed.TEC1_MeasuredTemp.Text = Meas_temp[0].ToString("0.000") + " °C";
                    TEC_Detailed.TEC2_MeasuredTemp.Text = Meas_temp[1].ToString("0.000") + " °C";
                    TEC_Detailed.TEC3_MeasuredTemp.Text = Meas_temp[2].ToString("0.000") + " °C";
                    TEC_Detailed.TEC4_MeasuredTemp.Text = Meas_temp[3].ToString("0.000") + " °C";
                    //Heat-Sink Temp
                    TEC_Detailed.TEC1_SinkTemp.Text = GetSinkTemp(1).ToString("0.000") + " °C";
                    TEC_Detailed.TEC2_SinkTemp.Text = GetSinkTemp(2).ToString("0.000") + " °C";
                    TEC_Detailed.TEC3_SinkTemp.Text = GetSinkTemp(3).ToString("0.000") + " °C";
                    TEC_Detailed.TEC4_SinkTemp.Text = GetSinkTemp(4).ToString("0.000") + " °C";
                    //Current
                    TEC_Detailed.TEC1_Current.Text = GetCurrent(1).ToString("0.000") + " A";
                    TEC_Detailed.TEC2_Current.Text = GetCurrent(2).ToString("0.000") + " A";
                    TEC_Detailed.TEC3_Current.Text = GetCurrent(3).ToString("0.000") + " A";
                    TEC_Detailed.TEC4_Current.Text = GetCurrent(4).ToString("0.000") + " A";
                    //Voltage
                    TEC_Detailed.TEC1_Voltage.Text = GetVoltage(1).ToString("0.000") + " V";
                    TEC_Detailed.TEC2_Voltage.Text = GetVoltage(2).ToString("0.000") + " V";
                    TEC_Detailed.TEC3_Voltage.Text = GetVoltage(3).ToString("0.000") + " V";
                    TEC_Detailed.TEC4_Voltage.Text = GetVoltage(4).ToString("0.000") + " V";

                    //Power
                    TEC_Detailed.TEC1_Power.Text = (Meas_current[0] * Meas_voltage[0]).ToString("0.000") + " W";
                    TEC_Detailed.TEC2_Power.Text = (Meas_current[1] * Meas_voltage[1]).ToString("0.000") + " W";
                    TEC_Detailed.TEC3_Power.Text = (Meas_current[2] * Meas_voltage[2]).ToString("0.000") + " W";
                    TEC_Detailed.TEC4_Power.Text = (Meas_current[3] * Meas_voltage[3]).ToString("0.000") + " W";
                });


            }

            #endregion detailed

        }


        //********************************************************************************************************************
        //                                           AutoConnect
        //********************************************************************************************************************

        /*
    public string AutoOpen(Load_Screen myLoadScreen)
    {
        int iterration = 5;

        if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
        {
            return "TEC controller: No COM-Port selected!" + Environment.NewLine; 
        }



        //Neuen ComPort erstellen
        meComPhySerialPort = new MeComPhySerialPort()
        {
            ReadTimeout = 500,
        };


        //Verbindung aufbauen
        try
        {
            meComPhySerialPort.OpenWithDefaultSettings(ComPort_select.Text, 57600);
            TEC_Connection = meComPhySerialPort;
        }
        catch (UnauthorizedAccessException)
        {
            return "TEC controller: No COM-Port already in use!" + Environment.NewLine;
        }
        catch (System.IO.IOException)
        {
            return "TEC controller: No COM-Port is not available!" + Environment.NewLine;
        }

        myLoadScreen.ChangeTask( "Checking device ...", iterration);

        //Abfragen ob richtiges Gerät
        Int32 diviceType = 0;
        try
        {
            diviceType = ReadValueInt("DeviceType", 1);
        }
        catch (MeComPhyTimeoutException)
        {
            meComPhySerialPort.Close();
            return "TEC controller: COM Port represents no Meerstetter TEC Controller!" + Environment.NewLine;
        }
        catch (ComCommandException)
        {
            meComPhySerialPort.Close();
            return "TEC controller: COM Port represents no Meerstetter TEC Controller!" + Environment.NewLine;
        }

        if (diviceType != 1122)
        {
            meComPhySerialPort.Close();
            return "TEC controller: Not Correct Device: TEC - " + diviceType.ToString() + "selected" + Environment.NewLine;
        }

        myLoadScreen.ChangeTask("Loading init file ...", iterration);

        //Jetzt ist verbunden
        IsConnected = true;

        //Alle Daten aus ini-File senden
        SendRegisters2TEC_Controller();

        myLoadScreen.ChangeTask( "Change GUI ...", iterration);

        //Eingestellte Temperatur auslesen
        Target_temp_aver = (GetTargetTemperature(1) + GetTargetTemperature(2)) / 2;
        UI_TargetTemp.Value = (decimal)Target_temp_aver;

        //Oberfläche anpassen
        ComPort_select.Enabled = false;
        barButtonItem_Detailed.Enabled = true;
        Switch_OnOff.Enabled = true;
        UI_TargetTemp.Enabled = true;

        OpenClose.Text = "Close";

        //Timer Starten
        threath_05sec_timer = new Parallel_Thread_TEC();
        threath_05sec_timer.Event_OnParallelThread += new OnParaThreadHandler(OnThreath_05sec_timer);
        threath_05sec_timer.timer_05sec.Start();

        return "";

    }
    */


    }
}
