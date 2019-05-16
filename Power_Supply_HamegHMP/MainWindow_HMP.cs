using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;

namespace Power_Supply_HamegHMP
{
    public partial class MainWindow_HMP : Form
    {

        //Variabls***************************************************************
        PowerSupply_HMP myHMP;

        //Initialization*********************************************************

        //Open as single Programm
        public MainWindow_HMP()
        {
            //Fenster generieren
            InitializeComponent();

            //HMP neu anlegen
            myHMP = new PowerSupply_HMP();

            //Alle ComPorts suchen & und ersten auswählen
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                ComPort_select.Items.Add(port);
            ComPort_select.SelectedIndex = 0;

        }

        //Open from MainProgamm
        public MainWindow_HMP(PowerSupply_HMP callingHMP)
        {
            //Fenster initialisieren
            InitializeComponent();

            //Quelle übernehmen
            myHMP = callingHMP;

            //Werte der Quelle annehmen
            InitInterface();

            //ComPort daten übernehmen
            foreach (var COM in myHMP.ComPort_select.Items)
                ComPort_select.Items.Add(COM);
            ComPort_select.SelectedIndex = myHMP.ComPort_select.SelectedIndex;
          

            //Oberfläche je nach dem ob verbunden anpassen
            if (myHMP.IsConnected)
            {
                //Oberfläche anpassen an diesen fall
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                button_OpenClose.Text = "Close";
                button_ShowLog.Enabled = true;
                ComPort_select.Enabled = false;

                //Timer starten
                timer_1500ms.Start();
            }
            else
            {
                //Oberfläche anpassen an diesen fall
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                button_OpenClose.Text = "Open";
                button_ShowLog.Enabled = true;
                ComPort_select.Enabled = true;
            }


        }

        //Help*******************************************************************

        public void InitInterface()
        {
            //Alle Werte übernehmen
            VoltageSet1.Value = myHMP.Voltage[0];
            VoltageSet2.Value = myHMP.Voltage[1];
            VoltageSet3.Value = myHMP.Voltage[2];
            VoltageSet4.Value = myHMP.Voltage[3];

            CurrentSet1.Value = myHMP.Current[0];
            CurrentSet2.Value = myHMP.Current[1];
            CurrentSet3.Value = myHMP.Current[2];
            CurrentSet4.Value = myHMP.Current[3];

            SwitchOnOff1.Checked = myHMP.StatusOnOff[0];
            SwitchOnOff2.Checked = myHMP.StatusOnOff[1];
            SwitchOnOff3.Checked = myHMP.StatusOnOff[2];
            SwitchOnOff4.Checked = myHMP.StatusOnOff[3];
        }

        //*******************Button Events***************************************

        #region Main_Buttons

        private void Button_OpenClose_Click(object sender, EventArgs e)
        {
            if (!myHMP.IsConnected)
            {
                if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
                {
                    MessageBox.Show("A COM Port has to be selected! \nTry again.", "Warning",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //COMPort anpassen
                myHMP.Serial_Interface.PortName = ComPort_select.Text;

                //Verbindung herstellen
                try
                {
                    myHMP.Serial_Interface.Open();
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
                myHMP.Type = myHMP.Write_N_Read("*IDN?");

                //Checken ob vom richtigen Typ
                if (!myHMP.Type.Contains("HMP4040"))
                {
                    MessageBox.Show("COM Port represents no Hameg HMP power source!\nTry again.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    myHMP.Serial_Interface.Close();
                    return;
                }

                //Jetzt verbunden
                myHMP.IsConnected = true;


                //Oberfläche anpassen
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                button_ShowLog.Enabled = true;
                ComPort_select.Enabled = false;

                button_OpenClose.Text = "Close";

                //Default Einstellungen setzen
                myHMP.SetDefaultSetup();

                //Werte der Quelle annehmen
                InitInterface();

                //Timer starten
                timer_1500ms.Start();
            }

            else
            {
                //Timer stoppen
                timer_1500ms.Stop();

                //Alle Kanäle ausschalten
                myHMP.SwitchOffAll();

                //COMPort schließen
                myHMP.Serial_Interface.Close();

                //Oberfläche anpassen
                myHMP.IsConnected = false;

                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                button_OpenClose.Text = "Open";
                button_ShowLog.Enabled = false;

                ComPort_select.Enabled = true;

            }
        }

        private void Button_ShowLog_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LOG:\n" + myHMP.Communication_LOG, "Communication log Rth-Rack table");
        }

        #endregion Main_Buttons

        #region Set_Events

        private void VoltageSet1_ValueChanged(object sender, EventArgs e)
        {
            if(myHMP.IsConnected)
                myHMP.SetVoltage(VoltageSet1.Value, 1);
        }

        private void VoltageSet2_ValueChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetVoltage(VoltageSet2.Value, 2);
        }

        private void VoltageSet3_ValueChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetVoltage(VoltageSet3.Value, 3);
        }

        private void VoltageSet4_ValueChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetVoltage(VoltageSet4.Value, 4);
        }

        private void CurrentSet1_ValueChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetCurrent(CurrentSet1.Value, 1);
        }

        private void CurrentSet2_ValueChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetCurrent(CurrentSet2.Value, 2);
        }

        private void CurrentSet3_ValueChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetCurrent(CurrentSet3.Value, 3);
        }

        private void CurrentSet4_ValueChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetCurrent(CurrentSet4.Value, 4);
        }

        private void SwitchOnOff1_CheckedChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetOnOff(SwitchOnOff1.Checked, 1);
        }

        private void SwitchOnOff2_CheckedChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetOnOff(SwitchOnOff2.Checked, 2);
        }

        private void SwitchOnOff3_CheckedChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetOnOff(SwitchOnOff3.Checked, 3);
        }

        private void SwitchOnOff4_CheckedChanged(object sender, EventArgs e)
        {
            if (myHMP.IsConnected)
                myHMP.SetOnOff(SwitchOnOff4.Checked, 4);
        }


        #endregion set_Events

        //*******************Beim Schließen************************************

        private void Window_Hameg_HMP_Detailed_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Timer stoppen
            timer_1500ms.Stop();
            //Groupbox wieder aktivieren
            myHMP.groupBox_HMT.Enabled = true;
            //Oberfläche anpassen
            if (myHMP.IsConnected)
            {
                myHMP.ComPort_select.Enabled = false;
                myHMP.Voltage_Heat.Enabled = true;
                myHMP.button_OpenClose.Text = "Close";
            }
            else
            {
                myHMP.ComPort_select.Enabled = true;
                myHMP.Voltage_Heat.Enabled = false;
                myHMP.button_OpenClose.Text = "Open";
            }

            /*
            while (!isCloseAble)
            {
                button_OpenClose.Text = "Waiting...";
            }*/
        }

        //*******************Timmer*********************************************
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //Alle Werte Abfragen
            VoltageMeas1.Text = myHMP.GetVoltage(1) + " V";
            VoltageMeas2.Text = myHMP.GetVoltage(2) + " V";
            VoltageMeas3.Text = myHMP.GetVoltage(3) + " V";
            VoltageMeas4.Text = myHMP.GetVoltage(4) + " V";

            CurrentMeas1.Text = myHMP.GetCurrent(1) + " A";
            CurrentMeas2.Text = myHMP.GetCurrent(2) + " A";
            CurrentMeas3.Text = myHMP.GetCurrent(3) + " A";
            CurrentMeas4.Text = myHMP.GetCurrent(4) + " A";
        }

    }
}
