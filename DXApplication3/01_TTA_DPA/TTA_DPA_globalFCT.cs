using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using DAQ_Units;
using XYZ_Table;
using RthTEC_Rack;

using Read_Coordinates;
using Hilfsfunktionen;

//To Read .mat-Files
using Accord.IO;
using Accord.Math;

namespace ATIM_GUI
{
    public partial class TTA_DPA
    {
        //*****************************************************************************************************
        //                                        only global FCTs
        //*****************************************************************************************************

        /// <summary>
        /// TTA measurment without XYZ (but with repetation cycles)
        /// </summary>
        public bool Start_Single_TTA()
        {
            /*
            //Plots initisieren
            GUI.Graph_new_Measurment_for_TTA(this);

            //Status-Bar initialisieren
            GUI.StatusBar_TTA_Single(0, (int)MyRthTEC_Rack.Cycles);

            //DAQ-System anpassen
            if (!MyDAQ.TTA_set_Device(MyRthTEC_Rack.Time_Heat, MyRthTEC_Rack.Time_Meas))
                return false;
            if (!MyDAQ.TTA_set_Trigger(MyFrontEnd.Gain, MyFrontEnd.V_Offset))
                return false;

            //Definiere Feld für Raw-Daten
            Binary_Raw_Files = new short[MyRthTEC_Rack.Cycles, MyDAQ.Samples];

            //Speicher reservieren
            MyDAQ.TTA_reserve_Storage(Binary_Raw_Files);

            //Loop für Wiederholungs-Zyklen
            for (int repetation_nr = 0; repetation_nr < MyRthTEC_Rack.Cycles; repetation_nr++)
            {
                //Bei bedarf abbrechen
                if (GUI.myBackroundWorker.CancellationPending)
                {
                    //Falls abbruch --> Speicher lösen
                    MyDAQ.TTA_free_Storage(Binary_Raw_Files);
                    return false;
                }

                //DAQ scharf stellen
                if (!MyDAQ.TTA_wait_for_Trigger())
                    return false;

                //Puls starten (bei NI mit Warten sonst ohne)
                //MyRack.SinglePuls_withoutDelay();

                //MyRack.SinglePuls_withDelay();
                MyRthTEC_Rack.Start_std_Pulse(true);

                Thread.Sleep(300);


                //Daten abholen
                if (!MyDAQ.TTA_Collect_Data(Binary_Raw_Files, repetation_nr))
                    return false;

                //Daten prüfen
                //????????????????????????????????????????

                //Daten in Raw plotten
                GUI.Add_Series_to_RAW(this, repetation_nr);

                //Statusbar anpassen
                GUI.StatusBar_TTA_Single(repetation_nr + 1, (int)MyRthTEC_Rack.Cycles);
            }

            //DatenSatz auswerten
            if (!Convert_Data(true))
                return false;

            //Plotten
            GUI.Add_Series_to_Data(this);
            GUI.Update_Voltage_Plots_for_TTA();

            //Speicher lösen
            MyDAQ.TTA_free_Storage(Binary_Raw_Files);
            */

            return true;
        }

