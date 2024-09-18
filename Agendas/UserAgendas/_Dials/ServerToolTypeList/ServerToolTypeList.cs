using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class ServerToolTypeList {
        public int Id { get; set; } = 0;
        public int Sequence { get; set; }
        public string Name { get; set; } = null;
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}