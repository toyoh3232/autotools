using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

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
            var serverSettings = new DhcpServerSettings
            {
                MyIP = ""
            };
            server = new DHCPServer(serverSettings)
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
    }
}
