using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ATIM_GUI._0_Classes_Measurement
{
    public partial class Window_Sensitivity_TempStepSelect : Form
    {
        //**************************************************************************************************
        //                                      Erklärung
        //**************************************************************************************************

        /* Sensitivity Temperature Step Select V2.0:
         * 06.09.2018
         * Schmid Maxi
         * 
         *                                  !Voll Funktionsfähig!
         * 
         * - Fenster wird vor Sensitivity Messung geöffnet
         * - Die notwendigen Temperatur-Steps werden über Start-Temperatur, End-Temperature, und Schrittweite
         *   bestimmt.
         * - Wenn das Fenster korrekt geschlossen wird (nicht Cancel) werden die einzelnen Temperatur-Schritte
         *   in die Klasse "Sensitivity_Measurement" übernommen
         * - Bei Cancel bleibt werden die Werte nicht übernommen
         * - Bei ungeraden Schritten wird gewarnt
         * 
        */


        //**************************************************************************************************
        //                                       Variablen
        //**************************************************************************************************

        //Liste mit gewählten Temperatur werten
        public List<Decimal> temperaturen;

        //Verweis auf Hauptfenster
        Form1 mainForm = null;

        //**************************************************************************************************
        //                                      Konstruktor
        //**************************************************************************************************

        public Window_Sensitivity_TempStepSelect(Form1 callingForm)
        {
            //Referenz für calling Form
            mainForm = callingForm as Form1;

            //Fenster initialisieren
            InitializeComponent();

            //Liste aus Intiallwerten berechnen
            temperaturen = Calculate_TemperatureList();
         
            //Graph Updaten
            Update_Graph();

            //Zeit Updaten
            Update_Time();
        }

        //**************************************************************************************************
        //                                       UI-Events
        //**************************************************************************************************

        #region UI-Events

        private void TempSetting_ValueChanged(object sender, EventArgs e)
        {
            //Liste aus Intiallwerten berechnen
            temperaturen = Calculate_TemperatureList();

            //Graph Updaten
            Update_Graph();

            //Zeit updaten
            Update_Time();
        }

        public void Button_start_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.Yes;
                     
            foreach (decimal selected in temperaturen)
            {
                if(selected % 1 != 0)
                {
                    string selectedValue = selected.ToString();
                     dialogResult = MessageBox.Show("Attention: At least one temperature value is not even.\r\nFor example: " 
                         + selected + " °C \r\nWould you like to continue? ", "Warning", MessageBoxButtons.YesNo);
                   
                    break;
                }
            }

            if (dialogResult == DialogResult.Yes)
            {
                //Return Temp-List
                mainForm.mySensitivity.TempSteps = temperaturen;

                //Close window
                this.Close();
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {           
            this.Close();
        }

        #endregion UI-Events

        //**************************************************************************************************
        //                                    lokale Funktionen
        //**************************************************************************************************

        #region Hilfsfunktionen

        public List<Decimal> Calculate_TemperatureList()
        {
            //Hilfsvariablen
            Decimal start_Temperature = start_Temp.Value;
            Decimal stop_Temperature = stop_Temp.Value;
            int steps = (int)numberOfSteps.Value;

            List<Decimal> returnList = new List<decimal>();

            //Temperaturschritte
            //Beispiel (start = 25, stop = 45, steps 4 => (45 - 25) / (4 - 2) = 10 
            Decimal stepWidth = (stop_Temperature - start_Temperature) / (steps - 1);

            //Liste Füllen
            for (int i = 0; i < steps; i++)
            {
                returnList.Add(Decimal.Round(start_Temperature + i * stepWidth, 3));
            }

            return returnList;
     
        }

        public void Update_Graph()
        {
            //Punkte aus aktuellem Graph löschen
            k_factor_preview.Series[0].Points.Clear();

            //Punkte hinzufügen
            int i = 0;
            foreach (Decimal punkt in temperaturen)
            {
                i++;
                k_factor_preview.Series[0].Points.AddXY(i, punkt);
            }

            //Achse anpassen
            k_factor_preview.ChartAreas["ChartArea1"].AxisX.Maximum = i;
        }

        public void Update_Time()
        {
            int zeit_in_sekunden = 30 * (int)numberOfSteps.Value;

            //Estimation of the Time
            //Temperature Change
            //Temperature Regulation to Target
            //MeasurmentTime

            //umrechnen
            string zeit_als_string = (zeit_in_sekunden / 60).ToString() + " min";

            //Übernehmen
            textBox_Time.Text = zeit_als_string;
        }

        #endregion Hilfsfunktionen

    }
}
