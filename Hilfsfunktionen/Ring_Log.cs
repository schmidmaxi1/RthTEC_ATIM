using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hilfsfunktionen
{
    public class Ring_Log
    {
        //********************************************************************************************************************
        //                                    Eigenschaften der Klasse
        //********************************************************************************************************************
        
        /// <summary>
        /// Ring_Buffer for Data 
        /// </summary>
        public string[] Log_Text { get; internal set; }

        /// <summary>
        /// Position in RingBuffer
        /// </summary>
        public int Position { get; internal set; } = 0;

        /// <summary>
        /// Length of RingBuffer
        /// </summary>
        public int Size { get; internal set; }

        /// <summary>
        /// Flag for at least one Overflow
        /// </summary>
        public bool Ones_Looped { get; internal set; } = false;

        //********************************************************************************************************************
        //                                                Konstruktoren
        //********************************************************************************************************************

        public Ring_Log(int size)
        {
            //RingBuffer erzeugen
            Log_Text = new string[size];

            //Size übernhemen
            Size = size;
        }

        //********************************************************************************************************************
        //                                                Globale Fct.
        //********************************************************************************************************************

        public void Add_Line(string text)
        {
            //Position hochzälen
            Position++;

            //Bei Überlauf zurücksetzen
            if (Position >= Size)
            {
                Position = 0;
                Ones_Looped = true;
            }
                

            //Text übernehmen
            Log_Text[Position] = text;
        }

        public void ShowLog()
        {
            //RingBuffer Sortieren
            string output = "";

            //Wenn RingBuffer noch nicht voll war, dann bei 0 starten
            if (!Ones_Looped)
            {
                for (int i = 0; i < Position; i++)
                {
                    output += Log_Text[i] + Environment.NewLine;
                }
            }
            //Wenn RingBuffer schon einmal übergelaufen, dann bei Position 1 starten, Bis Size, dann zurücksetzen und bis Position
            else
            {
                for (int i = Position + 1; i != Position; i++)
                {
                    //Bei bedarf zurücksetzen
                    if (i >= Size)
                        i = 0;

                    //Text anhängen
                    output += Log_Text[i] + Environment.NewLine;
                }
            }

            //Fenster über Dialog öffnen
            Ring_Log_Window myForm = new Ring_Log_Window(output);
            myForm.Show();

        }

    }
}
