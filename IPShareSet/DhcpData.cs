using SmallDHCPServer;

namespace IPShareSet
{
    public class DhcpData
    {
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

        internal byte[] BuildSendData(DhcpMessgeType msgType)
        {
            packet.ApplySettings(msgType, RelatedServer.Settings, RelatedServer.GetRandomClientSettings());
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
