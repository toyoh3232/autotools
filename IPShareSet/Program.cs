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
            server.Discovered += ShowMessage;
            server.Requested += ShowMessage;
            thread = new Thread(server.Start);
            thread.Start();
            while (true)
            {

            }
        }

        private static void ShowMessage(DhcpData.DhcpClientInfomation data)
        {
            Console.WriteLine($@"MacAddress:{data.MacAddress ?? string.Empty}");
            Console.WriteLine($@"RequestAddress:{data.RequestAddress ?? string.Empty}");
            Console.WriteLine($@"ServerAddress:{data.ServerAddress ?? string.Empty}");
            Console.WriteLine($@"TransactionID:{data.TransactionID}");
            Console.WriteLine($@"ClientAddress:{data.ClientAddress ?? string.Empty}");
            Console.WriteLine($@"ClientIdentifier:{data.ClientIdentifier ?? string.Empty}");
        }
    }
}
