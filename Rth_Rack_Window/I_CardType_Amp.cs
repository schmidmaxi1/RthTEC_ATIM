using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RthTEC_Rack
{
    /// <summary>
    /// This interface is used to group all Amplifieres/Frontends
    /// </summary>
    public interface I_CardType_Amp
    {
        decimal Gain { get; }

        /// <summary>
        /// Offset Voltage in mV
        /// </summary>
        decimal V_Offset { get; }
    }
}
