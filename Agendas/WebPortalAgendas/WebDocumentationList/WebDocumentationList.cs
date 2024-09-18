using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebDocumentationList {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = null;
        public int Sequence { get; set; }
        public string Description { get; set; } = null;
        public string MdContent { get; set; } = "";
        public string HtmlContent { get; set; } = "";
        public int UserId { get; set; }
        public bool Active { get; set; }
        public int AutoVersion { get; set; } = 0;
        public DateTime TimeStamp { get; set; }
    }
}