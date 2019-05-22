using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using Spcm;

using _8_Rth_TEC_Rack;

using DevExpress.XtraCharts;

using ATIM_GUI._0_Classes_Measurement;
using ATIM_GUI._2_AutoConnect;

namespace ATIM_GUI._09_DAQ_Unit
{
    public partial class Spectrum : DAQ_Unit
    {
        /**********************************************************************************************************************
        * Unterklasse Für Spectrum 
        * Schmid Maximlian
        * 25.10.2017
        * V1_0 (funkionsfähig)
        * 
        * -Enthält die Komminikationsbefehle
        *********************************************************************************************************************/

        //********************************************************************************************************************
        //                                    Variablen nur für Spectrum
        //********************************************************************************************************************

        //Pointer und Händler für Datenfeld & Device
        public IntPtr Daten_Pointer { get; internal set; }
        public IntPtr H_Device { get; internal set; }                     //Pointer/Händler für Spectrum digitizerNETBOX
        public GCHandle H_BufferHandle { get; internal set; }

        //Error-Code
        public uint ErrorCode_Spectrum { get; internal set; } = 0;


        //********************************************************************************************************************
        //                                                 Konstruktoren
        //********************************************************************************************************************

        public Spectrum()
        {
            //GUI aus Main-Klasse initialisieren
            Init_GUI();

            //Name in GroupBox ändern
            groupBox_DAQ.Text = "Spectrum 10MHz";

            //COM-PORT DropDown verstecken
            ComPort_select.Visible = false;
            ComPort_select.Enabled = false;

            //ComboBoxen initialisieren
            Init_ComboBox_Frequency();
            Init_ComboBox_Range();
        }

        //********************************************************************************************************************
        //                                           Initialisation ComboBoxen
        //********************************************************************************************************************

        private void Init_ComboBox_Range()
        {
            comboBox_Range.Items.AddRange(new String[]
            {
                "+/- 200mV",
                "+/- 500mV",
                "+/- 1V",
                "+/- 2V",
                "+/- 5V",
                "+/- 10V"
            });
            comboBox_Range.Text = "+/- 1V";
        }

        private void Init_ComboBox_Frequency()
        {
            comboBox_Frequency.Items.AddRange(new String[]
            {
                "10MHz",
                "5MHz",
                "2MHz",
                "1MHz"
            });
            comboBox_Frequency.Text = "10MHz";
        }

        //********************************************************************************************************************
        //                                         GUI: Open/Close + ComboBoxes
        //********************************************************************************************************************

        #region Open/Close

