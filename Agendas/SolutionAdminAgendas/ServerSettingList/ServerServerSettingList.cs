using System;

namespace EasyITSystemCenter.GlobalClasses {

    public class ServerServerSettingList {
        public int Id { get; set; }
        public string InheritedSrvConfigType { get; set; } = null;
        public string Type { get; set; } = null;
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public string GroupNameTranslation { get; set; } = null;
        public string KeyTranslation { get; set; } = null;
    }
}