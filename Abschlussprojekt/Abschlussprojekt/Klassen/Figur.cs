using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class Figur
    {
        public Image bild { get; }
        public Feld startposition { get; }
        public Feld aktuelle_Position { get; set; }
        public Feld mögliche_Position { get; }
        public FARBE farbe { get;  }

        public Figur(FARBE farbe)
        {
            this.farbe = farbe;
            this.bild = new Image();
            //
            // Hier werden die Jeweiligen Figuren, die zur Laufzeit erstellt werden,
            // den jeweiligen statischen Listen hinzugefügt und die Bitmaps für das image ausgewählt.
            //
            switch (farbe)
            {
                case FARBE.ROT:
                    {
                        spieler_rot.Add(this);
                        this.bild.Source = Figur_rot;
                        break;
                    }
                case FARBE.GELB:
                    {
                        spieler_gelb.Add(this);
                        bild.Source = Figur_gelb;
                        break;
                    }
                case FARBE.GRUEN:
                    {
                        spieler_gruen.Add(this);
                        bild.Source = Figur_gruen;
                        break;
                    }
                case FARBE.BLAU:
                    {
                        spieler_blau.Add(this);
                        bild.Source = Figur_blau;
                        break;
                    }
            }

            bild.Height = figur_höhe;
            bild.Width = figur_breite;
            
            foreach(Feld startfeld in start_felder)
            {
                if (startfeld.farbe == this.farbe && startfeld.figur == null) // In diesem IF Block wird einem Freien startfeld für die jeweilige farbe die figur zugewiesen und der Figur das ausgewählte Startfeld.
                {
                    startfeld.figur = this;
                    this.startposition = startfeld;
                    break;
                }
            }

            bild.Margin = new System.Windows.Thickness(startposition.position.X - 1550/2, startposition.position.X - 838/2, 0, 0);
        }
    }
}
