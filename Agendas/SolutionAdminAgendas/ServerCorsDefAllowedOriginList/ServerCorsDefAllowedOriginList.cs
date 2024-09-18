using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class ServerCorsDefAllowedOriginList {
        public int Id { get; set; } = 0;
        public string AllowedDomain { get; set; } = null;
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}