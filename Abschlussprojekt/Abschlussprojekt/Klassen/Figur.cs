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
    class Figur
    {
        Image bild;
        Feld startposition;
        Feld aktuelle_Position;
        Feld mögliche_Position;
        FARBE farbe { get; set; }

        public Figur(FARBE farbe)
        {
            this.farbe = farbe;

            //
            // Hier werden die Jeweiligen Felder, die zur Laufzeit erstellt werden,
            // den jeweiligen statischen Listen hinzugefügt.
            //
            switch (farbe)
            {
                case FARBE.ROT: spieler_rot.Add(this); break;
                case FARBE.GELB: spieler_gelb.Add(this); break;
                case FARBE.GRUEN: spieler_gruen.Add(this); break;
                case FARBE.BLAU: spieler_blau.Add(this); break;
            }
        }
    }
}
