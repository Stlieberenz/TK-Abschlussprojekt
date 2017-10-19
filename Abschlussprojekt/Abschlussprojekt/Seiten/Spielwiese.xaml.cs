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
        
        bool TCP_listener_status;
        Frame root_Frame;
        public Spielwiese(Frame root_Frame)
        {
            verbleibende_würfelversuche = 20;// testcode
            InitializeComponent();
            aktive_Seite = AKTIVE_SEITE.SPIELWIESE;
            this.root_Frame = root_Frame;
            lokaler_spieler = Ermittele_lokalen_Spieler();
            nächster_Spieler = Ermittele_nächsten_Spieler().ip;
            this.TCP_listener_status = true;
            active_chat = Chat_rot;
            switch (lokaler_spieler.farbe)
            {
                case FARBE.ROT: figuren_lokal = spieler_rot;break;
                case FARBE.GELB: figuren_lokal = spieler_gelb; break;
                case FARBE.GRUEN: figuren_lokal = spieler_gruen; break;
                case FARBE.BLAU: figuren_lokal = spieler_blau; break;
            }
            
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
            
            foreach (Spieler spieler in alle_Spieler)
            {
                if (spieler.status)
                {
                    TB_aktiver_Spieler.Text = spieler.name;
                }
            }
            if (lokaler_spieler.status == false)
            {
                Btn_Wuerfel.IsEnabled = false;
            }
            else Btn_Wuerfel.IsEnabled = true;
            Würfel = Btn_Wuerfel;
            Task TCPListener = Task.Factory.StartNew(Listen_for_TCP_Pakete);
            if (lokaler_spieler.status == true) Netzwerkkommunikation.Anlaysiere_IP_Paket("Spielrecht");
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

        
        private void Btn_Wuerfel_Click(object sender, RoutedEventArgs e)
        {
            
            
            z = zufallszahl.Next(1, 7);

            Btn_Wuerfel.Content = z.ToString();
            if (Sind_alle_Figuren_im_Haus() && verbleibende_würfelversuche > 0)
            {
                if (z != 6 && verbleibende_würfelversuche >0)
                {
                    verbleibende_würfelversuche--;

                }
                else
                {
                    Zug_ist_möglich(z);
                    Btn_Wuerfel.IsEnabled = false;
                }
            }
            else if (Sind_alle_Figuren_im_Haus() && verbleibende_würfelversuche < 1)
            {
                Btn_Wuerfel.IsEnabled = false;
                Forward_Spielrecht();
            }
            else
            {
                if (z != 6)
                {
                    if (Zug_ist_möglich(z))
                    {
                        Btn_Wuerfel.IsEnabled = false;
                    }
                    else
                    {
                        Btn_Wuerfel.IsEnabled = false;
                        MessageBox.Show("Es ist kein Zug möglich", "Information", MessageBoxButton.OK);
                        Forward_Spielrecht();
                    }
                }
                else
                {
                    if (Zug_ist_möglich(z))
                    {
                        Btn_Wuerfel.IsEnabled = false;
                    }
                    else
                    {
                        lokaler_spieler.status = true;
                    }
                }

            }
        }

        private void btn_Aufgeben_Click(object sender, RoutedEventArgs e)
        {
            this.TCP_listener_status = false;
            root_Frame.Content = new Startseite(root_Frame);
            spiel_felder.Clear();
            ziel_felder.Clear();
            start_felder.Clear();
            spieler_rot.Clear();
            spieler_gelb.Clear();
            spieler_gruen.Clear();
            spieler_blau.Clear();
            alle_Spieler.Clear();
        }

        

        private void TB_aktiver_Spieler_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (TB_aktiver_Spieler.Text.ToString().Contains(lokaler_spieler.name))
            //{

            //}
        }

        
    }
}
//Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Spielfigur Update," + Statische_Methoden.Konvertiere_FARBE_zu_string(lokaler_spieler.farbe) + "," + spieler_rot[0].id.ToString() + "," + spiel_felder[i].position.X.ToString() + "," + spiel_felder[i].position.Y.ToString());

