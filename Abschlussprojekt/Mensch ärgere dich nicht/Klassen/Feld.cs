using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensch_ärgere_dich_nicht.Klassen
{
    class Feld
    {
        public Statische_Variablen.SPIELFELD_ART spielfeld_art { get; }
        public Statische_Variablen.FARBE spielfeld_farbe { get; }
        public int Id { get; }
        public int Zeile { get; }
        public int Spalte { get; }

        public Feld(Statische_Variablen.SPIELFELD_ART spielfeld_art,int id,Statische_Variablen.FARBE spielfeld_farbe, int zeile, int spalte)
        {
            this.spielfeld_art = spielfeld_art;
            this.spielfeld_farbe = spielfeld_farbe;
            this.Id = id;
            this.Zeile = zeile;
            this.Spalte = spalte;
            Ordne_Feld_in_tabellen_ein();
        }

        // In dieser Funktion wird das Feldobjeckt in die Jeweiligen Listen eingeordnet, zum teil auch an einem Bestimmten index eingefügt.
        private void Ordne_Feld_in_tabellen_ein()
        {
            SeitenFunktionen.Spielfeld.alle_Felder.Add(this);
            switch (spielfeld_art)
            {
                case Statische_Variablen.SPIELFELD_ART.HAUS:
                    {
                        SeitenFunktionen.Spielfeld.alle_Hausfelder.Add(this);
                        switch (spielfeld_farbe)
                        {
                            case Statische_Variablen.FARBE.ROT: SeitenFunktionen.Spielfeld.alle_Hausfelder_Rot.Insert(this.Id,this);break;
                            case Statische_Variablen.FARBE.GELB: SeitenFunktionen.Spielfeld.alle_Hausfelder_Gelb.Insert(this.Id, this); break;
                            case Statische_Variablen.FARBE.GRÜN: SeitenFunktionen.Spielfeld.alle_Hausfelder_Grün.Insert(this.Id, this); break;
                            case Statische_Variablen.FARBE.BLAU: SeitenFunktionen.Spielfeld.alle_Hausfelder_Blau.Insert(this.Id, this); break;

                        }
                        break;
                    }
                case Statische_Variablen.SPIELFELD_ART.ZIEL:
                    {
                        SeitenFunktionen.Spielfeld.alle_Zielfelder.Add(this);
                        switch (spielfeld_farbe)
                        {
                            case Statische_Variablen.FARBE.ROT: SeitenFunktionen.Spielfeld.alle_Zielfelder_Rot.Insert(this.Id,this); break;
                            case Statische_Variablen.FARBE.GELB: SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb.Insert(this.Id, this); break;
                            case Statische_Variablen.FARBE.GRÜN: SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün.Insert(this.Id, this); break;
                            case Statische_Variablen.FARBE.BLAU: SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau.Insert(this.Id, this); break;

                        }
                        break;
                    }
                case Statische_Variablen.SPIELFELD_ART.SPIELFELD:
                    {
                        SeitenFunktionen.Spielfeld.alle_Spielfelder.Insert(this.Id, this);
                        break;
                    }
            }
        }
    }
}
