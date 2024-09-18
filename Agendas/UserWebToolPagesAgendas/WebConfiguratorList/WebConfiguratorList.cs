using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebConfiguratorList {

        public int Id { get; set; } = 0;
        public string Name { get; set; } = null;
        public bool IsStartupPage { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public string ServerUrl { get; set; }
        public string AuthRole { get; set; }
        public bool AuthIgnore { get; set; }
        public bool AuthRedirect { get; set; }
        public string AuthRedirectUrl { get; set; }
        public string IncludedIdList { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}