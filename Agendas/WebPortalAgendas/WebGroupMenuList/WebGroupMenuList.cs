using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebGroupMenuList {
        public int Id { get; set; } = 0;
        public int Sequence { get; set; }
        public string Onclick { get; set; } = null;
        public string Name { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}