using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mensch_ärgere_dich_nicht;
using System.Net;
using System.Threading;
using System.Windows;

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
    class Spieler
    {
        public Statische_Variablen.FARBE farbe { get; }
        public Statische_Variablen.SPIELER_ART spieler_art { get; }
        public string name { get; }
        public IPAddress ip { get; }
        public List<Figur> eigene_Figuren { get; }
        public List<Feld> wegstrecke { get; }
        public bool[] figurpositionen { get; set; }
        public Spieler(Statische_Variablen.FARBE farbe,string name, IPAddress ip)
        {
            this.name = name;
            this.farbe = farbe;
            this.wegstrecke = new List<Feld>();
            this.eigene_Figuren = new List<Figur>();
            this.figurpositionen = new bool[44];
            for (int i = 0; i < 44; i++) figurpositionen[i] = false;
            this.spieler_art = spieler_art;
            this.ip = ip;
            if (!name.Contains("CP Gegner")) this.spieler_art = Statische_Variablen.SPIELER_ART.NORMALER_SPIELER;
            else this.spieler_art = Statische_Variablen.SPIELER_ART.CP_GEGNER;
            SeitenFunktionen.Spielfeld.alle_Mitspieler.Add(this);
        }


        //Initialisieren der Wegstrecke
        public void Initialisiere_Wegstrecke()
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
                            wegstrecke[i] = SeitenFunktionen.Spielfeld.alle_Spielfelder[i];
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
                            if (i < 30) wegstrecke[i] = SeitenFunktionen.Spielfeld.alle_Spielfelder[i + 10];
                            else wegstrecke[i] = SeitenFunktionen.Spielfeld.alle_Spielfelder[i - 30];
                        }
                        wegstrecke[40] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb[0];
                        wegstrecke[41] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb[1];
                        wegstrecke[42] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb[2];
                        wegstrecke[43] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Gelb[3];
                        break;
                    }
                case Statische_Variablen.FARBE.GRÜN:
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            if (i < 20) wegstrecke[i] = SeitenFunktionen.Spielfeld.alle_Spielfelder[i + 20];
                            else wegstrecke[i] = SeitenFunktionen.Spielfeld.alle_Spielfelder[i - 20];
                        }
                        wegstrecke[40] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün[0];
                        wegstrecke[41] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün[1];
                        wegstrecke[42] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün[2];
                        wegstrecke[43] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Grün[3];
                        break;
                    }
                case Statische_Variablen.FARBE.BLAU:
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            if (i < 10) wegstrecke[i] = SeitenFunktionen.Spielfeld.alle_Spielfelder[i + 30];
                            else wegstrecke[i] = SeitenFunktionen.Spielfeld.alle_Spielfelder[i - 10];
                        }
                        wegstrecke[40] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau[0];
                        wegstrecke[41] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau[1];
                        wegstrecke[42] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau[2];
                        wegstrecke[43] = SeitenFunktionen.Spielfeld.alle_Zielfelder_Blau[3];
                        break;
                    }
            }
        }

        public void Computergegner_Runtime()
        {
            while (SeitenFunktionen.Spielfeld.spielstatus)
            {
                Thread.Sleep(750);
                if (SeitenFunktionen.Spielfeld.aktiver_Spieler == this)
                {
                    if (SeitenFunktionen.Spielfeld.versuche > 0) SeitenFunktionen.Spielfeld.spielfeld.Dispatcher.Invoke(new SeitenFunktionen.Spielfeld.void_Funtion(Künstlich_würfeln));
                    else SeitenFunktionen.Spielfeld.Gebe_Spielrecht_weiter();
                    Thread.Sleep(750);
                    if (SeitenFunktionen.Spielfeld.Prüfe_Figurbeweglichkeit() && SeitenFunktionen.Spielfeld.aktiver_Spieler.spieler_art == Statische_Variablen.SPIELER_ART.CP_GEGNER)
                    {
                        foreach (Figur figur in SeitenFunktionen.Spielfeld.aktiver_Spieler.eigene_Figuren)
                        {
                            if (figur.bewegbar)
                            {
                                SeitenFunktionen.Spielfeld.spielfeld.Dispatcher.Invoke(new SeitenFunktionen.Spielfeld.Bild_Funktion(Figur_bewegen), figur);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void Figur_bewegen(Figur figur)
        {
            figur.Bewege_Figur();
        }

        private void Künstlich_würfeln()
        {
            SeitenFunktionen.Spielfeld.BTN_Würfel.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
        }
    }
}
