using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SmallDhcpServer;
namespace IPShareSet
{
    static class Program
    {
        static DHCPServer server;
        static Thread thread;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            server = new DHCPServer("192.168.2.1");
            server.Announced += new DHCPServer.AnnouncedEventHandler(DHCP_Announced);
            server.Requested += new DHCPServer.RequestEventHandler(DHCP_Requested);
            thread = new Thread(server.StartDHCPServer);
            thread.Start();
            while(true)
            {
                continue;
            }

        }
        static void DHCP_Announced(DhcpData data)
        {
            Console.WriteLine(string.Format("DHCP Discover message from {0}", data.ClientMacAddr));
            //options should be filled with valid data
            data.IPAddr = "192.168.2.10";
            data.SubMask = "255.255.255.0";
            data.LeaseTime = 2000;
            data.ServerName = "DHCP Server";
            data.MyIP = "192.168.2.1";
            data.RouterIP = "0.0.0.0";
            data.LogServerIP = "0.0.0.0";
            data.DomainIP = "0.0.0.0";

            server.SendDHCPMessage(DhcpMessgeType.DHCP_OFFER, data);
        }
        static void DHCP_Requested(DhcpData data)
        {
            Console.WriteLine(string.Format("DHCP Request message from {0}", data.ClientMacAddr));
            //announced so then send the offer
            data.IPAddr = "192.168.2.10"; ;
            data.SubMask = "255.255.255.0";
            data.LeaseTime = 2000;
            data.ServerName = "DHCP Server";
            data.MyIP = "192.168.2.1";
            data.RouterIP = "0.0.0.0";
            data.LogServerIP = "0.0.0.0";
            data.DomainIP = "0.0.0.0";
            server.SendDHCPMessage(DhcpMessgeType.DHCP_ACK, data);
        }
    }
}
