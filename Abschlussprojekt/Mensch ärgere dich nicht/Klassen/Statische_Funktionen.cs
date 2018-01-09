using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mensch_ärgere_dich_nicht.Klassen
{
    static class Statische_Funktionen
    {
        public static string Aktuelles_Verzeichniss()
        {
            string result = Directory.GetCurrentDirectory();
            if (result.Contains("\\bin\\Debug")) return result.Replace("\\bin\\Debug", "");
            else return result;
        }
    }
}
