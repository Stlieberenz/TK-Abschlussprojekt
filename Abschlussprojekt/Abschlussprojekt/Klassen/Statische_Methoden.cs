using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
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
        public delegate void Click_Event();

        public delegate void Update_Aufgeben_btn();

        public static void Initialisiere_alle_Felder(Grid spielwiese_grid)
        {
            try
            {
                // Für jedes Image das ein Feld ist wird ein Feld Objeckt erzeugt.
                foreach (System.Windows.Controls.Image control_element in spielwiese_grid.Children)
                {
                    Point image_point = new Point(control_element.Margin.Left, control_element.Margin.Top);
                    if (control_element.Uid.Contains("Start_rot"))
                    {
                        Feld start_feld = new Feld(FARBE.ROT, FELD_EIGENSCHAFT.STARTPOSITION, image_point,Konvertiere_in_Feld_id(control_element.Uid));
                    }
                    else if (control_element.Uid.Contains("Start_gelb"))
                    {
                        Feld start_feld = new Feld(FARBE.GELB, FELD_EIGENSCHAFT.STARTPOSITION, image_point, Konvertiere_in_Feld_id(control_element.Uid));
                    }
                    else if (control_element.Uid.Contains("Start_gruen"))
                    {
                        Feld start_feld = new Feld(FARBE.GRUEN, FELD_EIGENSCHAFT.STARTPOSITION, image_point, Konvertiere_in_Feld_id(control_element.Uid));
                    }
                    else if (control_element.Uid.Contains("Start_blau"))
                    {
                        Feld start_feld = new Feld(FARBE.BLAU, FELD_EIGENSCHAFT.STARTPOSITION, image_point, Konvertiere_in_Feld_id(control_element.Uid));
                    }
                    else if (control_element.Uid.Contains("Feld_"))
                    {
                        Feld feld = new Feld(FARBE.LEER, FELD_EIGENSCHAFT.SPIELFELD, image_point, Konvertiere_in_Feld_id(control_element.Uid));
                    }
                    else if (control_element.Uid.Contains("Ziel_rot"))
                    {
                        Feld ziel_feld = new Feld(FARBE.ROT, FELD_EIGENSCHAFT.ZIEL, image_point, Konvertiere_in_Feld_id(control_element.Uid));
                    }
                    else if (control_element.Uid.Contains("Ziel_gelb"))
                    {
                        Feld ziel_feld = new Feld(FARBE.GELB, FELD_EIGENSCHAFT.ZIEL, image_point, Konvertiere_in_Feld_id(control_element.Uid));
                    }
                    else if (control_element.Uid.Contains("Ziel_gruen"))
                    {
                        Feld ziel_feld = new Feld(FARBE.GRUEN, FELD_EIGENSCHAFT.ZIEL, image_point, Konvertiere_in_Feld_id(control_element.Uid));
                    }
                    else if (control_element.Uid.Contains("Ziel_blau"))
                    {
                        Feld ziel_feld = new Feld(FARBE.BLAU, FELD_EIGENSCHAFT.ZIEL, image_point, Konvertiere_in_Feld_id(control_element.Uid));
                    }
                }
            }
            catch { }
        }

        private static int Konvertiere_in_Feld_id(string name)
        {
            string result = "";
            foreach(char c in name)
            {
                switch (c)
                {
                    case '0': result += "0"; break;
                    case '1': result += "1"; break;
                    case '2': result += "2"; break;
                    case '3': result += "3"; break;
                    case '4': result += "4"; break;
                    case '5': result += "5"; break;
                    case '6': result += "6"; break;
                    case '7': result += "7"; break;
                    case '8': result += "8"; break;
                    case '9': result += "9"; break;
                }
            }
            return Convert.ToInt32(result);
        }

        public static void Initialisiere_Spiel()
        {
            foreach(Spieler spieler in alle_Spieler)
            {
                spieler.Initialisiere_Figuren();
            }
        }

        public static List<Figur> Initialisiere_Figuren(FARBE farbe)
        {
            List<Figur> result = new List<Figur>();
            for (int i = 0; i < 4; i++)
            {
                Figur figur = new Figur(farbe,i);
                result.Add(figur);
            }
            return result;
        }

        public static void Initialisiere_Images_für_Figuren()
        {
            string pfad = Erzeuge_Dateipfad();
            Figur_rot.BeginInit();
            Figur_rot.UriSource = new Uri(pfad + "/Bilder/Figur_rot.bmp");
            Figur_rot.EndInit();
            Figur_gelb.BeginInit();
            Figur_gelb.UriSource = new Uri(pfad + "/Bilder/Figur_gelb.bmp");
            Figur_gelb.EndInit();
            Figur_gruen.BeginInit();
            Figur_gruen.UriSource = new Uri(pfad + "/Bilder/Figur_gruen.bmp");
            Figur_gruen.EndInit();
            Figur_blau.BeginInit();
            Figur_blau.UriSource = new Uri(pfad + "/Bilder/Figur_blau.bmp");
            Figur_blau.EndInit();
        }


        public static string Erzeuge_Dateipfad()
        {
            string temp = Directory.GetCurrentDirectory();
            string result = temp.Replace("bin\\Debug", ""); 
            return result;
        }

        public static Spieler Finde_Spieler_nach_Farbe(FARBE farbe)
        {
            foreach(Spieler spieler in alle_Spieler)
            {
                if (spieler.farbe == farbe) return spieler;
            }
            return null;
        }

        public static FARBE Erkenne_Farbe(string farbe)
        {
            switch (farbe)
            {
                case "rot": return FARBE.ROT; 
                case "gelb": return FARBE.GELB;
                case "gruen": return FARBE.GRUEN;
                case "blau": return FARBE.BLAU;
            }
            return FARBE.LEER;
        }

        public static string Konvertiere_FARBE_zu_string(FARBE farbe)
        {
            switch (farbe)
            {
                case FARBE.ROT: return "rot";
                case FARBE.GELB: return "gelb";
                case FARBE.GRUEN: return "gruen";
                case FARBE.BLAU: return "blau";
            }
            return null;
        }

        public static SPIELER_ART Erkenne_Spielerart(string spielername)
        {
            if (spielername.Contains("Computergegner")) return SPIELER_ART.COMPUTERGEGNER;
            else return SPIELER_ART.NORMALER_SPIELER;
        }

        public static Feld Finde_Feld(int x, int y)
        {
            foreach(Feld feld in spiel_felder)
            {
                if (feld.position.X == x && feld.position.Y == y)
                {
                    return feld;
                }
            }
            foreach(Feld feld in ziel_felder)
            {
                if (feld.position.X == x && feld.position.Y == y)
                {
                    return feld;
                }
            }
            return null;
        }

        public static int Ermittle_start_Spieler()
        {
            return zufallszahl.Next(0, alle_Spieler.Count);
        }

        public static bool Sind_alle_Figuren_im_Haus()
        {
            foreach(Figur figur in aktiver_spieler.eigene_Figuren)
            {
                if (figur.aktuelle_Position.feld_art == FELD_EIGENSCHAFT.SPIELFELD)
                {
                    return false;
                }
                if (figur.aktuelle_Position.feld_art == FELD_EIGENSCHAFT.ZIEL)
                {
                    bool temp = false;
                    if      ((figur.wegstecke[40].figur == null && figur.wegstecke[41].figur != null && figur.wegstecke[42].figur != null && figur.wegstecke[43].figur != null)) temp = true;
                    else if ((figur.wegstecke[40].figur == null && figur.wegstecke[41].figur == null && figur.wegstecke[42].figur != null && figur.wegstecke[43].figur != null)) temp = true; 
                    else if ((figur.wegstecke[40].figur == null && figur.wegstecke[41].figur == null && figur.wegstecke[42].figur == null && figur.wegstecke[43].figur != null)) temp = true;
                    if (temp != true) return false;
                }
            }
            return true;
        }
        
        public static bool Zug_ist_möglich(int zahl, Figur figur)
        {
            bool result = false;
            if (figur.a_Postition + zahl > -1 && figur.a_Postition + zahl < 44)
            {
                if (figur.wegstecke[figur.a_Postition + zahl].figur != null)
                {
                    if (figur.wegstecke[figur.a_Postition + zahl].figur.farbe == figur.farbe) figur.mögliche_Position = null;
                    else figur.mögliche_Position = figur.wegstecke[figur.a_Postition + zahl]; result = true;
                }
                else figur.mögliche_Position = figur.wegstecke[figur.a_Postition + zahl]; result = true;
            }
            else figur.mögliche_Position = null;
            return result;
        }

        public static bool Zug_ist_möglich(int zahl,List<Figur> figuren)
        {
            bool result = false;
            foreach (Figur figur in figuren)
            {
                if (figur.a_Postition + zahl > -1 && figur.a_Postition + zahl < 44)
                {
                    if (figur.wegstecke[figur.a_Postition + zahl].figur != null)
                    {
                        if (figur.wegstecke[figur.a_Postition + zahl].figur.farbe == figur.farbe) figur.mögliche_Position = null;
                        else figur.mögliche_Position = figur.wegstecke[figur.a_Postition + zahl]; result = true;
                    }
                    else figur.mögliche_Position = figur.wegstecke[figur.a_Postition + zahl]; result = true;
                }
                else figur.mögliche_Position = null;
            }
            return result;
        }

        public static void Figur_wurde_bewegt()
        {
            bool ziel_erreicht = Ziel_Erreicht();
            if (!ziel_erreicht && z != 6)
            {
                Forward_Spielrecht();
            }
            else if(!ziel_erreicht && z == 6)
            {
                if (aktiver_spieler.spieler_art != SPIELER_ART.COMPUTERGEGNER) Würfel.IsEnabled = true;
            }
            else
            {
                Sende_Spielende_an_Mitspieler();
            }
        }

        public static bool Ziel_Erreicht()
        {
            foreach(Figur figur in aktiver_spieler.eigene_Figuren)
            {
                if (figur.aktuelle_Position.feld_art != FELD_EIGENSCHAFT.ZIEL)
                {
                    return false;
                }
            }
            return true;
        }

        public static void Forward_Spielrecht()
        {
            aktiver_spieler.status = false;
            if (aktiver_spieler.nächster_Spieler.spieler_art == SPIELER_ART.NORMALER_SPIELER && aktiver_spieler.nächster_Spieler.ip.Address != eigene_IPAddresse.Address) Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Spielrecht," + aktiver_spieler.nächster_Spieler.name);
            else
            {
                aktiver_spieler.nächster_Spieler.status = true;
                aktiver_spieler = aktiver_spieler.nächster_Spieler;
                if (aktiver_spieler.spieler_art == SPIELER_ART.NORMALER_SPIELER) Würfel.Dispatcher.Invoke(new Click_Event(Würfel_einschalten));
                verbleibende_würfelversuche = 3;
            }
        }

        public static void Sende_Spielende_an_Mitspieler()
        {
            Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Spielende" + aktiver_spieler.name);
            Spielende();
        }

        public static void Spielende()
        {
            MessageBox.Show(aktiver_spieler.name + " hat das Spiel gewonnen !!!", "Spielende", MessageBoxButton.OK);
            Aufgeben.Dispatcher.Invoke(new Update_Aufgeben_btn(Update_aufgeben_btn));
        }

        public static void Würfel_einschalten()
        {
            Würfel.IsEnabled = true;
        }

        public static Spieler Ermittele_nächsten_Spieler(FARBE farbe)
        {
            switch (farbe)
            {
                case FARBE.ROT:
                    {
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.GELB) return spieler;
                        }
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.GRUEN) return spieler;
                        }
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.BLAU) return spieler;
                        }
                        break;
                    }
                case FARBE.GELB:
                    {

                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.GRUEN) return spieler;
                        }
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.BLAU) return spieler;
                        }
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.ROT) return spieler;
                        }
                        break;
                    }
                case FARBE.GRUEN:
                    {
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.BLAU) return spieler;
                        }
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.ROT) return spieler;
                        }
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.GELB) return spieler;
                        }
                        break;
                    }
                case FARBE.BLAU:
                    {
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.ROT) return spieler;
                        }
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.GELB) return spieler;
                        }
                        foreach (Spieler spieler in alle_Spieler)
                        {
                            if (spieler.farbe == FARBE.GRUEN) return spieler;
                        }
                        break;
                    }
            }
            return null;
        }

        public static string Erstelle_Startnachricht_für_clients()
        {
            string message = "Spielstart";
            int rest = alle_Spieler.Count;
            foreach (Spieler spieler in alle_Spieler)
            {
                message += "," + spieler.name + "," + spieler.ip.ToString() + "," + Statische_Methoden.Konvertiere_FARBE_zu_string(spieler.farbe);
            }
            switch (rest)
            {
                case 2: message += ",Geschlossen,_,_,Geschlossen,_,_"; break;
                case 3: message += ",Geschlossen,_,_"; break;
            }
            int s = Statische_Methoden.Ermittle_start_Spieler();
            if (s > 0)
            {
                alle_Spieler[s].status = true;
                message += "," + Statische_Methoden.Konvertiere_FARBE_zu_string(alle_Spieler[s].farbe);
            }
            return message;
        }

        public static void Update_aufgeben_btn()
        {
            Aufgeben.Content = "Beenden";
        }
    }
}