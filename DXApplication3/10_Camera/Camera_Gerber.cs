using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
    public partial class Camera_Gerber : UserControl
    {
        //********************************************************************************************************************
        //                                                Variablen
        //********************************************************************************************************************

        public bool IsConnected { get; internal set; } = false;

        public List<Detected_Object> MyObjects { get; internal set; } = new List<Detected_Object>();

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;
        private Bitmap aktFrame;


        public int Filter_RED { get; internal set; } = 255;
        public int Filter_GREEN { get; internal set; } = 144;
        public int Filter_BLUE { get; internal set; } = 200;

        public int Filter_Radius { get; internal set; } = 100;

        public EuclideanColorFiltering myRGB_Filter { get; internal set; } = new EuclideanColorFiltering();

        Grayscale myGrayScale = new Grayscale(0.2125, 0.7154, 0.0721);

        GaussianBlur blurfilter = new GaussianBlur();

        SobelEdgeDetector myEdge_Filter = new SobelEdgeDetector();

        public int Threshold { get; internal set; } = 50;

        public Threshold myThreshold {get; internal set;}

        //Fenster
        private Camera_detailed myDetaildWindow;
        private bool windowOpen = false;

        //********************************************************************************************************************
        //                                                Konstruktor
        //********************************************************************************************************************

        public Camera_Gerber()
        {
            InitializeComponent();

            GetCamList();

            //Filter definieren
            myRGB_Filter.CenterColor = new RGB((byte)Filter_RED, (byte)Filter_GREEN, (byte)Filter_BLUE);
            myRGB_Filter.Radius = (short)Filter_Radius;

            myThreshold = new Threshold(Threshold);
        }

        //Change Enable Status form MainForm
        public void Change_Enabled(Boolean input)
        {
            groupBox1.Invoke((MethodInvoker)delegate
            {
                groupBox1.Enabled = input;
            });
        }

        //********************************************************************************************************************
        //                                                Button-Events
        //********************************************************************************************************************

        #region Events

        private void OpenClose_Click(object sender, EventArgs e)
        {
            //Öffnen
            if (!IsConnected)
            {
                //Kamera öffnen
                videoSource = new VideoCaptureDevice(videoDevices[Camera_select.SelectedIndex].MonikerString);

                //Eventhandler für neuen Frame (Funktion wird ausgeführt)
                videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);

                //Eigene Funktion k.P was hier gemacht wird???????????????
                //CloseVideoSource();

                //Auflösung einstellen????????????????????????????????????
                //videoSource.VideoResolution = videoSource.VideoCapabilities[2];

                //Kamera eingenschaften einstellen ???????????????????????
                //videoSource.SetCameraProperty(CameraControlProperty.Exposure, 60, CameraControlFlags.Manual);

                videoSource.Start();

                

                //timer1.Enabled = true;

                IsConnected = true;
                OpenClose.Text = "Close";

            }
            //Schließen
            else
            {
                CloseVideoSource();
                IsConnected = false;
                OpenClose.Text = "Open";
            }

        }

        private void Button_Detailed_Click(object sender, EventArgs e)
        {
            myDetaildWindow = new Camera_detailed(this);
            myDetaildWindow.Show();
            windowOpen = true;
        }

        #endregion Events

        //********************************************************************************************************************
        //                                                Hilfs-Funktionen 
        //********************************************************************************************************************

        #region Hilfsfunktionen

        private void GetCamList()
        {
            //Aktuelle Liste leeren
            Camera_select.Items.Clear();

            //Kameras zuschen
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //In KomboBox eintragen
            foreach (FilterInfo device in videoDevices)
            {
                Camera_select.Items.Add(device.Name);
            }
            
            //Eins auswählen
            if(Camera_select.Items.Count > 0)
                Camera_select.SelectedIndex = 0; //make dafault to first cam
            
        }

        #endregion Hilfsfunktionen

        //********************************************************************************************************************
        //                                                Kamera - Funktionen
        //********************************************************************************************************************

        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
           

            aktFrame = (Bitmap)eventArgs.Frame.Clone();

            aktFrame = Mark_all_Shapes(aktFrame);


            pictureBox1.Image = new Bitmap(aktFrame, new Size(240, 192)); 



            /* Alt
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            if (radioButton1.Checked == true)
            {

                pictureBox1.Image = img;
                //Frame = img;

            }
            else if (radioButton2.Checked == true)
            {
                ProcessImage(img);
            }
            */
        }

        // Close Video Source
        private void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }


        //Detect Shapes
        private Bitmap Mark_all_Shapes(Bitmap input)
        {
            if (windowOpen)
                myDetaildWindow.Set_PictureOriginal(input);


            //Bitmap zwischenspeiche
            Bitmap help = input;

            //1. Farbilter
            input =  myRGB_Filter.Apply(input);
            if (windowOpen)
                myDetaildWindow.Set_PictureColorFilter(input);


            //Graustufen-Bild           
            input = myGrayScale.Apply(input);
            if (windowOpen)
                myDetaildWindow.Set_PictureGreyScale(input);

            //Blur Filter           
            input = blurfilter.Apply(input);

            //Kantenfilter            
            input = myEdge_Filter.Apply(input);
            if (windowOpen)
                myDetaildWindow.Set_PictureEdge(input);

            //Threshold-Filter            
            input = myThreshold.Apply(input);
            if (windowOpen)
                myDetaildWindow.Set_PictureThreshold(input);


            //Create a instance of blob counter algorithm
            BlobCounter blobCounter = new BlobCounter() {
                MinWidth = 70,
                MinHeight = 70,
                FilterBlobs = true,
            };

            //Objekte finden
            blobCounter.ProcessImage(input);          
            Blob[] objectFound = blobCounter.GetObjectsInformation();

            Graphics _g = Graphics.FromImage(help);

          
            //Shapes überprüfen
            SimpleShapeChecker _shapeChecker = new SimpleShapeChecker();

            //Liste meiner Objeckte
            MyObjects = new List<Detected_Object>();

            foreach(Blob element in objectFound)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(element);

                Pen _pen = new Pen(Color.Red, 5);

                //Kreis
                if (_shapeChecker.IsCircle(edgePoints, out AForge.Point mittelPunkt, out float radius))
                {
                    Detected_Object _object = new Circile(mittelPunkt, radius);

                    _object.DrawInGraphic(_pen, _g);

                    MyObjects.Add(_object);
                }
                //Rechteck
                else if (_shapeChecker.IsConvexPolygon(edgePoints, out List<IntPoint> corners) & corners.Count == 4)
                {
                    Detected_Object _object = new Rectangular(corners);

                    _object.DrawInGraphic(_pen, _g);

                    MyObjects.Add(_object);
                }

            }

           
            if (windowOpen)
            {
                myDetaildWindow.Set_PictureObjects(help);
                myDetaildWindow.ShowObjectsInGUI(MyObjects);
            }
                
            return help;
        }

        private double FindDistance(int _pixel)
        {
            double _distance;
            double _ObjectWidth = 10, _focalLength = 604.8;

            //_distance = Convert.ToInt16((_ObjectWidth * _focalLength) / _pixel);
            _distance = (_ObjectWidth * _focalLength) / _pixel;

            return _distance;
        }


    }
}
