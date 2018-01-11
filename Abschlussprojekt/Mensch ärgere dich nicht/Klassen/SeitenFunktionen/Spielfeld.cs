using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using static Mensch_ärgere_dich_nicht.Statische_Variablen;

namespace Mensch_ärgere_dich_nicht.Klassen.SeitenFunktionen
{
    static class Spielfeld
    {
        public static List<Spieler> alle_Mitspieler = new List<Spieler>();
        public static List<Figur> alle_Figuren = new List<Figur>();
        public static List<Feld> alle_Felder = new List<Feld>();
        public static List<Image> alle_Zahlen = new List<Image>();

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

        public static List<Label> Spieler_namen_Label = new List<Label>();

        public static Grid spielfeld;
        public static Button BTN_Würfel;
        public static Spieler aktiver_Spieler;

        public delegate void BTN_Funktion(bool value);
        public delegate void Funktion0(FARBE value);
        public delegate void Funktion(int value);
        public delegate void Funktion1(string value1, string value2, string value3);
        public static bool spielstatus = true;
        public static int würfelzahl;

        //
        // Initialisierung verschiedener Objeckte und Werten ------------------------------------------------------------

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
            new Feld(SPIELFELD_ART.SPIELFELD, 0, FARBE.NULL, 10, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 1, FARBE.NULL, 9, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 2, FARBE.NULL, 8, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 3, FARBE.NULL, 7, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 4, FARBE.NULL, 6, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 5, FARBE.NULL, 6, 3);
            new Feld(SPIELFELD_ART.SPIELFELD, 6, FARBE.NULL, 6, 2);
            new Feld(SPIELFELD_ART.SPIELFELD, 7, FARBE.NULL, 6, 1);
            new Feld(SPIELFELD_ART.SPIELFELD, 8, FARBE.NULL, 6, 0);
            new Feld(SPIELFELD_ART.SPIELFELD, 9, FARBE.NULL, 5, 0);
            new Feld(SPIELFELD_ART.SPIELFELD, 10, FARBE.NULL, 4, 0);
            new Feld(SPIELFELD_ART.SPIELFELD, 11, FARBE.NULL, 4, 1);
            new Feld(SPIELFELD_ART.SPIELFELD, 12, FARBE.NULL, 4, 2);
            new Feld(SPIELFELD_ART.SPIELFELD, 13, FARBE.NULL, 4, 3);
            new Feld(SPIELFELD_ART.SPIELFELD, 14, FARBE.NULL, 4, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 15, FARBE.NULL, 3, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 16, FARBE.NULL, 2, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 17, FARBE.NULL, 1, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 18, FARBE.NULL, 0, 4);
            new Feld(SPIELFELD_ART.SPIELFELD, 19, FARBE.NULL, 0, 5);
            new Feld(SPIELFELD_ART.SPIELFELD, 20, FARBE.NULL, 0, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 21, FARBE.NULL, 1, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 22, FARBE.NULL, 2, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 23, FARBE.NULL, 3, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 24, FARBE.NULL, 4, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 25, FARBE.NULL, 4, 7);
            new Feld(SPIELFELD_ART.SPIELFELD, 26, FARBE.NULL, 4, 8);
            new Feld(SPIELFELD_ART.SPIELFELD, 27, FARBE.NULL, 4, 9);
            new Feld(SPIELFELD_ART.SPIELFELD, 28, FARBE.NULL, 4, 10);
            new Feld(SPIELFELD_ART.SPIELFELD, 29, FARBE.NULL, 5, 10);
            new Feld(SPIELFELD_ART.SPIELFELD, 30, FARBE.NULL, 6, 10);
            new Feld(SPIELFELD_ART.SPIELFELD, 31, FARBE.NULL, 6, 9);
            new Feld(SPIELFELD_ART.SPIELFELD, 32, FARBE.NULL, 6, 8);
            new Feld(SPIELFELD_ART.SPIELFELD, 33, FARBE.NULL, 6, 7);
            new Feld(SPIELFELD_ART.SPIELFELD, 34, FARBE.NULL, 6, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 35, FARBE.NULL, 7, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 36, FARBE.NULL, 8, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 37, FARBE.NULL, 9, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 38, FARBE.NULL, 10, 6);
            new Feld(SPIELFELD_ART.SPIELFELD, 39, FARBE.NULL, 10, 5);


