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
    class Figur
    {
        public delegate void Bild_Update();
        public Image bild { get; }
        public Feld startposition { get; }
        public Feld spiel_startposition { get; }
        public Feld aktuelle_Position { get; set; }
        public int a_Postition { get; set; }
        public Feld mögliche_Position { get; set; }
        public Feld [] wegstecke { get; }
        public FARBE farbe { get; }
        public int id { get; }

        public Figur(FARBE farbe,int id)
        {
            this.id = id;
            this.farbe = farbe;
            this.bild = new Image();
            this.wegstecke = new Feld[44];
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
            bild.MouseEnter += new  System.Windows.Input.MouseEventHandler(bild_MoubseEnter);
            bild.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(bild_Click);

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
            Init_Wegstrecke();
            this.spiel_startposition = wegstecke[0];
        }

        public void Set_Figure_to_Start()
        {
            bild.Dispatcher.Invoke(new Bild_Update(Set_Bild_Startposition));
        }

        public void Set_Figureposition(int id)
        {
            Set_Figureposition(wegstecke[id]);
            a_Postition = id;
        }

        public void Set_Figureposition(Feld feld)
        {
            aktuelle_Position.figur = null;
            if (feld.figur != null)
            {
                if (feld.figur.farbe != this.farbe)
                {
                    feld.Set_figur(this);
                    aktuelle_Position = feld;
                    a_Postition += z;
                    bild.Dispatcher.Invoke(new Bild_Update(Set_Bild_Position));
                }
                else return;
            }
            feld.Set_figur(this);
            aktuelle_Position = feld;
            a_Postition += z;
            bild.Dispatcher.Invoke(new Bild_Update(Set_Bild_Position));
        }

        public void Set_Bild_Position()
        {
            bild.Margin = new System.Windows.Thickness(aktuelle_Position.position.X, aktuelle_Position.position.Y, 0, 0);
        }

        public void Set_Bild_Startposition()
        {
            bild.Margin = new System.Windows.Thickness(startposition.position.X, startposition.position.Y, 0, 0);
            aktuelle_Position = startposition;
            a_Postition = -6;
        }

        public void Init_Wegstrecke()
        {
            switch (this.farbe)
            {
                case FARBE.ROT: Init_felder(0);break;
                case FARBE.GELB: Init_felder(10);break;
                case FARBE.GRUEN: Init_felder(20); break;
                case FARBE.BLAU: Init_felder(30);break;
            }
        }

        public void Init_felder(int offset)
        {
            for (int i = 0; i < 40; i++)
            {
                this.wegstecke[i] = spiel_felder[offset];
                if (offset == 39) offset = 0;
                else offset += 1;
            }
            foreach (Feld feld in ziel_felder)
            {
                if (feld.farbe == this.farbe)
                {
                    this.wegstecke[feld.id + 40] = feld;
                    this.wegstecke[feld.id + 40] = feld;
                    this.wegstecke[feld.id + 40] = feld;
                    this.wegstecke[feld.id + 40] = feld;
                }
            }
        }

        public void bild_MoubseEnter(object o, System.Windows.Input.MouseEventArgs e)
        {
            if (mögliche_Position != null)
            {
                
            }
        }

        public void bild_Click(object o, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (mögliche_Position != null)
            {
                Set_Figureposition(mögliche_Position);
                Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Spielfigur Update,"+Statische_Methoden.Konvertiere_FARBE_zu_string(this.farbe)+","+this.id+","+aktuelle_Position.position.X+","+ aktuelle_Position.position.Y);
                Statische_Methoden.Figur_wurde_bewegt();
            }
        }
    }
}
