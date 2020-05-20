using MinjiWorld.DHCP.Internal;
using System;
using System.Net;
using System.Text;

namespace MinjiWorld.DHCP
{
    public class DhcpData
    {
        public struct DhcpClientInfomation
        {
            public string RequestAddress;
            public string ClientIdentifier;
            public string ServerAddress;
            public string ClientAddress;
            public string MacAddress;
            public uint TransactionID;
        }

        public bool IsBuiltTobeSent { get; private set; }

        public DhcpServer RelatedServer { get; internal set; }

        private DhcpPacketStruct packet;

        internal DhcpData(byte[] data)
        {
            packet = new DhcpPacketStruct(data);
            IsBuiltTobeSent = false;
        }

        public DhcpMessgeType GetCurrentMessageType()
        {
            var data = packet.options.GetOptionData(DhcpOptionType.DHCPMessageType);
            //　TODO
            if (IsBuiltTobeSent) throw new Exception();
            // TODO
            if (data == null) throw new Exception();
            return (DhcpMessgeType)data[0];
        }

        internal byte[] BuildSendData(DhcpMessgeType msgType, string clientIp)
        {
            if (!IsBuiltTobeSent)
            {
                packet.ApplySettings(msgType, RelatedServer.Settings, clientIp);
                IsBuiltTobeSent = true;
                return packet.ToArray();
            }
            // TODO
            throw new Exception();

        }
        


        public DhcpClientInfomation GetClientInfo()
        {

            var client = new DhcpClientInfomation
            {
                MacAddress = Utils.ByteToString(packet.chaddr, packet.hlen),
                ClientAddress = new IPAddress(packet.ciaddr).ToString(),
                TransactionID = BitConverter.ToUInt32(packet.xid, 0)
            };
            
            if (packet.options.GetOptionData(DhcpOptionType.ClientIdentifier) != null)
                client.ClientIdentifier = Encoding.GetEncoding(932).GetString(packet.options.GetOptionData(DhcpOptionType.ClientIdentifier));
            if (packet.options.GetOptionData(DhcpOptionType.RequestedIPAddress) != null)
                client.RequestAddress = new IPAddress(packet.options.GetOptionData(DhcpOptionType.RequestedIPAddress)).ToString();
            if (packet.options.GetOptionData(DhcpOptionType.ServerIdentifier) != null)
                client.ServerAddress = new IPAddress(packet.options.GetOptionData(DhcpOptionType.ServerIdentifier)).ToString();

            return client;
        }
    }
}
