using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Windows.Forms;
using System.IO;


using ATIM_GUI._01_TTA;
using ATIM_GUI._02_Sensitivity;

using RthTEC_Rack;

using Hilfsfunktionen;

namespace ATIM_GUI
{
    public partial class ATIM_MainWindow : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        //**************************************************************************************************
        //                                  Measurement - Buttons
        //**************************************************************************************************

        public BackgroundWorker myBackroundWorker = null;

        public Sensitivity_Measurement_new mySensitivity_new;

        public TTA_measurement_new myTTA_new;


        #region Meas-Buttons

        //**************************************************************************************************
        //                                            GUI
        //**************************************************************************************************

        #region GUI

        private void Button_Zth_signle_Click(object sender, EventArgs e)
        {
            TTA_single();
        }

        private void Button_Auto_Zth_Click(object sender, EventArgs e)
        {
            TTA_Automatic();
        }

        private void Button_Single_Sensitivity_Click(object sender, EventArgs e)
        {
            Sensitivity_Single();
        }

        private void Button_Auto_Sensitivity_Click(object sender, EventArgs e)
        {
            Sensitivity_Automatic();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            //Flag wird gesetzt, bei nächstmöglichen Abbruch Punkt wird die Messung gestoppt
            myBackroundWorker.CancelAsync();
        }

        private void Button_Single_UI_Click(object sender, EventArgs e)
        {
            
            //rthTEC_Rack1.Deterministic_Pulse_Start();
            Characteristics_Single();
        }

        private void Button_Auto_UI_Click(object sender, EventArgs e)
        {
            Characteristics_Automatic();
        }

        #endregion GUI

        //**************************************************************************************************
        //                                            Z_th
        //**************************************************************************************************

        #region Z_th

        private void TTA_single()
        {
            //TTA-Mess-Klasse erzeugen und Geräte übergeben
            myTTA_new = new TTA_measurement_new()
            {
                MyRthTEC_Rack = myRthTEC,
                MyHeatSource = (I_CardType_Power)myRthTEC.Cards[0],     //Später Variabel
                MyFrontEnd = (I_CardType_Amp)myRthTEC.Cards[1],
                MyDAQ = myDAQ,
                GUI = this,
            };

            //Check if all neccessary Devices and Settings are available
            if (!Operationalitiy_TTA_Single())
                return;

            //Plots initisieren (Anpassung muss im MainThread passiern)
            Graph_new_Measurment_for_TTA(myTTA_new);

            //Backroundworker definieren
            myBackroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };

            //Aufgabe definieren
            myBackroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                //Messen 
                myTTA_new.Start_Single_TTA();

