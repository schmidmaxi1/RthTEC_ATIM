using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

namespace Read_Coordinates
{
    /// <summary>
    /// Contains all informations for automatic testing. Information are read form a .txt File
    /// </summary>
    public class Movement_Infos
    {

        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        #region Variables

        
        /// <summary>
        /// Indication Flag if file is correct and compleate [default: FALSE]
        /// </summary>
        public Boolean IsCorect { get; internal set; } = false;

        /// <summary>
        /// Array of all measurment Points on BRD with Name and Position [X, Y, Angle]
        /// </summary>
        public Measurement_Point_XYZA[] MyMeasurment_Point { get; internal set; }

        /// <summary>
        /// X Dimension of BRD for Camera in mm
        /// </summary>
        public decimal BRD_Dimension_X { get; internal set; } = Decimal.MinValue;           //Default Vaulues is neccessary for check

        /// <summary>
        /// Y Dimension of BRD for Camera in mm
        /// </summary>
        public decimal BRD_Dimension_Y { get; internal set; } = Decimal.MinValue;           //Default Vaulues is neccessary for check

        /// <summary>
        /// Positions [X, Y, Angle] of 3 Feducials
        /// </summary>
        public Measurement_Point_XYZA[] Feducials { get; internal set; } = new Measurement_Point_XYZA[3];

        /// <summary>
        /// Positions [X, Y, Angle] of QR-Code (optional)
        /// </summary>
        public Measurement_Point_XYZA QR_Code { get; internal set; }

        /// <summary>
        /// Hight in mm to contact BRD
        /// </summary>
        public decimal TouchDown_Hight { get; internal set; } = Decimal.MinValue;           //Default Vaulues is neccessary for check

        /// <summary>
        /// Hight in mm to drive above BRD with components without touching anything
        /// </summary>
        public decimal Driving_Hight { get; internal set; } = Decimal.MinValue;             //Default Vaulues is neccessary for check

        #endregion Variables

        //**************************************************************************************************
        //                                          Konstruktor
        //**************************************************************************************************

        public Movement_Infos()
        { }

