using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

using System.IO;


using ATIM_GUI._6_TempPlatte;
using _8_Rth_TEC_Rack;
using ATIM_GUI._05_XYZ;
using ATIM_GUI._09_DAQ_Unit;

using TEC_Controller;

namespace ATIM_GUI._0_Classes_Measurement
{
    public class Sensitvity_Measurement
    {
        //**************************************************************************************************
        //                                           Erklärung
        //**************************************************************************************************


        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        //Devices
        public RthTEC_Rack MyRack { get; set; }
        public DAQ_Unit MySpectrum { get; set; }
        //public TEC_Meerstetter MyTEC { get; set; }
        public I_TEC_Controller MyTEC { get; set; }
        public XYZ_table MyXYZ { get; set; }

        //Main Window
        public ATIM_MainWindow GUI { get; set; }

        //Info über Speichern und verfahren
        public long Nr_of_samples { get; } = 100000;
        public decimal[,] MyMovement { get; set; }
        public string[] MyFileNames { get; set; }

        //Other variablens
        private int Counter_TempStep { get; set; }
        public List<decimal> TempSteps { get; set; } = new List<decimal>();
        public int Nr_of_LEDs { get; internal set; }

        public List<Sensitivity_DataPoint_Voltage>[] Voltage_Values { get; internal set; }

        //Indikator, that temperature progress should be plottet
        public bool IsRunning { get; internal set; } = false;

        //Datei-Namen usw.
        public string Output_File_Name { get; internal set; }
        public string Output_File_Folder { get; internal set; }

        //Results
        public short[] RawData { get; set; }

        //Timer Thread - Für Plotten
        private Sensitivity_Parallel_Thread thread_Temp_Plotting;

        public List<Sensitivity_DataPoint_Temperature> Data_Temp_at_Time { get; internal set; }
        public List<Sensitivity_DataPoint_Temperature>[] Data_MeasurementMarker { get; internal set; }

        private bool Flag_LED_Measurement { get; set; } = false;

        //**************************************************************************************************
        //                                          Konstruktor
        //**************************************************************************************************

        public Sensitvity_Measurement() { }

        //**************************************************************************************************
        //                                       Globale Funktionen
        //**************************************************************************************************

        #region GlobaleFunktionen

