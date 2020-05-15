using System;
using System.Net;
using System.Windows.Forms;

namespace SmallDhcpServer
{
    public class DHCPServer
    {
        public delegate void AnnouncedEventHandler(DhcpClientSettings data);
        public delegate void ReleasedEventHandler();
        public delegate void RequestEventHandler(DhcpClientSettings data);
        public delegate void AssignedEventHandler(string ipAdd);

        public event AnnouncedEventHandler Announced;
        public event RequestEventHandler Requested;

        private UDPListener udpListener; // the udp snd/rcv class

        public DhcpServerSettings Settings { get; }

        public string[] clientSettingsPool;

        public string GetAvailibleClientSettings()
        {
            // TODO
            return clientSettingsPool[0];
        }
        public bool IsAuto = true;
        public DHCPServer(DhcpServerSettings setting)
        {
            Settings = setting;
            clientSettingsPool = null;
        }

        ~DHCPServer()
        {
            udpListener?.StopListener();
        }

        // function to start the DHCP server
        // port 67 to recieve, 68 to send
        public void StartDHCPServer()
        {
            try
            {   // start the DHCP server
                // assign the event handlers
                udpListener = new UDPListener(67, 68, Settings.MyIP);
                udpListener.Reveived += new UDPListener.DataRcvdEventHandler(UDPListener_Received);

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
                        Announced(dhcpData.ToClientSettings());
                        if (IsAuto)
                            SendDHCPMessage(DhcpMessgeType.DHCP_OFFER, dhcpData);
                        break;
                    case DhcpMessgeType.DHCP_REQUEST:
                        Requested(dhcpData.ToClientSettings());
                        if (IsAuto)
                            SendDHCPMessage(DhcpMessgeType.DHCP_ACK, dhcpData);
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
            //we shall leave everything as is structure wise
            //shall CHANGE the type to OFFER
            //shall set the client's IP-Address
            try
            {
                var dataToSend = data.BuildSendData(msgType);
                udpListener.SendData(dataToSend);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendDHCPMessage:" + ex.Message);
            }
        }
    }
}
