namespace MinjiWorld.DHCP
{
    public struct DhcpServerSettings
    {
        public DhcpServerSettings(string ip)
        {
            ServerIp = ip;
            SubMask = Utils.GetIPv4Mask(ip);
            LeaseTime = 2000;
            ServerName = "Little DHCP Server";
            RouterIp = "0.0.0.0";
            DomainIp = "0.0.0.0";
            LogServerIp = "0.0.0.0";
        }
        public string SubMask;
        public uint LeaseTime;
        public string ServerName;
        public string ServerIp;
        public string RouterIp;
        public string DomainIp;
        public string LogServerIp;
    }
}