using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DXApplication3._10_Camera
{
    public class Detected_Object
    {
        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        //Variablen die alle Unterklassen/Kinder haben müssen
        public PointF Mittelpunkt_in_mm { get; set; }
        public PointF Mittelpunkt_in_Pixel { get; set; }

        //4,5µm/Pixel * 2[Zoom 0,5]
        public float Umrechnung_Pixel_to_mm { get; } = 0.0045f * 2;
        public PointF ImageMittelpunkt { get; } = new PointF(1600 / 2, 1200 / 2);

        //**************************************************************************************************
        //                                          Funktionen
        //**************************************************************************************************

        //Funktionen die in den Unterklassen überschreiben werden
        public virtual void DrawInGraphic(Pen myPen, Graphics input)
        {
        }

    }
}
