using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
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
            DhcpRequestType res;
            if (clientInfo.ServerAddress != null && clientInfo.RequestAddress != null && clientInfo.ClientAddress.Equals(IPAddress.Any))
            {
                res = DhcpRequestType.Selecting;
            }
            else switch (clientInfo.ServerAddress)
            {
                case null when clientInfo.RequestAddress != null && clientInfo.ClientAddress.Equals(IPAddress.Any):
                    res = DhcpRequestType.InitReboot;
                    break;
                case null when clientInfo.RequestAddress == null && endPoint.Address.Equals(Settings.ServerIp):
                    res = DhcpRequestType.ReNewing;
                    break;
                case null when clientInfo.RequestAddress == null && endPoint.Address.Equals(IPAddress.Broadcast):
                    res = DhcpRequestType.ReBinding;
                    break;
                default:
                    res = DhcpRequestType.Unknown;
                    break;
            }
            return res;
        }

        private readonly List<OwnedIpAddress> ownedIpAddressPool;

        private Logger logger;

        public DhcpServer(DhcpServerSettings setting, Logger logger = null)
        {
            this.logger = logger;
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
                        logger?.Log("DHCPDISCOVER received.");
                        Discovered?.Invoke(client);
                        var newIp = ownedIpAddressPool.Find(x =>(x.AuthorizedClientMac == client.MacAddress) || (x.IsAllocated == false && x.AuthorizedClientMac == null));
                        if (newIp.Ip == null)
                        {
                            logger?.Log("No ip is available to allocate.");
                            return;
                        } 
                        logger?.Log("DHCPOFFER sent.");
                        // MUST be unicast over raw socket (unimplemented)
                        // broadcast used
                        SendDhcpMessage(DhcpMessgeType.DHCP_OFFER, dhcpData, newIp);
                        break;

                    case DhcpMessgeType.DHCP_REQUEST:
                        logger?.Log("DHCPREQUEST received.");
                        Requested?.Invoke(client);
                        switch (GetDhcpRequestType(client, endPoint))
                        {
                            // respond to client which has responded to DHCPOFFER message from this server 
                            case DhcpRequestType.Selecting:
                                logger?.Log("Response to DHCPREQUEST generated during SELECTING state.");
                                if (Settings.ServerIp.Equals(client.ServerAddress))
                                {
                                    var allocatedIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.RequestAddress));
                                    if (allocatedIp.Ip != null && !allocatedIp.IsAllocated)
                                    {
                                        allocatedIp.IsAllocated = true;
                                        allocatedIp.AuthorizedClientMac = client.MacAddress;
                                        logger?.Log("DHCPACK sent.");
                                        // broadcast
                                        SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, allocatedIp);
                                    }
                                }
                                break;
                            case DhcpRequestType.InitReboot:
                                logger?.Log("Response to DHCPREQUEST generated during INIT-REBOOT state.");
                                if (!client.RelayAgentAddress.Equals(IPAddress.Any))
                                    logger?.Log("Relay agent is not supported in this version.");
                                var rebootIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.RequestAddress));
                                if (rebootIp.Ip != null && rebootIp.AuthorizedClientMac == client.MacAddress)
                                {
                                    // broadcast
                                    SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, rebootIp);
                                    logger?.Log("DHCPACK sent.");
                                }
                                break;
                            case DhcpRequestType.ReNewing:
                                logger.Log("Response to DHCPREQUEST generated during RENEWING state.");
                                var reNewIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.ClientAddress));
                                if (reNewIp.Ip != null && reNewIp.AuthorizedClientMac == client.MacAddress)
                                {
                                    // unicast
                                    SendDhcpMessage(client.ClientAddress.ToString(), DhcpMessgeType.DHCP_ACK, dhcpData, reNewIp);
                                    logger?.Log("DHCPACK sent.");
                                }
                                break;
                            case DhcpRequestType.ReBinding:
                                logger?.Log("Response to DHCPREQUEST generated during REBINDING state.");
                                var reBindIp = ownedIpAddressPool.Find(x => x.IsAllocated == false);
                                if (reBindIp.Ip != null)
                                {
                                    reBindIp.IsAllocated = true;
                                    reBindIp.AuthorizedClientMac = client.MacAddress;
                                    // broadcast
                                    SendDhcpMessage(DhcpMessgeType.DHCP_ACK, dhcpData, reBindIp);
                                    logger?.Log("DHCPACK sent.");
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case DhcpMessgeType.DHCP_DECLINE:
                        logger?.Log("DHCPDECLINE received.");
                        var declinedIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.ClientAddress));
                        if (declinedIp.Ip != null) ownedIpAddressPool.Remove(declinedIp);
                        break;
                    case DhcpMessgeType.DHCP_RELEASE:
                        logger?.Log("DHCPRELESE received.");
                        var releasedIp = ownedIpAddressPool.Find(x => x.Ip.Equals(client.ClientAddress));
                        if (releasedIp.Ip != null) releasedIp.IsAllocated = false;
                        break;
                    case DhcpMessgeType.DHCP_INFORM:
                        logger?.Log("DHCPINFORM received.");
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
