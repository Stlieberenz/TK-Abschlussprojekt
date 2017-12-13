using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mensch_ärgere_dich_nicht.Klassen.SeitenFunktionen
{
    static class S_suchen
    {
        public static ListBox Server_liste;
        public static void Analysiere_Nachricht(string[] content)
        {
            //Index 0: Art der Nachricht
            //Index 1: IP des Hosts
            //Index 2: Name des Spiels; 
            //Index 3: Name des Spieler + Farbe
            //Index n: Weitere Spieler
            if (content[0] == "Spielangebot")
            {

            }
        }
    }
}
