using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class DocSrvDocumentationGroupList {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = null;
        public int Sequence { get; set; }
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}