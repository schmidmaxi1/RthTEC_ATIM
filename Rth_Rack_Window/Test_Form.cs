using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace RthTEC_Rack
{
    public partial class Test_Form : Form
    {
        public Test_Form()
        {
            //Punkt statt Komma
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-Us");

            InitializeComponent();

            I_RthTEC myRthTEC = new RthTEC_V1(this, 0, 0);
        }
    }
}