        public override void Open()
        {
            //Öffnen
            Communication_LOG += "Try to connect SPECTRUM:\n";

            //Öffnen   
            try
            {
                H_Device = Drv.spcm_hOpen(VISA_or_Channel_Name);
            }
            catch (System.DllNotFoundException)
            {
                MessageBox.Show("Conection to Spectrum DAQ was NOT found!\nTry again.", "Warning",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Communication_LOG += "FAILED!\n";
                return;
            }

            //Prüfen ob verbindung erfolgreich war, sonst abbrechen
            if ((long)H_Device == 0)
            {
                MessageBox.Show("Conection to Spectrum DAQ was NOT seccessful!\nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Communication_LOG += "FAILED!\n";
                return;
            }
            Communication_LOG += "Seccessful\n";

            //Reseten
            ErrorCode_Spectrum += Reset();

            //Ab hier verbunden
            IsConnected = true;

            //UI anpassen
            button_OpenClose.Text = "Close";
            comboBox_Frequency.Enabled = true;
            comboBox_Range.Enabled = true;
            numericUpDown_Trigger.Enabled = true;
        }

        public override void Close()
        {
            //Schließen
            Drv.spcm_vClose(H_Device);
            Communication_LOG += "Connenction canceled\n";

            //Ab hier verbunden
            IsConnected = false;

            //UI anpassen
            button_OpenClose.Text = "Open";
            comboBox_Frequency.Enabled = false;
            comboBox_Range.Enabled = false;
            numericUpDown_Trigger.Enabled = false;
        }

        #endregion Open/Close

        #region AutoOpen

        public override string AutoOpen(Load_Screen myLoadScreen)
        {
            int iterration = 7;

            Communication_LOG += "Try to connect SPECTRUM:\n";

            //Öffnen
            try
            {
                H_Device = Drv.spcm_hOpen(VISA_or_Channel_Name);
            }
            catch (System.DllNotFoundException)
            {
                Communication_LOG += "FAILED!\n";
                return "DAQ-Unit: Connection was not found!" + Environment.NewLine;
            }

            //Prüfen ob verbindung erfolgreich war, sonst abbrechen
            if ((long)H_Device == 0)
            {
                Communication_LOG += "FAILED!\n";
                return "DAQ-Unit: Connection was not seccessfull!" + Environment.NewLine;
            }
            Communication_LOG += "Seccessful\n";

            myLoadScreen.ChangeTask("Reset ...", iterration);

            //Reseten
            ErrorCode_Spectrum += Reset();

            myLoadScreen.ChangeTask("Change UI ...", iterration);

            //Ab hier verbunden
            IsConnected = true;

            //UI anpassen
            button_OpenClose.Text = "Close";
            comboBox_Frequency.Enabled = true;
            comboBox_Range.Enabled = true;
            numericUpDown_Trigger.Enabled = true;

            return "";
        }

        #endregion AutoOpen

        #region ComboBox

        internal override void ComboBox_Range_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Range.Text)
            {
                case "+/- 200mV":
                    Range = 200;
                    break;
                case "+/- 500mV":
                    Range = 500;
                    break;
                case "+/- 1V":
                    Range = 1000;
                    break;
                case "+/- 2V":
                    Range = 2000;
                    break;
                case "+/- 5V":
                    Range = 5000;
                    break;
                case "+/- 10V":
                    Range = 10000;
                    break;
            }
        }

