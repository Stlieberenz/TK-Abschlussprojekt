using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mensch_ärgere_dich_nicht.Klassen.SeitenFunktionen
{
    static class Startseite
    {
        public static bool Überprüfe_anmeldenamen(string anmeldename)
        {
            bool result = true;

            // Algemeine bedingungen für den Anmeldenamen sind:
            // Der name muss eine länge zwischen 4 und 20 Zeichen sein und darf keine "," enthalten.
            if (anmeldename.Contains(",") || anmeldename.Length < 4 || anmeldename.Length > 20)
            {
                result = false;
                Zeige_Fehlermeldung("Der Name darf kein \",\" enthalten und muss \nzwischen 4 und 20 Zeichen lang sein.", "Algemeiner Fehler");
            }

            //Zusätzlich sind folgende Namen nicht erlaubt.
            if (anmeldename == "Offen" || anmeldename == "Ich" || anmeldename == "Geschlossen" || anmeldename == "Computergegner")
            {
                result = false;
                Zeige_Fehlermeldung("Die folgenden Namen sind nicht erlaubt:\n   Ich\n   Offen\n   Geschlossen\n   Computergegner", "Außnahmefehler");
            }
            return result;
        }

        private static void Zeige_Fehlermeldung(string Fehlermeldung, string Überschrift)
        {
            MessageBox.Show(Fehlermeldung, Überschrift, MessageBoxButton.OK);
        }
    }
}
