using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Abschlussprojekt.Klassen.Statische_Methoden;
using static Abschlussprojekt.Klassen.Statische_Variablen;
using static Abschlussprojekt.Klassen.Spieler;
using Abschlussprojekt.Klassen;

// Namenskonvention: --------------------------------------+
//                                                         |
// Alle Wörter eines Namens werden mit einem "_" getrennt. |
// Klassen     = Klasse_Bsp    => erster Buchstabe groß    |
// Methoden    = Methode_Bsp   => erster Buchstabe groß    |
// Variable    = variable_Bsp  => erster Buchstabe klein   |
// ENUM        = ENUM_BSP      => alle Buchstaben groß     |
//---------------------------------------------------------+

namespace Abschlussprojekt.Seiten
{
    /// <summary>
    /// Interaktionslogik für Spielwiese.xaml
    /// </summary>
    // Alle informationen außer den Chatinformationen werden immer an alle SPieler gesendet
    public partial class Spielwiese : UserControl
    {
        TextBox active_chat;
        Spieler aktiver_chat_spieler;

        //Spieler lokaler_spieler;
        Spieler nächster_Spieler;
        bool TCP_listener_status;
        Frame root_Frame;
        public Spielwiese(Frame root_Frame)
        {
            
            InitializeComponent();
            aktive_Seite = AKTIVE_SEITE.SPIELWIESE;
            this.root_Frame = root_Frame;
            lokaler_spieler = Ermittele_lokalen_Spieler();
            this.nächster_Spieler = Ermittele_nächsten_Spieler();
            this.TCP_listener_status = true;
            active_chat = Chat_rot;
            
            Initialisiere_Images_für_Figuren(); // Hier werden die Bilder für die Figuren geladen.
            Initialisiere_alle_Felder(Grid_Spielwiese);// Hier werden alle Felder anhand der UIElement Control elemente erzeugt.
            Initialisiere_Spiel(); // Hier werden die Spielfiguren der Spieler erzeugt.

            //
            //Hier wird jede figur der oberfläche hinzugefügt
            //
            foreach (Figur figur in spieler_rot)
            {
                Grid_Spielwiese.Children.Add(figur.bild);
            }
            foreach (Figur figur in spieler_gelb)
            {
                Grid_Spielwiese.Children.Add(figur.bild);
            }
            foreach (Figur figur in spieler_gruen)
            {
                Grid_Spielwiese.Children.Add(figur.bild);
            }
            foreach (Figur figur in spieler_blau)
            {
                Grid_Spielwiese.Children.Add(figur.bild);
            }
            foreach (Spieler spieler in alle_Spieler)
            {
                switch (spieler.farbe)
                {
                    case Klassen.Statische_Variablen.FARBE.ROT: L_Spielername_rot.Content = spieler.name; break;
                    case Klassen.Statische_Variablen.FARBE.GELB: L_Spielername_gelb.Content = spieler.name; break;
                    case Klassen.Statische_Variablen.FARBE.GRUEN: L_Spielername_gruen.Content = spieler.name; break;
                    case Klassen.Statische_Variablen.FARBE.BLAU: L_Spielername_blau.Content = spieler.name; break;
                }
            }
            // ToDo: Foreach CHildren in grid -> finde die Images heraus und füge event hinzu

            Task TCPListener = Task.Factory.StartNew(Listen_for_TCP_Pakete);

            Erzeuge_zufälligen_Afnfänger();
            spieler_gelb[0].Set_Figureposition(spiel_felder[10]);
        }

        private void Erzeuge_zufälligen_Afnfänger()
        {
            int z = zufallszahl.Next(1, alle_Spieler.Count);//  D = [1,4[
            Netzwerkkommunikation.Send_TCP_Packet("Spielrecht", alle_Spieler[z - 1].ip);
        }

