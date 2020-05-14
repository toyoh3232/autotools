using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SmallDhcpServer
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
            byte code = (byte)optionType;
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
            catch (Exception ex)
            {
                Console.WriteLine("GetOptionData:" + ex.Message);
            }

            return null;
        }

        internal void CreateOptionStruct(DhcpMessgeType messageType, DhcpSettings info)
        {
            byte[] pReqList, t1, leaseTime, myIp;

            try
            {
                //we look for the parameter request list
                pReqList = GetOptionData(DhcpOptionType.ParameterRequestList);
                //erase the options array, and set the message type to ack
                options = null;
                CreateOptionElement(DhcpOptionType.DHCPMessageType, new byte[] { (byte)messageType }, ref options);
                //server identifier, my IP
                myIp = IPAddress.Parse(info.MyIP).GetAddressBytes();
                CreateOptionElement(DhcpOptionType.ServerIdentifier, myIp, ref options);


                //PReqList contains the option data in a byte that is requested by the unit
                foreach (byte i in pReqList)
                {
                    t1 = null;
                    switch ((DhcpOptionType)i)
                    {
                        case DhcpOptionType.SubnetMask:
                            t1 = IPAddress.Parse(info.SubMask).GetAddressBytes();
                            break;
                        case DhcpOptionType.Router:
                            t1 = IPAddress.Parse(info.RouterIP).GetAddressBytes();
                            break;
                        case DhcpOptionType.DomainNameServer:
                            t1 = IPAddress.Parse(info.DomainIP).GetAddressBytes();
                            break;
                        case DhcpOptionType.DomainName:
                            t1 = System.Text.Encoding.ASCII.GetBytes(info.ServerName);
                            break;
                        case DhcpOptionType.ServerIdentifier:
                            t1 = IPAddress.Parse(info.MyIP).GetAddressBytes();
                            break;
                        case DhcpOptionType.LogServer:
                            t1 = System.Text.Encoding.ASCII.GetBytes(info.LogServerIP);
                            break;
                        case DhcpOptionType.NetBIOSoverTCPIPNameServer:
                            break;

                    }
                    if (t1 != null)
                        CreateOptionElement((DhcpOptionType)i, t1, ref options);
                }

                //lease time
                leaseTime = new byte[4];
                leaseTime[3] = (byte)(info.LeaseTime);
                leaseTime[2] = (byte)(info.LeaseTime >> 8);
                leaseTime[1] = (byte)(info.LeaseTime >> 16);
                leaseTime[0] = (byte)(info.LeaseTime >> 24);
                CreateOptionElement(DhcpOptionType.IPAddressLeaseTime, leaseTime, ref options);
                CreateOptionElement(DhcpOptionType.RenewalTimeValue_T1, leaseTime, ref options);
                CreateOptionElement(DhcpOptionType.RebindingTimeValue_T2, leaseTime, ref options);
                //create the end option
                Array.Resize(ref options, options.Length + 1);
                Array.Copy(new byte[] { 255 }, 0, options, options.Length - 1, 1);
                //send the data to the unit
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateOptionStruct:" + ex.Message);
            }
        }

        public void CreateOptionElement(DhcpOptionType optionType, byte[] dataToAdd, ref byte[] source)
        {
            byte[] newOption;
            try
            {
                newOption = new byte[dataToAdd.Length + 2];
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
            catch (Exception ex)
            {
                Console.WriteLine("CreateOptionElement:" + ex.Message);
            }

        }

    }
}
