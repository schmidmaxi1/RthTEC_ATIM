using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAQ_Unit
{
    public partial class Detailed_Window : Form
    {

        I_DAQ calling;

        public Detailed_Window(I_DAQ input)
        {
            InitializeComponent();

            //Calling übernehmen
            calling = input;

            //Comboboxen füllen
            comboBox_Range.Items.AddRange(input.RangeList);
            comboBox_Freq.Items.AddRange(input.FrequencyList);

            //Passendes auswählen
            if (calling.Frequency >= 1000000)
                comboBox_Freq.Text = (calling.Frequency / 1000000).ToString() + "MHz";
            else if (calling.Frequency >= 1000)
                comboBox_Freq.Text = (calling.Frequency / 1000).ToString() + "kHz";
            else
                comboBox_Freq.Text = (calling.Frequency).ToString() + "Hz";

            if (calling.Range >= 1000)
                comboBox_Range.Text = "+/- " + (calling.Range / 1000).ToString() + "V";
            else
                comboBox_Range.Text = "+/- " + (calling.Range).ToString() + "V";
        }

        private void ComboBox_Range_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = comboBox_Range.Text;
            long factor;
            long number;

            //Einheit
            if (temp.EndsWith("mV"))
                factor = 1;
            else
                factor = 1000;

            //Nummer
            temp = temp.Trim(new char[] { 'm', 'V', '+', '/', '-', ' ' });
            long.TryParse(temp, out number);

            //Nummer
            calling.Range = number * factor;
        }

        private void ComboBox_Freq_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = comboBox_Freq.Text;
            long factor;
            long number;

            //Einheit
            if (temp.EndsWith("kHz"))
                factor = 1000;
            else if (temp.EndsWith("MHz"))
                factor = 1000000;
            else
                factor = 1;

            //Nummer
            temp = temp.TrimEnd(new char[] { 'k', 'M', 'H', 'z', ' ' });
            long.TryParse(temp, out number);

            //Nummer
            calling.Frequency = number * factor;
        }
    }
}