            new Feld(SPIELFELD_ART.HAUS, 0, FARBE.ROT, 10, 1);
            new Feld(SPIELFELD_ART.HAUS, 1, FARBE.ROT, 9, 1);
            new Feld(SPIELFELD_ART.HAUS, 2, FARBE.ROT, 9, 0);
            new Feld(SPIELFELD_ART.HAUS, 3, FARBE.ROT, 10, 0);
            new Feld(SPIELFELD_ART.HAUS, 0, FARBE.GELB, 1, 1);
            new Feld(SPIELFELD_ART.HAUS, 1, FARBE.GELB, 0, 1);
            new Feld(SPIELFELD_ART.HAUS, 2, FARBE.GELB, 0, 0);
            new Feld(SPIELFELD_ART.HAUS, 3, FARBE.GELB, 1, 0);
            new Feld(SPIELFELD_ART.HAUS, 0, FARBE.GRÜN, 1, 10);
            new Feld(SPIELFELD_ART.HAUS, 1, FARBE.GRÜN, 0, 10);
            new Feld(SPIELFELD_ART.HAUS, 2, FARBE.GRÜN, 0, 9);
            new Feld(SPIELFELD_ART.HAUS, 3, FARBE.GRÜN, 1, 9);
            new Feld(SPIELFELD_ART.HAUS, 0, FARBE.BLAU, 10, 10);
            new Feld(SPIELFELD_ART.HAUS, 1, FARBE.BLAU, 9, 10);
            new Feld(SPIELFELD_ART.HAUS, 2, FARBE.BLAU, 9, 9);
            new Feld(SPIELFELD_ART.HAUS, 3, FARBE.BLAU, 10, 9);


            new Feld(SPIELFELD_ART.ZIEL, 0, FARBE.ROT, 9, 5);
            new Feld(SPIELFELD_ART.ZIEL, 1, FARBE.ROT, 8, 5);
            new Feld(SPIELFELD_ART.ZIEL, 2, FARBE.ROT, 7, 5);
            new Feld(SPIELFELD_ART.ZIEL, 3, FARBE.ROT, 6, 5);
            new Feld(SPIELFELD_ART.ZIEL, 0, FARBE.GELB, 5, 1);
            new Feld(SPIELFELD_ART.ZIEL, 1, FARBE.GELB, 5, 2);
            new Feld(SPIELFELD_ART.ZIEL, 2, FARBE.GELB, 5, 3);
            new Feld(SPIELFELD_ART.ZIEL, 3, FARBE.GELB, 5, 4);
            new Feld(SPIELFELD_ART.ZIEL, 0, FARBE.GRÜN, 1, 5);
            new Feld(SPIELFELD_ART.ZIEL, 1, FARBE.GRÜN, 2, 5);
            new Feld(SPIELFELD_ART.ZIEL, 2, FARBE.GRÜN, 3, 5);
            new Feld(SPIELFELD_ART.ZIEL, 3, FARBE.GRÜN, 4, 5);
            new Feld(SPIELFELD_ART.ZIEL, 0, FARBE.BLAU, 5, 9);
            new Feld(SPIELFELD_ART.ZIEL, 1, FARBE.BLAU, 5, 8);
            new Feld(SPIELFELD_ART.ZIEL, 2, FARBE.BLAU, 5, 7);
            new Feld(SPIELFELD_ART.ZIEL, 3, FARBE.BLAU, 5, 6);
        }

        private static void Referenziere_Figurlisten()
        {
            foreach(Figur figur in alle_Figuren)
            {
                switch (figur.farbe)
                {
                    case FARBE.ROT:  rote_Figuren.Add(figur); break;
                    case FARBE.GELB: gelbe_Figuren.Add(figur); break;
                    case FARBE.GRÜN: grüne_Figuren.Add(figur); break;
                    case FARBE.BLAU: blaue_Figuren.Add(figur); break;
                }
            }
        }

        private static void Referenzeier_Feldllisten()
        {
            foreach(Feld feld in alle_Felder)
            {
                switch (feld.spielfeld_art)
                {
                    case SPIELFELD_ART.HAUS: alle_Hausfelder.Add(feld);break;
                    case SPIELFELD_ART.ZIEL: alle_Zielfelder.Add(feld);break;
                    case SPIELFELD_ART.SPIELFELD: alle_Spielfelder.Add(feld);break;
                }
            }
        }


