/* Verlauf Versionen: 
 * 
 * V 0.0.1: (Maxi 09.07.2018)
 * **********************************************************
 * -Einfügen aller Geräte in Klassen-Struktur
 * -Hameg, RthRack und TEC funktionieren (Test von XYZ und Hameg noch nicht möglich
 * -Einführung Setting Funktion(noch nicht vollständig)
 * 
 * 
 * V 0.0.2: (Maxi 12.07.2018)
 * **********************************************************
 * -Kommunikations-Settings (vollständig implementiert)
 * -AutoConnect-Vollständig implementiert
 * -Kamera angefangen
 * 
 * V 0.0.3: (Maxi 20.07.2018)
 * **********************************************************
 * -Implementierung neuer XYZ-Tisch (unterteilung in Unterklassen)
 * -Signal-Lampe integrieren
 * -GUI XYZ vollständig
 * 
 *  * V 0.0.4: (Maxi 27.07.2018)
 * **********************************************************
 * -Buttons für Measurment eingefügt
 * -ISEL Klassen vollständig getrennt
 * 
 *  * V 0.0.5: (Maxi 28.08.2018)
 * **********************************************************
 * -Graphen eingefüft
 * -Demo für TTA-Daten (Angefangen)
 * 
 *  * V 0.0.6: (Maxi 28.08.2018)
 * **********************************************************
 * -Geräte automatisch öffnen
 * -StatusBar
 * -DEMO fü TTA
 * 
 *  *  * V 0.0.7: (Maxi 05.09.2018)
 * **********************************************************
 * -Automatisches sperren bei Messung
 * -7b: Versuch umschreiben XYZ-Klasse --> aber Falscher weg
 * 
 *  *  *  * V 0.0.10: (Maxi 26.09.2018)
 * **********************************************************
 * -Demo für Sensitivity-Messung vollständig
 * 
 *  *  *  *  * V 0.0.11: (Maxi 28.09.2018)
 * **********************************************************
 * -Test am System --> TTA single funktioniert
 * 
 *  *  *  *  *  * V 0.0.13: (Maxi 30.09.2018)
 * **********************************************************
 * -XYZ-Tisch implementierung 
 * -Fahren zu Punkt
 * 
 *  *  *  *  *  *  * V 0.0.15: (Maxi 23.10.2018)
 * **********************************************************
 * -Aufteilen DAQ-System
 * -Integration NI-Karte
 * 
 *  *  *  *  *  *  *  * V 0.0.15: (Maxi 23.10.2018)
 * **********************************************************
 * -Ribbon gerichten
 * 
 *  *  *  *  *  *  *  *  * V 0.0.16: (Maxi 28.10.2018)
 * **********************************************************
 * -Setting automatisiert
 * 
 * Offene Punkte
 * **********************************************************
 * -Automatic TTA
 * -UI-Messung
 * -Kamera
 * -Peltier auf 4Elemente
 */


using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO.Ports;
using System;

using System.IO;

using DevExpress.XtraCharts;


//Meine NameSpaces
using ATIM_GUI._0_Classes_Measurement;
using ATIM_GUI._1_Communication_Settings;
using ATIM_GUI._2_AutoConnect;
using ATIM_GUI._3_Project;
using ATIM_GUI._4_Settings;

using ATIM_GUI._05_XYZ;

using Power_Supply_HamegHMP;
using TEC_Controller;

using Read_Coordinates;


namespace ATIM_GUI
{
    public partial class ATIM_MainWindow : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        #region Variablen

        //Devices  
        public _05_XYZ.XYZ_table xyZ_table1;
        //public _6_TempPlatte.TEC_Meerstetter teC_Meerstetter1;
        //public _7_PowerSupply.Hameg_HMP hameg_HMP1;
        public _8_Rth_TEC_Rack.RthTEC_Rack rthTEC_Rack1;
        public _09_DAQ_Unit.DAQ_Unit DAQ_Unit;
        public DXApplication3._10_Camera.Camera_Gerber camera_Gerber1;

