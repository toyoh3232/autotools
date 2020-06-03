using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CpyFcDel.NET
{
    class ElapsedTimer : System.Windows.Forms.Timer
    {
        public double ElapsedSeconds { get; private set; }

        public new void Start()
        {
            ElapsedSeconds = 0;
            base.Start();
        }

        public void Resume()
        {
            base.Start();
        }

        protected override void OnTick(EventArgs e)
        {
            ElapsedSeconds++;
            base.OnTick(e);
        }
    }


}
