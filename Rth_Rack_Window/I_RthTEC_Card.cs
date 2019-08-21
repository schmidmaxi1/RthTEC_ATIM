using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RthTEC_Rack
{
    public interface I_RthTEC_Card
    {

        Boolean IsActiv { get; set; }
        int Slot_Nr { get; set; }

        void Add_to_DetailedForm(Window_RthTEC_Rack myDetailed, int x, int y);


    }
}
