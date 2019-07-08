using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoConnect;
using Communication_Settings;

namespace DAQ_Units
{
    public interface I_DAQ
    {

        //***********************Variablen********************************************

        Boolean IsConnected { get;}

        //Mess-Parameter

        /// <summary>
        /// Half Range in mV [1000 := +-1V]
        /// </summary>
        long Range { get; set; }
        /// <summary>
        /// List of all possible Ranges as string
        /// </summary>
        string[] RangeList { get; }


        /// <summary>
        /// Sample frequency in Hz
        /// </summary>
        long Frequency { get; set; }
        /// <summary>
        /// List of all possible sample frequencys as string
        /// </summary>
        string[] FrequencyList { get; }

        /// <summary>
        /// Real Trigger Level of DAQ (after Front-End)
        /// </summary>
        decimal Trigger_Level_in_V { get; }

        /// <summary>
        /// Number of samples for measurement
        /// </summary>
        long Samples { get; set; }

        //Log
        string Communication_LOG { get;  }


        //***********************Functions********************************************

        //TTA
        bool TTA_set_Device(decimal t_heat_ms, decimal t_meas_ms);
        bool TTA_set_Trigger(decimal frontend_gain, decimal forntend_offset);
        bool TTA_wait_for_Trigger();
        bool TTA_Collect_Data(short[,] data_out, int cycle);

        bool TTA_reserve_Storage(short[,] array);
        bool TTA_free_Storage(short[,] array);

        //Sensitivity
        bool Sensitivity_Set_Device(short[] array, long nr_of_samples);
        bool Sensitivity_Set_Trigger();
        bool Sensitivity_Measure_and_Collect_Data(short[] output);


        //GUI
        void Change_ADR(string adr);

        void Change_Enabled(Boolean input);

        void Update_settings(NI_CommunicationDevice myInput);
        void Update_settings(EthernetCommunicationDevice myInput);

        string AutoOpen(AutoConnect_Window myLoadScreen);

        void Close();
    }
}
