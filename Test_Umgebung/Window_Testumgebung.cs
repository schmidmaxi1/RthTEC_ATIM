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
using Read_Coordinates;

namespace Test_Umgebung
{
    public partial class Window_Testumgebung : Form
    {
        PowerSupply_HMP mySupply = new PowerSupply_HMP();
        ReadBox_Movement myMovement;

        public Window_Testumgebung()
        {
            InitializeComponent();

            PowerSupply_HMP mySupply = new PowerSupply_HMP(this, 10, 10);

            myMovement = new ReadBox_Movement(this, 10, 100);
        }
    }
}
