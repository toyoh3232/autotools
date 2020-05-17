using System;
using System.Net;
using System.Text;
using SmallDHCPServer;

namespace IPShareSet
{
    internal class Options
    {
        internal byte[] options;

        public Options(byte[] data)
        {
            this.options = data;
        }

        internal byte[] GetOptionData(DhcpOptionType optionType)
        {
            var code = (byte)optionType;
            try
            {
                //loop through look for the bit that states that the identifier is there
                for (int i = 0; i < options.Length; i++)
                {
                    //at the start we have the code + length
                    //i has the code, i+1 = length of data, i+1+n = data skip
                    if (options[i] == code)
                    {
                        var len = options[i + 1];
                        var data = new byte[len];
                        Array.Copy(options, i + 2, data, 0, len);
                        return data;
                    }
                    else
                    {
                        // jump to next option message
                        i += 1 + options[i + 1];
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{this.GetType().FullName}:{e.Message}");
            }

            return null;
        }

        internal void CreateOptionStruct(DhcpMessgeType messageType, DhcpServerSettings server)
        {
            try
            {
                //we look for the parameter request list
                var reqList = GetOptionData(DhcpOptionType.ParameterRequestList);
                //erase the options array, and set the message type to ack
                options = null;
                CreateOptionElement(DhcpOptionType.DHCPMessageType, new byte[] { (byte)messageType }, ref options);
                //server identifier, my IP
                var myIp = IPAddress.Parse(server.MyIP).GetAddressBytes();
                CreateOptionElement(DhcpOptionType.ServerIdentifier, myIp, ref options);


                //PReqList contains the option data in a byte that is requested by the unit
                foreach (var i in reqList)
                {
                    byte[] t1 = null;
                    switch ((DhcpOptionType)i)
                    {
                        case DhcpOptionType.SubnetMask:
                            t1 = IPAddress.Parse(server.SubMask).GetAddressBytes();
                            break;
                        case DhcpOptionType.Router:
                            t1 = IPAddress.Parse(server.RouterIP).GetAddressBytes();
                            break;
                        case DhcpOptionType.DomainNameServer:
                            t1 = IPAddress.Parse(server.DomainIP).GetAddressBytes();
                            break;
                        case DhcpOptionType.DomainName:
                            t1 = Encoding.ASCII.GetBytes(server.ServerName);
                            break;
                        case DhcpOptionType.ServerIdentifier:
                            t1 = IPAddress.Parse(server.MyIP).GetAddressBytes();
                            break;
                        case DhcpOptionType.LogServer:
                            t1 = Encoding.ASCII.GetBytes(server.LogServerIP);
                            break;
                        case DhcpOptionType.NetBIOSoverTCPIPNameServer:
                            break;

                        case DhcpOptionType.Pad:
                            break;
                        case DhcpOptionType.TimeOffset:
                            break;
                        case DhcpOptionType.TimeServer:
                            break;
                        case DhcpOptionType.NameServer:
                            break;
                        case DhcpOptionType.CookieServer:
                            break;
                        case DhcpOptionType.LPRServer:
                            break;
                        case DhcpOptionType.ImpressServer:
                            break;
                        case DhcpOptionType.ResourceLocServer:
                            break;
                        case DhcpOptionType.HostName:
                            break;
                        case DhcpOptionType.BootFileSize:
                            break;
                        case DhcpOptionType.MeritDump:
                            break;
                        case DhcpOptionType.SwapServer:
                            break;
                        case DhcpOptionType.RootPath:
                            break;
                        case DhcpOptionType.ExtensionsPath:
                            break;
                        case DhcpOptionType.IpForwarding:
                            break;
                        case DhcpOptionType.NonLocalSourceRouting:
                            break;
                        case DhcpOptionType.PolicyFilter:
                            break;
                        case DhcpOptionType.MaximumDatagramReAssemblySize:
                            break;
                        case DhcpOptionType.DefaultIPTimeToLive:
                            break;
                        case DhcpOptionType.PathMTUAgingTimeout:
                            break;
                        case DhcpOptionType.PathMTUPlateauTable:
                            break;
                        case DhcpOptionType.InterfaceMTU:
                            break;
                        case DhcpOptionType.AllSubnetsAreLocal:
                            break;
                        case DhcpOptionType.BroadcastAddress:
                            break;
                        case DhcpOptionType.PerformMaskDiscovery:
                            break;
                        case DhcpOptionType.MaskSupplier:
                            break;
                        case DhcpOptionType.PerformRouterDiscovery:
                            break;
                        case DhcpOptionType.RouterSolicitationAddress:
                            break;
                        case DhcpOptionType.StaticRoute:
                            break;
                        case DhcpOptionType.TrailerEncapsulation:
                            break;
                        case DhcpOptionType.ARPCacheTimeout:
                            break;
                        case DhcpOptionType.EthernetEncapsulation:
                            break;
                        case DhcpOptionType.TCPDefaultTTL:
                            break;
                        case DhcpOptionType.TCPKeepaliveInterval:
                            break;
                        case DhcpOptionType.TCPKeepaliveGarbage:
                            break;
                        case DhcpOptionType.NetworkInformationServiceDomain:
                            break;
                        case DhcpOptionType.NetworkInformationServers:
                            break;
                        case DhcpOptionType.NetworkTimeProtocolServers:
                            break;
                        case DhcpOptionType.VendorSpecificInformation:
                            break;
                        case DhcpOptionType.NetBIOSoverTCPIPDatagramDistributionServer:
                            break;
                        case DhcpOptionType.NetBIOSoverTCPIPNodeType:
                            break;
                        case DhcpOptionType.NetBIOSoverTCPIPScope:
                            break;
                        case DhcpOptionType.XWindowSystemFontServer:
                            break;
                        case DhcpOptionType.XWindowSystemDisplayManager:
                            break;
                        case DhcpOptionType.RequestedIPAddress:
                            break;
                        case DhcpOptionType.IPAddressLeaseTime:
                            break;
                        case DhcpOptionType.OptionOverload:
                            break;
                        case DhcpOptionType.DHCPMessageType:
                            break;
                        case DhcpOptionType.ParameterRequestList:
                            break;
                        case DhcpOptionType.Message:
                            break;
                        case DhcpOptionType.MaximumDHCPMessageSize:
                            break;
                        case DhcpOptionType.RenewalTimeValue_T1:
                            break;
                        case DhcpOptionType.RebindingTimeValue_T2:
                            break;
                        case DhcpOptionType.Vendorclassidentifier:
                            break;
                        case DhcpOptionType.ClientIdentifier:
                            break;
                        case DhcpOptionType.NetworkInformationServicePlusDomain:
                            break;
                        case DhcpOptionType.NetworkInformationServicePlusServers:
                            break;
                        case DhcpOptionType.TFTPServerName:
                            break;
                        case DhcpOptionType.BootfileName:
                            break;
                        case DhcpOptionType.MobileIPHomeAgent:
                            break;
                        case DhcpOptionType.SMTPServer:
                            break;
                        case DhcpOptionType.POP3Server:
                            break;
                        case DhcpOptionType.NNTPServer:
                            break;
                        case DhcpOptionType.DefaultWWWServer:
                            break;
                        case DhcpOptionType.DefaultFingerServer:
                            break;
                        case DhcpOptionType.DefaultIRCServer:
                            break;
                        case DhcpOptionType.StreetTalkServer:
                            break;
                        case DhcpOptionType.STDAServer:
                            break;
                        case DhcpOptionType.End:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    if (t1 != null)
                        CreateOptionElement((DhcpOptionType)i, t1, ref options);
                }

                //lease time
                var leaseTime = new byte[4];
                leaseTime[3] = (byte)(server.LeaseTime);
                leaseTime[2] = (byte)(server.LeaseTime >> 8);
                leaseTime[1] = (byte)(server.LeaseTime >> 16);
                leaseTime[0] = (byte)(server.LeaseTime >> 24);
                CreateOptionElement(DhcpOptionType.IPAddressLeaseTime, leaseTime, ref options);
                CreateOptionElement(DhcpOptionType.RenewalTimeValue_T1, leaseTime, ref options);
                CreateOptionElement(DhcpOptionType.RebindingTimeValue_T2, leaseTime, ref options);
                //create the end option
                Array.Resize(ref options, options.Length + 1);
                Array.Copy(new byte[] { 255 }, 0, options, options.Length - 1, 1);
                //send the data to the unit
            }
            catch (Exception e)
            {
                Console.WriteLine($"{this.GetType().FullName}:{e.Message}");
            }
        }

        public void CreateOptionElement(DhcpOptionType optionType, byte[] dataToAdd, ref byte[] source)
        {
            try
            {
                var newOption = new byte[dataToAdd.Length + 2];
                //add the code, and data length
                newOption[0] = (byte)optionType;
                newOption[1] = (byte)dataToAdd.Length;
                //add the code to put in
                Array.Copy(dataToAdd, 0, newOption, 2, dataToAdd.Length);
                //copy the data to the out array
                if (source == null)
                    Array.Resize(ref source, newOption.Length);
                else
                    Array.Resize(ref source, source.Length + newOption.Length);
                Array.Copy(newOption, 0, source, source.Length - newOption.Length, newOption.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{this.GetType().FullName}:{e.Message}");
            }
        }
    }
}
