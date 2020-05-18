using MinjiWorld.DHCP.Internal;
using System.Net;

namespace MinjiWorld.DHCP
{
    public class DhcpData
    {
        public struct DhcpClientInfomation
        {
            public string RequestAddress;
            public string ClientIdentifier;
            public string ServerAddress;
            public string MacAddress;
            public uint TransactionID;
        }

        public DhcpServer RelatedServer { get; internal set; }

        private DhcpPacketStruct packet;

        internal DhcpData(byte[] data)
        {
            packet = new DhcpPacketStruct(data);
        }

        public DhcpMessgeType GetDhcpMessageType()
        {
            return packet.GetDhcpMessageType();
        }

        internal byte[] BuildSendData(DhcpMessgeType msgType, string clientIp)
        {
            packet.ApplySettings(msgType, RelatedServer.Settings, clientIp);
            return packet.ToArray();
        }

        public DhcpClientInfomation GetClientInfo()
        {

            var client = new DhcpClientInfomation
            {
                MacAddress = Utils.ByteToString(packet.chaddr, packet.hlen),
                TransactionID = System.BitConverter.ToUInt32(packet.xid, 0)
            };
            if (packet.options.GetOptionData(DhcpOptionType.ClientIdentifier) != null)
                client.ClientIdentifier = System.Text.Encoding.ASCII.GetString(packet.options.GetOptionData(DhcpOptionType.ClientIdentifier));
            if (packet.options.GetOptionData(DhcpOptionType.RequestedIPAddress) != null)
                client.RequestAddress = new IPAddress(packet.options.GetOptionData(DhcpOptionType.ClientIdentifier)).ToString();
            if (packet.options.GetOptionData(DhcpOptionType.ServerIdentifier) != null)
                client.ClientIdentifier = new IPAddress(packet.options.GetOptionData(DhcpOptionType.ServerIdentifier)).ToString();

            return client;
        }
    }
}
