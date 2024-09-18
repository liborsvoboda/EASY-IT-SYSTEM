using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class DocSrvDocTemplateList {
        public int Id { get; set; } = 0;
        public string InheritedCodeType { get; set; } = null;
        public int GroupId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; } = null;
        public string Description { get; set; }
        public string Template { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string GroupName { get; set; } = null;
    }
}