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

using TEC_Controller;

namespace Test_Umgebung
{
    public partial class Window_Testumgebung : Form
    {
<<<<<<< HEAD
        PowerSupply_HMP mySupply;
        I_TEC_Controller myTEC;
        

=======
        PowerSupply_HMP mySupply = new PowerSupply_HMP();
        ReadBox_Movement myMovement;
>>>>>>> 635ae0261fbcdedc98392f70a69617626c53d66d

        public Window_Testumgebung()
        {
            InitializeComponent();

<<<<<<< HEAD
            mySupply = new PowerSupply_HMP(this, 10, 10);

            myTEC = new Meerstetter_4fach(this, 10, 100);
            
=======
            PowerSupply_HMP mySupply = new PowerSupply_HMP(this, 10, 10);

            myMovement = new ReadBox_Movement(this, 10, 100);
>>>>>>> 635ae0261fbcdedc98392f70a69617626c53d66d
        }
    }
}
