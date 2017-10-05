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
    /// Interaktionslogik für Startseite.xaml
    /// </summary>
    public partial class Startseite : UserControl
    {
        Frame root_Frame;
        public Startseite(Frame root_Frame)
        {
            this.root_Frame = root_Frame;
            InitializeComponent();
        }

        private void Btn_Spiel_starten_Click(object sender, RoutedEventArgs e)
        {
            root_Frame.Content = new Spiel_erstellen(root_Frame);
        }

        private void btn_Spiel_suchen_Click(object sender, RoutedEventArgs e)
        {
            root_Frame.Content = new Spiel_suchen(root_Frame);
        }
    }
}
