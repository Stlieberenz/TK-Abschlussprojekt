using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    static class Statische_Methoden
    {
        public static void Initialisiere_Statische_Variablen()
        {

        }

        public static void Initialisiere_Spiel(string name_rot, string name_gelb, string name_gruen, string name_blau )
        {
            Spieler rot = new Spieler(FARBE.ROT,name_rot);
            Spieler gelb = new Spieler(FARBE.GELB, name_gelb);
            Spieler gruen = new Spieler(FARBE.GRUEN, name_gruen);
            Spieler blau = new Spieler(FARBE.BLAU, name_blau);
        }

        public static void Initialisiere_Figuren(FARBE farbe)
        {
            for (int i = 0; i < 4; i++)
            {
                Figur figur = new Figur(farbe);
            }
        }
    }
}