using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using DAQ_Units;
using XYZ_Table;
using RthTEC_Rack;

using Read_Coordinates;
using Hilfsfunktionen;

using Accord.IO;

using ATIM_GUI._01_TTA;


namespace ATIM_GUI
{
    public partial class TTA_DPA
    {

        //*****************************************************************************************************
        //                                        local Variables
        //*****************************************************************************************************

        //Fixed values
        static short schwelle = 800;


        //*****************************************************************************************************
        //                                        only local FCT
        //*****************************************************************************************************

        /// <summary>
        /// Searches all samples for switching points
        /// </summary>
        private bool Find_Switching_Points()
        {
            /*
            //Define Arrays
            SwPo_2Heat_DPA = new long[MyRthTEC_Rack.DPA_Count];
            SwPo_2Sense_DPA = new long[MyRthTEC_Rack.DPA_Count];

            //Local Variables
            int counter_DPA_Heat = 0;
            int counter_DPA_Sense = 0;

            //Shift to reduce calculation Time 
            //Set to 80% of the DPA pulse length in samples
            long shift_DPA       = (long)(MyRthTEC_Rack.DPA_Time  / 1000 * MyDAQ.Frequency * 0.8m);
            long shift_STD_Heat  = (long)(MyRthTEC_Rack.Time_Heat / 1000 * MyDAQ.Frequency * 0.9m);
            long shift_STD_Sense = (long)(MyRthTEC_Rack.Time_Meas / 1000 * MyDAQ.Frequency * 0.9m);
            */

            //Define Arrays
            SwPo_2Heat_DPA = new long[50];
            SwPo_2Sense_DPA = new long[50];

            //Local Variables
            int counter_DPA_Heat = 0;
            int counter_DPA_Sense = 0;

            //Shift to reduce calculation Time 
            //Set to 80% of the DPA pulse length in samples
            long shift_DPA = 8000;
            long shift_STD_Heat = 27000000;
            long shift_STD_Sense = 27000000;


            //Go through all data points
            for (long i = 2; i < Binary_Raw_Values.Length; i++)
            {
                //Unterschied der zwei Punkte berechnen
                Int32 change = Binary_Raw_Values[i] - Binary_Raw_Values[i - 1];

                //Positiv Edge (--> to Heat)
                if (change > schwelle)
                {

                    //If in DPA sequence
                    //if (counter_DPA_Heat < MyRthTEC_Rack.DPA_Count)
                    if (counter_DPA_Heat < 50)
                    {
                        //Punkt übernhemen
                        SwPo_2Heat_DPA[counter_DPA_Heat] = i;

                        //Counter hochzählen
                        counter_DPA_Heat++;

                        //Punkte überspringen
                        i += shift_DPA;
                    }


                    //if already in std. Sequence
                    else
                    {
                        //Punkt übernhemen
                        SwPo_2Heat_STD = i;

                        //Punkte überspringen
                        i += shift_STD_Heat;
                    }

                }

                //Negative Edge (--> to Meas)
                if (change < -schwelle)
                {
                    //If in DPA sequence
                    //if (counter_DPA_Sense < MyRthTEC_Rack.DPA_Count)
                    if (counter_DPA_Sense < 50)
                    {
                        //Punkt übernhemen
                        SwPo_2Sense_DPA[counter_DPA_Sense] = i;

                        //Counter hochzählen
                        counter_DPA_Sense++;

                        //Punkte überspringen
                        i += shift_DPA;
                    }

                    //if already in std. Sequence
                    else
                    {
                        //Punkt übernhemen
                        SwPo_2Sense_STD = i;

                        //Abbrechen
                        break;
                    }
                }

            }

            //Ausschaltpunkt finden
            for (long i = SwPo_2Sense_STD + shift_STD_Sense; i < Binary_Raw_Values.Length; i++)
            {
                //If the minimum bit value is reached
                if ((Binary_Raw_Values[i] < short.MinValue + 10) | (Binary_Raw_Values[i] - Binary_Raw_Values[i-1] < -schwelle))
                {
                    SwPo_2Off_STD = i;
                    break;
                }

            }

            //Wenn kein Ende gefunden --> dann Länge - 1
            if (SwPo_2Off_STD == 0)
                SwPo_2Off_STD = Binary_Raw_Values.Length - 1;

            return true;
        }

