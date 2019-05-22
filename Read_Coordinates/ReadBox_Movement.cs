using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Read_Coordinates
{
    public partial class ReadBox_Movement : UserControl
    {
        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        public Boolean IsCorect { get; internal set; } = false;
        public decimal[,] Movements_XYA { get; internal set; }
        public string[] DUT_Name { get; internal set; }
        public string MyPath { get; internal set; }

        //**************************************************************************************************
        //                                    Hinterlegte Bilder
        //**************************************************************************************************

        Image picture_Check = Properties.Resources.Apply_16x16;
        Image picture_False = Properties.Resources.Delete_16x16;
        Image picture_Warning = Properties.Resources.Warning_16x16;

        //**************************************************************************************************
        //                                         Konstruktor
        //**************************************************************************************************

        public ReadBox_Movement()
        {
            InitializeComponent();
            pictureBox2.Image = picture_False;
        }

        public ReadBox_Movement(Form callingForm, int x, int y)
        {
            InitializeComponent();
            pictureBox2.Image = picture_False;

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "ReadBox_Movement";
            this.Size = new System.Drawing.Size(510, 40);
            this.TabIndex = 31;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }

        //**************************************************************************************************
        //                                           GUI
        //**************************************************************************************************

        private void TextBox_Gerber_TextChanged(object sender, EventArgs e)
        {
            //Pfad übernehmen
            MyPath = textBox_Gerber.Text;
            //Movement Informations berechnen
            Calculate_Movement_from_ASCII_File();
        }

        private void TextBox_Gerber_DoubleClick(object sender, EventArgs e)
        {
            //Dialog-Fenster öffnen
            OpenFileDialog myOpenFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Title = "Select driving file",
                InitialDirectory = "C:\\Users\\schmidm\\Desktop\\ATIM_Software\\2_DrivingFiles",
                Filter = "Driving files(*.txt)|*.txt|All files(*.*)|*.*",
                FilterIndex = 0,
                RestoreDirectory = false
            };

            //Prüfen obs funktioniert hat
            if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_Gerber.Text = myOpenFileDialog.FileName;
            }
        }

        //**************************************************************************************************
        //                                      Lokale Funktion
        //**************************************************************************************************

        #region Local_Functions

        private void Calculate_Movement_from_ASCII_File()
        {
            try
            {
                //Read all single Lines
                string[] datei_Inhalt = File.ReadAllLines(MyPath);

                //Kommentar-Zeilen (Beginnend mit # löschen)
                //oder leere zeile enferen
                for (int i = 0; i < datei_Inhalt.Length; i++)
                {
                    //Leerzeichen entfernen
                    datei_Inhalt[i].Trim();
                    //Prüfen
                    if (datei_Inhalt[i].StartsWith("#") | String.IsNullOrEmpty(datei_Inhalt[i]))
                    {
                        datei_Inhalt = datei_Inhalt.Where(w => w != datei_Inhalt[i]).ToArray();
                        i--;
                    }
                }

                //Feld definieren
                Movements_XYA = new decimal[datei_Inhalt.Length, 3];
                DUT_Name = new string[datei_Inhalt.Length];

                //Daten herauslösen
                for (int i = 0; i < datei_Inhalt.Length; i++)
                {
                    int index_first_komma = datei_Inhalt[i].IndexOf(';', 0);
                    int index_second_komma = datei_Inhalt[i].IndexOf(';', index_first_komma + 1);
                    int index_third_komma = datei_Inhalt[i].IndexOf(';', index_second_komma + 1);

                    Movements_XYA[i, 0] = Convert.ToDecimal(datei_Inhalt[i].Substring(0, index_first_komma));
                    Movements_XYA[i, 1] = Convert.ToDecimal(datei_Inhalt[i].Substring(index_first_komma + 1, index_second_komma - index_first_komma - 1));
                    Movements_XYA[i, 2] = Convert.ToDecimal(datei_Inhalt[i].Substring(index_second_komma + 1, index_third_komma - index_second_komma - 1));

                    if (!String.IsNullOrEmpty((datei_Inhalt[i].Substring(index_third_komma + 1))))
                        DUT_Name[i] = datei_Inhalt[i].Substring(index_third_komma + 1);
                    else
                        DUT_Name[i] = (i + 1).ToString();

                }

                //Wenn keine Exeption aufgetretten dann Correct
                IsCorect = true;
                pictureBox2.Image = picture_Check;
            }
            catch (Exception)
            {
                IsCorect = false;

                pictureBox2.Image = picture_Warning;
            }

        }

        #endregion Local_Functions
    }
}
