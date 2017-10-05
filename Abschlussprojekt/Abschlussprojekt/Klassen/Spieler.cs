using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abschlussprojekt.Klassen.Statische_Variablen;
using static Abschlussprojekt.Klassen.Statische_Methoden;

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
    class Spieler
    {
        FARBE farbe { get; }
        public string name { get; }
        public Spieler(FARBE farbe,string name)
        {
            this.name = name;
            this.farbe = farbe;
            alle_Spieler.Add(this);
            Initialisiere_Figuren(farbe);
        }
    }
}
