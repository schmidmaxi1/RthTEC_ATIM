using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;


using ATIM_GUI._01_TTA;
using ATIM_GUI._02_Sensitivity;

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

        public bool break_is_wished = false;

        #region Meas-Buttons

        //**************************************************************************************************
        //                                            Z_th
        //**************************************************************************************************

        #region Z_th

        private void Button_Zth_signle_Click(object sender, EventArgs e)
        {
            //TTA-Mess-Klasse erzeugen und Geräte übergeben
            myTTA_new = new TTA_measurement_new()
            {
                MyRack = rthTEC_Rack1,
                MyDAQ = myDAQ,
                GUI = this,
            };

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

       
        private void Button_Auto_Zth_Click(object sender, EventArgs e)
        {
            //TTA-Mess-File erzeugen erzeugen
            myTTA_new = new TTA_measurement_new()
            {
                MyRack = rthTEC_Rack1,
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

        private void Button_Single_Sensitivity_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not relized yet");
        }

        private void Button_Auto_Sensitivity_Click(object sender, EventArgs e)
        {
            //Neues Mess-Klasse erzeugen
            mySensitivity_new = new Sensitivity_Measurement_new()
            {
                MyRack = rthTEC_Rack1,
                MyDAQ = myDAQ,
                MyTEC = myTEC,
                MyXYZ = myXYZ,
                Output_File_Folder = myFileSetting.readBox_FileFolder1.MyPath,
                Output_File_Name = myFileSetting.readBox_FileFolder1.MyFileName,
                GUI = this,
                MyMovement_Infos = myFileSetting.readBox_Movement1.MyMovementInfo,
            };

            //Temperatur-Schritte abfragen über Fenster
            Window_Sensitivity_TempStepSelect kFactorWindow = new Window_Sensitivity_TempStepSelect(this);

            //Fenster öffnen und warten bis geschlossen
            kFactorWindow.ShowDialog();

            //Falls keine Temperatur-Schritte zurückgegeben werden, dann abbrechen
            if (mySensitivity_new.TempSteps.Count == 0)
                return;

            //Graphen anpassen???????????????????????????????????????????????????????????????????????
            if (akt_Graph_Setup != "Sensitivity")
                //Graphen anpassen
                Graph_Init_for_Sensitivity(mySensitivity_new);
            else
                //Nur leeren (muss noch angepasst werden)
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

        private void Button_Single_UI_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not relized yet");
            //rthTEC_Rack1.Deterministic_Pulse_Start();
        }

        private void Button_Auto_UI_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not relized yet");
        }
    

        #endregion UI

        //**************************************************************************************************
        //                                            Cancel
        //**************************************************************************************************

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            //Flag wird gesetzt, bei nächstmöglichen Abbruch Punkt wird die Messung gestoppt
            break_is_wished = true;
            myBackroundWorker.CancelAsync();
        }

        #endregion Meas-Buttons

        //**************************************************************************************************
        //                                       Operationality
        //**************************************************************************************************

        #region Operationalitiy

        private Boolean Operationalitiy_TTA_Single()
        {
            return true;
        }

        private Boolean Operationalitiy_TTA_Auto()
        {
            return true;

            //Abfrage ob alle nötigen Geräte angeschlossen (BSP alt)
            /*
            if (!Is_Spectrum_Activ()    ) { return; }
            if (!Is_Heller_Activ()      ) { return; }
            if (!Is_Power_Supply_Activ()) { return; }
            if (!Is_XYZ_Aktive()        ) { return; }
            if (!Is_Directory_Correct() ) { return; }
            if (!Is_FileName_Correct()  ) { return; }
            if (!Is_Gerber_Correct()    ) { return; }
            */
        }

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

                string test = myTTA.Output_File_Folder + file_start + ".aver.TTAcool";

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

        #endregion Operationalitiy
    }
}
