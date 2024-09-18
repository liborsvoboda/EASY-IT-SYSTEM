using System;
using System.ComponentModel.DataAnnotations;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class WebCodeLibraryList {

        public int Id { get; set; } = 0;
        public string InheritedCodeType { get; set; }
        public string Name { get; set; } = null;
        public string Description { get; set; }
        public string Content { get; set; } = null;
        public bool IsCompletion { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}