using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using ATIM_GUI._0_Classes_Measurement;

using ATIM_GUI._01_TTA;
using ATIM_GUI._02_Sensitivity;

namespace ATIM_GUI
{
    public partial class ATIM_MainWindow : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        //**************************************************************************************************
        //                                  Measurement - Buttons
        //**************************************************************************************************

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

            //Plots initisieren
            Graph_new_Measurment_for_TTA(myTTA_new);

            //DAQ einstellen
            myDAQ.TTA_set_Device(rthTEC_Rack1.Time_Heat, rthTEC_Rack1.Time_Meas);
            myDAQ.TTA_set_Trigger(rthTEC_Rack1.Gain, rthTEC_Rack1.U_offset);

            //Backroundworker definieren
            myBackroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };

            //Aufgabe definieren
            myBackroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                bool noError = true;

                //Messen 
                if (noError)
                    noError = myTTA_new.Start_Single_TTA();

                //Graphen plotten                
                if (noError)
                {
                    Add_Series_to_Data(myTTA_new);
                    Update_Voltage_Plots_for_TTA();
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

        /* ALTE VERSION
        private void Button_Zth_signle_Click(object sender, EventArgs e)
        {

            //TTA-Mess-File erzeugen erzeugen
            myTTA = new TTA_measurement()
            {
                MyRack = rthTEC_Rack1,
                MyDAQ = DAQ_Unit,
                GUI = this,
            };

            //Plots initisieren
            Graph_new_Measurment_for_TTA(myTTA);

            //Spectrum einstellen
            DAQ_Unit.Setting_for_TTA();
            DAQ_Unit.Setting_Trigger(rthTEC_Rack1);

            //Backroundworker definieren
            myBackroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };

            //Aufgabe definieren
            myBackroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                bool noError = true;

                //Messen --> aktuell noch DEMO
                if (noError)
                    noError = myTTA.Start_Single_TTA();

                //Daten konvertieren und abarbeiten
                if (noError)
                    noError = myTTA.Convert_Data();

                //Graphen plotten
                if (noError)
                {
                    Add_Series_to_Data(myTTA);
                    Update_Voltage_Plots_for_TTA();
                }

                //UI wieder aktiviern
                Set_Old_Enable_Status();
            }
            );

            //Alle Knöpfe deaktiveren
            Disable_All_Controlls();

            //Backroundworker starten
            myBackroundWorker.RunWorkerAsync();
        }*/

        private void Button_Auto_Zth_Click(object sender, EventArgs e)
        {
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

            //Plots initisieren
            Graph_new_Measurment_for_TTA(myTTA_new);

            //Spectrum einstellen
            DAQ_Unit.Setting_for_TTA();
            DAQ_Unit.Setting_Trigger(rthTEC_Rack1);

            //Backroundworker definieren
            myBackroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };

            //Aufgabe definieren
            myBackroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                //Messung für alle starten


                //spectrum1.Measure_TTA_Several_Cycles_DEMO(myTTA, this);
                DAQ_Unit.Measure_TTA_Several_Cycles(myTTA, this);

                //Daten konvertieren und abarbeiten
                myTTA.Convert_Data();

                //Graphen plotten
                Add_Series_to_Data(myTTA_new);
                Update_Voltage_Plots_for_TTA();

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
            rthTEC_Rack1.Deterministic_Pulse_Start();
        }

        private void Button_Auto_UI_Click(object sender, EventArgs e)
        {
            
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
    }
}
