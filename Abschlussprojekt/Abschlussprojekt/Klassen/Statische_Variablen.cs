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
using System.Net.Sockets;

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
        public enum AKTIVE_SEITE
        {
            SPIEL_SUCHEN,
            SPIEL_ERSTELLEN,
            SPIELWIESE,
            STARTSEITE
        }

        public static bool initialized = false;
        public static string Host_name;
        public static MainWindow mainWindow = new MainWindow();
        public static AKTIVE_SEITE aktive_Seite;
        public static Random zufallszahl = new Random();
        public static int z;

        public static List<Feld> start_felder = new List<Feld>();
        public static List<Feld> spiel_felder = new List<Feld>();
        public static List<Feld> ziel_felder = new List<Feld>();

        public static List<Figur> spieler_rot = new List<Figur>();
        public static List<Figur> spieler_gelb = new List<Figur>();
        public static List<Figur> spieler_gruen = new List<Figur>();
        public static List<Figur> spieler_blau = new List<Figur>();

        public static List<Figur> figuren_lokal; // ist nur ein verweis auf eine der obigen listen.

        public static List<Spieler> alle_Spieler = new List<Spieler>();
        public static Spieler lokaler_spieler;

        public static List<TextBox> Beitrittslabel = new List<TextBox>();//Für wenn man dem Spiel beitritt
        public static List<TextBox> Spielerstellenlabel = new List<TextBox>();//Für wenn man ein Spiel Hostet
        
        public static BitmapImage Figur_rot = new BitmapImage();
        public static BitmapImage Figur_gelb = new BitmapImage();
        public static BitmapImage Figur_gruen = new BitmapImage();
        public static BitmapImage Figur_blau = new BitmapImage();

        public static int figur_höhe = 50;
        public static int figur_breite = 50;
        
        public static List<Host> alle_Hosts = new List<Host>();
        public static List<string> known_IP_S = new List<string>();
        public static ListBox hosts = new ListBox();

        public static bool anfragen_result;

        public static IPAddress eigene_IPAddresse = null;
        public static IPAddress nächster_Spieler = null;
        public static List<IPAddress> broadcast_IPAdresse = new List<IPAddress>(0);
        public static int port = 50000;
        public static UdpClient receivingUdpClient = new UdpClient(port);

        
        public static Grid Spiel_suchen_Grid = new Grid();

        public static int verbleibende_würfelversuche;
        public static Button Würfel = new Button();
        public static bool Figuren_waren_in_Haus;
    }
}
