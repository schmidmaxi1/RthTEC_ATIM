using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEC_Controller
{
    public interface I_TEC_Controller
    {
        //***********************Variablen********************************************
        bool IsConnected { get; }
        bool IsRunning { get; }

        bool Stable_for_30sec { get; }
        int Counter_0_5sec { get; } 

        float Target_temp_aver { get; }
        float Meas_temp_aver { get; }


        //***********************Functions********************************************
        void SetTemperature(float newTemp);
        void SetTemperature_w_TimerStop(float newTemp);

        void Switch_Channel_OnOff(bool switch_on);
        void Switch_Fan_OnOff(bool switch_on);

        void Change_Enabled(Boolean input);

        //string AutoOpen(Load_Screen myLoadScreen);

    }
}
