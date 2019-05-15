using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace ATIM_GUI._05_XYZ
{
    public class Movement_Informations
    {

        //**************************************************************************************************
        //                                          Variablen
        //**************************************************************************************************

        public Boolean IsCorect { get; internal set; } = false;
        public decimal[,] Calculated_movements { get; internal set; }

        private string Path;

        //**************************************************************************************************
        //                                         Konstruktor
        //**************************************************************************************************

        public Movement_Informations(string path)
        {
            Path = path;

            Calculate_Movement_from_ASCII_File();
        }

        //**************************************************************************************************
        //                                      Lokale Funktion
        //**************************************************************************************************

        #region Local_Functions

        private void Calculate_Movement_from_ASCII_File()
        {

            try
            {
                //Read File
                string[] datei_Inhalt = File.ReadAllLines(Path);

                //Feld definieren
                Calculated_movements = new decimal[datei_Inhalt.Length - 1, 2];

                //Daten herauslösen
                for (int i = 1; i < datei_Inhalt.Length; i++)
                {
                    int index_first_komma = datei_Inhalt[i].IndexOf(',', 0);
                    int index_last_komma = datei_Inhalt[i].IndexOf(',', index_first_komma + 1);

                    Calculated_movements[i - 1, 0] = Convert.ToDecimal(datei_Inhalt[i].Substring(0, index_first_komma));
                    Calculated_movements[i - 1, 1] = Convert.ToDecimal(datei_Inhalt[i].Substring(index_first_komma + 1, index_last_komma - index_first_komma - 1));
                }

                //Wenn keine Exeption aufgetretten dann Correct
                IsCorect = true;
            }
            catch (Exception)
            {
                IsCorect = false;
            }

        }

        #endregion Local_Functions


    }
}
