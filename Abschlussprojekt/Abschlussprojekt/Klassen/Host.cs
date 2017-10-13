using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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
    class Host
    {
        public string hostname { get; }
        public IPAddress host_ip { get; }
        public int freie_plätze { get; set; }
        public int freie_farben { get; set; }
        public string Spieler_rot { get; set; }
        public string Spieler_gelb { get; set; }
        public string Spieler_gruen { get; set; }
        public string Spieler_blau { get; set; }

        public Host(string hostname, IPAddress host_ip)
        {
            this.hostname = hostname;
            this.host_ip = host_ip;
            this.freie_plätze = 0;
            Statische_Variablen.alle_Hosts.Add(this);
        }
        public Host(string hostname, IPAddress host_ip,int freie_plätze)
        {
            this.hostname = hostname;
            this.host_ip = host_ip;
            this.freie_plätze = freie_plätze;
            Statische_Variablen.alle_Hosts.Add(this);
        }
    }
}
