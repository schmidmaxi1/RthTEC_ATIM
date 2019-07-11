using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rth_Rack_Window
{
    public partial class Card_LED_Source : UserControl, I_RthTEC_Card
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
        /// Heating Current in mA
        /// </summary>
        public decimal I_Heat { get; set; }

        /// <summary>
        /// Measuring Current in mA
        /// </summary>
        public decimal I_Meas { get; set; }

        /// <summary>
        /// Micro Controller for Communication
        /// </summary>
        private I_RthTEC MyMC { get; }

        //********************************************************************************************************************
        //                                                 Konstruktor
        //********************************************************************************************************************

        public Card_LED_Source(I_RthTEC calling, int slotNr)
        {
            //Init LED Source UserControll
            InitializeComponent();

            //MC & Slot zuweisen
            MyMC = calling;
            Slot_Nr = slotNr;

            //Limits & Iteration
            numericUpDown_I_Heat.Minimum = 0;
            numericUpDown_I_Heat.Maximum = 1500;
            numericUpDown_I_Heat.Increment = 100;
            numericUpDown_I_Heat.DecimalPlaces = 0;

            numericUpDown_I_Meas.Minimum = 0;
            numericUpDown_I_Meas.Maximum = 25;
            numericUpDown_I_Meas.Increment = 5;
            numericUpDown_I_Meas.DecimalPlaces = 1;

            //Get current values from µC
            I_Heat = Get_I_Heat();
            I_Meas = Get_I_Meas();

            //In GUI Numeric Up Down
            numericUpDown_I_Heat.Value = I_Heat;
            numericUpDown_I_Meas.Value = I_Meas;            

        }

        public void Add_to_DetailedForm(Window_RthTEC_Rack myDetailed, int x, int y)
        {
            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "LED_Source";
            this.Size = new System.Drawing.Size(116, 680);
            this.TabIndex = 33;

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

        private void NnumericUpDown_I_Heat_ValueChanged(object sender, EventArgs e)
        {
            Set_I_Heat(numericUpDown_I_Heat.Value);
        }

        private void NumericUpDown_I_Meas_ValueChanged(object sender, EventArgs e)
        {
            Set_I_Meas(numericUpDown_I_Meas.Value);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsActiv = checkBox.Checked;
            MyMC.SetSlotActivation(checkBox.Checked, Slot_Nr);
        }

        //********************************************************************************************************************
        //                                               local Functions
        //********************************************************************************************************************

        #region Local

        private void Set_I_Heat(decimal iHeat_in_mA)
        {
            MyMC.Write_N_Read("SHC" + Slot_Nr.ToString() + "L " + iHeat_in_mA.ToString("#"));
        }

        private void Set_I_Meas(decimal iMeas_in_mA)
        {
            MyMC.Write_N_Read("SMC" + Slot_Nr.ToString() + "L " + (iMeas_in_mA * 10).ToString("#"));
        }

        private decimal Get_I_Heat()
        {
            string answer = MyMC.Write_N_Read("GHC" + Slot_Nr.ToString() + "L");

            answer = answer.Substring(6, answer.LastIndexOf(' ') - 6);

            return Convert.ToDecimal(answer);
        }

        private decimal Get_I_Meas()
        {
            string answer = MyMC.Write_N_Read("GMC" + Slot_Nr.ToString() + "L");

            answer = answer.Substring(6, answer.LastIndexOf(' ') - 6);

            return Convert.ToDecimal(answer) / 10;
        }

        #endregion Local


    }
}
