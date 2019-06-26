using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIM_GUI
{
    public class Saving_Options
    {

        //**************************************************************************************************
        //                          Variablen (Flags ob ein File gespeichert werden soll)
        //**************************************************************************************************

        //default settings
        public bool Save_Aver_Cool { get; set; } = true;
        public bool Save_Aver_Heat { get; set; } = false;
        public bool Save_Signle_Cool { get; set; } = false;
        public bool Save_Single_Heat { get; set; } = false;
        public bool Save_Raw { get; set; } = false;

        //**************************************************************************************************
        //                                    FCT für Setting-Files
        //**************************************************************************************************

        /// <summary>
        /// Returns the txt für the Setting file
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string text = "*SaveOptions:" + Environment.NewLine;

            text += "Save_Aver_Cool: " + Save_Aver_Cool.ToString() + Environment.NewLine;
            text += "Save_Aver_Heat: " + Save_Aver_Heat.ToString() + Environment.NewLine;
            text += "Save_Signle_Cool: " + Save_Signle_Cool.ToString() + Environment.NewLine;
            text += "Save_Single_Heat: " + Save_Single_Heat.ToString() + Environment.NewLine;
            text += "Save_Raw: " + Save_Raw.ToString() + Environment.NewLine;

            return text;
        }

        /// <summary>
        /// Sets the flags according to the setting File
        /// </summary>
        public void FromString(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].StartsWith("Save_Aver_Cool:"))
                    Save_Aver_Cool = STR2Bool(input[i]);
                else if (input[i].StartsWith("Save_Aver_Heat:"))
                    Save_Aver_Heat = STR2Bool(input[i]);
                else if (input[i].StartsWith("Save_Signle_Cool:"))
                    Save_Signle_Cool = STR2Bool(input[i]);
                else if (input[i].StartsWith("Save_Single_Heat:"))
                    Save_Single_Heat = STR2Bool(input[i]);
                else if (input[i].StartsWith("Save_Raw:"))
                    Save_Raw = STR2Bool(input[i]);
            }
        }

        private Boolean STR2Bool(string input)
        {
            if (input.Contains("True"))
                return true;
            else
                return false;
        }
    }
}
