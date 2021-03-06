﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;

using Hilfsfunktionen;
using Communication_Settings;
using AutoConnect;

namespace Power_Supply_HamegHMP
{
    public partial class PowerSupply_HMP : UserControl
    {

        //********************************************************************************************************************
        //                                              Variables
        //********************************************************************************************************************
        public bool IsConnected { get; internal set; } = false;
        public string Type { get; internal set; }

        //Default Values
        public decimal[] Voltage { get; internal set; } = new decimal[4];
        public decimal[] Current { get; internal set; } = new decimal[4];
        public bool[] StatusOnOff { get; internal set; } = new bool[4];

        //Default Einstellungen setzen
        public decimal[] Def_voltage { get; internal set; } = new decimal[] { 24, 0, 0, 10 };
        public decimal[] Def_current { get; internal set; } = new decimal[] { 2, 0, 0, 2 };
        public bool[] Def_statusOnOff { get; internal set; } = new bool[] { true, false, false, true };

        private static uint Ch_heat { get; } = 4;

        public uint Selected_channel { get; internal set; }

        public SerialPort Serial_Interface { get; set; } = new SerialPort()
        {
            BaudRate = 115200,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = 0,
            ReadTimeout = 500,
        };

        public string Communication_LOG { get; internal set; }


        //********************************************************************************************************************
        //                                          Initialization
        //********************************************************************************************************************

        //Normales Init
        public PowerSupply_HMP()
        {
            InitializeComponent();

            //Alle ComPorts suchen
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                ComPort_select.Items.Add(port);
        }

        //Einfaches einfügen in Andere Fenster
        public PowerSupply_HMP(Form callingForm, int x, int y)
        {
            InitializeComponent();

            //Alle ComPorts suchen
            HelpFCT.SetComPortBox(ComPort_select);

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "Hameg HMP 4040";
            this.Size = new System.Drawing.Size(515, 75);
            this.TabIndex = 30;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }

        //Change Enable Status form MainForm
        public void Change_Enabled(Boolean input)
        {
            if (groupBox_HMT.InvokeRequired)
            {
                groupBox_HMT.Invoke((MethodInvoker)delegate
                {
                    groupBox_HMT.Enabled = input;
                });
            }
            else
            {
                groupBox_HMT.Enabled = input;
            }


        }

        //********************************************************************************************************************
        //                                          GUI - Events
        //********************************************************************************************************************

        #region GUI-Events

        private void OpenClose_Click(object sender, EventArgs e)
        {
            if (!IsConnected)
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

                //Abfragen ob Power-Supply
                Type = Write_N_Read("*IDN?");

                //Checken ob vom richtigen Typ
                if (!Type.Contains("HMP4040"))
                {
                    MessageBox.Show("COM Port represents no Hameg HMP power source!\nTry again.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Serial_Interface.Close();
                    return;
                }

                //Default Einstellungen setzen
                //SetDefaultSetup();

                //Oberfläche anpassen
                IsConnected = true;

                Voltage_Heat.Enabled = true;

                ComPort_select.Enabled = false;

                button_OpenClose.Text = "Close";

                //Default Einstellungen setzen
                SetDefaultSetup();
            }

            else
            {
                //Alle Kanäle ausschalten
                SwitchOffAll();

                //COMPort schließen
                Serial_Interface.Close();

                //Oberfläche anpassen
                IsConnected = false;

                Voltage_Heat.Enabled = false;

                ComPort_select.Enabled = true;

                button_OpenClose.Text = "Open";
            }
        }

        private void Voltage_Heat_ValueChanged(object sender, EventArgs e)
        {
            SetVoltage(Voltage_Heat.Value, 4);
        }

        private void BarButtonItem_ShowLog_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("LOG:\n" + Communication_LOG, "Communication log Rth-Rack table");
        }

        private void BarButtonItem_Detailed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MainWindow_HMP myWindow = new MainWindow_HMP(this);
            myWindow.Show();

