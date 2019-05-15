using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIM_GUI._3_Project
{

    public class Messung_Info
    {
        
        
        public string Kat1 { get; set; }
        public string Kat2 { get; set; }
        public string Kat3 { get; set; }
        public string Kat4 { get; set; }

        public string Name { get; set; }

        public uint Anzahl_Prozent { get; set; }
        public uint Error_Prozent { get; set; }


        public Messung_Info() { }


    }
}
