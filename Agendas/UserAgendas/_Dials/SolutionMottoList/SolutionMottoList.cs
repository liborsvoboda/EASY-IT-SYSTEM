using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionMottoList {
        public int Id { get; set; } = 0;
        public string SystemName { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Translation { get; set; }
    }
}