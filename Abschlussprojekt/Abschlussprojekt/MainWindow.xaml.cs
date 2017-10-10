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
            Klassen.Datenbankschnittstelle.init();
            Klassen.Netzwerkkommunikation.Iinitialisiere_IP_Addresse();
            Task task1 = Task.Factory.StartNew(TCPListener);
            rootFrame.Content = new Seiten.Startseite(rootFrame);
        }

        static void TCPListener()
        {
            while (true)
            {
                Klassen.Netzwerkkommunikation.Start_TCP_Listener();
            }
        }
    }
}
