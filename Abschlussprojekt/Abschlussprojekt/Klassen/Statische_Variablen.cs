using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    static class Statische_Variablen
    {
        public enum FARBE
        {
            ROT,
            GELB,
            BLAU,
            GRUEN
        }
        public enum FELD_EIGENSCHAFT
        {
            STARTPOSITION,
            SPIELFELD,
            ZIEL
        }
        public enum NETZWERK_NACHRICHT
        {
            TEXTNACHRICHT,
            SPIELFIGURENPOSITIONEN,
            GEWÜRFELTE_ZAHL
        }

        public static List<Feld> start_felder = new List<Feld>();
        public static List<Feld> spiel_felder = new List<Feld>();
        public static List<Feld> ziel_felder = new List<Feld>();

        public static List<Figur> spieler_rot = new List<Figur>();
        public static List<Figur> spieler_gelb = new List<Figur>();
        public static List<Figur> spieler_gruen = new List<Figur>();
        public static List<Figur> spieler_blau = new List<Figur>();

        public static List<Spieler> alle_Spieler = new List<Spieler>();
    }
}
