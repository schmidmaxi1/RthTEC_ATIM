using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Communication_Settings
{
    public partial class NI_CommunicationDevice : UserControl
    {
        public NI_CommunicationDevice(string name)
        {
            InitializeComponent();

            groupBox1.Text = name;
        }
    }
}
