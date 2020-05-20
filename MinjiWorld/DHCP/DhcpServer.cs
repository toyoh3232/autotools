using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using MinjiWorld.DHCP.Internal;

namespace MinjiWorld.DHCP
{
    public class DhcpServer
    {
        public delegate void DiscoverEventHandler(DhcpData.DhcpClientInfomation data);
        public delegate void ReleasedEventHandler();
        public delegate void RequestEventHandler(DhcpData.DhcpClientInfomation data);
        public delegate void AssignedEventHandler(string ipAdd);

        public event DiscoverEventHandler Discovered;
        public event RequestEventHandler Requested;

        private UdpListener udpListener; // the udp snd/rcv class


        public bool IsAuto = true;

        public DhcpServerSettings Settings;

        private class OwnedIpAddress
        {
            public string Ip;
            public uint? TransactionID;
            public bool IsAllocated;
        }

        // RFC 2131 p30-33
        private enum DhcpRequestType
        {
            Unknown,
            Selecting,
            InitReboot,
            ReNewing,
            ReBinding
        }

        private DhcpRequestType GetDhcpRequestType(DhcpData.DhcpClientInfomation clientInfo, IPEndPoint endPoint)
        {
            var res = DhcpRequestType.Unknown;
            if (clientInfo.ServerAddress != null && clientInfo.RequestAddress != null && clientInfo.ClientAddress == "0.0.0.0")
            {
                res = DhcpRequestType.Selecting;
            }
            else if (clientInfo.ServerAddress == null && clientInfo.RequestAddress != null && clientInfo.ClientAddress == "0.0.0.0")
            {
                res = DhcpRequestType.InitReboot;
            }
            else if (clientInfo.ServerAddress == null && clientInfo.RequestAddress == null && endPoint.Address.ToString() == Settings.ServerIp)
            {
                res = DhcpRequestType.ReNewing;
            }
            else if (clientInfo.ServerAddress == null && clientInfo.RequestAddress == null &&  endPoint.Address.ToString() == "255.255.255.255")
            {
                res = DhcpRequestType.ReBinding;
            }
            else
            {
                res = DhcpRequestType.Unknown;
            }
            return res;
        }

        private readonly List<OwnedIpAddress> ownedIpAddressPool;

        public DhcpServer(DhcpServerSettings setting)
        {
            Settings = setting;
            ownedIpAddressPool = Utils.GetAllSubnetIPv4(Settings.ServerIp, Settings.SubMask)
                .Select(ip => new OwnedIpAddress { Ip = ip, IsAllocated = false, TransactionID = null }).ToList();
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

        public void End()
        {
            udpListener.Received -= UdpListener_Received;
            udpListener.StopListener();
        }

        public void UdpListener_Received(byte[] data, IPEndPoint endPoint)
        {
            try
            {
                var dhcpData = new DhcpData(data)
                {
                    RelatedServer = this
                };
                var msgType = dhcpData.GetCurrentMessageType();
                var client = dhcpData.GetClientInfo();
                switch (msgType)
                {
                    case DhcpMessgeType.DHCP_DISCOVER:
                        Console.WriteLine("Discover Asked");
                        Discovered?.Invoke(client);
                        var newIp = ownedIpAddressPool.Find(x => x.TransactionID == null);
                        if (newIp.Ip == null)
                        {
                            Console.WriteLine("no ip is avalaible to allocate");
                            return;
                        }
                        newIp.TransactionID = client.TransactionID;
                        SendDhcpMessage(DhcpMessgeType.DHCP_OFFER, dhcpData, newIp);                                                                                          
                        break;

                    case DhcpMessgeType.DHCP_REQUEST:
                        Console.WriteLine("Request Asked");
                        Requested?.Invoke(client);
                        switch (GetDhcpRequestType(client, endPoint))
                        {
                            // respond to client which has responded to DHCPOFFER message from this server 
                            case DhcpRequestType.Selecting:
                                Console.WriteLine("Selecting Asked");
                                if (Settings.ServerIp == client.ServerAddress)
                                {
                                    var allocatedIp = ownedIpAddressPool.Find(x => x.TransactionID == client.TransactionID);
                                    if (allocatedIp.Ip != null)
                                    {
                                        if (allocatedIp.Ip == client.RequestAddress)
                                        {
                                            allocatedIp.IsAllocated = true;
                                            SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, allocatedIp);

                                        }
                                        else
                                        {
                                            allocatedIp.TransactionID = null;
                                        }
                                    }
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case DhcpMessgeType.DHCP_DECLINE:
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
        private void SendDhcpMessage(DhcpMessgeType msgType, DhcpData data, OwnedIpAddress client)
        {
            try
            {
                var dataToSend = data.BuildSendData(msgType, client.Ip);
                udpListener.SendData(dataToSend);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"{GetType().FullName}:{MethodBase.GetCurrentMethod()}{e.Message}");
                throw e;
            }
        }
    }
}
