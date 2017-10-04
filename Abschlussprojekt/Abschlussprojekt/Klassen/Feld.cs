using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Abschlussprojekt.Klassen.Statische_Variablen;

// Namenskonvention: --------------------------------------+
//                                                         |
// Alle Wörter eines Namens werden mit einem "_" getrennt. |
// Klassen     = Klasse_Bsp    => erster Buchstabe groß    |
// Methoden    = Methode_Bsp   => erster Buchstabe groß    |
// Variable    = variable_Bsp  => erster Buchstabe klein   |
// ENUM        = ENUM_BSP      => alle Buchstaben groß     |
//---------------------------------------------------------+

namespace Abschlussprojekt.Klassen
{

    class Feld
    {
        FARBE farbe { get; set; }
        FELD_EIGENSCHAFT feld_art { get; set; }
        Figur figur { get; set; }
        Image bild;

        public Feld(FARBE farbe, FELD_EIGENSCHAFT feld_art)
        {
            this.farbe = farbe;
            this.feld_art = feld_art;

            //
            // Hier werden die Jeweiligen Felder, die zur Laufzeit erstellt werden,
            // den jeweiligen statischen Listen hinzugefügt.
            //
            switch (feld_art) 
            {
                case FELD_EIGENSCHAFT.STARTPOSITION: start_felder.Add(this); break;
                case FELD_EIGENSCHAFT.SPIELFELD: spiel_felder.Add(this); break;
                case FELD_EIGENSCHAFT.ZIEL: ziel_felder.Add(this); break;
            }
        }
    }
}
