using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mensch_ärgere_dich_nicht
{
    public static class Statische_Variablen
    {
        public static MainWindow mainWindow = new MainWindow();
        public static Frame rootFrame = new Frame();
        public static string lokaler_Spieler;
        public static string aktuelle_Seite;
        public enum FARBE
        {
            ROT,
            GELB,
            GRÜN,
            BLAU,
            NULL
        }
        public enum SPIELER_ART
        {
            NORMALER_SPIELER,
            CP_GEGNER
        }
        public enum SPIELFELD_ART
        {
            HAUS,
            SPIELFELD,
            ZIEL
        }

    }
}
