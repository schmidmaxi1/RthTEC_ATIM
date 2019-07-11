using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rth_Rack_Window
{
    public partial class Card_Empty : UserControl, I_RthTEC_Card
    {
        //********************************************************************************************************************
        //                                                  Variablen
        //********************************************************************************************************************

        /// <summary>
        /// Flag, if Card is active in pulsing
        /// </summary>
        public Boolean IsActiv { get; set; }

        /// <summary>
        /// Number of the Slot in which the card is placed
        /// </summary>
        public int Slot_Nr { get; set; }

        //********************************************************************************************************************
        //                                                 Konstruktor
        //********************************************************************************************************************

        public Card_Empty(I_RthTEC calling, int slotNr)
        {
            //Init LED Source UserControll
            InitializeComponent();

            //MC & Slot zuweisen
            //MyMC = calling;
            //Slot_Nr = slotNr;

        }


        public void Add_to_DetailedForm(Window_RthTEC_Rack myDetailed, int x, int y)
        {
            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "Empty";
            this.Size = new System.Drawing.Size(116, 680);
            this.TabIndex = 33;
            
            //Hinzufügen
            myDetailed.Controls.Add(this);

            //Nach vorne bringen
            this.BringToFront();
        }

    }
}
