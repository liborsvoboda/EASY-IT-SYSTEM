using System;

namespace EasyITSystemCenter.GlobalClasses {

    public class BasicAttachmentList {
        public int Id { get; set; } = 0;
        public int ParentId { get; set; }
        public string ParentType { get; set; } = null;
        public string FileName { get; set; } = null;
        public byte[] Attachment { get; set; } = null;
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public partial class BasicViewAttachmentList {
        public int Id { get; set; }
        public string FileName { get; set; } = null;
        public string PartNumber { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}