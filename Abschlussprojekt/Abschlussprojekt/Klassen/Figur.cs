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
        public delegate void Bild_Update();
        public Image bild { get; }
        public Feld startposition { get; }
        public Feld aktuelle_Position { get; set; }
        public Feld mögliche_Position { get; }
        public FARBE farbe { get; }
        public int id { get; }

        public Figur(FARBE farbe,int id)
        {
            this.id = id;
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
            bild.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            bild.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            foreach (Feld startfeld in start_felder)
            {
                if (startfeld.farbe == this.farbe && startfeld.figur == null) // In diesem IF Block wird einem Freien startfeld für die jeweilige farbe die figur zugewiesen und der Figur das ausgewählte Startfeld.
                {
                    startfeld.figur = this;
                    this.startposition = startfeld;
                    break;
                }
            }

            Set_Figure_to_Start();
            
        }

        public void Set_Figure_to_Start()
        {
            bild.Dispatcher.Invoke(new Bild_Update(Set_Bild_Startposition));
        }

        public void Set_Figureposition(Feld feld)
        {
            if (feld.figur != null)
            {
                if (feld.figur.farbe != this.farbe)
                {
                    feld.Set_figur(this);
                    aktuelle_Position = feld;
                    bild.Dispatcher.Invoke(new Bild_Update(Set_Bild_Position));
                }
                else return;
            }
            feld.Set_figur(this);
            aktuelle_Position = feld;
            bild.Dispatcher.Invoke(new Bild_Update(Set_Bild_Position));
            
        }

        public void Set_Bild_Position()
        {
            bild.Margin = new System.Windows.Thickness(aktuelle_Position.position.X, aktuelle_Position.position.Y, 0, 0);
        }

        public void Set_Bild_Startposition()
        {
            bild.Margin = new System.Windows.Thickness(startposition.position.X, startposition.position.Y, 0, 0);
        }
    }
}
