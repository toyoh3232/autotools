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
            
            new Thread(() =>
            {
                var oStyle = mtSet.Style;
                var oText = mtSet.Text;
                this.Invoke(new Action(() => mtSet.Style = "Red"));
                this.Invoke(new Action(() => stoppingTimer.Start()));
                Thread.Sleep(5000);
                this.Invoke(new Action(() => stoppingTimer.Stop()));
                this.Invoke(new Action(() => {
                    mtSet.Style = oStyle;
                    mtSet.Text = oText;
                    }));
            }).Start();
        }
    }
}
