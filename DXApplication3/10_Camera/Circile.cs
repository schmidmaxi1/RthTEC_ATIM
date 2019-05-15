using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DXApplication3._10_Camera
{
    public class Circile:Detected_Object
    {
        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        public float Radius_in_mm { get; set; }
        public float Radius_in_Pixel { get; set; }

        //**************************************************************************************************
        //                                          Kunstruktor
        //**************************************************************************************************

        public Circile(){ }

        public Circile(AForge.Point mittelPunkt, float radius)
        {
            Mittelpunkt_in_Pixel = new PointF(mittelPunkt.X, mittelPunkt.Y);
            Radius_in_Pixel = radius;

            //Umrechnen
            Radius_in_mm = Radius_in_Pixel * Umrechnung_Pixel_to_mm;

            Mittelpunkt_in_mm = new PointF(
                (Mittelpunkt_in_Pixel.X - ImageMittelpunkt.X) * Umrechnung_Pixel_to_mm,
                (Mittelpunkt_in_Pixel.Y - ImageMittelpunkt.Y) * Umrechnung_Pixel_to_mm);
        }

        //**************************************************************************************************
        //                                           ToString()
        //**************************************************************************************************

        public override string ToString()
        {
            string newLine = Environment.NewLine;
            return "Circile:" + newLine + 
                "  Center: X " + Mittelpunkt_in_mm.X.ToString() + " mm; Y " + Mittelpunkt_in_mm.Y.ToString() + " mm" + newLine +
                "  Radius:" + Radius_in_mm.ToString() + " mm" + newLine;
        }

        //**************************************************************************************************
        //                                         DrawInGraphic()
        //**************************************************************************************************

        public override void DrawInGraphic(Pen myPen, Graphics input)
        {
            //Kreis zeichnen
            input.DrawEllipse(myPen, (Mittelpunkt_in_Pixel.X - Radius_in_Pixel),
                                        (Mittelpunkt_in_Pixel.Y - Radius_in_Pixel),
                                        (Radius_in_Pixel * 2),
                                        (Radius_in_Pixel * 2));

            //Mittelpunkt markieren
            input.DrawLine(myPen, (Mittelpunkt_in_Pixel.X - Radius_in_Pixel / 3),
                                    (Mittelpunkt_in_Pixel.Y - Radius_in_Pixel / 3),
                                    (Mittelpunkt_in_Pixel.X + Radius_in_Pixel / 3),
                                    (Mittelpunkt_in_Pixel.Y + Radius_in_Pixel / 3));
            input.DrawLine(myPen, (Mittelpunkt_in_Pixel.X + Radius_in_Pixel / 3),
                                    (Mittelpunkt_in_Pixel.Y - Radius_in_Pixel / 3),
                                    (Mittelpunkt_in_Pixel.X - Radius_in_Pixel / 3),
                                    (Mittelpunkt_in_Pixel.Y + Radius_in_Pixel / 3));
        }


    }
}
