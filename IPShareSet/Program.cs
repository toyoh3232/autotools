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
            server = new DHCPServer()
            {
                Settings = new DhcpServerSettings
                {
                    MyIP = "192.168.10.52"
                },
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
