using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Read_Coordinates
{
    public partial class FileSettings_Box : UserControl
    {
        public FileSettings_Box()
        {
            InitializeComponent();
        }

        public FileSettings_Box(Form callingForm, int x, int y)
        {
            InitializeComponent();

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "FillSetting_Box";
            this.Size = new System.Drawing.Size(520, 105);
            this.TabIndex = 32;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }
    }
}
