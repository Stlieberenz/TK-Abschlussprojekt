using System;
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
            
        }

        private void btn_spiel_starten_Click(object sender, RoutedEventArgs e)
        {
            //
            //In den If Abfragen wird geprüft ob min 2 gültige spieler da sind und kein slot mehr offen ist und ein Name angegeben wurde.
            //
            int temp = 0;
            bool result = true;
            if(L_Name_Spieler_rot.Text.ToString() == "Computergegner" || L_Name_Spieler_rot.Text.ToString() == "Ich" )
            {
                temp++;
            }
            else if( L_Name_Spieler_rot.Text.ToString() == "Offen")
            {
                MessageBox.Show("Der Slot darf nicht offen sein. \nEs muss ein Spieler auf den Freien Slot kommen,\noder der Slot muss geschlossen werden", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (L_Name_Spieler_gelb.Text.ToString() == "Computergegner" || L_Name_Spieler_gelb.Text.ToString() == "Ich" )
            {
                temp++;
            }
            else if (L_Name_Spieler_gelb.Text.ToString() == "Offen")
            {
                MessageBox.Show("Der Slot darf nicht offen sein. \nEs muss ein Spieler auf den Freien Slot kommen,\noder der Slot muss geschlossen werden", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (L_Name_Spieler_gruen.Text.ToString() == "Computergegner" || L_Name_Spieler_gruen.Text.ToString() == "Ich" )
            {
                temp++;
            }
            else if (L_Name_Spieler_gruen.Text.ToString() == "Offen")
            {
                MessageBox.Show("Der Slot darf nicht offen sein. \nEs muss ein Spieler auf den Freien Slot kommen,\noder der Slot muss geschlossen werden", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (L_Name_Spieler_blau.Text.ToString() == "Computergegner" || L_Name_Spieler_blau.Text.ToString() == "Ich" )
            {
                temp++;
            }
            else if (L_Name_Spieler_blau.Text.ToString() == "Offen")
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
                switch (comboBox_rot.SelectedIndex)
                {
                    case 1: Klassen.globale_temporäre_Variablen.lokaler_spieler =   new Klassen.Spieler(FARBE.ROT, Spielername_eingabe.Text,SPIELER_ART.NORMALER_SPIELER, eigene_IPAddresse); break;
                    case 2: Klassen.Spieler computergegner0 =                       new Klassen.Spieler(FARBE.ROT,"computergegner0", SPIELER_ART.COMPUTERGEGNER,new System.Net.IPAddress(0));break;
                    case 3: if (L_Name_Spieler_rot.Text.ToString() != "Offen") ; break; 
                    case 0: break;
                }
                switch (comboBox_gelb.SelectedIndex)
                {
                    case 1: Klassen.globale_temporäre_Variablen.lokaler_spieler =   new Klassen.Spieler(FARBE.GELB, Spielername_eingabe.Text, SPIELER_ART.NORMALER_SPIELER, eigene_IPAddresse); break;
                    case 2: Klassen.Spieler computergegner0 =                       new Klassen.Spieler(FARBE.GELB, "computergegner1", SPIELER_ART.COMPUTERGEGNER, new System.Net.IPAddress(0)); break;
                    case 3: if (L_Name_Spieler_gelb.Text.ToString() != "Offen") ; break;
                    case 0: break;
                }
                switch (comboBox_gruen.SelectedIndex)
                {
                    case 1: Klassen.globale_temporäre_Variablen.lokaler_spieler =   new Klassen.Spieler(FARBE.GRUEN, Spielername_eingabe.Text, SPIELER_ART.NORMALER_SPIELER, eigene_IPAddresse); break;
                    case 2: Klassen.Spieler computergegner0 =                       new Klassen.Spieler(FARBE.GRUEN, "computergegner2", SPIELER_ART.COMPUTERGEGNER, new System.Net.IPAddress(0)); break;
                    case 3: if (L_Name_Spieler_gruen.Text.ToString() != "Offen") ; break; 
                    case 0: break;
                }
                switch (comboBox_blau.SelectedIndex)
                {
                    case 1: Klassen.globale_temporäre_Variablen.lokaler_spieler = new Klassen.Spieler(FARBE.BLAU, Spielername_eingabe.Text, SPIELER_ART.NORMALER_SPIELER,eigene_IPAddresse); break;
                    case 2: Klassen.Spieler computergegner0 = new Klassen.Spieler(FARBE.BLAU, "computergegner3", SPIELER_ART.COMPUTERGEGNER, new System.Net.IPAddress(0)); break;
                    case 3: if (L_Name_Spieler_blau.Text.ToString() != "Offen") ;  break; 
                    case 0: break;
                }

                broadcast_status = false;
                root_Frame.Content = new Spielwiese();
            }
            
        }

        private void btn_abbrechen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Achtung!", "Wollen sie wirklich abbrechen ?!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                broadcast_status = false;
                Spielerstellenlabel.Clear();
                //ToDo: absage an Clients senden
                for (int i = 0; i < 10; i++)
                {
                    Netzwerkkommunikation.SendBroadcastPacket("Hostinformationen," + eigene_IPAddresse.ToString() + ",absage,,,,,,");
                }
                root_Frame.Content = new Startseite(root_Frame);
            }
        }

        private void btn_Hosten_Click(object sender, RoutedEventArgs e)
        {
            int temp = 0;
            bool result = true;
            if (L_Name_Spieler_rot.Text.ToString() == "Computergegner" || L_Name_Spieler_rot.Text.ToString() == "Ich" || L_Name_Spieler_rot.Text.ToString() == "Offen")
            {
                temp++;
            }

            if (L_Name_Spieler_gelb.Text.ToString() == "Computergegner" || L_Name_Spieler_gelb.Text.ToString() == "Ich" || L_Name_Spieler_gelb.Text.ToString() == "Offen")
            {
                temp++;
            }

            if (L_Name_Spieler_gruen.Text.ToString() == "Computergegner" || L_Name_Spieler_gruen.Text.ToString() == "Ich" || L_Name_Spieler_gruen.Text.ToString() == "Offen")
            {
                temp++;
            }

            if (L_Name_Spieler_blau.Text.ToString() == "Computergegner" || L_Name_Spieler_blau.Text.ToString() == "Ich" || L_Name_Spieler_blau.Text.ToString() == "Offen")
            {
                temp++;
            }

            if (temp < 2)
            {
                MessageBox.Show("Es müssen mindestens 2 Spieler gegeneinander antreten", "Fehler", MessageBoxButton.OK);
                return;
            }
            if (Spielername_eingabe.Text == "" || Spielername_eingabe.Text == "Hier Namen eingeben" || Spielername_eingabe.Text.Length >= 20)
            {
                MessageBox.Show("Es muss ein gültiger Name eingegeben werden!", "Fehler", MessageBoxButton.OK);
                return;
            }

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

        private int Update_offene_Plätze()
        {
            int result = 0;
            if (L_Name_Spieler_rot.Text.ToString() == "Offen") result += 1 ;
            if (L_Name_Spieler_gelb.Text.ToString() == "Offen") result += 1;
            if (L_Name_Spieler_gruen.Text.ToString() == "Offen") result += 1;
            if (L_Name_Spieler_blau.Text.ToString() == "Offen") result += 1;
            return result;
        }

        private void Berechne_Freieplätze()
        {
            int result = 0;
            if (L_Name_Spieler_rot.Text.ToString() == "Offen") result += 1;
            if (L_Name_Spieler_gelb.Text.ToString() == "Offen") result += 2;
            if (L_Name_Spieler_gruen.Text.ToString() == "Offen") result += 4;
            if (L_Name_Spieler_blau.Text.ToString() == "Offen") result += 8;
            Freieplätze = result;
        }

        private string Spieler_name(FARBE farbe)
        {
            foreach (Spieler spieler in alle_Spieler)
            {
                if (farbe == spieler.farbe) return spieler.name;
            }
            return null;
        }

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
        }

        private void comboBox_gelb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_gelb.SelectedIndex == 1)
            {
                if (comboBox_rot.SelectedIndex == 1) comboBox_rot.SelectedIndex = 0;
                if (comboBox_gruen.SelectedIndex == 1) comboBox_gruen.SelectedIndex = 0;
                if (comboBox_blau.SelectedIndex == 1) comboBox_blau.SelectedIndex = 0;
                this.ausgewählte_farbe = FARBE.GELB;
            }
            else if(comboBox_gelb != null && comboBox_gruen != null && comboBox_blau != null && comboBox_rot != null)
            {
                if (comboBox_gelb.SelectedIndex != 1 && comboBox_gruen.SelectedIndex != 1 && comboBox_blau.SelectedIndex != 1 && comboBox_rot.SelectedIndex != 1) comboBox_gelb.SelectedIndex = 1;
            }
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
        }

        private void Spielername_eingabe_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Spielername_eingabe.Text == "") Spielername_eingabe.Text = "Hier Namen eingeben";
        }

        private void Spielername_eingabe_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Spielername_eingabe.Text == "Hier Namen eingeben") Spielername_eingabe.Text = "";
        }

        private void Send_Broadcast()
        {
            while (broadcast_status)
            {
                Netzwerkkommunikation.SendBroadcastPacket(broadcast_string);
                Console.WriteLine("BC gesendet");
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
        
        private void L_Name_Spieler_rot_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (comboBox_rot.SelectedIndex == 3 && L_Name_Spieler_rot.Text != "Offen") comboBox_rot.IsEnabled = false; // Wenn sich ein spieler erfolgreich einwählt wird die Combobox deaktiviert.
            globale_temporäre_Variablen.eigener_Host.freie_plätze = Update_offene_Plätze();
            Berechne_Freieplätze();
            Create_BC_message();
        }

        private void L_Name_Spieler_gelb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (comboBox_gelb.SelectedIndex == 3 && L_Name_Spieler_gelb.Text != "Offen") comboBox_gelb.IsEnabled = false; // Wenn sich ein spieler erfolgreich einwählt wird die Combobox deaktiviert.
            globale_temporäre_Variablen.eigener_Host.freie_plätze = Update_offene_Plätze();
            Berechne_Freieplätze();
            Create_BC_message();
        }

        private void L_Name_Spieler_gruen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (comboBox_gruen.SelectedIndex == 3 && L_Name_Spieler_gruen.Text != "Offen") comboBox_gruen.IsEnabled = false; // Wenn sich ein spieler erfolgreich einwählt wird die Combobox deaktiviert.
            globale_temporäre_Variablen.eigener_Host.freie_plätze = Update_offene_Plätze();
            Berechne_Freieplätze();
            Create_BC_message();
        }

        private void L_Name_Spieler_blau_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (comboBox_blau.SelectedIndex == 3 && L_Name_Spieler_blau.Text != "Offen") comboBox_blau.IsEnabled = false; // Wenn sich ein spieler erfolgreich einwählt wird die Combobox deaktiviert.
            globale_temporäre_Variablen.eigener_Host.freie_plätze = Update_offene_Plätze();
            Berechne_Freieplätze();
            Create_BC_message();
        }

        private void Create_BC_message()
        {
            broadcast_string = "Hostinformationen," + eigene_IPAddresse.ToString() + "," + Host_name + "," + Klassen.globale_temporäre_Variablen.eigener_Host.freie_plätze.ToString() + "," + Freieplätze.ToString() + "," + L_Name_Spieler_rot.Text + "," + L_Name_Spieler_gelb.Text + "," + L_Name_Spieler_gruen.Text + "," + L_Name_Spieler_blau.Text;
        }
    }
}
