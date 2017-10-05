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
    /// Interaktionslogik für Spiel_erstellen.xaml
    /// </summary>
    public partial class Spiel_erstellen : UserControl
    {
        Frame root_Frame;
        Klassen.Statische_Variablen.FARBE ausgewählte_farbe;
        public Spiel_erstellen(Frame root_Frame)
        {
            this.ausgewählte_farbe = Klassen.Statische_Variablen.FARBE.LEER;
            this.root_Frame = root_Frame;
            InitializeComponent();
            this.comboBox_rot.SelectedIndex = 1;
            this.comboBox_gelb.SelectedIndex = 2;
        }

        private void btn_spiel_starten_Click(object sender, RoutedEventArgs e)
        {
            //
            //In den If Abfragen wird geprüft ob min 2 gültige spieler da sind und kein slot mehr offen ist und ein Name angegeben wurde.
            //
            int temp = 0;
            bool result = true;
            if(L_Name_Spieler_rot.Content.ToString() == "Computergegner" || L_Name_Spieler_rot.Content.ToString() == "Ich" )
            {
                temp++;
            }
            else if( L_Name_Spieler_rot.Content.ToString() == "Offen")
            {
                MessageBox.Show("Der Slot darf nicht offen sein. \nEs muss ein Spieler auf den Freien Slot kommen,\noder der Slot muss geschlossen werden", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (L_Name_Spieler_gelb.Content.ToString() == "Computergegner" || L_Name_Spieler_gelb.Content.ToString() == "Ich" )
            {
                temp++;
            }
            else if (L_Name_Spieler_gelb.Content.ToString() == "Offen")
            {
                MessageBox.Show("Der Slot darf nicht offen sein. \nEs muss ein Spieler auf den Freien Slot kommen,\noder der Slot muss geschlossen werden", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (L_Name_Spieler_gruen.Content.ToString() == "Computergegner" || L_Name_Spieler_gruen.Content.ToString() == "Ich" )
            {
                temp++;
            }
            else if (L_Name_Spieler_gruen.Content.ToString().ToString() == "Offen")
            {
                MessageBox.Show("Der Slot darf nicht offen sein. \nEs muss ein Spieler auf den Freien Slot kommen,\noder der Slot muss geschlossen werden", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (L_Name_Spieler_blau.Content.ToString().ToString() == "Computergegner" || L_Name_Spieler_blau.Content.ToString() == "Ich" )
            {
                temp++;
            }
            else if (L_Name_Spieler_blau.Content.ToString() == "Offen")
            {
                MessageBox.Show("Der Slot darf nicht offen sein. \nEs muss ein Spieler auf den Freien Slot kommen,\noder der Slot muss geschlossen werden", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (temp < 2)
            {
                MessageBox.Show("Es müssen mindestens 2 Spieler gegeneinander antreten", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (Spielername_eingabe.Text == "" || Spielername_eingabe.Text == "Hier Namen eingeben" || Spielername_eingabe.Text.Length >=20)
            {
                MessageBox.Show("Es muss ein gültiger Name eingegeben werden!", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (temp >=2 && result == true)
            {
                Klassen.globale_temporäre_Variablen.lokaler_spieler = Erstelle_lokalen_Spieler();
                root_Frame.Content = new Spielwiese();
            }
        }

        private Klassen.Spieler Erstelle_lokalen_Spieler()
        {
            Klassen.Spieler lokaler_Spieler = new Klassen.Spieler(ausgewählte_farbe,Spielername_eingabe.Text);
            return lokaler_Spieler;
        }

        private void comboBox_rot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_rot.SelectedIndex == 1 )
            {
                if (comboBox_gelb.SelectedIndex == 1) comboBox_gelb.SelectedIndex = 0;
                if (comboBox_gruen.SelectedIndex == 1) comboBox_gruen.SelectedIndex = 0;
                if (comboBox_blau.SelectedIndex == 1) comboBox_blau.SelectedIndex = 0;
                this.ausgewählte_farbe = Klassen.Statische_Variablen.FARBE.ROT;
            } // Diese IF abfrage sorgt dafür, das nur ein Slot mit Ich ausgefüllt werden kann. Zusätzlich wird die Farbe ausgewählt.
        }

        private void comboBox_gelb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_gelb.SelectedIndex == 1)
            {
                if (comboBox_rot.SelectedIndex == 1) comboBox_rot.SelectedIndex = 0;
                if (comboBox_gruen.SelectedIndex == 1) comboBox_gruen.SelectedIndex = 0;
                if (comboBox_blau.SelectedIndex == 1) comboBox_blau.SelectedIndex = 0;
                this.ausgewählte_farbe = Klassen.Statische_Variablen.FARBE.GELB;
            }
        }

        private void comboBox_gruen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_gruen.SelectedIndex == 1)
            {
                if (comboBox_gelb.SelectedIndex == 1) comboBox_gelb.SelectedIndex = 0;
                if (comboBox_rot.SelectedIndex == 1) comboBox_rot.SelectedIndex = 0;
                if (comboBox_blau.SelectedIndex == 1) comboBox_blau.SelectedIndex = 0;
                this.ausgewählte_farbe = Klassen.Statische_Variablen.FARBE.GRUEN;
            }
        }

        private void comboBox_blau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_blau.SelectedIndex == 1)
            {
                if (comboBox_gelb.SelectedIndex == 1) comboBox_gelb.SelectedIndex = 0;
                if (comboBox_gruen.SelectedIndex == 1) comboBox_gruen.SelectedIndex = 0;
                if (comboBox_rot.SelectedIndex == 1) comboBox_rot.SelectedIndex = 0;
                this.ausgewählte_farbe = Klassen.Statische_Variablen.FARBE.BLAU;
            }
        }

        private void Spielername_eingabe_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Spielername_eingabe.Text == "") Spielername_eingabe.Text = "Hier Namen eingeben";
        }

        private void Spielername_eingabe_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Spielername_eingabe.Text == "Hier Namen eingeben") Spielername_eingabe.Text = "";
        }
    }
}
