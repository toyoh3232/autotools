using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            public uint? Id;
            public bool IsAllocated;
        }

        private readonly List<OwnedIpAddress> ownedIpAddressPool;

        public DhcpServer(DhcpServerSettings setting)
        {
            Settings = setting;
            ownedIpAddressPool = Utils.GetAllSubnetIPv4(Settings.ServerIp, Settings.SubMask)
                .Select(ip => new OwnedIpAddress { Ip = ip, IsAllocated = false, Id = null }).ToList();
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
                var msgType = dhcpData.GetDhcpMessageType();
                var client = dhcpData.GetClientInfo();
                switch (msgType)
                {
                    case DhcpMessgeType.DHCP_DISCOVER:
                        Console.WriteLine("Discover Asked");
                        Discovered?.Invoke(dhcpData.GetClientInfo());
                        try
                        {
                            var newIp = ownedIpAddressPool.Find(x => x.Id == null);
                            newIp.Id = client.TransactionID;
                            SendDhcpMessage(DhcpMessgeType.DHCP_OFFER, dhcpData, newIp);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{GetType().FullName}:{MethodBase.GetCurrentMethod()}.{e.Message}");
                        }
                        break;

                    case DhcpMessgeType.DHCP_REQUEST:
                        Console.WriteLine("Request Asked");
                        Requested?.Invoke(dhcpData.GetClientInfo());
                        if (Settings.ServerIp == client.ServerAddress)
                        {
                            for (var i = 0; i < ownedIpAddressPool.Length; i++)
                            {
                                if (ownedIpAddressPool[i].Id == client.TransactionID)
                                {
                                    if (ownedIpAddressPool[i].Ip == client.RequestAddress)
                                    {
                                        ownedIpAddressPool[i].IsAllocated = true;
                                        SendDhcpMessage(DhcpMessgeType.DHCP_OFFER, dhcpData, ownedIpAddressPool[i]);
                                    }
                                    else
                                    {
                                        ownedIpAddressPool[i].Id = null;
                                    }
                                    break;
                                }
                            }
                        }
                        Requested?.Invoke(dhcpData.GetClientInfo());
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
                Console.WriteLine($"{GetType().FullName}:{e.Message}");
            }
        }
    }
}
