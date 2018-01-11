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

namespace Mensch_ärgere_dich_nicht.Seiten
{
    /// <summary>
    /// Interaktionslogik für Spielfeld.xaml
    /// </summary>
    public partial class Spielfeld : Page
    {
        public Spielfeld()
        {
            InitializeComponent();
            Statische_Variablen.aktuelle_Seite = "Spielfeld";
            Statische_Variablen.mainWindow.WindowState = WindowState.Maximized;
            Klassen.SeitenFunktionen.Spielfeld.spielfeld = G_spielfeld;
            Klassen.SeitenFunktionen.Spielfeld.BTN_Würfel = BTN_Würfel;
            Klassen.SeitenFunktionen.Spielfeld.Erstelle_Oberfläche();

            Klassen.SeitenFunktionen.Spielfeld.alle_Zahlen.Add(Bild_W1);
            Klassen.SeitenFunktionen.Spielfeld.alle_Zahlen.Add(Bild_W2);
            Klassen.SeitenFunktionen.Spielfeld.alle_Zahlen.Add(Bild_W3);
            Klassen.SeitenFunktionen.Spielfeld.alle_Zahlen.Add(Bild_W4);
            Klassen.SeitenFunktionen.Spielfeld.alle_Zahlen.Add(Bild_W5);
            Klassen.SeitenFunktionen.Spielfeld.alle_Zahlen.Add(Bild_W6);
            Klassen.SeitenFunktionen.Spielfeld.Spieler_namen_Label.Insert(0, L_Spieler_Rot);
            Klassen.SeitenFunktionen.Spielfeld.Spieler_namen_Label.Insert(1, L_Spieler_Gelb);
            Klassen.SeitenFunktionen.Spielfeld.Spieler_namen_Label.Insert(2, L_Spieler_Grün);
            Klassen.SeitenFunktionen.Spielfeld.Spieler_namen_Label.Insert(3, L_Spieler_Blau);
            foreach (Klassen.Spieler spieler in Klassen.SeitenFunktionen.Spielfeld.alle_Mitspieler)
            {
                switch (spieler.farbe)
                {
                    case Statische_Variablen.FARBE.ROT: L_Spieler_Rot.Content = spieler.name; break;
                    case Statische_Variablen.FARBE.GELB: L_Spieler_Gelb.Content = spieler.name; break;
                    case Statische_Variablen.FARBE.GRÜN: L_Spieler_Grün.Content = spieler.name; break;
                    case Statische_Variablen.FARBE.BLAU: L_Spieler_Blau.Content = spieler.name; break;
                }
            }

            Task.Factory.StartNew(Klassen.SeitenFunktionen.Spielfeld.TCP_Listener);
        }

        private void BTN_Beenden_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wollen sie wirklich aufgeben?", "Achtung!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Statische_Variablen.aktuelle_Seite = "Menü";
                //Statische_Variablen.mainWindow.Content = new Seiten.Menü();
                Klassen.SeitenFunktionen.Spielfeld.alle_Mitspieler.Clear();
                Klassen.SeitenFunktionen.Spielfeld.alle_Figuren.Clear();
                Klassen.SeitenFunktionen.Spielfeld.alle_Felder.Clear();
                Klassen.SeitenFunktionen.Spielfeld.alle_Zahlen.Clear();
                Klassen.SeitenFunktionen.Spielfeld.spielstatus = false;
                Statische_Variablen.rootFrame.GoBack();
                Statische_Variablen.rootFrame.GoBack();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
        private void BTN_Würfel_Click(object sender, RoutedEventArgs e)
        {
            BTN_Würfel.IsEnabled = false;
            
            Klassen.SeitenFunktionen.Spielfeld.Würfeln();
        }


        //Chatfunktionen -------------------------------------------------------------------------
        private void send_msg_Click(object sender, RoutedEventArgs e)
        {
            if(Msg.Text.StartsWith("/p"))
            {
                Msg.Foreground = Brushes.Blue;
                sende_nachricht_party(Statische_Variablen.lokaler_Spieler + Msg.Text.Remove(0,2));
            }
            else if(Msg.Text.StartsWith("/w"))
            {
                Msg.Foreground = Brushes.LightPink;
                sende_whisper(Statische_Variablen.lokaler_Spieler + Msg.Text.Remove(0, 2));
            }
        }

        private void sende_nachricht_party(string lokaler_spieler)
        {
            Klassen.Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler(Msg.Text);
        }        

        private void sende_whisper(string spielername)
        {
            foreach(Klassen.Spieler spieler in Klassen.SeitenFunktionen.Spielfeld.alle_Mitspieler)
            {
                if (Msg.Text.Contains(spieler.name))
                {
                    Klassen.Netzwerkkommunikation.Send_TCP_Packet(Msg.Text, spieler.ip);
                }
            }
        }
    }
}
