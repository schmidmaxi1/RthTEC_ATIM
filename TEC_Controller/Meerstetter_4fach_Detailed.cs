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
using MeSoft.MeCom.Core;
using MeSoft.MeCom.PhyWrapper;

namespace TEC_Controller
{
    public partial class Meerstetter_4fach_Detailed : Form
    {
        private Meerstetter_4fach myTEC;

        //******************************************************************************************
        //                                   Fenster initialisieren & Schließen
        //******************************************************************************************

        public Meerstetter_4fach_Detailed()
        {
            //Fenster generieren
            InitializeComponent();

            //Meerstetter neu anlegen
            myTEC = new Meerstetter_4fach()
            {
                TEC_Detailed = this
            };


            //Muss gemacht werden , da Buttons sonst nicht existent
            myTEC.CreateControl();

            //Alle ComPorts suchen & und ersten auswählen
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                ComPort_select1.Items.Add(port);
            ComPort_select1.SelectedIndex = 0;
            foreach (string port in ports)
                ComPort_select2.Items.Add(port);
            ComPort_select2.SelectedIndex = 0;

        }

        public Meerstetter_4fach_Detailed(Meerstetter_4fach callingTEC)
        {
            //Fenster initialisieren
            InitializeComponent();
            //TEC übernehmen
            myTEC = callingTEC;

            //Werte übernehmen
            if (myTEC.IsConnected)
                Settings2GUI();

            //ComPort daten übernehmen
            foreach (var COM in myTEC.ComPort_select1.Items)
                ComPort_select1.Items.Add(COM);
            ComPort_select1.SelectedIndex = myTEC.ComPort_select1.SelectedIndex;

            foreach (var COM in myTEC.ComPort_select2.Items)
                ComPort_select2.Items.Add(COM);
            ComPort_select2.SelectedIndex = myTEC.ComPort_select2.SelectedIndex;

            //Oberfläche je nach dem ob verbunden anpassen
            if (myTEC.IsConnected)
            {
                //Oberfläche anpassen an diesen fall
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                button_OpenClose.Text = "Close";
                ComPort_select1.Enabled = false;
                ComPort_select2.Enabled = false;
            }
            else
            {
                //Oberfläche anpassen an diesen fall
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                button_OpenClose.Text = "Open";
                ComPort_select1.Enabled = true;
                ComPort_select2.Enabled = true;
            }
        }

        private void Meerstetter_4fach_Detailed_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Flag setzen
            myTEC.WindowOpen = false;
            //GroupBox wieder aktiviern
            myTEC.groupBox_TEC.Enabled = true;

            if (myTEC.IsConnected)
            {
                myTEC.ComPort_select1.Enabled = false;
                myTEC.ComPort_select2.Enabled = false;
                myTEC.UI_TargetTemp.Enabled = true;
                myTEC.OpenClose.Text = "Close";
                myTEC.barButtonItem_OnOff.Enabled = true;
                myTEC.barButtonItem_Fan.Enabled = true;

            }
            else
            {
                myTEC.ComPort_select1.Enabled = true;
                myTEC.ComPort_select2.Enabled = true;
                myTEC.UI_TargetTemp.Enabled = false;
                myTEC.OpenClose.Text = "Open";
                myTEC.barButtonItem_OnOff.Enabled = false;
                myTEC.barButtonItem_Fan.Enabled = false;
            }
        }

        //******************************************************************************************
        //                                          GUI
        //******************************************************************************************

        private void Button_OpenClose_Click(object sender, EventArgs e)
        {
            if (!myTEC.IsConnected)
            {
                //COM übernehmen
                myTEC.ComPort_select1.Text = ComPort_select1.Text;
                myTEC.ComPort_select2.Text = ComPort_select2.Text;

                //Öffnen
                myTEC.TEC_Open();

                //TEC anschalten
                myTEC.Switch_Channel_OnOff(true);
                //Fan anschalten
                myTEC.Switch_Fan_OnOff(true);

                //Oberfläche anpassen
                ComPort_select1.Enabled = false;
                ComPort_select2.Enabled = false;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;

                Settings2GUI();

                myTEC.WindowOpen = true;

                button_OpenClose.Text = "Close";
            }

            else
            {
                myTEC.TEC_Close();

                ComPort_select1.Enabled = true;
                ComPort_select2.Enabled = true;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;

                myTEC.WindowOpen = false;

                button_OpenClose.Text = "Open";


            }

        }

        #region Settings

        //******************************************************************************************
        //                                   Temperature Setting
        //******************************************************************************************

