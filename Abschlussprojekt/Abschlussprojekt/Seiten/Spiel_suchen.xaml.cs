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
        public Spiel_suchen(Frame root_Frame)
        {
            this.root_Frame = root_Frame;
            InitializeComponent();
        }

        private void btn_beitreten_Click(object sender, RoutedEventArgs e)
        {
            // #### Muss später durch richtigen Code ersetzt werden ####
            root_Frame.Content = new Spiel_beitreten(root_Frame);
        }
    }
}
