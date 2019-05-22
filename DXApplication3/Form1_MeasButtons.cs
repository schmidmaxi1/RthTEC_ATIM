using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ATIM_GUI._0_Classes_Measurement;

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
        }

        private void Button_Auto_Zth_Click(object sender, EventArgs e)
        {
            //TTA-Mess-File erzeugen erzeugen
            TTA_measurement myTTA = new TTA_measurement()
            {
                MyRack = rthTEC_Rack1,
                MyDAQ = DAQ_Unit
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
                //spectrum1.Measure_TTA_Several_Cycles_DEMO(myTTA, this);
                DAQ_Unit.Measure_TTA_Several_Cycles(myTTA, this);

                //Daten konvertieren und abarbeiten
                myTTA.Convert_Data();

                //Graphen plotten
                Add_Series_to_Data(myTTA);
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
            
        }

        private void Button_Auto_Sensitivity_Click(object sender, EventArgs e)
        {
            //Neues Mess-Klasse erzeugen
            mySensitivity = new Sensitvity_Measurement()
            {
                MyRack = rthTEC_Rack1,
                MySpectrum = DAQ_Unit,
                MyTEC = myTEC,
                MyXYZ = xyZ_table1,
                Output_File_Folder = textBox_Path.Text,
                Output_File_Name = textBox_File.Text,
                GUI = this,
            };

            mySensitivity.MyMovement = myXYZ_Movements.Calculated_movements;

            //Temperatur-Schritte abfragen über Fenster
            Window_Sensitivity_TempStepSelect kFactorWindow = new Window_Sensitivity_TempStepSelect(this);
            kFactorWindow.ShowDialog();

            //Falls keine Temperatur-Schritte zurückgegeben werden, dann abbrechen
            if (mySensitivity.TempSteps.Count == 0)
                return;

            //Graphen anpassen
            if (akt_Graph_Setup != "Sensitivity")
                //Graphen anpassen
                Graph_Init_for_Sensitivity(mySensitivity);
            else
                //Nur leeren (muss noch angepasst werden)
                Graph_Init_for_Sensitivity(mySensitivity);

            //Spectrum einstellen
            mySensitivity.MySpectrum.Setting_for_Sensitivity(mySensitivity);

            //Backroundworker definieren
            myBackroundWorker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
            };

            //Aufgabe definieren
            myBackroundWorker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                //Messen --> aktuell noch DEMO
                mySensitivity.Start_Measurement();

                //Daten konvertieren und abarbeiten
                //Buttons wieder aktiveren
                Set_Old_Enable_Status();

                //UI wieder aktiviern
                Set_Old_Enable_Status();

            }
            );

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
