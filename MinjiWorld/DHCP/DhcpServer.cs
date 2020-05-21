using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using MinjiWorld.DHCP.Extension;
using MinjiWorld.DHCP.Internal;

namespace MinjiWorld.DHCP
{
    public class DhcpServer
    {
        public delegate void DiscoverEventHandler(DhcpData.ClientInfomation data);
        public delegate void ReleasedEventHandler();
        public delegate void RequestEventHandler(DhcpData.ClientInfomation data);
        public delegate void AssignedEventHandler(string ipAdd);

        public event DiscoverEventHandler Discovered;
        public event RequestEventHandler Requested;

        private UdpListener udpListener; // the udp snd/rcv class

        public DhcpServerSettings Settings;

        private class OwnedIpAddress
        {
            public IPAddress Ip;
            public bool IsAllocated;
            public string AuthorizedClientMac;
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

        private DhcpRequestType GetDhcpRequestType(DhcpData.ClientInfomation clientInfo, IPEndPoint endPoint)
        {
            var res = DhcpRequestType.Unknown;
            if (clientInfo.ServerAddress != null && clientInfo.RequestAddress != null && clientInfo.ClientAddress.Equals(IPAddress.Any))
            {
                res = DhcpRequestType.Selecting;
            }
            else if (clientInfo.ServerAddress == null && clientInfo.RequestAddress != null && clientInfo.ClientAddress.Equals(IPAddress.Any))
            {
                res = DhcpRequestType.InitReboot;
            }
            else if (clientInfo.ServerAddress == null && clientInfo.RequestAddress == null && endPoint.Address.Equals(Settings.ServerIp))
            {
                res = DhcpRequestType.ReNewing;
            }
            else if (clientInfo.ServerAddress == null && clientInfo.RequestAddress == null && endPoint.Address.Equals(IPAddress.Broadcast))
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
            ownedIpAddressPool = Settings.ServerIp.GetAllSubnet(Settings.SubnetMask)
                .Select(ip => new OwnedIpAddress { Ip = ip, IsAllocated = false}).ToList();
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
                udpListener = new UdpListener(67, 68, Settings.ServerIp.ToString());
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
                        Console.WriteLine("received DHCPDISCOVER");
                        Discovered?.Invoke(client);
                        var newIp = ownedIpAddressPool.Find(x => x.IsAllocated == false);
                        if (newIp.Ip == null)
                        {
                            Console.WriteLine("no ip is avalaible to allocate");
                            return;
                        }
                        Console.WriteLine("send DHCPOFFER");
                        SendDhcpMessage(DhcpMessgeType.DHCP_OFFER, dhcpData, newIp);
                        break;

                    case DhcpMessgeType.DHCP_REQUEST:
                        Console.WriteLine("received DHCPREQUEST");
                        Requested?.Invoke(client);
                        switch (GetDhcpRequestType(client, endPoint))
                        {
                            // respond to client which has responded to DHCPOFFER message from this server 
                            case DhcpRequestType.Selecting:
                                Console.WriteLine("response to DHCPREQUEST generated during SELECTING state");
                                if (Settings.ServerIp.Equals(client.ServerAddress))
                                {
                                    var allocatedIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.RequestAddress));
                                    if (allocatedIp.Ip != null && !allocatedIp.IsAllocated)
                                    {
                                        allocatedIp.IsAllocated = true;
                                        allocatedIp.AuthorizedClientMac = client.MacAddress;
                                        Console.WriteLine("send DHCPACK");
                                        SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, allocatedIp);
                                    }
                                }
                                break;
                            case DhcpRequestType.InitReboot:
                                Console.WriteLine("response to DHCPREQUEST generated during INIT-REBOOT state");
                                if (!client.RelayAgentAddress.Equals(IPAddress.Any))
                                    Console.WriteLine("relay agent is not supported in this version");
                                var rebootIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.RequestAddress));
                                if (rebootIp.Ip != null && rebootIp.AuthorizedClientMac == client.MacAddress)
                                {
                                    Console.WriteLine("send DHCPACK");
                                    SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, rebootIp);
                                }
                                break;
                            case DhcpRequestType.ReNewing:
                                Console.WriteLine("response to DHCPREQUEST generated during RENEWING state");
                                var reNewIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.ClientAddress));
                                if (reNewIp.Ip != null && reNewIp.AuthorizedClientMac == client.MacAddress)
                                {
                                    Console.WriteLine("send DHCPACK");
                                    SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, reNewIp);
                                }
                                break;
                            case DhcpRequestType.ReBinding:
                                Console.WriteLine("response to DHCPREQUEST generated during REBINDING state");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case DhcpMessgeType.DHCP_DECLINE:
                        break;
                    case DhcpMessgeType.DHCP_RELEASE:
                        Console.WriteLine("received DHCPRELESE");
                        var oldIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.ClientAddress));
                        if (oldIp.Ip != null) oldIp.IsAllocated = false;
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
        private void SendDhcpMessage(DhcpMessgeType msgType, DhcpData data, OwnedIpAddress client = null)
        {
            BroadCastType castType;
            string dest = "255.255.255.255";
            var clientInfo = data.GetClientInfo();
            if (clientInfo.RelayAgentAddress.Equals(IPAddress.Any) && !clientInfo.ClientAddress.Equals(IPAddress.Any))
            {
                castType = BroadCastType.UniCast;
                dest = clientInfo.ToString();
            }
            if (clientInfo.RelayAgentAddress.Equals(IPAddress.Any) && clientInfo.ClientAddress.Equals(IPAddress.Any) && clientInfo.CastType == BroadCastType.BroadCast)
            {
                castType = BroadCastType.BroadCast;
            }
            if (clientInfo.RelayAgentAddress.Equals(IPAddress.Any) && clientInfo.ClientAddress.Equals(IPAddress.Any) && clientInfo.CastType == BroadCastType.UniCast)
            {
                // TODO
                castType = BroadCastType.UniCast;
            }
            try
            {
                var dataToSend = data.BuildSendData(msgType, client?.Ip);
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
