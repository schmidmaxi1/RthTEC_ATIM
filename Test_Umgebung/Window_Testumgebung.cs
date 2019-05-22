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

using TEC_Controller;

namespace Test_Umgebung
{
    public partial class Window_Testumgebung : Form
    {
        PowerSupply_HMP mySupply;
        UserControl myTEC;


        public Window_Testumgebung()
        {
            InitializeComponent();

            mySupply = new PowerSupply_HMP(this, 10, 10);

            myTEC = new Meerstetter_4fach(this, 10, 100);
            
        }
    }
}
