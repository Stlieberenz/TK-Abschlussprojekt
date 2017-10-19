using Abschlussprojekt.Klassen;
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

namespace Abschlussprojekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            rootFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            //Klassen.Datenbankschnittstelle.init();
            //Klassen.Netzwerkkommunikation.Iinitialisiere_IP_Addressen();
            //Klassen.Netzwerkkommunikation.Iinitialisiere_BC_IP_Addressen();
            
            
            Statische_Variablen.mainWindow = this;
            rootFrame.Content = new Seiten.Startseite(rootFrame);
        }
    }
}
