using System;

namespace EasyITSystemCenter.GlobalClasses {

    public class BasicImageGalleryList {
        public int Id { get; set; } = 0;
        public bool IsPrimary { get; set; }
        public string FileName { get; set; }
        public byte[] Attachment { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}