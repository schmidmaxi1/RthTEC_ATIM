using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using DAQ_Units;
using XYZ_Table;

using _8_Rth_TEC_Rack;

using Read_Coordinates;
using Hilfsfunktionen;

namespace ATIM_GUI._01_TTA
{
    public class TTA_measurement_new
    {

        //********************************************************************************************************************
        //                                    Variablen
        //********************************************************************************************************************

        public RthTEC_Rack MyRack { get; set; }
        public I_DAQ MyDAQ { get; set; }
        public I_XYZ MyXYZ { get; set; }
        public ATIM_MainWindow GUI { get; set; }

        public short[,] Binary_Raw_Files { get; set; }

        public UInt16 ErrorCode { get; set; }

        public decimal V_f_at_Imeas_Troom { get; set; } = 2;

        private long[] Start_to_Heat { get; set; }
        private long[] Heat_to_Meas { get; set; }
        private long[] Meas_to_End { get; set; }

        private long[] Average_Heat_Compleate { get; set; }
        private long[] Average_Meas_Compleate { get; set; }

        public List<TTA_DataPoint> Average_Heat_Compressed { get; set; }
        public List<TTA_DataPoint> Average_Meas_Compressed { get; set; }

        private static long Puffer_Messpunkte { get; } = 5; //Messpunkte vor eigentlichem Signal
        private static long Max_daten_dichte { get; } = 2000;


        //Datei-Namen usw.
        public string Output_File_Name { get; internal set; }
        public string Output_File_Folder { get; internal set; }

        public Movement_Infos MyMovement {get; internal set;}

        //********************************************************************************************************************
        //                                           Konstruktor
        //********************************************************************************************************************

        public TTA_measurement_new() { }

        //********************************************************************************************************************
        //                                        globale Funktionen
        //********************************************************************************************************************

        /// <summary>
        /// TTA measurment without XYZ (but with repetation cycles)
        /// </summary>
        /// <returns>Flag of status</returns>
        public bool Start_Single_TTA() {

            //Plots initisieren
            GUI.Graph_new_Measurment_for_TTA(this);

            //Status-Bar initialisieren
            GUI.StatusBar_TTA_Single(0, (int)MyRack.Cycles);

            //DAQ-System anpassen
            if (!MyDAQ.TTA_set_Device(MyRack.Time_Heat, MyRack.Time_Meas))
                return false;
            if (!MyDAQ.TTA_set_Trigger(MyRack.Gain, MyRack.U_offset))
                return false;
            
            //Definiere Feld für Raw-Daten
            Binary_Raw_Files = new short[MyRack.Cycles, MyDAQ.Samples];

            //Speicher reservieren
            MyDAQ.TTA_reserve_Storage(Binary_Raw_Files);

            //Loop für Wiederholungs-Zyklen
            for (int repetation_nr = 0; repetation_nr < MyRack.Cycles; repetation_nr++)
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

                MyRack.SinglePuls_withDelay();

                Thread.Sleep(300);


                //Daten abholen
                if (!MyDAQ.TTA_Collect_Data(Binary_Raw_Files, repetation_nr))
                    return false;

                //Daten prüfen
                //????????????????????????????????????????

                //Daten in Raw plotten
                GUI.Add_Series_to_RAW(this, repetation_nr);

                //Statusbar anpassen
                GUI.StatusBar_TTA_Single(repetation_nr + 1, (int)MyRack.Cycles);
            }

            //DatenSatz auswerten
            if (!Convert_Data())
                return false;

            //Plotten
            GUI.Add_Series_to_Data(this);
            GUI.Update_Voltage_Plots_for_TTA();

            //Speicher lösen
            MyDAQ.TTA_free_Storage(Binary_Raw_Files);

            return true;
        }

