using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt.Klassen
{
    class Netzwerkkommunikation
    {
        public static string [] Hohle_Beitrittsinformationen()
        {
            string[] arr = new string[4] { "Horst","Marina","Günter","Werner" };//ToDo: Werte aus Datenbank hohlen
            return arr;
        }

        public static void Anlaysiere_IP_Paket(string nachricht)
        {
            if (nachricht.Contains("Hostinformationen"))
            {
                Update_Hostinformationen(nachricht);
            }
            else if (nachricht.Contains("Beitrittsinformationen"))
            {
                Update_Beitrittsinformationen(nachricht);
            }
            else if (nachricht.Contains("Spielinformationen"))
            {
                Update_Spielinformationen(nachricht);
            }
            else if (nachricht.Contains("Clientinformationen"))
            {
                Update_Clientinformationen(nachricht);
            }
            else if (nachricht.Contains("Chatinformationen")) ;
        }

        public static void Update_Hostinformationen(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Beitrittsinformationen,<ip>,<Hostname>"
            // <ip> muss folgendes Format haben,bsp ->: [...],255.255.255.255,[...]
            //
            if (nachricht.Count(x => x.Equals(',')) == 2)
            {
                string[] hostinfos = new string[2];
                string temp = nachricht.Replace("Hostinformationen,", "");
                int counter;
                int index = 0;
                for (counter = 0; counter < temp.Count(); counter++)
                {
                    if (temp[counter] != ',') hostinfos[index] += temp[counter].ToString();
                    else index++;
                }
                Host host = new Host(hostinfos[1], new System.Net.IPAddress(Konvertiere_in_Bytearray(hostinfos[0])));
            }
        }

        private static byte[] Konvertiere_in_Bytearray(string address)
        {
            byte[] ipaddress = new byte[4];
            int index = 0;
            for (int i = 0; i < address.Count(); i++)
            {
                switch (address[i])
                {
                    case '0': ipaddress[index] += 0; break;
                    case '1': break;
                    case '2': break;
                    case '3': break;
                    case '4': break;
                    case '5': break;
                    case '6': break;
                    case '7': break;
                    case '8': break;
                    case '9': break;
                    case ',': index++; break;
                }
            }
            return ipaddress;
        }

        public static void Update_Beitrittsinformationen(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Beitrittsinformationen,<Spielername>,<Spielername>,<Spielername>"
            //

            if (nachricht.Count(x => x.Equals(',')) == 4) 
            {
                string[] spielernamen = new string[4];
                string temp = nachricht.Replace("Beitrittsinformationen,", "");
                int counter;
                int index = 0;
                for (counter = 0; counter < temp.Count(); counter++)
                {
                    if (temp[counter] != ',') spielernamen[index] += temp[counter].ToString();
                    else index++;
                }
                for (int i = 0; i < 4; i++) Statische_Variablen.Beitrittslabel[i].Text = spielernamen[i]; 
            }
        }

        public static void Update_Spielinformationen(string nachricht)
        {

        }

        public static void Update_Clientinformationen(string nachricht)
        {

        }

        public static void Update_Chatinformationen(string nachricht)
        {

        }
    }
}
