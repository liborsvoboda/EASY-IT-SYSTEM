using System;
using System.ComponentModel.DataAnnotations;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SystemMenuList {
        public int Id { get; set; } = 0;
        public string InheritedSystemMenuType { get; set; } = null;
        public int GroupId { get; set; }
        public string FormPageName { get; set; } = null;
        public string AccessRole { get; set; } = null;
        public string AccessUser { get; set; } = null;
        public string OrderBy { get; set; } = null;
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public bool NotShowInMenu { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public string GroupName { get; set; }
        public string FormTranslate { get; set; }
    }
}