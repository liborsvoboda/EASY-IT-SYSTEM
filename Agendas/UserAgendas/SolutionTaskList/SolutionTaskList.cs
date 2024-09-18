using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionTaskList {
        public int Id { get; set; } = 0;
        public string InheritedTargetType { get; set; }
        public string InheritedStatusType { get; set; }
        public string Message { get; set; } = null;
        public string Documentation { get; set; } = null;

        public string ImageName { get; set; } = null;
        public byte[] Image { get; set; } = null;
        public string AttachmentName { get; set; } = null;
        public byte[] Attachment { get; set; } = null;

        public int? UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}