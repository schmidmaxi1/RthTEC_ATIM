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
using System.Threading;

namespace Rth_Rack_Window
{
    public partial class Window_RthTEC_Rack : Form
    {

        /////////////////////////////////////////////////////////////////////
        //                     Variablen  (allgemein)                      //
        /////////////////////////////////////////////////////////////////////

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

        //Version of Rth_Rack
        public string DeviceType { get; internal set; }

        /////////////////////////////////////////////////////////////////////
        //                     Variablen  (für TTA)                        //
        /////////////////////////////////////////////////////////////////////

        //Liste mit den einzelnen Slots
        //public Slot_00_Insert[] Slots { get; internal set; }

        //Flags, ob Slot die Pulse_Sequence bekommen soll
        public bool[] Slot_Included_in_Pulses { get; set; } = { false, false, false, false, false, false, false, false};

        //Einstellungen für Pulse




        //Neu
        I_RthTEC callingRthTEC;

        /////////////////////////////////////////////////////////////////////
        //                          Initialization                         //
        /////////////////////////////////////////////////////////////////////

        public Window_RthTEC_Rack(I_RthTEC calling)
        {
            InitializeComponent();

            callingRthTEC = calling;

            //Alle Slots einfügen
            for (int i = 0; i < calling.Slot_Count; i++)
            {
                calling.Cards[i].Add_to_DetailedForm(this, 404 + i*116, 94);
            }

            //Zeit-Einstellungen
            numericUpDown_t_Heat.Value = callingRthTEC.Time_Heat;
            numericUpDown_t_Sense.Value = callingRthTEC.Time_Meas;
            numericUpDown_t_DPA.Value = callingRthTEC.DPA_Time;
            numericUpDown_count_DPA.Value = callingRthTEC.DPA_Count;

            //Dropdown auswählen
            comboBox_Mode.SelectedIndex = 0;

        }

        /////////////////////////////////////////////////////////////////////
        //                    Globale Funktionen (Pulse)                   //
        /////////////////////////////////////////////////////////////////////

            /*

        #region 2.Start pulses

      

        //5. Slot register
        public string SetSlotRegisters(Boolean[] input)
        {
            //Nachricht zusammenstellen
            string temp = "";
            for (int i = 0; i < 8; i++)
            {
                if (input[i])
                    temp = "1" + temp;
                else
                    temp = "0" + temp;
            }

            //Nachricht senden
            return Write_N_Read("SPR " + temp);
        }

        #endregion 2.Start pulses


        */

        /////////////////////////////////////////////////////////////////////
        //                              GUI                                //
        /////////////////////////////////////////////////////////////////////

            
        private void NumericUpDown_t_Heat_ValueChanged(object sender, EventArgs e)
        {
            callingRthTEC.SetHeatTime(numericUpDown_t_Heat.Value);
        }

        private void NumericUpDown_t_Sense_ValueChanged(object sender, EventArgs e)
        {
            callingRthTEC.SetMeasTime(numericUpDown_t_Sense.Value, numericUpDown_t_Heat.Value);
        }

        private void NumericUpDown_t_DPA_ValueChanged(object sender, EventArgs e)
        {
            callingRthTEC.SetDPATime(numericUpDown_t_DPA.Value);
        }

        private void NumericUpDown_count_DPA_ValueChanged(object sender, EventArgs e)
        {
            callingRthTEC.SetDPACount(Convert.ToInt32(numericUpDown_count_DPA.Value));
        }

        private void Button_StartPulse_Click(object sender, EventArgs e)
        {
            switch(comboBox_Mode.SelectedItem)
            {
                case "std. TTA":
                    callingRthTEC.Start_std_Pulse(true);
                    break;
                case "DPA TTA":
                    callingRthTEC.Start_DPA_Pulse(true);
                    break;
                case "Sensitivity":
                    callingRthTEC.Start_SEN_Pulse(true);
                    break;
                case "Pre Pulse":
                    callingRthTEC.Start_Pre_Pulse(true);
                    break;
            }
        }

        private void ComboBox_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Mode.SelectedItem)
            {
                case "std. TTA":
                    //Bild ändern
                    pictureBox_Mode.Image = global::Rth_Rack_Window.Properties.Resources.std_TTA;
                    //Visibility
                    numericUpDown_t_Heat.Visible = true;
                    numericUpDown_t_Sense.Visible = true;
                    numericUpDown_t_DPA.Visible = false;
                    numericUpDown_count_DPA.Visible = false;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = false;
                    label4.Visible = false;
                    break;
                case "DPA TTA":
                    pictureBox_Mode.Image = global::Rth_Rack_Window.Properties.Resources.DPA_TTA;
                    //Visibility
                    numericUpDown_t_Heat.Visible = true;
                    numericUpDown_t_Sense.Visible = true;
                    numericUpDown_t_DPA.Visible = true;
                    numericUpDown_count_DPA.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    break;
                case "Sensitivity":
                    //Bild ändern
                    pictureBox_Mode.Image = global::Rth_Rack_Window.Properties.Resources.Sensitivity;
                    //Visibility
                    numericUpDown_t_Heat.Visible = false;
                    numericUpDown_t_Sense.Visible = false;
                    numericUpDown_t_DPA.Visible = false;
                    numericUpDown_count_DPA.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    break;
                case "Pre Pulse":
                    //Bild ändern
                    pictureBox_Mode.Image = global::Rth_Rack_Window.Properties.Resources.PrePulse;
                    //Visibility
                    numericUpDown_t_Heat.Visible = false;
                    numericUpDown_t_Sense.Visible = false;
                    numericUpDown_t_DPA.Visible = false;
                    numericUpDown_count_DPA.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    break;
            }

        }

        /////////////////////////////////////////////////////////////////////
        //                      Closing                                   //
        /////////////////////////////////////////////////////////////////////

        private void Window_RthTEC_Rack_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Freigeben der Slot, damit sie bei nächsten öffenen wieder verfügbar sind
            this.Controls.Clear();

        }


        /////////////////////////////////////////////////////////////////////
        //                      Hilfs-Funktionen                           //
        /////////////////////////////////////////////////////////////////////


    }
}
