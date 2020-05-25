using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;

namespace Tohasoft.Net.DHCP
{
    static class Utils
    {
        public static void AddToArray(byte fromValue, ref byte[] targetValue)
        {
            AddToArray(new byte[] { fromValue }, ref targetValue);
        }

        public static void AddToArray(byte[] fromValue, ref byte[] targetArray)
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
                throw new Exception($@"{MethodBase.GetCurrentMethod()}.{e.Message}");
            }
        }

    }
}
