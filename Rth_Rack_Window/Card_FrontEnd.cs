using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RthTEC_Rack
{
    public partial class Card_FrontEnd : UserControl, I_RthTEC_Card, I_CardType_Amp
    {
        //********************************************************************************************************************
        //                                                  Variablen
        //********************************************************************************************************************

        /// <summary>
        /// Flag, if Card is active in pulsing
        /// </summary>
        public Boolean IsActiv { get; set; }

        /// <summary>
        /// Number of the Slot in which the card is placed
        /// </summary>
        public int Slot_Nr { get; set; }

        /// <summary>
        /// Gain of Amplifier (without Unit)
        /// </summary>
        public decimal Gain { get; set; }

        /// <summary>
        /// Offset Voltage of Amplifier in mV
        /// </summary>
        public decimal V_Offset { get; set; }

        /// <summary>
        /// Micro Controller for Communication
        /// </summary>
        private I_RthTEC MyMC { get; }

        //********************************************************************************************************************
        //                                                 Konstruktor
        //********************************************************************************************************************

        public Card_FrontEnd(I_RthTEC calling, int slotNr)
        {
            //Init LED Source UserControll
            InitializeComponent();

            //MC & Slot zuweisen
            MyMC = calling;
            Slot_Nr = slotNr;

            //Limits & Iteration
            comboBox_gain.Items.Add("25 (+/-100mV)");
            comboBox_gain.Items.Add("50 (+/- 50mV)");
            comboBox_gain.Items.Add("100(+/- 25mV)");

            numericUpDown_Offset.Minimum = 0;
            numericUpDown_Offset.Maximum = 10;
            numericUpDown_Offset.Increment = 0.1m;
            numericUpDown_Offset.DecimalPlaces = 1;

            //Get current values from µC
            Gain = Get_Gain();
            V_Offset = Get_Offset_Voltage();

            //In GUI Numeric Up Down
            numericUpDown_Offset.Value = V_Offset;
            Gain2ComboBox(Gain);

        }

        public void Add_to_DetailedForm(Window_RthTEC_Rack myDetailed, int x, int y)
        {
            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "Amplfier";
            this.Size = new System.Drawing.Size(116, 680);
            this.TabIndex = 40;

            //Hinzufügen
            myDetailed.Controls.Add(this);

            //Nach vorne bringen
            this.BringToFront();

            //Activation Bit setzen
            checkBox.Checked = IsActiv;
        }

        //********************************************************************************************************************
        //                                                  GUI-Events
        //********************************************************************************************************************


        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsActiv = checkBox.Checked;
            MyMC.SetSlotActivation(checkBox.Checked, Slot_Nr);
        }

        private void ComboBox_gain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string temp = comboBox_gain.SelectedItem.ToString().Substring(0, comboBox_gain.SelectedItem.ToString().IndexOf('('));
                Set_Gain(Convert.ToDecimal(temp));

            }
            catch (Exception)
            {
                MessageBox.Show("Pleas select available Gain", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void NumericUpDown_Offset_ValueChanged(object sender, EventArgs e)
        {
            Set_Offset_Voltage(numericUpDown_Offset.Value * 1000);
        }

        private void SimpleButton_RefreshVoltage_Click(object sender, EventArgs e)
        {
            textBox_InputVoltage.Text = Get_FullRange_Voltage().ToString();
        }

        //********************************************************************************************************************
        //                               FCT (teilweise global um von extern zugreifen zu können)
        //********************************************************************************************************************

        #region FCT

        public void Set_Gain(decimal gain)
        {
            Gain = gain;
            MyMC.Write_N_Read("SWG" + Slot_Nr.ToString() + "F " + gain.ToString("#"));
        }

        public void Set_Offset_Voltage(decimal voltage_in_mV)
        {
            V_Offset = voltage_in_mV;
            MyMC.Write_N_Read("SWO" + Slot_Nr.ToString() + "F " + voltage_in_mV.ToString("#"));
        }

        private decimal Get_Gain()
        {

            string answer = MyMC.Write_N_Read("GWG" + Slot_Nr.ToString() + "F");

            answer = answer.Substring(6, answer.LastIndexOf(' ') - 6);

            return Convert.ToDecimal(answer);

        }

        private decimal Get_Offset_Voltage()
        {
            string answer = MyMC.Write_N_Read("GWO" + Slot_Nr.ToString() + "F");

            answer = answer.Substring(6, answer.LastIndexOf(' ') - 6);

            return Convert.ToDecimal(answer);
        }

        /// <summary>
        /// Returns the Full-Range Volage of the Input in [mV]
        /// </summary>
        /// <returns></returns>
        private decimal Get_FullRange_Voltage()
        {
            string answer = MyMC.Write_N_Read("GVF" + Slot_Nr.ToString() + "F");

            answer = answer.Substring(6, answer.LastIndexOf(' ') - 6);

            return Convert.ToDecimal(answer) / 1000;
        }

        #endregion FCT

        //********************************************************************************************************************
        //                               FCT (teilweise global um von extern zugreifen zu können)
        //********************************************************************************************************************

        private void Gain2ComboBox(decimal input)
        {
            if (input == 25)
                comboBox_gain.SelectedText = "25 (+/-100mV)";
            else if(input == 50)
                comboBox_gain.SelectedText = "50 (+/- 50mV)";
            else if (input == 100)
                comboBox_gain.SelectedText = "100(+/- 25mV)";
            else
                comboBox_gain.SelectedText = "Select gain";
        }


    }
}
