using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ATIM_GUI
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        //**************************************************************************************************
        //                                       Status-Bar
        //**************************************************************************************************

        #region StausBar

        public void StatusBar_Reset()
        {
            // Muss so kompliziert sein, da es von einem anderen Thread aufgerufen wird
            statusStrip_MainWindow.Invoke((MethodInvoker)delegate
            {
                statusBar_Headline.Text = "Status: ";
                statusBar_ProgressBar.Value = 0;
                statusBar_Detailed.Text = "0%";
            });
        }

        public void StatusBar_TTA_all(int cycle, int cycle_max, int dut_nr, int dut_max)
        {
            // Muss so kompliziert sein, da es von einem anderen Thread aufgerufen wird
            statusStrip_MainWindow.Invoke((MethodInvoker)delegate
            {

                //Titel ändern
                statusBar_Headline.Text = "TTA automated: ";

                //Prozent berechnen
                int prozent = ((cycle * 100 / cycle_max) + dut_nr * 100) / dut_max;

                //Progressbar updaten
                statusBar_ProgressBar.Value = prozent;

                //Text ändern
                statusBar_Detailed.Text = prozent.ToString() + "% " +
                    "( Cycle: " + cycle.ToString() + " / " + cycle_max.ToString() + " | " +
                    "DUT: " + dut_nr.ToString() + " / " + dut_max.ToString() + " )";

            });
        }

        public void StatusBar_TTA_Single(int cycle, int cycle_max)
        {
            // Muss so kompliziert sein, da es von einem anderen Thread aufgerufen wird
            statusStrip_MainWindow.Invoke((MethodInvoker)delegate
            {
                //Titel ändern
                statusBar_Headline.Text = "TTA single: ";

                //Prozent berechnen
                int prozent = (cycle * 100 / cycle_max);

                //Progressbar updaten
                statusBar_ProgressBar.Value = prozent;

                //Text ändern
                statusBar_Detailed.Text = prozent.ToString() + "% " +
                        "( Cycle: " + cycle.ToString() + " / " + cycle_max.ToString() + " ) ";

                //Updaten
                statusStrip_MainWindow.Refresh();
            });
        }

        public void StatusBar_Sensitivity_all(int dut_nr, int dut_max, int temp_nr, int temp_max)
        {
            // Muss so kompliziert sein, da es von einem anderen Thread aufgerufen wird
            statusStrip_MainWindow.Invoke((MethodInvoker)delegate
            {

                //Titel ändern
                statusBar_Headline.Text = "Sensitivity automated: ";

                //Prozent berechnen
                int prozent = ((dut_nr * 100 / dut_max) + (temp_nr - 1) * 100) / temp_max;

                //Progressbar updaten
                statusBar_ProgressBar.Value = prozent;

                //Text ändern
                statusBar_Detailed.Text = prozent.ToString() + "% " +
                    "( DUT: " + dut_nr.ToString() + " / " + dut_max.ToString() + " | " +
                    "Temperature: " + temp_nr.ToString() + " / " + temp_max.ToString() + " )";

            });
        }

        public void StatusBar_Measurement_FINISHED()
        {
            // Muss so kompliziert sein, da es von einem anderen Thread aufgerufen wird
            statusStrip_MainWindow.Invoke((MethodInvoker)delegate
            {
                //Text anhängen
                statusBar_Detailed.Text += " Finished!";
            });
        }

        public void StatusBar_Measurement_CANCELED()
        {
            // Muss so kompliziert sein, da es von einem anderen Thread aufgerufen wird
            statusStrip_MainWindow.Invoke((MethodInvoker)delegate
            {
                //Text anhängen
                statusBar_Detailed.Text += " Measurement was canceled!";
            });
        }

        #endregion StatusBar
    }
}
