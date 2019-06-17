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
    public partial class ReadBox_FileFolder : UserControl
    {
        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        public string MyPath { get; internal set; }
        public string MyFileName { get; internal set; }
        public Boolean IsCorrect { get; internal set; }

        //**************************************************************************************************
        //                                    Hinterlegte Bilder
        //**************************************************************************************************

        Image picture_Check = Properties.Resources.Apply_16x16;
        Image picture_False = Properties.Resources.Delete_16x16;
        Image picture_Warning = Properties.Resources.Warning_16x16;

        //**************************************************************************************************
        //                                         Konstruktor
        //**************************************************************************************************

        public ReadBox_FileFolder()
        {
            InitializeComponent();
            pictureBox_Path.Image = picture_False;
            pictureBox_FineName.Image = picture_False;
        }

        public ReadBox_FileFolder(Form callingForm, int x, int y)
        {
            InitializeComponent();
            pictureBox_Path.Image = picture_False;
            pictureBox_FineName.Image = picture_False;

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "ReadBox_FileFolder";
            this.Size = new System.Drawing.Size(510, 40);
            this.TabIndex = 32;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }

        //**************************************************************************************************
        //                                          GUI
        //**************************************************************************************************

        private void TextBox_Path_TextChanged(object sender, EventArgs e)
        {
            MyPath = textBox_Path.Text;

            //Prüfen ob Ordner ok ist
            if (Directory.Exists(textBox_Path.Text))
                pictureBox_Path.Image = picture_Check;
            else
                pictureBox_Path.Image = picture_False;
        }

        private void TextBox_FileName_TextChanged(object sender, EventArgs e)
        {
            MyFileName = textBox_FileName.Text;

            //Prüfen ob %L[n] in File Name vorhanden
            int index_Prozent_R = textBox_FileName.Text.IndexOf("%R");

            if (index_Prozent_R < 0)
            {
                pictureBox_FineName.Image = picture_False;
                return;
            }
            else
                pictureBox_FineName.Image = picture_Check;

        }

        private void TextBox_Path_DoubleClick(object sender, EventArgs e)
        {
            //Dialog-Fenster öffnen
            FolderBrowserDialog myFolderBrowserDialog = new FolderBrowserDialog();

            //Prüfen obs funktioniert hat
            if (myFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_Path.Text = myFolderBrowserDialog.SelectedPath;
            }
        }

        private void TextBox_FileName_DoubleClick(object sender, EventArgs e)
        {
            //Dialog-Fenster öffnen
            OpenFileDialog myOpenFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Title = "Select file (will be overwritten)",
                InitialDirectory = textBox_Path.Text,
                RestoreDirectory = false
            };

            //Prüfen obs funktioniert hat
            if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Path entfernen
                textBox_FileName.Text = Path.GetFileName(myOpenFileDialog.FileName);
                //File type entfernen
                textBox_FileName.Text = textBox_FileName.Text.Substring(0, textBox_FileName.Text.LastIndexOf("."));
            }
        }

        //**************************************************************************************************
        //                                      Lokale Funktion
        //**************************************************************************************************

        private void CheckIfCorrect()
        {
            if (Directory.Exists(textBox_Path.Text) & textBox_FileName.Text.IndexOf("%R") > 0)
                IsCorrect = true;
            else
                IsCorrect = false;
        }


    }
}
