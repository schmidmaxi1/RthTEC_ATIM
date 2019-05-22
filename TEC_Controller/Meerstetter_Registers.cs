using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEC_Controller
{
    public class Meerstetter_Registers
    {
        public string registerName;
        public string iniFileName;
        public ushort registerNumber;
        public bool isInteger;
        public Int32 valueInt;
        public float valueFloat;
        public byte channel;

        public Meerstetter_Registers(ushort number, byte ch, string name, string ini, bool intOrFloat)
        {
            registerName = name;
            iniFileName = ini;
            registerNumber = number;
            isInteger = intOrFloat;
            valueInt = 0;
            valueFloat = 0;
            channel = ch;
        }
    }
}