        internal override void ComboBox_Frequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Frequency.Text)
            {
                case "10MHz":
                    Frequency = 10000000;
                    break;
                case "5MHz":
                    Frequency = 5000000;
                    break;
                case "2MHz":
                    Frequency = 2000000;
                    break;
                case "1MHz":
                    Frequency = 1000000;
                    break;
            }
        }

        #endregion ComboBox


        //********************************************************************************************************************
        //                                            Globale Messbefehle
        //********************************************************************************************************************

        #region HauptBefehle

        /// <summary>
        /// Retruns true if no error occured while sending the settings
        /// </summary> 
        public override bool Setting_for_TTA()
        {
            //Mögliche Errors mitzählen
            uint error_Sum = 0;

            //Notwendige Settings
            error_Sum += Activate_Chanel_0();
            error_Sum += Diff_Channel_0to1();
            error_Sum += Input_1MOhm_Channel_0();
            error_Sum += Input_1MOhm_Channel_1();
            error_Sum += Set_Range();
            error_Sum += Set_Frequency();
            error_Sum += Setting_for_Single_Measurment();

            //Auf Errors checken
            if (error_Sum != 0)
            {
                MessageBox.Show("An error occured while sending the Settings!\n Look on LOG and try again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Retruns true if no error occured while sending the settings
        /// </summary> 
        public override bool Setting_Trigger(RthTEC_Rack myRackSettings)
        {
            //Mögliche Errors mitzählen
            uint error_Sum = 0;

            //TriggerLevel berechnen (Ziel-Level - OffsetFrontEnd)*2*Bits/(2*Range)
            Trigger_Level_UI = (long)((numericUpDown_Trigger.Value * 1000 - myRackSettings.U_offset) * myRackSettings.Gain * 8191 / (2 * Range));

            //Notwendige Settings
            error_Sum += Trigger_1();
            error_Sum += Trigger_2();
            error_Sum += Trigger_3();
            error_Sum += Trigger_Rising();
            error_Sum += Trigger_Level(Trigger_Level_UI);

            //Auf Errors checken
            if (error_Sum != 0)
            {
                MessageBox.Show("An error occured while sending the Settings!\n Look on LOG and try again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Retruns true if no error occured while sending the settings
        /// </summary> 
        public override bool Measure_TTA_Several_Cycles(TTA_measurement myTTA, ATIM_MainWindow GUI)
        {
            //StatusBar anpassen
            GUI.StatusBar_TTA_Single(0, (int)myTTA.MyRack.Cycles);

            //Mögliche Errors mitzählen
            uint error_Sum = 0;

            //1. Parameter für Messlänge senden********************************************************************************

            //Sample Anzahl berechnen (Heizplus + Messpuls + Puffer[alle in ms]) * Frequenz 
            long samples = Decimal.ToInt64((myTTA.MyRack.Time_Heat + myTTA.MyRack.Time_Meas + 10m) / 1000 * Frequency);
            //Sampels nach Trigger berechnen (100µs davor)
            long postTriggerSamples = samples - Decimal.ToInt64(0.0001m * Frequency);

            error_Sum += Set_Sample_Count(samples);
            error_Sum += Set_Samples_PostTrigger(postTriggerSamples);


            //2. Feld für die Messdaten generiern******************************************************************************
            //Pointer und Händler für Datenfeld
            IntPtr daten_Pointer;
            GCHandle hBufferHandle;

            //Zugehöriges Feld in Klasse TTA measurement erzeugen
            myTTA.Creat_RowDataField(samples);

            //Speicherplatz sperren
            hBufferHandle = GCHandle.Alloc(myTTA.Binary_Raw_Files, GCHandleType.Pinned);
            //Pointer für gesperten Speicherplatz suchen            
            daten_Pointer = hBufferHandle.AddrOfPinnedObject();


            //Init Stability Check (weiß nicht wozu)
            //double stability_limit = 1; //1%
            //bool[] no_stability = new bool[3];
            //int[,] values_for_stability_check = new int[3, myTTA_Measurement.param.anzahl_Zyklen];

            //3. Messung starten************************************************************************************************
            for (int i = 0; i < myTTA.MyRack.Cycles; i++)
            {
                //Falls über Cancel abgebrochen wurde --> Speicher freigeben
                if (GUI.myBackroundWorker.CancellationPending)
                {
                    hBufferHandle.Free();
                    return false;
                }


                error_Sum += WaitTime_for_Trigger_inf();
                error_Sum += Adopt_Settings();
                error_Sum += Start_Card();
                error_Sum += Wait_to_fill_PreTrigger();
                error_Sum += Enable_Trigger();
                error_Sum += Force_Trigger_after_time((long)myTTA.MyRack.Time_Heat);

                //Zählvariablen in Button hochzählen
                GUI.StatusBar_TTA_Single(i + 1, (int)myTTA.MyRack.Cycles);


                //Stromquelle Pulsen
                myTTA.MyRack.SinglePuls_withoutDelay();

                //Wurde ein Trigger Event gefunden (wenn nich Rückgabewert ist 263)
                if (Did_Trigger_Event_occured() == 263)
                {
                    //Fehlermeldung
                    //SetAsyncText(Spectrum_answer, "Kein Trigger gefunden");

                    error_Sum += 263;
                    error_Sum += Force_Trigger();
                }


                //4. Daten abholen**********************************************************************************************

                //WArten bis alle Daten vorhanden sind (unendlich lang möglich)
                error_Sum += WaitTime_for_Trigger_inf();
                error_Sum += Wait_for_all_samples();

                //Datentransfer einstellen (Datapointer enspricht der Stelle des ersten Samples im Array)
                //Bei weiteren Zyklen muss dieser Pointer geändert werden
                //DataPointer is [0,0]
                //anzahl_samples entspricht der Länge einer Zeile
                //2 ist notwendig für short (2byte)
                //i ist die aktuelle Zeile (Zyklus)

                IntPtr aktuellerPointer = new IntPtr(daten_Pointer.ToInt64() + 2 * i * samples);

                //Daten für Demo erzeugen
                /*
                 * 
                 *                 Random rnd = new Random();
                int j = 0;
                int help_t = 0;
                for (; j < 1000; j++)
                    myTTA.Spectrum_raw_files[i, j] = -32767;
                for (; j < samples / 2; j++)
                {
                    help_t++;
                    myTTA.Spectrum_raw_files[i, j] = (short)(10000 + 15000 * Math.Exp(-(double)help_t / 1500000) + rnd.Next(-100, 100));
                }
                help_t = 0;
                for (; j < samples - 1000; j++)
                {
                    help_t++;
                    myTTA.Spectrum_raw_files[i, j] = (short)(-10000 - 15000 * Math.Exp(-(double)help_t / 1500000) + rnd.Next(-100, 100));
                }
                for (; j < samples; j++)
                    myTTA.Spectrum_raw_files[i, j] = -32767;
                */
                error_Sum += Send_Pointer_of_Array(aktuellerPointer, samples);
                error_Sum += Get_Data();


                //5. Auf Fehler prüfen*****************************************************************************************
                /*
                 * (Text in Antwortfenster ändern
                if (error_Sum == 0) { SetAsyncText(Spectrum_answer, "Measurement correct\r\n"); }
                else { SetAsyncText(Spectrum_answer, "Error while measurement\r\n"); }
                */

                //Gleich in Graph ausgeben
                GUI.Add_Series_to_RAW(myTTA, i);

                myTTA.ErrorCode = ErrorCheck_ShortOpen(myTTA, i);
                //Wenn Short oder Open --> Abbruch (keine weiteren Zyklen)
                if (myTTA.ErrorCode != 0)
                    break;

            }

            //Stabilitäts-Check
            //if (myTTA.ErrorCode == 0)
            //  myTTA.ErrorCode = ErrorCheck_Instabil(myTTA);

            hBufferHandle.Free();


            //Auf Errors checken
            if (error_Sum != 0)
            {
                MessageBox.Show("An Error occured while measurment!\n Error Code: ." + myTTA.ErrorCode.ToString(), "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Retruns true if no error occured while sending the settings
        /// </summary> 
        public override bool Measure_TTA_Several_Cycles_DEMO(TTA_measurement myTTA, ATIM_MainWindow GUI)
        {
            //StatusBar anpassen
            GUI.StatusBar_TTA_Single(0, (int)myTTA.MyRack.Cycles);

            //Plot für Single Curves initialsisiern
            //AsyncChart_single_Vf_init();

            //Mögliche Errors mitzählen
            uint error_Sum = 0;

            //1. Parameter für Messlänge senden********************************************************************************

            //Sample Anzahl berechnen (Heizplus + Messpuls + Puffer[alle in ms]) * Frequenz 
            long samples = Decimal.ToInt64((myTTA.MyRack.Time_Heat + myTTA.MyRack.Time_Meas + 0.5m) / 1000 * Frequency);
            //Sampels nach Trigger berechnen (100µs davor)
            long postTriggerSamples = samples - Decimal.ToInt64(0.0001m * Frequency);

            //error_Sum += Set_Sample_Count(samples);
            //error_Sum += Set_Samples_PostTrigger(postTriggerSamples);


            //2. Feld für die Messdaten generiern******************************************************************************
            //Pointer und Händler für Datenfeld
            IntPtr daten_Pointer;
            GCHandle hBufferHandle;

            //Zugehöriges Feld in Klasse TTA measurement erzeugen
            myTTA.Creat_RowDataField(samples);

            //Speicherplatz sperren
            hBufferHandle = GCHandle.Alloc(myTTA.Binary_Raw_Files, GCHandleType.Pinned);
            //Pointer für gesperten Speicherplatz suchen            
            daten_Pointer = hBufferHandle.AddrOfPinnedObject();


            //Init Stability Check (weiß nicht wozu)
            //double stability_limit = 1; //1%
            //bool[] no_stability = new bool[3];
            //int[,] values_for_stability_check = new int[3, myTTA_Measurement.param.anzahl_Zyklen];

            //3. Messung starten************************************************************************************************
            for (int i = 0; i < myTTA.MyRack.Cycles; i++)
            {
                //Falls über Cancel abgebrochen wurde --> Speicher freigeben

                if (GUI.myBackroundWorker.CancellationPending)
                {
                    hBufferHandle.Free();
                    return false;
                }


                //error_Sum += WaitTime_for_Trigger_inf();
                //error_Sum += Adopt_Settings();
                //error_Sum += Start_Card();
                //error_Sum += Wait_to_fill_PreTrigger();
                //error_Sum += Enable_Trigger();
                //error_Sum += Force_Trigger_after_time((long)myTTA.MyRack.Time_Heat);

                //Zählvariablen in Button hochzählen
                GUI.StatusBar_TTA_Single(i + 1, (int)myTTA.MyRack.Cycles);

                //System.Threading.Thread.Sleep((int)(myTTA.MyRack.Time_Heat + myTTA.MyRack.Time_Meas));
                System.Threading.Thread.Sleep(1000);

                //Stromquelle Pulsen
                //myTTA.MyRack.SinglePuls_withoutDelay();

                //Wurde ein Trigger Event gefunden (wenn nich Rückgabewert ist 263)
                /*if (Did_Trigger_Event_occured() == 263)
                {
                    //Fehlermeldung
                    //SetAsyncText(Spectrum_answer, "Kein Trigger gefunden");

                    error_Sum += 263;
                    error_Sum += Force_Trigger();
                }*/


                //4. Daten abholen**********************************************************************************************

                //WArten bis alle Daten vorhanden sind (unendlich lang möglich)
                //error_Sum += WaitTime_for_Trigger_inf();
                //error_Sum += Wait_for_all_samples();

                //Datentransfer einstellen (Datapointer enspricht der Stelle des ersten Samples im Array)
                //Bei weiteren Zyklen muss dieser Pointer geändert werden
                //DataPointer is [0,0]
                //anzahl_samples entspricht der Länge einer Zeile
                //2 ist notwendig für short (2byte)
                //i ist die aktuelle Zeile (Zyklus)

                IntPtr aktuellerPointer = new IntPtr(daten_Pointer.ToInt64() + 2 * i * samples);

                Random rnd = new Random();

                //Daten für Demo erzeugen
                int j = 0;
                int help_t = 0;
                for (; j < 1000; j++)
                    myTTA.Binary_Raw_Files[i, j] = -32767;
                for (; j < samples/2; j++) {
                    help_t++;
                    myTTA.Binary_Raw_Files[i, j] = (short)(10000 + 15000 * Math.Exp(-(double)help_t / 1500000) + rnd.Next(-100, 100));
                }                    
                help_t = 0;
                for (; j < samples-1000; j++)
                {
                    help_t++;
                    myTTA.Binary_Raw_Files[i, j] = (short)(-10000 - 15000 * Math.Exp(-(double)help_t / 1500000) + rnd.Next(-100, 100));
                }
                for (; j < samples; j++)
                    myTTA.Binary_Raw_Files[i, j] = -32767;

                //error_Sum += Send_Pointer_of_Array(aktuellerPointer, samples);
                //error_Sum += Get_Data();


                //5. Auf Fehler prüfen*****************************************************************************************
                /*
                 * (Text in Antwortfenster ändern
                if (error_Sum == 0) { SetAsyncText(Spectrum_answer, "Measurement correct\r\n"); }
                else { SetAsyncText(Spectrum_answer, "Error while measurement\r\n"); }
                */

                //Gleich in Graph ausgeben
                GUI.Add_Series_to_RAW(myTTA, i);

                //myTTA.ErrorCode = ErrorCheck_ShortOpen(myTTA, i);
                //Wenn Short oder Open --> Abbruch (keine weiteren Zyklen)
                //if (myTTA.ErrorCode != 0)
                //  break;

            }

            //Stabilitäts-Check
            //if (myTTA.ErrorCode == 0)
            //  myTTA.ErrorCode = ErrorCheck_Instabil(myTTA);

            hBufferHandle.Free();


            //Auf Errors checken
            if (error_Sum != 0)
            {
                MessageBox.Show("An Error occured while measurment!\n Error Code: ." + myTTA.ErrorCode.ToString(), "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Retruns true if no error occured while sending the settings
        /// </summary>      
        public override bool Setting_for_Sensitivity(Sensitvity_Measurement mySensitivity)
        {
            //1. Error Counter zurücksetzen**************************************************************************************************************
            uint error_Sum = 0;

            //2. Mess-Einstellungen anpassen*************************************************************************************************************
            error_Sum += Activate_Chanel_0();
            error_Sum += Diff_Channel_0to1();
            error_Sum += Input_1MOhm_Channel_0();
            error_Sum += Input_1MOhm_Channel_1();
            error_Sum += Set_Range();
            error_Sum += Set_Frequency();                   //Eventuell Freuenz ändern
            error_Sum += Setting_for_Single_Measurment();

            //3. Parameter für Messlänge senden**********************************************************************************************************          
            error_Sum += Set_Sample_Count(mySensitivity.Nr_of_samples);
            error_Sum += Set_Samples_PostTrigger(mySensitivity.Nr_of_samples);

            //4. Feld für die Messdaten generiern********************************************************************************************************
            mySensitivity.RawData = new short[mySensitivity.Nr_of_samples];                                 //Feld in der Länger definieren          
            H_BufferHandle = GCHandle.Alloc(mySensitivity.RawData, GCHandleType.Pinned);                    //Speicherplatz sperren            
            Daten_Pointer = H_BufferHandle.AddrOfPinnedObject();                                            //Pointer für gesperten Speicherplatz suchen

            //5. Fehlerauswertung********************************************************************************************************
            if (error_Sum != 0)
            {
                MessageBox.Show("An error occured while sending the Settings!\n Look on LOG and try again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else            
                return true;
           
        }

        /// <summary>
        /// Retruns true if no error occured while sending the settings
        /// </summary>      
        public override bool Measure_Sensitivity(Sensitvity_Measurement mySensitivity, ATIM_MainWindow GUI)
        {
            //Mögliche Errors mitzählen
            uint error_Sum = 0;

            //Messung starten
            error_Sum += WaitTime_for_Trigger_inf();
            error_Sum += Adopt_Settings();
            error_Sum += Start_Card();
            error_Sum += Force_Trigger();

            //Daten abholen
            error_Sum += WaitTime_for_Trigger_inf();
            error_Sum += Wait_for_all_samples();

            //Datentransfer einstellen (Datapointer enspricht der Stelle des ersten Samples im Array)
            IntPtr aktuellerPointer = new IntPtr(Daten_Pointer.ToInt64());

            error_Sum += Send_Pointer_of_Array(aktuellerPointer, mySensitivity.Nr_of_samples);
            error_Sum += Get_Data();

            //Plot in Graph RAW
            GUI.Add_Series_to_RAW(mySensitivity);

            //Auf Errors checken
            if (error_Sum != 0)
            {
                MessageBox.Show("An Error occured while measurment!\n Look at LOG: .", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else           
                return true;
           
        }

        /// <summary>
        /// Retruns true if no error occured while sending the settings
        /// </summary>  
        public override bool Measure_Sensitivity_DEMO(Sensitvity_Measurement mySensitivity)
        {
            //Mögliche Errors mitzählen
            uint error_Sum = 0;

            //Speicher definieren & Spreren
            mySensitivity.RawData = new short[mySensitivity.Nr_of_samples];
            H_BufferHandle = GCHandle.Alloc(mySensitivity.RawData, GCHandleType.Pinned);
            Daten_Pointer = H_BufferHandle.AddrOfPinnedObject();

            //Messung starten
            error_Sum += WaitTime_for_Trigger_inf();
            error_Sum += Adopt_Settings();
            error_Sum += Start_Card();
            error_Sum += Force_Trigger();

            //Daten abholen
            error_Sum += WaitTime_for_Trigger_inf();
            error_Sum += Wait_for_all_samples();

            //Datentransfer einstellen (Datapointer enspricht der Stelle des ersten Samples im Array)
            IntPtr aktuellerPointer = new IntPtr(Daten_Pointer.ToInt64());

            error_Sum += Send_Pointer_of_Array(aktuellerPointer, mySensitivity.Nr_of_samples);
            error_Sum += Get_Data();

            //Auf Errors checken
            if (error_Sum != 0)
            {
                MessageBox.Show("An Error occured while measurment!\n Look at LOG: .", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }

        }

        #endregion HauptBefehle

        //********************************************************************************************************************
        //                                         Error Check Functions
        //********************************************************************************************************************


        /// <summary>
        /// Retruns Error Code:
        /// 0: OK;
        /// 1: Short;
        /// 2: Open;
        /// 4: Instable;
        /// </summary> 
        private UInt16 ErrorCheck_ShortOpen(TTA_measurement myTTA, int MeasCycle)
        {
            long raw_file_mean = 0;
            int pre_points = 1001;

            //Mittelwert berechnen (Short und Open)
            for (int j = pre_points; j < myTTA.Binary_Raw_Files.GetLength(1); j++)
            {
                raw_file_mean = raw_file_mean + myTTA.Binary_Raw_Files[MeasCycle, j];
            }
            raw_file_mean = raw_file_mean / (myTTA.Binary_Raw_Files.GetLength(1) - pre_points);

            //Short (2^15 = 32768 --> mit Puffer)
            if (raw_file_mean < -32000)
                return 1;

            //Open (2^15 = 32768 --> mit Puffer)
            if (raw_file_mean > 32000)
                return 2;

            //Kein Fehler
            return 0;
        }

        private UInt16 ErrorCheck_Instabil(TTA_measurement myTTA)
        {
            //Wenn nur ein Zyklus, dann kann stabilität nich überprüft werden
            if (myTTA.MyRack.Cycles < 2)
                return 0;

            //Punkte zum definieren
            List<long> Liste_Punkte = new List<long>()
            {
                myTTA.Binary_Raw_Files.GetLength(1) - (myTTA.Binary_Raw_Files.GetLength(1) / 3),    //@ 2/3
                myTTA.Binary_Raw_Files.GetLength(1) - (myTTA.Binary_Raw_Files.GetLength(1) / 5),    //@ 4/5
                myTTA.Binary_Raw_Files.GetLength(1) - 1                                               //@ End
            };


            //Jeden Punkt definieren
            foreach (long point in Liste_Punkte)
            {
                //Feld für Werte erzeugen
                long[] values = new long[myTTA.MyRack.Cycles];

                //Werte herauslösen
                for (int cycle_nr = 0; cycle_nr < myTTA.MyRack.Cycles; cycle_nr++)
                {
                    values[cycle_nr] = myTTA.Binary_Raw_Files[cycle_nr, point];
                }

                //Auf stabilität prüfen (immer mit erstem vergleichen)
                for (int cycle_nr = 1; cycle_nr < myTTA.MyRack.Cycles; cycle_nr++)
                {
                    //Voltage = Bitwert * 2*Range / 2^16 / Gain + Offset
                    long differenc_in_mv = (values[0] - values[cycle_nr]) * Range / 2 / (long)Math.Pow(2, 15);
                    //Wenn untesschied > 5mV
                    if (Math.Abs(differenc_in_mv) > 5)
                    {
                        return 4;
                    }
                }
            }
            //Kein Fehler
            return 0;

        }


        //********************************************************************************************************************
        //                                       Lokale Spectrum Einzelbefehle
        //********************************************************************************************************************

        #region Spectrum_Commands

        private uint Reset()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_CARD_RESET);
            Communication_LOG += "RESET:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Activate_Chanel_0()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_CHENABLE, Regs.CHANNEL0);
            Communication_LOG += "Activate Channel 0:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Diff_Channel_0to1()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_DIFF0, 1);
            Communication_LOG += "Diff. Channel 0 to 1:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Input_1MOhm_Channel_0()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_50OHM0, 0);
            Communication_LOG += "Input 1MOhm Channel 0:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Input_1MOhm_Channel_1()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_50OHM1, 0);
            Communication_LOG += "Input 1MOhm Channel 1:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Set_Range()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_AMP0, Range);
            Communication_LOG += "Range to +/- " + Range.ToString() + "mV:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Set_Frequency()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_SAMPLERATE, Frequency);
            Communication_LOG += "Frequency to " + Frequency.ToString() + "Hz:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Setting_for_Single_Measurment()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_CARDMODE, Regs.SPC_REC_STD_SINGLE);
            Communication_LOG += "Set single measurement:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        //Unklar was Trigger 1 bis 3 machen
        private uint Trigger_1()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_TRIG_ORMASK, Regs.SPC_TMASK_NONE);       //aus
            Communication_LOG += "Trigger Set 1:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Trigger_2()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_TRIG_CH_ORMASK0, Regs.SPC_TMASK0_CH0);   //ein
            Communication_LOG += "Trigger Set 2:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Trigger_3()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_TRIG_CH_ORMASK1, 0);                     //aus
            Communication_LOG += "Trigger Set 3:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Trigger_Rising()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_TRIG_CH0_MODE, Regs.SPC_TM_POS);
            Communication_LOG += "Trigger Set rising:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Trigger_Level(long value)
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_TRIG_CH0_LEVEL0, value);
            Communication_LOG += "Trigger Level:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }


        private uint Set_Sample_Count(long sampleCount)
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_MEMSIZE, sampleCount);
            Communication_LOG += "Set Sample Count:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Set_Samples_PostTrigger(long postTriggerCount)
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_POSTTRIGGER, postTriggerCount);
            Communication_LOG += "Set Post Trigger Sample Count:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }


        private uint WaitTime_for_Trigger_inf()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_TIMEOUT, 0); //unendlich
            Communication_LOG += "Set Infinite Wait Time for Trigger:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Adopt_Settings()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_CARD_WRITESETUP);
            Communication_LOG += "Addopt parameter:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Start_Card()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_CARD_START);
            Communication_LOG += "Start Card:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Wait_to_fill_PreTrigger()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_CARD_WAITPREFULL);
            Communication_LOG += "Wait to Fill PreTrigger:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Enable_Trigger()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_CARD_ENABLETRIGGER);
            Communication_LOG += "Enable Trigger:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Force_Trigger_after_time(long max_wait_time_in_ms)
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_TIMEOUT, max_wait_time_in_ms);
            Communication_LOG += "Set max Wait_Time:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Did_Trigger_Event_occured()
        {
            //263 is Rückgabewert wenn kein Trigger gefunden wird
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_CARD_WAITTRIGGER);
            Communication_LOG += "Trigger found:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Force_Trigger()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_CARD_FORCETRIGGER);
            Communication_LOG += "Foruce Trigger:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Wait_for_all_samples()
        {
            uint errorCode = Drv.spcm_dwSetParam_i64(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_CARD_WAITREADY);
            Communication_LOG += "Wait for all samples:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Send_Pointer_of_Array(IntPtr pointer, long nr_of_samples)
        {
            uint errorCode = Drv.spcm_dwDefTransfer_i64(H_Device, Drv.SPCM_BUF_DATA, Drv.SPCM_DIR_CARDTOPC, 0, pointer, 0, 2 * (ulong)nr_of_samples);
            Communication_LOG += "Set Pointer for Data:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        private uint Get_Data()
        {
            uint errorCode = Drv.spcm_dwSetParam_i32(H_Device, Regs.SPC_M2CMD, Regs.M2CMD_DATA_STARTDMA | Regs.M2CMD_DATA_WAITDMA);
            Communication_LOG += "Get Data:\n" + errorCode.ToString() + "\n";
            return errorCode;
        }

        #endregion Spectrum_Commands




    }

}
