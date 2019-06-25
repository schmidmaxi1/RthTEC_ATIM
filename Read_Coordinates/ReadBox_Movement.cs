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
        public Movement_Infos MyMovementInfo { get; internal set; }


        private string MyPath { get;  set; }

        //Ab hier kann weg

        /*
        public Boolean IsCorect { get; internal set; } = false;

        public decimal[,] Movements_XYA { get; internal set; }
        public string[] DUT_Name { get; internal set; }

        public Measurement_Point_XYZA[] MyMeasurment_Point { get; internal set; }

        public decimal BRD_Dimension_X { get; internal set; } = Decimal.MinValue;
        public decimal BRD_Dimension_Y { get; internal set; } = Decimal.MinValue;

        public PointF[] Feducials { get; internal set; } = new PointF[3];
        public PointF QR_Code { get; internal set; }

        public decimal TouchDown_Hight { get; internal set; } = Decimal.MinValue;
        public decimal Driving_Hight { get; internal set; } = Decimal.MinValue;
        */


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

            MyMovementInfo = new Movement_Infos(MyPath);

            //Wenn Korrekt dann Hacken in Bild, sonst Ausrufenzeichen
            if (MyMovementInfo.IsCorect)
                pictureBox_GUI.Image = picture_Check;
            else
                pictureBox_GUI.Image = picture_Warning;

        }

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


    }
}
