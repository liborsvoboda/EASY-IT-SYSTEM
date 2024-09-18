using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionWebsiteList {
        public int Id { get; set; } = 0;
        public string WebsiteName { get; set; } = null;
        public string Description { get; set; }
        public int MinimalReadAccessValue { get; set; }
        public int MinimalWriteAccessValue { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public string MinimalReadAccessRole { get; set; }
        public string MinimalWriteAccessRole { get; set; }

    }
}