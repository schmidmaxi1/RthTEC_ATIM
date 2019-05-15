using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIM_GUI._4_Settings
{
    public class Settings_ATIM
    {
        //Eigenschaften TTA
        public string Device { get; set; }
        public decimal Heat_time { get; set; }
        public decimal Heat_current { get; set; }
        public decimal Heat_voltage { get; set; }
        public decimal Meas_time { get; set; }
        public decimal Meas_current { get; set; }
        public decimal Meas_voltage { get; set; }
        public decimal Offset_voltage { get; set; }
        public UInt16 Cycles { get; set; }

        //Eigenschaten Temperature
        public decimal Temperature_TTA { get; set; }
        public decimal Default_Sensitivity { get; set; }
        public bool TEC_OnOff { get; set; }
        public bool FAN_Enable { get; set; }

        public decimal Temperature_SEN_Start { get; set; }
        public decimal Temperature_SEN_End { get; set; }
        public UInt16 Temperature_SEN_Step { get; set; }

        //Eigenschaften Save
        public bool Save_Aver_Cool { get; set; }
        public bool Save_Aver_Heat { get; set; }
        public bool Save_Signle_Cool { get; set; }
        public bool Save_Single_Heat { get; set; }
        public bool Save_Raw { get; set; }

        public string Save_Folder { get; set; }
        public string Save_FileName { get; set; }

        //Eigenschaften XYZ
        public string GerberFile_Path { get; set; }

        //Eigenschaften Spectrum
        public string Spectrum_range { get; set; }
        public string Spectrum_frequency { get; set; }
        public decimal Spectrum_triggerLevel { get; set; }


        //**************************************************************************************************
        //                                      Konstruktor
        //**************************************************************************************************

        public Settings_ATIM() { }

        public Settings_ATIM Take_default_settings()
        {
            return new Settings_ATIM()
            {
                Default_Sensitivity = -2,
            };
        }

        //**************************************************************************************************
        //                                      Funktionen
        //**************************************************************************************************

        public void Save(string path)
        {
            System.IO.File.WriteAllText(path, Generate_TXT_file());
        }

        public void Open(string path)
        {
            // Open the stream and read it back.
            using (System.IO.StreamReader sr = System.IO.File.OpenText(path))
            {
                //Jede Zeile nacheinander öffenen
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string command;
                    string info;
                    //Versuchen Command und Info rauszulösen
                    try
                    {
                        command = line.Substring(0, 34).Trim(' ');
                        info = line.Substring(34);

                        switch (command) {

                            case "Device Type:":
                                Device = info;
                                break;
                            case "Heat Time:":
                                Heat_time =  Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Heat Current:":
                                Heat_current = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Heat Voltage:":
                                Heat_voltage = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Meas Time:":
                                Meas_time = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Meas Current:":
                                Meas_current = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Meas Voltage:":
                                Meas_voltage = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Offset Voltage:":
                                Offset_voltage = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Number of Cycles:":
                                Cycles = Convert.ToUInt16(info);
                                break;


                            case "Temperature for TTA:":
                                Temperature_TTA = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Default sensitivity:":
                                Default_Sensitivity = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "TEC is switched on:":
                                TEC_OnOff = Convert.ToBoolean(info);
                                break;
                            case "FAN is enabled:":
                                FAN_Enable = Convert.ToBoolean(info);
                                break;
                            case "Temperature Sensitivity Start:":
                                Temperature_SEN_Start = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Temperature Sensitivity End:":
                                Temperature_SEN_End = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            case "Temperature Sensitivity Steps:":
                                Temperature_SEN_Step = Convert.ToUInt16(info);
                                break;


                            case "Average cooling file(s):":
                                Save_Aver_Cool = Convert.ToBoolean(info);
                                break;
                            case "Average heating file(s):":
                                Save_Aver_Heat = Convert.ToBoolean(info);
                                break;
                            case "Raw file(s):":
                                Save_Raw = Convert.ToBoolean(info);
                                break;
                            case "Single heating file(s):":
                                Save_Single_Heat = Convert.ToBoolean(info);
                                break;
                            case "Single cooling file(s):":
                                Save_Signle_Cool = Convert.ToBoolean(info);
                                break;

                            case "Folder name for file(s):":
                                Save_Folder = info;
                                break;
                            case "File name:":
                                Save_FileName = info;
                                break;

                            case "Path for gerber file:":
                                GerberFile_Path = info;
                                break;


                            case "Spectrum meas. range:":
                                Spectrum_range = info;
                                break;
                            case "Spectrum sample frequency:":
                                Spectrum_frequency = info;
                                break;
                            case "Spectrum trigger level:":
                                Spectrum_triggerLevel = Convert.ToDecimal(info.Substring(0, info.LastIndexOf(" ")));
                                break;
                            default:
                                break;
                        }


                    }
                    catch (Exception)
                    {
                        //Zeile zu kurz --> Keine Infozeile
                    }

                }
            }
        }

        private string Generate_TXT_file()
        {
            string newLine = Environment.NewLine;

            return
                "Please don't change me! Especially when you don't know how I work!" + newLine +
                "##################################################################" + newLine +
                "Date of Edit/Creat:               " + DateTime.Now.ToString("dd.MM.yyyy  H:mm:ss") + newLine +
                "##################################################################" + newLine +
                "TTA:" + newLine +
                "Device Type:                      " + Device + newLine +
                "Heat Time:                        " + Heat_time + " ms" + newLine +
                "Heat Current:                     " + Heat_current.ToString() + " mA" + newLine +
                "Heat Voltage:                     " + Heat_voltage.ToString() + " V" + newLine +
                "Meas Time:                        " + Meas_time.ToString() + " ms" + newLine +
                "Meas Current:                     " + Meas_current.ToString() + " mA" + newLine +
                "Meas Voltage:                     " + Meas_voltage.ToString() + " V" + newLine +
                "Offset Voltage:                   " + Offset_voltage.ToString() + " V" + newLine +
                "Number of Cycles:                 " + Cycles.ToString()  + newLine +
                "##################################################################" + newLine +
                "Temperature:" + newLine +
                "Temperature for TTA:              " + Temperature_TTA.ToString() + " °C" + newLine +
                "Default sensitivity:              " + Default_Sensitivity.ToString() + " mV/K" + newLine +
                "TEC is switched on:               " + TEC_OnOff.ToString()  + newLine +
                "FAN is enabled:                   " + FAN_Enable.ToString()  + newLine +
                "Temperature Sensitivity Start:    " + Temperature_SEN_Start.ToString() + " °C" + newLine +
                "Temperature Sensitivity End:      " + Temperature_SEN_End.ToString() + " °C" + newLine +
                "Temperature Sensitivity Steps:    " + Temperature_SEN_Step.ToString() + newLine +
                "##################################################################" + newLine +
                "Which files should be saved:      " + newLine +
                "Average cooling file(s):          " + Save_Aver_Cool.ToString()  + newLine +
                "Average heating file(s):          " + Save_Aver_Heat.ToString()  + newLine +
                "Single cooling file(s):           " + Save_Signle_Cool.ToString() + newLine +
                "Single heating file(s):           " + Save_Single_Heat.ToString() + newLine +
                "Raw file(s):                      " + Save_Raw.ToString()  + newLine +
                "Folder name for file(s):          " + Save_Folder.ToString() + newLine +
                "File name:                        " + Save_FileName.ToString() + newLine +         
                "##################################################################" + newLine +
                "GerberFile Infos:" + newLine +
                "Path for gerber file:             " + GerberFile_Path  + newLine +
                "##################################################################" + newLine +
                "Spectrum settings:" + newLine +
                "Spectrum meas. range:             " + Spectrum_range + newLine +
                "Spectrum sample frequency:        " + Spectrum_frequency + newLine +
                "Spectrum trigger level:           " + Spectrum_triggerLevel.ToString() + " V" + newLine +
                "##################################################################";
        }

    }
}
