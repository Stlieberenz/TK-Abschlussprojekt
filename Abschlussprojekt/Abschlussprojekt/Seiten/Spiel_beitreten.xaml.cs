using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
    /// Interaktionslogik für Spiel_beitreten.xaml
    /// </summary>
    public partial class Spiel_beitreten : UserControl
    {
        
        Frame root_Frame;
        bool status; // false = bestätigt; true = noch nicht bestätigt;
        TextBox ausgewählte_farbe;

        public Spiel_beitreten(Frame root_Frame)
        {
            this.status = true;
            this.root_Frame = root_Frame;
            InitializeComponent();
            Klassen.Statische_Variablen.Beitrittslabel.Add(L_Name_Spieler_rot);
            Klassen.Statische_Variablen.Beitrittslabel.Add(L_Name_Spieler_gelb);
            Klassen.Statische_Variablen.Beitrittslabel.Add(L_Name_Spieler_gruen);
            Klassen.Statische_Variablen.Beitrittslabel.Add(L_Name_Spieler_blau);
           
        }

        private void btn_Bestätigen_Click(object sender, RoutedEventArgs e)
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
            
            if (Convert.ToBoolean(RB_rot.IsChecked) || Convert.ToBoolean(RB_gelb.IsChecked) || Convert.ToBoolean(RB_gruen.IsChecked) || Convert.ToBoolean(RB_blau.IsChecked))
            {
                status = false;
                RB_rot.IsEnabled = false;
                RB_gelb.IsEnabled = false;
                RB_gruen.IsEnabled = false;
                RB_blau.IsEnabled = false;
                btn_Bestätigen.IsEnabled = false;
                Spieler_name.IsEnabled = false;
                ausgewählte_farbe.Text = Spieler_name.Text;
            }
            else MessageBox.Show("Sie müssen eine Farbe wählen!", "Achtung", MessageBoxButton.OK);
        }

        private void RB_rot_Checked(object sender, RoutedEventArgs e)
        {
            this.ausgewählte_farbe = L_Name_Spieler_rot;
        }

        private void RB_gelb_Checked(object sender, RoutedEventArgs e)
        {
            this.ausgewählte_farbe = L_Name_Spieler_gelb;
        }

        private void RB_gruen_Checked(object sender, RoutedEventArgs e)
        {
            this.ausgewählte_farbe = L_Name_Spieler_gruen;
        }

        private void RB_blau_Checked(object sender, RoutedEventArgs e)
        {
            this.ausgewählte_farbe = L_Name_Spieler_blau;
        }

        private void Spieler_name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Spieler_name.Text == "Gib einen Spielernamen ein") Spieler_name.Text = "";
        }

        private void Spieler_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Spieler_name.Text == "") Spieler_name.Text = "Gib einen Spielernamen ein";
        }

        private void L_Name_Spieler_rot_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_Name_Spieler_rot.Text != "Offen" && RB_rot != null) RB_rot.IsEnabled = false;
            else if (L_Name_Spieler_rot.Text == "Offen" && RB_rot != null) RB_rot.IsEnabled = true;
        }

        private void L_Name_Spieler_gelb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_Name_Spieler_gelb.Text != "Offen"&& RB_gelb != null) RB_gelb.IsEnabled = false;
            else if (L_Name_Spieler_gelb.Text == "Offen" && RB_gelb != null) RB_gelb.IsEnabled = true;
        }

        private void L_Name_Spieler_gruen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_Name_Spieler_gruen.Text != "Offen" && RB_gruen != null) RB_gruen.IsEnabled = false;
            else if (L_Name_Spieler_gruen.Text == "Offen" && RB_gruen != null) RB_gruen.IsEnabled = true;
        }

        private void L_Name_Spieler_blau_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_Name_Spieler_blau.Text != "Offen" && RB_blau != null) RB_blau.IsEnabled = false;
            else if(L_Name_Spieler_blau.Text == "Offen" && RB_blau != null) RB_blau.IsEnabled = true;
        }

        private void Label_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Klassen.Netzwerkkommunikation.Anlaysiere_IP_Paket("Beitrittsinformationen,Horst,Günter,Walter,P99");
        }

        private void btn_Abbrechen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Achtung!", "Wollen sie wirklich abbrechen ?!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                root_Frame.Content = new Spiel_suchen(root_Frame);
            }
        }
    }
}
