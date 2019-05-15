using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIM_GUI
{
    public class TTA_DataPoint
    {
        //Informationen in einem Punkt
        public short Binary { get; internal set; }
        public decimal Voltage { get; internal set; }
        public decimal Thermal_Impedance { get; internal set; }
        public decimal Time { get; internal set; }

    }
}
