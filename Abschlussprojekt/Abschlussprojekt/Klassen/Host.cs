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

        public Host(string hostname, IPAddress host_ip)
        {
            this.hostname = hostname;
            this.host_ip = host_ip;
            Statische_Variablen.alle_Hosts.Add(this);
        }
    }
}
