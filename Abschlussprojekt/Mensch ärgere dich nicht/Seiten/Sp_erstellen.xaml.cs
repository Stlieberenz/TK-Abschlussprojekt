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
            L_Titel.Content = Statische_Variablen.lokaler_Spieler + "'s Spiel erstellen";
            Statische_Variablen.aktuelle_Seite = "Spiel_erstellen";
        }

        private void BTN_Starten_Click(object sender, RoutedEventArgs e)
        {
            if (Klassen.SeitenFunktionen.S_erstellen.Prüfe_auswahl())
            {
                BTN_Starten.IsEnabled = false;
                CB_Blau.IsEnabled = false;
                CB_Gelb.IsEnabled = false;
                CB_Rot.IsEnabled = false;
                CB_Grün.IsEnabled = false;
                Klassen.SeitenFunktionen.S_erstellen.UDP_Threadstatus = true;
                Task.Factory.StartNew(Klassen.SeitenFunktionen.S_erstellen.Warte_auf_Spieler);
                Task.Factory.StartNew(Klassen.SeitenFunktionen.S_erstellen.Sende_UDP);
            }
            else
            {
                MessageBox.Show("Es müssen mindestens 2 Spieler gegeneinander antreten", "Fehler - ungültige Auswahl", MessageBoxButton.OK);
            }
        }

        private void BTN_Zurück_Click(object sender, RoutedEventArgs e)
        {
            Klassen.SeitenFunktionen.S_erstellen.UDP_Threadstatus = false;
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
            Update_Index_variablen();
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
            Klassen.SeitenFunktionen.S_erstellen.Spieler_Rot = L_Rot.Text;
        }

        private void L_Gelb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Klassen.SeitenFunktionen.S_erstellen.Spieler_Gelb = L_Gelb.Text;
        }

        private void L_Grün_TextChanged(object sender, TextChangedEventArgs e)
        {
            Klassen.SeitenFunktionen.S_erstellen.Spieler_Grün = L_Grün.Text;
        }

        private void L_Blau_TextChanged(object sender, TextChangedEventArgs e)
        {
            Klassen.SeitenFunktionen.S_erstellen.Spieler_Blau = L_Blau.Text;
        }


    }
}
