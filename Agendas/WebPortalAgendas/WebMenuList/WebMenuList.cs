using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebMenuList {
        public int Id { get; set; } = 0;
        public int GroupId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string MenuClass { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; } = "";
        public bool UserMenu { get; set; }
        public bool AdminMenu { get; set; }
        public string UserIpaddress { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public bool Default { get; set; }
        public DateTime TimeStamp { get; set; }

        public string GroupName { get; set; }
    }
}