        //Devices new
        public PowerSupply_HMP myPowerSupply;
        public I_TEC_Controller myTEC;

        public FileSettings_Box myFileSetting;


        //Settings
        public Settings_ATIM mySettings = null;


        #endregion Variablen

        public BackgroundWorker myBackroundWorker = null;

        public Sensitvity_Measurement mySensitivity;

        public TTA_measurement myTTA;

        //**************************************************************************************************
        //                                          Init
        //**************************************************************************************************

        #region Init

        public ATIM_MainWindow()
        {
            //Punkt statt Komma
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-Us");
            
            //Communication Settings_öffnen und schließen, damit COM-Settings übernommen werden
            //Window_Communication_Settingscs myComSettings = new Window_Communication_Settingscs(this);
            //myComSettings.Show();
            //myComSettings.Close();

            //Muss später ausgetauscht werden
            InitializeComponent();
            

            MyInitializeComponent();

            //Setting Window öffnen um Grundsettings zu laden
            mySettings = new Settings_ATIM();

            // vorrübergehend  COM Port settings
            //myPowerSupply.ComPort_select.SelectedIndex = 10;
            

        }

        private void MyInitializeComponent()
        {
            //Communication Settings_öffnen und schließen, damit COM-Settings übernommen werden
            Window_Communication_Settingscs myComSettings = new Window_Communication_Settingscs(this);
            myComSettings.Show();



            //Alle Geräte zur Oberfläche hinzufügen (muss hiergemacht werden, da sonst InitializeComponent überschrieben wird)
            foreach (var comDevice in myComSettings.ListeCOMs)
            {
                if (comDevice.Name.Contains("XYZ"))
                {
                    if (comDevice.Name.Contains("4Axis"))
                        xyZ_table1 = new ATIM_GUI._05_XYZ.ISEL_4Achsen();
                    else if (comDevice.Name.Contains("3Axis"))
                        xyZ_table1 = new ATIM_GUI._05_XYZ.ISEL_3Achsen();
                    else
                        xyZ_table1 = null;
                }
            }

            foreach (var ethernetDevice in myComSettings.ListEthernet)
            {
                if (ethernetDevice.Name.Contains("DAQ"))
                {
                    if (ethernetDevice.Name.Contains("Spectrum"))
                        DAQ_Unit = new ATIM_GUI._09_DAQ_Unit.Spectrum();
                }
            }

            foreach (var NIDevice in myComSettings.ListNI)
            {
                if (NIDevice.Name.Contains("DAQ"))
                {
                    if (NIDevice.Name.Contains("NI-USB6281"))
                        DAQ_Unit = new ATIM_GUI._09_DAQ_Unit.NI_USB6281();
                }
            }



            //teC_Meerstetter1 = new ATIM_GUI._6_TempPlatte.TEC_Meerstetter();
            //hameg_HMP1 = new ATIM_GUI._7_PowerSupply.Hameg_HMP();
            rthTEC_Rack1 = new _8_Rth_TEC_Rack.RthTEC_Rack();
            
            camera_Gerber1 = new DXApplication3._10_Camera.Camera_Gerber();

            //new
            myTEC = new Meerstetter_4fach(this, 10, 245);
            myPowerSupply = new PowerSupply_HMP(this, 10, 345);

            myFileSetting = new FileSettings_Box(this, 10, 140);

            #region Variablen_Setting

            // 
            // rthTEC_Rack1
            // 
            this.rthTEC_Rack1.Location = new System.Drawing.Point(10, 450);
            this.rthTEC_Rack1.Name = "rthTEC_Rack1";
            this.rthTEC_Rack1.Size = new System.Drawing.Size(250, 452);
            this.rthTEC_Rack1.TabIndex = 7;
            // 
            // hameg_HMP1
            // 
            //this.hameg_HMP1.Location = new System.Drawing.Point(270, 245);
            //this.hameg_HMP1.Name = "hameg_HMP1";
            //this.hameg_HMP1.Size = new System.Drawing.Size(250, 120);
            //this.hameg_HMP1.TabIndex = 5;
            // 
            // xyZ_table1
            // 
            this.xyZ_table1.A_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.xyZ_table1.Anfahrt_z = new decimal(new int[] {
            40,
            0,
            0,
            -2147483648});

            this.xyZ_table1.Location = new System.Drawing.Point(270, 450);
            this.xyZ_table1.Name = "xyZ_table1";
            this.xyZ_table1.Size = new System.Drawing.Size(250, 95);
            this.xyZ_table1.TabIndex = 1;
            this.xyZ_table1.X_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.xyZ_table1.Y_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.xyZ_table1.Z_ges_schnell = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // teC_Meerstetter1
            // 
            //this.teC_Meerstetter1.Location = new System.Drawing.Point(270, 480);
            //this.teC_Meerstetter1.Name = "teC_Meerstetter1";
            //this.teC_Meerstetter1.Size = new System.Drawing.Size(250, 130);
            //this.teC_Meerstetter1.TabIndex = 3;
            // 
            // DAQ-Unit
            // 
            this.DAQ_Unit.Location = new System.Drawing.Point(270, 620);
            this.DAQ_Unit.Name = "spectrum1";
            this.DAQ_Unit.Size = new System.Drawing.Size(250, 160);
            this.DAQ_Unit.TabIndex = 9;
            // 
            // camera_Gerber1
            // 
            this.camera_Gerber1.Location = new System.Drawing.Point(10, 780);
            this.camera_Gerber1.Name = "camera_Gerber1";
            this.camera_Gerber1.Size = new System.Drawing.Size(521, 207);
            this.camera_Gerber1.TabIndex = 13;

            #endregion Variablen_Setting

            //this.Controls.Add(this.teC_Meerstetter1);
            //this.Controls.Add(this.hameg_HMP1);
            this.Controls.Add(this.rthTEC_Rack1);
            this.Controls.Add(this.DAQ_Unit);
            this.Controls.Add(this.camera_Gerber1);
            this.Controls.Add(this.xyZ_table1);




            //Auto-Setting schließen
            myComSettings.Close();
        }

