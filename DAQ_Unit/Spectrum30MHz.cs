﻿using System;
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

using AutoConnect;
using Communication_Settings;

namespace DAQ_Unit
{
    public partial class Spectrum30MHz : UserControl, I_DAQ
    {
        //********************************************************************************************************************
        //                                              Variables
        //********************************************************************************************************************

        #region Variables

        //***********************Interface-Variablen***********************************

        public Boolean IsConnected { get; internal set; } = false;

        //Mess-Parameter
        public long Range { get; set; } = 1000;
        public string[] RangeList { get; } =
            {
                "+/- 200mV",
                "+/- 500mV",
                "+/- 1V",
                "+/- 2V",
                "+/- 5V",
                "+/- 10V"
            };

        public long Frequency { get; set; } = 10000000;
        public string[] FrequencyList { get; } =
            {
                "10MHz",
                "5MHz",
                "2MHz",
                "1MHz"
            };

        public long Trigger_Level_UI { get; internal set; }

        //Log
        public string Communication_LOG { get; internal set; }

        //************************Special-Variablen*************************************

        //IP - Adresse Device
        private string IP_Adresse { get; set; } = "";

        //Pointer und Händler für Datenfeld & Device
        public IntPtr Daten_Pointer { get; internal set; }
        public IntPtr H_Device { get; internal set; }                     //Pointer/Händler für Spectrum digitizerNETBOX
        public GCHandle H_BufferHandle { get; internal set; }

        //Error-Code
        public uint ErrorCode_Spectrum { get; internal set; } = 0;

        long samples;
        long postTriggerSamples;

        #endregion Variables

        //********************************************************************************************************************
        //                                            Initialization
        //********************************************************************************************************************


        public Spectrum30MHz()
        {
            InitializeComponent();
        }

        public Spectrum30MHz(Form callingForm, int x, int y)
        {
            InitializeComponent();

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "DAQ:Spectrum30MHz";
            this.Size = new System.Drawing.Size(515, 75);
            this.TabIndex = 30;

            //Hinzufügen
            callingForm.Controls.Add(this);
        }

        //Enablen
        public void Change_Enabled(Boolean input)
        {
            groupBox1.Invoke((MethodInvoker)delegate
            {
                groupBox1.Enabled = input;
            });
        }


        //********************************************************************************************************************
        //                                            GUI-Events
        //********************************************************************************************************************

        #region GUI

        private void Button_OpenClose_Click(object sender, EventArgs e)
        {
            if (!IsConnected)
                Open();
            else
                Close();
        }

        private void BarButtonItem_Detailed_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Detailed_Window myWindow = new Detailed_Window(this);
            myWindow.Show();
        }

        private void BarButtonItem_Log_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("Not realized yet!");
        }

        private void Voltage_Trigger_ValueChanged(object sender, EventArgs e)
        {

        }

        #endregion GUI

        //********************************************************************************************************************
        //                                         Interface Funktionen
        //********************************************************************************************************************

        #region TTA

        public bool TTA_set_Device(decimal t_heat_ms, decimal t_meas_ms)
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

            //1. Parameter für Messlänge senden********************************************************************************

            //Sample Anzahl berechnen (Heizplus + Messpuls + Puffer[alle in ms]) * Frequenz 
            samples = Decimal.ToInt64((t_heat_ms + t_meas_ms + 10m) / 1000 * Frequency);
            //Sampels nach Trigger berechnen (100µs davor)
            postTriggerSamples = samples - Decimal.ToInt64(0.0001m * Frequency);

            error_Sum += Set_Sample_Count(samples);
            error_Sum += Set_Samples_PostTrigger(postTriggerSamples);

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

        public bool TTA_set_Trigger(decimal frontend_gain, decimal forntend_offset)//RthTEC_Rack myRackSettings)
        {
            //Mögliche Errors mitzählen
            uint error_Sum = 0;

            //TriggerLevel berechnen (Ziel-Level - OffsetFrontEnd)*2*Bits/(2*Range)
            Trigger_Level_UI = (long)((Voltage_Trigger.Value * 1000 - forntend_offset) * frontend_gain * 8191 / (2 * Range));

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

        public bool TTA_wait_for_Trigger()
        {
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



            return false;
        }

        public bool TTA_Collect_Data(short[,] data_out, int cycle)
        {
            return false;
        }

        /*
        private void TTA_auslagerung()
        {
            //StatusBar anpassen
            GUI.StatusBar_TTA_Single(0, (int)myTTA.MyRack.Cycles);

            Setting_for_TTA(1,1);
            Setting_Trigger(1,1);

            //Feld für Daten definieren (mit 100x max- beginnen)
            myTTA.Creat_RowDataField(100 + anzahl_samples + sample_ueberschuss);

            //3. Loop**********************************************************************************************************
            for (int i = 0; i < myTTA.MyRack.Cycles; i++)
            {
                TTA_wait_for_Trigger();

                //5. Puls starten**********************************************************************************************              
                System.Threading.Thread.Sleep(300);
                myTTA.MyRack.SinglePuls_withDelay();
                System.Threading.Thread.Sleep(1000);

                TTA_Collect_Data(myTTA.RawData, i);

                //Gleich in Graph ausgeben
                GUI.Add_Series_to_RAW(myTTA, i);

                //7. StatusBar anpassen****************************************************************************************
                GUI.StatusBar_TTA_Single(i + 1, (int)myTTA.MyRack.Cycles);
            }
        }
        */

        #endregion TTA

        #region Sensitivity

        public bool Sensitivity_Set_Device()
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
        public bool Sensitivity_Set_Trigger()
        {
            //Nothing to do here
            //Task is done in "Sensitivity_Measure_and_Collect_Data"
            return true;
        }
        public bool Sensitivity_Measure_and_Collect_Data(short[] output)
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

        #endregion Sensitivity

        //********************************************************************************************************************
        //                                         Lokale Functionen
        //********************************************************************************************************************

        #region lokal
  
        private void Open()
        {
            //Öffnen
            Communication_LOG += "Try to connect SPECTRUM:\n";

            //Öffnen   
            IP_Adresse = textBox_IP.Text;
            try
            {
                H_Device = Drv.spcm_hOpen(IP_Adresse);
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
            Voltage_Trigger.Enabled = true;
        }

        private void Close()
        {
            //Schließen
            Drv.spcm_vClose(H_Device);
            Communication_LOG += "Connenction canceled\n";

            //Ab hier verbunden
            IsConnected = false;

            //UI anpassen
            button_OpenClose.Text = "Open";
            Voltage_Trigger.Enabled = false;
        }

        #endregion lokal

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

        //********************************************************************************************************************
        //                                           AutoConnect
        //********************************************************************************************************************

        #region AutoConnect

        public void Update_settings(NI_CommunicationDevice myInput)
        {
            //Not used but neccessary for Interface
        }

        public void Update_settings(EthernetCommunicationDevice myInput)
        {
            textBox_IP.Text = myInput.textBox_IP.Text;
        }

        public string AutoOpen(AutoConnect_Window myLoadScreen)
        {
            int iterration = 7;

            Communication_LOG += "Try to connect SPECTRUM:\n";

            //Öffnen
            IP_Adresse = textBox_IP.Text;
            try
            {
                H_Device = Drv.spcm_hOpen(IP_Adresse);
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
            Voltage_Trigger.Enabled = true;

            return "";
        }

        #endregion AutoConnect


    }
}