        //Senden / Empfangen -----------------------------------------------------------------------------------------------
        public static void Analysiere_Nachricht(string[] content)
        {
            if (content[0].Contains("Spielrecht_update"))//Schema-> "Spielrecht_update","Name"
            {
                Spielrecht_update(content[1]);
            }
            if (content[0].Contains("Figur_update")) //Schema-> "Figur_update","Farbe","Figur-id","Zielfeld-ID"
            {
                spielfeld.Dispatcher.Invoke(new Funktion1(Bewege_Figur),content[1],content[2],content[3]);
            }
            if (content[0].Contains("Würfel_update"))//Schema-> "Würfel_update","Zahl"
            {
                spielfeld.Dispatcher.Invoke(new Funktion(Zeige_Zahl),Convert.ToInt32(content[1]));
            }
            if (content[0].Contains("Spielende"))
            {

            }
            if (content[0].Contains("Chatnachricht"))
            {

            }
        }

        public static void TCP_Listener()
        {
            while (spielstatus)
            {
                //testcode////////////////////////////////////////////////////////////////
                Analysiere_Nachricht(new string[] { "Würfel_update", "6" });
                Analysiere_Nachricht(new string[] { "Figur_update", "GELB", "1", "10"});
                Analysiere_Nachricht(new string[] { "Figur_update", "ROT", "1", "20" });
                /*
                for (int i = 0; i < 44; i++)
                {
                    Analysiere_Nachricht(new string[] { "Figur_update", "GELB", "1", i.ToString() });
                    Analysiere_Nachricht(new string[] { "Figur_update", "BLAU", "1", i.ToString() });
                    Analysiere_Nachricht(new string[] { "Figur_update", "ROT", "1", i.ToString() });
                    Analysiere_Nachricht(new string[] { "Figur_update", "GRÜN", "1", i.ToString() });
                    Thread.Sleep(100);
                }*/
                //testcode///////////////////////////////////////////////////////////////
                Netzwerkkommunikation.Start_TCP_Listener();
            }
        }


        //Spielrecht -------------------------------------------------------------------------------------------------------
        public static void Gebe_Spielrecht_weiter()
        {
            Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Mitspieler;Spielrecht;" + Ermittle_nächsten_Spieler(aktiver_Spieler.farbe).name);
        }

        public static void Spielrecht_update(string name)
        {
            foreach(Spieler spieler in alle_Mitspieler)
            {
                if (spieler.name == name )
                {
                    aktiver_Spieler = spieler;
                    rootFrame.Dispatcher.Invoke(new Funktion0(Highlight_Spieler),spieler.farbe);
                    if (name == lokaler_Spieler) Bekomme_Spielrecht();
                }
            }
        }

        private static void Highlight_Spieler(FARBE farbe)
        {
            Spieler_namen_Label[0].Background = System.Windows.Media.Brushes.Transparent;
            Spieler_namen_Label[1].Background = System.Windows.Media.Brushes.Transparent;
            Spieler_namen_Label[2].Background = System.Windows.Media.Brushes.Transparent;
            Spieler_namen_Label[3].Background = System.Windows.Media.Brushes.Transparent;
            switch (farbe)
            {
                case FARBE.ROT: Spieler_namen_Label[0].Background = System.Windows.Media.Brushes.Green; break;
                case FARBE.GELB: Spieler_namen_Label[1].Background = System.Windows.Media.Brushes.Green; break;
                case FARBE.GRÜN: Spieler_namen_Label[2].Background = System.Windows.Media.Brushes.Green; break;
                case FARBE.BLAU: Spieler_namen_Label[3].Background = System.Windows.Media.Brushes.Green; break;
            }
        }

        private static void Bekomme_Spielrecht()
        {
            BTN_Würfel.Dispatcher.Invoke(new BTN_Funktion(Aktiviere_Deaktiviere_Würfel), true);
        }

        private static void Aktiviere_Deaktiviere_Würfel(bool value)
        {
            BTN_Würfel.IsEnabled = value;
        }

