using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SmallDhcpServer
{
    public class DhcpData
    {

        public DHCPServer RelatedServer { get; internal set; }

        private DhcpPacketStruct packet;

        internal DhcpData(byte[] data)
        {
            packet = new DhcpPacketStruct(data);
        }

        public DhcpMessgeType GetDhcpMessageType()
        {
            return packet.GetDhcpMessageType();
        }


        internal byte[] BuildSendData(DhcpMessgeType msgType)
        {
            packet.ApplySettings(msgType, RelatedServer.Settings, RelatedServer.GetAvailibleClientSettings());
            return packet.ToArray();
        }

        public DhcpClientSettings ToClientSettings()
        {
            return new DhcpClientSettings
            {
                MacAddress = Utils.ByteToString(packet.chaddr, packet.hlen)
            };
        }
    }
}
