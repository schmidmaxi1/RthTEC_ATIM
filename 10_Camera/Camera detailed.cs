using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using AForge.Video;
using AForge.Video.DirectShow;

namespace DXApplication3._10_Camera
{
    public partial class Camera_detailed : Form
    {

        Camera_Gerber callingCamera;

        UInt16 auswahl = 0;

        public Camera_detailed(Camera_Gerber calling)
        {
            InitializeComponent();
            callingCamera = calling;

            trackBar_RED.Value = callingCamera.Filter_RED;
            trackBar_GREEN.Value = callingCamera.Filter_GREEN;
            trackBar_BLUE.Value = callingCamera.Filter_BLUE;
            trackBar_Radius.Value = callingCamera.Filter_Radius;
            trackBar_Threshold.Value = callingCamera.Threshold;

            label_red.Text = callingCamera.Filter_RED.ToString();
            label_green.Text = callingCamera.Filter_GREEN.ToString();
            label_blue.Text = callingCamera.Filter_BLUE.ToString();
            label_radius.Text = callingCamera.Filter_Radius.ToString();
            label_threshold.Text = callingCamera.Threshold.ToString();
        }


        //********************************************************************************************************************
        //                                                Kamera - Funktionen
        //********************************************************************************************************************

        #region TrackBars

        private void TrackBar_RED_Scroll(object sender, EventArgs e)
        {
            callingCamera.Filter_RED = trackBar_RED.Value;
            label_red.Text = callingCamera.Filter_RED.ToString();
            callingCamera.myRGB_Filter.CenterColor = 
                new RGB((byte)callingCamera.Filter_RED, (byte)callingCamera.Filter_GREEN, (byte)callingCamera.Filter_BLUE);
        }

        private void TrackBar_GREEN_Scroll(object sender, EventArgs e)
        {
            callingCamera.Filter_GREEN = trackBar_GREEN.Value;
            label_green.Text = callingCamera.Filter_GREEN.ToString();
            callingCamera.myRGB_Filter.CenterColor =
                new RGB((byte)callingCamera.Filter_RED, (byte)callingCamera.Filter_GREEN, (byte)callingCamera.Filter_BLUE);
        }

        private void TrackBar_BLUE_Scroll(object sender, EventArgs e)
        {
            callingCamera.Filter_BLUE = trackBar_BLUE.Value;
            label_blue.Text = callingCamera.Filter_BLUE.ToString();
            callingCamera.myRGB_Filter.CenterColor =
                new RGB((byte)callingCamera.Filter_RED, (byte)callingCamera.Filter_GREEN, (byte)callingCamera.Filter_BLUE);
        }

        private void TrackBar_Radius_Scroll(object sender, EventArgs e)
        {
            callingCamera.Filter_Radius = trackBar_Radius.Value;
            label_radius.Text = callingCamera.Filter_Radius.ToString();
            callingCamera.myRGB_Filter.Radius = (short)callingCamera.Filter_Radius;
        }

        private void TrackBar_Threshold_Scroll(object sender, EventArgs e)
        {
            callingCamera.Threshold = trackBar_Threshold.Value;
            label_threshold.Text = callingCamera.Threshold.ToString();
            callingCamera.myThreshold = new Threshold(callingCamera.Threshold);
        }

        #endregion TrackBars

        //********************************************************************************************************************
        //                                             Bild auswahl
        //********************************************************************************************************************

        #region Bild_Auswahl

        private void PictureBox_Original_Click(object sender, EventArgs e)
        {
            auswahl = 0;
        }

        private void PictureBox_RGB_Filter_Click(object sender, EventArgs e)
        {
            auswahl = 1;
        }

        private void PictureBox_Greyscale_Click(object sender, EventArgs e)
        {
            auswahl = 2;
        }

        private void PictureBox_Edge_Click(object sender, EventArgs e)
        {
            auswahl = 3;
        }

        private void PictureBox_Threshold_Click(object sender, EventArgs e)
        {
            auswahl = 4;
        }

        private void PictureBox_Marked_Click(object sender, EventArgs e)
        {
            auswahl = 5;
        }

        #endregion Bildauswahl

        //********************************************************************************************************************
        //                                             Bilder übernehmen
        //********************************************************************************************************************

        #region Bilder_Übernehmern

        public void Set_PictureOriginal(Bitmap input)
        {
            pictureBox_Origninal.Image = new Bitmap(input, new Size(200, 160));

            if(auswahl == 0)
                pictureBox_Main.Image = new Bitmap(input, new Size(600, 480));
        }

        public void Set_PictureColorFilter(Bitmap input)
        {
            pictureBox_ColorFilter.Image = new Bitmap(input, new Size(200, 160));

            if (auswahl == 1)
                pictureBox_Main.Image = new Bitmap(input, new Size(600, 480));
        }

        public void Set_PictureGreyScale(Bitmap input)
        {
            pictureBox_GreyScale.Image = new Bitmap(input, new Size(200, 160));

            if (auswahl == 2)
                pictureBox_Main.Image = new Bitmap(input, new Size(600, 480));
        }

        public void Set_PictureEdge(Bitmap input)
        {
            pictureBox_Edge.Image = new Bitmap(input, new Size(200, 160));

            if (auswahl == 3)
                pictureBox_Main.Image = new Bitmap(input, new Size(600, 480));
        }

        public void Set_PictureThreshold(Bitmap input)
        {
            pictureBox_Threshold.Image = new Bitmap(input, new Size(200, 160));

            if (auswahl == 4)
                pictureBox_Main.Image = new Bitmap(input, new Size(600, 480));
        }

        public void Set_PictureObjects(Bitmap input)
        {
            pictureBox_Marked.Image = new Bitmap(input, new Size(200, 160));

            if (auswahl == 5)
                pictureBox_Main.Image = new Bitmap(input, new Size(600, 480));
        }



        #endregion Bilder_Übernehmen

        //********************************************************************************************************************
        //                                             Bilder übernehmen
        //********************************************************************************************************************

        public void ShowObjectsInGUI(List<Detected_Object> input)
        {
            string output = "";

            foreach(var _object in input)
            {
                output += _object.ToString();
            }
            if (textBox_Objects.InvokeRequired)
            {
                textBox_Objects.Invoke((MethodInvoker)delegate { textBox_Objects.Text = output; });
                return;
            }
            else
            {
                textBox_Objects.Text = output;
            }

        }
    }
}
