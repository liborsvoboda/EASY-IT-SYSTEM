using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class ServerStaticOrMvcDefPathList {
        public int Id { get; set; } = 0;
        public string SystemName { get; set; } = null;
        public string WebRootSubPath { get; set; } = null;
        public string AliasPath { get; set; }
        public string Description { get; set; }
        public bool IsBrowsable { get; set; }
        public bool IsStaticOrMvcDefOnly { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}