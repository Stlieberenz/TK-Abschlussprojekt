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
    /// Interaktionslogik für Startseite.xaml
    /// </summary>
    public partial class Startseite : Page
    {
        public Startseite()
        {
            InitializeComponent();
            Klassen.Netzwerkkommunikation.Iinitialisiere_IP_Addressen();
            Klassen.Netzwerkkommunikation.Iinitialisiere_BC_IP_Addressen();
        }

        private void BTN_Anmelden_Click(object sender, RoutedEventArgs e)
        {
            if (Klassen.SeitenFunktionen.Startseite.Überprüfe_anmeldenamen(TB_Anmeldename.Text))
            {
                Statische_Variablen.lokaler_Spieler = TB_Anmeldename.Text;
                Statische_Variablen.rootFrame.Content = new Seiten.Menü();
            }
        }

        private void BTN_Beenden_Click(object sender, RoutedEventArgs e)
        {
            Statische_Variablen.mainWindow.Close();
            Application.Current.Shutdown();
        }

        private void TB_Anmeldename_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TB_Anmeldename.Text == "Spieler1") TB_Anmeldename.Text = "";
        }

        private void TB_Anmeldename_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TB_Anmeldename.Text == "") TB_Anmeldename.Text = "Spieler1";
        }
    }
}