        /// <summary>
        /// Checks the lengths of all time periodes on correct length
        /// </summary>
        private bool Check_Switching_Points()
        {

            //Init Arrays **********************************************************************************************
            /*
            long[] Lengths_DPA_Heat = new long[MyRthTEC_Rack.DPA_Count];
            long[] Lengths_DPA_Sense = new long[MyRthTEC_Rack.DPA_Count];
            */
            long[] Lengths_DPA_Heat = new long[50];
            long[] Lengths_DPA_Sense = new long[50];

            long Length_STD_Heat;
            long Length_STD_Sense;


            //Referenz-Längen berechnen*********************************************************************************

            decimal limit_Top = 1.01m;
            decimal limit_Bot = 0.99m;
            /*
            decimal sampleCount_DPA = MyRthTEC_Rack.DPA_Time / 1000 * MyDAQ.Frequency;
            decimal sampleCount_STD_Heat = MyRthTEC_Rack.Time_Heat / 1000 * MyDAQ.Frequency;
            decimal sampleCount_STD_Sense = MyRthTEC_Rack.Time_Meas / 1000 * MyDAQ.Frequency;
            */

            decimal sampleCount_DPA = 10000;
            decimal sampleCount_STD_Heat = 30000000;
            decimal sampleCount_STD_Sense = 30000000;

            //Längen berechnen ******************************************************************************************

            //Calculate Pulse Lengthes Heat Intervalls
            for (int i = 0; i < Lengths_DPA_Heat.Length; i++)           
                Lengths_DPA_Heat[i] = SwPo_2Sense_DPA[i] - SwPo_2Heat_DPA[i];

            //Calculate Pulse Length Sense Intervalls
            for (int i = 0; i < Lengths_DPA_Sense.Length - 1; i++)
                Lengths_DPA_Sense[i] = SwPo_2Heat_DPA[i + 1] - SwPo_2Sense_DPA[i];

            //Letzer Punkt DPA Sense
            Lengths_DPA_Sense[Lengths_DPA_Sense.Length - 1] = SwPo_2Heat_STD - SwPo_2Sense_DPA[Lengths_DPA_Sense.Length - 1];

            //STD length berechnen
            Length_STD_Heat = SwPo_2Sense_STD - SwPo_2Heat_STD;
            Length_STD_Sense = SwPo_2Off_STD - SwPo_2Sense_STD;


            //Mittelwert berechnen **************************************************************************************
            Aver_DPA_Heat_Length = (long) Math.Round(Lengths_DPA_Heat.Average());
            Aver_DPA_Sense_Length = (long) Math.Round(Lengths_DPA_Sense.Average());


            //Längen prüfen *********************************************************************************************

            string error_MSG = "";

            //DPA
            for (int i = 0; i < Lengths_DPA_Heat.Length; i++)
            {
                if (Lengths_DPA_Heat[i] < sampleCount_DPA * limit_Bot)
                    error_MSG += "t_Heat_DPA Pulse-Nr. " + (i+1).ToString() + " is too short!" + Environment.NewLine;

                if (Lengths_DPA_Heat[i] > sampleCount_DPA * limit_Top)
                    error_MSG += "t_Heat_DPA Pulse-Nr. " + (i + 1).ToString() + " is too long!" + Environment.NewLine;

                if (Lengths_DPA_Sense[i] < sampleCount_DPA * limit_Bot)
                    error_MSG += "t_Sense_DPA Pulse-Nr. " + (i + 1).ToString() + " is too short!" + Environment.NewLine;

                if (Lengths_DPA_Sense[i] > sampleCount_DPA * limit_Top)
                    error_MSG += "t_Sense_DPA Pulse-Nr. " + (i + 1).ToString() + " is too long!" + Environment.NewLine;
            }

            //STD
            if (Length_STD_Heat < sampleCount_STD_Heat * limit_Bot)
                error_MSG += "t_Heat_STD is too short!" + Environment.NewLine;

            if (Length_STD_Heat > sampleCount_STD_Heat * limit_Top)
                error_MSG += "t_Heat_STD is too long!" + Environment.NewLine;

            if (Length_STD_Sense < sampleCount_STD_Sense * limit_Bot)
                error_MSG += "t_Sense_STD is too short!" + Environment.NewLine;

            if (Length_STD_Sense > sampleCount_STD_Sense * limit_Top)
                error_MSG += "t_Sense_STD is too long!" + Environment.NewLine;

            //Ausgabe ****************************************************************************************************
            if (error_MSG == "")
                return true;
            else
            {
                //Output as Error
                System.Windows.Forms.MessageBox.Show(error_MSG, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return false;
            }
        }

        /// <summary>
        /// Calculates all Power-Steps while DPA
        /// </summary>
        private bool Calc_PowerSteps()
        {
            //Local Setttings********************************************************************************************
            const long cutSamples_Start = 10;
            const long cutSamples_End = 10;


            //Init Arrays **********************************************************************************************
            /*
            PowerSteps_Heat_DPA = new decimal[MyRthTEC_Rack.DPA_Count];
            PowerSteps_Sense_DPA = new decimal[MyRthTEC_Rack.DPA_Count];

            short[] PowerSteps_Heat_DPA_binary = new short[MyRthTEC_Rack.DPA_Count];
            short[] PowerSteps_Sense_DPA_binary = new short[MyRthTEC_Rack.DPA_Count];
            */
            PowerSteps_2Heat_DPA = new decimal[50];
            PowerSteps_2Sense_DPA = new decimal[50];

            //Faktor 16 gegen quanisierungs-Rauschen
            Int32[] Voltage_Heat_DPA_binary_times16 = new Int32[50];
            Int32[] Voltage_Sense_DPA_binary_times16 = new Int32[50];

            Int32 Voltage_Heat_STD_Start_binary_times16;
            Int32 Voltage_Heat_STD_End_binary_times16;
            Int32 Voltage_Sense_STD_Start_binary_times16;


            //Calculate Binary Average**************************************************************************************** 
            Int64 localSum = 0;
            long sampleCount = 0;

            //DPA Heat
            for (int i = 0; i < Voltage_Heat_DPA_binary_times16.Length; i++)
            {
                localSum = 0;
                sampleCount = 0;

                //Sum up
                for (long n = SwPo_2Heat_DPA[i] + cutSamples_Start; n < SwPo_2Sense_DPA[i] - cutSamples_End; n++)
                {
                    localSum += Binary_Raw_Values[n];
                    sampleCount++;
                }

                //Average
                Voltage_Heat_DPA_binary_times16[i] = (Int32)(localSum * 16 / sampleCount);
            }

            //DPA Sense
            for (int i = 0; i < Voltage_Sense_DPA_binary_times16.Length; i++)
            {
                localSum = 0;
                sampleCount = 0;

                //Für alle außer letzen (DPA switchin point)
                if (i < Voltage_Sense_DPA_binary_times16.Length - 1)
                {
                    //Sum up
                    for (long n = SwPo_2Sense_DPA[i] + cutSamples_Start; n < SwPo_2Heat_DPA[i + 1] - cutSamples_End; n++)
                    {
                        localSum += Binary_Raw_Values[n];
                        sampleCount++;
                    }
                }
                //Für letzen mit std Heat switching Point
                else
                {
                    //Sum up
                    for (long n = SwPo_2Sense_DPA[i] + cutSamples_Start; n < SwPo_2Heat_STD - cutSamples_End; n++)
                    {
                        localSum += Binary_Raw_Values[n];
                        sampleCount++;
                    }
                }

                //Average
                Voltage_Sense_DPA_binary_times16[i] = (Int32)(localSum * 16 / sampleCount);
            }

            //std Heat start
            localSum = 0;
            sampleCount = 0;

            for (long n = SwPo_2Heat_STD + cutSamples_Start; n < SwPo_2Heat_STD + Aver_DPA_Heat_Length - cutSamples_End; n++)
            {
                localSum += Binary_Raw_Values[n];
                sampleCount++;
            }
            //Average
            Voltage_Heat_STD_Start_binary_times16 = (Int32)(localSum * 16 / sampleCount);

            //std Heat start
            localSum = 0;
            sampleCount = 0;

            for (long n = SwPo_2Sense_STD - Aver_DPA_Heat_Length + cutSamples_Start; n < SwPo_2Sense_STD  - cutSamples_End; n++)
            {
                localSum += Binary_Raw_Values[n];
                sampleCount++;
            }
            //Average
            Voltage_Heat_STD_End_binary_times16 = (Int32)(localSum * 16 / sampleCount);

            //std Sense
            localSum = 0;
            sampleCount = 0;

            for (long n = SwPo_2Sense_STD + cutSamples_Start; n < SwPo_2Sense_STD + Aver_DPA_Heat_Length - cutSamples_End; n++)
            {
                localSum += Binary_Raw_Values[n];
                sampleCount++;
            }
            //Average
            Voltage_Sense_STD_Start_binary_times16 = (Int32)(localSum * 16 / sampleCount);


            //Calculate Power Steps ******************************************************************************************

            /*
            decimal I_Heat_A = MyHeatSource.I_Heat / 1000;
            decimal I_Sense_A = MyHeatSource.I_Meas / 1000;

            decimal Gain = MyFrontEnd.Gain;
            decimal Offset_V = MyFrontEnd.V_Offset / 1000;

            decimal Range = MyDAQ.Range / 1000;
            double Resulution = Math.Pow(2, MyDAQ.Resolution);
            */

            decimal I_Heat_A = 1;
            decimal I_Sense_A = 0.02m;

            decimal Gain = 2;
            decimal Offset_V = 3;

            decimal Range = 1;
            decimal Resolution = (decimal)Math.Pow(2, 16);


            //Umrechnungs-Faktoren definieren
            decimal V_factor = 2 * Range / (Resolution * Gain * 16);
            decimal V_offset = Offset_V;


            //Steps DPA Heat
            PowerSteps_2Heat_DPA[0] = (Voltage_Heat_DPA_binary_times16[0] * V_factor + V_offset) * I_Heat_A;

            for(int i = 1; i < PowerSteps_2Heat_DPA.Length; i++)
            {
                PowerSteps_2Heat_DPA[i] = (Voltage_Heat_DPA_binary_times16[i] * V_factor + V_offset) * I_Heat_A
                    - (Voltage_Sense_DPA_binary_times16[i - 1] * V_factor + V_offset) * I_Sense_A;
            }

            //Steps DPA Sense
            for (int i = 0; i < PowerSteps_2Sense_DPA.Length; i++)
            {
                PowerSteps_2Sense_DPA[i] = (Voltage_Sense_DPA_binary_times16[i] * V_factor + V_offset) * I_Sense_A
                 - (Voltage_Heat_DPA_binary_times16[i] * V_factor + V_offset) * I_Heat_A;                    
            }

            //Step STD Heat
            PowerStep_2Heat_STD = (Voltage_Heat_STD_Start_binary_times16 * V_factor + V_offset) * I_Heat_A
                - (Voltage_Sense_DPA_binary_times16[Voltage_Sense_DPA_binary_times16.Length - 1] * V_factor + V_offset) * I_Sense_A;

            //Step STD Sense
            PowerStep_2Sense_STD = (Voltage_Sense_STD_Start_binary_times16 * V_factor + V_offset) * I_Sense_A
                - (Voltage_Heat_STD_End_binary_times16 * V_factor + V_offset) * I_Heat_A;

            return true;
        }

        /// <summary>
        /// Calculates the Power Correction Vectors
        /// </summary>
        private bool Calc_PowerCorrection()
        {
            //Init Arrays **********************************************************************************************
            /*
            CorrVec_A = new decimal[MyRthTEC_Rack.DPA_Count];
            CorrVec_B = new decimal[2 * MyRthTEC_Rack.DPA_Count -1];
            decimal[,] CorrVec_B_Help = new decimal[MyRthTEC_Rack.DPA_Count, 2 * MyRthTEC_Rack.DPA_Count];
            */

            CorrVec_A = new decimal[50];
            CorrVec_B = new decimal[99];

            decimal[,] CorrVec_B_Help = new decimal[50, 100];

            //Calc A ***************************************************************************************************

            // Alg. gilt: a_i = P-Step_Sense_STD / P_Sense_DPA_i

            for (int i = 0; i < CorrVec_A.Length; i++)
            {
                CorrVec_A[i] = PowerStep_2Sense_STD / PowerSteps_2Sense_DPA[i];
            }

            //Calc B ***************************************************************************************************
            //Erst hilfs-Matrix füllen
            for (int i = 0; i < CorrVec_B_Help.GetLength(0); i++)
            {
                for (int j = 0; j < CorrVec_B_Help.GetLength(0); j++)
                {
                    //Nur linke untere Häflte mit Diagonale beschreiben
                    if (j <= i)
                    {
                        //Sense in alle ungeraden(1, 3, ...) 
                        //wobei 1 nachher gestrichen wird
                        CorrVec_B_Help[i, 2 * j] = PowerSteps_2Sense_DPA[i - j] / PowerSteps_2Sense_DPA[i];

                        //Sense in alle geraden(2, 4, ...) 
                        CorrVec_B_Help[i, 2 * j + 1] = PowerSteps_2Heat_DPA[i - j] / PowerSteps_2Sense_DPA[i];
                    }
                }
            }

            //Spalten zusammenfassen (Erste spalte weglassen)
            for (int i = 1; i < CorrVec_B_Help.GetLength(1); i++)
            {
                for (int j = 0; j < CorrVec_B_Help.GetLength(0); j++)
                {
                    CorrVec_B[i-1] += CorrVec_B_Help[j, i];
                }
            }


            return true;
        }

        /// <summary>
        /// Filters the std. sense response with savitzky golay
        /// </summary>
        private bool Filter_STD_Response()
        {
            //Settings
            const int number_of_Points = 499;
            const int derivation_Order = 0;
            const int polynom_Order = 2;


            //Init Array **************************************************************************************************
            Binary_STD_Respons = new double[SwPo_2Off_STD - SwPo_2Sense_STD + 1];

            //Relevanten Teil herauslösen
            //Array.Copy(Binary_Raw_Values, SwPo_2Sense_STD, Binary_STD_Respons, 0, SwPo_2Off_STD - SwPo_2Sense_STD);

            //Filter *******************************************************************************************************            
            var mySaGoFi = new Altaxo.Calc.Regression.SavitzkyGolay(number_of_Points, derivation_Order, polynom_Order);

            double[] Raw_Values_double = Array.ConvertAll(Binary_Raw_Values.Skip((int)SwPo_2Sense_STD).ToArray(), x => (double)x);

            mySaGoFi.Apply(Raw_Values_double, Binary_STD_Respons);

            
            return true;
        }

        /// <summary>
        /// Calculates the corrected Average of the DPA pulses
        /// </summary>
        private bool Average_DPA()
        {
            //Init Arrays **********************************************************************************************
            //Int32[] DPA_Pulse_Sum = new Int32[Aver_DPA_Sense_Length];
            //Int32[] DPA_Offset_Sum = new Int32[Aver_DPA_Sense_Length];
            decimal[] DPA_Pulse_Sum = new decimal[Aver_DPA_Sense_Length];
            decimal[] DPA_Offset_Sum = new decimal[Aver_DPA_Sense_Length];

            Binary_DPA_Average = new double[Aver_DPA_Sense_Length];

            //Calc Pulse Sum *******************************************************************************************
            //Alle mit Correction Faktor addieren
            for (int i = 0; i < CorrVec_A.Length; i++)  //Alle Pulse
            {
                for(int j = 0; j < DPA_Pulse_Sum.Length; j++)   //Alle Punkte
                {
                    DPA_Pulse_Sum[j] += CorrVec_A[i] * Binary_Raw_Values[SwPo_2Sense_DPA[i] + j];
                }
            }

            //Calc Offset Sum *******************************************************************************************
            long timeshift = 0;

            for (int i = 0; i < CorrVec_B.Length; i++)  //Alle Pulse
            {
                //Als zeit-Shift immer abwechselnd Aver_Heat und Aver_Sense addien
                //Anfangen mit Heat
                if (i % 2 == 0)
                    timeshift += Aver_DPA_Heat_Length;
                else
                    timeshift += Aver_DPA_Sense_Length;
                    
                for (int j = 0; j < DPA_Offset_Sum.Length; j++)   //Alle Punkte
                {                   
                    DPA_Offset_Sum[j] += CorrVec_B[i] * (decimal)Binary_STD_Respons[timeshift + j];
                }
            }

            //Calc Average ***********************************************************************************************

            for (int i = 0; i < DPA_Offset_Sum.Length; i++)   //Alle Punkte
            {
                Binary_DPA_Average[i] = (double)(DPA_Pulse_Sum[i] - DPA_Offset_Sum[i]) / CorrVec_B.Length;
            }

            return true;
        }

        /// <summary>
        /// Fits DPA uns STD TTA together in a defined range
        /// </summary>
        /// <returns></returns>
        private bool Fitting()
        {
            //Parameter ****************************************************************************
            const double StartRange_P = 0.9;
            const double EndRange_P = 1.0;


            //Calc Indicies ************************************************************************
            long startIndex = (long)(Aver_DPA_Sense_Length * StartRange_P);
            long endIndex = (long)(Aver_DPA_Sense_Length * EndRange_P);

            double Sum_DPA = 0;
            double Sum_STD = 0;

            //Calc Average in relevant range *******************************************************
            for (long i = startIndex; i < endIndex; i++)
            {
                Sum_DPA += Binary_DPA_Average[i];
                Sum_STD += Binary_STD_Respons[i];
            }

            double offset_DPA = (Sum_DPA - Sum_STD) / (endIndex - startIndex);

            //Daten zusammensetzen *****************************************************************
            for (int i = 0; i < endIndex; i++)
            {
                Binary_STD_Respons[i] = Binary_DPA_Average[i] - offset_DPA;
            }


            return true;
        }

        /// <summary>
        /// Daten Komprimieren & Umrechnen (& Zeit-Achse hinzufügen)
        /// </summary>
        /// <param name="return">returns Liste with TTA_DataPoints; otherwise, null.</param>
        private List<TTA_DataPoint> Compress_Data()
        {
            /* Erklärung:
             * 
             * Hintergrund:
             * Wenn Daten nicht komprimiert werden, ist der gespeicherte File zu groß.
             * Deshalb werden im späteren Zeitbereich Messpunkte gestrichen.
             * Damit die Information erhalten bleibt, werden die gestrichenen Punkte in weiter bestehende "gemittelt".
             * 
             * Funktion:
             * Wenn eine definierte Datendichte erreicht ist, wird die Schrittweite ehrhöht (d.h. f_Abspeicherung wird kleiner)
             * In diesem Algorithmus werden nach der ersten erhöhung 3 Punkte zusammengefasst, nach der zweiten 9, usw. 
             * Die Punkte werden dazu gemittelt, und in den Mittleren Punkt geschreiben.
             * Der Faktor 3 wurde ausgewählt um die mittelung symetrisch zu machen
             * Die Datendichte ist definiert als der Abstand zwischen zwei Messpunkten / den aktuellen Zeitpunkt
             * 
             * 
           */

            //Leere, temporäre Liste für Output generieren
            List<TTA_DataPoint> output = new List<TTA_DataPoint>();

            //Einige Parameter für schnelleres rechnen bestimmen
            //decimal periode = 1m / MyDAQ.Frequency;                                                                     //Abstand zwischen Samples
            //decimal faktor_voltage = 1m / MyFrontEnd.Gain * MyDAQ.Range / 1000 / (decimal)Math.Pow(2, 15);         //Binary --> Voltage Faktor (mit Anzahl Zyklen)
            //decimal offset_voltage = MyFrontEnd.V_Offset / 1000;                                                            //Offset


            decimal periode = 1m / 10000000;                                                                     //Abstand zwischen Samples
            decimal faktor_voltage = 1m / 2 * 1 / (decimal)Math.Pow(2, 15);         //Binary --> Voltage Faktor (mit Anzahl Zyklen)
            decimal offset_voltage = 3;
            //[Puffer_Messpunkte] Messpunkte mit Zeit "0" in Liste eintragen
            for (int i = 0; i < Puffer_Messpunkte; i++)
            {
                output.Add(
                    new TTA_DataPoint()
                    {
                        Time = 0,
                        Binary = (short)(Binary_STD_Respons[i]),
                        Voltage = (decimal)Binary_STD_Respons[i] * faktor_voltage + offset_voltage,
                        Thermal_Impedance = 0,
                    }
                );
            }

            //Ersten wirklichen Punkt in Liste Aufnehmen
            output.Add(
                new TTA_DataPoint()
                {
                    Time = periode,
                    Binary = (short)(Binary_STD_Respons[Puffer_Messpunkte]),
                    Voltage = (decimal)Binary_STD_Respons[Puffer_Messpunkte] * faktor_voltage + offset_voltage,
                    Thermal_Impedance = 0,
                }
            );

            //Anzahl der Punkte über die im Moment gemittelt wird
            int anzahl_Punkte_Mittelung_gesamt = 1;
            //Anzahl der Punkte vor oder nach dem mittleren Punkt
            long anzahl_Punkte_Mittelung_eine_Seite = (anzahl_Punkte_Mittelung_gesamt - 1) / 2;

            //Zählvariable in Liste (notwendig Schrittweiten erhöhung)
            long Count_Var_outputList = 1 + Puffer_Messpunkte;                      //Zählvariable für die neue Liste

            for (long i = 1 + Puffer_Messpunkte; i < (Binary_STD_Respons.Length - anzahl_Punkte_Mittelung_eine_Seite);)
            {
                //Punkte zusammenfassen********************************
                long summe = 0;
                //Summieren
                for (long a = i - anzahl_Punkte_Mittelung_eine_Seite; a <= i + anzahl_Punkte_Mittelung_eine_Seite; a++)
                    summe += (long)Binary_STD_Respons[a];
                //Mitteln
                summe /= anzahl_Punkte_Mittelung_gesamt;

                //In Liste übernehmen*********************************
                output.Add(
                    new TTA_DataPoint()
                    {
                        Time = (i + 1 - Puffer_Messpunkte) * periode,
                        Binary = (short)(summe),
                        Voltage = summe * faktor_voltage + offset_voltage,
                        Thermal_Impedance = 0,
                    }
                );

                //Datendichte überprüfen******************************
                //(notfalls schritte erhöhen) und Zählvariablen anpassen
                if (output[(int)Count_Var_outputList].Time / (output[(int)Count_Var_outputList].Time - output[(int)Count_Var_outputList - 1].Time) > Max_daten_dichte)
                {
                    //Zum nächsten Mittelpunkt springen
                    i += 2 * anzahl_Punkte_Mittelung_gesamt;

                    //Schrittweite erhöhen
                    anzahl_Punkte_Mittelung_gesamt *= 3;
                    //Punkte vor bzw. dannach neu berechnen
                    anzahl_Punkte_Mittelung_eine_Seite = (anzahl_Punkte_Mittelung_gesamt - 1) / 2;
                }
                else
                {
                    //Zum nächsen Mittelpunkt springen
                    i += anzahl_Punkte_Mittelung_gesamt;
                }
                Count_Var_outputList++;
            }
            //Liste ausgeben
            return output;
        }

        /// <summary>
        /// Export Powersteps, Correction-Vektors as .csv
        /// </summary>
        private bool Export_as_CSV()
        {
            //Powersteps
            using (var myCSV_Writer = new CsvWriter(Path.Combine(Output_File_Folder, Output_File_Name + "_PS.csv"), ';'))
            {
                //Hilfs-Array definieren
                decimal[,] helpArray = new decimal[PowerSteps_2Heat_DPA.Length, 4];

                for (int i = 0; i < PowerSteps_2Heat_DPA.Length; i++)
                {
                    helpArray[i, 0] = PowerSteps_2Heat_DPA[i];
                    helpArray[i, 1] = PowerSteps_2Sense_DPA[i];
                }

                helpArray[0, 2] = PowerStep_2Heat_STD;
                helpArray[0, 3] = PowerStep_2Sense_STD;

                //Settings ändern
                myCSV_Writer.Escape = ' ';
                myCSV_Writer.Quote = ' ';

                //Header & Daten
                myCSV_Writer.WriteHeaders(new string[] { "DPA_Heat", "DPA_Sense" , "STD Heat", "STD Sense"});
                myCSV_Writer.Write<decimal>(helpArray);
            }

            //Correction-Vektors
            //Powersteps
            using (var myCSV_Writer = new CsvWriter(Path.Combine(Output_File_Folder, Output_File_Name + "_CV.csv"), ';'))
            {
                //Hilfs-Array definieren
                decimal[,] helpArray = new decimal[CorrVec_B.Length, 2];

                for (int i = 0; i < CorrVec_A.Length; i++)
                    helpArray[i, 0] = CorrVec_A[i];

                for (int i = 0; i < CorrVec_B.Length; i++)
                    helpArray[i, 1] = CorrVec_B[i];

                //Settings ändern
                myCSV_Writer.Escape = ' ';
                myCSV_Writer.Quote = ' ';

                //Header & Daten
                myCSV_Writer.WriteHeaders(new string[] { "A", "B"});
                myCSV_Writer.Write<decimal>(helpArray);
            }



            return true;
        }

    }
}
