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

    public partial class Card_MOSFET_BreakDown : UserControl, I_RthTEC_Card
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
        /// Micro Controller for Communication
        /// </summary>
        private I_RthTEC MyMC { get; }

        /// <summary>
        /// Current Relais Setting
        /// </summary>
        public int MyRelais { get; internal set; }
        
        /// <summary>
        /// Voltage Gate Source in [V] (adjusted)
        /// </summary>
        public decimal Voltage_Set_GS { get; internal set; }

        /// <summary>
        /// Voltage Drain Source in [V] (measured)
        /// </summary>
        public decimal Voltage_Get_DS { get; internal set; }

        /// <summary>
        /// Current Drain Source in [A] (measured)
        /// </summary>
        public decimal Current_Get_DS { get; internal set; }

        //********************************************************************************************************************
        //                                                 Konstruktor
        //********************************************************************************************************************

        public Card_MOSFET_BreakDown(I_RthTEC calling, int slotNr)
        {
            //Init LED Source UserControll
            InitializeComponent();

            //MC & Slot zuweisen
            MyMC = calling;
            Slot_Nr = slotNr;

            //ComboBox
            comboBox_Relais.Items.Add("Non");
            comboBox_Relais.Items.Add("BreskD. U_DS");
            comboBox_Relais.Items.Add("Leakage U_DS");
            comboBox_Relais.Items.Add("Leakage U_GS");
            comboBox_Relais.Items.Add("Charac. MOSFET");
            comboBox_Relais.Items.Add("Charac. BodyDiode");
            comboBox_Relais.SelectedIndex = 0;

            //Limits
            numericUpDown_U_GS.Maximum = 20;
            numericUpDown_U_GS.Minimum = -20;
            numericUpDown_U_GS.Increment = 1;
            numericUpDown_U_GS.DecimalPlaces = 1;

            //Get V_GS
            Voltage_Set_GS = Get_V_GS();

            //In GUI
            numericUpDown_U_GS.Value = Voltage_Set_GS;

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

        private void ComboBox_Relais_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Relais.SelectedIndex)
            {
                case Relais_Setting.No_Relais:
                    Set_Relais_Open();
                    break;

                case Relais_Setting.Breakdown_DS:
                    Set_Relais_BreakDown_DS();
                    break;

                case Relais_Setting.Leakage_DS:
                    Set_Relais_Leakage_DS();
                    break;

                case Relais_Setting.Leakage_GS:
                    Set_Relais_Leakage_GS();
                    break;

                case Relais_Setting.Charac_MOSFET:
                    Set_Relais_Characteristic_MOSFET();
                    break;

                case Relais_Setting.Charac_BodyDiode:
                    Set_Relais_Charcterictic_BodyDiode();
                    break;

                default:
                    Set_Relais_Open();
                    break;
            }

        }

        private void NumericUpDown_U_GS_ValueChanged(object sender, EventArgs e)
        {
            Set_V_GS(numericUpDown_U_GS.Value);
        }

        private void Button_Update_UDS_Click(object sender, EventArgs e)
        {
            textBox_U_DS.Text = Get_V_DS().ToString() + " V";
        }

        private void Button_Update_IDS_Click(object sender, EventArgs e)
        {
            textBox_I_DS.Text = Get_I_DS().ToString() + " A";
        }

        //********************************************************************************************************************
        //                               FCT (teilweise global um von extern zugreifen zu können)
        //********************************************************************************************************************

        #region Relais

        public void Set_Relais_Open()
        {
            MyRelais = Relais_Setting.No_Relais;
            MyMC.Write_N_Read("SRB" + Slot_Nr.ToString() + "B " + "0");
        }

        public void Set_Relais_BreakDown_DS()
        {
            MyRelais = Relais_Setting.No_Relais;
            MyMC.Write_N_Read("SRB" + Slot_Nr.ToString() + "B " + "1");
        }

        public void Set_Relais_Leakage_DS()
        {
            MyRelais = Relais_Setting.No_Relais;
            MyMC.Write_N_Read("SRB" + Slot_Nr.ToString() + "B " + "1");
        }

        public void Set_Relais_Leakage_GS()
        {
            MyRelais = Relais_Setting.No_Relais;
            MyMC.Write_N_Read("SRL" + Slot_Nr.ToString() + "B " + "1");
        }

        public void Set_Relais_Characteristic_MOSFET()
        {
            MyRelais = Relais_Setting.No_Relais;
            MyMC.Write_N_Read("SRC" + Slot_Nr.ToString() + "B " + "1");
        }

        public void Set_Relais_Charcterictic_BodyDiode()
        {
            MyRelais = Relais_Setting.No_Relais;
            MyMC.Write_N_Read("SRD" + Slot_Nr.ToString() + "B " + "1");
        }

    #endregion Relais


        /// <summary>
        /// Sets the GS Voltage in mV
        /// </summary>
        public void Set_V_GS(decimal voltage_in_V)
        {
            Voltage_Set_GS = voltage_in_V;
            MyMC.Write_N_Read("SVG" + Slot_Nr.ToString() + "B " + (voltage_in_V *1000).ToString("0"));
        }

        /// <summary>
        /// Returngs the setted voltage for V_GS in V
        /// </summary>
        public decimal Get_V_GS()
        {
            string answer = MyMC.Write_N_Read("GVG" + Slot_Nr.ToString() + "B");

            answer = answer.Substring(6, answer.LastIndexOf(' ') - 6);

            return Convert.ToDecimal(answer) / 1000;
        }

        /// <summary>
        /// Returns the measured voltage V_DS in V
        /// </summary>
        /// <returns></returns>
        public decimal Get_V_DS()
        {
            string answer = MyMC.Write_N_Read("GVD" + Slot_Nr.ToString() + "B");

            answer = answer.Substring(6, answer.LastIndexOf(' ') - 6);

            return Convert.ToDecimal(answer) / 1000;
        }

        /// <summary>
        /// Returns the measured current I_DS in A
        /// </summary>
        /// <returns></returns>
        public decimal Get_I_DS()
        {
            string answer = MyMC.Write_N_Read("GCD" + Slot_Nr.ToString() + "B");

            answer = answer.Substring(6, answer.LastIndexOf(' ') - 6);

            return Convert.ToDecimal(answer) / 1000;
        }

    }

    //********************************************************************************************************************
    //                                         Additinal Class for Relais-Setting
    //********************************************************************************************************************

    public class Relais_Setting
    {
        public const int No_Relais = 0;
        public const int Breakdown_DS = 1;
        public const int Leakage_DS = 2;
        public const int Leakage_GS = 3;
        public const int Charac_MOSFET = 4;
        public const int Charac_BodyDiode = 5;
    }
}
