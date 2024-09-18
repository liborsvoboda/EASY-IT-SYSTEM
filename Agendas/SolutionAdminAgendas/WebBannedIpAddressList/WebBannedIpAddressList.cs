using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebBannedIpAddressList {
        public int Id { get; set; } = 0;
        public string IpAddress { get; set; } = null;
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}