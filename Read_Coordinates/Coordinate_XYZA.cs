using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_Coordinates
{
    /// <summary>
    /// Klasse für Koodinaten für Movement File 
    /// </summary>
    public class Measurement_Point_XYZA
    {
        /// <summary>
        /// X Koordinate in mm
        /// </summary>
        public decimal X { get; set; }

        /// <summary>
        /// Y Koordinate in mm
        /// </summary>
        public decimal Y { get; set; }

        /// <summary>
        /// Winkel in °
        /// </summary>
        public decimal Angle { get; set; }

        /// <summary>
        /// Name für speichen des Files
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Flag is Measurment point should be measured. Default: true
        /// </summary>
        public bool IsSelected { get; set; }


        //Konstruktor
        public Measurement_Point_XYZA(decimal x, decimal y, decimal a, string name)
        {
            X = x;
            Y = y;
            Angle = a;
            Name = name;
            IsSelected = true;
        }

        public Measurement_Point_XYZA(decimal x, decimal y, decimal a, string name, bool isSelected)
        {
            X = x;
            Y = y;
            Angle = a;
            Name = name;
            IsSelected = isSelected;
        }


    }
}
