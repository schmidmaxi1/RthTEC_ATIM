using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiplexer
{
    public partial class Test_Form : Form
    {
        public Test_Form()
        {
            InitializeComponent();

            var myMUX = new Keithley_3706A(this, 10, 10);

            //Temporär 
            myMUX.textBox_ADR.Text = "USB0::0x05E6::0x3706::04076987::INSTR";
        }
    }
}
