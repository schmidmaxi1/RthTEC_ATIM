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

namespace Test_Umgebung2
{
    public partial class Form1 : Form
    {
        PowerSupply_HMP mySupply;
        I_TEC_Controller myTEC;

        ReadBox_Movement myMovement;

        public Form1()
        {
            InitializeComponent();

            InitializeComponent();

            mySupply = new PowerSupply_HMP(this, 10, 10);

            myTEC = new Meerstetter_4fach(this, 10, 100);


            myMovement = new ReadBox_Movement(this, 10, 100);
        }
    }
}
