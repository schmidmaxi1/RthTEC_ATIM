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
using System.Globalization;
using System.Threading;

namespace Read_Coordinates
{
    public partial class ReadBox_Movement : UserControl
    {
        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        /// <summary>
        /// Local Mofment Infos form File
        /// </summary>
        public Movement_Infos MyMovementInfo { get; internal set; } = new Movement_Infos();


        private string MyPath { get;  set; }


        //**************************************************************************************************
        //                                    Hinterlegte Bilder
        //**************************************************************************************************

        static readonly Image picture_Check = Properties.Resources.Apply_16x16;
        static readonly Image picture_False = Properties.Resources.Delete_16x16;
        static readonly Image picture_Warning = Properties.Resources.Warning_16x16;

        //**************************************************************************************************
        //                                         Konstruktor
        //**************************************************************************************************

        public ReadBox_Movement()
        {
            //Punkt statt Komma
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-Us");

            InitializeComponent();
            pictureBox_GUI.Image = picture_False;
        }

        public ReadBox_Movement(Form callingForm, int x, int y)
        {
            InitializeComponent();
            pictureBox_GUI.Image = picture_False;

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "ReadBox_Movement";
            this.Size = new System.Drawing.Size(510, 60);
            this.TabIndex = 31;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }

        //**************************************************************************************************
        //                                           GUI
        //**************************************************************************************************


        /// <summary>
        /// Opens an File Dialog to Change the Text in the TextBox
        /// </summary>
        private void TextBox_Gerber_DoubleClick(object sender, EventArgs e)
        {
            //Dialog-Fenster öffnen
            OpenFileDialog myOpenFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Title = "Select driving file",
                InitialDirectory = "C:\\Users\\schmidm\\Desktop\\ATIM_GIT\\2_DrivingFiles_neu",
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

        /// <summary>
        /// If The Text is change, the new path is "recalculated"
        /// </summary>
        private void TextBox_Gerber_TextChanged(object sender, EventArgs e)
        {
            //Pfad übernehmen
            MyPath = textBox_Gerber.Text;

            MyMovementInfo = new Movement_Infos(MyPath);

            //Wenn Korrekt dann Hacken in Bild, sonst Ausrufenzeichen
            if (MyMovementInfo.IsCorect)
                pictureBox_GUI.Image = picture_Check;
            else
                pictureBox_GUI.Image = picture_Warning;

            //Selected LEDs updaten
            Update_DUT_Count();

        }

        /// <summary>
        /// Opens a Window, to select specific DUTs
        /// </summary>
        private void Button_Select_Click(object sender, EventArgs e)
        {
            //Generate new Form to Select LEDs
            Form_Select_DUTs myForm = new Form_Select_DUTs(MyMovementInfo);

            //Als Dialog öffnen
            myForm.ShowDialog();

            //Selected LEDs updaten
            Update_DUT_Count();
        }

        /// <summary>
        /// Update the Info in the textBox
        /// </summary>
        private void Update_DUT_Count()
        {
            //Local counter
            int counter = 0;

            try
            {            
                //Increment for each selected DUT
                for (int i = 0; i < MyMovementInfo.MyMeasurment_Point.Length; i++)
                    if (MyMovementInfo.MyMeasurment_Point[i].IsSelected == true)
                        counter++;

                //Print in Textbox
                textBox_Seleted.Text = counter.ToString() + " / " + MyMovementInfo.MyMeasurment_Point.Length.ToString();
            }
            catch(Exception)
            {
                textBox_Seleted.Text = "";
            }
        }
    }
}
