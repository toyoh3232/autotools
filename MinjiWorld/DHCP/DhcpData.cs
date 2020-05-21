using MinjiWorld.DHCP.Extension;
using MinjiWorld.DHCP.Internal;
using System;
using System.Net;
using System.Text;

namespace MinjiWorld.DHCP
{
    public class DhcpData
    {
        public struct ClientInfomation
        {
            public IPAddress RequestAddress;
            public IPAddress ServerAddress;
            public IPAddress ClientAddress;
            public IPAddress YourAddress;
            public IPAddress RelayAgentAddress;
            public string ClientIdentifier;
            public string MacAddress;
            public uint TransactionID;
            public BroadCastType CastType;
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

        internal byte[] BuildSendData(DhcpMessgeType msgType, IPAddress clientIp)
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
        


        public ClientInfomation GetClientInfo()
        {
            // TODO
            if (IsBuiltTobeSent) throw new Exception();

            var client = new ClientInfomation
            {
                MacAddress = packet.chaddr.ToString(packet.hlen),
                ClientAddress = new IPAddress(packet.ciaddr),
                YourAddress = new IPAddress(packet.yiaddr),
                TransactionID = BitConverter.ToUInt32(packet.xid, 0),
                RelayAgentAddress = new IPAddress(packet.giaddr),
                CastType = (BroadCastType)packet.flags[0]
                
            };
            
            if (packet.options.GetOptionData(DhcpOptionType.ClientIdentifier) != null)
                client.ClientIdentifier = Encoding.Default.GetString(packet.options.GetOptionData(DhcpOptionType.ClientIdentifier));
            if (packet.options.GetOptionData(DhcpOptionType.RequestedIPAddress) != null)
                client.RequestAddress = new IPAddress(packet.options.GetOptionData(DhcpOptionType.RequestedIPAddress));
            if (packet.options.GetOptionData(DhcpOptionType.ServerIdentifier) != null)
                client.ServerAddress = new IPAddress(packet.options.GetOptionData(DhcpOptionType.ServerIdentifier));

            return client;
        }
    }
}
