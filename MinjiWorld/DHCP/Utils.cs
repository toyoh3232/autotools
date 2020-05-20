using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;

namespace MinjiWorld.DHCP
{
    public static class Utils
    {
        public static string ByteToString(byte[] data, int len)
        {
            var dString = string.Empty;
            if (data == null) return dString;
            for (var i = 0; i < len; i++)
            {
                dString += data[i].ToString("X2");
            }
            return dString;
        }

        public static string GetIPv4Mask(string address)
        {
            foreach (var adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (var addressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (addressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(addressInformation.Address.ToString()))
                        {
                            return addressInformation.IPv4Mask.ToString();
                        }
                    }
                }
            }
            return null;
        }

        public static string[] GetAllSubnetIPv4(string hostIP, string netmask)
        {
            var IPs = new List<string>();
            var hostInt = ToInt(IPAddress.Parse(hostIP).GetAddressBytes());
            var netmaskInt = ToInt(IPAddress.Parse(netmask).GetAddressBytes());
            var wildCard = ~(uint)netmaskInt;
            var netBytesInt = hostInt & netmaskInt;
            for (var start = 1; start <= wildCard - 1; start++)
            {
                var ip = (new IPAddress(ToBytes(netBytesInt + start))).ToString();
                if (!ip.Equals(hostIP)) IPs.Add(ip);
            }

            return IPs.ToArray();
        }

        private static long ToInt(IList<byte> data)
        {
            uint ip = 0;

            for (var i = 0; i < data.Count; i++)
            {
                ip += ((uint)data[i]) << (8 * (data.Count - i - 1));
            }
            return ip;
        }

        private static byte[] ToBytes(long data)
        {
            var bytes = new byte[4];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(data >> (8 * (bytes.Length - i - 1)));
            }
            return bytes;
        }

        public static void FillZero(byte[] data)
        {
            for (var i=0; i<data.Length;i++)
            {
                data[i] = 0;
            }
        }

        public static void AddtoArray(byte fromValue, ref byte[] targetValue)
        {
            AddtoArray(new byte[] { fromValue }, ref targetValue);
        }

        public static void AddtoArray(byte[] fromValue, ref byte[] targetArray)
        {
            try
            {
                if (targetArray != null)
                    Array.Resize(ref targetArray, targetArray.Length + fromValue.Length);
                else
                    Array.Resize(ref targetArray, fromValue.Length);
                Array.Copy(fromValue, 0, targetArray, targetArray.Length - fromValue.Length, fromValue.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"{MethodBase.GetCurrentMethod()}.{e.Message}");
            }
        }

    }
}
