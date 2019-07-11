using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rth_Rack_Window
{
    public partial class Test_Form : Form
    {
        public Test_Form()
        {
            InitializeComponent();

            I_RthTEC myRthTEC = new RthTEC_V1(this, 0, 0);
        }
    }
}
