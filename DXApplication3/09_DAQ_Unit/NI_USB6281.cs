using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;              //Serielle Schnittstelle

using NationalInstruments.DAQmx;            //Eigeneintrag für NI-Karte
using NationalInstruments;

using _8_Rth_TEC_Rack;

using DevExpress.XtraCharts;

using ATIM_GUI._0_Classes_Measurement;
using AutoConnect;

namespace ATIM_GUI._09_DAQ_Unit
{
    public partial class NI_USB6281 : DAQ_Unit
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
        //                                      Variablen nur für NI_USB6281
        //********************************************************************************************************************

        //Mess-Aufgabe
        private NationalInstruments.DAQmx.Task myTask_NI;

        //********************************************************************************************************************
        //                                          Konstruktoren
        //********************************************************************************************************************

        public NI_USB6281()
        {
            //GUI aus Main-Klasse initialisieren
            Init_GUI();

            //Name in GroupBox ändern
            groupBox_DAQ.Text = "NI_USB6281";          

            //ComboBoxen initialisieren
            Init_ComboBox_Frequency();
            Init_ComboBox_Range();
            Init_ComboBox_Channels();
        }

        //********************************************************************************************************************
        //                                      Initialisation ComboBoxen
        //********************************************************************************************************************

        private void Init_ComboBox_Range()
        {
            comboBox_Range.Items.AddRange(new String[]
            {
                "+/- 100mV",
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
                "500kHz",
                "200kHz",
                "100kHz",
            });
            comboBox_Frequency.Text = "500kHz";
        }

