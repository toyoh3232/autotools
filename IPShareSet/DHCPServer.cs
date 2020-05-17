using System;
using System.Linq;
using System.Net;
using SmallDHCPServer;

namespace IPShareSet
{
    public class DhcpServer
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

        private readonly DhcpClientSettings[] clientSettingsPool;

        public DhcpClientSettings GetRandomClientSettings()
        {
            for (var i=0; i< clientSettingsPool.Length; i++)
            {
                if (clientSettingsPool[i].IsAllocated) continue;
                clientSettingsPool[i].IsAllocated = true;
                return clientSettingsPool[i];
            }
            // TODO
            throw new Exception();
        }

        public DhcpServer(DhcpServerSettings setting)
        {
            Settings = setting;
            clientSettingsPool = Utils.GetAllSubnetIPv4(setting.ServerIp, setting.SubMask).Select(ip => new DhcpClientSettings {IpAddress = ip, IsAllocated = false}).ToArray();
        }

        ~DhcpServer()
        {
            udpListener?.StopListener();
        }

        // function to start the DHCP server
        // port 67 to receive, 68 to send
        public void Start()
        {
            try
            {   // start the DHCP server
                // assign the event handlers
                udpListener = new UdpListener(67, 68, Settings.ServerIp);
                udpListener.Received += UdpListener_Received;

            }
            catch (Exception e)
            {
                Console.WriteLine("Start:" + e.Message);
            }
        }

        public void UdpListener_Received(byte[] data, IPEndPoint endPoint)
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
                            SendDhcpMessage(DhcpMessgeType.DHCP_OFFER, dhcpData);
                        Announced?.Invoke(dhcpData.ToClientSettings());
                        break;
                    case DhcpMessgeType.DHCP_REQUEST:
                        if (IsAuto)
                            SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData);
                        Requested?.Invoke(dhcpData.ToClientSettings());
                        break;
                    case DhcpMessgeType.DHCP_OFFER:
                        break;
                    case DhcpMessgeType.DHCP_DECLINE:
                        break;
                    case DhcpMessgeType.DHCP_ACK:
                        break;
                    case DhcpMessgeType.DHCP_NAK:
                        break;
                    case DhcpMessgeType.DHCP_RELEASE:
                        break;
                    case DhcpMessgeType.DHCP_INFORM:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UdpListener_Received:" + ex.Message);
            }
        }

        //mac announced itself, established IP etc....
        //send the offer to the mac
        public void SendDhcpMessage(DhcpMessgeType msgType, DhcpData data)
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
