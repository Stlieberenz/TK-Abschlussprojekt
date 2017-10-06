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
    public partial class Spielwiese : UserControl
    {
        TextBox active_chat;
        Klassen.Spieler lokaler_spieler;
        public Spielwiese()
        {
           
            InitializeComponent();
            Initialisiere_Images_für_Figuren();
            this.lokaler_spieler = Klassen.globale_temporäre_Variablen.lokaler_spieler;
            Initialisiere_alle_Felder(Grid_Spielwiese);
            this.active_chat = new TextBox();
            Initialisiere_Spiel("","","","");
            foreach(Klassen.Figur figur in spieler_rot)
            {
                Grid_Spielwiese.Children.Add(figur.bild);
            }

            switch (lokaler_spieler.farbe)
            {
                case Klassen.Statische_Variablen.FARBE.ROT: L_Spielername_rot.Content = lokaler_spieler.name; break;
                case Klassen.Statische_Variablen.FARBE.GELB: L_Spielername_gelb.Content = lokaler_spieler.name; break;
                case Klassen.Statische_Variablen.FARBE.GRUEN: L_Spielername_gruen.Content = lokaler_spieler.name; break;
                case Klassen.Statische_Variablen.FARBE.BLAU: L_Spielername_blau.Content = lokaler_spieler.name; break;
            }
            // ToDo: Foreach CHildren in grid -> finde die Images heraus und füge event hinzu
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
            active_chat.ScrollToEnd();
            Chat_eingabe.Text = "";
            Chat_eingabe.Focus();
        }

        private void Spieler_rot_GotFocus(object sender, RoutedEventArgs e)
        {
            active_chat = Chat_rot;
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
    }
}
