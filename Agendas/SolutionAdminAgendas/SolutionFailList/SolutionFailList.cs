using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionFailList {
        public int Id { get; set; } = 0;
        public string Source { get; set; } = null;
        public string Message { get; set; } = null;

        public string ImageName { get; set; } = null;
        public byte[] Image { get; set; } = null;
        public string AttachmentName { get; set; } = null;
        public byte[] Attachment { get; set; } = null;

        public int? UserId { get; set; } = null;
        public string UserName { get; set; } = null;
        public string LogLevel { get; set; } = null;
        public DateTime TimeStamp { get; set; }
    }
}