        /// <summary>
        /// DPA TTA measurment with XYZ
        /// </summary>
        public bool Start_Automatic_TTA()
        {

            return true;

            /*
            //1. Pre-Settings ................................................................................
            //Anzahl LEDs für Counter
            int Nr_of_LEDs = MyMovement.MyMeasurment_Point.Length;

            //Initial-Name für File speicherung zwischenspeichen
            string initial_file_name = Output_File_Name;

            //Plots initisieren
            GUI.Graph_new_Measurment_for_TTA(this);

            //Status-Bar initialisieren
            GUI.StatusBar_TTA_all(0, (int)MyRthTEC_Rack.Cycles, 0, Nr_of_LEDs);

            //DAQ-System anpassen
            if (!MyDAQ.TTA_set_Device(MyRthTEC_Rack.Time_Heat, MyRthTEC_Rack.Time_Meas))
                return false;
            if (!MyDAQ.TTA_set_Trigger(MyFrontEnd.Gain, MyFrontEnd.V_Offset))
                return false;

            //Definiere Feld für Raw-Daten
            Binary_Raw_Files = new short[MyRthTEC_Rack.Cycles, MyDAQ.Samples];

            //Speicher reservieren
            MyDAQ.TTA_reserve_Storage(Binary_Raw_Files);

            //Rauffahrn (immer genau auf 0)
            MyXYZ.Move2Position(MyXYZ.Akt_x_Koordinate, MyXYZ.Akt_y_Koordinate, 0, MyXYZ.Akt_Winkel);

            //An Startpunkt fahren lassen auf Verfahr-Höhe
            Measurement_Point_XYZA newPoint = MyMovement.MyMeasurment_Point[0];
            MyXYZ.Move2Position(newPoint.X, newPoint.Y, MyMovement.Driving_Hight, newPoint.Angle);

            //Measure all LEDs after one other
            for (int akt_DUT_Nr = 1; akt_DUT_Nr <= Nr_of_LEDs; akt_DUT_Nr++)
            {
                //Bei bedarf abbrechen
                if (GUI.myBackroundWorker.CancellationPending)
                {
                    //Falls abbruch --> Speicher lösen
                    MyDAQ.TTA_free_Storage(Binary_Raw_Files);
                    return false;
                }

                //Text StatusBar ändern
                GUI.StatusBar_TTA_all(0, (int)MyRthTEC_Rack.Cycles, akt_DUT_Nr, Nr_of_LEDs);

                //Raw-Data plot leeren
                GUI.Graph_new_Measurment_for_TTA(this);

                //Drive XYZ down to LED
                MyXYZ.MoveADistance(0, 0, MyMovement.TouchDown_Hight - MyMovement.Driving_Hight, 0);

                //Loop für TTA Wiederholungs-Zyklen
                for (int repetation_nr = 0; repetation_nr < MyRthTEC_Rack.Cycles; repetation_nr++)
                {
                    //Bei bedarf abbrechen
                    if (GUI.myBackroundWorker.CancellationPending)
                    {
                        //Falls abbruch --> Speicher lösen
                        MyDAQ.TTA_free_Storage(Binary_Raw_Files);
                        return false;
                    }

                    //DAQ scharf stellen
                    if (!MyDAQ.TTA_wait_for_Trigger())
                        return false;

                    //Puls starten (bei NI mit Warten sonst ohne)
                    MyRthTEC_Rack.Start_std_Pulse(true);

                    //MyRack.SinglePuls_withDelay();

                    Thread.Sleep(300);

                    //Daten abholen
                    if (!MyDAQ.TTA_Collect_Data(Binary_Raw_Files, repetation_nr))
                        //Weitere Messungen überspringen
                        goto next_Device;

                    //return false;

                    //Daten prüfen
                    //????????????????????????????????????????

                    //Daten in Raw plotten
                    GUI.Add_Series_to_RAW(this, repetation_nr);

                    //Text StatusBar ändern
                    GUI.StatusBar_TTA_all(repetation_nr + 1, (int)MyRthTEC_Rack.Cycles, akt_DUT_Nr, Nr_of_LEDs);
                }

                //DatenSatz auswerten, wenn kein Fehler dann auswerten
                if (Convert_Data(false))
                {
                    //Plotten
                    GUI.Add_Series_to_Data(this, MyMovement.MyMeasurment_Point[akt_DUT_Nr - 1].Name);
                    GUI.Update_Voltage_Plots_for_TTA();

                    //OutputFile-Name anpassen
                    Output_File_Name = HelpFCT.Replace_Output_STR(initial_file_name, MyMovement.MyMeasurment_Point[akt_DUT_Nr - 1].Name);
                    //Wpeichern
                    Save_AllFiles();
                }

                goto next_Device;



            /*ALTE Version
            //DatenSatz auswerten
            if (!Convert_Data())
                return false;

            //Plotten
            GUI.Add_Series_to_Data(this, MyMovement.MyMeasurment_Point[akt_DUT_Nr-1].Name);
            GUI.Update_Voltage_Plots_for_TTA();

            //OutputFile-Name anpassen
            Output_File_Name = HelpFCT.Replace_Output_STR(initial_file_name, MyMovement.MyMeasurment_Point[akt_DUT_Nr - 1].Name);
            //Wpeichern
            Save_AllFiles();
            */
            /*
            next_Device:

                //Drive XYZ up
                MyXYZ.MoveADistance(0, 0, MyMovement.Driving_Hight - MyMovement.TouchDown_Hight, 0);

                //Drive to next LED (exept at the end)
                if (akt_DUT_Nr < Nr_of_LEDs)
                {
                    newPoint = MyMovement.MyMeasurment_Point[akt_DUT_Nr];
                    MyXYZ.MoveADistance(newPoint.X, newPoint.Y, 0, newPoint.Angle);
                }
            }

            //Speicher frei geben
            MyDAQ.TTA_free_Storage(Binary_Raw_Files);

            return true;
            */
            
        }


        /// <summary>
        /// Test-FCT for .mat Files (Output of Spectrum)
        /// </summary>
        public bool DPA_Test_matFILE()
        {

            //File Dialog erstellen
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //Settings
                openFileDialog.Filter = "mat files (*.mat)|*.mat|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    //Alle Files abarbeiten
                    foreach (string filePath in openFileDialog.FileNames)
                    {
                        //Pfad und File übernhemen
                        Output_File_Name = filePath.Substring(filePath.LastIndexOf('\\') + 1).Replace(".mat", "");
                        Output_File_Folder = filePath.Substring(0, filePath.LastIndexOf('\\'));

                        //Read .mat File into a stream
                        MatReader reader = new MatReader(filePath);

                        //Daten herauslösen
                        var values = reader["AI_Ch0"].GetValue<Int16[,]>();

                        //Daten in Binary_Raw_Values schreiben
                        Binary_Raw_Values = new short[values.Length];
                        Buffer.BlockCopy(values, 0, Binary_Raw_Values, 0, 2 * values.Length);

                        //Reader schließen
                        reader.Dispose();

                        //Daten Analysieren
                        DPA_Analyse();

                    }
                }
            }



            return true;
        }

        /// <summary>
        /// Compleate Analyse on a short[] of DPA data
        /// </summary>
        public void DPA_Analyse()
        {
            //Plots initialisieren
            GUI.Graph_new_Measurment_for_TTA(this);

            //Daten in Raw plotten
            GUI.Add_Series_to_RAW(this);


            //Ab hier, normale DPA analyse
            Find_Switching_Points();

            //Checken
            Check_Switching_Points();

            //
            Calc_PowerSteps();

            Calc_PowerCorrection();

            Filter_STD_Response();

            Average_DPA();

            Fitting();

            Compressed_Data = Compress_Data();

            //Plotten
            GUI.Add_Series_to_Data(this, Output_File_Name);
            GUI.Update_Voltage_Plots_for_TTA();

            //Export_as_CSV();

        }

    }
}
