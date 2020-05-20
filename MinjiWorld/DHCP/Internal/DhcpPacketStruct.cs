using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace MinjiWorld.DHCP.Internal
{
    internal struct DhcpPacketStruct
    {
        internal const int OptionOffset = 240;

        public DhcpPacketStruct(byte[] data) : this()
        {
            try
            {
                using (var stream = new MemoryStream(data, 0, data.Length))
                using (var reader = new BinaryReader(stream))
                {
                    op = reader.ReadByte();
                    htype = reader.ReadByte();
                    hlen = reader.ReadByte();
                    hops = reader.ReadByte();
                    xid = reader.ReadBytes(4);
                    secs = reader.ReadBytes(2);
                    flags = reader.ReadBytes(2);
                    ciaddr = reader.ReadBytes(4);
                    yiaddr = reader.ReadBytes(4);
                    siaddr = reader.ReadBytes(4);
                    giaddr = reader.ReadBytes(4);
                    chaddr = reader.ReadBytes(16);
                    sname = reader.ReadBytes(64);
                    file = reader.ReadBytes(128);
                    cookie = reader.ReadBytes(4);
                    options = new Options(reader.ReadBytes(data.Length - OptionOffset));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{this.GetType().FullName}:{e.Message}");
            }

        }

        public void ApplySettings(DhcpMessgeType msgType, DhcpServerSettings server, string clientIp)
        {
           
            switch (msgType)
            {
                case DhcpMessgeType.DHCP_OFFER:
                    op = (byte)BootMessageType.BootReply;
                    // htype
                    // hlen
                    hops = 0;
                    // xid from client DHCPDISCOVER message
                    Utils.FillZero(secs);
                    Utils.FillZero(ciaddr);
                    yiaddr = IPAddress.Parse(clientIp).GetAddressBytes();
                    siaddr = IPAddress.Parse("0.0.0.0").GetAddressBytes();
                    // flags from client DHCPDISCOVER message
                    // giaddr from client DHCPDISCOVER message
                    // chaddr from client DHCPDISCOVER message
                    Utils.FillZero(sname);
                    Utils.FillZero(file);
                    options.ApplyOptionSettings(msgType, server);
                    break;
                case DhcpMessgeType.DHCP_ACK:
                    op = (byte)BootMessageType.BootReply;
                    // htype
                    // hlen 
                    hops = 0;
                    // xid from client DHCPREQUEST message
                    Utils.FillZero(secs);
                    Utils.FillZero(ciaddr);
                    yiaddr = IPAddress.Parse(clientIp).GetAddressBytes();
                    siaddr = IPAddress.Parse("0.0.0.0").GetAddressBytes();
                    // flags from client DHCPREQUEST message
                    // giaddr from client DHCPREQUEST message
                    // chaddr from client DHCPREQUEST message
                    Utils.FillZero(sname);
                    Utils.FillZero(file);
                    options.ApplyOptionSettings(msgType, server);
                    break;
                case DhcpMessgeType.DHCP_NAK:
                    op = (byte)BootMessageType.BootReply;
                    // htype
                    // hlen 
                    hops = 0;
                    // xid from client DHCPREQUEST message
                    Utils.FillZero(secs);
                    // ciaddr from client DHCPREQUEST message
                    Utils.FillZero(yiaddr);
                    Utils.FillZero(siaddr);
                    // flags from client DHCPREQUEST message
                    // giaddr from client DHCPREQUEST message
                    // chaddr from client DHCPREQUEST message
                    Utils.FillZero(sname);
                    Utils.FillZero(file);
                    options.ApplyOptionSettings(msgType, server);
                    break;
            }
        }
        
        public byte[] ToArray()
        {
            var mArray = new byte[0];
            Utils.AddtoArray(op, ref mArray);
            Utils.AddtoArray(htype, ref mArray);
            Utils.AddtoArray(hlen, ref mArray);
            Utils.AddtoArray(hops, ref mArray);
            Utils.AddtoArray(xid, ref mArray);
            Utils.AddtoArray(secs, ref mArray);
            Utils.AddtoArray(flags, ref mArray);
            Utils.AddtoArray(ciaddr, ref mArray);
            Utils.AddtoArray(yiaddr, ref mArray);
            Utils.AddtoArray(siaddr, ref mArray);
            Utils.AddtoArray(giaddr, ref mArray);
            Utils.AddtoArray(chaddr, ref mArray);
            Utils.AddtoArray(sname, ref mArray);
            Utils.AddtoArray(file, ref mArray);
            Utils.AddtoArray(cookie, ref mArray);
            Utils.AddtoArray(options.options, ref mArray);
            return mArray;
        }

    #region Data
        public byte op;           // Op code:   1 = bootRequest, 2 = BootReply
        public byte htype;        // Hardware Address Type: 1 = 10MB ethernet
        public byte hlen;         // hardware address length: length of MACID
        public byte hops;         // Hw options
        public byte[] xid;        // transaction id (5)
        public byte[] secs;       // elapsed time from trying to boot (3)
        public byte[] flags;      // flags (3)
        public byte[] ciaddr;     // client IP (5)
        public byte[] yiaddr;     // your client IP (5)
        public byte[] siaddr;     // Server IP  (5)
        public byte[] giaddr;     // relay agent IP (5)
        public byte[] chaddr;     // Client HW address (16)
        public byte[] sname;      // Optional server host name (64)
        public byte[] file;       // Boot file name (128)
        public byte[] cookie;     // Magic cookie (4)
        public Options options;   // options (rest)
        #endregion
    }
}
