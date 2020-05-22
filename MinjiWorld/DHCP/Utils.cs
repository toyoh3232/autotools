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
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString("MM/dd HH:mm:ss");
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