                //UI wieder aktiviern
                Set_Old_Enable_Status();
            }
            );

            //Alle Knöpfe deaktiveren
            Disable_All_Controlls();

            //Backroundworker starten
            myBackroundWorker.RunWorkerAsync();
        }

        private void TTA_Automatic()
        {
            //TTA-Mess-File erzeugen erzeugen
            myTTA_new = new TTA_measurement_new()
            {
                MyRthTEC_Rack = myRthTEC,
                MyHeatSource = (I_CardType_Power)myRthTEC.Cards[0],     //Später Variabel
                MyFrontEnd = (I_CardType_Amp)myRthTEC.Cards[1],
                MyDAQ = myDAQ,
                MyXYZ = myXYZ,
                Output_File_Folder = myFileSetting.readBox_FileFolder1.MyPath,
                Output_File_Name = myFileSetting.readBox_FileFolder1.MyFileName,
                GUI = this,
                MyMovement = myFileSetting.readBox_Movement1.MyMovementInfo
            };

            //Check if all neccessary Devices and Settings are available
            if (!Operationalitiy_TTA_Auto())
                return;

            //Check if Files would be overwritten
            if (!Check_File_Overwrite(myTTA_new))
                return;


            //Plots initisieren (Anpassung muss im MainThread passiern)
            Graph_new_Measurment_for_TTA(myTTA_new);

            chartControl_RAW.Update();

            //Backroundworker definieren
            myBackroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };

            //Aufgabe definieren
            myBackroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                //Messen
                myTTA_new.Start_Automatic_TTA();

                //UI wieder aktiviern
                Set_Old_Enable_Status();
            }
            );

            //Alle Knöpfe deaktiveren
            Disable_All_Controlls();

            //Backroundworker starten
            myBackroundWorker.RunWorkerAsync();
        }

        #endregion Z_th

        //**************************************************************************************************
        //                                           Sensitivity
        //**************************************************************************************************

        #region Sensitivity

        private void Sensitivity_Single()
        {
            MessageBox.Show("Not relized yet");
        }

        private void Sensitivity_Automatic()
        {
            //Neues Mess-Klasse erzeugen
            mySensitivity_new = new Sensitivity_Measurement_new()
            {
                MyRack = myRthTEC,
                MyHeatSource = (I_CardType_Power)myRthTEC.Cards[0],     //Später Variabel
                MyFrontEnd = (I_CardType_Amp)myRthTEC.Cards[1],
                MyDAQ = myDAQ,
                MyTEC = myTEC,
                MyXYZ = myXYZ,
                Output_File_Folder = myFileSetting.readBox_FileFolder1.MyPath,
                Output_File_Name = myFileSetting.readBox_FileFolder1.MyFileName,
                GUI = this,
                MyMovement_Infos = myFileSetting.readBox_Movement1.MyMovementInfo,
            };

            //Check if all neccessary Devices and Settings are available
            if (!Operationalitiy_Sensitivity_Auto())
                return;

            //Check if Files would be overwritten
            if (!Check_File_Overwrite(mySensitivity_new))
                return;

            //Temperatur-Schritte abfragen über Fenster
            Window_Sensitivity_TempStepSelect kFactorWindow = new Window_Sensitivity_TempStepSelect(this);

            //Fenster öffnen und warten bis geschlossen
            kFactorWindow.ShowDialog();

            //Falls keine Temperatur-Schritte zurückgegeben werden, dann abbrechen
            if (mySensitivity_new.TempSteps.Count == 0)
                return;

            //Graphen anpassen
            Graph_Init_for_Sensitivity(mySensitivity_new);


            //Backroundworker definieren
            myBackroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };

            //Aufgabe definieren
            myBackroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                //Messung starten
                mySensitivity_new.Start_Measurement();

                //UI wieder aktiviern
                Set_Old_Enable_Status();
            });

            //Alle Knöpfe deaktiveren
            Disable_All_Controlls();

            //Backroundworker starten
            myBackroundWorker.RunWorkerAsync();
        }

        #endregion Sensitivity


        //**************************************************************************************************
        //                                             U(I)
        //**************************************************************************************************

        #region UI

        private void Characteristics_Single() {
            MessageBox.Show("Not relized yet");
        }

        private async void Characteristics_Automatic()
        {
            // Wird für Hella all genutzt (VORLÄUFIG)

            //Starten mit Sensitivity --> alle LEDs auf einmal
            myFileSetting.readBox_Movement1.textBox_Gerber.Text = @"C:\Users\schmidm\Desktop\ATIM_GIT\2_DrivingFiles_neu\LED_Comparison_BRD_ALL.txt";

            //Sensitivity Messung starten
            Sensitivity_Automatic();

            //Warten bis fertig
            while (button_Auto_UI.Enabled == false)
                await Task.Delay(5000);

            //10 minuten warten damit TEC wieder auf Raum-Temperature komm
            Wait_withoutBreak(600);

            while (button_Auto_UI.Enabled == false)
                await Task.Delay(500);


            //TTA für Samsung @ 350mA
            ((Card_LED_Source)myRthTEC.Cards[0]).Set_I_Heat(350);       //Typ convert um auf Funktion zugreifen zu können
            myRthTEC.SetEnable(true);
            myFileSetting.readBox_Movement1.textBox_Gerber.Text = @"C:\Users\schmidm\Desktop\ATIM_GIT\2_DrivingFiles_neu\LED_Comparison_BRD_350mA.txt";

            while (!myRthTEC.IsEnabled)
            {
                myRthTEC.SetEnable(true);
                await Task.Delay(100);
            }

            TTA_Automatic();

            //Warten bis fertig
            while (button_Auto_UI.Enabled == false)
                await Task.Delay(5000);

            //TTA für Seaul @ 700mA
            ((Card_LED_Source)myRthTEC.Cards[0]).Set_I_Heat(700);       //Typ convert um auf Funktion zugreifen zu können

            myRthTEC.SetEnable(true);
            myFileSetting.readBox_Movement1.textBox_Gerber.Text = @"C:\Users\schmidm\Desktop\ATIM_GIT\2_DrivingFiles_neu\LED_Comparison_BRD_700mA.txt";

            while (!myRthTEC.IsEnabled)
            {
                myRthTEC.SetEnable(true);
                await Task.Delay(100);
            }

            TTA_Automatic();

            //Warten bis fertig
            while (button_Auto_UI.Enabled == false)
                await Task.Delay(5000);

            //TTA für Rest @ 1000mA
            ((Card_LED_Source)myRthTEC.Cards[0]).Set_I_Heat(1000);       //Typ convert um auf Funktion zugreifen zu können
            myRthTEC.SetEnable(true);
            myFileSetting.readBox_Movement1.textBox_Gerber.Text = @"C:\Users\schmidm\Desktop\ATIM_GIT\2_DrivingFiles_neu\LED_Comparison_BRD_1000mA_ohneBF.txt";

            while (!myRthTEC.IsEnabled)
            {
                myRthTEC.SetEnable(true);
                await Task.Delay(100);
            }


            TTA_Automatic();

        }

        #endregion UI


        #endregion Meas-Buttons

        //**************************************************************************************************
        //                                          WAIT
        //**************************************************************************************************

        private void Wait_withoutBreak(int time_in_Sec)
        {
            //Backroundworker definieren
            myBackroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };

            //Aufgabe definieren
            myBackroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                //Messen 
                for (int i = 0; i < time_in_Sec; i++)
                {
                    //StatusBar anpassen
                    StatusBar_Wait_Time(i , time_in_Sec);

                    //Eine Sekunde Warten
                    Thread.Sleep(1000);

                    if (myBackroundWorker.CancellationPending)                    
                        i = time_in_Sec;
                    
                }

                //UI wieder aktiviern
                Set_Old_Enable_Status();
            }
            );

            //Alle Knöpfe deaktiveren
            Disable_All_Controlls();

            //Backroundworker starten
            myBackroundWorker.RunWorkerAsync();
        }

        //**************************************************************************************************
        //                                       Operationality
        //**************************************************************************************************

        #region Operationalitiy

        /// <summary>
        /// Checks if all neccessary Equipments are connected and working (false at failure of on)
        /// </summary>
        /// <returns>true: no failure, false: at least on failure</returns>
        private Boolean Operationalitiy_TTA_Single()
        {
            //temporär string
            string fehlerTXT = "";

            //DAQ Unit
            if (!myDAQ.IsConnected)
                fehlerTXT += "DAQ is not connected" + Environment.NewLine;

            //RthRack
            if (!myRthTEC.IsConnected)
                fehlerTXT += "RthTEC Equipment is not connected." + Environment.NewLine;

            //RthRack
            if (!myRthTEC.IsEnabled)
                fehlerTXT += "RthTEC Equipment is not enabled." + Environment.NewLine;

            //Power Supply
            if (!myPowerSupply.IsConnected)
                fehlerTXT += "PowerSupply is not connected" + Environment.NewLine;



            //Wenn etwas in temporären String geschreieben wurden --> MessageBox
            if (fehlerTXT == "")
                return true;

            else
            {
                string warningTXT = "Not able to run TTA: " + Environment.NewLine + Environment.NewLine
                    + fehlerTXT + Environment.NewLine
                    + "Measurement is canceld.";

                MessageBox.Show(warningTXT, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }


        /// <summary>
        /// Checks if all neccessary Equipments are connected and working (false at failure of on)
        /// </summary>
        /// <returns>true: no failure, false: at least on failure</returns>
        private Boolean Operationalitiy_TTA_Auto()
        {
            //temporär string
            string fehlerTXT = "";

            //DAQ Unit
            if (!myDAQ.IsConnected)
                fehlerTXT += "DAQ is not connected" + Environment.NewLine;

            //XYZ
            if(!myXYZ.IsConnected)
                fehlerTXT += "XYZ is not connected" + Environment.NewLine;

            //Movement
            if (!myFileSetting.readBox_Movement1.MyMovementInfo.IsCorect)
                fehlerTXT += "Movement file is not correct." + Environment.NewLine;

            //RthRack
            if(!myRthTEC.IsConnected)
                fehlerTXT += "RthTEC Equipment is not connected." + Environment.NewLine;

            //RthRack
            if (!myRthTEC.IsEnabled)
                fehlerTXT += "RthTEC Equipment is not enabled." + Environment.NewLine;

            //Power Supply
            if(!myPowerSupply.IsConnected)
                fehlerTXT += "PowerSupply is not connected" + Environment.NewLine;



            //Wenn etwas in temporären String geschreieben wurden --> MessageBox
            if (fehlerTXT == "")
                return true;

            else
            {
                string warningTXT = "Not able to run TTA: " + Environment.NewLine + Environment.NewLine
                    + fehlerTXT + Environment.NewLine 
                    + "Measurement is canceld.";

                MessageBox.Show(warningTXT, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Checks if all neccessary Equipments are connected and working (false at failure of on)
        /// </summary>
        /// <returns>true: no failure, false: at least on failure</returns>
        private Boolean Operationalitiy_Sensitivity_Auto()
        {
            //temporär string
            string fehlerTXT = "";

            //DAQ Unit
            if (!myDAQ.IsConnected)
                fehlerTXT += "DAQ is not connected" + Environment.NewLine;

            //XYZ
            if (!myXYZ.IsConnected)
                fehlerTXT += "XYZ is not connected" + Environment.NewLine;

            //Movement
            if (!myFileSetting.readBox_Movement1.MyMovementInfo.IsCorect)
                fehlerTXT += "Movement file is not correct." + Environment.NewLine;

            //TEC
            if (!myTEC.IsConnected)
                fehlerTXT += "TEC is not connected." + Environment.NewLine;

            //TEC
            if (!myTEC.IsRunning)
                fehlerTXT += "TEC is not running." + Environment.NewLine;

            //RthRack
            if (!myRthTEC.IsConnected)
                fehlerTXT += "RthTEC Equipment is not connected." + Environment.NewLine;

            //RthRack
            if (!myRthTEC.IsEnabled)
                fehlerTXT += "RthTEC Equipment is not enabled." + Environment.NewLine;

            //Power Supply
            if (!myPowerSupply.IsConnected)
                fehlerTXT += "PowerSupply is not connected" + Environment.NewLine;



            //Wenn etwas in temporären String geschreieben wurden --> MessageBox
            if (fehlerTXT == "")
                return true;

            else
            {
                string warningTXT = "Not able to run TTA: " + Environment.NewLine + Environment.NewLine
                    + fehlerTXT + Environment.NewLine
                    + "Measurement is canceld.";

                MessageBox.Show(warningTXT, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }


        /// <summary>
        /// Checks if Files would be overwritten and lists them in a MessageBox.
        /// </summary>
        /// <param name="myTTA">My TTA setup with all parameters incl. Folder and File-Name</param>
        /// <returns></returns>
        private Boolean Check_File_Overwrite(TTA_measurement_new myTTA)
        {
            //Liste mit vorhandenen Überschneidungen
            List<string> overlapping = new List<string>();

            //Jedes Bauteil in Mess-Positionen prüfen
            for (int i = 0; i < myTTA.MyMovement.MyMeasurment_Point.Length; i++)
            {
                //File-Name Anfang generieren
                string file_start = HelpFCT.Replace_Output_STR(myTTA.Output_File_Name, myTTA.MyMovement.MyMeasurment_Point[i].Name);

                //Je nachdem was gespeichert werden soll überprüfen
                if (mySaving_Options.Save_Aver_Cool)
                    if (File.Exists(myTTA.Output_File_Folder + Path.DirectorySeparatorChar + file_start + ".aver.TTAcool"))
                        overlapping.Add(file_start + ".aver.TTAcool");

                if (mySaving_Options.Save_Aver_Heat)
                    if (File.Exists(myTTA.Output_File_Folder + Path.DirectorySeparatorChar + file_start + ".aver.TTAheat"))
                        overlapping.Add(file_start + ".aver.TTAheat");

                if (mySaving_Options.Save_Signle_Cool)
                    if (File.Exists(myTTA.Output_File_Folder + Path.DirectorySeparatorChar + file_start + "0001.TTAcool"))
                        overlapping.Add(file_start + "0001.TTAcool");

                if (mySaving_Options.Save_Single_Heat)
                    if (File.Exists(myTTA.Output_File_Folder + Path.DirectorySeparatorChar + file_start + "0001.TTAheat"))
                        overlapping.Add(file_start + "0001.TTAheat");

                if (mySaving_Options.Save_Raw)
                    if (File.Exists(myTTA.Output_File_Folder + Path.DirectorySeparatorChar + file_start + ".0001.TTAraw"))
                        overlapping.Add(file_start + ".0001.TTAraw");
            }

            //Wenn keine Überschneidung gefunden
            if (overlapping.Count == 0)
                return true;

            //Wenn mehr als 10, die Liste kürzen
            else if (overlapping.Count > 10)
            {
                int temp_count = overlapping.Count;
                overlapping.RemoveRange(9, overlapping.Count - 9);
                overlapping.Add("And " + (temp_count - 9).ToString() + "further");
            }

            //Message-Box Text generieren
            string text = "Following Files would be overwritten:" + Environment.NewLine + Environment.NewLine;

            foreach(string temp in overlapping)
                text += temp + Environment.NewLine;

            text += Environment.NewLine + "Do you want to overwrite them?";



            DialogResult dialogResult = MessageBox.Show(text, "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)           
                return true;
            
            else          
                return false;            
        }

        /// <summary>
        /// Checks if Files would be overwritten and lists them in a MessageBox.
        /// </summary>
        /// <param name="mySEN"></param>
        /// <returns>My Sensitivity setup with all parameters incl. Folder and File-Name</returns>
        private Boolean Check_File_Overwrite(Sensitivity_Measurement_new mySEN)
        {
            //Liste mit vorhandenen Überschneidungen
            List<string> overlapping = new List<string>();

            //Jedes Bauteil in Mess-Positionen prüfen
            for (int i = 0; i < mySEN.MyMovement_Infos.MyMeasurment_Point.Length; i++)
            {
                //File-Name Anfang generieren
                string file_start = HelpFCT.Replace_Output_STR(mySEN.Output_File_Name, mySEN.MyMovement_Infos.MyMeasurment_Point[i].Name);

                //Je nachdem was gespeichert werden soll überprüfen
                if (File.Exists(mySEN.Output_File_Folder + Path.DirectorySeparatorChar + file_start + ".sen"))
                    overlapping.Add(file_start + ".aver.TTAcool");
            }

            //Wenn keine Überschneidung gefunden
            if (overlapping.Count == 0)
                return true;

            //Wenn mehr als 10, die Liste kürzen
            else if (overlapping.Count > 10)
            {
                int temp_count = overlapping.Count;
                overlapping.RemoveRange(9, overlapping.Count - 9);
                overlapping.Add("And " + (temp_count - 9).ToString() + "further");
            }

            //Message-Box Text generieren
            string text = "Following Files would be overwritten:" + Environment.NewLine + Environment.NewLine;

            foreach (string temp in overlapping)
                text += temp + Environment.NewLine;

            text += Environment.NewLine + "Do you want to overwrite them?";



            DialogResult dialogResult = MessageBox.Show(text, "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                return true;

            else
                return false;
        }


        #endregion Operationalitiy
    }
}
