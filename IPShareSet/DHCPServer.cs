using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace SmallDhcpServer
{
    public class DHCPServer
    {
        public delegate void AnnouncedEventHandler(DhcpData data);
        public delegate void ReleasedEventHandler();
        public delegate void RequestEventHandler(DhcpData data);
        public delegate void AssignedEventHandler(string ipAdd);

        public event AnnouncedEventHandler Announced;
        public event RequestEventHandler Requested;

        private UDPListener udpListener; // the udp snd/rcv class
        
        private string serverIP;

        public DHCPServer(string serverIP)
        {
            this.serverIP = serverIP;
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
                udpListener = new UDPListener(67, 68, serverIP);
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
                var dhcpData = new DhcpData(data);
                var msgType = dhcpData.GetDhcpMessageType();
                switch (msgType)
                {
                    case DhcpMessgeType.DHCP_DISCOVER:
                        Announced(dhcpData);
                        break;
                    case DhcpMessgeType.DHCP_REQUEST:
                        Requested(dhcpData);
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
