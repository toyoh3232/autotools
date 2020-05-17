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
        static DhcpServer server;
        static Thread thread;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var serverSettings = new DhcpServerSettings
            {
                ServerIp = ""
            };
            server = new DhcpServer(serverSettings)
            {
                IsAuto = false
            };
            server.Announced += (s) => Console.WriteLine(s.MacAddress);
            thread = new Thread(server.Start);
            thread.Start();
            while (true)
            {
            }
        }
    }
}