        public bool Start_Measurement()
        {
            //1. Pre-Settings ................................................................................
            //Anzahl LEDs für Counter
            Nr_of_LEDs = MyMovement.GetLength(0);        

            //Text StatusBar ändern
            GUI.StatusBar_Sensitivity_all(0, Nr_of_LEDs, 1, TempSteps.Count);

            //Define voltage measurement array
            Voltage_Values = new List<Sensitivity_DataPoint_Voltage>[Nr_of_LEDs];
            for (int i = 0; i < Nr_of_LEDs; i++)
                Voltage_Values[i] = new List<Sensitivity_DataPoint_Voltage>();

            //Start plotting the temperature
            IsRunning = true;
            Start_OnLine_Temp_Plotting();

            //2. Set Temperature .................................................................................

            //Set temperatures in a for loop
            for (Counter_TempStep = 0; Counter_TempStep < TempSteps.Count; Counter_TempStep++)
            {
                //Set temperature to new target value
                MyTEC.SetTemperature_w_TimerStop((float)TempSteps[Counter_TempStep]);

                //Text StatusBar ändern
                GUI.StatusBar_Sensitivity_all(0, Nr_of_LEDs, Counter_TempStep + 1, TempSteps.Count);

                //Wait 500ms, so Temperature is measured one time (avoid failure)
                System.Threading.Tasks.Task.Delay(1000);

                //Wait until temperature is stable (Cancel if temp no stable after 5 min)
                int cancel_time = 0;
                while (!GUI.myTEC.Stable_for_30sec)
                {
                    //Abfragen ob abbrechen
                    if (GUI.break_is_wished) { goto END_kFactor_all; }

                    // Eine Sekunde Warten
                    System.Threading.Thread.Sleep(1000);

                    cancel_time++;

                    if (cancel_time >= 300) //Nach 5 Minuten Warunung abbrechen
                    {
                        //Warning Message
                        MessageBox.Show("Attention: The Temperature is never stable in the range. Please control the settings of the TEC");
                        //Zum Ende springen
                        goto END_kFactor_all;
                    }
                }

                //3. Measure all DUTs..........................................................................

                //Switch On Current Source with selected current
                MyRack.MeasCurrent_on();

                //Rauffahrn (immer genau auf 0)
                MyXYZ.Move2Position(MyXYZ.Akt_x_Koordinate, MyXYZ.Akt_y_Koordinate, 0, MyXYZ.Akt_Winkel);

                //An Startpunkt fahren lassen (oberer Anschlag)
                MyXYZ.Move2Position(MyMovement[0, 0], MyMovement[0, 1], -70, 0);

                //Measure all LEDs after one other
                Flag_LED_Measurement = true;
                for (int akt_DUT_Nr = 1; akt_DUT_Nr <= Nr_of_LEDs; akt_DUT_Nr++)
                {
                    //Abfragen ob abbrechen
                    if (GUI.break_is_wished) { goto END_kFactor_all; }

                    //Text StatusBar ändern
                    GUI.StatusBar_Sensitivity_all(akt_DUT_Nr, Nr_of_LEDs, Counter_TempStep + 1, TempSteps.Count);

                    //Drive XYZ down to LED
                    MyXYZ.MoveADistance(0, 0, -25,0);

                    //Short break against starting oscillations (100ms)
                    System.Threading.Thread.Sleep(100);

                    //Measure LED and save to data array
                    MySpectrum.Measure_Sensitivity(this, GUI);

                    //Save Point
                    decimal voltage_help = (decimal)RawData.Average(x => x)  / MyRack.Cycles * MySpectrum.Range / 1000 / (decimal)Math.Pow(2, 15) + MyRack.U_offset / 1000;
                    Voltage_Values[akt_DUT_Nr - 1].Add(new Sensitivity_DataPoint_Voltage()
                    {
                        Voltage = (decimal)RawData.Average(x => x) / MyRack.Cycles * MySpectrum.Range / 1000 / (decimal)Math.Pow(2, 15) / MyRack.Gain + MyRack.U_offset / 1000,
                        Temperature = TempSteps[Counter_TempStep]
                    });

                    GUI.Update_Voltage_Plot_for_Sensitivity(this);

                    //Drive XYZ up
                    MyXYZ.MoveADistance(0, 0, 25, 0);

                    //Drive to next LED (exept at the end)
                    if (akt_DUT_Nr < Nr_of_LEDs)
                    {
                        MyXYZ.MoveADistance(MyMovement[akt_DUT_Nr, 0], MyMovement[akt_DUT_Nr, 1], 0,0);
                    }
                }

                Flag_LED_Measurement = false;


                //Switch Current source of (until next temperature is reached
                MyRack.MeasCurrent_off();

            }
            //Speicher wieder Frei geben (gegen überflutung Arbeitsspeicher)
            //hBufferHandle.Free();

            //4. Daten auswerten..........................................................................
            Sensitivity_Calculation();

            //Fertig (Trotzdem müssen Grundeinstellungen wieder hergestellt werden)
            goto END_kFactor_all;

            END_kFactor_all:

            //set start temperature to TEC controller
            MyTEC.SetTemperature_w_TimerStop((float)TempSteps[0]);

            //stop Plotting
            IsRunning = false;
            Stop_OnLine_Temp_Plotting();

            //Zu Parkposition fahren
            //GUI.xyZ_table1.Move2Position(xyz_park_pos[0], xyz_park_pos[1], xyz_park_pos[2]);

            //Wenn abgebrochen wurde dann false zurückgeben
            if (GUI.break_is_wished)
            {
                GUI.break_is_wished = false;// zurücksetzen
                //Text StatusBar ändern
                GUI.StatusBar_Measurement_CANCELED();
                return false;
            }
            //Text StatusBar ändern
            GUI.StatusBar_Measurement_FINISHED();
            //sonst true
            return true;
        }

