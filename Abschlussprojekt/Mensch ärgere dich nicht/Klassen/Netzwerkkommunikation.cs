using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using System.IO;

namespace Mensch_ärgere_dich_nicht.Klassen
{
    class Netzwerkkommunikation
    {
        // Attribute -----------------------------------------------------------------------
        public delegate bool ClientSpieler(string name);

        public delegate void Hosts_Update();

        public delegate void Ende();

        public delegate void Würfelupdate(string zahl);

        public delegate void LabelUpdate(string content);


        private static int port = 50000;
        private static IPAddress eigene_IPAddresse;
        private static List<IPAddress> broadcast_IPAdresse = new List<IPAddress>();
        private static UdpClient receivingUdpClient = new UdpClient(port);

        // Initialisierung -------------------------------------------------------------------
        public static void Iinitialisiere_IP_Addressen()
        {
            IPAddress VB_addresse = new IPAddress(new byte[] { 192, 168, 0, 1 });
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress adr in entry.AddressList)
            {
                if (adr.AddressFamily == AddressFamily.InterNetwork && adr.Address != VB_addresse.Address) //-> Die VB_Addresse muss rausgefiltert werden, da sie virtual box gehört und ich sie bisher noch nicht anders gefiltert bekomme.
                {
                    if (adr != null) eigene_IPAddresse = adr;

                }
            }
            if (eigene_IPAddresse == null) eigene_IPAddresse = new IPAddress(0);
        }

        public static void Iinitialisiere_BC_IP_Addressen()
        {
            var NetworkInfo = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection MOC = NetworkInfo.Get();
            foreach (ManagementObject mo in MOC)
            {
                IPAddress adapterAddresses = IPAddress.Parse(((string[])mo["IPAddress"])[0]);
                IPAddress adapterSubnetMasks = IPAddress.Parse(((string[])mo["IPSubnet"])[0]);
                if (adapterAddresses.GetAddressBytes().Count() > 0 && adapterSubnetMasks.GetAddressBytes().Count() > 0)
                {
                    //throw new ArgumentException("Both IP address and subnet mask should be of the same length");
                }
                var result = new byte[adapterAddresses.GetAddressBytes().Length];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (byte)(adapterAddresses.GetAddressBytes()[i] | (adapterSubnetMasks.GetAddressBytes()[i] ^ 255));
                }
                broadcast_IPAdresse.Add(new IPAddress(result));
            }
        }



        // Senden - Empfangen ---------------------------------------------------------------
        public static void Start_TCP_Listener()
        {
            try
            {
                TcpListener myListener = new TcpListener(eigene_IPAddresse, port);
                myListener.Start();
                Console.WriteLine("Warte auf eingehende requests");
                Socket s = myListener.AcceptSocket();
                byte[] b = new byte[250];
                int k = s.Receive(b);
                string message = "";
                for (int i = 0; i < k; i++)
                {
                    message += Convert.ToChar(b[i]);
                }
                Analysiere_IP_Paket(message);
                s.Close();
                myListener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
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
                Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                Analysiere_IP_Paket(Encoding.ASCII.GetString(receiveBytes));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Send_UDP_BC_Packet(string content)
        {
            foreach (IPAddress ip in broadcast_IPAdresse)
            {
                Socket sock = null;
                try
                {
                    var destinationEndpoint = new IPEndPoint(ip, port);
                    sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                    sock.SendTo(Konvertiere_string_in_Bytearray(content), destinationEndpoint);
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

        public static void Send_TCP_Packet(string content, IPAddress zieladresse)
        {
            byte[] ba = new ASCIIEncoding().GetBytes(content);
            try
            {
                TcpClient tcpclient = new TcpClient();
                tcpclient.Connect(zieladresse, port);

                Stream stm = tcpclient.GetStream();

                stm.Write(ba, 0, ba.Length);
                byte[] bb = new byte[250];
                int k = stm.Read(bb, 0, 250);
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));
                tcpclient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace + "\n" + e.Message);
            }
        }

        //public static void Sende_TCP_Nachricht_an_alle_Spieler(string nachricht)
        //{
        //    foreach (Spieler spieler in alle_Spieler)
        //    {
        //        if (spieler.spieler_art != SPIELER_ART.COMPUTERGEGNER && spieler.ip.Address != lokaler_spieler.ip.Address)
        //        {
        //            Netzwerkkommunikation.Send_TCP_Packet(nachricht, spieler.ip);
        //        }
        //    }
        //}


        // IP-Paketanalyse ----------------------------------------------------------------------

        
        public static string[] Konvertiere_in_Stringarray(string nachricht)
        {
            string[] temp = new string[20];
           
            int index = 0;
            for (int counter = 0; counter < nachricht.Count(); counter++)
            {
                if (nachricht[counter] != ';') temp[index] += nachricht[counter].ToString();
                else index++;
            }
            index = 0;
            foreach (string str in temp) if (str != null) index++;
            string[] result = new string[index];
            index = 0;
            foreach (string str in temp)
            {
                if (str != null) result[index] = str;
                index++;
            }
            return result;
        }

        private static byte[] Konvertiere_string_in_Bytearray(string message)
        {
            if (message.Count() < 65536)
            {
                ASCIIEncoding asencoding = new ASCIIEncoding();

                return asencoding.GetBytes(message);
            }
            return new byte[] { 0 };
        }

        public static void Analysiere_IP_Paket(string nachricht)
        {
            //------------------------------------------------------------------------------
            // Alle nachrichten werden nach folgendem Schema aufgebaut:
            // "für wen, was, benötigte Informationen"
            //------------------------------------------------------------------------------
            if (nachricht.Contains("Host") && Statische_Variablen.aktuelle_Seite == "Spiel_erstellen")
            {
                SeitenFunktionen.S_erstellen.Analysiere_Nachricht(Konvertiere_in_Stringarray(nachricht));
            }
            if (nachricht.Contains("Client") && Statische_Variablen.aktuelle_Seite == "Spiel_suchen")
            {
                SeitenFunktionen.S_suchen.Analysiere_Nachricht(Konvertiere_in_Stringarray(nachricht));
            }
            if (nachricht.Contains("Mitspieler") && Statische_Variablen.aktuelle_Seite == "Spielfeld")
            {
                SeitenFunktionen.Spielfeld.Analysiere_Nachricht(Konvertiere_in_Stringarray( nachricht));
            }
        }

        public static string Eigene_IP_Adresse()
        {
            return eigene_IPAddresse.ToString();
        }
    }
}
