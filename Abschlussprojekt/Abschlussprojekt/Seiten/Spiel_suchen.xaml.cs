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
using Abschlussprojekt.Klassen;
using Abschlussprojekt.Klassen.Seiten_Funktionen;

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
    /// Interaktionslogik für Spiel_suchen.xaml
    /// </summary>
    public partial class Spiel_suchen : UserControl
    {
        Frame root_Frame;
        Host ausgewählten_Host = null;
        bool UDP_thread_status = true;
        string ausgewählte_Farbe = "";

        public delegate void Hosts_Update();
        
        public Spiel_suchen(Frame root_Frame)
        {
            this.root_Frame = root_Frame;
            InitializeComponent();
            Statische_Variablen.hosts = Hosts;
            Task.Factory.StartNew( invoker);
            
            btn_Abbrechen.IsEnabled = false;
            Task UDP_Listener = Task.Factory.StartNew(Start_UDP_Listener);
            Statische_Variablen.aktive_Seite = Statische_Variablen.AKTIVE_SEITE.SPIEL_SUCHEN;
            Statische_Variablen.Spiel_suchen_Grid = Spiel_suchen_grid;
        }

        private void btn_beitreten_Click(object sender, RoutedEventArgs e)
        {
            if (Spieler_name.Text == "" || Spieler_name.Text == "Gib einen Spielernamen ein" || Spieler_name.Text.Length > 20)
            {
                MessageBox.Show("Gegen sie einen gültigen Namen ein!", "Fehler", MessageBoxButton.OK);
                Spieler_name.Focus();
                return;
            }

            if (Spieler_name.Text.Contains(","))
            {
                MessageBox.Show("Der Spielername darf kein \",\"enthalten", "Fehler", MessageBoxButton.OK);
                Spieler_name.Focus();
                return;
            }
            if (ausgewählten_Host.freie_plätze > 0)
            {
                string message = "Clientanfrage," + Spieler_name.Text + "," + Statische_Variablen.eigene_IPAddresse.ToString() + "," + ausgewählte_Farbe;
                Netzwerkkommunikation.Send_TCP_Packet(message, ausgewählten_Host.host_ip);
                //Warten auf zusage
                Netzwerkkommunikation.Start_TCP_Listener();
                if (Statische_Variablen.anfragen_result == true)
                {
                    Task tcp_listener = Task.Factory.StartNew(Sart_TCP_Listener);
                    btn_beitreten.IsEnabled = false;
                    Spieler_name.IsEnabled = false;
                    RB_blau.IsEnabled = false;
                    RB_gelb.IsEnabled = false;
                    RB_gruen.IsEnabled = false;
                    RB_blau.IsEnabled = false;
                    btn_Abbrechen.IsEnabled = true;
                    Hosts.IsEnabled = false;
                }
            }
        }

        private void btn_aktualisieren_Click(object sender, RoutedEventArgs e)
        {
            Statische_Variablen.alle_Hosts.Clear();
            Statische_Variablen.known_IP_S.Clear();
            //Hosts.Dispatcher.Invoke(new Hosts_Update(Updater));
        }

        private void Hosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Hosts.SelectedIndex >= 0)
            {
                foreach (Host host in Statische_Variablen.alle_Hosts)
                {
                    if (Hosts.Items[Hosts.SelectedIndex].ToString().Contains(host.hostname))
                    {
                        L_Name_Spieler_rot.Text = host.Spieler_rot;
                        L_Name_Spieler_gelb.Text = host.Spieler_gelb;
                        L_Name_Spieler_gruen.Text = host.Spieler_gruen;
                        L_Name_Spieler_blau.Text = host.Spieler_blau;
                        ausgewählten_Host = host;
                    }
                }
            }
        }

        private void RB_rot_Checked(object sender, RoutedEventArgs e)
        {
            ausgewählte_Farbe = "rot";
        }

        private void RB_gelb_Checked(object sender, RoutedEventArgs e)
        {
            ausgewählte_Farbe = "gelb";
        }

        private void RB_gruen_Checked(object sender, RoutedEventArgs e)
        {
            ausgewählte_Farbe = "gruen";
        }

        private void RB_blau_Checked(object sender, RoutedEventArgs e)
        {
            ausgewählte_Farbe = "blau";
        }

        private void L_Name_Spieler_rot_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_Name_Spieler_rot.Text != "Offen" && RB_rot != null) RB_rot.IsEnabled = false;
            else if (L_Name_Spieler_rot.Text == "Offen" && RB_rot != null && btn_beitreten.IsEnabled) RB_rot.IsEnabled = true;
        }

        private void L_Name_Spieler_gelb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_Name_Spieler_gelb.Text != "Offen" && RB_gelb != null) RB_gelb.IsEnabled = false;
            else if (L_Name_Spieler_gelb.Text == "Offen" && RB_gelb != null && btn_beitreten.IsEnabled) RB_gelb.IsEnabled = true;
        }

        private void L_Name_Spieler_gruen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_Name_Spieler_gruen.Text != "Offen" && RB_gruen != null) RB_gruen.IsEnabled = false;
            else if (L_Name_Spieler_gruen.Text == "Offen" && RB_gruen != null && btn_beitreten.IsEnabled) RB_gruen.IsEnabled = true;
        }

        private void L_Name_Spieler_blau_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_Name_Spieler_blau.Text != "Offen" && RB_blau != null) RB_blau.IsEnabled = false;
            else if (L_Name_Spieler_blau.Text == "Offen" && RB_blau != null && btn_beitreten.IsEnabled) RB_blau.IsEnabled = true;
        }

        private void Spieler_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Spieler_name.Text == "") Spieler_name.Text = "Gib einen Spielernamen ein";
        }

        private void Spieler_name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Spieler_name.Text == "Gib einen Spielernamen ein") Spieler_name.Text = "";
        }

        private void btn_Abbrechen_Click(object sender, RoutedEventArgs e)
        {
            if (ausgewählten_Host != null) Netzwerkkommunikation.Send_TCP_Packet("Clientabsage," + Statische_Variablen.eigene_IPAddresse.ToString(), ausgewählten_Host.host_ip);
            btn_Abbrechen.IsEnabled = false;
            btn_beitreten.IsEnabled = true;
            Spieler_name.IsEnabled = true;
            RB_blau.IsEnabled = true;
            RB_gelb.IsEnabled = true;
            RB_gruen.IsEnabled = true;
            RB_blau.IsEnabled = true;
            Hosts.IsEnabled = true;
        }

        private void invoker()
        {
            while (UDP_thread_status)
            {
                Hosts.Dispatcher.Invoke(new Hosts_Update(Updater));
                System.Threading.Thread.Sleep(500);
            }
        }

        private void Start_UDP_Listener()
        {
            while (UDP_thread_status)
            {
                Netzwerkkommunikation.Start_UDP_Listener();
            }
        }

        private void Sart_TCP_Listener()
        {
            while (UDP_thread_status)
            {
                Netzwerkkommunikation.Start_TCP_Listener();
            }
        }

        public void Updater()// Macht nix weiter als die Liste mit Hosts zu aktualisieren.
        {
            Hosts.Items.Clear();
            foreach (Host host in Statische_Variablen.alle_Hosts)
            {
                Hosts.Items.Add(host.hostname + " --- Freie plätze:" + host.freie_plätze.ToString());
            }
            if (L_Name_Spieler_blau != null && ausgewählten_Host != null) L_Name_Spieler_blau.Text = ausgewählten_Host.Spieler_blau;
            if (L_Name_Spieler_blau != null && ausgewählten_Host != null) L_Name_Spieler_gelb.Text = ausgewählten_Host.Spieler_gelb;
        }

        private void btn_zurueck_Click(object sender, RoutedEventArgs e)
        {
            if (btn_Abbrechen.IsEnabled) btn_Abbrechen.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            UDP_thread_status = false;
            Statische_Variablen.alle_Hosts.Clear();
            Statische_Variablen.known_IP_S.Clear();
            root_Frame.Content = new Startseite(root_Frame);
        }

        private void Spiel_suchen_grid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Statische_Variablen.alle_Spieler.Count > 0)
            {
                UDP_thread_status = false;
                root_Frame.Content = new Spielwiese(root_Frame);
            }
        }
    }
}