        private Spieler Ermittele_nächsten_Spieler()
        {
            switch (lokaler_spieler.farbe)
            {
                case FARBE.ROT:
                    {
                        foreach(Spieler spieler in alle_Spieler)
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

        private void Listen_for_TCP_Pakete()
        {
            while (TCP_listener_status)
            {
                Netzwerkkommunikation.Start_TCP_Listener();
            }
        }

        private Spieler Ermittele_lokalen_Spieler()
        {
            foreach(Spieler spieler in alle_Spieler)
            {
                if (spieler.ip.Address == eigene_IPAddresse.Address) return spieler;
            }
            return null;
        }

        private void Btn_senden_Click(object sender, RoutedEventArgs e)
        {
            if (Chat_eingabe.Text != "Schreibe eine Nachricht") Text_in_Chat_senden(lokaler_spieler.name);
        }

        private void Chat_eingabe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Text_in_Chat_senden(lokaler_spieler.name);
        }

        public void Text_in_Chat_senden(string spielername)
        {
            active_chat.Text += "\n" + spielername + ": " + Chat_eingabe.Text;
            if (aktiver_chat_spieler != null) Netzwerkkommunikation.Send_TCP_Packet(Chat_eingabe.Text, aktiver_chat_spieler.ip);
            else Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler(Chat_eingabe.Text);
            active_chat.ScrollToEnd();
            Chat_eingabe.Text = "";
            Chat_eingabe.Focus();
        }

        private void Spieler_rot_GotFocus(object sender, RoutedEventArgs e)
        {
            active_chat = Chat_rot;
            aktiver_chat_spieler = Finde_Spieler_nach_Farbe(FARBE.ROT);
        }

        private void Chat_eingabe_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Chat_eingabe.Text == "Schreibe eine Nachricht") Chat_eingabe.Text = "";
        }

        private void Chat_eingabe_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Chat_eingabe.Text == "") Chat_eingabe.Text = "Schreibe eine Nachricht";
        }

        public void Add_to_control(Image bild)
        {
            this.AddChild(bild);
        }

        private void Spieler_gelb_GotFocus(object sender, RoutedEventArgs e)
        {
            active_chat = Chat_gelb;
            aktiver_chat_spieler = Finde_Spieler_nach_Farbe(FARBE.GELB);
        }
        
        private void Spieler_gruen_GotFocus(object sender, RoutedEventArgs e)
        {
            active_chat = Chat_gruen;
            aktiver_chat_spieler = Finde_Spieler_nach_Farbe(FARBE.GRUEN);
        }

        private void Spieler_blau_GotFocus(object sender, RoutedEventArgs e)
        {
            active_chat = Chat_blau;
            aktiver_chat_spieler = Finde_Spieler_nach_Farbe(FARBE.BLAU);
        }

        private void Gruppenchat_GotFocus(object sender, RoutedEventArgs e)
        {
            active_chat = Chat_gruppe;
            aktiver_chat_spieler = null;
        }
        private int i = 0;
        private void Btn_Wuerfel_Click(object sender, RoutedEventArgs e)
        {
            i += 1;
            spieler_rot[0].Set_Figureposition(spiel_felder[i]);

            Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Spielfigur Update," + Statische_Methoden.Konvertiere_FARBE_zu_string(lokaler_spieler.farbe) + "," + spieler_rot[0].id.ToString() + "," + spiel_felder[i].position.X.ToString() + "," + spiel_felder[i].position.Y.ToString());

        }

        private void btn_Aufgeben_Click(object sender, RoutedEventArgs e)
        {
            this.TCP_listener_status = false;
            root_Frame.Content = new Startseite(root_Frame);
        }

        private void Forward_Spielrecht()
        {
            Netzwerkkommunikation.Send_TCP_Packet("Spielrecht", nächster_Spieler.ip);
        }

        private void TB_aktiver_Spieler_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (TB_aktiver_Spieler.Text.ToString().Contains(lokaler_spieler.name))
            //{

            //}
        }

        
    }
}
