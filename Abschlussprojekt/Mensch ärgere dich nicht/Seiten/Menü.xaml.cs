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
    /// Interaktionslogik für Menü.xaml
    /// </summary>
    public partial class Menü : Page
    {
        public Menü()
        {
            InitializeComponent();
        }

        private void BTN_Beenden_Click(object sender, RoutedEventArgs e)
        {
            Statische_Variablen.mainWindow.Close();
            Application.Current.Shutdown();
        }

        private void BTN_Zurück_Click(object sender, RoutedEventArgs e)
        {
            Statische_Variablen.rootFrame.Content = new Seiten.Startseite();
        }

        private void BTN_Suchen_Click(object sender, RoutedEventArgs e)
        {
            Statische_Variablen.rootFrame.Content = new Seiten.Sp_suchen();
        }

        private void BTN_Erstellen_Click(object sender, RoutedEventArgs e)
        {
            Statische_Variablen.rootFrame.Content = new Seiten.Sp_erstellen();
        }
    }
}
