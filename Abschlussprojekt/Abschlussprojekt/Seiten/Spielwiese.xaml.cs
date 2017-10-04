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
using static Abschlussprojekt.Klassen.Statische_Methoden;

namespace Abschlussprojekt.Seiten
{
    /// <summary>
    /// Interaktionslogik für Spielwiese.xaml
    /// </summary>
    public partial class Spielwiese : UserControl
    {
        public Spielwiese()
        {
            InitializeComponent();
            Initialisiere_Statische_Variablen();
            Initialisiere_Spiel("","","","");
        }
    }
}