        #endregion Init

        //**************************************************************************************************
        //                             Adopt - Funktionen für Ribbons
        //**************************************************************************************************

        #region Adopt Funktionen

        public void Adopt_Communication_settings(List<SerialCommunicationDivice> serial, List<EthernetCommunicationDevice> ethernet, List<NI_CommunicationDevice> NI)
        {
            foreach(var element in serial)
            {
                if (element.Name.IndexOf("Rth") >= 0)
                {
                    rthTEC_Rack1.Serial_Interface = element.ToSerialPort();
                    rthTEC_Rack1.ComPort_select.Text = element.comboBox_Port.Text;
                }
                else if (element.Name.IndexOf("TEC") >= 0)
                {
                    //TEC geht nicht so einfach zum umschreiben (zwei verschiedene Klassen
                    //SerialPort help = element.ToSerialPort();
                    //teC_Meerstetter1.meComPhySerialPort.Parity = help.Parity;
                    //teC_Meerstetter1.meComPhySerialPort.ReadTimeout = help.ReadTimeout;
                    //teC_Meerstetter1.meComPhySerialPort.DataBits = help.DataBits;
                    //teC_Meerstetter1.meComPhySerialPort.StopBits = help.StopBits;
                    //teC_Meerstetter1.meComPhySerialPort.BaudRate = help.BaudRate;

                    //teC_Meerstetter1.ComPort_select.Text = element.comboBox_Port.Text;
                }
                if (element.Name.IndexOf("PowerSupply") >= 0)
                {
                    //hameg_HMP1.Serial_Interface = element.ToSerialPort();
                    //hameg_HMP1.ComPort_select.Text = element.comboBox_Port.Text;
                }
                if (element.Name.IndexOf("XYZ") >= 0)
                {
                    xyZ_table1.Serial_Interface = element.ToSerialPort();
                    xyZ_table1.ComPort_select.Text = element.comboBox_Port.Text;
                }
            }

            foreach(var element in ethernet)
            {
                if (element.Name.IndexOf("DAQ") >= 0)
                {
                    DAQ_Unit.VISA_or_Channel_Name = element.textBox_IP.Text;
                }
            }

            foreach (var element in NI)
            {
                if (element.Name.IndexOf("DAQ") >= 0)
                {
                    DAQ_Unit.ComPort_select.Text = element.comboBox_Channel.Text;
                }
            }


        }

