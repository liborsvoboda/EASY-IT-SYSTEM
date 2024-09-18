using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class BasicCalendarList {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = null;
        public DateTime TimeStamp { get; set; }
    }
}