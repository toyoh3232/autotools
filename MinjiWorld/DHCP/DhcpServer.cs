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

        // only broadcast, unicast w/out ARP is not supported
        // change to raw socket in future version
        private UdpListener udpListener; 

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
                        Console.WriteLine($"{Utils.GetCurrentTime()} DHCPDISCOVER received.");
                        Discovered?.Invoke(client);
                        var newIp = ownedIpAddressPool.Find(x =>(x.AuthorizedClientMac == client.MacAddress) || (x.IsAllocated == false && x.AuthorizedClientMac == null));
                        if (newIp.Ip == null)
                        {
                            Console.WriteLine($"{Utils.GetCurrentTime()} No ip is avalaible to allocate.");
                            return;
                        }
                        Console.WriteLine($"{Utils.GetCurrentTime()}:DHCPOFFER sent.");
                        // MUST be unicast over raw socket (unimplemented)
                        // broadcast used
                        SendDhcpMessage(DhcpMessgeType.DHCP_OFFER, dhcpData, newIp);
                        break;

                    case DhcpMessgeType.DHCP_REQUEST:
                        Console.WriteLine($"{Utils.GetCurrentTime()} DHCPREQUEST received.");
                        Requested?.Invoke(client);
                        switch (GetDhcpRequestType(client, endPoint))
                        {
                            // respond to client which has responded to DHCPOFFER message from this server 
                            case DhcpRequestType.Selecting:
                                Console.WriteLine($"{Utils.GetCurrentTime()} Response to DHCPREQUEST generated during SELECTING state.");
                                if (Settings.ServerIp.Equals(client.ServerAddress))
                                {
                                    var allocatedIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.RequestAddress));
                                    if (allocatedIp.Ip != null && !allocatedIp.IsAllocated)
                                    {
                                        allocatedIp.IsAllocated = true;
                                        allocatedIp.AuthorizedClientMac = client.MacAddress;
                                        Console.WriteLine($"{Utils.GetCurrentTime()} DHCPACK sent.");
                                        // broadcast
                                        SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, allocatedIp);
                                    }
                                }
                                break;
                            case DhcpRequestType.InitReboot:
                                Console.WriteLine($"{Utils.GetCurrentTime()} Response to DHCPREQUEST generated during INIT-REBOOT state.");
                                if (!client.RelayAgentAddress.Equals(IPAddress.Any))
                                    Console.WriteLine($"{Utils.GetCurrentTime()} Relay agent is not supported in this version.");
                                var rebootIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.RequestAddress));
                                if (rebootIp.Ip != null && rebootIp.AuthorizedClientMac == client.MacAddress)
                                {
                                    // broadcast
                                    SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, rebootIp);
                                    Console.WriteLine($"{Utils.GetCurrentTime()} DHCPACK sent.");
                                }
                                break;
                            case DhcpRequestType.ReNewing:
                                Console.WriteLine($"{Utils.GetCurrentTime()} Response to DHCPREQUEST generated during RENEWING state.");
                                var reNewIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.ClientAddress));
                                if (reNewIp.Ip != null && reNewIp.AuthorizedClientMac == client.MacAddress)
                                {
                                    // unicast
                                    SendDhcpMessage(client.ClientAddress.ToString(), DhcpMessgeType.DHCP_ACK, dhcpData, reNewIp);
                                    Console.WriteLine($"{Utils.GetCurrentTime()} DHCPACK sent.");
                                }
                                break;
                            case DhcpRequestType.ReBinding:
                                Console.WriteLine($"{Utils.GetCurrentTime()} Response to DHCPREQUEST generated during REBINDING state.");
                                var reBindIp = ownedIpAddressPool.Find(x => x.IsAllocated == false);
                                if (reBindIp.Ip != null)
                                {
                                    reBindIp.IsAllocated = true;
                                    reBindIp.AuthorizedClientMac = client.MacAddress;
                                    // broadcast
                                    SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, reBindIp);
                                    Console.WriteLine($"{Utils.GetCurrentTime()} DHCPACK sent.");
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case DhcpMessgeType.DHCP_DECLINE:
                        Console.WriteLine($"{Utils.GetCurrentTime()} DHCPDECLINE received.");
                        var declinedIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.ClientAddress));
                        if (declinedIp.Ip != null) ownedIpAddressPool.Remove(declinedIp);
                        break;
                    case DhcpMessgeType.DHCP_RELEASE:
                        Console.WriteLine($"{Utils.GetCurrentTime()} DHCPRELESE received.");
                        var releasedIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.ClientAddress));
                        if (releasedIp.Ip != null) releasedIp.IsAllocated = false;
                        break;
                    case DhcpMessgeType.DHCP_INFORM:
                        Console.WriteLine($"{Utils.GetCurrentTime()} DHCPINFORM received.");
                        // unicast
                        SendDhcpMessage(client.ClientAddress.ToString(), DhcpMessgeType.DHCP_ACK, dhcpData, null);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($@"{GetType().FullName}:{MethodBase.GetCurrentMethod()}{e.Message}");
            }
        }

        // override method for broadcast 
        private void SendDhcpMessage(DhcpMessgeType msgType, DhcpData data, OwnedIpAddress newClient)
        {
            SendDhcpMessage(IPAddress.Broadcast.ToString(), msgType, data, newClient);
        }

        private void SendDhcpMessage(string dest, DhcpMessgeType msgType, DhcpData data, OwnedIpAddress newClient)
        {
            try
            {
                var dataToSend = data.BuildSendData(msgType, newClient.Ip);
                udpListener.SendData(dest, dataToSend);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"{GetType().FullName}:{MethodBase.GetCurrentMethod()}{e.Message}");
                throw e;
            }
        }
    }
}
