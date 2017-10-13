using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Management;
using System.Threading;
using static Abschlussprojekt.Klassen.Statische_Variablen;
using System.IO;
using System.Windows.Controls;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;

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
        public delegate bool ClientSpieler(string name);

        public delegate void Hosts_Update();

        public static void Iinitialisiere_IP_Addresse()
        {
            IPAddress VB_addresse = new IPAddress(new byte[] { 192, 168, 0, 1 });
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress adr in entry.AddressList)
            {
                if (adr.AddressFamily == AddressFamily.InterNetwork && adr.Address != VB_addresse.Address ) //-> Die VB_Addresse muss rausgefiltert werden, da sie virtual box gehört und ich sie bisher noch nicht anders gefiltert bekomme.
                {
                    eigene_IPAddresse = adr;
                }
            }
        }

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
            if (nachricht.Contains("Hostabsage"))
            {
                anfragen_result = false;
            }
            if (nachricht.Contains("Hostzusage"))
            {
                anfragen_result = true;
            }
            else if (nachricht.Contains("Clientanfrage"))
            {
                Clientanfrage(nachricht);
            }
            else if (nachricht.Contains("Clientabsage"))
            {
                Clientabsage(nachricht);
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

        private static void Hostzusage(string nachricht)
        {
            
        }

        public static void Update_Hostinformationen(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Hostinformationen,<ip>,<Hostname>,<offene slots>,<offen farben>,<Spieler rot>,<Spieler gelb>,<Spieler gruen>,<Spieler blau>"
            // <ip> muss folgendes Format haben,bsp ->: [...],255.255.255.255,[...]
            //
            // |    #Kranker scheiß     |
            //\ /                      \ /
            if (nachricht.Count(x => x.Equals(',')) == 8)
            {
                string[] hostinfos = Konvertiere_in_Stringarray(nachricht,8);
                IPAddress host_addres = IPAddress.Parse(hostinfos[0]);
                if (hostinfos[1] != "absage") {
                    if (known_IP_S.Contains(hostinfos[0]))
                    {
                        foreach (Host host_ in alle_Hosts)
                        {
                            if (host_.host_ip.Address == host_addres.Address && (host_.freie_plätze != Convert.ToInt32(hostinfos[2]) || host_.Spieler_rot != hostinfos[4] || host_.Spieler_gelb != hostinfos[5] || host_.Spieler_gruen != hostinfos[6] || host_.Spieler_blau != hostinfos[7]))
                            {
                                alle_Hosts[alle_Hosts.IndexOf(host_)].freie_plätze = Convert.ToInt32(hostinfos[2]);
                                alle_Hosts[alle_Hosts.IndexOf(host_)].freie_farben = Convert.ToInt32(hostinfos[3]);
                                alle_Hosts[alle_Hosts.IndexOf(host_)].Spieler_rot = hostinfos[4];
                                alle_Hosts[alle_Hosts.IndexOf(host_)].Spieler_gelb = hostinfos[5];
                                alle_Hosts[alle_Hosts.IndexOf(host_)].Spieler_gruen = hostinfos[6];
                                alle_Hosts[alle_Hosts.IndexOf(host_)].Spieler_blau = hostinfos[7];
                                hosts.Dispatcher.Invoke(new Hosts_Update(Updater));
                            }
                        }
                    }
                    else
                    {
                        known_IP_S.Add(hostinfos[0]);
                        Host host = new Host(hostinfos[1], new IPAddress(Konvertiere_in_Bytearray(hostinfos[0])), Convert.ToInt32(hostinfos[2]));
                        hosts.Dispatcher.Invoke(new Hosts_Update(Updater));
                    }
                }
                else
                {
                    int result = -1;
                    foreach (Host host_ in alle_Hosts)
                    {
                        if (host_.host_ip.Address == host_addres.Address)
                        {
                            result = alle_Hosts.IndexOf(host_);
                        }
                    }
                    if (result > -1)
                    {
                        alle_Hosts.RemoveAt(result);
                        hosts.Dispatcher.Invoke(new Hosts_Update(Updater));
                    }
                }
            }
        }

        private static void Updater()// Macht nix weiter als die Liste mit Hosts zu aktualisieren.
        {
            hosts.Items.Clear();
            foreach (Host host in Statische_Variablen.alle_Hosts)
            {
                hosts.Items.Add(host.hostname + " --- Freie plätze:" + host.freie_plätze.ToString());
                hosts.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.TextBoxBase.SelectionChangedEvent));
            }
        }

        private static string[] Konvertiere_in_Stringarray(string nachricht, int array_länge)
        {
            string[] result = new string[array_länge];
            string temp = nachricht.Replace("Hostinformationen,", "");
            int counter;
            int index = 0;
            for (counter = 0; counter < temp.Count(); counter++)
            {
                if (temp[counter] != ',') result[index] += temp[counter].ToString();
                else index++;
            }
            return result;
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
                for (int i = 0; i < 4; i++) Beitrittslabel[i].Text = spielernamen[i]; 
            }
        }

        public static void Hostabsage(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Hostabsage"
            //

            //ToDo:
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

        public static void Clientanfrage(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Clientanfrage,<Spielername>,<ip>,<farbe>"
            //
            if (nachricht.Count(x => x.Equals(',')) == 3)
            {
                string[] clientinfo = new string[3];
                string temp = nachricht.Replace("Clientanfrage,", "");
                int counter;
                int index = 0;
                for (counter = 0; counter < temp.Count(); counter++)
                {
                    if (temp[counter] != ',') clientinfo[index] += temp[counter].ToString();
                    else index++;
                }
                IPAddress ip = new IPAddress(Konvertiere_in_Bytearray(clientinfo[1]));
                foreach(Spieler spieler in alle_Spieler)// Prüfen ob der spieler evtl schon vorhanden ist.
                {
                    if (spieler.ip == ip)
                    {
                        //Send absage 
                        return;
                    }
                }

                FARBE clientfarbe = FARBE.LEER;
                switch (clientinfo[2])
                {
                    case "rot": clientfarbe = FARBE.ROT; break;
                    case "gelb": clientfarbe = FARBE.GELB; break;
                    case "gruen": clientfarbe = FARBE.GRUEN; break;
                    case "blau": clientfarbe = FARBE.BLAU; break;
                }
                object result = false;
                switch (clientfarbe)
                {
                    case FARBE.ROT:
                        {
                            result = Spielerstellenlabel[0].Dispatcher.Invoke(new ClientSpieler(Prüfe_Label_rot), clientinfo[0]);
                            break;
                        }
                    case FARBE.GELB:
                        {
                            result = Spielerstellenlabel[1].Dispatcher.Invoke(new ClientSpieler(Prüfe_Label_gelb), clientinfo[0]);
                            break;
                        }
                    case FARBE.GRUEN:
                        {
                            result = Spielerstellenlabel[2].Dispatcher.Invoke(new ClientSpieler(Prüfe_Label_gruen), clientinfo[0]);
                            break;
                        }
                    case FARBE.BLAU:
                        {
                            result = Spielerstellenlabel[3].Dispatcher.Invoke(new ClientSpieler(Prüfe_Label_blau), clientinfo[0]);
                            break;
                        }
                } // Prüft ob der slot frei ist und fügt namen dem label hinzu

                if (Convert.ToBoolean(result) == true)
                {
                    alle_Spieler.Add(new Spieler(clientfarbe, clientinfo[0], SPIELER_ART.NORMALER_SPIELER, ip));
                    Task.Factory.StartNew((Zusagen) => { Send_TCP_Packet("Hostzusage", ip); },ip);
                }
                else Task.Factory.StartNew((absagen) => { Send_TCP_Packet("Hostabsage", ip); }, ip);
            }
        }

        public static bool Prüfe_Label_rot(string name)
        {
            if (Spielerstellenlabel[0].Text == "Offen") Spielerstellenlabel[0].Text = name;
            else return false;
            return true;
        }
        public static bool Prüfe_Label_gelb(string name)
        {
            if (Spielerstellenlabel[1].Text == "Offen") Spielerstellenlabel[1].Text = name;
            else return false;
            return true;
        }
        public static bool Prüfe_Label_gruen(string name)
        {
            if (Spielerstellenlabel[2].Text == "Offen") Spielerstellenlabel[2].Text = name;
            else return false;
            return true;
        }
        public static bool Prüfe_Label_blau(string name)
        {
            if (Spielerstellenlabel[3].Text == "Offen") Spielerstellenlabel[3].Text = name;
            else return false;
            return true;
        }

        public static bool Setze_Label_rot_auf_Offen(string offen)
        {
            Spielerstellenlabel[0].Text = offen;
            return true;
        }
        public static bool Setze_Label_gelb_auf_Offen(string offen)
        {
            Spielerstellenlabel[1].Text = offen;
            return true;
        }
        public static bool Setze_Label_gruen_auf_Offen(string offen)
        {
            Spielerstellenlabel[2].Text = offen;
            return true;
        }
        public static bool Setze_Label_blau_auf_Offen(string offen)
        {
            Spielerstellenlabel[3].Text = offen;
            return true;
        }



        public static void Clientabsage(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Clientabsage,<ip>"
            //
            if (nachricht.Count(x => x.Equals(',')) == 1)
            {
                
                string clientinfo = nachricht.Replace("Clientabsage,", "");
                
                IPAddress ip = new IPAddress(Konvertiere_in_Bytearray(clientinfo));
                Spieler spiele = null;
                foreach (Spieler spieler in alle_Spieler)
                {
                    if (spieler.ip.Address == ip.Address)
                    {
                        spiele = spieler;
                    }
                }
                switch (spiele.farbe)
                {
                    case FARBE.ROT:
                        {
                            Spielerstellenlabel[0].Dispatcher.Invoke(new ClientSpieler(Setze_Label_rot_auf_Offen), "Offen");
                            break;
                        }
                    case FARBE.GELB:
                        {
                            Spielerstellenlabel[1].Dispatcher.Invoke(new ClientSpieler(Setze_Label_gelb_auf_Offen), "Offen");
                            break;
                        }
                    case FARBE.GRUEN:
                        {
                            Spielerstellenlabel[2].Dispatcher.Invoke(new ClientSpieler(Setze_Label_gruen_auf_Offen), "Offen");
                            break;
                        }
                    case FARBE.BLAU:
                        {
                            Spielerstellenlabel[3].Dispatcher.Invoke(new ClientSpieler(Setze_Label_blau_auf_Offen), "Offen");
                            break;
                        }
                } // fügt "Offen" dem label hinzu
                if (spiele != null) alle_Spieler.Remove(spiele);
            }
        }

        public static void Start_TCP_Listener()
        {
            
            try
            {
                TcpListener myListener = new TcpListener(eigene_IPAddresse, port);
                myListener.Start();
                Console.WriteLine("Warte auf eingehende requests");
                Socket s = myListener.AcceptSocket();
                byte[] b = new byte[100];
                int k = s.Receive(b);
                string message = "";
                for (int i = 0; i < k; i++)
                {
                    message += Convert.ToChar(b[i]);
                }
                Anlaysiere_IP_Paket(message);
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
                        SendBroadcastPacketToBroadcastIp(broadcastIpForAdapter, port, Konvertiere_string_in_Bytearray(content));
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                }
            }
        }

        public static void Send_TCP_Packet(string content,IPAddress zieladresse)
        {
            byte[] ba = new ASCIIEncoding().GetBytes(content);
            try
            {
                TcpClient tcpclient = new TcpClient();
                tcpclient.Connect(zieladresse, port);

                Stream stm = tcpclient.GetStream();

                stm.Write(ba, 0, ba.Length);
                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));
                tcpclient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace + "\n" + e.Message);
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
