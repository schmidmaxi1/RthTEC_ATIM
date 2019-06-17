using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Hilfsfunktionen
{
    public class HelpFCT
    {
        //Static ist notwendig, um Funktion auzurufen, ohne die klasse zu erzeugen
        public static void SetComPortBox(ComboBox input)
        {
            //Liste füllen
            string[] ports = SerialPort.GetPortNames();

            //Liste sortieren (Alles größer 9 wird nach der 1 eingeordnet)
            Array.Sort(ports);

            if (ports.Length == 0)
                input.Items.Add("N/A");
            else
                foreach (string port in ports)
                    input.Items.Add(port);

            //Erstes auswehlen (NA oder COM x)
            input.SelectedIndex = 0;
        }
    }
}
