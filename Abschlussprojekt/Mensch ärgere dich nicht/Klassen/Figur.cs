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
        public List<Feld> wegstrecke { get; }
        public Feld aktuelle_position { get; set; }
        public Feld Haus_position { get; }
        public Image bild { get; set; }

        public Figur(Statische_Variablen.FARBE farbe, int id)
        {
            this.id = id;
            this.farbe = farbe;
            this.bild = new Image();
            this.wegstrecke = new List<Feld>();

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

            //Initialisieren der Wegstrecke
            Initialisiere_Wegstrecke();
        }

        private void bild_Click(object sender, MouseButtonEventArgs e)
        {
            //bewege den angeklickten spieler (wenn erlaubt und möglich)
            Netzwerkkommunikation.Sende_TCP_Nachricht_an_alle_Spieler("Mitspieler;Figurklick" + ";" + farbe + ";" + id);
        }

        //Diese Funktion Setzt die Figur auf ein belibiges Feld. Dafür nutzt sie die Zeilen und Spaltenangaben des Felds.
        public void Setze_Figur(Feld feld)
        {
            aktuelle_position = feld;
            Grid.SetColumn(bild, feld.Spalte);
            Grid.SetRow(bild, feld.Zeile);
        }

        //Initialisieren der Wegstrecke
        private void Initialisiere_Wegstrecke()
        {
            for (int i = 0; i < 44; i++)
            {
                wegstrecke.Add(null);
            }
            switch (this.farbe)
            {
                case Statische_Variablen.FARBE.ROT:
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            wegstrecke[i] =  SeitenFunktionen.Spielfeld.alle_Spielfelder[i];
                        }
                        wegstrecke[40] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Rot[0];
                        wegstrecke[41] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Rot[1];
                        wegstrecke[42] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Rot[2];
                        wegstrecke[43] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Rot[3];
                        break;
                    }
                case Statische_Variablen.FARBE.GELB:
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            if (i<30)wegstrecke[i] =  SeitenFunktionen.Spielfeld.alle_Spielfelder[i+10];
                            else wegstrecke[i] =  SeitenFunktionen.Spielfeld.alle_Spielfelder[i-30];
                        }
                        wegstrecke[40] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb[0];
                        wegstrecke[41] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb[1];
                        wegstrecke[42] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb[2];
                        wegstrecke[43] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb[3];
                        break;
                    }
                case Statische_Variablen.FARBE.GRÜN:
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            if (i < 20) wegstrecke[i] =  SeitenFunktionen.Spielfeld.alle_Spielfelder[i+20];
                            else wegstrecke[i] =  SeitenFunktionen.Spielfeld.alle_Spielfelder[i-20];
                        }
                        wegstrecke[40] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün[0];
                        wegstrecke[41] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün[1];
                        wegstrecke[42] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün[2];
                        wegstrecke[43] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün[3];
                        break;
                    }
                case Statische_Variablen.FARBE.BLAU:
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            if (i < 10) wegstrecke[i] =  SeitenFunktionen.Spielfeld.alle_Spielfelder[i+30];
                            else wegstrecke[i] =  SeitenFunktionen.Spielfeld.alle_Spielfelder[i-10];
                        }
                        wegstrecke[40] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau[0];
                        wegstrecke[41] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau[1];
                        wegstrecke[42] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau[2];
                        wegstrecke[43] =  SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau[3];
                        break;
                    }
            }
        }
    }
}
