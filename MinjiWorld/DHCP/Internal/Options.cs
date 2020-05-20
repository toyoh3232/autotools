using System;
using System.Net;
using System.Reflection;
using System.Text;

namespace MinjiWorld.DHCP.Internal
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
            Console.WriteLine($"length:{options.Length},code:{code}");
            try
            {
                //loop through look for the bit that states that the identifier is there
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"i:{i}");
                    if (options[i] == (byte)DhcpOptionType.End) break;
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
                Console.WriteLine($"{this.GetType().FullName}.GetOptionData:{e.Message}");
            }

            return null;
        }

        internal void ApplyOptionSettings(DhcpMessageType messageType, DhcpServerSettings server)
        {
            try
            {
                var oldMsgType =(DhcpMessageType)GetOptionData(DhcpOptionType.DHCPMessageType)[0];

                //erase the options array, and set the message type to ack
                byte[] newOptions = null;
                CreateOptionElement(DhcpOptionType.DHCPMessageType, new byte[] { (byte)messageType }, ref newOptions);
                //server identifier, my IP
                var myIp = IPAddress.Parse(server.ServerIp).GetAddressBytes();
                CreateOptionElement(DhcpOptionType.ServerIdentifier, myIp, ref newOptions);

                //lease time
                var leaseTime = new byte[4];
                leaseTime[3] = (byte)(server.LeaseTime);
                leaseTime[2] = (byte)(server.LeaseTime >> 8);
                leaseTime[1] = (byte)(server.LeaseTime >> 16);
                leaseTime[0] = (byte)(server.LeaseTime >> 24);
                switch (messageType)
                {
                    case DhcpMessageType.DHCP_NAK:
                        goto EndOption;
                    case DhcpMessageType.DHCP_OFFER:

                        CreateOptionElement(DhcpOptionType.IPAddressLeaseTime, leaseTime, ref newOptions);
                        CreateOptionElement(DhcpOptionType.RenewalTimeValue_T1, leaseTime, ref newOptions);
                        CreateOptionElement(DhcpOptionType.RebindingTimeValue_T2, leaseTime, ref newOptions);
                        break;
                    case DhcpMessageType.DHCP_ACK:
                        if (oldMsgType == DhcpMessageType.DHCP_INFORM) break;
                        CreateOptionElement(DhcpOptionType.IPAddressLeaseTime, leaseTime, ref newOptions);
                        CreateOptionElement(DhcpOptionType.RenewalTimeValue_T1, leaseTime, ref newOptions);
                        CreateOptionElement(DhcpOptionType.RebindingTimeValue_T2, leaseTime, ref newOptions);
                        break;
                }
                // look for the parameter request list
                var reqList = GetOptionData(DhcpOptionType.ParameterRequestList);
                // reqList contains the option data in a byte that is requested by the unit
                foreach (var i in reqList)
                {
                    byte[] t1 = null;
                    switch ((DhcpOptionType) i)
                    {
                        case DhcpOptionType.SubnetMask:
                            t1 = IPAddress.Parse(server.SubMask).GetAddressBytes();
                            break;
                        case DhcpOptionType.Router:
                            t1 = IPAddress.Parse(server.RouterIp).GetAddressBytes();
                            break;
                        case DhcpOptionType.DomainNameServer:
                            t1 = IPAddress.Parse(server.DomainIp).GetAddressBytes();
                            break;
                        case DhcpOptionType.DomainName:
                            t1 = Encoding.ASCII.GetBytes(server.ServerName);
                            break;
                        case DhcpOptionType.LogServer:
                            t1 = Encoding.ASCII.GetBytes(server.LogServerIp);
                            break;
                    }

                    if (t1 != null)
                        CreateOptionElement((DhcpOptionType)i, t1, ref newOptions);
                }
                EndOption:
                //create the end option
                Array.Resize(ref newOptions, newOptions.Length + 1);
                Array.Copy(new byte[] { (byte)DhcpOptionType.End }, 0, newOptions, newOptions.Length - 1, 1);
                options = newOptions;
            }
            catch (Exception e)
            {
                Console.WriteLine($@"{GetType().FullName}.{MethodBase.GetCurrentMethod()}:{e.Message}");
            }
        }

        internal void CreateOptionElement(DhcpOptionType optionType, byte[] dataToAdd, ref byte[] source)
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
                Console.WriteLine($"{GetType().FullName}.CreateOptionElement:{e.Message}");
            }
        }
    }
}
