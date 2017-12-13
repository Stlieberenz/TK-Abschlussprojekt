using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;

namespace Mensch_ärgere_dich_nicht.Klassen.SeitenFunktionen
{
    static class S_erstellen
    {
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
            return Netzwerkkommunikation.Eigene_IP_Adresse() + ";" + Spieler_Rot + ";" + Spieler_Gelb + ";" + Spieler_Grün + ";" + Spieler_Blau;
        }

        public static void Analysiere_Nachricht(string[] content)
        {
            switch (content[0])
            {
                case "Clientanfrage":
                    {

                        break;
                    }
                case "Clientabsage":
                    {
                        break;
                    }
            }
        }
    }
}