        public Movement_Infos(string input_Path)
        {
            //Zähl-Variable
            int LineCount = 0;

            //Temporäre Liste für Koordinaten (Liste wird abschließend in Array umgewandelt)
            List<Measurement_Point_XYZA> temp_Liste_Koordinaten = new List<Measurement_Point_XYZA>();

            //Leeres Feld für Texteinlesen erzeugen
            string[] datei_Inhalt = new string[1];

            try
            {
                //Read all single Lines
                datei_Inhalt = File.ReadAllLines(input_Path);

                //Alle Zeilen nacheinander durchlaufen
                for (LineCount = 0; LineCount < datei_Inhalt.Length; LineCount++)
                {
                    //Vorstehende Leerzeichen entfernen
                    datei_Inhalt[LineCount].Trim();

                    //Falls Kommentar (Zeilenbeginn #) oder leere Zeile --> Zeile ignorieren
                    if (datei_Inhalt[LineCount].StartsWith("#") | String.IsNullOrEmpty(datei_Inhalt[LineCount]))
                        datei_Inhalt[LineCount].Trim();//Nichts machen

                    else if (datei_Inhalt[LineCount].StartsWith("BRD-Dimension X:"))
                        BRD_Dimension_X = Convert.ToDecimal(datei_Inhalt[LineCount].Substring(16));

                    else if (datei_Inhalt[LineCount].StartsWith("BRD-Dimension Y:"))
                        BRD_Dimension_Y = Convert.ToDecimal(datei_Inhalt[LineCount].Substring(16));

                    else if (datei_Inhalt[LineCount].StartsWith("FEDUCIAL"))
                    {
                        Int32 temp_i = Convert.ToInt32(datei_Inhalt[LineCount].Substring(9, 1));

                        //String um wandeln (allse nach : an stelle 11)
                        String2Coordinates(datei_Inhalt[LineCount].Substring(11), out decimal temp_x, out decimal temp_y, out decimal temp_a, out string temp_name);

                        //Fals kein Name vorhanden, Durchnummerieren
                        if (String.IsNullOrEmpty(temp_name))
                            temp_name = "Failure";

                        //Zur Liste hinzufügen
                        Feducials[temp_i - 1]  = new Measurement_Point_XYZA(temp_x, temp_y, temp_a, temp_name);
                    }

                    else if (datei_Inhalt[LineCount].StartsWith("QR-CODE:"))
                    {
                        //String um wandeln (allse nach : an stelle 11)
                        String2Coordinates(datei_Inhalt[LineCount].Substring(8), out decimal temp_x, out decimal temp_y, out decimal temp_a, out string temp_name);

                        //Fals kein Name vorhanden, Durchnummerieren
                        if (String.IsNullOrEmpty(temp_name))
                            temp_name = "QR-Code";

                        //Zur Liste hinzufügen
                        QR_Code = new Measurement_Point_XYZA(temp_x, temp_y, 0, temp_name);     //Angle not neccessary
                    }

                    else if (datei_Inhalt[LineCount].StartsWith("TouchDown_Hight"))
                        TouchDown_Hight = Convert.ToDecimal(datei_Inhalt[LineCount].Substring(16));

                    else if (datei_Inhalt[LineCount].StartsWith("Driving_Hight:"))
                        Driving_Hight = Convert.ToDecimal(datei_Inhalt[LineCount].Substring(14));

                    else
                    {
                        //String um wandeln
                        String2Coordinates(datei_Inhalt[LineCount], out decimal temp_x, out decimal temp_y, out decimal temp_a, out string temp_name);

                        //Fals kein Name vorhanden, Durchnummerieren
                        if (String.IsNullOrEmpty(temp_name))
                            temp_name = (temp_Liste_Koordinaten.Count + 1).ToString();

                        //Zur Liste hinzufügen
                        temp_Liste_Koordinaten.Add(new Measurement_Point_XYZA(temp_x, temp_y, temp_a, temp_name));
                    }

                }

                //Liste in Feld umwanden
                MyMeasurment_Point = temp_Liste_Koordinaten.ToArray();

                //Falls Eigenschaft nich belegt wurde --> Exeption werfen
                Check_if_all_Variables_overwitten();

                //Wenn keine Exeption aufgetretten dann Correct
                IsCorect = true;
            }
            //Bei Fehler falsch zurückgeben
            catch (Exception ex)
            {
                //Wenn beim Einlesen einer Zeile ein Fehler aufgetreten ist
                if(LineCount < datei_Inhalt.Length)
                    MessageBox.Show(
                        "In Line: "+ LineCount.ToString() + Environment.NewLine +
                        datei_Inhalt[LineCount] + Environment.NewLine + Environment.NewLine +
                        "Failure: " + ex.Message, 
                        "Failure while reading movement file");

                //Wenn bei der überprüfung Fehler aufgetreten
                else
                    MessageBox.Show(
                        "Failure: " + ex.Message,
                        "Failure while reading movement file");

                //Flag setzen
                IsCorect = false;
            }
        }

        //**************************************************************************************************
        //                                       Lokal FCTs
        //**************************************************************************************************

        private void Check_if_all_Variables_overwitten()
        {
            //Dimensionen checken
            if (BRD_Dimension_X == Decimal.MinValue)
                throw new Exception("Missing X Dimension");
            if (BRD_Dimension_Y == Decimal.MinValue)
                throw new Exception("Missing Y Dimension");

            //Feducial nicht definiert
            if (Feducials[0] == null)
                throw new Exception("Missing feducial 1");
            if (Feducials[1] == null)
                throw new Exception("Missing feducial 2");
            if (Feducials[2] == null)
                throw new Exception("Missing feducial 3");

            //Feducial mit falschen angeben
            if ( Feducials[0].Name.Contains("Failure"))
                throw new Exception("Feducial 1 error");
            if (Feducials[1].Name.Contains("Failure"))
                throw new Exception("Feducial 1 error");
            if (Feducials[2].Name.Contains("Failure"))
                throw new Exception("Feducial 1 error");

            //Falls kein QR-Position definiert
            if (QR_Code == null)
                QR_Code = new Measurement_Point_XYZA(0, 0, 0, "Non defined");
            
            //Höhen checken
            if (TouchDown_Hight == Decimal.MinValue)
                throw new Exception("No touch down hight defined");
            if (Driving_Hight == Decimal.MinValue)
                throw new Exception("No driving hight defined");

            //Checken ob Liste leer
            if (MyMeasurment_Point.Length == 0)
                throw new Exception("No Measurment Points defined");
        }

