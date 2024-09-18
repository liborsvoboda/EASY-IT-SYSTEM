using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class ServerLiveDataMonitorList {
        public int Id { get; set; } = 0;
        public string RootPath { get; set; } = null;
        public string FileExtensions { get; set; } = null;
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}