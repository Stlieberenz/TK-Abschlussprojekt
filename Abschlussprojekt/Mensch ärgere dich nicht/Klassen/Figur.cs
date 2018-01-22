using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Input;

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
        public int aktuelle_Wegstreckenposition { get; set; }
        public Feld aktuelle_position { get; set; }
        public Feld Haus_position { get; }
        public Image bild { get; set; }
        public Spieler figur_eigentümer { get;}
        public bool bewegbar { get; set; }

        public Figur(Statische_Variablen.FARBE farbe, int id,Spieler figur_eigentümer)
        {
            this.bewegbar = false;
            this.id = id;
            this.farbe = farbe;
            this.bild = new Image();
            this.figur_eigentümer = figur_eigentümer;
            figur_eigentümer.eigene_Figuren.Add(this);
            // Weist dem Image objekt sein Bild zu
            switch (farbe)
            {
                case Statische_Variablen.FARBE.ROT: bild.Source = new BitmapImage(new Uri(Statische_Funktionen.Aktuelles_Verzeichniss() + "\\Bilder\\Figur_rot.gif")); break;
                case Statische_Variablen.FARBE.GELB: bild.Source = new BitmapImage(new Uri(Statische_Funktionen.Aktuelles_Verzeichniss() + "\\Bilder\\Figur_gelb.gif")); break;
                case Statische_Variablen.FARBE.GRÜN: bild.Source = new BitmapImage(new Uri(Statische_Funktionen.Aktuelles_Verzeichniss() + "\\Bilder\\Figur_gruen.gif")); break;
                case Statische_Variablen.FARBE.BLAU: bild.Source = new BitmapImage(new Uri(Statische_Funktionen.Aktuelles_Verzeichniss() + "\\Bilder\\Figur_blau.gif")); break;
            }

            //Hinzufügen der Figur der jeweiligen Listen
            SeitenFunktionen.Spielfeld.alle_Figuren.Add(this);

            //Bestimmen der Startposition
            switch (farbe)
            {
                case Statische_Variablen.FARBE.ROT:
                    {
                        Haus_position = SeitenFunktionen.Spielfeld.alle_Hausfelder_Rot[id];
                        SeitenFunktionen.Spielfeld.rote_Figuren.Insert(this.id, this);
                        break;
                    }
                case Statische_Variablen.FARBE.GELB:
                    {
                        Haus_position = SeitenFunktionen.Spielfeld.alle_Hausfelder_Gelb[id];
                        SeitenFunktionen.Spielfeld.gelbe_Figuren.Insert(this.id, this);
                        break;
                    }
                case Statische_Variablen.FARBE.GRÜN:
                    {
                        Haus_position = SeitenFunktionen.Spielfeld.alle_Hausfelder_Grün[id];
                        SeitenFunktionen.Spielfeld.grüne_Figuren.Insert(this.id, this);
                        break;
                    }
                case Statische_Variablen.FARBE.BLAU:
                    {
                        Haus_position = SeitenFunktionen.Spielfeld.alle_Hausfelder_Blau[id];
                        SeitenFunktionen.Spielfeld.blaue_Figuren.Insert(this.id, this);
                        break;
                    }
            }
            Setze_Figur(Haus_position);

            //Dem Bild ein Click-Ereigniss hinzufügen
            this.bild.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(bild_Click);
        }

        private void bild_Click(object sender, MouseButtonEventArgs e)
        {
            if (this.figur_eigentümer == SeitenFunktionen.Spielfeld.aktiver_Spieler) Bewege_Figur();
        }

        public void Bewege_Figur()
        {
            if (this.bewegbar)
            {
                Setze_Figur(this.figur_eigentümer.wegstrecke[aktuelle_Wegstreckenposition + SeitenFunktionen.Spielfeld.würfelzahl]);
                Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Mitspieler;Figurklick" + ";" + farbe + ";" + id);
                this.bewegbar = false;
                SeitenFunktionen.Spielfeld.Prüfe_Spielrecht();
            }
        }

        public void Setze_Figur_ins_Haus()
        {
            this.figur_eigentümer.figurpositionen[aktuelle_Wegstreckenposition] = false;
            this.aktuelle_position = Haus_position;
            aktuelle_Wegstreckenposition = -6;
            Grid.SetColumn(bild, Haus_position.Spalte);
            Grid.SetRow(bild, Haus_position.Zeile);
        }

        //Diese Funktion Setzt die Figur auf ein belibiges Feld. Dafür nutzt sie die Zeilen und Spaltenangaben des Felds.
        public void Setze_Figur(Feld feld)
        {
            if (aktuelle_Wegstreckenposition != -6)figur_eigentümer.figurpositionen[aktuelle_Wegstreckenposition] = false;
            aktuelle_position = feld;
            if (aktuelle_position.spielfeld_art != Statische_Variablen.SPIELFELD_ART.HAUS)
            {
                aktuelle_Wegstreckenposition = figur_eigentümer.wegstrecke.IndexOf(aktuelle_position);
                figur_eigentümer.figurpositionen[aktuelle_Wegstreckenposition] = true;
            }
            else aktuelle_Wegstreckenposition = -6;

            Grid.SetColumn(bild, feld.Spalte);
            Grid.SetRow(bild, feld.Zeile);
            foreach (Figur figur in SeitenFunktionen.Spielfeld.alle_Figuren)
            {
                if (figur.aktuelle_position == this.aktuelle_position && figur != this) figur.Setze_Figur_ins_Haus();
            }
        }
    }
}