        public bool Start_Measurement_DEMO()
        {
            //1. Pre-Settings ................................................................................

            //Collect all neccessary data
            //decimal[,] my_xy_driving_info = xy_driving_info;                       //Fahrwege
            Nr_of_LEDs = 10;        // nummerierung.Length;        //Anzahl LEDs 

            //Text StatusBar ändern
            GUI.StatusBar_Sensitivity_all(0, Nr_of_LEDs, 1, TempSteps.Count);

            //Define voltage measurement array
            Voltage_Values = new List<Sensitivity_DataPoint_Voltage>[Nr_of_LEDs];
            for (int i = 0; i < Nr_of_LEDs; i++)
                Voltage_Values[i] = new List<Sensitivity_DataPoint_Voltage>();

            //Start plotting the temperature
            IsRunning = true;
            Start_OnLine_Temp_Plotting();    

            //2. Set Temperature .................................................................................

            //Set temperatures in a for loop
            for (Counter_TempStep = 0; Counter_TempStep < TempSteps.Count; Counter_TempStep++)
            {
                //Set temperature to new target value
                GUI.myTEC.SetTemperature_w_TimerStop((float)TempSteps[Counter_TempStep]);

                //Text StatusBar ändern
                GUI.StatusBar_Sensitivity_all(0, Nr_of_LEDs, Counter_TempStep + 1, TempSteps.Count);

                //Wait 500ms, so Temperature is measured one time (avoid failure)
                System.Threading.Tasks.Task.Delay(1000);

                //Wait until temperature is stable (Cancel if temp no stable after 5 min)
                int cancel_time = 0;
                while (!GUI.myTEC.Stable_for_30sec)
                {
                    //Abfragen ob abbrechen
                    if (GUI.break_is_wished) { goto END_kFactor_all; }

                    // Eine Sekunde Warten
                    System.Threading.Thread.Sleep(1000);

                    cancel_time++;

                    if (cancel_time >= 300) //Nach 5 Minuten Warunung abbrechen
                    {
                        //Warning Message
                        MessageBox.Show("Attention: The Temperature is never stable in the range. Please control the settings of the TEC");
                        //Zum Ende springen
                        goto END_kFactor_all;
                    }
                }

                //3. Measure all DUTs..........................................................................

                //Switch On Current Source with selected current
                //GUI.rthTEC_Rack1.MeasCurrent_on();

                //Rauffahrn (immer genau auf 0)
                //GUI.xyZ_table1.Move2Position(xyz_Tisch.akt_x_Koordinate, xyz_Tisch.akt_y_Koordinate, 0);

                //An Startpunkt fahren lassen (oberer Anschlag)
                //GUI.xyZ_table1.Move2Position(myVerfahrweg[0, 0], myVerfahrweg[0, 1], 0);

                //Measure all LEDs after one other
                Flag_LED_Measurement = true;
                for (int akt_DUT_Nr = 1; akt_DUT_Nr <= Nr_of_LEDs; akt_DUT_Nr++)
                {
                    //Abfragen ob abbrechen
                    if (GUI.break_is_wished) { goto END_kFactor_all; }

                    //Text StatusBar ändern
                    GUI.StatusBar_Sensitivity_all(akt_DUT_Nr, Nr_of_LEDs, Counter_TempStep + 1, TempSteps.Count);

                    //Drive XYZ down to LED
                    //GUI.xyZ_table1.MoveADistance(0, 0, xyz_Tisch.anfahrt_z);

                    //Short break against starting oscillations (50ms)
                    System.Threading.Thread.Sleep(1000);

                    //Measure LED and save to data array
                    Voltage_Values[akt_DUT_Nr - 1].Add( new Sensitivity_DataPoint_Voltage()
                    {
                        Voltage = 3 - 0.1m*(akt_DUT_Nr) - 0.2m*(Counter_TempStep), // K_Factor_GetVoltageAverage();
                        Temperature = TempSteps[Counter_TempStep]
                    });

                    //add Data to Series (Generate Name --> Add to Series --> Update plat)
                    //string name = "LED Nr. " + (akt_DUT_Nr).ToString("#0");
                    //AsyncChart_kFactor_Voltage(name, my_kFactor_Measurement.tempSteps[akt_Temp_Nr], my_kFactor_Measurement.voltageValues[akt_DUT_Nr - 1, akt_Temp_Nr]);
                    GUI.Update_Voltage_Plot_for_Sensitivity(this);

                    //Drive XYZ up
                    //GUI.xyZ_table1.MoveADistance(0, 0, -xyz_Tisch.anfahrt_z);

                    //Pause für Demo
                    System.Threading.Tasks.Task.Delay(1000);

                    //Drive to next LED (exept at the end)
                    if (akt_DUT_Nr < Nr_of_LEDs)
                    {
                        //GUI.xyZ_table1.MoveADistance(my_xy_driving_info[akt_DUT_Nr, 0], my_xy_driving_info[akt_DUT_Nr, 1], 0);
                    }
                }

                Flag_LED_Measurement = false;


                //Switch Current source of (until next temperature is reached
                //GUI.rthTEC_Rack1.MeasCurrent_off();

            }
            //Speicher wieder Frei geben (gegen überflutung Arbeitsspeicher)
            //hBufferHandle.Free();

            //4. Daten auswerten..........................................................................
            Sensitivity_Calculation();

            //Fertig (Trotzdem müssen Grundeinstellungen wieder hergestellt werden)
            goto END_kFactor_all;

            END_kFactor_all:

            //set start temperature to TEC controller
            GUI.myTEC.SetTemperature_w_TimerStop((float)TempSteps[0]);

            //stop Plotting
            IsRunning = false;
            Stop_OnLine_Temp_Plotting();

            //Zu Parkposition fahren
            //GUI.xyZ_table1.Move2Position(xyz_park_pos[0], xyz_park_pos[1], xyz_park_pos[2]);

            //Wenn abgebrochen wurde dann false zurückgeben
            if (GUI.break_is_wished)
            {
                GUI.break_is_wished = false;// zurücksetzen
                //Text StatusBar ändern
                GUI.StatusBar_Measurement_CANCELED();
                return false;
            }
            //Text StatusBar ändern
            GUI.StatusBar_Measurement_FINISHED();
            //sonst true
            return true;
        }

