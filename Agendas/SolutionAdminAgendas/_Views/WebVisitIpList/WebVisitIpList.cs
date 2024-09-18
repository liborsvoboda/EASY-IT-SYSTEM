using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebVisitIpList {
        public int Id { get; set; } = 0;
        public string WebHostIp { get; set; } = null;
        public string Description { get; set; }
        public string WhoIsInformations { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}