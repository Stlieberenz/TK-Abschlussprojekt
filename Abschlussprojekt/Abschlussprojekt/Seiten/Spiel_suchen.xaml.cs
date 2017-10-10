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
        bool UDP_thread_status = true;

        public Spiel_suchen(Frame root_Frame)
        {
            this.root_Frame = root_Frame;
            InitializeComponent();
            Statische_Variablen.hosts = Hosts;
            Statische_Variablen.Spiel_Suchen = Spiel_suchen_grid;
            Task UDP_Listener = Task.Factory.StartNew(Start_UDP_Listener);
            Task updater = Task.Factory.StartNew(Updater);
        }

        private void btn_beitreten_Click(object sender, RoutedEventArgs e)
        {
            // #### Muss später durch richtigen Code ersetzt werden ####
            UDP_thread_status = false;
            root_Frame.Content = new Spiel_beitreten(root_Frame);
        }

        private void btn_aktualisieren_Click(object sender, RoutedEventArgs e)
        {
            Hosts.Items.Clear();
            foreach (Host host in Statische_Variablen.alle_Hosts)
            {
                Hosts.Items.Add(host.hostname);
            }
        }

        private void Start_UDP_Listener()
        {
            while (UDP_thread_status)
            {
                Netzwerkkommunikation.Start_UDP_Listener();
            }
        }

        private void Updater()// Macht nix weiter als die Liste mit Hosts alle 5 s zu löschen damit alte nicht mehr verfügbare Hosts gelöscht werden und nur wirklich aktuelle angezeigt werden.
        {
            while (UDP_thread_status)
            {
                Statische_Variablen.alle_Hosts.Clear();
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
