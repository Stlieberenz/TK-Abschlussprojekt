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

        public static void Iinitialisiere_IP_Addressen()
        {
            IPAddress VB_addresse = new IPAddress(new byte[] { 192, 168, 0, 1 });
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress adr in entry.AddressList)
            {
                if (adr.AddressFamily == AddressFamily.InterNetwork && adr.Address != VB_addresse.Address) //-> Die VB_Addresse muss rausgefiltert werden, da sie virtual box gehört und ich sie bisher noch nicht anders gefiltert bekomme.
                {
                    eigene_IPAddresse = adr;
                }
            }
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

        public static void Update_Hostinformationen(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Hostinformationen,<ip>,<Hostname>,<offene slots>,<offen farben>,<Spieler rot>,<Spieler gelb>,<Spieler gruen>,<Spieler blau>"
            // <ip> muss folgendes Format haben,bsp ->: [...],255.255.255.255,[...]
            //
            // |    #Kranker scheiß     |
            //\ /                      \ /
            if (nachricht.Count(x => x.Equals(',')) == 7)
            {
                string[] hostinfos = Konvertiere_in_Stringarray(nachricht,7);
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
                                hosts.Dispatcher.Invoke(new Hosts_Update(Hosts_ListBox_aktualisieren));
                            }
                        }
                    }
                    else
                    {
                        known_IP_S.Add(hostinfos[0]);
                        Host host = new Host(hostinfos[1],IPAddress.Parse(hostinfos[0]), Convert.ToInt32(hostinfos[2]));
                        hosts.Dispatcher.Invoke(new Hosts_Update(Hosts_ListBox_aktualisieren));
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
                        hosts.Dispatcher.Invoke(new Hosts_Update(Hosts_ListBox_aktualisieren));
                    }
                }
            }
        }

        public static void Update_Chatinformationen(string nachricht)
        {

        }

        public static void Clientanfrage(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Clientanfrage,<Spielername>,<ip>,<farbe>"
            //
            if (nachricht.Count(x => x.Equals(',')) == 2)
            {
                string[] clientinfo = Konvertiere_in_Stringarray(nachricht,3);
                IPAddress ip = IPAddress.Parse(clientinfo[1]);
                foreach (Spieler spieler in alle_Spieler)// Prüfen ob der spieler evtl schon vorhanden ist.
                {
                    if (spieler.ip.Address == ip.Address)
                    {
                        Task.Factory.StartNew((absagen) => { Send_TCP_Packet("Hostabsage", ip); }, ip);
                        return;
                    }
                }

                object result = false;
                FARBE clientfarbe = FARBE.LEER;

                switch (clientinfo[2])
                {
                    case "rot":
                        {
                            result = Spielerstellenlabel[0].Dispatcher.Invoke(new ClientSpieler(Prüfe_Label_rot), clientinfo[0]);
                            clientfarbe = FARBE.ROT;
                            break;
                        }
                    case "gelb":
                        {
                            result = Spielerstellenlabel[1].Dispatcher.Invoke(new ClientSpieler(Prüfe_Label_gelb), clientinfo[0]);
                            clientfarbe = FARBE.GELB;
                            break;
                        }
                    case "gruen":
                        {
                            result = Spielerstellenlabel[2].Dispatcher.Invoke(new ClientSpieler(Prüfe_Label_gruen), clientinfo[0]);
                            clientfarbe = FARBE.GRUEN;
                            break;
                        }
                    case "blau":
                        {
                            result = Spielerstellenlabel[3].Dispatcher.Invoke(new ClientSpieler(Prüfe_Label_blau), clientinfo[0]);
                            clientfarbe = FARBE.BLAU;
                            break;
                        }
                }
                 // Prüft ob der slot frei ist und fügt namen dem label hinzu

                if (Convert.ToBoolean(result))
                {
                    new Spieler(clientfarbe, clientinfo[0], SPIELER_ART.NORMALER_SPIELER, ip);
                    Task.Factory.StartNew((Zusagen) => { Send_TCP_Packet("Hostzusage", ip); },ip);
                }
                else Task.Factory.StartNew((absagen) => { Send_TCP_Packet("Hostabsage", ip); }, ip);
            }
        }

        public static void Clientabsage(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Clientabsage,<ip>"
            //
            if (nachricht.Count(x => x.Equals(',')) == 0)
            {
                string clientinfo = nachricht.Replace("Clientabsage,", "");

                IPAddress ip = IPAddress.Parse(clientinfo);
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

        public static void Spielstart(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Spielstart,<Name_S1>,<ip_S1>,<farbe_S1>,<Name_S2>,<ip_S2>,<farbe_S2>,<Name_S3>,<ip_S3>,<farbe_S3>,<Name_S4>,<ip_S4>,<farbe_S4>,<start_farbe>"
            // 
            string[] Spieler = Konvertiere_in_Stringarray(nachricht, 12);

            if (Spieler[0] != "Geschlossen") new Spieler(Statische_Methoden.Erkenne_Farbe(Spieler[2]), Spieler[0], Statische_Methoden.Erkenne_Spielerart(Spieler[0]), IPAddress.Parse(Spieler[1]));
            if (Spieler[3] != "Geschlossen") new Spieler(Statische_Methoden.Erkenne_Farbe(Spieler[5]), Spieler[3], Statische_Methoden.Erkenne_Spielerart(Spieler[3]), IPAddress.Parse(Spieler[4]));
            if (Spieler[6] != "Geschlossen") new Spieler(Statische_Methoden.Erkenne_Farbe(Spieler[8]), Spieler[6], Statische_Methoden.Erkenne_Spielerart(Spieler[6]), IPAddress.Parse(Spieler[7]));
            if (Spieler[9] != "Geschlossen") new Spieler(Statische_Methoden.Erkenne_Farbe(Spieler[11]), Spieler[9], Statische_Methoden.Erkenne_Spielerart(Spieler[9]), IPAddress.Parse(Spieler[10]));
            foreach(Spieler spieler in alle_Spieler)
            {
                if (spieler.farbe == Statische_Methoden.Erkenne_Farbe(Spieler[12]))
                {
                    spieler.status = true;
                }
            }
        }

        public static void Spielfigur_Update(string nachricht)
        {
            //
            // Die Informationen müssen folgendes Schema haben: "Spielfigur Update,<Farbe>,<Figur Nr>,<zielfeld_Position_X>,<zielfeldposition_Y>"
            //
            string[] informationen = Konvertiere_in_Stringarray(nachricht, 4);
            int x = Convert.ToInt32(informationen[2]);
            int y = Convert.ToInt32(informationen[3]);
            
            switch (Statische_Methoden.Erkenne_Farbe(informationen[0]))
            {
                case FARBE.ROT:
                    {
                        foreach (Figur figur in spieler_rot)
                        {
                            if (figur.id == Convert.ToInt32(informationen[1])) figur.Set_Figureposition(Statische_Methoden.Finde_Feld(x, y));
                        }
                        break;
                    }
                case FARBE.GELB:
                    {
                        foreach (Figur figur in spieler_gelb)
                        {
                            if (figur.id == Convert.ToInt32(informationen[1])) figur.Set_Figureposition(Statische_Methoden.Finde_Feld(x, y));
                        }
                        break;
                    }
                case FARBE.GRUEN:
                    {
                        foreach (Figur figur in spieler_gruen)
                        {
                            if (figur.id == Convert.ToInt32(informationen[1])) figur.Set_Figureposition(Statische_Methoden.Finde_Feld(x, y));
                        }
                        break;
                    }
                case FARBE.BLAU:
                    {
                        foreach (Figur figur in spieler_blau)
                        {
                            if (figur.id == Convert.ToInt32(informationen[1])) figur.Set_Figureposition(Statische_Methoden.Finde_Feld(x, y));
                        }
                        break;
                    }
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
                byte[] b = new byte[250];
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

        public static void Send_UDP_BC_Packet(string content, IPAddress target_ip)
        {

            Socket sock = null;
            try
            {
                var destinationEndpoint = new IPEndPoint(target_ip, port);
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

        public static void Send_TCP_Packet(string content,IPAddress zieladresse)
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

        public static void Sende_TCP_Nachricht_an_alle_Spieler(string nachricht)
        {
            foreach (Spieler spieler in alle_Spieler)
            {
                if (spieler.spieler_art != SPIELER_ART.COMPUTERGEGNER && spieler.ip.Address != lokaler_spieler.ip.Address)
                {
                    Netzwerkkommunikation.Send_TCP_Packet(nachricht, spieler.ip);
                }
            }
        }



        //      Relativ trivialer Kram
        //       ||               ||
        //       \/               \/  

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

        private static string[] Konvertiere_in_Stringarray(string nachricht, int array_länge)
        {
            string[] result = new string[array_länge +1];
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

        public static void Anlaysiere_IP_Paket(string nachricht)
        {
           
            if (nachricht.Contains("Hostinformationen")&& aktive_Seite == AKTIVE_SEITE.SPIEL_SUCHEN)
            {
                string temp = nachricht.Replace("Hostinformationen,", "");
                Update_Hostinformationen(temp);
            }
            else if(nachricht.Contains("Hostabsage") && aktive_Seite == AKTIVE_SEITE.SPIEL_SUCHEN)
            {
                anfragen_result = false;
            }
            else if(nachricht.Contains("Hostzusage") && aktive_Seite == AKTIVE_SEITE.SPIEL_SUCHEN)
            {
                anfragen_result = true;
            }
            else if(nachricht.Contains("Spielstart") && aktive_Seite == AKTIVE_SEITE.SPIEL_SUCHEN)
            {
                string temp = nachricht.Replace("Spielstart,", "");
                Spielstart(temp);
                Spiel_suchen_Grid.Dispatcher.Invoke(new Hosts_Update(Spiel_suchen_Spiel_starten));
            }
            else if (nachricht.Contains("Clientanfrage") && aktive_Seite == AKTIVE_SEITE.SPIEL_ERSTELLEN)
            {
                string temp = nachricht.Replace("Clientanfrage,", "");
                Clientanfrage(temp);
            }
            else if (nachricht.Contains("Clientabsage") && aktive_Seite == AKTIVE_SEITE.SPIEL_ERSTELLEN)
            {
                string temp = nachricht.Replace("Clientabsage,", "");
                Clientabsage(temp);
            }
            else if (nachricht.Contains("Chatinformationen") && aktive_Seite == AKTIVE_SEITE.SPIELWIESE)
            {
                string temp = nachricht.Replace("Chatinformationen,", "");
                Update_Chatinformationen(temp);
            }
            else if (nachricht.Contains("Spielrecht") && aktive_Seite == AKTIVE_SEITE.SPIELWIESE)
            {
                lokaler_spieler.status = true;
                Würfel.Dispatcher.Invoke(new Hosts_Update(Aktiviere_Würfel));
                verbleibende_würfelversuche = 3;
            }
            else if (nachricht.Contains("Spielfigur Update") && aktive_Seite == AKTIVE_SEITE.SPIELWIESE)
            {
                string temp = nachricht.Replace("Spielfigur Update,", "");
                Spielfigur_Update(temp);
            }

        }

        public static void Spiel_suchen_Spiel_starten()
        {
            Spiel_suchen_Grid.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.GridViewRowPresenterBase.LostFocusEvent));
        }

        public static void Aktiviere_Würfel()
        {
            Würfel.IsEnabled = true;
        }

        private static void Hosts_ListBox_aktualisieren()
        {
            hosts.Items.Clear();
            foreach (Host host in alle_Hosts)
            {
                hosts.Items.Add(host.hostname + " --- Freie plätze:" + host.freie_plätze.ToString());
                hosts.RaiseEvent(new System.Windows.RoutedEventArgs(System.Windows.Controls.Primitives.TextBoxBase.SelectionChangedEvent));
            }
        }

        public static IPAddress GetBroadcastAddress(IPAddress ipAddress, IPAddress subnetMask)
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
    }
}
