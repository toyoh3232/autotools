using System;
using System.Linq;
using System.Net;
using SmallDHCPServer;

namespace IPShareSet
{
    public class DHCPServer
    {
        public delegate void AnnouncedEventHandler(DhcpClientSettings data);
        public delegate void ReleasedEventHandler();
        public delegate void RequestEventHandler(DhcpClientSettings data);
        public delegate void AssignedEventHandler(string ipAdd);

        public event AnnouncedEventHandler Announced;
        public event RequestEventHandler Requested;

        private UdpListener udpListener; // the udp snd/rcv class


        public bool IsAuto = true;

        public DhcpServerSettings Settings { get; }

        public DhcpClientSettings[] clientSettingsPool;

        public DhcpClientSettings GetRandomClientSettings()
        {
            for (var i=0; i< clientSettingsPool.Length; i++)
            {
                if (!clientSettingsPool[i].IsAllocated)
                {
                    clientSettingsPool[i].IsAllocated = true;
                    return clientSettingsPool[i];
                }   
            }
            // TODO
            throw new Exception();
        }

        public DHCPServer(DhcpServerSettings setting)
        {
            Settings = setting;
            clientSettingsPool = Utils.GetAllSubnetIPv4(setting.MyIP, setting.SubMask).Select(ip => new DhcpClientSettings {IPAddr = ip, IsAllocated = false}).ToArray();
        }

        ~DHCPServer()
        {
            udpListener?.StopListener();
        }

        // function to start the DHCP server
        // port 67 to receive, 68 to send
        public void StartDHCPServer()
        {
            try
            {   // start the DHCP server
                // assign the event handlers
                udpListener = new UdpListener(67, 68, Settings.MyIP);
                udpListener.Reveived += UDPListener_Received;

            }
            catch (Exception e)
            {
                Console.WriteLine("StartDHCPServer:" + e.Message);
            }
        }

        public void UDPListener_Received(byte[] data, IPEndPoint endPoint)
        {
            try
            {
                var dhcpData = new DhcpData(data)
                {
                    RelatedServer = this
                };
                var msgType = dhcpData.GetDhcpMessageType();
                switch (msgType)
                {
                    case DhcpMessgeType.DHCP_DISCOVER:       
                        if (IsAuto)
                            SendDHCPMessage(DhcpMessgeType.DHCP_OFFER, dhcpData);
                        Announced?.Invoke(dhcpData.ToClientSettings());
                        break;
                    case DhcpMessgeType.DHCP_REQUEST:
                        if (IsAuto)
                            SendDHCPMessage(DhcpMessgeType.DHCP_ACK, dhcpData);
                        Requested?.Invoke(dhcpData.ToClientSettings());
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UDPListener_Received:" + ex.Message);
            }
        }

        //mac announced itself, established IP etc....
        //send the offer to the mac
        public void SendDHCPMessage(DhcpMessgeType msgType, DhcpData data)
        {
            try
            {
                var dataToSend = data.BuildSendData(msgType);
                udpListener.SendData(dataToSend);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{GetType().FullName}:{e.Message}");
            }
        }
    }
}
