﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using static Abschlussprojekt.Klassen.Statische_Variablen;
using  Abschlussprojekt.Klassen;
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
    /// Interaktionslogik für Spiel_erstellen.xaml
    /// </summary>
    public partial class Spiel_erstellen : UserControl
    {
        Frame root_Frame;
        FARBE ausgewählte_farbe;
        bool broadcast_status = true;
        int Freieplätze = 0;
        string broadcast_string;
        public delegate void UpdateTextCallback(string name);

        public Spiel_erstellen(Frame root_Frame)
        {
            this.ausgewählte_farbe = FARBE.LEER;
            this.root_Frame = root_Frame;
            InitializeComponent();
            this.comboBox_rot.SelectedIndex = 1;
            Spielerstellenlabel.Add(L_Name_Spieler_rot);
            Spielerstellenlabel.Add(L_Name_Spieler_gelb);
            Spielerstellenlabel.Add(L_Name_Spieler_gruen);
            Spielerstellenlabel.Add(L_Name_Spieler_blau);
            aktive_Seite = AKTIVE_SEITE.SPIEL_ERSTELLEN; 
            Netzwerkkommunikation.Iinitialisiere_BC_IP_Addressen();
            Netzwerkkommunikation.Iinitialisiere_IP_Addressen();
        }

        private void btn_spiel_starten_Click(object sender, RoutedEventArgs e)
        {
            //
            //In den If Abfragen wird geprüft ob min 2 gültige spieler da sind und kein slot mehr offen ist und ein Name angegeben wurde.
            //
            int temp = 0;
            if (!Überprüfe_eingabe_Name()) return;

            if (Prüfe_Startbedingungen(L_Name_Spieler_rot)) temp++;
            if (Prüfe_Startbedingungen(L_Name_Spieler_gelb)) temp++;
            if (Prüfe_Startbedingungen(L_Name_Spieler_gruen)) temp++;
            if (Prüfe_Startbedingungen(L_Name_Spieler_blau)) temp++;
            
            if (temp < 2)
            {
                MessageBox.Show("Es müssen mindestens 2 Spieler gegeneinander antreten", "Fehler", MessageBoxButton.OK);
                return;
            }

            if (temp >= 2)
            {
                broadcast_status = false;
                if (L_Name_Spieler_rot.Text.ToString() == "Computergegner")
                {
                    Spieler CP_Gegner1 = new Spieler(FARBE.ROT, "Computergegner 1", SPIELER_ART.COMPUTERGEGNER, new System.Net.IPAddress(0));
                }
                if (L_Name_Spieler_gelb.Text.ToString() == "Computergegner")
                {
                    Spieler CP_Gegner1 = new Spieler(FARBE.GELB, "Computergegner 2", SPIELER_ART.COMPUTERGEGNER, new System.Net.IPAddress(0));
                }
                if (L_Name_Spieler_gruen.Text.ToString() == "Computergegner")
                {
                    Spieler CP_Gegner1 = new Spieler(FARBE.GRUEN, "Computergegner 3", SPIELER_ART.COMPUTERGEGNER, new System.Net.IPAddress(0));
                }
                if (L_Name_Spieler_blau.Text.ToString() == "Computergegner")
                {
                    Spieler CP_Gegner1 = new Spieler(FARBE.BLAU, "Computergegner 4", SPIELER_ART.COMPUTERGEGNER, new System.Net.IPAddress(0));
                }

                Spieler host_spieler = new Spieler(ausgewählte_farbe, Spielername_eingabe.Text, SPIELER_ART.NORMALER_SPIELER, eigene_IPAddresse);
                host_spieler.status = true;

                string client_startmessage = Statische_Methoden.Erstelle_Startnachricht_für_clients();
                foreach (Spieler spieler in alle_Spieler)
                {
                    if (spieler.spieler_art == SPIELER_ART.NORMALER_SPIELER) Netzwerkkommunikation.Send_TCP_Packet(client_startmessage, spieler.ip);
                }

                Spielerstellenlabel.Clear();
                root_Frame.Content = new Spielwiese(root_Frame);
            }
        }
       
        private void btn_Hosten_Click(object sender, RoutedEventArgs e)
        {
            if (!Überprüfe_eingabe_Name()) return;
           
            comboBox_rot.IsEnabled = false;
            comboBox_gelb.IsEnabled = false;
            comboBox_gruen.IsEnabled = false;
            comboBox_blau.IsEnabled = false;
            Spielername_eingabe.IsEnabled = false;
            btn_Hosten.IsEnabled = false;

            switch (ausgewählte_farbe)
            {
                case FARBE.ROT: L_Name_Spieler_rot.Text = Spielername_eingabe.Text; break;
                case FARBE.GELB: L_Name_Spieler_gelb.Text = Spielername_eingabe.Text; break;
                case FARBE.GRUEN: L_Name_Spieler_gruen.Text = Spielername_eingabe.Text; break;
                case FARBE.BLAU: L_Name_Spieler_blau.Text = Spielername_eingabe.Text; break;
            }

            Create_BC_message();
            Task send_Broadcast = Task.Factory.StartNew(Send_Broadcast);
            Task TCPListener = Task.Factory.StartNew(Listen_for_TCP_Pakete);
        }

        private void btn_abbrechen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Achtung!", "Wollen sie wirklich abbrechen ?!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                broadcast_status = false;
                Spielerstellenlabel.Clear();
                alle_Spieler.Clear();
                known_IP_S.Clear();
                alle_Hosts.Clear();

                for (int i = 0; i < 10; i++)
                {
                    Netzwerkkommunikation.Send_UDP_BC_Packet("Hostinformationen," + eigene_IPAddresse.ToString() + ",absage,,,,,,");
                }
                root_Frame.Content = new Startseite(root_Frame);
            }
        }

        // Bei den Folgenden 3 Funktionen passiert genau das gleiche wie in dieser
        private void comboBox_rot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_rot.SelectedIndex == 1)
            {
                if (comboBox_gelb.SelectedIndex == 1) comboBox_gelb.SelectedIndex = 0;
                if (comboBox_gruen.SelectedIndex == 1) comboBox_gruen.SelectedIndex = 0;
                if (comboBox_blau.SelectedIndex == 1) comboBox_blau.SelectedIndex = 0;
                this.ausgewählte_farbe = FARBE.ROT;
            } // Diese IF abfrage sorgt dafür, das nur ein Slot mit Ich ausgefüllt werden kann. Zusätzlich wird die Farbe ausgewählt.
            else if(comboBox_gelb != null && comboBox_gruen != null && comboBox_blau != null && comboBox_rot != null)
            {
                if (comboBox_gelb.SelectedIndex != 1 && comboBox_gruen.SelectedIndex != 1 && comboBox_blau.SelectedIndex != 1 && comboBox_rot.SelectedIndex != 1) comboBox_rot.SelectedIndex = 1; // Sorgt dafür das man sich nicht selbst aus dem Spiel ausschließen kann.
            }
            Überprüfe_Comboboxauswahl();
        }
        //     \/       \/       \/       \/       \/       \/

        private void comboBox_gelb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_gelb.SelectedIndex == 1)
            {
                // Diese Anweisungen sorgen dafür das man sich nicht auf 2 Plätze gleichzeitig eintragen kann.
                if (comboBox_rot.SelectedIndex == 1) comboBox_rot.SelectedIndex = 0;
                if (comboBox_gruen.SelectedIndex == 1) comboBox_gruen.SelectedIndex = 0;
                if (comboBox_blau.SelectedIndex == 1) comboBox_blau.SelectedIndex = 0;
                this.ausgewählte_farbe = FARBE.GELB;
            }
            else if(comboBox_gelb != null && comboBox_gruen != null && comboBox_blau != null && comboBox_rot != null)
            {
                // Diese Anweisungen sorgen dafür, das man sich nicht aus dem Spiel ausschließen kann.
                if (comboBox_gelb.SelectedIndex != 1 && comboBox_gruen.SelectedIndex != 1 && comboBox_blau.SelectedIndex != 1 && comboBox_rot.SelectedIndex != 1) comboBox_gelb.SelectedIndex = 1;
            }
            Überprüfe_Comboboxauswahl();

        }

        private void comboBox_gruen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_gruen.SelectedIndex == 1)
            {
                if (comboBox_gelb.SelectedIndex == 1) comboBox_gelb.SelectedIndex = 0;
                if (comboBox_rot.SelectedIndex == 1) comboBox_rot.SelectedIndex = 0;
                if (comboBox_blau.SelectedIndex == 1) comboBox_blau.SelectedIndex = 0;
                this.ausgewählte_farbe = FARBE.GRUEN;
            }
            else if (comboBox_gelb != null && comboBox_gruen != null && comboBox_blau != null && comboBox_rot != null)
            {
                if (comboBox_gelb.SelectedIndex != 1 && comboBox_gruen.SelectedIndex != 1 && comboBox_blau.SelectedIndex != 1 && comboBox_rot.SelectedIndex != 1) comboBox_gruen.SelectedIndex = 1;
            }
            Überprüfe_Comboboxauswahl();
        }

        private void comboBox_blau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_blau.SelectedIndex == 1)
            {
                if (comboBox_gelb.SelectedIndex == 1) comboBox_gelb.SelectedIndex = 0;
                if (comboBox_gruen.SelectedIndex == 1) comboBox_gruen.SelectedIndex = 0;
                if (comboBox_rot.SelectedIndex == 1) comboBox_rot.SelectedIndex = 0;
                this.ausgewählte_farbe = FARBE.BLAU;
            }
            else if (comboBox_gelb != null && comboBox_gruen != null && comboBox_blau != null && comboBox_rot != null)
            {
                if (comboBox_gelb.SelectedIndex != 1 && comboBox_gruen.SelectedIndex != 1 && comboBox_blau.SelectedIndex != 1 && comboBox_rot.SelectedIndex != 1) comboBox_blau.SelectedIndex = 1;
            }
            Überprüfe_Comboboxauswahl();
        }

        // Bei den Folgenden 3 Funktionen passiert im prinzip das Gleiche wie in dieser
        private void L_Name_Spieler_rot_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label_text_hat_sich_verändert();
        }
        //      \/       \/       \/       \/       \/       \/

        private void L_Name_Spieler_gelb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label_text_hat_sich_verändert();
        }

        private void L_Name_Spieler_gruen_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label_text_hat_sich_verändert();
        }

        private void L_Name_Spieler_blau_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label_text_hat_sich_verändert();
        }


        //      Relativ trivialer Kram
        //       ||               ||
        //       \/               \/  

        private void Spielername_eingabe_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Spielername_eingabe.Text == "") Spielername_eingabe.Text = "Hier Namen eingeben";
        }

        private void Spielername_eingabe_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Spielername_eingabe.Text == "Hier Namen eingeben") Spielername_eingabe.Text = "";
        }

        private bool Prüfe_Startbedingungen(TextBox Label)
        {
            switch (Label.Text.ToString())
            {
                case "Ich":
                    {
                        string eing = Spielername_eingabe.Text;
                        if (eing == "Ich" || eing == "Geschlossen" || eing == "Computergegner" || eing.Length > 20 || eing.Contains(","))
                        {
                            MessageBox.Show( "Ihr Spielername dar nicht \"Ich , Geschlossen, Computergegner\" lauten, leer sein, länger als 20 Zeichen sein\noder \",\" enthalten !", "Fehler", MessageBoxButton.OK);
                            return false;
                        }
                        else return true;
                    }
                case "Offen": MessageBox.Show("Fehler", "Es darf kein Slott mehr offen sein!", MessageBoxButton.OK); return false;
                case "Geschlossen": return false;
            }
            return true;
        }

        private int Ermittle_freie_Plätze() //Wird dazu verwendet um die BC-Informationen aktuell zu halten.
        {
            int result = 0;
            if (L_Name_Spieler_rot.Text.ToString() == "Offen") result += 1;
            if (L_Name_Spieler_gelb.Text.ToString() == "Offen") result += 1;
            if (L_Name_Spieler_gruen.Text.ToString() == "Offen") result += 1;
            if (L_Name_Spieler_blau.Text.ToString() == "Offen") result += 1;
            return result;
        }
        
        private void Send_Broadcast()
        {
            while (broadcast_status)
            {
                Netzwerkkommunikation.Send_UDP_BC_Packet(broadcast_string);
                Thread.Sleep(1000);// Ein mal in der Sekunde wird ein Broadcast gesendet;
            }
        }

        private void Listen_for_TCP_Pakete()
        {
            while (broadcast_status)
            {
                Netzwerkkommunikation.Start_TCP_Listener();
            }
        }

        private void Label_text_hat_sich_verändert()
        {
            globale_temporäre_Variablen.eigener_Host.freie_plätze = Ermittle_freie_Plätze();
            if (broadcast_status)
            {
                Create_BC_message();
                Überprüfe_Label();
            }
        }

        private void Create_BC_message()
        {
             broadcast_string = "Hostinformationen," +
                                eigene_IPAddresse.ToString() + "," +
                                Host_name + "," +
                                globale_temporäre_Variablen.eigener_Host.freie_plätze.ToString() + "," +
                                Freieplätze.ToString() + "," +
                                L_Name_Spieler_rot.Text + "," +
                                L_Name_Spieler_gelb.Text + "," +
                                L_Name_Spieler_gruen.Text + "," +
                                L_Name_Spieler_blau.Text;
        }

        private void Überprüfe_Comboboxauswahl()
        {
            if (btn_Hosten == null) return;
            bool rot = false, gelb = false, gruen = false, blau = false;
            if (comboBox_rot.SelectedIndex == 3) rot = true;
            if (comboBox_gelb.SelectedIndex == 3) gelb = true;
            if (comboBox_gruen.SelectedIndex == 3) gruen = true;
            if (comboBox_blau.SelectedIndex == 3) blau = true;

            if (rot == true || gelb == true || gruen == true || blau == true)
            {
                btn_Hosten.IsEnabled = true;
                btn_spiel_starten.IsEnabled = false;
            }
            else
            {
                btn_Hosten.IsEnabled = false;
                btn_spiel_starten.IsEnabled = true;
            }
        }

        private void Überprüfe_Label()
        {
            if (L_Name_Spieler_rot.Text.ToString() == "Offen") return;
            if (L_Name_Spieler_gelb.Text.ToString() == "Offen") return;
            if (L_Name_Spieler_gruen.Text.ToString() == "Offen") return;
            if (L_Name_Spieler_blau.Text.ToString() == "Offen") return;

            btn_spiel_starten.IsEnabled = true;
        }

        private bool Überprüfe_eingabe_Name()
        {
            if (Spielername_eingabe.Text.Length >= 20)
            {
                MessageBox.Show("Dein Name darf nicht länger als 20 Zeichen sein!", "Fehler", MessageBoxButton.OK);
                return false;
            }
            if (Spielername_eingabe.Text.Contains(","))
            {
                MessageBox.Show("Dein Name darf keine \",\"'s enthalten!", "Fehler", MessageBoxButton.OK);
                return false;
            }
            switch (Spielername_eingabe.Text)
            {
                case "Ich": MessageBox.Show("Dein Name darf nicht \"Ich\" lauten!", "Fehler", MessageBoxButton.OK); return false;
                case "Computergegner": MessageBox.Show("Dein Name darf nicht \"Computergegner\" lauten!", "Fehler", MessageBoxButton.OK); return false;
                case "": MessageBox.Show("Dein Name darf nicht leer sein!", "Fehler", MessageBoxButton.OK); return false;
                case "Hier Namen eingeben": MessageBox.Show("Dein Name darf nicht \"Hier Namen eingeben\" lauten!", "Fehler", MessageBoxButton.OK); return false;
                case "Offen": MessageBox.Show("Dein Name darf nicht \"Offen\" lauten!", "Fehler", MessageBoxButton.OK); return false;
                case "Geschlossen": MessageBox.Show("Dein Name darf nicht \"Geschlossen\" lauten!", "Fehler", MessageBoxButton.OK); return false;
            }
            return true;
        }
    }
}
