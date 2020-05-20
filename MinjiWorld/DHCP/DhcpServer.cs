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
        public delegate void DiscoverEventHandler(DhcpData.DhcpClientInformation data);
        public delegate void ReleasedEventHandler();
        public delegate void RequestEventHandler(DhcpData.DhcpClientInformation data);
        public delegate void AssignedEventHandler(string ipAdd);

        public event DiscoverEventHandler Discovered;
        public event RequestEventHandler Requested;

        private UdpListener udpListener; // the udp snd/rcv class

        public DhcpServerSettings Settings;

        private class OwnedIpAddress
        {
            public string Ip;
            public uint? TransactionId;
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

        private DhcpRequestType GetDhcpRequestType(DhcpData.DhcpClientInformation clientInfo, IPEndPoint endPoint)
        {
            DhcpRequestType res;
            if (clientInfo.ServerAddress != null && clientInfo.RequestAddress != null && clientInfo.ClientAddress == "0.0.0.0")
            {
                res = DhcpRequestType.Selecting;
            }
            else switch (clientInfo.ServerAddress)
            {
                case null when clientInfo.RequestAddress != null && clientInfo.ClientAddress == "0.0.0.0":
                    res = DhcpRequestType.InitReboot;
                    break;
                case null when clientInfo.RequestAddress == null && endPoint.Address.ToString() == Settings.ServerIp:
                    res = DhcpRequestType.ReNewing;
                    break;
                case null when clientInfo.RequestAddress == null && endPoint.Address.ToString() == "255.255.255.255":
                    res = DhcpRequestType.ReBinding;
                    break;
                default:
                    res = DhcpRequestType.Unknown;
                    break;
            }
            return res;
        }

        private readonly List<OwnedIpAddress> ownedIpAddressPool;

        public DhcpServer(DhcpServerSettings setting)
        {
            Settings = setting;
            ownedIpAddressPool = Utils.GetAllSubnetIPv4(Settings.ServerIp, Settings.SubMask)
                .Select(ip => new OwnedIpAddress { Ip = ip, IsAllocated = false, TransactionId = null }).ToList();
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
                    case DhcpMessageType.DHCP_DISCOVER:
                        Console.WriteLine("Discover Asked");
                        Discovered?.Invoke(client);
                        var newIp = ownedIpAddressPool.Find(x => x.TransactionId == null);
                        if (newIp.Ip == null)
                        {
                            Console.WriteLine("no ip is available to allocate");
                            return;
                        }
                        newIp.TransactionId = client.TransactionId;
                        SendDhcpMessage(DhcpMessageType.DHCP_OFFER, dhcpData, newIp);                                                                                          
                        break;

                    case DhcpMessageType.DHCP_REQUEST:
                        Console.WriteLine("Request Asked");
                        Requested?.Invoke(client);
                        switch (GetDhcpRequestType(client, endPoint))
                        {
                            // respond to client which has responded to DHCPOFFER message from this server 
                            case DhcpRequestType.Selecting:
                                Console.WriteLine("Selecting Asked");
                                if (Settings.ServerIp == client.ServerAddress)
                                {
                                    var allocatedIp = ownedIpAddressPool.Find(x => x.TransactionId == client.TransactionId);
                                    if (allocatedIp.Ip != null)
                                    {
                                        if (allocatedIp.Ip == client.RequestAddress)
                                        {
                                            allocatedIp.IsAllocated = true;
                                            SendDhcpMessage(DhcpMessageType.DHCP_ACK, dhcpData, allocatedIp);

                                        }
                                        else
                                        {
                                            allocatedIp.TransactionId = null;
                                        }
                                    }
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case DhcpMessageType.DHCP_DECLINE:
                        break;
                    case DhcpMessageType.DHCP_RELEASE:
                        break;
                    case DhcpMessageType.DHCP_INFORM:
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
        private void SendDhcpMessage(DhcpMessageType msgType, DhcpData data, OwnedIpAddress client)
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
