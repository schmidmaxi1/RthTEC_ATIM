using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAQ_Units
{
    public partial class TestForm : Form
    {
        NI_USB6281 myNI;
        Spectrum30MHz mySpec;
        public TestForm()
        {
            InitializeComponent();

            myNI = new NI_USB6281(this, 10, 10);
            mySpec = new Spectrum30MHz(this, 10, 100);
        }
    }
}
