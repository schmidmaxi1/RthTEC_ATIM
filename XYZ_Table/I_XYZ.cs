using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZ_Table
{
    public interface IXYZ
    {

        //********************************************************************************************************************
        //                                    Eigenschaften der Klasse
        //********************************************************************************************************************
        
        //1. Zustands Parameter
        Boolean IsConnected { get; }          //Wenn verbindung über COM-Port besteht
        Boolean IsReady { get; }            //Wenn Init-Befehl und Referenzfahrt durchgeführt wurden

        //5.Aktuelle Position
        decimal Akt_x_Koordinate { get; }
        decimal Akt_y_Koordinate { get; }
        decimal Akt_z_Koordinate { get; }
        decimal Akt_Winkel { get; }


        //********************************************************************************************************************
        //                                    Funktionen der Klasse
        //********************************************************************************************************************

        void Initialisieren();

        void ReferenceDrive();

        bool Move2Position(decimal xKoordinate, decimal yKoordinate, decimal zKoordinate, decimal winkel);

        bool MoveADistance(decimal xWeg, decimal yWeg, decimal zWeg, decimal winkel);

        void Position_aktualisiern();

        //public override string AutoOpen(Load_Screen myLoadScreen)


    }
}
