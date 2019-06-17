using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace XYZ_Table
{
    public class ISEL
    {

        public ISEL() { }

        public SerialPort Serial_Interface { get; set; } = new SerialPort()
        {
            BaudRate = 19200,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = 0,

        };

        public string Antwort { get; set; }
        public string Communication_LOG { get; set; }

        //********************************************************************************************************************
        //                                  Hilfs-Funktionen die immer gleich sind
        //********************************************************************************************************************

        #region Allgemeine Hilfsfunktionen

        /// <summary>
        /// Converts the HEX answer from the ISEL SYSTEM to the decimal number of steps
        /// </summary>
        public static Decimal HexString_to_Decimal(string input)
        {
            //Hex-Sting in int umwandeln
            Int32 anzahl_schritte = Convert.ToInt32(input, 16);

            //Da Rückgabewert ein 2er Komplement mit 24 Bit ist, muss bei negativen Zahlen umgerechnet werden
            if ((anzahl_schritte & 0x80000) == 0x80000)
            {
                anzahl_schritte = -1 * (((~anzahl_schritte) & 0xFFFFFF) + 1);
            }

            //Umwandlung von Schritte in mm
            return (Decimal)anzahl_schritte;
        }

        #endregion Allgemeine Hilfsfunktionen

        //********************************************************************************************************************
        //                                 Kommunikations-Funktionen sind immer gleich
        //********************************************************************************************************************

        #region Kommunikation
        //1. Nachricht senden und Antwort empfangen mit unbestimmter länge-------------------------
        public string SendMessage_withAnswer(string nachricht)
        {
            //Nachricht senden


            Serial_Interface.WriteLine(nachricht + "\r");
            Communication_LOG += DateTime.Now.ToString() + " --> " + nachricht + "\n";

            //Pause damit iM8 die antwort vollständig gesendet hat
            System.Threading.Thread.Sleep(50);

            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }

            //Communication Log beschreiben
            Communication_LOG += DateTime.Now.ToString() +
              "\n     --> " + nachricht +
              "\n     <-- " + Antwort + "\n";

            return Antwort;
        }

        //2. Nachricht senden und Antwort empfangen mit bestimmter Länge (nicht möglich mit zu wenig Bytes)
        public string SendMessage_withAnswer(string nachricht, UInt16 ziel_laenge)
        {
            //Nachricht senden
            Serial_Interface.WriteLine(nachricht + "\r");

            //Pause damit iM8 die antwort vollständig gesendet hat
            System.Threading.Thread.Sleep(10);

            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }

            int i = 0;
            while (Antwort.Length < ziel_laenge & i < 50)
            {
                i++;
                System.Threading.Thread.Sleep(10);
                Task.Delay(10);
                Antwort += Serial_Interface.ReadExisting();
            }

            //Communication Log beschreiben
            Communication_LOG += DateTime.Now.ToString() + " --> " + nachricht + "\n" +
              " <-- " + Antwort + "\n";

            return Antwort;
        }

        //3. Nachricht senden ohne die Antwort gleich zu wollen-------------------------------------------
        public string SendMessage_withoutAnswer(string nachricht)
        {
            //Nachricht senden
            Serial_Interface.WriteLine(nachricht + "\r");

            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }

            //Communication Log beschreiben
            Communication_LOG += DateTime.Now.ToString() + " --> " + nachricht + "\n" +
              " <-- " + Antwort + "\n";

            return Antwort;

        }

        public string Check_if_position_is_reached()
        {
            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }
            //0 kommt erst wenn Ziel erreicht
            int i = 0; //maximal 500ms
            while (Antwort.Length != 1 & i < 50)
            {
                i++;
                System.Threading.Thread.Sleep(10);
                Task.Delay(10);
                Antwort += Serial_Interface.ReadExisting();
            }

            //Communication Log beschreiben
            Communication_LOG += " <-- " + Antwort + "\n";

            return Antwort;
        }

        public string Check_if_position_is_reached_REFDrive()
        {
            //Antwort auslesen, evtl. TimeOut
            try
            {
                Antwort = Serial_Interface.ReadExisting();
            }
            catch (TimeoutException)
            {
                Antwort = "Time-Out";
            }
            //0 kommt erst wenn Ziel erreicht
            int i = 0; //max. 100s
            while (Antwort.Length != 1 & i < 10000)
            {
                i++;
                System.Threading.Thread.Sleep(10);
                Task.Delay(10);
                Antwort += Serial_Interface.ReadExisting();
            }

            //Communication Log beschreiben
            Communication_LOG += " <-- " + Antwort + "\n";

            return Antwort;
        }

        #endregion Kommunikation


    }
}
