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
using static Abschlussprojekt.Klassen.Spieler;

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
            this.lokaler_spieler = Klassen.globale_temporäre_Variablen.lokaler_spieler;
            InitializeComponent();
            Initialisiere_Statische_Variablen();
            this.active_chat = new TextBox();
            Initialisiere_Spiel("","","","");
            // ToDo: Foreach CHildren in grid -> finde die Images heraus und füge event hinzu
        }

        private void Btn_senden_Click(object sender, RoutedEventArgs e)
        {
            Text_in_Chat_senden(lokaler_spieler.name);
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
    }
}
