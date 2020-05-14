using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallDhcpServer
{
    public class DhcpSettings
    {
        #region Exported Property
        public string IPAddr { get; set; }
        public string SubMask { get; set; }
        public uint LeaseTime { get; set; }
        public string ServerName { get; set; }
        public string MyIP { get; set; }
        public string RouterIP { get; set; }
        public string DomainIP { get; set; }
        public string LogServerIP { get; set; }
        #endregion
    }
}
