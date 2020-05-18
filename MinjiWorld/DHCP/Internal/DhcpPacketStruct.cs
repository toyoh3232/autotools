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
            op = 2;
            yiaddr = IPAddress.Parse(clientIp).GetAddressBytes();
            options.CreateOption(msgType, server);
        }
        public byte[] ToArray()
        {
            var mArray = new byte[0];
            AddtoArray(op, ref mArray);
            AddtoArray(htype, ref mArray);
            AddtoArray(hlen, ref mArray);
            AddtoArray(hops, ref mArray);
            AddtoArray(xid, ref mArray);
            AddtoArray(secs, ref mArray);
            AddtoArray(flags, ref mArray);
            AddtoArray(ciaddr, ref mArray);
            AddtoArray(yiaddr, ref mArray);
            AddtoArray(siaddr, ref mArray);
            AddtoArray(giaddr, ref mArray);
            AddtoArray(chaddr, ref mArray);
            AddtoArray(sname, ref mArray);
            AddtoArray(file, ref mArray);
            AddtoArray(cookie, ref mArray);
            AddtoArray(options.options, ref mArray);
            return mArray;
        }

        private void AddtoArray(byte fromValue, ref byte[] targetValue)
        {
            AddtoArray(new byte[] { fromValue }, ref targetValue);
        }

        private void AddtoArray(byte[] fromValue, ref byte[] targetArray)
        {
            try
            {
                if (targetArray != null)
                    Array.Resize(ref targetArray, targetArray.Length + fromValue.Length);
                else
                    Array.Resize(ref targetArray, fromValue.Length);
                Array.Copy(fromValue, 0, targetArray, targetArray.Length - fromValue.Length, fromValue.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{this.GetType().FullName}:{e.Message}");
            }
        }


        public DhcpMessgeType GetDhcpMessageType()
        {
            try
            {
                var data = options.GetOptionData(DhcpOptionType.DHCPMessageType);
                if (data != null)
                {
                    return (DhcpMessgeType)data[0];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{this.GetType().FullName}:{e.Message}");
            }
            return 0;
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
        public Options options;            // options (rest)
        #endregion
    }
}
