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

namespace Abschlussprojekt.Seiten
{
    /// <summary>
    /// Interaktionslogik für Spiel_beitreten.xaml
    /// </summary>
    public partial class Spiel_beitreten : UserControl
    {
        Frame root_Frame;
        bool status; // false = bestätigt; true = noch nicht bestätigt;
        public Spiel_beitreten(Frame root_Frame)
        {
            this.status = true;
            this.root_Frame = root_Frame;
            InitializeComponent();
        }

        public void update(string rot, string gelb, string gruen, string blau)
        {
            L_Name_Spieler_rot.Content = rot;
            L_Name_Spieler_gelb.Content = gelb;
            L_Name_Spieler_gruen.Content = gruen;
            L_Name_Spieler_blau.Content = blau;

            if (L_Name_Spieler_rot.Content.ToString() == "Offen" && status) RB_rot.IsEnabled = true;
            else RB_rot.IsEnabled = false;

            if (L_Name_Spieler_gelb.Content.ToString() == "Offen" && status) RB_gelb.IsEnabled = true;
            else RB_gelb.IsEnabled = false;

            if (L_Name_Spieler_gruen.Content.ToString() == "Offen" && status) RB_gruen.IsEnabled = true;
            else RB_gruen.IsEnabled = false;

            if (L_Name_Spieler_blau.Content.ToString() == "Offen" && status) RB_blau.IsEnabled = true;
            else RB_blau.IsEnabled = false;
        }

        private void DV_button_Click(object sender, RoutedEventArgs e)
        {
            update("Offen","Hans","Offen","Admin");
        }

        private void btn_Bestätigen_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(RB_rot.IsChecked) || Convert.ToBoolean(RB_gelb.IsChecked) || Convert.ToBoolean(RB_gruen.IsChecked) || Convert.ToBoolean(RB_blau.IsChecked))
            {
                status = false;
                RB_rot.IsEnabled = false;
                RB_gelb.IsEnabled = false;
                RB_gruen.IsEnabled = false;
                RB_blau.IsEnabled = false;
            }
            else MessageBox.Show("Sie müssen eine Farbe wählen!", "Achtung", MessageBoxButton.OK);
        }
    }
}
