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
        //Atribute -----------------------------------------------------------------------------------------------------------------------------
        public delegate void Click_Event();

        public static int index_rot_alt, index_gelb_alt, index_grün_alt, index_blau_alt; // Dienen zum Wiederherstellen der Werte im Fehlerfall

        public static string Spieler_Rot, Spieler_Gelb, Spieler_Grün, Spieler_Blau;

        public static Button versteckter_Button;

        public static Button Start_Button;

        public static bool UDP_Threadstatus;

        //Senden / Empfangen -----------------------------------------------------------------------------------------------
        public static void Warte_auf_Spieler()
        {
            while (UDP_Threadstatus)
            {
                if (Prüfe_Startfähigkeit())
                {
                    UDP_Threadstatus = false;
                    Starte_Spiel();
                    break;
                }
                Netzwerkkommunikation.Start_TCP_Listener();
            }
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
                        if (Prüfe_anfrage(content)) Erstelle_Spieler(content);
                        break;
                    }
                case "Clientabsage":
                    {
                        Entferne_Client(content);
                        break;
                    }
            }
            Aktualisiere_GUI();
        }

        public static void Sende_UDP()
        {
            while (UDP_Threadstatus)
            {
                Netzwerkkommunikation.Send_UDP_BC_Packet(Generiere_UDP_Nachricht());
                Thread.Sleep(1000);
            }
        }

        private static string Generiere_UDP_Nachricht()
        {
            return "Client" + ";" +
                "Spielangebot" + ";" +
                Netzwerkkommunikation.Eigene_IP_Adresse() + ";" +
                "Spielname" + ";" +
                Spieler_Rot + ";" +
                Spieler_Gelb + ";" +
                Spieler_Grün + ";" +
                Spieler_Blau;
        }

        // Funktionen zum Überprüfen von  dingen -----------------------------------------------------------------------------
        private static bool Prüfe_Startfähigkeit()
        {

            if (Prüfe_Spielernamen(Spieler_Rot) && Prüfe_Spielernamen(Spieler_Gelb) &&
                Prüfe_Spielernamen(Spieler_Grün) && Prüfe_Spielernamen(Spieler_Blau)) return true;
            else return false;
        }

        private static bool Prüfe_Spielernamen(string name)
        {
            if (name != "Offen") return true;
            else return false;
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
        { // prüft, dass immer mindestens 2 Spieler mit einander Spielen
            int prüfsumme = 0;
            if (index_rot_alt != 1) prüfsumme += 1;
            if (index_gelb_alt != 1) prüfsumme += 1;
            if (index_grün_alt != 1) prüfsumme += 1;
            if (index_blau_alt != 1) prüfsumme += 1;

            if (prüfsumme > 1) return true;
            else return false;
            
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

        private static int Wähle_richtigen_Wert_aus(ComboBox CB_aktuell)
        {
            //Stellt den alten Wert einer Auswahlbox wiederher, wenn man eine Falsche auswahl getroffen hat
            if (CB_aktuell.Name.Contains("Rot")) return index_rot_alt;
            else if (CB_aktuell.Name.Contains("Gelb")) return index_gelb_alt;
            else if (CB_aktuell.Name.Contains("Grün")) return index_grün_alt;
            else if (CB_aktuell.Name.Contains("Blau")) return index_blau_alt;
            else return 1;
        }


        // Reaktionen auf ausgewertete Nachrichten ------------------------------------------------------------------------------------------
        private static void Aktualisiere_GUI()
        {
            foreach(Spieler spieler in Spielfeld.alle_Mitspieler)
            {
                switch (spieler.farbe)
                {
                    case Statische_Variablen.FARBE.ROT: Spieler_Rot = spieler.name; break;
                    case Statische_Variablen.FARBE.GELB: Spieler_Gelb = spieler.name; break;
                    case Statische_Variablen.FARBE.GRÜN: Spieler_Grün = spieler.name; break;
                    case Statische_Variablen.FARBE.BLAU: Spieler_Blau = spieler.name; break;
                }
            }
            versteckter_Button.Dispatcher.Invoke(new Click_Event(() => 
            versteckter_Button.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent))));
        }

        private static void Entferne_Client(string[] content)
        {
            int index_zum_Löschen = -1;
            foreach (Spieler spieler in Spielfeld.alle_Mitspieler) if (spieler.name == content[1]) index_zum_Löschen = Spielfeld.alle_Mitspieler.IndexOf(spieler);
            Spielfeld.alle_Mitspieler.RemoveAt(index_zum_Löschen);
        }

        private static void Erstelle_Spieler(string[] content)
        {
            new Spieler(Ermittle_Spielerfarbe(content[0]), content[1], IPAddress.Parse(content[2]));
        }

        private static Statische_Variablen.FARBE Ermittle_Spielerfarbe(string farbe)
        {
            if (farbe == Statische_Variablen.FARBE.ROT.ToString()) return Statische_Variablen.FARBE.ROT;
            else if (farbe == Statische_Variablen.FARBE.GELB.ToString()) return Statische_Variablen.FARBE.GELB;
            else if (farbe == Statische_Variablen.FARBE.GRÜN.ToString()) return Statische_Variablen.FARBE.GRÜN;
            else if (farbe == Statische_Variablen.FARBE.BLAU.ToString()) return Statische_Variablen.FARBE.BLAU;
            else return Statische_Variablen.FARBE.NULL;
        }

        // Spielstart ------------------------------------------------------------------------------------------------------------------------
        private static void Starte_Spiel()
        {
            // Erstellt Computergegner
            if (Spieler_Rot == "Computergegner") Erstelle_Spieler(new string[] { "ROT", "CP Gegner Rot", Netzwerkkommunikation.Eigene_IP_Adresse() });
            if (Spieler_Gelb == "Computergegner") Erstelle_Spieler(new string[] { "GELB", "CP Gegner Gelb", Netzwerkkommunikation.Eigene_IP_Adresse() });
            if (Spieler_Grün == "Computergegner") Erstelle_Spieler(new string[] { "GRÜN", "CP Gegner Grün", Netzwerkkommunikation.Eigene_IP_Adresse() });
            if (Spieler_Blau == "Computergegner") Erstelle_Spieler(new string[] { "BLAU", "CP Gegner Blau", Netzwerkkommunikation.Eigene_IP_Adresse() });
            if (Spieler_Rot == "Ich") Erstelle_Spieler(new string[] { "ROT", Statische_Variablen.lokaler_Spieler, Netzwerkkommunikation.Eigene_IP_Adresse()});
            if (Spieler_Gelb == "Ich") Erstelle_Spieler(new string[] { "GELB", Statische_Variablen.lokaler_Spieler, Netzwerkkommunikation.Eigene_IP_Adresse() });
            if (Spieler_Grün == "Ich") Erstelle_Spieler(new string[] { "GRÜN", Statische_Variablen.lokaler_Spieler, Netzwerkkommunikation.Eigene_IP_Adresse()});
            if (Spieler_Blau == "Ich") Erstelle_Spieler(new string[] { "BLAU", Statische_Variablen.lokaler_Spieler, Netzwerkkommunikation.Eigene_IP_Adresse() });
            Start_Button.Dispatcher.Invoke(new Click_Event(Start_Invoker));
        }

        private static void Start_Invoker()
        {
            Start_Button.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
        }

        public static string Generiere_Startnachricht()
        {
            // Farbe,Name,IP
            string result = "Client;" + Spielfeld.alle_Mitspieler[0].farbe + ";"
                             + Spielfeld.alle_Mitspieler[0].name + ";"
                             + Spielfeld.alle_Mitspieler[0].ip.Address + ";"
                             + Spielfeld.alle_Mitspieler[1].farbe + ";"
                             + Spielfeld.alle_Mitspieler[1].name + ";"
                             + Spielfeld.alle_Mitspieler[1].ip.Address + ";";
            if (Spielfeld.alle_Mitspieler.Count >= 3)
            {
                result += Spielfeld.alle_Mitspieler[2].farbe + ";"
                        + Spielfeld.alle_Mitspieler[2].name + ";"
                        + Spielfeld.alle_Mitspieler[2].ip.Address + ";";
            }
            if (Spielfeld.alle_Mitspieler.Count >= 4)
            {
                result += Spielfeld.alle_Mitspieler[3].farbe + ";"
                       + Spielfeld.alle_Mitspieler[3].name + ";"
                       + Spielfeld.alle_Mitspieler[3].ip.Address + ";";
            }
            return result;
        }
    }
}
