using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
        static void Main(string[] args)
        {
            server = new DHCPServer(new DhcpServerSettings
            {
                MyIP = args[0]
            })
            {
                IsAuto = false
            };
            server.Announced += (s) => Console.WriteLine(s.MacAddress);
            thread = new Thread(server.StartDHCPServer);
            thread.Start();
            while (true)
            {
                continue;
            }
        }

        static void SetIp()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
               
            }
            
        }
    }
}
