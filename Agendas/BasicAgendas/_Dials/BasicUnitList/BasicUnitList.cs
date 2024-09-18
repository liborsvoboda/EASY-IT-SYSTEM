using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class BasicUnitList {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = null;
        public string Description { get; set; } = null;
        public bool Default { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}