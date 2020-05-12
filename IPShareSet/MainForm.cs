using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SmallDHCPServer_C;

namespace IPShareSet
{
    public partial class MainForm : Form
    {
        DHCP server;
        private Thread dHCPThread = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server = new DHCP("0.0.0.0");
            server.Request += (CDHCPStruct d_DHCP, string MacId) => textBox1.Text = textBox1.Text + "\n" + MacId;
            dHCPThread = new Thread(server.StartDHCPServer);
            dHCPThread.Start();
        }
    }
}
