using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionStaticFileList {
        public int Id { get; set; } = 0;
        public int WebsiteId { get; set; }
        public int StaticPathId { get; set; }
        public string FileNamePath { get; set; } = null;
        public string MimeType { get; set; } = null;
        public byte[] Content { get; set; } = null;
        public bool Active { get; set; }
        public int? UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string WebsiteName { get; set; } = null;
    }
}