            groupBox_HMT.Enabled = false;
        }

        #endregion GUI-Events

        //********************************************************************************************************************
        //                                       Communiction Fuctions
        //********************************************************************************************************************

        #region Communication

        public string Write_N_Read(string message)
        {
            string antwort;
            //Nachricht senden
            Serial_Interface.WriteLine(message);
            //Auslesen versuchen
            try
            {
                antwort = Serial_Interface.ReadLine();
            }
            //Fehler bei Timeout schmeißen
            catch (TimeoutException)
            {
                //Log Füllen
                Communication_LOG += DateTime.Now.ToString() +
                    "\n     --> " + message +
                    "\n     <-- " + "TIMOUT" + "\n";

                return "TIMEOUT";
            }
            //Log Füllen
            Communication_LOG += DateTime.Now.ToString() +
                "\n     --> " + message +
                "\n     <-- " + antwort + "\n";


            return antwort;
        }

        public void Write_Only(string message)
        {
            //Nachricht senden
            Serial_Interface.WriteLine(message);

            //Log Füllen
            Communication_LOG += DateTime.Now.ToString() +
                "\n     --> " + message + "\n";

        }

        #endregion Communication

        //********************************************************************************************************************
        //                                          Adjuste Values
        //********************************************************************************************************************

        #region Adjuste_Values

        public void SetCurrent(decimal newCurrent, uint ch)
        {
            //Kanal umstellen
            ChangeChannel(ch);

            //Strom einstellen: CURRent {<current>|MIN|MAX|UP|DOWN}
            string message = "CURRent " + newCurrent.ToString().Replace(",", ".");
            Write_Only(message);

            //Global anpassen
            Current[ch - 1] = newCurrent;
        }

        public void SetVoltage(decimal newVoltage, uint ch)
        {
            //Kanal umstellen
            ChangeChannel(ch);

            //Spannung einstellen: VOLTage {<voltage>|MIN|MAX|UP|DOWN}
            string message = "VOLTage " + newVoltage.ToString().Replace(",", ".");
            Write_Only(message);

            //Global anpassen
            Voltage[ch - 1] = newVoltage;

            if (ch == Ch_heat)
            {
                Voltage_Heat.Value = newVoltage;
            }

        }

        private void ChangeChannel(uint ch)
        {
            if (ch != Selected_channel)
            {
                //Kanal umstellen: INSTrument:NSELect {1|2|3|4}
                string message = "INSTrument:NSELect " + ch.ToString();
                Write_Only(message);
            }
            //Global anpassen
            Selected_channel = ch;
        }

        public void SetOnOff(bool newStatus, uint ch)
        {
            //Kanal umstellen
            ChangeChannel(ch);

            //Status ändern: OUTPut {OFF|ON|0|1}
            string message = "OUTPut " + Convert.ToInt32(newStatus).ToString();
            Write_Only(message);

            //Global anpassen
            StatusOnOff[ch - 1] = newStatus;
        }

        public string GetCurrent(uint ch)
        {
            //Kanal umstellen
            ChangeChannel(ch);

            //STrom abfragen: MEASure:CURRent?
            string message = "MEASure:CURRent?";
            return Write_N_Read(message).Replace(".", ",");
        }

        public string GetVoltage(uint ch)
        {
            //Kanal umstellen
            ChangeChannel(ch);

            //Spannung abfragen: MEASure:VOLTage?
            string message = "MEASure:VOLTage?";
            return Write_N_Read(message).Replace(".", ",");
        }

        #endregion Adjuste_Values

        //********************************************************************************************************************
        //                                          Other Functions
        //********************************************************************************************************************

        #region otherFunctions

        public void SetDefaultSetup()
        {

            //Alle Kanäle auf Default setzen
            for (uint i = 1; i <= 4; i++)
            {
                SetVoltage(Def_voltage[i - 1], i);
                SetCurrent(Def_current[i - 1], i);
                SetOnOff(Def_statusOnOff[i - 1], i);
            }

            //Oberfläche anpassen
            Voltage_Heat.Value = Voltage[3];

        }

        public void SwitchOffAll()
        {
            //Alle Kanäle auf Default setzen (Rückwärts)
            for (uint i = 4; i >= 1; i--)
            {
                SetOnOff(false, i);
            }
        }



        #endregion otherFunctions

        //********************************************************************************************************************
        //                                           AutoConnect
        //********************************************************************************************************************

        #region AutoConnect

        public void Update_settings(SerialCommunicationDivice myInput)
        {
            //COM-Port eigenschaften übernehmen
            Serial_Interface = myInput.ToSerialPort();

            //Combox übernehen
            HelpFCT.SetComboBox2ComboBox(myInput.comboBox_Port, ComPort_select);
        }
        
        /// <summary>
        /// AutoConnects the HMP4040 Device with Loadscreen change
        /// </summary>
        /// <param name="myLoadScreen"> Window with ProgressBar </param>
        /// <returns></returns>
        public string AutoOpen(AutoConnect_Window myLoadScreen)
        {
            int iterration = 5;

            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                return "Power supply: No COM-Port selected!" + Environment.NewLine;
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
                return "Power supply: COM_Port is already used" + Environment.NewLine; ;
            }
            catch (System.IO.IOException)
            {
                return "Power supply: No COM-Port is not available!" + Environment.NewLine; ;
            }

            myLoadScreen.ChangeTask("Checking device ...", iterration);

            //Abfragen ob Power-Supply
            Type = Write_N_Read("*IDN?");

            //Checken ob vom richtigen Typ
            if (!Type.Contains("HMP4040"))
            {
                Serial_Interface.Close();
                return "Power supply: COM Port represents no Hameg HMP power source!" + Environment.NewLine; ;
            }

            myLoadScreen.ChangeTask("Adjusting GUI ...", iterration);

            //Default Einstellungen setzen
            //SetDefaultSetup();

            //Oberfläche anpassen
            IsConnected = true;

            Voltage_Heat.Enabled = true;
            barButtonItem_Detailed.Enabled = true;

            ComPort_select.Enabled = false;

            button_OpenClose.Text = "Close";

            //Default Einstellungen setzen
            SetDefaultSetup();

            return "";
        }

        #endregion AutoConnect

    }
}

