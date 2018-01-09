using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Controls;

namespace Mensch_ärgere_dich_nicht.Klassen.SeitenFunktionen
{
    public delegate void Servername(string[] Name);
    static class S_suchen
    {
        public static ListBox Server_Liste = null;
        public static void Analysiere_Nachricht(string[] content)
        {
            Servername servername_=Liste_Füllen;

            if (content[0] == "client")
            {
                Server_Liste.Dispatcher.Invoke(new Servername(servername_), content);
            }            
        }
        public static void Liste_Füllen(string[] servername)
        {
            Server_Liste.Items.Add(servername);
        }
    }
}