        #endregion GlobaleFunktionen

        //**************************************************************************************************
        //                                       Lokale Funktionen
        //**************************************************************************************************

        #region LokaleFunktionen

        private void Sensitivity_Calculation()
        {
            //Output-File Name zwischenspeichern
            string file_Name_at_Start = Output_File_Name;
            int counter = 1;

            //Nacheinander die Daten auswerten und abspeichern
            foreach(var DataSet in Voltage_Values)
            {
                //Decimal --> Double        (für Regression, usw.)
                double[] temporary_TempSteps = new double[DataSet.Count];
                double[] temporary_Voltage = new double[DataSet.Count];
                for (int i = 0; i < TempSteps.Count; i++)
                {
                    temporary_TempSteps[i] = Convert.ToDouble(DataSet[i].Temperature);
                    temporary_Voltage[i] = Convert.ToDouble(DataSet[i].Voltage);
                }

                //Sensitivity und Regression berechnen
                LinearRegression(temporary_TempSteps, temporary_Voltage, 0, DataSet.Count, out double rsquared, out double offset, out double gradient);

                //Output-File-Name anpassen
                Output_File_Name = Replace_DUT_Nr_in_String(file_Name_at_Start, counter++);

                //Speichern
                Save(DataSet, Math.Round(gradient, 4), rsquared);

            }                        
        }