        private void String2Coordinates(string inputSTR, out decimal x, out decimal y, out decimal a, out string name)
        {
            int index_first_komma = inputSTR.IndexOf(';', 0);
            int index_second_komma = inputSTR.IndexOf(';', index_first_komma + 1);
            int index_third_komma = inputSTR.IndexOf(';', index_second_komma + 1);

            x = Convert.ToDecimal(inputSTR.Substring(0, index_first_komma));
            y = Convert.ToDecimal(inputSTR.Substring(index_first_komma + 1, index_second_komma - index_first_komma - 1));
            a = Convert.ToDecimal(inputSTR.Substring(index_second_komma + 1, index_third_komma - index_second_komma - 1));

            name = inputSTR.Substring(index_third_komma + 1).Trim();

        }

        //**************************************************************************************************
        //                                       Sonstige FCTs
        //**************************************************************************************************

        /// <summary>
        /// Kopiert nicht den "Pointer der Instanz sondern nur den Inhalt (Notwendig um alte Werte zu speichern)"
        /// </summary>
        /// <returns></returns>
        public Movement_Infos CopyByValue()
        {
            Movement_Infos returnVar = new Movement_Infos()
            {
                //Einfache Variablen
                IsCorect = this.IsCorect,
                BRD_Dimension_X = this.BRD_Dimension_X,
                BRD_Dimension_Y = this.BRD_Dimension_Y,
                TouchDown_Hight = this.TouchDown_Hight,
                Driving_Hight = this.Driving_Hight,
                //XYZA - Variablen
                MyMeasurment_Point = this.MyMeasurment_Point,
                Feducials = this.Feducials,
                
            };

            QR_Code = new Measurement_Point_XYZA(this.QR_Code.X, this.QR_Code.Y, this.QR_Code.Angle, this.QR_Code.Name);
            
            returnVar.MyMeasurment_Point = new Measurement_Point_XYZA[this.MyMeasurment_Point.Length];

            for (int i = 0; i < this.MyMeasurment_Point.Length; i++)
                returnVar.MyMeasurment_Point[i] = new Measurement_Point_XYZA
                    (this.MyMeasurment_Point[i].X,
                    this.MyMeasurment_Point[i].Y,
                    this.MyMeasurment_Point[i].Angle,
                    this.MyMeasurment_Point[i].Name,
                    this.MyMeasurment_Point[i].IsSelected);

            returnVar.Feducials = new Measurement_Point_XYZA[this.Feducials.Length];

            for (int i = 0; i < this.Feducials.Length; i++)
                returnVar.Feducials[i] = new Measurement_Point_XYZA
                    (this.Feducials[i].X,
                    this.Feducials[i].Y,
                    this.Feducials[i].Angle,
                    this.Feducials[i].Name,
                    this.Feducials[i].IsSelected);


            return returnVar;
        }

        public Movement_Infos Minimize()
        {
            //Copy by Value Kopie erstellen
            Movement_Infos returnVar = this.CopyByValue();

            //Alle Mess-Positionen nacheinander durchgehen
            for (int i = 0; i < returnVar.MyMeasurment_Point.Length; i++)
            {
                //Wenn Punkt nicht vermessen werden soll --> Aufaddieren zu nächstem MessPunkt
                if (!returnVar.MyMeasurment_Point[i].IsSelected)
                {
                    //Nur weiterrechnen wenn nächster Punkt vorhanden
                    if(i+1 < returnVar.MyMeasurment_Point.Length)
                    {
                        returnVar.MyMeasurment_Point[i + 1].X += returnVar.MyMeasurment_Point[i].X;
                        returnVar.MyMeasurment_Point[i + 1].Y += returnVar.MyMeasurment_Point[i].Y;
                        returnVar.MyMeasurment_Point[i + 1].Angle += returnVar.MyMeasurment_Point[i].Angle;
                    }

                    //Punkt entfernen
                    //Umwandeln in Liste
                    var tempList = new List<Measurement_Point_XYZA>(returnVar.MyMeasurment_Point);
                    //Punkt entfernen
                    tempList.RemoveAt(i);
                    //Zurückwandeln in Array und ersetzen
                    returnVar.MyMeasurment_Point = tempList.ToArray();

                    //i eins zurückzählen (Weniger elemente)
                    i--;
                }

            }


            return returnVar;
        }

    }
}
