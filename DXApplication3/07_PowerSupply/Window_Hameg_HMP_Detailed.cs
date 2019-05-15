using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIM_GUI._7_PowerSupply
{
    public partial class Window_Hameg_HMP_Detailed : Form
    {
        //Variable für calling Button
        Hameg_HMP calling;


        public Window_Hameg_HMP_Detailed(Hameg_HMP calling_Button)
        {
            InitializeComponent();

            calling = calling_Button;

            //Werte der Quelle annehmen
            InitInterface();

            //Timer starten
            timer_1500ms.Start();
        }

        public void InitInterface()
        {
            //Alle Werte übernehmen
            VoltageSet1.Value = calling.Voltage[0];
            VoltageSet2.Value = calling.Voltage[1];
            VoltageSet3.Value = calling.Voltage[2];
            VoltageSet4.Value = calling.Voltage[3];

            CurrentSet1.Value = calling.Current[0];
            CurrentSet2.Value = calling.Current[1];
            CurrentSet3.Value = calling.Current[2];
            CurrentSet4.Value = calling.Current[3];

            SwitchOnOff1.Checked = calling.StatusOnOff[0];
            SwitchOnOff2.Checked = calling.StatusOnOff[1];
            SwitchOnOff3.Checked = calling.StatusOnOff[2];
            SwitchOnOff4.Checked = calling.StatusOnOff[3];
        }


        //*******************Button Events************************************

        #region Set_Events

        private void VoltageSet1_ValueChanged(object sender, EventArgs e)
        {
            calling.SetVoltage(VoltageSet1.Value, 1);
        }

        private void VoltageSet2_ValueChanged(object sender, EventArgs e)
        {
            calling.SetVoltage(VoltageSet2.Value, 2);
        }

        private void VoltageSet3_ValueChanged(object sender, EventArgs e)
        {
            calling.SetVoltage(VoltageSet3.Value, 3);
        }

        private void VoltageSet4_ValueChanged(object sender, EventArgs e)
        {
            calling.SetVoltage(VoltageSet4.Value, 4);
        }

        private void CurrentSet1_ValueChanged(object sender, EventArgs e)
        {
            calling.SetCurrent(CurrentSet1.Value, 1);
        }

        private void CurrentSet2_ValueChanged(object sender, EventArgs e)
        {
            calling.SetCurrent(CurrentSet2.Value, 2);
        }

        private void CurrentSet3_ValueChanged(object sender, EventArgs e)
        {
            calling.SetCurrent(CurrentSet3.Value, 3);
        }

        private void CurrentSet4_ValueChanged(object sender, EventArgs e)
        {
            calling.SetCurrent(CurrentSet4.Value, 4);
        }

        private void SwitchOnOff1_CheckedChanged(object sender, EventArgs e)
        {
            calling.SetOnOff(SwitchOnOff1.Checked, 1);
        }

        private void SwitchOnOff2_CheckedChanged(object sender, EventArgs e)
        {
            calling.SetOnOff(SwitchOnOff2.Checked, 2);
        }

        private void SwitchOnOff3_CheckedChanged(object sender, EventArgs e)
        {
            calling.SetOnOff(SwitchOnOff3.Checked, 3);
        }

        private void SwitchOnOff4_CheckedChanged(object sender, EventArgs e)
        {
            calling.SetOnOff(SwitchOnOff4.Checked, 4);
        }


        #endregion set_Events

        //*******************Beim Schließen************************************

        private void Window_Hameg_HMP_Detailed_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_1500ms.Stop();
            calling.groupBox_HMT.Enabled = true;
        }

        //*******************Timmer*********************************************
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //Nur abf
            VoltageMeas1.Text = calling.GetVoltage(1) + " V";
            VoltageMeas2.Text = calling.GetVoltage(2) + " V";
            VoltageMeas3.Text = calling.GetVoltage(3) + " V";
            VoltageMeas4.Text = calling.GetVoltage(4) + " V";

            CurrentMeas1.Text = calling.GetCurrent(1) + " A";
            CurrentMeas2.Text = calling.GetCurrent(2) + " A";
            CurrentMeas3.Text = calling.GetCurrent(3) + " A";
            CurrentMeas4.Text = calling.GetCurrent(4) + " A";
        }


    }
}
