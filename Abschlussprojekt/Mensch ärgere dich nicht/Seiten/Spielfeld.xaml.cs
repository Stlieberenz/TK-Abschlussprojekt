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
    /// Interaktionslogik für Spielfeld.xaml
    /// </summary>
    public partial class Spielfeld : Page
    {
        public Spielfeld()
        {
            InitializeComponent();
            Statische_Variablen.aktuelle_Seite = "Spielfeld";
            Statische_Variablen.mainWindow.WindowState = WindowState.Maximized;
            Klassen.SeitenFunktionen.Spielfeld.spielfeld = G_spielfeld;
            Klassen.SeitenFunktionen.Spielfeld.Erstelle_Oberfläche();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wollen sie wirklich aufgeben?", "Achtung!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Statische_Variablen.aktuelle_Seite = "Menü";
                Statische_Variablen.mainWindow.Content = new Seiten.Menü();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Würfel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
