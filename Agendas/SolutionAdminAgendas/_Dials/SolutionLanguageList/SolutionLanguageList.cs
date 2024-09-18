using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionLanguageList {
        public int Id { get; set; } = 0;
        public string SystemName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Translation { get; set; }
    }
}