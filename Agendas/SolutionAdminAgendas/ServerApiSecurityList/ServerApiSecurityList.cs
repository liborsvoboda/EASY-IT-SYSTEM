using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class ServerApiSecurityList {
        public int Id { get; set; } = 0;
        public string InheritedApiType { get; set; } = null;
        public string Name { get; set; } = null;
        public string Description { get; set; }
        public string UrlSubPath { get; set; }
        public string WriteAllowedRoles { get; set; }
        public string ReadAllowedRoles { get; set; }
        public bool WriteRestrictedAccess { get; set; }
        public bool ReadRestrictedAccess { get; set; }
        public string RedirectPathOnError { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string ApiTypeTranslation { get; set; } = null;
    }
}