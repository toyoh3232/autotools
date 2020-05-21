using System.Net;
using MinjiWorld.DHCP.Extension;

namespace MinjiWorld.DHCP
{
    public struct DhcpServerSettings
    {
        public DhcpServerSettings(string ip)
        {
            ServerIp = IPAddress.Parse(ip);
            SubnetMask = IPAddress.Parse(ip).GetSubnetMask() ;
            LeaseTime = 20000;
            ServerName = "Little DHCP Server";
            RouterIp = "0.0.0.0";
            DomainIp = "0.0.0.0";
            LogServerIp = "0.0.0.0";
        }
        public IPAddress ServerIp;
        public IPAddress SubnetMask;
        public uint LeaseTime;
        public string ServerName;
        public string RouterIp;
        public string DomainIp;
        public string LogServerIp;
    }
}