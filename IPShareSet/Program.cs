using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using MinjiWorld;
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
        public static void Main(string[] args)
        {
            var serverSettings = new DhcpServerSettings(args[0]);
            server = new DhcpServer(serverSettings, new Logger(Console.Out));
            server.Discovered += ShowMessage;
            server.Requested += ShowMessage;
            thread = new Thread(server.Start);
            thread.Start();
            while (true)
            {

            }
        }

        private static void ShowMessage(DhcpData.ClientInfomation data)
        {
            Console.WriteLine($@"               MacAddress:{data.MacAddress ?? string.Empty}");
            Console.WriteLine($@"               ServerAddress:{data.ServerAddress?.ToString()}");
            Console.WriteLine($@"               TransactionID:{data.TransactionID}");
            Console.WriteLine($@"               ClientAddress:{data.ClientAddress?.ToString()}");
        }
    }
}
