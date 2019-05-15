using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using _8_Rth_TEC_Rack;

using DevExpress.XtraCharts;

using ATIM_GUI._0_Classes_Measurement;
using ATIM_GUI._2_AutoConnect;
using ATIM_GUI._4_Settings;

namespace ATIM_GUI._09_DAQ_Unit
{
    public partial class DAQ_Unit : UserControl
    {

        /**********************************************************************************************************************
        * Oberklasse für alle DAQ-Systeme
        * Schmid Maximlian
        * 25.10.2017
        * V1_0 (funkionsfähig)
        * 
        * -Aufteilung in NI-Karte & Spectrum-Karte
        * -Enthält GUI
        * -Enthält alle funktionen die global aufgerufen werden müssen 
        *********************************************************************************************************************/

        //********************************************************************************************************************
        //                                           Variablen
        //********************************************************************************************************************
        //Variablen die alle DAQ-Systeme benötigen
        public Boolean IsConnected { get; internal set; } = false;

        //Mess-Parameter
        public long Range { get; internal set; }
        public long Frequency { get; internal set; }
        public long Trigger_Level_UI { get; internal set; }

        //Log
        public string Communication_LOG { get; internal set; }

        //IP - Adresse Device
        public string VISA_or_Channel_Name { get; set; } = "";


        //********************************************************************************************************************
        //                                      GUI of MAIN CLASS
        //********************************************************************************************************************

        internal void Init_GUI()
        {
            InitializeComponent();
        }

        //Change Enable Status form MainForm
        public void Change_Enabled(Boolean input)
        {
            if (groupBox_DAQ.InvokeRequired)
            {
                groupBox_DAQ.Invoke((MethodInvoker)delegate
                {
                    groupBox_DAQ.Enabled = input;
                });
            }
            else
            {
                groupBox_DAQ.Enabled = input;
            }
        }

        //********************************************************************************************************************
        //                              UI:   ComboBox / Buttons / Numerical Up Down
        //********************************************************************************************************************

        #region Button

        private void OpenClose_Click(object sender, EventArgs e)
        {
            if (!IsConnected)
                Open();
            else
                Close();
        }

        #endregion Button

        #region AutoOpen

        public virtual string AutoOpen(Load_Screen myLoadScreen){ return ""; }

        #endregion AutoOpen

        #region ComboBox

        internal virtual void ComboBox_Range_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Range.Text)
            {
                case "+/- 200mV":
                    Range = 200;
                    break;
                case "+/- 500mV":
                    Range = 500;
                    break;
                case "+/- 1V":
                    Range = 1000;
                    break;
                case "+/- 2V":
                    Range = 2000;
                    break;
                case "+/- 5V":
                    Range = 5000;
                    break;
                case "+/- 10V":
                    Range = 10000;
                    break;
            }
        }

        internal virtual void ComboBox_Frequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Frequency.Text)
            {
                case "10MHz":
                    Frequency = 10000000;
                    break;
                case "5MHz":
                    Frequency = 5000000;
                    break;
                case "2MHz":
                    Frequency = 2000000;
                    break;
                case "1MHz":
                    Frequency = 1000000;
                    break;
            }
        }

        #endregion ComboBox


        //********************************************************************************************************************
        //                                  Befehle die in jedem Typ anderst sind
        //********************************************************************************************************************

        public virtual void Open() { }
        public virtual void Close() { }

        public virtual bool Setting_for_TTA() { return false; }
        public virtual bool Setting_Trigger(RthTEC_Rack myRackSettings){ return false; }
        public virtual bool Measure_TTA_Several_Cycles(TTA_measurement myTTA, Form1 GUI) { return false; }
        public virtual bool Measure_TTA_Several_Cycles_DEMO(TTA_measurement myTTA, Form1 GUI){ return false; }   
        public virtual bool Setting_for_Sensitivity(Sensitvity_Measurement mySensitivity){ return false; }  
        public virtual bool Measure_Sensitivity(Sensitvity_Measurement mySensitivity, Form1 GUI){ return false; }
        public virtual bool Measure_Sensitivity_DEMO(Sensitvity_Measurement mySensitivity){ return false; }

        public virtual decimal Get_Constant_Voltage(TTA_measurement myTTA, Form1 GUI) { return 0; }

        //********************************************************************************************************************
        //                                  Befehle die in jedem Typ anderst sind
        //********************************************************************************************************************

        public void AutoSettings(Settings_ATIM mySettings)
        {
            comboBox_Frequency.SelectedItem = mySettings.Spectrum_frequency;
            comboBox_Range.SelectedItem = mySettings.Spectrum_range;
            numericUpDown_Trigger.Value = mySettings.Spectrum_triggerLevel;
        }

    }
}
