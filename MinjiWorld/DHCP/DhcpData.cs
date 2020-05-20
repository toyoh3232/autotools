using MinjiWorld.DHCP.Internal;
using System;
using System.Net;
using System.Text;

namespace MinjiWorld.DHCP
{
    public class DhcpData
    {
        public struct DhcpClientInformation
        {
            public string RequestAddress;
            public string ClientIdentifier;
            public string ServerAddress;
            public string ClientAddress;
            public string MacAddress;
            public uint TransactionId;
        }

        public bool IsBuiltToBeSent { get; private set; }

        public DhcpServer RelatedServer { get; internal set; }

        private DhcpPacketStruct packet;

        internal DhcpData(byte[] data)
        {
            packet = new DhcpPacketStruct(data);
            IsBuiltToBeSent = false;
        }

        public DhcpMessageType GetCurrentMessageType()
        {
            var data = packet.options.GetOptionData(DhcpOptionType.DHCPMessageType);
            //　TODO
            if (IsBuiltToBeSent) throw new Exception();
            // TODO
            if (data == null) throw new Exception();
            return (DhcpMessageType)data[0];
        }

        internal byte[] BuildSendData(DhcpMessageType msgType, string clientIp)
        {
            if (IsBuiltToBeSent) throw new Exception();
            packet.ApplySettings(msgType, RelatedServer.Settings, clientIp);
            IsBuiltToBeSent = true;
            return packet.ToArray();
            // TODO

        }
        


        public DhcpClientInformation GetClientInfo()
        {

            var client = new DhcpClientInformation
            {
                MacAddress = Utils.ByteToString(packet.chaddr, packet.hlen),
                ClientAddress = new IPAddress(packet.ciaddr).ToString(),
                TransactionId = BitConverter.ToUInt32(packet.xid, 0)
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
