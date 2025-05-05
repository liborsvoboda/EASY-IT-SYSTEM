using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SystemParameterList {
        public int Id { get; set; } = 0;
        public int? UserId { get; set; } = null;
        public string SystemName { get; set; } = null;
        public string Value { get; set; }
        public string InheritedDataType { get; set; } = null;
        public string Description { get; set; } = null;
        public DateTime TimeStamp { get; set; }

        public string Translation { get; set; } = null;
        public string User { get; set; } = null;
    }
}