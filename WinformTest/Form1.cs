using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WinformTest
{
    public partial class Form1 : Form
    {
        private BindingList<string> ds = new BindingList<string> { "a", "b", "c" };

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = ds;
            comboBox2.DataSource = ds;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds.Add("d");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds.Remove("a");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetIPv4Mask("133.113.87.48"));
        }
        public static string GetIPv4Mask(string address)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(unicastIPAddressInformation.Address.ToString()))
                        {
                            return unicastIPAddressInformation.IPv4Mask.ToString();
                        }
                    }
                }
            }
            return null;
        }
    }
}
