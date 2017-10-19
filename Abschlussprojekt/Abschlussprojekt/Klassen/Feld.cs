using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Abschlussprojekt.Klassen.Statische_Variablen;

// Namenskonvention: --------------------------------------+
//                                                         |
// Alle Wörter eines Namens werden mit einem "_" getrennt. |
// Klassen     = Klasse_Bsp    => erster Buchstabe groß    |
// Methoden    = Methode_Bsp   => erster Buchstabe groß    |
// Variable    = variable_Bsp  => erster Buchstabe klein   |
// ENUM        = ENUM_BSP      => alle Buchstaben groß     |
//---------------------------------------------------------+

namespace Abschlussprojekt.Klassen
{

    class Feld
    {
        public FARBE farbe { get; }
        public FELD_EIGENSCHAFT feld_art { get; }
        public Figur figur { get; set; }
        public Point position { get; }
        public int id { get; }

        public Feld(FARBE farbe, FELD_EIGENSCHAFT feld_art, Point position,int id)
        {
            this.farbe = farbe;
            this.feld_art = feld_art;
            this.position = position;
            this.id = id;

            //
            // Hier werden die Jeweiligen Felder, die zur Laufzeit erstellt werden,
            // den jeweiligen statischen Listen hinzugefügt.
            //
            switch (feld_art)
            {
                case FELD_EIGENSCHAFT.STARTPOSITION: start_felder.Add(this); break;
                case FELD_EIGENSCHAFT.SPIELFELD: spiel_felder.Add(this); break;
                case FELD_EIGENSCHAFT.ZIEL: ziel_felder.Add(this); break;
            }

        }

        public void Set_figur(Figur figur)
        {
            if (this.figur != null) this.figur.Set_Figure_to_Start();
            else this.figur = figur;
        }
    }
}
