using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SystemMenuList {
        public int Id { get; set; } = 0;
        public string MenuType { get; set; } = null;
        public int GroupId { get; set; }
        public string FormPageName { get; set; } = null;
        public string AccessRole { get; set; } = null;
        public string Description { get; set; } = null;
        public bool NotShowInMenu { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public string GroupName { get; set; }
        public string FormTranslate { get; set; }
    }
}