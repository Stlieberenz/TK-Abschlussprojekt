using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Net;

namespace Mensch_ärgere_dich_nicht.Klassen.SeitenFunktionen
{
    static class S_erstellen
    {
        private static List<Spieler> spielerliste = new List<Spieler>();
        public static int index_rot_alt, index_gelb_alt, index_grün_alt, index_blau_alt; // Dienen zum Wiederherstellen der Werte im Fehlerfall

        public static string Spieler_Rot, Spieler_Gelb, Spieler_Grün, Spieler_Blau;

        public static bool UDP_Threadstatus;

        public static void Warte_auf_Spieler()
        {
            while (UDP_Threadstatus)
            {
                Netzwerkkommunikation.Start_TCP_Listener();
            }
        }

        public static void Sende_UDP()
        {
            while (UDP_Threadstatus)
            {
                Netzwerkkommunikation.Send_UDP_BC_Packet(Generiere_UDP_Nachricht());
                Thread.Sleep(1000);
            }
        }

        public static bool Prüfe_auswahl(List<int> Indexe,ComboBox CB_aktuell)
        {
            int prüfsumme1 = 0;
            int prüfsumme2 = 0;
            bool result = true;
            foreach (int index in Indexe) if (index == 1) prüfsumme1 += 1;
            foreach (int index in Indexe) if (index == 3) prüfsumme2 += 1;
            if (prüfsumme1 == 4 || prüfsumme2 == 0) CB_aktuell.SelectedIndex = Wähle_richtigen_Wert_aus(CB_aktuell); // Hier wurde dann eine falsche Auswahl getroffen und der alte Wert wiederhergestellt
            if (prüfsumme2 > 1) result = false;
            return result;
        }

        internal static bool Prüfe_auswahl()
        {
            int prüfsumme = 0;
            if (index_rot_alt != 1) prüfsumme += 1;
            if (index_gelb_alt != 1) prüfsumme += 1;
            if (index_grün_alt != 1) prüfsumme += 1;
            if (index_blau_alt != 1) prüfsumme += 1;
            if (prüfsumme > 1) return true;
            else
            {
                return false;
            }
        }

        private static int Wähle_richtigen_Wert_aus(ComboBox CB_aktuell)
        {
            if (CB_aktuell.Name.Contains("Rot")) return index_rot_alt;
            else if (CB_aktuell.Name.Contains("Gelb")) return index_gelb_alt;
            else if (CB_aktuell.Name.Contains("Grün")) return index_grün_alt;
            else if (CB_aktuell.Name.Contains("Blau")) return index_blau_alt;
            else return 1;
        }

        private static string Generiere_UDP_Nachricht()
        {
            return "Client" + ";"+ 
                "Spielangebot" + ";" + 
                Netzwerkkommunikation.Eigene_IP_Adresse() + ";" + 
                "Spielname" + ";" + 
                Spieler_Rot + ";" + 
                Spieler_Gelb + ";" + 
                Spieler_Grün + ";" + 
                Spieler_Blau;
        }

        public static void Analysiere_Nachricht(string[] content)
        {
            //Format der Nachricht: 
            // index 0 = "Art der Nachricht"
            // index 1 = "Spielername"
            // index 2 = "Farbe"
            // index 3 = "IP-Adresse"
            switch (content[0])
            {
                case "Clientanfrage":
                    {
                        if (Prüfe_anfrage(content)) spielerliste.Add(Erstelle_Spieler(content));
                        break;
                    }
                case "Clientabsage":
                    {
                        Entferne_Client(content);
                        break;
                    }
            }
        }

        private static void Entferne_Client(string[] content)
        {
            int index_zum_Löschen = -1;
            foreach (Spieler spieler in spielerliste) if (spieler.name == content[1]) index_zum_Löschen = spielerliste.IndexOf(spieler);
            spielerliste.RemoveAt(index_zum_Löschen);
        }

        private static Spieler Erstelle_Spieler(string[] content)
        {
            return new Spieler(Ermittle_Spielerfarbe(content[2]), content[1], IPAddress.Parse(content[3]));
        }

        private static Statische_Variablen.FARBE Ermittle_Spielerfarbe(string farbe)
        {
            if (farbe == Statische_Variablen.FARBE.ROT.ToString()) return Statische_Variablen.FARBE.ROT;
            else if (farbe == Statische_Variablen.FARBE.GELB.ToString()) return Statische_Variablen.FARBE.GELB;
            else if (farbe == Statische_Variablen.FARBE.GRÜN.ToString()) return Statische_Variablen.FARBE.GRÜN;
            else if (farbe == Statische_Variablen.FARBE.BLAU.ToString()) return Statische_Variablen.FARBE.BLAU;
            else return Statische_Variablen.FARBE.NULL;
        }

        private static bool Prüfe_anfrage(string[] content)
        {
            try
            {
                IPAddress test = IPAddress.Parse(content[3]);
            }
            catch
            {
                return false;
            }
            Statische_Variablen.FARBE Client_Farbe = Ermittle_Spielerfarbe(content[2]);
            switch (Client_Farbe)
            {
                case Statische_Variablen.FARBE.ROT:
                    {
                        if (Spieler_Rot != "Offen")
                            return false;
                        break;
                    }
                case Statische_Variablen.FARBE.GELB:
                    {
                        if (Spieler_Gelb != "Offen")
                            return false;
                        break;
                    }
                case Statische_Variablen.FARBE.GRÜN:
                    {
                        if (Spieler_Grün != "Offen")
                            return false;
                        break;
                    }
                case Statische_Variablen.FARBE.BLAU:
                    {
                        if (Spieler_Blau != "Offen")
                            return false;
                        break;
                    }
                case Statische_Variablen.FARBE.NULL: Klassen.Netzwerkkommunikation.Send_TCP_Packet("Client,Absage", IPAddress.Parse(content[3])); break;
            }

            return true;
        }
    }
}
