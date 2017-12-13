using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mensch_ärgere_dich_nicht;
using System.Net;

// Namenskonvention: --------------------------------------+
//                                                         |
// Alle Wörter eines Namens werden mit einem "_" getrennt. |
// Klassen     = Klasse_Bsp    => erster Buchstabe groß    |
// Methoden    = Methode_Bsp   => erster Buchstabe groß    |
// Variable    = variable_Bsp  => erster Buchstabe klein   |
// ENUM        = ENUM_BSP      => alle Buchstaben groß     |
//---------------------------------------------------------+

namespace Mensch_ärgere_dich_nicht.Klassen
{
    class Spieler
    {
        public Statische_Variablen.FARBE farbe { get; }
        public Statische_Variablen.SPIELER_ART spieler_art { get; }
        public string name { get; }
        public bool status { get; set; }
        public IPAddress ip { get; }
        public List<Figur> eigene_Figuren { get; set; }

        public Spieler(Statische_Variablen.FARBE farbe,string name, Statische_Variablen.SPIELER_ART spieler_art,IPAddress ip)
        {
            this.name = name;
            this.farbe = farbe;
            this.spieler_art = spieler_art;
            this.ip = ip;
        }
    }
}
