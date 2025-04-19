using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebGlobalPageBlockList {
        public int Id { get; set; } = 0;
        public string InheritedPagePartType { get; set; } = null;
        public int Sequence { get; set; }
        public string Name { get; set; } = null;
        public string Description { get; set; }
        public bool RewriteLowerLevel { get; set; }
        public string GuestHtmlContent { get; set; }
        public string UserHtmlContent { get; set; }
        public string AdminHtmlContent { get; set; }
        public string ProviderHtmlContent { get; set; }

        public bool Active { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}