using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class ServerModuleAndServiceList {
        public int Id { get; set; } = 0;
        public string InheritedPageType { get; set; } = null;
        public string Name { get; set; } = null;
        public string InheritedLayoutType { get; set; } = null;
        public string Description { get; set; }
        public string UrlSubPath { get; set; } = null;
        public string OptionalConfiguration { get; set; }
        public string AllowedRoles { get; set; } = null;
        public bool RestrictedAccess { get; set; }
        public string RedirectPathOnError { get; set; }
        public string CustomHtmlContent { get; set; }
        public bool IsLoginModule { get; set; }

        public bool PathSetAllowed { get; set; }
        public bool RestrictionSetAllowed { get; set; }
        public bool HtmlSetAllowed { get; set; }
        public bool RedirectSetAllowed { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string PageTypeTranslation { get; set; } = null;
        public string LayoutTypeTranslation { get; set; } = "";
    }
}