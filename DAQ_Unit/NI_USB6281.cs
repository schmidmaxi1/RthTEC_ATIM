using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NationalInstruments.DAQmx;            //Eigeneintrag für NI-Karte
using NationalInstruments;

using AutoConnect;
using Communication_Settings;
using Hilfsfunktionen;

namespace DAQ_Units
{
    public partial class NI_USB6281 : UserControl, I_DAQ
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
                "+/- 100mV",
                "+/- 200mV",
                "+/- 500mV",
                "+/- 1V",
                "+/- 2V",
                "+/- 5V",
                "+/- 10V"
            };

        public long Frequency { get; set; } = 500000;
        public string[] FrequencyList { get; } =
            {
                "500kHz",
                "200kHz",
                "100kHz",
            };

        
        public decimal Trigger_Level_in_V { get; internal set; }
        //private double Trigger_Level_in_V_double { get; internal set; }

        //Log
        public string Communication_LOG { get; internal set; }

        public long Samples { get; set; }

        //************************Special-Variablen*************************************

        //IP - Adresse Device
        private string Channel_Name { get; set; } = "";

        private NationalInstruments.DAQmx.Task myTask_NI;



        //Übergabe-Parameter für NI-Karte definieren
        private IAsyncResult uebergabe_parameter_ReadWaveform = null;

        //Ausgangsdaten
        private AnalogSingleChannelReader messInfo_NI_SinglePulse = null;

        #endregion Variables

        //********************************************************************************************************************
        //                                            Initialization
        //********************************************************************************************************************
        public NI_USB6281()
        {
            InitializeComponent();

            Init_ComboBox_Channels();
        }

        public NI_USB6281(Form callingForm, int x, int y)
        {
            InitializeComponent();

            Init_ComboBox_Channels();

            //In GUI einfügen
            this.Location = new System.Drawing.Point(x, y);
            this.Name = "DAQ:NI_USB6281";
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

        public void Change_ADR(string adr)
        {
            //ComboBox übernehmen
            Channel_select.SelectedItem = adr;
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

        public  bool TTA_set_Device(decimal t_heat_ms, decimal t_meas_ms)
        {
            //1. Parameter für Messlänge senden********************************************************************************
            //Sampelzahl und Abtastfrequenz definieren (Times sind in ms deswegen /1000)
            int anzahl_samples = Convert.ToInt32(Frequency) / 1000
                                * Decimal.ToInt32(t_heat_ms + t_meas_ms);
            //Sample überschuss, da Umschalten derzeit nicht richtig
            int sample_ueberschuss = 2000;

            //Zusammenzählen
            Samples = anzahl_samples + sample_ueberschuss;
            return true;
        }

        public bool TTA_set_Trigger(decimal frontend_gain, decimal forntend_offset)//RthTEC_Rack myRackSettings)
        {
            //Trigger berechnen
            Trigger_Level_in_V = (Voltage_Trigger.Value - forntend_offset / 1000) * frontend_gain;
            return true;
        }

        public bool TTA_wait_for_Trigger()
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
                myTask_NI.AIChannels.CreateVoltageChannel(Channel_Name, "",
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
                    SampleQuantityMode.FiniteSamples, (int)Samples);


                // Trigger definieren
                //---------------------------------------------------------------------------------
                //source As String: Herkuft des Triggers. Hier gleich wie Daten (Dev1/ai3)
                //slope As AnalogEdgeStartTriggerSlope: Rising or Falling
                //level As Double: Trigger Level (vorher bestimmt) im mV
                //---------------------------------------------------------------------------------
                myTask_NI.Triggers.StartTrigger.ConfigureAnalogEdgeTrigger(Channel_Name,
                    AnalogEdgeStartTriggerSlope.Rising, (double)Trigger_Level_in_V);


                //Hysterese festlegen (keine Ahnung ob das Wichtig ist)
                //---------------------------------------------------------------------------------
                //Hysteresis as Double: Wert der Hysterese
                //---------------------------------------------------------------------------------
                myTask_NI.Triggers.StartTrigger.AnalogEdge.Hysteresis = 0.05;

                //TimeOut anpassen 
                myTask_NI.Stream.Timeout = (Int32)(1.2m * (Samples * 1000 / Frequency));

                // Verify the Task
                myTask_NI.Control(TaskAction.Verify);

                //Messwert-Ausgabe definieren 
                messInfo_NI_SinglePulse = new AnalogSingleChannelReader(myTask_NI.Stream);

                // Use SynchronizeCallbacks to specify that the object 
                // marshals callbacks across threads appropriately.
                messInfo_NI_SinglePulse.SynchronizeCallbacks = true;
                uebergabe_parameter_ReadWaveform = messInfo_NI_SinglePulse.BeginReadWaveform((int)Samples, null, null);

                return true;
            }
            catch (DaqException ex)
            {
                //Bei Daq Exception soll die Fehlermeldung ausgegeben werden (z.B. kein Kanal, ...)
                MessageBox.Show(ex.Message);
                //Task beenden
                myTask_NI.Dispose();

                return false;
            }
        }

        public bool TTA_Collect_Data(short [,] data_out, int cycle)
        {           
            //6. Daten auswerten*******************************************************************************************
            try
            {
                // vorhandene Daten auslesen    
                AnalogWaveformSampleCollection<double> test = messInfo_NI_SinglePulse.EndReadWaveform(uebergabe_parameter_ReadWaveform).Samples;


                double factor = Math.Pow(2, 15) / (Range / 1000);
                //Mit 100mal Minimalwert beginnen. (Für umschaltpunkt suche)
                for (int counter = 0; counter < 100; counter++)
                {
                    data_out[cycle, counter] = short.MinValue;
                }
                //Daten in Binär (short umrechnen) & TTA übergeben
                for (int counter = 100; counter < test.Count; counter++)
                {
                    double helping_Double = test[counter].Value * factor;
                    data_out[cycle, counter] = (short)(helping_Double);
                }
                return true;
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
        }

        public bool TTA_reserve_Storage(short[,] array) { return true;} //not neccessary for NI-USB6821
        public bool TTA_free_Storage(short[,] array) { return true; } //not neccessary for NI-USB6821

        #endregion TTA

        #region Sensitivity

        public bool Sensitivity_Set_Device(short[] array, long nr_of_samples)
        {
            //Nothing to do here
            //Task is done in "Sensitivity_Measure_and_Collect_Data"
            return true;
        }
        public bool Sensitivity_Set_Trigger()
        {
            //Nothing to do here
            //Task is done in "Sensitivity_Measure_and_Collect_Data"
            return true;
        }
        public bool Sensitivity_Measure_and_Collect_Data(short [] output)
        {
            //2. NI-Karte scharf stellen***********************************************************************************
            try
            {
                myTask_NI = new NationalInstruments.DAQmx.Task("myTask_Constant");

                // Kanal erzeugen:
                //---------------------------------------------------------------------------------
                //physicalChannelName As String: z.B. Dev1/ai3
                //nameToAssignChannel As String: sollte leer bleiben (Zweck keine Ahnung)
                //terminalConfiguration As AITerminalConfiguration: Differential oder ähnlich
                //minimumValue As Double: z.B. -10 [V] untere Grenze
                //maximumValue As Double: z.B. 10 [V]
                //customScaleName As String:
                //---------------------------------------------------------------------------------
                myTask_NI.AIChannels.CreateVoltageChannel(Channel_Name, "",
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
                    SampleQuantityMode.FiniteSamples, (int)Frequency / 1000);


                // Trigger definieren
                //---------------------------------------------------------------------------------
                //Kein Trigger definieren --> Sofort starten
                //---------------------------------------------------------------------------------
                myTask_NI.Triggers.StartTrigger.ConfigureNone();

                //TimeOut anpassen 
                myTask_NI.Stream.Timeout = 2000;

                // Verify the Task
                myTask_NI.Control(TaskAction.Verify);

                //Messwert-Ausgabe definieren 
                messInfo_NI_SinglePulse = new AnalogSingleChannelReader(myTask_NI.Stream);

                // Use SynchronizeCallbacks to specify that the object 
                // marshals callbacks across threads appropriately.
                messInfo_NI_SinglePulse.SynchronizeCallbacks = true;
                uebergabe_parameter_ReadWaveform = messInfo_NI_SinglePulse.BeginReadWaveform((int)Frequency / 1000, null, null);
            }
            catch (DaqException ex)
            {
                //Bei Daq Exception soll die Fehlermeldung ausgegeben werden (z.B. kein Kanal, ...)
                MessageBox.Show(ex.Message);
                //Task beenden
                myTask_NI.Dispose();

                return false;
            }


            //6. Daten auswerten*******************************************************************************************
            try
            {
                // vorhandene Daten auslesen    
                AnalogWaveformSampleCollection<double> test = messInfo_NI_SinglePulse.EndReadWaveform(uebergabe_parameter_ReadWaveform).Samples;

                //Durchschnitt berechnen
                output = new short[test.Count];
                double factor = Math.Pow(2, 15) / (Range / 1000);

                for (int counter = 0; counter < test.Count; counter++)
                   output[counter] = (short)(test[counter].Value * factor);

                return true;
                //return (decimal)average / test.Count / myTTA.MyRack.Gain + myTTA.MyRack.U_offset / 1000;
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

        }

        #endregion Sensitivity

        //********************************************************************************************************************
        //                                         Lokale Functionen
        //********************************************************************************************************************

        #region lokal
        private void Init_ComboBox_Channels()
        {
            try
            {
                //Alle Kanäle, aller NI-Karten finden
                Channel_select.Items.AddRange(DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External));
                //Ziel: Dev1/ai3 zu finden (steht normalerweise an Indexstelle Nr. 3 und wird standardmäßig ausgewählt)
                if (Channel_select.Items.Count > 0)
                    Channel_select.SelectedIndex = 3;
            }
            catch (Exception)
            {
                MessageBox.Show("No NI-DAQ-Unit found! \nTry again.", "Warning",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
      
        private void Open()
        {
            //NI-Karte muss nicht geöffnet werden (immer Verbunden)
            //--> Abfragen ob Kanal ausgewählt wurde

            Communication_LOG += "Try to connect NI_USB6281:\n";

            if (Channel_select.Text == "")   //Wenn kein Port gewählt ist
            {
                MessageBox.Show("No Channel selected! \nTry again.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Communication_LOG += "Failure: No channel selected\n";

                return;
            }

            Channel_Name = Channel_select.Text;
            Communication_LOG += "Seccessful\n";

            IsConnected = true;

            //UI anpassen
            Channel_select.Enabled = false;
            Voltage_Trigger.Enabled = true;

            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Close";
        }

        public void Close()
        {
            //Verbindung muss nicht getrennt werden
            IsConnected = false;

            //UI anpassen
            Channel_select.Enabled = true;
            Voltage_Trigger.Enabled = false;

            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Open";

        }

        #endregion lokal

        //********************************************************************************************************************
        //                                           AutoConnect
        //********************************************************************************************************************

        #region AutoConnect

        public void Update_settings(NI_CommunicationDevice myInput)
        {
            HelpFCT.SetComboBox2ComboBox(myInput.comboBox_Channel, Channel_select);
        }

        public void Update_settings(EthernetCommunicationDevice myInput)
        {
            //Not used but neccessary for Interface
        }

        public string AutoOpen(AutoConnect_Window myLoadScreen)
        {

            int iterration = 10;

            //NI-Karte muss nicht geöffnet werden (immer Verbunden)
            //--> Abfragen ob Kanal ausgewählt wurde

            Communication_LOG += "Try to connect NI_USB6281:\n";

            if (Channel_select.Text == "")   //Wenn kein Port gewählt ist
            {
                return "No Channel selected!";
            }

            myLoadScreen.ChangeTask("Change UI ...", iterration);

            Channel_Name = Channel_select.Text;
            Communication_LOG += "Seccessful\n";

            IsConnected = true;

            //UI anpassen
            Channel_select.Enabled = false;
            Voltage_Trigger.Enabled = true;

            //Text und Kontext-Menü auf Button ändern
            button_OpenClose.Text = "Close";

            return "";
        }

        #endregion AutoConnect

    }
}
