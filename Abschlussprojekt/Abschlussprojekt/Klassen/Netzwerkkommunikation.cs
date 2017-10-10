using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Management;
using System.Threading;

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
            // Die Informationen müssen folgendes Schema haben: "Hostinformationen,<ip>,<Hostname>"
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
                Host host = new Host(hostinfos[1], new IPAddress(Konvertiere_in_Bytearray(hostinfos[0])));
            }
        }

        private static byte[] Konvertiere_in_Bytearray(string address)
        {
            string [] ipaddress = new string[4];
            int index = 0;
            for (int i = 0; i < address.Count(); i++)
            {
                switch (address[i])
                {
                    case '0': ipaddress[index] += "0"; break;
                    case '1': ipaddress[index] += "1"; break;
                    case '2': ipaddress[index] += "2"; break;
                    case '3': ipaddress[index] += "3"; break;
                    case '4': ipaddress[index] += "4"; break;
                    case '5': ipaddress[index] += "5"; break;
                    case '6': ipaddress[index] += "6"; break;
                    case '7': ipaddress[index] += "7"; break;
                    case '8': ipaddress[index] += "8"; break;
                    case '9': ipaddress[index] += "9"; break;
                    case '.': index++; break;
                }
            }
            return new byte[4] {Convert.ToByte(ipaddress[0]) , Convert.ToByte(ipaddress[1]), Convert.ToByte(ipaddress[2]), Convert.ToByte(ipaddress[3]) };
        }

        private static byte[] Konvertiere_string_in_Bytearray(string message)
        {
            if (message.Count() < 65536)
            {
                ASCIIEncoding asencoding = new ASCIIEncoding();

                return asencoding.GetBytes(message) ;
            }
            return new byte[] { 0 };
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

        public static void Iinitialisiere_IP_Addresse()
        {
            byte[] arr = Konvertiere_in_Bytearray(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString());
            Statische_Variablen.eigene_IPAddresse = new IPAddress(arr);
        }

        public static void Start_TCP_Listener()
        {
            TcpListener myListener = new TcpListener(Statische_Variablen.eigene_IPAddresse, 50000);
            myListener.Start();
            try
            {
                
                Console.WriteLine("Warte auf eingehende requests");
                Socket s = myListener.AcceptSocket();
                byte[] b = new byte[100];
                int k = s.Receive(b);
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(b[i]));
                s.Close();
                myListener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
                myListener.Stop();
            }
        }

        public static void Start_UDP_Listener()
        {
            //Creates an IPEndPoint to record the IP Address and port number of the sender. 
            // The IPEndPoint will allow you to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = Statische_Variablen.receivingUdpClient.Receive(ref RemoteIpEndPoint);

                Anlaysiere_IP_Paket( Encoding.ASCII.GetString(receiveBytes));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SendBroadcastPacket( string content)
        {
            var NetworkInfo = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection MOC = NetworkInfo.Get();
            foreach (ManagementObject mo in MOC)
            {
                var adapterAddresses = (string[])mo["IPAddress"];
                var adapterSubnetMasks = (string[])mo["IPSubnet"];
                if (adapterAddresses.Count() > 0 && adapterSubnetMasks.Count() > 0)
                {
                    try
                    {
                        IPAddress broadcastIpForAdapter = GetBroadcastAddress(IPAddress.Parse(adapterAddresses[0]),
                                                                              IPAddress.Parse(adapterSubnetMasks[0]));
                        SendBroadcastPacketToBroadcastIp(broadcastIpForAdapter, Statische_Variablen.port, Konvertiere_string_in_Bytearray(content));
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private static IPAddress GetBroadcastAddress(IPAddress ipAddress, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = ipAddress.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();
            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Both IP address and subnet mask should be of the same length");
            var result = new byte[ipAdressBytes.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            return new IPAddress(result);
        }

        private static void SendBroadcastPacketToBroadcastIp(IPAddress broadcastIp, int destinationPort, byte[] content)
        {
            Socket sock = null;
            try
            {
                var destinationEndpoint = new IPEndPoint(broadcastIp, destinationPort);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                sock.SendTo(content, destinationEndpoint);
            }
            finally
            {
                if (sock != null)
                {
                    sock.Close();
                }
            }
        }
    }
}
