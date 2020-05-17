namespace IPShareSet
{
    public struct DhcpServerSettings
    {
        public string SubMask;
        public uint LeaseTime;
        public string ServerName;
        public string ServerIp;
        public string RouterIp;
        public string DomainIp;
        public string LogServerIp;
    }

    public struct DhcpClientSettings
    {
        public string IpAddress;
        public string SubMask;
        public uint LeaseTime;
        public string MacAddress;
        public bool IsAllocated;
    }
}