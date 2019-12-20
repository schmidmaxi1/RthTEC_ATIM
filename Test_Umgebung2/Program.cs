using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using ATIM_GUI;



namespace Test_Umgebung2
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //DPA erstellen
            TTA_DPA myDPA = new TTA_DPA();

            //Test-FCT
            myDPA.DPA_Test_matFILE();
        }
    }
}
