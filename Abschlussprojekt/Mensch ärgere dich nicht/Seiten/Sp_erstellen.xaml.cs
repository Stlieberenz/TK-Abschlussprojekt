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
    /// Interaktionslogik für Sp_erstellen.xaml
    /// </summary>
    public partial class Sp_erstellen : Page
    {
        public  delegate void Spiel_starter();
        public Sp_erstellen()
        {
            InitializeComponent();
            L_Tietel.Content = Statische_Variablen.lokaler_Spieler + "'s Spiel erstellen";
            Statische_Variablen.aktuelle_Seite = "Spiel_erstellen";
            Klassen.SeitenFunktionen.S_erstellen.versteckter_Button = BTN_Versteckter_Button;
        }

        private void BTN_Starten_Click(object sender, RoutedEventArgs e)
        {
            // Sobald man Starten drückt wird die getroffene Auswahl Überprüft. Bei einer gültigen Auswahl werden zwei neue
            // Threads gestartet. Der eine Sendet eine UDP-Nachricht für potentielle Spieler(Clients) und der Andere horcht auf die
            // Leitung um Client-Anfragen zu verarbeiten. Dabei ist der Button disabled. Wenn das Spiel gestartet wird, wird der 
            // Button erneut getriggert um das Spiel zu starten.
            if (BTN_Starten.IsEnabled)
            {
                if (Klassen.SeitenFunktionen.S_erstellen.Prüfe_auswahl())
                {
                    BTN_Starten.IsEnabled = false;
                    CB_Blau.IsEnabled = false;
                    CB_Gelb.IsEnabled = false;
                    CB_Rot.IsEnabled = false;
                    CB_Grün.IsEnabled = false;
                    Klassen.SeitenFunktionen.S_erstellen.Spieler_Rot = CB_Rot.Text;
                    Klassen.SeitenFunktionen.S_erstellen.Spieler_Gelb = CB_Gelb.Text;
                    Klassen.SeitenFunktionen.S_erstellen.Spieler_Grün = CB_Grün.Text;
                    Klassen.SeitenFunktionen.S_erstellen.Spieler_Blau = CB_Blau.Text;
                    Klassen.SeitenFunktionen.S_erstellen.UDP_Threadstatus = true;
                    Task.Factory.StartNew(Klassen.SeitenFunktionen.S_erstellen.Warte_auf_Spieler);
                    Task.Factory.StartNew(Klassen.SeitenFunktionen.S_erstellen.Sende_UDP);
                }
                else
                {
                    MessageBox.Show("Es müssen mindestens 2 Spieler gegeneinander antreten", "Fehler - ungültige Auswahl", MessageBoxButton.OK);
                }
            }
            else
            {
                string startnachricht = Klassen.SeitenFunktionen.S_erstellen.Generiere_Startnachricht();
                Klassen.Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler(startnachricht);
                Statische_Variablen.aktuelle_Seite = "Spielfeld";
                Statische_Variablen.rootFrame.Content = new Seiten.Spielfeld();
            }
        }
        
        private void BTN_Zurück_Click(object sender, RoutedEventArgs e)
        {
            Klassen.SeitenFunktionen.S_erstellen.UDP_Threadstatus = false;
            Statische_Variablen.aktuelle_Seite = "Menü";
            Statische_Variablen.rootFrame.Content = new Seiten.Menü();
        }

        private void BTN_Abbrechen_Click(object sender, RoutedEventArgs e)
        {
            Klassen.SeitenFunktionen.S_erstellen.UDP_Threadstatus = false;
            BTN_Starten.IsEnabled = true;
            CB_Blau.IsEnabled = true;
            CB_Gelb.IsEnabled = true;
            CB_Rot.IsEnabled = true;
            CB_Grün.IsEnabled = true;
        }

        private void CB_Rot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool result = Klassen.SeitenFunktionen.S_erstellen.Prüfe_auswahl(new List<int>(){CB_Rot.SelectedIndex, CB_Gelb.SelectedIndex, CB_Grün.SelectedIndex, CB_Blau.SelectedIndex},CB_Rot);
            if (result == false)
            {
                if (CB_Gelb.SelectedIndex == 3) CB_Gelb.SelectedIndex = 1;
                if (CB_Grün.SelectedIndex == 3) CB_Grün.SelectedIndex = 1;
                if (CB_Blau.SelectedIndex == 3) CB_Blau.SelectedIndex = 1;
            }
            Update_Index_variablen();
        }

        private void CB_Gelb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool result = Klassen.SeitenFunktionen.S_erstellen.Prüfe_auswahl(new List<int>() { CB_Rot.SelectedIndex, CB_Gelb.SelectedIndex, CB_Grün.SelectedIndex, CB_Blau.SelectedIndex },CB_Gelb);
            if (result == false)
            {
                if (CB_Rot.SelectedIndex == 3) CB_Rot.SelectedIndex = 1;
                if (CB_Grün.SelectedIndex == 3) CB_Grün.SelectedIndex = 1;
                if (CB_Blau.SelectedIndex == 3) CB_Blau.SelectedIndex = 1;
            }

            Update_Index_variablen();
        }

        private void CB_Grün_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool result = Klassen.SeitenFunktionen.S_erstellen.Prüfe_auswahl(new List<int>() { CB_Rot.SelectedIndex, CB_Gelb.SelectedIndex, CB_Grün.SelectedIndex, CB_Blau.SelectedIndex },CB_Grün);
            if (result == false)
            {
                if (CB_Gelb.SelectedIndex == 3) CB_Gelb.SelectedIndex = 1;
                if (CB_Rot.SelectedIndex == 3) CB_Rot.SelectedIndex = 1;
                if (CB_Blau.SelectedIndex == 3) CB_Blau.SelectedIndex = 1;
            }

            Update_Index_variablen();
        }

        private void CB_Blau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool result = Klassen.SeitenFunktionen.S_erstellen.Prüfe_auswahl(new List<int>() { CB_Rot.SelectedIndex, CB_Gelb.SelectedIndex, CB_Grün.SelectedIndex, CB_Blau.SelectedIndex },CB_Blau);
            if (result == false)
            {
                if (CB_Gelb.SelectedIndex == 3) CB_Gelb.SelectedIndex = 1;
                if (CB_Grün.SelectedIndex == 3) CB_Grün.SelectedIndex = 1;
                if (CB_Rot.SelectedIndex == 3) CB_Rot.SelectedIndex = 1;
            }

            Update_Index_variablen();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CB_Rot.SelectedIndex = 3;
            CB_Gelb.SelectedIndex = 2;
            CB_Grün.SelectedIndex = 1;
            CB_Blau.SelectedIndex = 1;
            L_Rot.Text = CB_Rot.Text;
            L_Gelb.Text = CB_Gelb.Text;
            L_Grün.Text = CB_Grün.Text;
            L_Blau.Text = CB_Blau.Text;
            Update_Index_variablen();
            Klassen.SeitenFunktionen.S_erstellen.Spieler_Rot = CB_Rot.Text;
            Klassen.SeitenFunktionen.S_erstellen.Spieler_Gelb = CB_Gelb.Text;
            Klassen.SeitenFunktionen.S_erstellen.Spieler_Grün = CB_Grün.Text;
            Klassen.SeitenFunktionen.S_erstellen.Spieler_Blau = CB_Blau.Text;
            Klassen.SeitenFunktionen.S_erstellen.Start_Button = BTN_Starten;
        }

        private void Update_Index_variablen()
        {
            Klassen.SeitenFunktionen.S_erstellen.index_rot_alt = CB_Rot.SelectedIndex;
            Klassen.SeitenFunktionen.S_erstellen.index_gelb_alt = CB_Gelb.SelectedIndex;
            Klassen.SeitenFunktionen.S_erstellen.index_grün_alt = CB_Grün.SelectedIndex;
            Klassen.SeitenFunktionen.S_erstellen.index_blau_alt = CB_Blau.SelectedIndex;
        }

        private void L_Rot_TextChanged(object sender, TextChangedEventArgs e)
        {
            L_Rot.Text = Klassen.SeitenFunktionen.S_erstellen.Spieler_Rot;
        }

        private void L_Gelb_TextChanged(object sender, TextChangedEventArgs e)
        {
            L_Gelb.Text = Klassen.SeitenFunktionen.S_erstellen.Spieler_Gelb;
        }

        private void L_Grün_TextChanged(object sender, TextChangedEventArgs e)
        {
            L_Grün.Text = Klassen.SeitenFunktionen.S_erstellen.Spieler_Grün;
        }

        private void L_Blau_TextChanged(object sender, TextChangedEventArgs e)
        {
            L_Blau.Text = Klassen.SeitenFunktionen.S_erstellen.Spieler_Blau;
        }

        private void BTN_Versteckter_Button_Click(object sender, RoutedEventArgs e)
        {
            L_Rot.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent));
            L_Gelb.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent));
            L_Grün.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent));
            L_Blau.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