        private static void LinearRegression(double[] xVals, double[] yVals, int inclusiveStart, int exclusiveEnd, out double rsquared, out double yintercept, out double slope)
        {
            // <summary>
            // Fits a line to a collection of (x,y) points.
            // </summary>
            // <param name="xVals">The x-axis values.</param>
            // <param name="yVals">The y-axis values.</param>
            // <param name="inclusiveStart">The inclusive inclusiveStart index.</param>
            // <param name="exclusiveEnd">The exclusive exclusiveEnd index.</param>
            // <param name="rsquared">The r^2 value of the line.</param>
            // <param name="yintercept">The y-intercept value of the line (i.e. y = ax + b, yintercept is b).</param>
            // <param name="slope">The slop of the line (i.e. y = ax + b, slope is a).</param>
            //
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double ssX = 0;
            double ssY = 0;
            double sumCodeviates = 0;
            double sCo = 0;
            double count = exclusiveEnd - inclusiveStart;

            for (int ctr = inclusiveStart; ctr < exclusiveEnd; ctr++)
            {
                double x = xVals[ctr];
                double y = yVals[ctr];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            double RNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            double RDenom = (count * sumOfXSq - (sumOfX * sumOfX))
             * (count * sumOfYSq - (sumOfY * sumOfY));
            sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            double meanX = sumOfX / count;
            double meanY = sumOfY / count;
            double dblR = RNumerator / Math.Sqrt(RDenom);
            rsquared = dblR * dblR;
            yintercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }

        private string Replace_DUT_Nr_in_String(string startString, int number)
        {
            //%L[n] durch zahl ersetzen
            int place = startString.IndexOf("%R");                              //Stelle von %R

            /*alt
            int digets = (int)Char.GetNumericValue(startString[place + 2]);     //Wert bestimmen
            string format = "";                                                 //Format
            for (int i = 0; i < digets; i++)
                format += "0";
             */

            return startString.Substring(0, place-1) + MyFileNames[number - 1] + startString.Substring(place + 1);
        }

        private String KFactor_Header(double sensitivity, double rsquared)
        {
            double k_factor = 1 / sensitivity;

            //Speicherstring erzeugen
            string header = "";

            //Erste Zeile: ############################ (\r\n enspricht neue zeile)
            header = "###############################################\r\n";

            //Zweite Zeile: Zeitstempel
            header += "#Time Stamp:     " + DateTime.Now.ToString("dd.MM.yyyy   HH:mm:ss") + "\r\n";

            //Dritte Zeile: Messgeräte info
            header += "#Equipment:      Heller_V2_0 + Spectrum_V1_0\r\n";

            //Vierte bis Zeile Parameter-Info
            header += "#I_meas_current: " + MyRack.I_Meas.ToString() + " mA\r\n";
            header += "#Sensitivity:    " + sensitivity + " mV/K\r\n";
            header += "#K-factor:       " + Math.Round(k_factor, 4) + " K/mV\r\n";
            header += "#Quality (R2):   " + Math.Round(rsquared, 6) + "\r\n";

            //Letzte Zeile ###############################
            header += "###############################################";

            return header;
        }

        private void Save(List<Sensitivity_DataPoint_Voltage> data, double gradient, double rsquared)
        {
            //Bei Error --> File-Name anpassen
            if (gradient == 0)
                Output_File_Name +=  "_error";
            else if (rsquared < 0.99)
                Output_File_Name +=  "_R2low";

            //Datei Typ ist .txt für k-factor Files
            Output_File_Name += ".sen";

            // Neues Stream - Write erzeugen (false bedeutet, das die Datei überschrieben wird)
            using (StreamWriter kfactorFile = new StreamWriter(Output_File_Folder + @"\" + Output_File_Name, false))
            {
                //String definieren und mit Header füllen
                String ausgabesatz = KFactor_Header(gradient, rsquared);
                kfactorFile.WriteLine(ausgabesatz);

                //Header der Daten
                kfactorFile.WriteLine("Temp [°C]" + "\t" + "Voltage [mV]");

                //String schreiben
                for (int i = 0; i < data.Count; i++)
                {
                    //Satz definieren mit Tab und definierter 0-Stellen Anzahl
                    //0.000000  0.000000 (Erst Zeit, dann Spannung)
                    ausgabesatz = (data[i].Temperature.ToString("0.000000") + "\t" + Math.Round(data[i].Voltage, 6).ToString("0.000000"));

                    kfactorFile.WriteLine(ausgabesatz);
                }
                kfactorFile.Close();
            }
        }

        #endregion LokaleFunktionen

        //**************************************************************************************************
        //                                          Timer
        //**************************************************************************************************

        #region Timer

        private void Start_OnLine_Temp_Plotting()
        {
            //Liste für Temperatur-Werte leeren
            Data_Temp_at_Time = new List<Sensitivity_DataPoint_Temperature>();

            //Liste für Makierung Messung leeren und neu definieren
            Data_MeasurementMarker = new List<Sensitivity_DataPoint_Temperature>[TempSteps.Count];
            for(int i = 0; i < TempSteps.Count; i++)
                Data_MeasurementMarker[i] = new List<Sensitivity_DataPoint_Temperature>();

            //Alle Serien zuweisen
            GUI.Add_Series_to_Data(this);

            //Threath neu erstellen
            thread_Temp_Plotting = new Sensitivity_Parallel_Thread();
            //Event festlegen
            thread_Temp_Plotting.Event_OnParallelThread += new OnParaThreadHandler(Plot_Temp_every_SEC);
            //Timer Starten
            thread_Temp_Plotting.timer_1sec.Start();          
        }

        private void Stop_OnLine_Temp_Plotting()
        {
            //Timer Stoppen
            thread_Temp_Plotting.timer_1sec.Stop();
        }

        private void Plot_Temp_every_SEC()
        {
            //Neuen DatenPunkt mit aktueller Temperature und Zeit anhängen
            //Zeit enspricht Count, da Timer in Sekunden
            Data_Temp_at_Time.Add(new Sensitivity_DataPoint_Temperature()
                                    {
                                        Temperature = MyTEC.Meas_temp_aver,
                                        Time_in_s = Data_Temp_at_Time.Count,
                                    });

            //Wenn aktuell LED Messungen durchgeführt werden dann auch Markierung zeichnen
            if (Flag_LED_Measurement)
            {
                Data_MeasurementMarker[Counter_TempStep].Add(new Sensitivity_DataPoint_Temperature()
                {
                    Temperature = 1000,
                    Time_in_s = Data_Temp_at_Time.Count-1,
                });
            }

            //Daten updaten
            GUI.Update_Temperature_Plot_for_Sensitivity(this);
        }

        #endregion Timer
    }
}

