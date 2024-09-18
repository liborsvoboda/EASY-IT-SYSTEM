using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionSchedulerList {
        public int Id { get; set; } = 0;
        public string InheritedGroupName { get; set; } = null;
        public string Name { get; set; } = null;
        public int Sequence { get; set; }
        public string Email { get; set; }
        public string Data { get; set; } = null;
        public string Description { get; set; }
        public bool StartNowOnly { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? FinishAt { get; set; }
        public int Interval { get; set; }
        public string InheritedIntervalType { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public string GroupNameTranslation { get; set; } = null;
        public string IntervalTypeTranslation { get; set; } = null;
    }


    public partial class SolutionSchedulerProcessList {
        public int Id { get; set; }
        public int ScheduledTaskId { get; set; }
        public string ProcessData { get; set; }
        public string ProcessLog { get; set; }
        public bool ProcessCrashed { get; set; }
        public bool ProcessCompleted { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

