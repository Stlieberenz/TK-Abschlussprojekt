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
                //Anlaysiere_IP_Paket(message);
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

                //Anlaysiere_IP_Paket(Encoding.ASCII.GetString(receiveBytes));
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

        
        private static string[] Konvertiere_in_Stringarray(string nachricht, int array_länge)
        {
            string[] result = new string[array_länge + 1];
            int counter;
            int index = 0;
            for (counter = 0; counter < nachricht.Count(); counter++)
            {
                if (nachricht[counter] != ',') result[index] += nachricht[counter].ToString();
                else index++;
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

        //public static void Anlaysiere_IP_Paket(string nachricht)
        //{

        //    if (nachricht.Contains("Hostinformationen") && aktive_Seite == AKTIVE_SEITE.SPIEL_SUCHEN)
        //    {
        //        string temp = nachricht.Replace("Hostinformationen,", "");
        //        Update_Hostinformationen(temp);
        //    }
        //    else if (nachricht.Contains("Hostabsage") && aktive_Seite == AKTIVE_SEITE.SPIEL_SUCHEN)
        //    {
        //        anfragen_result = false;
        //    }
        //    else if (nachricht.Contains("Hostzusage") && aktive_Seite == AKTIVE_SEITE.SPIEL_SUCHEN)
        //    {
        //        anfragen_result = true;
        //    }
        //    else if (nachricht.Contains("Spielstart") && aktive_Seite == AKTIVE_SEITE.SPIEL_SUCHEN)
        //    {
        //        string temp = nachricht.Replace("Spielstart,", "");
        //        Spielstart(temp);
        //        Spiel_suchen_Grid.Dispatcher.Invoke(new Hosts_Update(Spiel_suchen_Spiel_starten));
        //    }
        //    else if (nachricht.Contains("Spielende") && aktive_Seite == AKTIVE_SEITE.SPIELWIESE)
        //    {
        //        string temp = nachricht.Replace("Spielende,", "");
        //        Statische_Methoden.Spielende();
        //        Aufgeben.Dispatcher.Invoke(new Ende(Statische_Methoden.Spielende));
        //    }
        //    else if (nachricht.Contains("Clientanfrage") && aktive_Seite == AKTIVE_SEITE.SPIEL_ERSTELLEN)
        //    {
        //        string temp = nachricht.Replace("Clientanfrage,", "");
        //        Clientanfrage(temp);
        //    }
        //    else if (nachricht.Contains("Clientabsage") && aktive_Seite == AKTIVE_SEITE.SPIEL_ERSTELLEN)
        //    {
        //        string temp = nachricht.Replace("Clientabsage,", "");
        //        Clientabsage(temp);
        //    }
        //    else if (nachricht.Contains("Chatinformationen") && aktive_Seite == AKTIVE_SEITE.SPIELWIESE)
        //    {
        //        string temp = nachricht.Replace("Chatinformationen,", "");
        //        Update_Chatinformationen(temp);
        //    }
        //    else if (nachricht.Contains("Spielrecht") && aktive_Seite == AKTIVE_SEITE.SPIELWIESE)
        //    {
        //        string temp = nachricht.Replace("Spielrecht,", "");
        //        if (nachricht.Contains(lokaler_spieler.name))
        //        {
        //            lokaler_spieler.status = true;
        //            Würfel.Dispatcher.Invoke(new Hosts_Update(Aktiviere_Würfel));
        //            verbleibende_würfelversuche = 3;
        //        }
        //        aktiver_Spieler.Dispatcher.Invoke(new LabelUpdate(LabelUpdate_aktiver_Spieler), temp);
        //    }
        //    else if (nachricht.Contains("Spielfigur Update") && aktive_Seite == AKTIVE_SEITE.SPIELWIESE)
        //    {
        //        string temp = nachricht.Replace("Spielfigur Update,", "");
        //        Spielfigur_Update(temp);
        //    }
        //    else if (nachricht.Contains("Wuerfelzahl") && aktive_Seite == AKTIVE_SEITE.SPIELWIESE)
        //    {
        //        string temp = nachricht.Replace("Wuerfelzahl,", "");
        //        Würfel.Dispatcher.Invoke(new Würfelupdate(Würfel_update), nachricht.Last().ToString());
        //    }
        //    else if (nachricht.Contains("Client_abbruch"))
        //    {
        //        string temp = nachricht.Replace("Client_abbruch,", "");
        //        int index = -1;
        //        foreach (Spieler sp in alle_Spieler)
        //        {
        //            if (sp.name == temp) index = alle_Spieler.IndexOf(sp);
        //        }
        //        foreach (Figur fig in alle_Spieler[index].eigene_Figuren)
        //        {
        //            fig.Set_Figure_to_Start();
        //        }

        //        if (index > -1) alle_Spieler.RemoveAt(index);
        //    }
        //}

        public static string Eigene_IP_Adresse()
        {
            return eigene_IPAddresse.ToString();
        }
    }
}
