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
    public partial class Meerstetter_2fach_Detailed : Form
    {
        private Meerstetter_2fach myTEC;

        //******************************************************************************************
        //                                   Fenster initialisieren & Schließen
        //******************************************************************************************

        public Meerstetter_2fach_Detailed()
        {
            //Fenster generieren
            InitializeComponent();

            //Meerstetter neu anlegen
            myTEC = new Meerstetter_2fach()
            {
                TEC_Detailed = this
            };


            //Muss gemacht werden , da Buttons sonst nicht existent
            myTEC.CreateControl();

            //Alle ComPorts suchen & und ersten auswählen
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                ComPort_select.Items.Add(port);
            ComPort_select.SelectedIndex = 0;

        }

        public Meerstetter_2fach_Detailed(Meerstetter_2fach callingTEC)
        {
            //Fenster initialisieren
            InitializeComponent();
            //TEC übernehmen
            myTEC = callingTEC;

            //Werte übernehmen
            if(myTEC.IsConnected)
                Settings2GUI();

            //ComPort daten übernehmen
            foreach (var COM in myTEC.ComPort_select.Items)
                ComPort_select.Items.Add(COM);
            ComPort_select.SelectedIndex = myTEC.ComPort_select.SelectedIndex;

            //Oberfläche je nach dem ob verbunden anpassen
            if (myTEC.IsConnected)
            {
                //Oberfläche anpassen an diesen fall
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                button_OpenClose.Text = "Close";
                ComPort_select.Enabled = false;
            }
            else
            {
                //Oberfläche anpassen an diesen fall
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                button_OpenClose.Text = "Open";
                ComPort_select.Enabled = true;
            }
        }

        private void Meerstetter_2fach_Detailed_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Flag setzen
            myTEC.WindowOpen = false;
            //GroupBox wieder aktiviern
            myTEC.groupBox_TEC.Enabled = true;

            if (myTEC.IsConnected)
            {
                myTEC.ComPort_select.Enabled = false;
                myTEC.UI_TargetTemp.Enabled = true;
                myTEC.OpenClose.Text = "Close";
                myTEC.barButtonItem_OnOff.Enabled = true;
                myTEC.barButtonItem_Fan.Enabled = true;

            }
            else
            {
                myTEC.ComPort_select.Enabled = true;
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
                myTEC.ComPort_select.Text = ComPort_select.Text;

                //Öffnen
                myTEC.TEC_Open();

                //TEC anschalten
                myTEC.Switch_Channel_OnOff(true);
                //Fan anschalten
                myTEC.Switch_Fan_OnOff(true);

                //Oberfläche anpassen
                ComPort_select.Enabled = false;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;

                Settings2GUI();

                myTEC.WindowOpen = true;

                button_OpenClose.Text = "Close";
            }

            else
            {
                myTEC.TEC_Close();

                ComPort_select.Enabled = true;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;

                myTEC.WindowOpen = false;
                //barButtonItem_Detailed.Enabled = false;
                //Switch_OnOff.Enabled = false;
                //UI_TargetTemp.Enabled = false;
                button_OpenClose.Text = "Open";

            }

        }

        #region Settings

        //******************************************************************************************
        //                                   Temperature Setting
        //******************************************************************************************

        private void TEC1_TargetTemp_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
                myTEC.SetTemperature((float)TEC1_TargetTemp.Value, 1);
        }

        private void TEC2_TargetTemp_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)           
                myTEC.SetTemperature((float)TEC1_TargetTemp.Value, 2);
        }

        //******************************************************************************************
        //                                   On / Off Buttons
        //******************************************************************************************

        private void TEC1_OnOff_Click(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
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


                //Buttons im Hauptfester ändern (wenn einer von beiden an ist, dann aus)
                if (myTEC.TEC_on[0] | myTEC.TEC_on[1])
                {
                    //myTEC.Switch_OnOff.Text = "Switch Off";
                }
                else
                {
                    //myTEC.Switch_OnOff.Text = "Switch On";
                }
            }
        }

        private void TEC2_OnOff_Click(object sender, EventArgs e)
        {
            if (TEC2_OnOff.Text == "Switch On")
            {
                //Anschalten
                myTEC.Switch_Channel_OnOff(true, 2);
                //Text ändern
                TEC2_OnOff.Text = "Switch Off";
            }
            else
            {
                //Ausschalten
                myTEC.Switch_Channel_OnOff(false, 2);
                //Text ändern
                TEC2_OnOff.Text = "Switch On";
            }
            //Buttons im Hauptfester ändern (wenn einer von beiden an ist, dann aus)
            if (myTEC.TEC_on[0] | myTEC.TEC_on[1])
            {
                //myTEC.Switch_OnOff.Text = "Switch Off";
            }
            else
            {
                //myTEC.Switch_OnOff.Text = "Switch On";
            }
        }

        //******************************************************************************************
        //                                   Fan Control
        //******************************************************************************************

        private void TEC1_Fan_Click(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
            {
                if (TEC1_Fan.Text == "Fan On")
                {
                    myTEC.Switch_Fan_OnOff(true, 1);
                    TEC1_Fan.Text = "Fan Off";
                }
                else
                {
                    myTEC.Switch_Fan_OnOff(false, 1);
                    TEC1_Fan.Text = "Fan On";
                }
            }
        }

        private void TEC2_Fan_Click(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
            {
                if (TEC2_Fan.Text == "Fan On")
                {
                    myTEC.Switch_Fan_OnOff(true, 2);
                    TEC2_Fan.Text = "Fan Off";
                }
                else
                {
                    myTEC.Switch_Fan_OnOff(false, 2);
                    TEC2_Fan.Text = "Fan On";
                }
            }

        }


        //******************************************************************************************
        //                                   PID
        //******************************************************************************************

        private void TEC1_P_ValueChanged(object sender, EventArgs e)
        {
            if(myTEC.IsConnected)
                myTEC.Set_P((float)TEC1_P.Value, 1);
        }

        private void TEC1_I_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
                myTEC.Set_P((float)TEC1_P.Value, 2);
        }

        private void TEC2_P_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
                myTEC.Set_I((float)TEC1_I.Value, 1);
        }


        private void TEC2_I_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
                myTEC.Set_I((float)TEC1_I.Value, 2);
        }

        private void TEC1_D_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
                myTEC.Set_D((float)TEC1_D.Value, 1);
        }

        private void TEC2_D_ValueChanged(object sender, EventArgs e)
        {
            if (myTEC.IsConnected)
                myTEC.Set_D((float)TEC1_D.Value, 2);
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
            TEC1_P.Value = (decimal)myTEC.Get_P(1);
            TEC2_P.Value = (decimal)myTEC.Get_P(2);
            TEC1_I.Value = (decimal)myTEC.Get_I(1);
            TEC2_I.Value = (decimal)myTEC.Get_I(2);
            TEC1_D.Value = (decimal)myTEC.Get_D(1);
            TEC2_D.Value = (decimal)myTEC.Get_D(2);

            //Initialization of bottons On/Off
            if (myTEC.TEC_on[0])
                TEC1_OnOff.Text = "Switch Off";
            else
                TEC1_OnOff.Text = "Switch On";


            if (myTEC.TEC_on[1])
                TEC2_OnOff.Text = "Switch Off";
            else
                TEC2_OnOff.Text = "Switch On";


            if (myTEC.GetFanStatus(1))
                TEC1_Fan.Text = "Fan Off";
            else
                TEC1_Fan.Text = "Fan On";


            if (myTEC.GetFanStatus(2))
                TEC2_Fan.Text = "Fan Off";
            else
                TEC2_Fan.Text = "Fan On";
        }

    }
}
