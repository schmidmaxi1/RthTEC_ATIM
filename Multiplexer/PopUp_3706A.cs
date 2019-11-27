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
    public partial class PopUp_3706A : Form
    {

        //********************************************************************************************************************
        //                                    Eigenschaften der Klasse
        //********************************************************************************************************************

        /// <summary>
        /// Reference to Instrument
        /// </summary>
        public Keithley_3706A MyMUX { get; internal set; }

        /// <summary>
        /// Slot Card Informatation
        /// </summary>
        public string[] Slot_Card { get; internal set; } = new string[6];

        /// <summary>
        /// Array with all Buttons
        /// </summary>
        Button[,] Relais_Buttons = new Button[6,30];


        //********************************************************************************************************************
        //                                                Konstruktoren
        //********************************************************************************************************************

        public PopUp_3706A(Keithley_3706A input)
        {
            InitializeComponent();

            //Übernehmen
            MyMUX = input;

            //Get SlotConfiguration
            for (int i = 0; i < 6; i++)
                Slot_Card[i] = "Slot " + (i+1).ToString() + ": " + MyMUX.Get_SlotInfo(i+1);

            //In Textbox schreiben
            textBox1.Text = string.Join(Environment.NewLine, Slot_Card);

            //Buttons Hinzufügen
            Add_Components();
            Update_States();
        }


        //********************************************************************************************************************
        //                                                GUI
        //********************************************************************************************************************

        private void Button_Click(object sender, EventArgs e)
        {
            //Welcher Button hat ausgelöst
            //string Button_Name = nameof(sender).ToString();
            string Button_Name = (sender as Button).Name;

            //Generate Communication String
            int slot_nr = Int32.Parse( Button_Name.Substring(Button_Name.LastIndexOf(".") - 1, 1));
            int relais_nr = Int32.Parse( Button_Name.Substring(Button_Name.LastIndexOf(".") + 1));
            string complete = slot_nr.ToString()  + relais_nr.ToString("000");


            //Zugehöriges Relais schalten
            if(Relais_Buttons[slot_nr - 1, relais_nr - 1].BackColor == Color.Green)
            {
                MyMUX.Open_Relais(new string[] { complete });
                Relais_Buttons[slot_nr - 1, relais_nr - 1].BackColor = Color.Red;
            }                
            else
            {
                MyMUX.Close_Relais(new string[] { complete });
                Relais_Buttons[slot_nr - 1, relais_nr - 1].BackColor = Color.Green;
            }
            
        }

        private void Button_Refresh_Click(object sender, EventArgs e)
        {
            Update_States();
        }

        private void Button_Open_All_Click(object sender, EventArgs e)
        {
            MyMUX.Open_ALL_Relais();
            Update_States();
        }


        //********************************************************************************************************************
        //                                                Locale FCT
        //********************************************************************************************************************

        private void Add_Components()
        {
            for(int i = 0; i < 6; i++)
            {

                if (Slot_Card[i].Contains("3720"))
                {
                    //Neue Button erstellen
                    for (int n = 0; n < 30; n++)
                    {
                        Relais_Buttons[i, n] = new Button();

                        //Button Eigenschaften
                        Relais_Buttons[i, n].Location = new System.Drawing.Point(50 + n*20, 140 + i*20);
                        Relais_Buttons[i, n].Name = "button" + (i+1).ToString() + "." + (n+1).ToString();
                        Relais_Buttons[i, n].Size = new System.Drawing.Size(16, 16);
                        Relais_Buttons[i, n].TabIndex = i*n;
                        Relais_Buttons[i, n].UseVisualStyleBackColor = true;

                        //Button Event
                        Relais_Buttons[i, n].Click += new System.EventHandler(this.Button_Click);

                        //Hinzufügen zum Fenster
                        this.Controls.Add(Relais_Buttons[i, n]);

                    }

                }
                else
                {

                }
                   
            }


        }

        private void Update_States()
        {
            for (int i = 0; i < 6; i++)
            {

                if (Slot_Card[i].Contains("3720"))
                {                 
                    for(int n = 0; n < 30; n++)
                    {
                        Boolean[] state = MyMUX.Get_Relais_State(new string[] { (i + 1).ToString() + "0" + (n+1).ToString("00")});

                        if (state[0])
                            Relais_Buttons[i, n].BackColor = Color.Green;

                        else
                            Relais_Buttons[i, n].BackColor = Color.Red;
                    }

                }
                else
                {

                }

            }

        }


    }
}
