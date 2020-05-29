using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using Tohasoft.Net.DHCP;
using Tohasoft.Utils;

namespace IPShareSet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            DhcpServer server;
            try
            {
                var serverSettings = new DhcpServerSettings
                {
                    ServerIp = IPAddress.Parse(args[0])
                };
                server = new DhcpServer(serverSettings);
                logger.AddSource(server); 
                server.Discovered += ShowMessage;
                server.Requested += ShowMessage;
                server.Start();
                while (true) ;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            


            
        }

        private static void ShowMessage(ClientInfomation data)
        {
            Console.WriteLine($@"               MacAddress:{data.MacAddress ?? string.Empty}");
            Console.WriteLine($@"               ServerAddress:{data.ServerAddress?.ToString()}");
            Console.WriteLine($@"               TransactionID:{data.TransactionID}");
            Console.WriteLine($@"               ClientAddress:{data.ClientAddress?.ToString()}");
        }
    }
}
