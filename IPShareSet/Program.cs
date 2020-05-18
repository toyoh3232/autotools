using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using MinjiWorld.DHCP;

namespace IPShareSet
{
    static class Program
    {
        static DhcpServer server;
        static Thread thread;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var serverSettings = new DhcpServerSettings(args[0]);
            server = new DhcpServer(serverSettings);
            server.Discovered += Server_Discovered;
            server.Requested += Server_Discovered;
            thread = new Thread(server.Start);
            thread.Start();
            while (true)
            {

            }
        }

        private static void Server_Discovered(DhcpData.DhcpClientInfomation data)
        {
            Console.WriteLine($"{data.MacAddress}");
        }
    }
}
