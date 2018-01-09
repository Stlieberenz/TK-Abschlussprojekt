using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mensch_ärgere_dich_nicht.Klassen.SeitenFunktionen
{
    static class Spielfeld
    {
        public static List<Spieler> alle_Mitspieler = new List<Spieler>();
        public static List<Figur> alle_Figuren = new List<Figur>();
        public static List<Feld> alle_Felder = new List<Feld>();

        public static List<Feld> alle_Hausfelder = new List<Feld>();
        public static List<Feld> alle_Spielfelder = new List<Feld>();
        public static List<Feld> alle_Zielfelder = new List<Feld>();

        public static List<Feld> alle_Hausfelder_Rot = new List<Feld>();
        public static List<Feld> alle_Hausfelder_Gelb = new List<Feld>();
        public static List<Feld> alle_Hausfelder_Grün = new List<Feld>();
        public static List<Feld> alle_Hausfelder_Blau = new List<Feld>();

        public static List<Feld> alle_Zielfelder_Rot = new List<Feld>();
        public static List<Feld> alle_Zielfelder_Gelb = new List<Feld>();
        public static List<Feld> alle_Zielfelder_Grün = new List<Feld>();
        public static List<Feld> alle_Zielfelder_Blau = new List<Feld>();

        public static List<Figur> rote_Figuren = new List<Figur>();
        public static List<Figur> gelbe_Figuren = new List<Figur>();
        public static List<Figur> grüne_Figuren = new List<Figur>();
        public static List<Figur> blaue_Figuren = new List<Figur>();

        public static Grid spielfeld;
        public static Spieler aktiver_Spieler;

        //------------------------------------------------------------
        // Initialisierung verschiedener Objeckte und Werten

        public static void Erstelle_Oberfläche()
        {
            Erstelle_Felder();
            Erstelle_Figuren();
        }

        private static void Erstelle_Figuren() // Erstelle für jeden Mitspieler 4 Figuren in der richtigen Farbe
        {
            foreach (Spieler spieler in alle_Mitspieler)
            {
                for (int i = 0; i < 4; i++) new Figur(spieler.farbe, i);
            }
            foreach (Figur figur in alle_Figuren) spielfeld.Children.Add(figur.bild);
        }

        private static void Erstelle_Felder()
        {
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 0, Statische_Variablen.FARBE.NULL, 10, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 1, Statische_Variablen.FARBE.NULL, 9, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 2, Statische_Variablen.FARBE.NULL, 8, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 3, Statische_Variablen.FARBE.NULL, 7, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 4, Statische_Variablen.FARBE.NULL, 6, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 5, Statische_Variablen.FARBE.NULL, 6, 3);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 6, Statische_Variablen.FARBE.NULL, 6, 2);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 7, Statische_Variablen.FARBE.NULL, 6, 1);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 8, Statische_Variablen.FARBE.NULL, 6, 0);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 9, Statische_Variablen.FARBE.NULL, 5, 0);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 10, Statische_Variablen.FARBE.NULL, 4, 0);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 11, Statische_Variablen.FARBE.NULL, 4, 1);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 12, Statische_Variablen.FARBE.NULL, 4, 2);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 13, Statische_Variablen.FARBE.NULL, 4, 3);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 14, Statische_Variablen.FARBE.NULL, 4, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 15, Statische_Variablen.FARBE.NULL, 3, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 16, Statische_Variablen.FARBE.NULL, 2, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 17, Statische_Variablen.FARBE.NULL, 1, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 18, Statische_Variablen.FARBE.NULL, 0, 4);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 19, Statische_Variablen.FARBE.NULL, 0, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 20, Statische_Variablen.FARBE.NULL, 0, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 21, Statische_Variablen.FARBE.NULL, 1, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 22, Statische_Variablen.FARBE.NULL, 2, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 23, Statische_Variablen.FARBE.NULL, 3, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 24, Statische_Variablen.FARBE.NULL, 4, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 25, Statische_Variablen.FARBE.NULL, 4, 7);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 26, Statische_Variablen.FARBE.NULL, 4, 8);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 27, Statische_Variablen.FARBE.NULL, 4, 9);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 28, Statische_Variablen.FARBE.NULL, 4, 10);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 29, Statische_Variablen.FARBE.NULL, 5, 10);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 30, Statische_Variablen.FARBE.NULL, 6, 10);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 31, Statische_Variablen.FARBE.NULL, 6, 9);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 32, Statische_Variablen.FARBE.NULL, 6, 8);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 33, Statische_Variablen.FARBE.NULL, 6, 7);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 34, Statische_Variablen.FARBE.NULL, 6, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 35, Statische_Variablen.FARBE.NULL, 7, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 36, Statische_Variablen.FARBE.NULL, 8, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 37, Statische_Variablen.FARBE.NULL, 9, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 38, Statische_Variablen.FARBE.NULL, 10, 6);
            new Feld(Statische_Variablen.SPIELFELD_ART.SPIELFELD, 39, Statische_Variablen.FARBE.NULL, 10, 5);


            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 0, Statische_Variablen.FARBE.ROT, 10, 1);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 1, Statische_Variablen.FARBE.ROT, 9, 1);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 2, Statische_Variablen.FARBE.ROT, 9, 0);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 3, Statische_Variablen.FARBE.ROT, 10, 0);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 0, Statische_Variablen.FARBE.GELB, 1, 1);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 1, Statische_Variablen.FARBE.GELB, 0, 1);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 2, Statische_Variablen.FARBE.GELB, 0, 0);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 3, Statische_Variablen.FARBE.GELB, 1, 0);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 0, Statische_Variablen.FARBE.GRÜN, 1, 10);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 1, Statische_Variablen.FARBE.GRÜN, 0, 10);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 2, Statische_Variablen.FARBE.GRÜN, 0, 9);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 3, Statische_Variablen.FARBE.GRÜN, 1, 9);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 0, Statische_Variablen.FARBE.BLAU, 10, 10);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 1, Statische_Variablen.FARBE.BLAU, 9, 10);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 2, Statische_Variablen.FARBE.BLAU, 9, 9);
            new Feld(Statische_Variablen.SPIELFELD_ART.HAUS, 3, Statische_Variablen.FARBE.BLAU, 10, 9);


            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 0, Statische_Variablen.FARBE.ROT, 9, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 1, Statische_Variablen.FARBE.ROT, 8, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 2, Statische_Variablen.FARBE.ROT, 7, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 3, Statische_Variablen.FARBE.ROT, 6, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 0, Statische_Variablen.FARBE.GELB, 5, 1);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 1, Statische_Variablen.FARBE.GELB, 5, 2);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 2, Statische_Variablen.FARBE.GELB, 5, 2);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 3, Statische_Variablen.FARBE.GELB, 5, 3);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 0, Statische_Variablen.FARBE.GRÜN, 1, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 1, Statische_Variablen.FARBE.GRÜN, 2, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 2, Statische_Variablen.FARBE.GRÜN, 3, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 3, Statische_Variablen.FARBE.GRÜN, 4, 5);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 0, Statische_Variablen.FARBE.BLAU, 5, 9);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 1, Statische_Variablen.FARBE.BLAU, 5, 8);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 2, Statische_Variablen.FARBE.BLAU, 5, 7);
            new Feld(Statische_Variablen.SPIELFELD_ART.ZIEL, 3, Statische_Variablen.FARBE.BLAU, 5, 6);

        }

        private static void Referenziere_Figurlisten()
        {
            foreach(Figur figur in alle_Figuren)
            {
                switch (figur.farbe)
                {
                    case Statische_Variablen.FARBE.ROT:  rote_Figuren.Add(figur); break;
                    case Statische_Variablen.FARBE.GELB: gelbe_Figuren.Add(figur); break;
                    case Statische_Variablen.FARBE.GRÜN: grüne_Figuren.Add(figur); break;
                    case Statische_Variablen.FARBE.BLAU: blaue_Figuren.Add(figur); break;
                }
            }
        }

        private static void Referenzeier_Feldllisten()
        {
            foreach(Feld feld in alle_Felder)
            {
                switch (feld.spielfeld_art)
                {
                    case Statische_Variablen.SPIELFELD_ART.HAUS: alle_Hausfelder.Add(feld);break;
                    case Statische_Variablen.SPIELFELD_ART.ZIEL: alle_Zielfelder.Add(feld);break;
                    case Statische_Variablen.SPIELFELD_ART.SPIELFELD: alle_Spielfelder.Add(feld);break;
                }
            }
        }

        public static void Analysiere_Nachricht(string[] content)
        {

        }

        public static void Gebe_Spielrecht_weiter()
        {
            if (Ermittle_nächsten_Spieler().ip.Address.ToString() == Netzwerkkommunikation.Eigene_IP_Adresse())
            {
                Netzwerkkommunikation.Anlaysiere_IP_Paket("");
            }
            else
            {
                Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Spielrecht...");
            }
        }

        private static Spieler Ermittle_nächsten_Spieler()
        {
            return null;
        }
    }
}
