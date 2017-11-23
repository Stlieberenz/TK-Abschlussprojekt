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
using Abschlussprojekt.Seiten;
using Abschlussprojekt.Klassen.Seiten_Funktionen;

// Namenskonvention: --------------------------------------+
//                                                         |
// Alle Wörter eines Namens werden mit einem "_" getrennt. |
// Klassen     = Klasse_Bsp    => erster Buchstabe groß    |
// Methoden    = Methode_Bsp   => erster Buchstabe groß    |
// Variable    = variable_Bsp  => erster Buchstabe klein   |
// ENUM        = ENUM_BSP      => alle Buchstaben groß     |
// Delegate    = ?                                         |
//---------------------------------------------------------+

namespace Abschlussprojekt.Seiten
{
    /// <summary>
    /// Interaktionslogik für Startseite.xaml
    /// </summary>
    public partial class Startseite : UserControl
    {
        Frame root_Frame;
        //private bool initialized = false;
        public Startseite(Frame root_Frame)
        {
            this.root_Frame = root_Frame;
            Klassen.Statische_Variablen.aktive_Seite = Klassen.Statische_Variablen.AKTIVE_SEITE.STARTSEITE;
            InitializeComponent();
            
        }

        private void Btn_Spiel_starten_Click(object sender, RoutedEventArgs e)
        {
            if (!Klassen.Statische_Variablen.initialized)
            {
                Klassen.Statische_Methoden.Initialisiere_Images_für_Figuren(); // Hier werden die Bilder für die Figuren geladen.
                Klassen.Netzwerkkommunikation.Iinitialisiere_IP_Addressen();
                Klassen.Netzwerkkommunikation.Iinitialisiere_BC_IP_Addressen();
                Klassen.Statische_Variablen.initialized = true;
            }
            
            Klassen.Statische_Variablen.Host_name = Spielername.Text;
            Klassen.globale_temporäre_Variablen.eigener_Host = new Klassen.Host(Spielername.Text, Klassen.Statische_Variablen.eigene_IPAddresse);
            root_Frame.Content = new Spiel_erstellen(root_Frame);
        }

        private void btn_Spiel_suchen_Click(object sender, RoutedEventArgs e)
        {
            if (!Klassen.Statische_Variablen.initialized)
            {
                Klassen.Statische_Methoden.Initialisiere_Images_für_Figuren(); // Hier werden die Bilder für die Figuren geladen.
                Klassen.Netzwerkkommunikation.Iinitialisiere_IP_Addressen();
                Klassen.Netzwerkkommunikation.Iinitialisiere_BC_IP_Addressen();
                Klassen.Statische_Variablen.initialized = true;
            }
            root_Frame.Content = new Spiel_suchen(root_Frame);
        }

        private void btn_Beenden_Click(object sender, RoutedEventArgs e)
        {
            Klassen.Statische_Variablen.mainWindow.Close();
            Application.Current.Shutdown();
        }
    }
}
