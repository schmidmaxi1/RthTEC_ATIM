using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DXApplication3._10_Camera
{
    public class Rectangular:Detected_Object
    {

        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        public List<PointF> Corners_in_mm { get; set; } = new List<PointF>();
        public List<PointF> Corners_in_Pixel { get; set; } = new List<PointF>();

        public float Width_in_mm { get; set; }
        public float Width_in_Pixel { get; set; }

        public float Height_in_mm { get; set; }
        public float Height_in_Pixel { get; set; }

        //**************************************************************************************************
        //                                          Kunstruktor
        //**************************************************************************************************

        public Rectangular(){}

        public Rectangular(List<AForge.IntPoint> corners)
        {
            //Int Punkt zu float umrechnen
            foreach (AForge.IntPoint punkt in corners)
            {
                Corners_in_Pixel.Add(new PointF((float)punkt.X, (float)punkt.Y));
            }

            //Mittelpunkt berechnen
            PointF temp_Point = new PointF(0, 0);
            foreach (var punkt in corners)
            {
                temp_Point.X += punkt.X;
                temp_Point.Y += punkt.Y;
            }
            Mittelpunkt_in_Pixel = new PointF(temp_Point.X / corners.Count, temp_Point.Y / corners.Count);

            //Höhe und Breite berechnen
            float line1 = corners[0].DistanceTo(corners[1]);
            float line2 = corners[1].DistanceTo(corners[2]);
            float line3 = corners[2].DistanceTo(corners[3]);
            float line4 = corners[3].DistanceTo(corners[0]);

            Width_in_Pixel = (line1 + line3) / 2;
            Width_in_Pixel = (line2 + line4) / 2;

            //Umrechnen
            Mittelpunkt_in_mm = new PointF(
                (Mittelpunkt_in_Pixel.X - ImageMittelpunkt.X) * Umrechnung_Pixel_to_mm,
                (Mittelpunkt_in_Pixel.Y - ImageMittelpunkt.Y) * Umrechnung_Pixel_to_mm);

            foreach(var point in Corners_in_Pixel)
            {
                Corners_in_mm.Add(new PointF((point.X -  ImageMittelpunkt.X) * Umrechnung_Pixel_to_mm,
                                             (point.Y -  ImageMittelpunkt.Y) * Umrechnung_Pixel_to_mm));
            }

            Height_in_mm = Height_in_Pixel * Umrechnung_Pixel_to_mm;
            Width_in_mm = Width_in_Pixel * Umrechnung_Pixel_to_mm;

        }

        //**************************************************************************************************
        //                                           ToString()
        //**************************************************************************************************

        public override string ToString()
        {
            string newLine = Environment.NewLine;
            return "Rectangular:" + newLine +
                "  Center: X " + Mittelpunkt_in_mm.X.ToString() + " mm; Y " + Mittelpunkt_in_mm.Y.ToString() + " mm" + newLine +
                "  Width:" + Width_in_mm.ToString() + " mm" + newLine + 
                "  Heigth:" + Width_in_mm.ToString() + " mm" + newLine;
        }

        //**************************************************************************************************
        //                                         DrawInGraphic()
        //**************************************************************************************************

        public override void DrawInGraphic(Pen myPen, Graphics input)
        {
            //Rechteck zeichnen
            input.DrawPolygon(myPen, Corners_in_Pixel.ToArray());


            //Mittelpunkt markieren
            input.DrawLine(myPen, (Mittelpunkt_in_Pixel.X - Width_in_Pixel / 6),
                                (Mittelpunkt_in_Pixel.Y - Width_in_Pixel / 6),
                                (Mittelpunkt_in_Pixel.X + Width_in_Pixel / 6),
                                (Mittelpunkt_in_Pixel.Y + Width_in_Pixel / 6));
            input.DrawLine(myPen, (Mittelpunkt_in_Pixel.X + Width_in_Pixel / 6),
                                (Mittelpunkt_in_Pixel.Y - Width_in_Pixel / 6),
                                (Mittelpunkt_in_Pixel.X - Width_in_Pixel / 6),
                                (Mittelpunkt_in_Pixel.Y + Width_in_Pixel / 6));
        }


    }
}