        private static Spieler Ermittle_nächsten_Spieler(FARBE farbe)
        {
            switch (farbe)
            {
                case FARBE.ROT:
                    {
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.GELB) return spieler;
                        }
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.GRÜN) return spieler;
                        }
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.BLAU) return spieler;
                        }
                        break;
                    }
                case FARBE.GELB:
                    {

                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.GRÜN) return spieler;
                        }
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.BLAU) return spieler;
                        }
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.ROT) return spieler;
                        }
                        break;
                    }
                case FARBE.GRÜN:
                    {
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.BLAU) return spieler;
                        }
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.ROT) return spieler;
                        }
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.GELB) return spieler;
                        }
                        break;
                    }
                case FARBE.BLAU:
                    {
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.ROT) return spieler;
                        }
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.GELB) return spieler;
                        }
                        foreach (Spieler spieler in alle_Mitspieler)
                        {
                            if (spieler.farbe == FARBE.GRÜN) return spieler;
                        }
                        break;
                    }
            }
            return null;
        }

        //Würfeln ----------------------------------------------------------------------------------------------------------
        public static void Würfeln()
        {
            würfelzahl = new Random().Next(1, 7);
            Zeige_Zahl(würfelzahl);
            Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Mitspieler;Würfel_update;" + würfelzahl);
        }

        private static void Zeige_Zahl(int value)
        {
            alle_Zahlen[0].Visibility = Visibility.Hidden;
            alle_Zahlen[1].Visibility = Visibility.Hidden;
            alle_Zahlen[2].Visibility = Visibility.Hidden;
            alle_Zahlen[3].Visibility = Visibility.Hidden;
            alle_Zahlen[4].Visibility = Visibility.Hidden;
            alle_Zahlen[5].Visibility = Visibility.Hidden;
            switch (Convert.ToInt32(value))
            {
                case 1: alle_Zahlen[0].Visibility = Visibility.Visible; break;
                case 2: alle_Zahlen[1].Visibility = Visibility.Visible; break;
                case 3: alle_Zahlen[2].Visibility = Visibility.Visible; break;
                case 4: alle_Zahlen[3].Visibility = Visibility.Visible; break;
                case 5: alle_Zahlen[4].Visibility = Visibility.Visible; break;
                case 6: alle_Zahlen[5].Visibility = Visibility.Visible; break;
            }
        }

        //Figur bewegen ----------------------------------------------------------------------------------------------------

        private static void Bewege_Figur(string value1, string value2, string value3 )
        {
            int index_figur = Convert.ToInt32(value2);
            int index_feld = Convert.ToInt32(value3);
            switch (value1)
            {
                case "ROT":
                    {
                        //Sorgt dafür das wenn das Feld besetzt ist, es zuerst frei gemacht wird.
                        foreach (Figur figur in alle_Figuren) if (figur.aktuelle_position == rote_Figuren[index_figur].wegstrecke[index_feld]) figur.Setze_Figur(figur.Haus_position);
                        //Setzt figur auf position
                        rote_Figuren[index_figur].Setze_Figur(rote_Figuren[index_figur].wegstrecke[index_feld]);
                        break;
                    }
                case "GELB":
                    {
                        foreach (Figur figur in alle_Figuren) if (figur.aktuelle_position == gelbe_Figuren[index_figur].wegstrecke[index_feld]) figur.Setze_Figur(figur.Haus_position);
                        gelbe_Figuren[index_figur].Setze_Figur(gelbe_Figuren[index_figur].wegstrecke[index_feld]);
                        break;
                    }
                case "GRÜN":
                    {
                        foreach (Figur figur in alle_Figuren) if (figur.aktuelle_position == grüne_Figuren[index_figur].wegstrecke[index_feld]) figur.Setze_Figur(figur.Haus_position);
                        grüne_Figuren[index_figur].Setze_Figur(grüne_Figuren[index_figur].wegstrecke[index_feld]);
                        break;
                    }
                case "BLAU":
                    {
                        foreach (Figur figur in alle_Figuren) if (figur.aktuelle_position == blaue_Figuren[index_figur].wegstrecke[index_feld]) figur.Setze_Figur(figur.Haus_position);
                        blaue_Figuren[index_figur].Setze_Figur(blaue_Figuren[index_figur].wegstrecke[index_feld]);
                        break;
                    }
            }
        }
    }
}
