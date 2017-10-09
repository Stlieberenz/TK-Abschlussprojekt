using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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
            Statische_Variablen.hosts.Items.Add(hostname);
        }
    }
}