        private void TEC_TargetTemp_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
            {
                if (sender.Equals(TEC1_TargetTemp))
                    myTEC.SetTemperature((float)TEC1_TargetTemp.Value, 1);
                else if (sender.Equals(TEC2_TargetTemp))
                    myTEC.SetTemperature((float)TEC2_TargetTemp.Value, 2);
                else if (sender.Equals(TEC3_TargetTemp))
                    myTEC.SetTemperature((float)TEC3_TargetTemp.Value, 3);
                else if (sender.Equals(TEC4_TargetTemp))
                    myTEC.SetTemperature((float)TEC4_TargetTemp.Value, 4);
            }
        }

        //******************************************************************************************
        //                                   On / Off Buttons
        //******************************************************************************************

        private void TEC_OnOff_Click(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
            {
                if (sender.Equals(TEC1_OnOff))
                {
                    if (TEC1_OnOff.Text == "Switch On")
                    {
                        //Anschalten
                        myTEC.Switch_Channel_OnOff(true, 1);
                        //Text auf Button ändern
                        TEC1_OnOff.Text = "Switch Off";
                    }
                    else
                    {
                        //Ausschalten
                        myTEC.Switch_Channel_OnOff(false, 1);
                        //Text ändern
                        TEC1_OnOff.Text = "Switch On";
                    }
                }
                else if (sender.Equals(TEC2_OnOff))
                {
                    if (TEC2_OnOff.Text == "Switch On")
                    {
                        //Anschalten
                        myTEC.Switch_Channel_OnOff(true, 2);
                        //Text auf Button ändern
                        TEC2_OnOff.Text = "Switch Off";
                    }
                    else
                    {
                        //Ausschalten
                        myTEC.Switch_Channel_OnOff(false, 2);
                        //Text ändern
                        TEC2_OnOff.Text = "Switch On";
                    }
                }
                else if (sender.Equals(TEC3_OnOff))
                {
                    if (TEC3_OnOff.Text == "Switch On")
                    {
                        //Anschalten
                        myTEC.Switch_Channel_OnOff(true, 3);
                        //Text auf Button ändern
                        TEC3_OnOff.Text = "Switch Off";
                    }
                    else
                    {
                        //Ausschalten
                        myTEC.Switch_Channel_OnOff(false, 3);
                        //Text ändern
                        TEC3_OnOff.Text = "Switch On";
                    }
                }
                else if (sender.Equals(TEC4_OnOff))
                {
                    if (TEC4_OnOff.Text == "Switch On")
                    {
                        //Anschalten
                        myTEC.Switch_Channel_OnOff(true, 4);
                        //Text auf Button ändern
                        TEC4_OnOff.Text = "Switch Off";
                    }
                    else
                    {
                        //Ausschalten
                        myTEC.Switch_Channel_OnOff(false, 4);
                        //Text ändern
                        TEC4_OnOff.Text = "Switch On";
                    }
                }

            }
        }

        //******************************************************************************************
        //                                   Fan Control
        //******************************************************************************************

        private void TEC_Fan_Click(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
            {
                if (sender.Equals(TEC1_Fan))
                {
                    if (TEC1_Fan.Text == "Fan On")
                    {
                        //Anschalten
                        myTEC.Switch_Fan_OnOff(true, 1);
                        //Text auf Button ändern
                        TEC1_Fan.Text = "Fan Off";
                    }
                    else
                    {
                        //Ausschalten
                        myTEC.Switch_Fan_OnOff(false, 1);
                        //Text ändern
                        TEC1_Fan.Text = "Fan On";
                    }
                }
                else if (sender.Equals(TEC2_Fan))
                {
                    if (TEC2_Fan.Text == "Fan On")
                    {
                        //Anschalten
                        myTEC.Switch_Fan_OnOff(true, 2);
                        //Text auf Button ändern
                        TEC2_Fan.Text = "Fan Off";
                    }
                    else
                    {
                        //Ausschalten
                        myTEC.Switch_Fan_OnOff(false, 2);
                        //Text ändern
                        TEC2_Fan.Text = "Fan On";
                    }
                }
                else if (sender.Equals(TEC3_Fan))
                {
                    if (TEC3_Fan.Text == "Fan On")
                    {
                        //Anschalten
                        myTEC.Switch_Fan_OnOff(true, 3);
                        //Text auf Button ändern
                        TEC3_Fan.Text = "Fan Off";
                    }
                    else
                    {
                        //Ausschalten
                        myTEC.Switch_Fan_OnOff(false, 3);
                        //Text ändern
                        TEC3_Fan.Text = "Fan On";
                    }
                }
                else if (sender.Equals(TEC4_Fan))
                {
                    if (TEC4_Fan.Text == "Fan On")
                    {
                        //Anschalten
                        myTEC.Switch_Fan_OnOff(true, 4);
                        //Text auf Button ändern
                        TEC4_Fan.Text = "Fan Off";
                    }
                    else
                    {
                        //Ausschalten
                        myTEC.Switch_Fan_OnOff(false, 4);
                        //Text ändern
                        TEC4_Fan.Text = "Fan On";
                    }
                }

            }
        }

        //******************************************************************************************
        //                                   PID
        //******************************************************************************************

        private void TEC_P_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
            {
                if (sender.Equals(TEC1_P))
                    myTEC.Set_P((float)TEC1_P.Value, 1);
                else if (sender.Equals(TEC2_P))
                    myTEC.Set_P((float)TEC2_P.Value, 2);
                else if (sender.Equals(TEC3_P))
                    myTEC.Set_P((float)TEC3_P.Value, 3);
                else if (sender.Equals(TEC4_P))
                    myTEC.Set_P((float)TEC4_P.Value, 4);
            }
        }

        private void TEC_I_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
            {
                if (sender.Equals(TEC1_I))
                    myTEC.Set_I((float)TEC1_I.Value, 1);
                else if (sender.Equals(TEC2_I))
                    myTEC.Set_I((float)TEC2_I.Value, 2);
                else if (sender.Equals(TEC3_I))
                    myTEC.Set_I((float)TEC3_I.Value, 3);
                else if (sender.Equals(TEC4_I))
                    myTEC.Set_I((float)TEC4_I.Value, 4);
            }
        }

        private void TEC_D_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
            {
                if (sender.Equals(TEC1_D))
                    myTEC.Set_D((float)TEC1_D.Value, 1);
                else if (sender.Equals(TEC2_D))
                    myTEC.Set_D((float)TEC2_D.Value, 2);
                else if (sender.Equals(TEC3_D))
                    myTEC.Set_D((float)TEC3_D.Value, 3);
                else if (sender.Equals(TEC4_D))
                    myTEC.Set_D((float)TEC4_D.Value, 4);
            }
        }
        #endregion Settings

        //******************************************************************************************
        //                                    Hilfs-Funktionen
        //******************************************************************************************

        private void Settings2GUI()
        {
            //initialization of the numeric up_down
            TEC1_TargetTemp.Value = (decimal)myTEC.Target_temp[0];
            TEC2_TargetTemp.Value = (decimal)myTEC.Target_temp[1];
            TEC3_TargetTemp.Value = (decimal)myTEC.Target_temp[2];
            TEC4_TargetTemp.Value = (decimal)myTEC.Target_temp[3];
            TEC1_P.Value = (decimal)myTEC.Get_P(1);
            TEC2_P.Value = (decimal)myTEC.Get_P(2);
            TEC3_P.Value = (decimal)myTEC.Get_P(3);
            TEC4_P.Value = (decimal)myTEC.Get_P(4);
            TEC1_I.Value = (decimal)myTEC.Get_I(1);
            TEC2_I.Value = (decimal)myTEC.Get_I(2);
            TEC3_I.Value = (decimal)myTEC.Get_I(3);
            TEC4_I.Value = (decimal)myTEC.Get_I(4);
            TEC1_D.Value = (decimal)myTEC.Get_D(1);
            TEC2_D.Value = (decimal)myTEC.Get_D(2);
            TEC3_D.Value = (decimal)myTEC.Get_D(3);
            TEC4_D.Value = (decimal)myTEC.Get_D(4);

            //Initialization of bottons On/Off
            if (myTEC.TEC_on[0])
                TEC1_OnOff.Text = "Switch Off";
            else
                TEC1_OnOff.Text = "Switch On";

            if (myTEC.TEC_on[1])
                TEC2_OnOff.Text = "Switch Off";
            else
                TEC2_OnOff.Text = "Switch On";

            if (myTEC.TEC_on[2])
                TEC3_OnOff.Text = "Switch Off";
            else
                TEC3_OnOff.Text = "Switch On";

            if (myTEC.TEC_on[3])
                TEC4_OnOff.Text = "Switch Off";
            else
                TEC4_OnOff.Text = "Switch On";


            if (myTEC.GetFanStatus(1))
                TEC1_Fan.Text = "Fan Off";
            else
                TEC1_Fan.Text = "Fan On";

            if (myTEC.GetFanStatus(2))
                TEC2_Fan.Text = "Fan Off";
            else
                TEC2_Fan.Text = "Fan On";

            if (myTEC.GetFanStatus(3))
                TEC3_Fan.Text = "Fan Off";
            else
                TEC3_Fan.Text = "Fan On";

            if (myTEC.GetFanStatus(4))
                TEC4_Fan.Text = "Fan Off";
            else
                TEC4_Fan.Text = "Fan On";
        }

    }
}
