using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiplexer
{
    public partial class PopUp_3706A_UserCommunication : Form
    {

        //********************************************************************************************************************
        //                                      Eigenschaften der Klasse
        //********************************************************************************************************************

        /// <summary>
        /// Reference to Mux Instance
        /// </summary>
        public Keithley_3706A MyMux { get; internal set; }

        //********************************************************************************************************************
        //                                            Konstruktoren
        //********************************************************************************************************************

        public PopUp_3706A_UserCommunication(Keithley_3706A input)
        {
            InitializeComponent();

            MyMux = input;
        }

        //********************************************************************************************************************
        //                                                 GUI
        //********************************************************************************************************************

        private void Button_Send_Click(object sender, EventArgs e)
        {
            Send_Message(textBox_Write.Text);
        }

        //********************************************************************************************************************
        //                                                Local FCT
        //********************************************************************************************************************

        private void Send_Message(string message)
        {
            //Mit empfangen
            if (checkBox_Read.Checked)
            {
                try
                {
                    textBox_Read.Text = MyMux.Write_n_Read(message);
                }
                catch (Exception)
                {
                    textBox_Read.Text = "TIMEOUT (invalid message)";
                }

            }
            else
            {
                MyMux.Write(message);
                textBox_Read.Text = "";
            }

        }
    }
}
