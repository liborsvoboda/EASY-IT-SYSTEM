using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebSettingList {
        public int Id { get; set; } = 0;
        public string Key { get; set; } = null;
        public string Value { get; set; } = null;
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string KeyDesccription { get; set; } = null;
    }
}