using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Globalization;

namespace XYZ_Table
{
    public partial class Window_XYZ_manuell_drive : Form
    {

        private IXYZ myTable = null;

        public Window_XYZ_manuell_drive(IXYZ callingTable)
        {
            myTable = callingTable;
            InitializeComponent();
            Dropdown_XY_init();
            Dropdown_Z_init();
            Dropdown_Angle_init();
            Fill_Up_Position_List();
        }

        //******************************************************************************************
        //                                   Pfeil Buttons
        //******************************************************************************************

        #region Arrow-Buttons

        private void manuel_X_positiv_Click(object sender, EventArgs e)
        {
            //this.mainForm.bewegung_rel(read_dropdown_XY(), 0, 0);
            myTable.MoveADistance(Read_dropdown_XY(), 0, 0, 0);
        }

        private void manuel_X_negativ_Click(object sender, EventArgs e)
        {
            //this.mainForm.bewegung_rel(-read_dropdown_XY(), 0, 0);
            myTable.MoveADistance(-Read_dropdown_XY(), 0, 0, 0);
        }

        private void manuel_Y_positiv_Click(object sender, EventArgs e)
        {
            //this.mainForm.bewegung_rel(0, read_dropdown_XY(), 0);
            myTable.MoveADistance(0, Read_dropdown_XY(), 0, 0);          
        }

        private void manuel_Y_negativ_Click(object sender, EventArgs e)
        {
            //this.mainForm.bewegung_rel(0, -read_dropdown_XY(), 0);
            myTable.MoveADistance(0, -Read_dropdown_XY(), 0, 0);           
        }

        private void manuel_Z_positiv_Click(object sender, EventArgs e)
        {
            //this.mainForm.bewegung_rel(0, 0, read_dropdown_Z());
            myTable.MoveADistance(0, 0, Read_dropdown_Z(), 0);
        }

        private void manuel_Z_negativ_Click(object sender, EventArgs e)
        {
            //this.mainForm.bewegung_rel(0, 0, -read_dropdown_Z());
            myTable.MoveADistance(0, 0, -Read_dropdown_Z(), 0);
        }

        private void manuel_Z_max_Click(object sender, EventArgs e)
        {
            //this.mainForm.bewegung_abs(Convert.ToDecimal(this.mainForm.akt_Position_x.Text), 
            //                           Convert.ToDecimal(this.mainForm.akt_Position_y.Text), 
            //                           0);
            myTable.Move2Position(myTable.Akt_x_Koordinate, myTable.Akt_y_Koordinate, 0 , 0);
        }

        private void manuell_Z_min_Click(object sender, EventArgs e)
        {
            //this.mainForm.bewegung_abs(Convert.ToDecimal(this.mainForm.akt_Position_x.Text),
            //                           Convert.ToDecimal(this.mainForm.akt_Position_y.Text),
            //                           -42);
            //myTable.Move2Position(myTable.Akt_x_Koordinate, myTable.Akt_y_Koordinate, myTable.Anfahrt_z , 0);
            MessageBox.Show("Nicht realisiert");
        }

        private void SimpleButton_inverse_Clockwise_Click(object sender, EventArgs e)
        {
            myTable.MoveADistance(0, 0, 0, Convert.ToDecimal(comboBox_Angle.Text));
        }

        private void SimpleButton_Clockwise_Click(object sender, EventArgs e)
        {
            myTable.MoveADistance(0, 0, 0, - Convert.ToDecimal(comboBox_Angle.Text));
        }

        #endregion Arrow-Buttons

        //******************************************************************************************
        //                                   Definierte Punkte
        //******************************************************************************************

        #region PreDefined_Positions

        private string path_Init_File = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\0_Initialisation_Files\\Predefined_Positions.ini";
        //SammelListe
        private List<XYZA_Position> defined_Positions = new List<XYZA_Position>();

