using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATIM_GUI._1_Communication_Settings
{
    public partial class EthernetCommunicationDevice : UserControl
    {
        public EthernetCommunicationDevice(string name)
        {
            InitializeComponent();

            groupBox1.Text = name;
        }
    }
}
