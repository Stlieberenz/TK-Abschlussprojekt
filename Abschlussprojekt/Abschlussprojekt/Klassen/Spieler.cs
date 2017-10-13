using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abschlussprojekt.Klassen.Statische_Variablen;
using static Abschlussprojekt.Klassen.Statische_Methoden;
using System.Net;

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
        public FARBE farbe { get; }
        public SPIELER_ART spieler_art { get; }
        public string name { get; }
        public bool status { get; set; }
        public IPAddress ip { get; }

        public Spieler(FARBE farbe,string name, SPIELER_ART spieler_art,IPAddress ip)
        {
            this.name = name;
            this.farbe = farbe;
            this.spieler_art = spieler_art;
            alle_Spieler.Add(this);
            this.ip = ip;
        }

        public void Initialisiere_Figuren()
        {
            Statische_Methoden.Initialisiere_Figuren(this.farbe);
        }
    }
}