        private void Init_ComboBox_Channels()
        {
            try
            {
                //Alle Kanäle, aller NI-Karten finden
                ComPort_select.Items.AddRange(DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External));
                //Ziel: Dev1/ai3 zu finden (steht normalerweise an Indexstelle Nr. 3 und wird standardmäßig ausgewählt)
                if (ComPort_select.Items.Count > 0)
                    ComPort_select.SelectedIndex = 3;
            }
            catch (Exception)
            {
                MessageBox.Show("No NI-DAQ-Unit found! \nTry again.", "Warning",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        }

        //********************************************************************************************************************
        //                                      GUI: Open/Close + ComboBoxes
        //********************************************************************************************************************

        #region Open/Close

        public override void Open()
        {
            //NI-Karte muss nicht geöffnet werden (immer Verbunden)
            //--> Abfragen ob Kanal ausgewählt wurde

            Communication_LOG += "Try to connect NI_USB6281:\n";

            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                MessageBox.Show("No Channel selected! \nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Communication_LOG += "Failure: No channel selected\n";

                return;
            }

            VISA_or_Channel_Name = ComPort_select.Text;
            Communication_LOG += "Seccessful\n";

            IsConnected = true;

            //UI anpassen
            ComPort_select.Enabled = false;
            comboBox_Frequency.Enabled = true;
            comboBox_Range.Enabled = true;
            numericUpDown_Trigger.Enabled = true;

            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Close";

            //UI anpassen
            //barButtonItem_Init.Enabled = true;
            //barButtonItem_REF.Enabled = true;
        }

        public override void Close()
        {
            //Verbindung muss nicht getrennt werden
            IsConnected = false;

            //UI anpassen
            ComPort_select.Enabled = true;
            comboBox_Frequency.Enabled = false;
            comboBox_Range.Enabled = false;
            numericUpDown_Trigger.Enabled = false;

            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Open";

            //UI anpassen
            //barButtonItem_Init.Enabled = false;
            //barButtonItem_REF.Enabled = false;
        }

        #endregion Open/Close

        #region AutoOpen

        public override string AutoOpen(AutoConnect_Window myLoadScreen)
        {

            int iterration = 2;

            Communication_LOG += "Try to connect NI_USB6281:\n";

            //Öffnen
            if (ComPort_select.Text == "")   //Wenn kein Port gewählt ist
            {
                MessageBox.Show("No Channel selected! \nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return "DAQ-Unit: Connection was not found!" + Environment.NewLine;
            }

            VISA_or_Channel_Name = ComPort_select.Text;
            Communication_LOG += "Seccessful\n";


            myLoadScreen.ChangeTask("Change UI ...", iterration);

            IsConnected = true;

            //UI anpassen
            ComPort_select.Enabled = false;
            comboBox_Frequency.Enabled = true;
            comboBox_Range.Enabled = true;
            numericUpDown_Trigger.Enabled = true;

            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Close";

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
                case "500kHz":
                    Frequency = 500000;
                    break;
                case "200kHz":
                    Frequency = 200000;
                    break;
                case "100kHz":
                    Frequency = 100000;
                    break;
            }
        }

        #endregion ComboBox

        //********************************************************************************************************************
        //                                          Hauptbefehle 
        //********************************************************************************************************************

        #region Globale_Messbefehle_TTA

        public override bool Setting_for_TTA()
        {
            //Wird alles in der eingentlichen Messung gemacht
            return true;
        }

        public override bool Setting_Trigger(RthTEC_Rack myRackSettings)
        {
            //Wird alles in der eingentlichen Messung gemacht
            return true;
        }

        public override bool Measure_TTA_Several_Cycles(TTA_measurement myTTA, ATIM_MainWindow GUI)
        {
            //StatusBar anpassen
            GUI.StatusBar_TTA_Single(0, (int)myTTA.MyRack.Cycles);

            //1. Parameter für Messlänge senden********************************************************************************
            //Sampelzahl und Abtastfrequenz definieren (Times sind in ms deswegen /1000)
            int anzahl_samples = Convert.ToInt32(Frequency) / 1000
                                * Decimal.ToInt32(myTTA.MyRack.Time_Heat + myTTA.MyRack.Time_Meas) ;
            //Sample überschuss, da Umschalten derzeit nicht richtig
            int sample_ueberschuss = 2000;

            //Trigger berechnen
            double trigger = decimal.ToDouble((numericUpDown_Trigger.Value - myTTA.MyRack.U_offset / 1000) * myTTA.MyRack.Gain);

            //2. Daten-Felder definieren***************************************************************************************

            //Übergabe-Parameter für NI-Karte definieren
            IAsyncResult uebergabe_parameter_ReadWaveform = null;

            //Ausgangsdaten
            AnalogSingleChannelReader messInfo_NI_SinglePulse = null;

            //Feld für Daten definieren (mit 100x max- beginnen)
            myTTA.Creat_RowDataField( 100 + anzahl_samples + sample_ueberschuss);


            //3. Loop**********************************************************************************************************
            for (int i = 0; i < myTTA.MyRack.Cycles; i++)
            {
                //4. NI-Karte scharf stellen***********************************************************************************
                try
                {
                    // Task für Messung erzeugen
                    myTask_NI = new NationalInstruments.DAQmx.Task("myTask");
                    

                    // Kanal erzeugen:
                    //---------------------------------------------------------------------------------
                    //physicalChannelName As String: z.B. Dev1/ai3
                    //nameToAssignChannel As String: sollte leer bleiben (Zweck keine Ahnung)
                    //terminalConfiguration As AITerminalConfiguration: Differential oder ähnlich
                    //minimumValue As Double: z.B. -10 [V] untere Grenze
                    //maximumValue As Double: z.B. 10 [V]
                    //customScaleName As String:
                    //---------------------------------------------------------------------------------
                    myTask_NI.AIChannels.CreateVoltageChannel(VISA_or_Channel_Name, "",
                        AITerminalConfiguration.Differential, -Range / 1000, Range / 1000, AIVoltageUnits.Volts);


                    // Timing-Parameter definieren:
                    //---------------------------------------------------------------------------------
                    //signalSource As string: Wenn interne Clock verwendet wird ""
                    //rate As double: Abtastfrequenz in Hz (Interne Clock)
                    //activeEdge As SampleClockActiveEdge: Bei welcher Flanke der Clock abgetastet wird
                    //sampleMode As SampleQuantityMode: Legt fest ob dauerhaft gemessen wird, oder bis
                    //                                  nur eine endliche Anzahl (nächster Parameter)
                    //samplesPerChannel As int: Anzahl der Samples
                    //---------------------------------------------------------------------------------
                    myTask_NI.Timing.ConfigureSampleClock("", Frequency, SampleClockActiveEdge.Rising,
                        SampleQuantityMode.FiniteSamples, anzahl_samples + sample_ueberschuss);


                    // Trigger definieren
                    //---------------------------------------------------------------------------------
                    //source As String: Herkuft des Triggers. Hier gleich wie Daten (Dev1/ai3)
                    //slope As AnalogEdgeStartTriggerSlope: Rising or Falling
                    //level As Double: Trigger Level (vorher bestimmt
                    //---------------------------------------------------------------------------------
                    myTask_NI.Triggers.StartTrigger.ConfigureAnalogEdgeTrigger(VISA_or_Channel_Name,
                        AnalogEdgeStartTriggerSlope.Rising, trigger);


                    //Hysterese festlegen (keine Ahnung ob das Wichtig ist)
                    //---------------------------------------------------------------------------------
                    //Hysteresis as Double: Wert der Hysterese
                    //---------------------------------------------------------------------------------
                    myTask_NI.Triggers.StartTrigger.AnalogEdge.Hysteresis = 0.05;

                    //TimeOut anpassen 
                    myTask_NI.Stream.Timeout = (Int32)(1.2m*(myTTA.MyRack.Time_Heat + myTTA.MyRack.Time_Meas));

                    // Verify the Task
                    myTask_NI.Control(TaskAction.Verify);

                    //Messwert-Ausgabe definieren 
                    messInfo_NI_SinglePulse = new AnalogSingleChannelReader(myTask_NI.Stream);

                    // Use SynchronizeCallbacks to specify that the object 
                    // marshals callbacks across threads appropriately.
                    messInfo_NI_SinglePulse.SynchronizeCallbacks = true;
                    uebergabe_parameter_ReadWaveform = messInfo_NI_SinglePulse.BeginReadWaveform(anzahl_samples + sample_ueberschuss, null, null);
                }
                catch (DaqException ex)
                {
                    //Bei Daq Exception soll die Fehlermeldung ausgegeben werden (z.B. kein Kanal, ...)
                    MessageBox.Show(ex.Message);
                    //Task beenden
                    myTask_NI.Dispose();
                }

                //5. Puls starten**********************************************************************************************              
                System.Threading.Thread.Sleep(300);
                myTTA.MyRack.SinglePuls_withDelay();
                System.Threading.Thread.Sleep(1000);

                //6. Daten auswerten*******************************************************************************************
                try
                {
                    // vorhandene Daten auslesen    
                    AnalogWaveformSampleCollection<double> test = messInfo_NI_SinglePulse.EndReadWaveform(uebergabe_parameter_ReadWaveform).Samples;


                    double factor = Math.Pow(2, 15) / (Range / 1000);
                    //Mit 100mal Minimalwert beginnen. (Für umschaltpunkt suche)
                    for (int counter = 0; counter < 100; counter++)
                    {
                        myTTA.Binary_Raw_Files[i, counter] = short.MinValue;
                    }
                    //Daten in Binär (short umrechnen) & TTA übergeben
                    for (int counter = 100; counter < test.Count; counter++)
                    {
                        double helping_Double = test[counter].Value * factor;
                        myTTA.Binary_Raw_Files[i, counter] = (short)(helping_Double);
                    }

                    //Gleich in Graph ausgeben
                    GUI.Add_Series_to_RAW(myTTA, i);

                }
                catch (DaqException ex)
                {
                    //Bei Fehler -> Fehlermeldung ausgeben (hier z.B. Timeout)
                    MessageBox.Show(ex.Message);

                    return false;
                }
                finally
                {
                    // Task beenden
                    myTask_NI.Dispose();
                }

                //7. StatusBar anpassen****************************************************************************************
                GUI.StatusBar_TTA_Single(i+1, (int)myTTA.MyRack.Cycles);

            }
            return true;
        }

        public override bool Measure_TTA_Several_Cycles_DEMO(TTA_measurement myTTA, ATIM_MainWindow GUI)
        {
            return true;
        }

        #endregion Globale_Messbefehle_TTA

        #region Globale_Messbefehle_Sensitivity
        public override bool Setting_for_Sensitivity(Sensitvity_Measurement mySensitivity)
        {
            return true;
        }
        public override bool Measure_Sensitivity(Sensitvity_Measurement mySensitivity, ATIM_MainWindow GUI)
        {
            return true;
        }
        public override bool Measure_Sensitivity_DEMO(Sensitvity_Measurement mySensitivity)
        {
            return true;
        }

        #endregion Globale_Messbefehle_Sensitivity


        public override decimal Get_Constant_Voltage(TTA_measurement myTTA, ATIM_MainWindow GUI)
        {
            //Konstante Spannung über mehrere Samples messen 



            //1. Daten-Felder definieren***************************************************************************************

            //Übergabe-Parameter für NI-Karte definieren
            IAsyncResult uebergabe_parameter_ReadWaveform_Constant = null;

            //Ausgangsdaten
            AnalogSingleChannelReader messInfo_NI_SinglePulse_Constant = null;

            // Task für Messung erzeugen
            NationalInstruments.DAQmx.Task myTask_NI_Constant = null;
            

            //2. NI-Karte scharf stellen***********************************************************************************
            try
            {
                myTask_NI_Constant = new NationalInstruments.DAQmx.Task("myTask_Constant");

                // Kanal erzeugen:
                //---------------------------------------------------------------------------------
                //physicalChannelName As String: z.B. Dev1/ai3
                //nameToAssignChannel As String: sollte leer bleiben (Zweck keine Ahnung)
                //terminalConfiguration As AITerminalConfiguration: Differential oder ähnlich
                //minimumValue As Double: z.B. -10 [V] untere Grenze
                //maximumValue As Double: z.B. 10 [V]
                //customScaleName As String:
                //---------------------------------------------------------------------------------
                myTask_NI_Constant.AIChannels.CreateVoltageChannel(VISA_or_Channel_Name, "",
                    AITerminalConfiguration.Differential, -Range / 1000, Range / 1000, AIVoltageUnits.Volts);


                // Timing-Parameter definieren:
                //---------------------------------------------------------------------------------
                //signalSource As string: Wenn interne Clock verwendet wird ""
                //rate As double: Abtastfrequenz in Hz (Interne Clock)
                //activeEdge As SampleClockActiveEdge: Bei welcher Flanke der Clock abgetastet wird
                //sampleMode As SampleQuantityMode: Legt fest ob dauerhaft gemessen wird, oder bis
                //                                  nur eine endliche Anzahl (nächster Parameter)
                //samplesPerChannel As int: Anzahl der Samples
                //---------------------------------------------------------------------------------
                myTask_NI_Constant.Timing.ConfigureSampleClock("", Frequency, SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples, (int)Frequency/1000);


                // Trigger definieren
                //---------------------------------------------------------------------------------
                //Kein Trigger definieren --> Sofort starten
                //---------------------------------------------------------------------------------
                myTask_NI_Constant.Triggers.StartTrigger.ConfigureNone();

                //TimeOut anpassen 
                myTask_NI_Constant.Stream.Timeout = 2000;

                // Verify the Task
                myTask_NI_Constant.Control(TaskAction.Verify);

                //Messwert-Ausgabe definieren 
                messInfo_NI_SinglePulse_Constant = new AnalogSingleChannelReader(myTask_NI_Constant.Stream);

                // Use SynchronizeCallbacks to specify that the object 
                // marshals callbacks across threads appropriately.
                messInfo_NI_SinglePulse_Constant.SynchronizeCallbacks = true;
                uebergabe_parameter_ReadWaveform_Constant = messInfo_NI_SinglePulse_Constant.BeginReadWaveform((int)Frequency / 1000, null, null);
            }
            catch (DaqException ex)
            {
                //Bei Daq Exception soll die Fehlermeldung ausgegeben werden (z.B. kein Kanal, ...)
                MessageBox.Show(ex.Message);
                //Task beenden
                myTask_NI_Constant.Dispose();

                return 0;
            }


            //6. Daten auswerten*******************************************************************************************
            try
            {
                // vorhandene Daten auslesen    
                AnalogWaveformSampleCollection<double> test = messInfo_NI_SinglePulse_Constant.EndReadWaveform(uebergabe_parameter_ReadWaveform_Constant).Samples;

                //Durchschnitt berechnen
                double average = 0;
                for (int counter = 0; counter < test.Count; counter++)               
                    average += test[counter].Value;

                return (decimal)average / test.Count / myTTA.MyRack.Gain + myTTA.MyRack.U_offset/1000;
            }
            catch (DaqException ex)
            {
                //Bei Fehler -> Fehlermeldung ausgeben (hier z.B. Timeout)
                MessageBox.Show(ex.Message);

                return 0;
            }
            finally
            {
                // Task beenden
                myTask_NI_Constant.Dispose();
            }

        }


    }
}
