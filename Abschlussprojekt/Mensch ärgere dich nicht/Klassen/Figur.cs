using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

// Namenskonvention: --------------------------------------+
//                                                         |
// Alle Wörter eines Namens werden mit einem "_" getrennt. |
// Klassen     = Klasse_Bsp    => erster Buchstabe groß    |
// Methoden    = Methode_Bsp   => erster Buchstabe groß    |
// Variable    = variable_Bsp  => erster Buchstabe klein   |
// ENUM        = ENUM_BSP      => alle Buchstaben groß     |
//---------------------------------------------------------+

namespace Mensch_ärgere_dich_nicht.Klassen
{
    class Figur
    {
        
        public Statische_Variablen.FARBE farbe { get; }
        public int id { get; }

        public Figur(Statische_Variablen.FARBE farbe,int id)
        {
            this.id = id;
            this.farbe = farbe;
        }
    }
}