        public void Adopt_Measurement_settings()
        {
            rthTEC_Rack1.AutoSettings(this.mySettings);
            DAQ_Unit.AutoSettings(this.mySettings);

            myFileSetting.readBox_FileFolder1.textBox_FileName.Text = this.mySettings.Save_FileName;
            myFileSetting.readBox_FileFolder1.textBox_Path.Text = this.mySettings.Save_Folder;
            myFileSetting.readBox_Movement1.textBox_Gerber.Text = this.mySettings.GerberFile_Path;         
        }

        #endregion Adopt Funktionen

        //**************************************************************************************************
        //                          Oberfläche aktivieren/ deaktivieren                              (check)
        //**************************************************************************************************

        #region Oberfläche deaktivieren

        private void Disable_All_Controlls()
        {
            //Devices deaktiveren
            xyZ_table1.Change_Enabled(false);           
            myPowerSupply.Change_Enabled(false);
            myTEC.Change_Enabled(false);
            DAQ_Unit.Change_Enabled(false);
            camera_Gerber1.Change_Enabled(false);
            rthTEC_Rack1.Change_Enabled(false);

            //Buttons deaktieren
            button_Auto_Sensitivity.Enabled = false;
            button_Auto_UI.Enabled = false;
            button_Auto_Zth.Enabled = false;

            button_Single_Sensitivity.Enabled = false;
            button_Single_UI.Enabled = false;
            button_Single_Zth.Enabled = false;
        }

        private void Set_Old_Enable_Status()
        {
            //Devices aktivieren
            xyZ_table1.Change_Enabled(true);
            myPowerSupply.Change_Enabled(true);
            myTEC.Change_Enabled(true);
            DAQ_Unit.Change_Enabled(true);
            camera_Gerber1.Change_Enabled(true);
            rthTEC_Rack1.Change_Enabled(true);

            //Buttons aktiveren
            Enable_Button_from_Parallel_Thread(button_Auto_Sensitivity);
            Enable_Button_from_Parallel_Thread(button_Auto_UI);
            Enable_Button_from_Parallel_Thread(button_Auto_Zth);

            Enable_Button_from_Parallel_Thread(button_Single_Sensitivity);
            Enable_Button_from_Parallel_Thread(button_Single_UI);
            Enable_Button_from_Parallel_Thread(button_Single_Zth);

        }

        private void Enable_Button_from_Parallel_Thread(DevExpress.XtraEditors.SimpleButton myButton)
        {
            myButton.Invoke((MethodInvoker)delegate
            {
                myButton.Enabled = true;
            });
        }

        #endregion Oberfläche deaktivieren


        //**************************************************************************************************
        //                                       Resize                                              (check)
        //**************************************************************************************************

        #region Resize

        private void MyResize_FCT()
        {
            int new_Height = this.Size.Height;
            int new_Width = this.Size.Width;

            //Graphs
            splitContainer1.Size = new Size(new_Width - 530 - 140, new_Height - 153 - 32);

            //Buttons verschieben
            groupBox_Single_Meas.Location = new Point(new_Width - 130, groupBox_Single_Meas.Location.Y);
            groupBox_Automatic.Location = new Point(new_Width - 130, groupBox_Automatic.Location.Y);
            groupBox_Cancel.Location = new Point(new_Width - 130, groupBox_Cancel.Location.Y);

            //test
            var test = this.MinimumSize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MyResize_FCT();
        }

        #endregion Resize

    }
}
