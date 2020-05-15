using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace SmallDhcpServer
{
    static class Utils
    {
        public static string ByteToString(byte[] data, int len)
        {
            string dString;
            dString = string.Empty;
            if (data != null)
            {
                for (int i = 0; i < len; i++)
                {
                    dString += data[i].ToString("X2");
                }
            }
            return dString;


        }
        public static string GetIPv4Mask(string address)
        {
            foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (var unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(unicastIPAddressInformation.Address.ToString()))
                        {
                            return unicastIPAddressInformation.IPv4Mask.ToString();
                        }
                    }
                }
            }
            return null;
        }
    }
}
