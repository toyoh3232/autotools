using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SmallDHCPServer;
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
            server = new DHCPServer("192.168.1.1");
            server.Announced += new DHCPServer.AnnouncedEventHandler(cDhcp_Announced);
            server.Request += new DHCPServer.RequestEventHandler(cDhcp_Request);
            thread = new Thread(server.StartDHCPServer);
            thread.Start();
            while (true)
                continue;

        }
        static void cDhcp_Announced(CDHCPStruct d_DHCP, string MacId)
        {

            //options should be filled with valid data
            d_DHCP.dData.IPAddr = "192.168.1.10";
            d_DHCP.dData.SubMask = "255.255.255.0";
            d_DHCP.dData.LeaseTime = 2000;
            d_DHCP.dData.ServerName = "Small DHCP Server";
            d_DHCP.dData.MyIP = "192.168.1.1";
            d_DHCP.dData.RouterIP = "0.0.0.0";
            d_DHCP.dData.LogServerIP = "0.0.0.0";
            d_DHCP.dData.DomainIP = "0.0.0.0";

            server.SendDHCPMessage(DHCPMsgType.DHCPOFFER, d_DHCP);
        }
        static void cDhcp_Request(CDHCPStruct d_DHCP, string MacId)
        {

            //announced so then send the offer
            d_DHCP.dData.IPAddr = "192.168.1.10"; ;
            d_DHCP.dData.SubMask = "255.255.255.0";
            d_DHCP.dData.LeaseTime = 2000;
            d_DHCP.dData.ServerName = "tiny DHCP Server";
            d_DHCP.dData.MyIP = "192.168.1.1";
            d_DHCP.dData.RouterIP = "0.0.0.0";
            d_DHCP.dData.LogServerIP = "0.0.0.0";
            d_DHCP.dData.DomainIP = "0.0.0.0";
            
            server.SendDHCPMessage(DHCPMsgType.DHCPACK, d_DHCP);
        }
    }
}
