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
    /// Interaktionslogik für Sp_suchen.xaml
    /// </summary>
    public partial class Sp_suchen : Page
    {
        public Sp_suchen()
        {
            InitializeComponent();
            Statische_Variablen.aktuelle_Seite = "Spiel_suchen";
        }
    }
}
