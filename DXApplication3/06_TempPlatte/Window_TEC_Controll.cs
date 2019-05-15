using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MeSoft.MeCom.PhyWrapper;
using MeSoft.MeCom.Core;
using System.IO;
using System.Timers;

namespace ATIM_GUI._6_TempPlatte
{
    public partial class Window_TEC_Controll : Form
    {
        private TEC_Meerstetter callingComponent;

        //******************************************************************************************
        //                                   Fenster initialisieren & Schließen
        //******************************************************************************************

        public Window_TEC_Controll(TEC_Meerstetter calling)
        {
            callingComponent = calling;
            InitializeComponent();

            //initialization of the numeric up_down
            TEC1_TargetTemp.Value = (decimal)callingComponent.Target_temp[0];
            TEC2_TargetTemp.Value = (decimal)callingComponent.Target_temp[1];
            TEC1_P.Value = (decimal)callingComponent.Get_P(1);
            TEC2_P.Value = (decimal)callingComponent.Get_P(2);
            TEC1_I.Value = (decimal)callingComponent.Get_I(1);
            TEC2_I.Value = (decimal)callingComponent.Get_I(2);
            TEC1_D.Value = (decimal)callingComponent.Get_D(1);
            TEC2_D.Value = (decimal)callingComponent.Get_D(2);

            //Initialization of bottons On/Off
            if (callingComponent.TEC_on[0])
                TEC1_OnOff.Text = "Switch Off";
            else
                TEC1_OnOff.Text = "Switch On";


            if (callingComponent.TEC_on[1])
                TEC2_OnOff.Text = "Switch Off";
            else
                TEC2_OnOff.Text = "Switch On";


            if (callingComponent.GetFanStatus(1))
                TEC1_Fan.Text = "Fan Off";
            else
                TEC1_Fan.Text = "Fan On";


            if (callingComponent.GetFanStatus(2))
                TEC2_Fan.Text = "Fan Off";
            else
                TEC2_Fan.Text = "Fan On";

        }


        private void TEC_Controll_Window_FormClosed(object sender, FormClosedEventArgs e)
        {
            //flag setzen
            callingComponent.WindowOpen = false;

            //Button wieder aktiviern
            callingComponent.barButtonItem_Detailed.Enabled = true;
        }

        //******************************************************************************************
        //                                   Temperature Setting
        //******************************************************************************************

        private void TEC1_TargetTemp_ValueChanged(object sender, EventArgs e)
        {
            callingComponent.SetTemperature((float)TEC1_TargetTemp.Value, 1);
        }

        private void TEC2_TargetTemp_ValueChanged(object sender, EventArgs e)
        {
            callingComponent.SetTemperature((float)TEC1_TargetTemp.Value, 2);
        }

        //******************************************************************************************
        //                                   On / Off Buttons
        //******************************************************************************************

        private void TEC1_OnOff_Click(object sender, EventArgs e)
        {
            if (TEC1_OnOff.Text == "Switch On")
            {
                //Anschalten
                callingComponent.Switch_Channel_OnOff(true, 1);
                //Text auf Button ändern
                TEC1_OnOff.Text = "Switch Off";
            }
            else
            {
                //Ausschalten
                callingComponent.Switch_Channel_OnOff(false, 1);
                //Text ändern
                TEC1_OnOff.Text = "Switch On";
            }


            //Buttons im Hauptfester ändern (wenn einer von beiden an ist, dann aus)
            if (callingComponent.TEC_on[0] | callingComponent.TEC_on[1])
            {
                callingComponent.Switch_OnOff.Text = "Switch Off";
            }
            else
            {
                callingComponent.Switch_OnOff.Text = "Switch On";
            }
        }

        private void TEC2_OnOff_Click(object sender, EventArgs e)
        {
            if (TEC2_OnOff.Text == "Switch On")
            {
                //Anschalten
                callingComponent.Switch_Channel_OnOff(true, 2);
                //Text ändern
                TEC2_OnOff.Text = "Switch Off";
            }
            else
            {
                //Ausschalten
                callingComponent.Switch_Channel_OnOff(false, 2);
                //Text ändern
                TEC2_OnOff.Text = "Switch On";
            }
            //Buttons im Hauptfester ändern (wenn einer von beiden an ist, dann aus)
            if (callingComponent.TEC_on[0] | callingComponent.TEC_on[1])
            {
                callingComponent.Switch_OnOff.Text = "Switch Off";
            }
            else
            {
                callingComponent.Switch_OnOff.Text = "Switch On";
            }
        }

        //******************************************************************************************
        //                                   Fan Control
        //******************************************************************************************

        private void TEC1_Fan_Click(object sender, EventArgs e)
        {
            if (TEC1_Fan.Text == "Fan On")
            {
                callingComponent.Switch_Fan_OnOff(true, 1);
                TEC1_Fan.Text = "Fan Off";
            }
            else
            {
                callingComponent.Switch_Fan_OnOff(false, 1);
                TEC1_Fan.Text = "Fan On";
            }
        }

        private void TEC2_Fan_Click(object sender, EventArgs e)
        {
            if (TEC2_Fan.Text == "Fan On")
            {
                callingComponent.Switch_Fan_OnOff(true, 2);
                TEC2_Fan.Text = "Fan Off";
            }
            else
            {
                callingComponent.Switch_Fan_OnOff(false, 2);
                TEC2_Fan.Text = "Fan On";
            }
        }


        //******************************************************************************************
        //                                   PID
        //******************************************************************************************

        private void TEC1_P_ValueChanged(object sender, EventArgs e)
        {
            callingComponent.Set_P((float)TEC1_P.Value, 1);
        }

        private void TEC1_I_ValueChanged(object sender, EventArgs e)
        {
            callingComponent.Set_P((float)TEC1_P.Value, 2);
        }

        private void TEC2_P_ValueChanged(object sender, EventArgs e)
        {
            callingComponent.Set_I((float)TEC1_I.Value, 1);
        }


        private void TEC2_I_ValueChanged(object sender, EventArgs e)
        {
            callingComponent.Set_I((float)TEC1_I.Value, 2);
        }

        private void TEC1_D_ValueChanged(object sender, EventArgs e)
        {
            callingComponent.Set_D((float)TEC1_D.Value, 1);
        }

        private void TEC2_D_ValueChanged(object sender, EventArgs e)
        {
            callingComponent.Set_D((float)TEC1_D.Value, 2);
        }


    }
}