        /// <summary>
        /// TTA measurment with XYZ
        /// </summary>
        /// <returns></returns>
        public bool Start_Automatic_TTA()
        {
            //1. Pre-Settings ................................................................................
            //Anzahl LEDs für Counter
            int Nr_of_LEDs = MyMovement.MyMeasurment_Point.Length;

            //Initial-Name für File speicherung zwischenspeichen
            string initial_file_name = Output_File_Name;

            //Plots initisieren
            GUI.Graph_new_Measurment_for_TTA(this);

            //Status-Bar initialisieren
            GUI.StatusBar_TTA_all(0, (int)MyRack.Cycles, 0, Nr_of_LEDs);

            //DAQ-System anpassen
            if (!MyDAQ.TTA_set_Device(MyRack.Time_Heat, MyRack.Time_Meas))
                return false;
            if (!MyDAQ.TTA_set_Trigger(MyRack.Gain, MyRack.U_offset))
                return false;

            //Definiere Feld für Raw-Daten
            Binary_Raw_Files = new short[MyRack.Cycles, MyDAQ.Samples];

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
                GUI.StatusBar_TTA_all(0, (int)MyRack.Cycles, akt_DUT_Nr, Nr_of_LEDs);

                //Raw-Data plot leeren
                GUI.Graph_new_Measurment_for_TTA(this);

                //Drive XYZ down to LED
                MyXYZ.MoveADistance(0, 0, MyMovement.TouchDown_Hight - MyMovement.Driving_Hight, 0);

                //Loop für TTA Wiederholungs-Zyklen
                for (int repetation_nr = 0; repetation_nr < MyRack.Cycles; repetation_nr++)
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
                    MyRack.SinglePuls_withDelay();
                    Thread.Sleep(300);

                    //Daten abholen
                    if (!MyDAQ.TTA_Collect_Data(Binary_Raw_Files, repetation_nr))
                        return false;

                    //Daten prüfen
                    //????????????????????????????????????????

                    //Daten in Raw plotten
                    GUI.Add_Series_to_RAW(this, repetation_nr);

                    //Text StatusBar ändern
                    GUI.StatusBar_TTA_all(repetation_nr + 1, (int)MyRack.Cycles, akt_DUT_Nr, Nr_of_LEDs);
                }

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
        }

        //********************************************************************************************************************
        //                                              lokale Funktionen
        //********************************************************************************************************************

        #region lokaleFunktionen

        private Boolean Convert_Data()
        {
            //Switching Points suchen
            if (!Search_for_switching_points())
                return false;

            //Mitteln über Cyclen
            if (!Average_Heat_and_Meas_Parts())
                return false;

            //Daten komprimieren
            Average_Heat_Compressed = Compress_Data(Average_Heat_Compleate, (short)GUI.rthTEC_Rack1.Cycles);
            Average_Meas_Compressed = Compress_Data(Average_Meas_Compleate, (short)GUI.rthTEC_Rack1.Cycles);

            //Alles richtig
            return true;
        }

        /// <summary>
        /// Sucht die Umschaltpunkte in den Raw-Files in binärer Form
        /// </summary>
        /// <param name="return">true if no error occured; otherwise, false.</param>
        private Boolean Search_for_switching_points()
        {
            //Felder definiern
            Start_to_Heat = new long[MyRack.Cycles];
            Heat_to_Meas = new long[MyRack.Cycles];
            Meas_to_End = new long[MyRack.Cycles];

            //Suchkriterien definiern
            decimal schwelle_in_mV = MyDAQ.Trigger_Level_in_V * 1000;

            short schwelle_in_Bit = (short)((schwelle_in_mV) / MyDAQ.Range * (decimal)Math.Pow(2, 15));
            short hysterese = 3000;

            //Umschaltpunkte finden
            //Jede Messreihe (jeder Zyklus) wird nacheinander durchlaufen, um alle drei Umschaltpunkte zu finden
            for (int akt_Zyklus_Nr = 0; akt_Zyklus_Nr < this.MyRack.Cycles; akt_Zyklus_Nr++)
            {

                //Alle Messpunkte durchlaufen um die Umschaltpunkte zu finden
                long akt_Messpunkt = Puffer_Messpunkte + 1;     //Laufvariable

                //Umschaltpung: OFF --> Heat [Überspringen der Schwellspannung]
                //*******************************************************************************************

                //Try funktion um ein herrauslaufen des Indexes aus dem Feld zu vermeiden
                try
                {
                    while ((long)Binary_Raw_Files[akt_Zyklus_Nr, akt_Messpunkt] - (long)Binary_Raw_Files[akt_Zyklus_Nr, akt_Messpunkt - 1] < hysterese)
                    {
                        akt_Messpunkt++;
                    }
                    //Punkt Übernehmen
                    Start_to_Heat[akt_Zyklus_Nr] = akt_Messpunkt;

                    //long test = (long)Spectrum_raw_files[akt_Zyklus_Nr, akt_Messpunkt] - (long)Spectrum_raw_files[akt_Zyklus_Nr, akt_Messpunkt - 1];

                    //akt_Messpunkt weiterschieben um Einschwingen nicht zu untersuchen (1ms) [N = f * t]
                    akt_Messpunkt += ((long)MyDAQ.Frequency * 1 / 1000);
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Off to Heat not found", "Error");
                    return false;
                }


                //Umschaltpung: Heat --> Meas [Um Hysterese nach unten]
                //*******************************************************************************************
                try
                {

                    while ((long)Binary_Raw_Files[akt_Zyklus_Nr, akt_Messpunkt] - (long)Binary_Raw_Files[akt_Zyklus_Nr, akt_Messpunkt - 1] > -hysterese)
                    {
                        akt_Messpunkt++;

                    }

                    //Punkt Übernehmen
                    Heat_to_Meas[akt_Zyklus_Nr] = akt_Messpunkt;

                    //akt_Messpunkt weiterschieben um Einschwingen nicht zu untersuchen (1ms) [N = f * t]
                    akt_Messpunkt += ((long)MyDAQ.Frequency * 1 / 1000);

                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Heat to Meas not Found", "Error");
                    return false;
                }


                //Umschaltpung: Meas --> Off [Überspringen der Schwellspannung]
                //*******************************************************************************************
                try
                {

                    while (akt_Messpunkt < Binary_Raw_Files.GetLength(1) - 1 &
                        (long)Binary_Raw_Files[akt_Zyklus_Nr, akt_Messpunkt] - (long)Binary_Raw_Files[akt_Zyklus_Nr, akt_Messpunkt - 1] > -hysterese)
                    {
                        akt_Messpunkt++;
                    }
                    //Punkt Übernehmen
                    Meas_to_End[akt_Zyklus_Nr] = akt_Messpunkt;
                    var test = (long)Binary_Raw_Files[akt_Zyklus_Nr, akt_Messpunkt];
                }
                catch (IndexOutOfRangeException)
                {
                    Meas_to_End[akt_Zyklus_Nr] = Binary_Raw_Files.GetLength(1) - 1;
                    MessageBox.Show("Meas to Off not Found", "Error");
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Bildet den Mittelwert aus den einzelnen Cycles in binärer Form
        /// Vorsicht Werte noch nicht durch Cycles geteilt (Auflösung höher)
        /// </summary>
        /// <param name="return">true if no error occured; otherwise, false.</param>
        private Boolean Average_Heat_and_Meas_Parts()
        {
            //Die average Felder dürfen nur die Länge des kürzesten Messfelds haben
            long laenge_heat = long.MaxValue;
            long laenge_cool = long.MaxValue;

            for (int i = 0; i < MyRack.Cycles; i++)
            {
                if (Heat_to_Meas[i] - Start_to_Heat[i] < laenge_heat)
                    laenge_heat = Heat_to_Meas[i] - Start_to_Heat[i];

                if (Meas_to_End[i] - Heat_to_Meas[i] < laenge_cool)
                    laenge_cool = Meas_to_End[i] - Heat_to_Meas[i];
            }

            //Felder initialisieren 
            Average_Heat_Compleate = new long[laenge_heat + Puffer_Messpunkte];
            Average_Meas_Compleate = new long[laenge_cool + Puffer_Messpunkte];


            //Mittelwert HEAT berechnen (Eingetentlich nur die Summe bilden ohne geteilt)
            /*for (long akt_Messpunkt = 0; akt_Messpunkt < laenge_heat + puffer_Messpunkte; akt_Messpunkt++)
            {
                //Alle Messzyklen addiern
                for (int akt_Zyklus = 0; akt_Zyklus < MyRack.Cycles; akt_Zyklus++)
                    Average_Heat[akt_Messpunkt] += Spectrum_raw_files[akt_Zyklus, akt_Messpunkt + Start_to_Heat[akt_Zyklus] - puffer_Messpunkte];                
            }*/

            for (long akt_Messpunkt = -Puffer_Messpunkte; akt_Messpunkt < laenge_heat; akt_Messpunkt++)
            {
                //Alle Messzyklen addiern
                for (int akt_Zyklus = 0; akt_Zyklus < MyRack.Cycles; akt_Zyklus++)
                    Average_Heat_Compleate[akt_Messpunkt + Puffer_Messpunkte] += Binary_Raw_Files[akt_Zyklus, akt_Messpunkt + Start_to_Heat[akt_Zyklus]];
            }


            //Mittelwert Mess berechnen (Eingetentlich nur die Summe bilden ohne geteilt)
            for (long akt_Messpunkt = -Puffer_Messpunkte; akt_Messpunkt < laenge_cool; akt_Messpunkt++)
            {
                //Alle Messzyklen addiern
                for (int akt_Zyklus = 0; akt_Zyklus < MyRack.Cycles; akt_Zyklus++)
                    Average_Meas_Compleate[akt_Messpunkt + Puffer_Messpunkte] += Binary_Raw_Files[akt_Zyklus, akt_Messpunkt + Heat_to_Meas[akt_Zyklus]];

            }

            return true;
        }

        /// <summary>
        /// Daten Komprimieren & Umrechnen (& Zeit-Achse hinzufügen)
        /// </summary>
        /// <param name="return">returns Liste with TTA_DataPoints; otherwise, null.</param>
        private List<TTA_DataPoint> Compress_Data(long[] input, short cycles)
        {
            //Temporäre Liste für Output generieren
            List<TTA_DataPoint> output = new List<TTA_DataPoint>();

            //Parameter für abspeicherung
            decimal periode = 1m / MyDAQ.Frequency;


            decimal faktor_voltage = 1m / cycles / MyRack.Gain * MyDAQ.Range / 1000 / (decimal)Math.Pow(2, 15);
            decimal offset_voltage = MyRack.U_offset / 1000;


            //Die ersten Werte (vor Flanke) mit Zeit 0 in Liste ablegen
            for (int i = 0; i < Puffer_Messpunkte; i++)
            {
                output.Add(
                    new TTA_DataPoint()
                    {
                        Time = 0,
                        Binary = (short)(input[i] / cycles),
                        Voltage = input[i] * faktor_voltage + offset_voltage,
                        Thermal_Impedance = 0,
                    }
                );
            }

            //Ersten wirklichen Punkt in Liste Aufnehmen
            output.Add(
                new TTA_DataPoint()
                {
                    Time = periode,
                    Binary = (short)(input[Puffer_Messpunkte] / cycles),
                    Voltage = input[Puffer_Messpunkte] * faktor_voltage + offset_voltage,
                    Thermal_Impedance = 0,
                }
            );


            //Zählvariablen
            int akt_komp_Faktor = 1;        //Indikator der Mittelbreite
            long j = 1 + Puffer_Messpunkte;                      //Zählvariable für die neue Liste

            for (long i = 1 + Puffer_Messpunkte; i < (input.Length - (akt_komp_Faktor - 1) / 2);)
            {
                //Punkte zusammenfassen

                //anzahl_punkte entspricht der Anzahl vor bzw. nach dem eigentlichen messpunkt
                long anzahl_punkte = (akt_komp_Faktor - 1) / 2;
                long summe = 0;
                //Summieren
                for (long a = i - anzahl_punkte; a <= i + anzahl_punkte; a++)
                {
                    summe += input[i];
                }
                //Teilen
                summe /= (anzahl_punkte * 2) + 1;

                //Test
                if (output.Count > 100)
                    if (output[output.Count - 2].Voltage - output[output.Count - 1].Voltage > 0.01m)
                    {
                        decimal Time = (i + 1 - Puffer_Messpunkte) * periode;
                        decimal Binary = (short)(summe / cycles);
                        decimal Voltage = summe * faktor_voltage + offset_voltage;
                    }

                //Neue Werte eintragen wenn noch genügent übrig sind   
                output.Add(
                    new TTA_DataPoint()
                    {
                        Time = (i + 1 - Puffer_Messpunkte) * periode,
                        Binary = (short)(summe / cycles),
                        Voltage = summe * faktor_voltage + offset_voltage,
                        Thermal_Impedance = 0,
                    }
                );



                //Datendichte überprüfen (notfalls schritte erhöhen) und Zählvariablen anpassen
                if (output[(int)j].Time / (output[(int)j].Time - output[(int)j - 1].Time) > Max_daten_dichte)
                {
                    i += 2 * akt_komp_Faktor;              //halb überspringen
                    akt_komp_Faktor *= 3; //Erhöhen
                }
                else
                {
                    i += akt_komp_Faktor;
                }
                j++;
            }

            return output;
        }

        #endregion lokaleFunktionen

        //********************************************************************************************************************
        //                                             Save
        //********************************************************************************************************************

        #region Save

        //1. Main-Funktion zu speichern***********************************************
        public void Save_AllFiles()
        {
            //1. Average files
            if (GUI.mySettings.Save_Aver_Heat)
                Save_SingleFile(Average_Heat_Compressed, "Average of " + GUI.rthTEC_Rack1.Cycles.ToString(), ".aver.TTAheat");

            if (GUI.mySettings.Save_Aver_Cool)
                Save_SingleFile(Average_Meas_Compressed, "Average of " + GUI.rthTEC_Rack1.Cycles.ToString(), ".aver.TTAcool");

            //2. Single files
            if (GUI.mySettings.Save_Signle_Cool | GUI.mySettings.Save_Single_Heat)
            {
                //Felder für einzelne Listen definieren
                List<TTA_DataPoint>[] all_single_heating_compressed = new List<TTA_DataPoint>[GUI.rthTEC_Rack1.Cycles];
                List<TTA_DataPoint>[] all_single_cooling_compressed = new List<TTA_DataPoint>[GUI.rthTEC_Rack1.Cycles];

                //Daten aus Binary_Raw_Files herauslösen und komprimieren
                for (int i = 1; i <= GUI.rthTEC_Rack1.Cycles; i++)
                {
                    //Raw Daten aus gesamten Feld herauslösen
                    long[] help_heat = new long[Heat_to_Meas[i - 1] - Start_to_Heat[i - 1] + Puffer_Messpunkte];
                    long[] help_meas = new long[Meas_to_End[i - 1] - Heat_to_Meas[i - 1] + Puffer_Messpunkte];

                    //Mit daten befüllen
                    for (long akt_Messpunkt = -Puffer_Messpunkte; akt_Messpunkt < help_heat.Length - Puffer_Messpunkte; akt_Messpunkt++)
                        help_heat[akt_Messpunkt + Puffer_Messpunkte] = Binary_Raw_Files[i - 1, akt_Messpunkt + Start_to_Heat[i - 1]];

                    for (long akt_Messpunkt = -Puffer_Messpunkte; akt_Messpunkt < help_meas.Length - Puffer_Messpunkte; akt_Messpunkt++)
                        help_meas[akt_Messpunkt + Puffer_Messpunkte] += Binary_Raw_Files[i - 1, akt_Messpunkt + Heat_to_Meas[i - 1]];

                    //Komprimieren
                    all_single_heating_compressed[i - 1] = Compress_Data(help_heat, 1);
                    all_single_cooling_compressed[i - 1] = Compress_Data(help_meas, 1);
                }

                if (GUI.mySettings.Save_Single_Heat)
                    for (int i = 1; i <= GUI.rthTEC_Rack1.Cycles; i++)
                        Save_SingleFile(all_single_heating_compressed[i - 1],
                            "Cycle " + i.ToString() + "/" + GUI.rthTEC_Rack1.Cycles.ToString(), "." + i.ToString("0000") + ".TTAheat");

                if (GUI.mySettings.Save_Signle_Cool)
                    for (int i = 1; i <= GUI.rthTEC_Rack1.Cycles; i++)
                        Save_SingleFile(all_single_cooling_compressed[i - 1],
                            "Cycle " + i.ToString() + "/" + GUI.rthTEC_Rack1.Cycles.ToString(), "." + i.ToString("0000") + ".TTAcool");
            }

            //3. Raw files
            if (GUI.mySettings.Save_Raw)
                for (int i = 1; i <= GUI.rthTEC_Rack1.Cycles; i++)
                {
                    //Daten einer Messung herauslösen
                    short[] help_raw = new short[Binary_Raw_Files.GetLength(1)];
                    for (int j = 0; j < help_raw.Length; j++)
                        help_raw[j] = Binary_Raw_Files[i - 1, j];
                    //Speichern
                    Save_SingleFile(help_raw, "Raw_Data of cycle" + i.ToString() + "/" + GUI.rthTEC_Rack1.Cycles.ToString() + GUI.rthTEC_Rack1.Cycles.ToString(),
                        "." + i.ToString("0000") + ".TTAraw");
                }
        }

        //2. Speichern verschiedener FileTypen******************************************

        private void Save_SingleFile(List<TTA_DataPoint> input, string type, string ending)
        {
            //Pfad erzeugen
            string file = Path.Combine(Output_File_Folder, Output_File_Name + ending);

            // Neues Stream - Write erzeugen (false bedeutet, das die Datei überschrieben wird)
            using (StreamWriter output_File = new StreamWriter(file, false))
            {
                //Header
                output_File.WriteLine(GenerateHeader(type));

                //MessWerte schreiben                   
                //Satz definieren mit Tab und definierter 0-Stellen Anzahl
                //0.000000  0.000000 (Erst Zeit, dann Spannung)
                foreach (TTA_DataPoint point in input)
                    output_File.WriteLine(point.Time.ToString("0.0000000") + "\t" + Math.Round(point.Voltage, 6).ToString("0.000000"));

                //Schließen
                output_File.Close();
            }
        }

        private void Save_SingleFile(short[] input, string type, string ending)
        {
            //Pfad erzeugen
            string file = Path.Combine(Output_File_Folder, Output_File_Name + ending);

            // Neues Stream - Write erzeugen (false bedeutet, das die Datei überschrieben wird)
            using (StreamWriter output_File = new StreamWriter(file, false))
            {
                //Header
                output_File.WriteLine(GenerateHeader(type));

                //MessWerte schreiben                   
                //Satz definieren mit Tab und definierter 0-Stellen Anzahl
                //0.000000  0.000000 (Erst Zeit, dann Spannung)
                foreach (short point in input)
                    output_File.WriteLine(point.ToString());

                //Schließen
                output_File.Close();
            }
        }

        //3. TeilInfos generieren*******************************************************

        #region Generate parts of Header

        private string GenerateHeader(string typ)
        {
            //Platzhalten für newLine
            string newLine = Environment.NewLine;

            //Header erzeugen
            return
                "#################################################################################" + newLine +
                "#Time Stamp:       " + DateTime.Now.ToString("dd.MM.yyyy   HH:mm:ss") + newLine +
                "#Equipment:        " + GUI.rthTEC_Rack1.DeviceType + " & " + /*GUI.DAQ_Unit.Name +*/ newLine +
                "#Datei-Typ:        " + typ + newLine +
                "#I_heat_current:   " + GUI.rthTEC_Rack1.I_Heat.ToString() + " mA" + newLine +
                "#t_heat_puls:      " + GUI.rthTEC_Rack1.Time_Heat.ToString() + " ms" + newLine +
                "#V_heat_voltage:   " + Calculate_HeatVoltage(out decimal my_VHeat) + newLine +
                "#I_meas_current:   " + GUI.rthTEC_Rack1.I_Meas.ToString() + " mA" + newLine +
                "#t_meas_puls:      " + GUI.rthTEC_Rack1.Time_Meas.ToString() + " ms" + newLine +
                "#V_meas_voltage:   " + Calculate_MeasVoltage(out decimal my_VMeas) + newLine +
                "#f_sampling:       " + GUI.myDAQ.Frequency.ToString() + " Hz" + newLine +
                "#Sensitivity:      " + Sensitiviy_from_File() + newLine +
                "#Temperature:      " + Generate_Temperature_String() + newLine +
                "#Power-Step:       " + CalculatePowerStep(my_VHeat, my_VMeas) + newLine +
                "#V@I_meas&T_sink:  " + V_f_at_Imeas_Troom.ToString("#.####") + " V" + newLine +
                "#################################################################################";
        }

        private string Sensitiviy_from_File()
        {
            //Default Werte definieren
            decimal sensitivity = 0;
            decimal rsquared = 0;

            //Pfad erzeugen
            string file = Path.Combine(Output_File_Folder, Output_File_Name + ".sen");

            //Versuchen zu öffnen
            try
            {
                //Öffnen
                string[] text = File.ReadAllLines(file);

                //Auslesen
                for (int i = 0; i < text.Length; i++)
                {
                    string line = text[i];

                    if (line.StartsWith("#Sensitivity:")) //Als Erkennung des Headers
                    {
                        var columns = line.Split(' '); //Leerzeichen als Trennzeichen
                        sensitivity = Convert.ToDecimal(columns[4]);
                    }

                    if (line.StartsWith("#Quality (R2):")) //Als Erkennung des Headers
                    {
                        var columns = line.Split(' '); //Leerzeichen als Trennzeichen
                        rsquared = Convert.ToDecimal(columns[4]);
                    }
                }
                return sensitivity.ToString() + " mV/K" + " (R2: " + rsquared.ToString() + ")";
            }
            catch (FileNotFoundException)
            {
                //Wenn File nicht vorhanden ist dann default Value verwenden
                sensitivity = GUI.mySettings.Default_Sensitivity;
                return sensitivity.ToString() + " mV/K" + " (default used)";
            }

        }

        private string Calculate_HeatVoltage(out decimal v_Heat)
        {
            if (GUI.rthTEC_Rack1.DUT_Type == "LED")
            {
                v_Heat = Average_Heat_Compressed[Average_Heat_Compressed.Count - 1].Voltage;
                return Average_Heat_Compressed[Average_Heat_Compressed.Count - 1].Voltage.ToString("#.###") + " V";
            }
            else if (GUI.rthTEC_Rack1.DUT_Type == "MOSFET")
            {
                v_Heat = 10.0m;
                return "10.0";
            }
            else
            {
                v_Heat = 5.2m;
                return "3.0";
            }
        }

        private string Calculate_MeasVoltage(out decimal v_Meas)
        {
            if (GUI.rthTEC_Rack1.DUT_Type == "LED")
            {
                v_Meas = Average_Meas_Compressed[Average_Meas_Compressed.Count - 1].Voltage;
                return Average_Meas_Compressed[Average_Meas_Compressed.Count - 1].Voltage.ToString("#.###") + " V";
            }
            else if (GUI.rthTEC_Rack1.DUT_Type == "MOSFET")
            {
                v_Meas = 10.0m;
                return "10.0";
            }
            else
            {
                v_Meas = 5.2m;
                return "3.0";
            }
        }

        private string Generate_Temperature_String()
        {
            //Wenn alle an
            if (GUI.myTEC.IsRunning)
                return GUI.myTEC.Target_temp_aver.ToString() + " °C";
            else
                return "Not all Elements switched on";
        }

        private string CalculatePowerStep(decimal v_Heat, decimal v_Meas)
        {
            return ((v_Heat * GUI.rthTEC_Rack1.I_Heat - v_Meas * GUI.rthTEC_Rack1.I_Meas) / 1000).ToString("#.###") + " W";
        }

        #endregion Generate parts of Header

        #endregion Save
    }
}
