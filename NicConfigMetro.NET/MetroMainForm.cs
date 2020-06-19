using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NicConfigMetro.NET
{
    public partial class MetroMainForm : MetroFramework.Forms.MetroForm
    {

        private readonly System.Timers.Timer stoppingTimer;
        public MetroMainForm()
        {
            InitializeComponent();

            stoppingTimer = new System.Timers.Timer
            {
                Interval = 300
            };
            stoppingTimer.Elapsed += (s, e) =>
            {
                if (mtSet.Text.EndsWith("..."))
                    mtSet.Text = mtSet.Text.TrimEnd('.');
                else
                    mtSet.Text += ".";
            };
        }

        private void mtSet_Click(object sender, EventArgs e)
        {
            new Thread(Dowork).Start();
        }

        private void Dowork()
        {
            // set ip for each nic if not set

            // reset nic

            // start dhcp server and fill client nic info nic by nic

            // wait until client netshare directory preparation is ready

            // generate temp folders and cpydelfc batch file  


        }
    }
}
