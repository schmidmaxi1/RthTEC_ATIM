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
        long Range { get; set; }
        string[] RangeList { get; }

        long Frequency { get; set; }
        string[] FrequencyList { get; }

        long Trigger_Level_UI { get; }

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
        bool Sensitivity_Set_Device();
        bool Sensitivity_Set_Trigger();
        bool Sensitivity_Measure_and_Collect_Data(short[] output);


        //GUI
        void Change_Enabled(Boolean input);

        void Update_settings(NI_CommunicationDevice myInput);
        void Update_settings(EthernetCommunicationDevice myInput);

        string AutoOpen(AutoConnect_Window myLoadScreen);
    }
}
