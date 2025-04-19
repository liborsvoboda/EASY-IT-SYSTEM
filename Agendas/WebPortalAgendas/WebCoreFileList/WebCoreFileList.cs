using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebCoreFileList {
        public int Id { get; set; } = 0;
        public string InheritedJsCssDefinitionType { get; set; } = null;
        public int Sequence { get; set; }
        public string MetroPath { get; set; } = null;
        public string FileName { get; set; } = null;
        public string Description { get; set; }
        public bool RewriteLowerLevel { get; set; }
        public string GuestFileContent { get; set; }
        public string UserFileContent { get; set; }
        public string AdminFileContent { get; set; }
        public string ProviderContent { get; set; }
        public bool IsUniquePath { get; set; }
        public bool AutoUpdateOnSave { get; set; }

        public bool Active { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}