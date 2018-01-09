using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

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
        public List<Feld> wegstrecke { get; set; }
        public Feld aktuelle_position { get; set; }
        public Feld Haus_position { get; }
        public Image bild { get; set; }

        public Figur(Statische_Variablen.FARBE farbe, int id)
        {
            this.id = id;
            this.farbe = farbe;
            this.bild = new Image();

            // Weist dem Image objekt sein Bild zu
            switch (farbe)
            {
                case Statische_Variablen.FARBE.ROT: bild.Source = new BitmapImage(new Uri(Statische_Funktionen.Aktuelles_Verzeichniss() + "\\Bilder\\Figur_rot.gif")); break;
                case Statische_Variablen.FARBE.GELB: bild.Source = new BitmapImage(new Uri(Statische_Funktionen.Aktuelles_Verzeichniss() + "\\Bilder\\Figur_gelb.gif")); break;
                case Statische_Variablen.FARBE.GRÜN: bild.Source = new BitmapImage(new Uri(Statische_Funktionen.Aktuelles_Verzeichniss() + "\\Bilder\\Figur_grün.gif")); break;
                case Statische_Variablen.FARBE.BLAU: bild.Source = new BitmapImage(new Uri(Statische_Funktionen.Aktuelles_Verzeichniss() + "\\Bilder\\Figur_blau.gif")); break;
            }

            //Hinzufügen der Figur der jeweiligen Listen
            SeitenFunktionen.Spielfeld.alle_Figuren.Add(this);
            switch (farbe)
            {
                case Statische_Variablen.FARBE.ROT: Haus_position = SeitenFunktionen.Spielfeld.alle_Hausfelder_Rot[id]; break;
                case Statische_Variablen.FARBE.GELB: Haus_position = SeitenFunktionen.Spielfeld.alle_Hausfelder_Gelb[id]; break;
                case Statische_Variablen.FARBE.GRÜN: Haus_position = SeitenFunktionen.Spielfeld.alle_Hausfelder_Grün[id]; break;
                case Statische_Variablen.FARBE.BLAU: Haus_position = SeitenFunktionen.Spielfeld.alle_Hausfelder_Blau[id]; break;
            }
            Setze_Figur(Haus_position);
        }

        //Diese Funktion Setzt die Figur auf ein belibiges Feld. Dafür nutzt sie die Zeilen und Spaltenangaben des Felds.
        public void Setze_Figur(Feld feld)
        {
            Grid.SetColumn(bild, feld.Spalte);
            Grid.SetRow(bild, feld.Zeile);
        }
    }
}
