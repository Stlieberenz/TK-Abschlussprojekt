using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Data.SqlClient;
using Abschlussprojekt.Klassen;
using System.Windows.Controls;
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
    static class Statische_Variablen
    {
        public enum FARBE
        {
            ROT,
            GELB,
            BLAU,
            GRUEN,
            LEER
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
        public enum SPIELER_ART
        {
            NORMALER_SPIELER,
            COMPUTERGEGNER,
            LEER
        }

        public static List<Feld> start_felder = new List<Feld>();
        public static List<Feld> spiel_felder = new List<Feld>();
        public static List<Feld> ziel_felder = new List<Feld>();

        public static List<Figur> spieler_rot = new List<Figur>();
        public static List<Figur> spieler_gelb = new List<Figur>();
        public static List<Figur> spieler_gruen = new List<Figur>();
        public static List<Figur> spieler_blau = new List<Figur>();

        public static List<Spieler> alle_Spieler = new List<Spieler>();

        public static List<TextBox> Beitrittslabel = new List<TextBox>();

        public static BitmapImage Figur_rot = new BitmapImage();
        public static BitmapImage Figur_gelb = new BitmapImage();
        public static BitmapImage Figur_gruen = new BitmapImage();
        public static BitmapImage Figur_blau = new BitmapImage();

        public static int figur_höhe = 50;
        public static int figur_breite = 50;
        
        public static List<Host> alle_Hosts = new List<Host>();
        public static ListBox hosts = new ListBox();
        
    }
}
