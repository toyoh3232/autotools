using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SmallDhcpServer
{


    public class DhcpData : DhcpSettings
    {

        #region Readonly Property
        public string ClientMacAddr
        {
            get
            {
                return ByteToString(packet.chaddr, packet.hlen);
            }
        }
        #endregion

        private DhcpPacketStruct packet;

        public DhcpData(byte[] data)
        {
            packet = new DhcpPacketStruct(data);
        }

        public DhcpMessgeType GetDhcpMessageType()
        {
            return packet.GetDhcpMessageType();
        }

        private string ByteToString(byte[] data, int len)
        {
            string dString;

            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine("ByteToString:" + ex.Message);
                return string.Empty;
            }

        }
        public byte[] BuildSendData(DhcpMessgeType msgType)
        {
            packet.op = 2;
            packet.yiaddr = IPAddress.Parse(IPAddr).GetAddressBytes();
            packet.BuildOptions(msgType, this);
            return packet.ToArray();
        }
        
    }
}
