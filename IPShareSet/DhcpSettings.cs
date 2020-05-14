using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallDhcpServer
{
    public struct DhcpServerSettings
    {
        public string SubMask;
        public uint LeaseTime;
        public string ServerName;
        public string MyIP;
        public string RouterIP;
        public string DomainIP;
        public string LogServerIP;
    }

    public struct DhcpClientSettings
    {
        public string IPAddr;
        public string SubMask;
        public uint LeaseTime;
        public string MacAddress;
    }
}