        //File Lesen
        private void Read_IniFile(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                //Jede Zeile nacheinander öffenen
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Wenn Zeile mit '#' anfängt ist es ein Kommentar                    
                    if (line.StartsWith("#") | line == "")
                    {
                        //-> Zeile überspringen
                    }
                    else
                    {
                        //Positionen finden
                        int index_DP = line.IndexOf(":");
                        int index_x = line.IndexOf("x=");
                        int index_y = line.IndexOf("y=");
                        int index_z = line.IndexOf("z=");
                        int index_a = line.IndexOf("a=");

                        defined_Positions.Add(new XYZA_Position()
                        {
                            Name = line.Substring(0, index_DP),
                            X_Position = Convert.ToDecimal(line.Substring(index_x + 2, index_y - index_x - 2), new CultureInfo("en-US")),
                            Y_Position = Convert.ToDecimal(line.Substring(index_y + 2, index_z - index_y - 2), new CultureInfo("en-US")),
                            Z_Position = Convert.ToDecimal(line.Substring(index_z + 2, index_a - index_z - 2), new CultureInfo("en-US")),
                            Angle = Convert.ToDecimal(line.Substring(index_a + 2), new CultureInfo("en-US")),
                    }
                        );
                    }
                }
            }
        }

        //Liste füllen
        private void Fill_Up_Position_List()
        {
            //Liste füllen
            Read_IniFile(path_Init_File);

            //In ComboBox
            comboBox_Predifined.Items.Clear();
            foreach(XYZA_Position position in defined_Positions)           
                comboBox_Predifined.Items.Add(position.Name);

            //Erstes auswählen
            comboBox_Predifined.SelectedIndex = 0;            
        }
       
        //Combobox
        private void ComboBox_Predifined_SelectedIndexChanged(object sender, EventArgs e)
        {
            XYZA_Position selected = defined_Positions.Find(xyz => xyz.Name == comboBox_Predifined.Text);

            numericUpDown_X_Position.Value = selected.X_Position;
            numericUpDown_Y_Position.Value = selected.Y_Position;
            numericUpDown_Angle.Value = selected.Angle;
        }

        //Verfahren
        private void Button_Drive2Position_Click(object sender, EventArgs e)
        {
            myTable.Position_aktualisiern();
            myTable.Move2Position(myTable.Akt_x_Koordinate, myTable.Akt_y_Koordinate, 0, myTable.Akt_Winkel);
            myTable.Move2Position(numericUpDown_X_Position.Value, numericUpDown_Y_Position.Value, 0, numericUpDown_Angle.Value);
        }

        #endregion PreDefined_Positions

        //******************************************************************************************
        //                                   Drop-Down Funktionen
        //******************************************************************************************

        #region DropDown_Distance

        private void Dropdown_XY_init()
        {
            String[] auswahl = { "0.125", "0.5", "1", "2", "5", "10" };
            comboBox_XY.Items.AddRange(auswahl);
            comboBox_XY.SelectedIndex = 5;
        }

        private void Dropdown_Z_init()
        {
            String[] auswahl = { "0.100", "1", "2", "5", "10", "40" };
            comboBox_Z.Items.AddRange(auswahl);
            comboBox_Z.SelectedIndex = 5;
        }

        private void Dropdown_Angle_init()
        {
            String[] auswahl = { "45", "90"};
            comboBox_Angle.Items.AddRange(auswahl);
            comboBox_Angle.SelectedIndex = 0;
        }

        private decimal Read_dropdown_XY()
        {
            //Auslesen der aktuellen Combo-Box "Schrittweite" und Rückgabe des eingestellten Werts
            Object fahrweg_combo = comboBox_XY.SelectedItem; // Combo-Box abrufen
            return Convert.ToDecimal(fahrweg_combo);

        }

        private decimal Read_dropdown_Z()
        {
            //Auslesen der aktuellen Combo-Box "Schrittweite" und Rückgabe des eingestellten Werts
            Object fahrweg_combo = comboBox_Z.SelectedItem; // Combo-Box abrufen
            return Convert.ToDecimal(fahrweg_combo);
        }

        #endregion DropDown_Distance


    }
}
