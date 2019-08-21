using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RthTEC_Rack
{
    /// <summary>
    /// This interface is used to group all Power-Cards (LED-Sourc, MOSFET-Source)
    /// </summary>
    public interface I_CardType_Power
    {
        /// <summary>
        /// Type of DUT (e.g. Diode, MOSFET)
        /// </summary>
        string DUT_Type { get; }

        /// <summary>
        /// Heating Current in mA
        /// </summary>
        decimal I_Heat { get; }

        /// <summary>
        /// Measuring Current in mA
        /// </summary>
        decimal I_Meas { get; }

        /// <summary>
        /// Heating Voltage in mV
        /// </summary>
        decimal V_Heat { get; }

        /// <summary>
        /// Measuring Voltage in mV
        /// </summary>
        decimal V_Meas { get; }
    }
}
