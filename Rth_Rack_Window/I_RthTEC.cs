using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rth_Rack_Window
{

    /// <summary>
    /// Interface für verschiedene Versionen vom RthTEC Rack
    /// </summary>
    public interface I_RthTEC
    {

        //Variablen

        string ID { get;  }
        string Communication_LOG { get;  }

        Boolean IsConnected { get;  }
        Boolean IsEnabled { get;  }

        int Slot_Count { get; }
        char[] SlotBelegung { get; set; } 
        I_RthTEC_Card[] Cards { get; set; }



        decimal Time_Heat { get;  }
        decimal Time_Meas { get;  }
        decimal DPA_Time { get;   }
        Int32 DPA_Count { get;  }

        //Funktionen

        string Write_N_Read(string message);

        void Write_Only(string message);

        //1. Enable
        string SetEnable(bool input);

        //2. Start Pulses
        string Start_std_Pulse(bool input);
        string Start_SEN_Pulse(bool input);
        string Start_DPA_Pulse(bool input);
        string Start_Pre_Pulse(bool input);

        //3. Puls Einstellungen
        string SetHeatTime(Decimal input);
        string SetMeasTime(Decimal input_mess, Decimal input_heiz);
        string SetDPACount(Int32 input);
        string SetDPATime(Decimal input);
        string SetSlotActivation(bool input, int slot_nr);


    }
}
