using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Power_Supply_HamegHMP;

namespace Test_Umgebung
{
    public partial class Window_Testumgebung : Form
    {
        PowerSupply_HMP mySupply = new PowerSupply_HMP();

        public Window_Testumgebung()
        {
            InitializeComponent();

            PowerSupply_HMP mySupply = new PowerSupply_HMP(this, 10, 10);
        }
    }
}
