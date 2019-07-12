using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;

namespace Hilfsfunktionen
{
    public class HelpFCT
    {
        //Static ist notwendig, um Funktion auzurufen, ohne die klasse zu erzeugen

        /// <summary>
        /// Seraches all available COM-Ports, sorts them and adds them to the ComboBox
        /// </summary>
        /// <param name="input">ComboBox itself</param>
        public static void SetComPortBox(ComboBox input)
        {
            //Liste füllen
            string[] ports = SerialPort.GetPortNames();

            //Liste sortieren (Alles größer 9 wird nach der 1 eingeordnet)
            Array.Sort(ports, (a,b) => Convert.ToInt32(a.Substring(3)).CompareTo(Convert.ToInt32(b.Substring(3))));
            

            if (ports.Length == 0)
                input.Items.Add("N/A");
            else
                foreach (string port in ports)
                    input.Items.Add(port);

            //Erstes auswehlen (NA oder COM x)
            input.SelectedIndex = 0;
        }

        /// <summary>
        /// Copys all Information form one ComboBox to the other.
        /// </summary>
        /// <param name="input"> Input ComboBox</param>
        /// <param name="output"> Output ComboBox</param>
        public static void SetComboBox2ComboBox(ComboBox input, ComboBox output)
        {
            //Alles aus alter ComboBox löschen
            output.Items.Clear();

            //Alle Items aus input übernehmen
            foreach (var Text in input.Items)
                output.Items.Add(Text);

            //Auswahl übernehmen
            output.Text = input.Text;
        }

        /// <summary>
        /// Replaces the Indicator (%R) in the initial string with the replace
        /// </summary>
        /// <param name="initial_STR">STR taken form the textBox </param>
        /// <param name="replacer">replace STR</param>
        /// <returns></returns>
        public static string Replace_Output_STR(string initial_STR, string replacer)
        {
            int place = initial_STR.IndexOf("%R");                              //Stelle von %R

            return initial_STR.Substring(0, place) + replacer + initial_STR.Substring(place + 2);
        }
    }
}
