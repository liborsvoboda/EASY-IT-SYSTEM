using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionMessageModuleList {
        public int Id { get; set; } = 0;
        public int Level { get; set; } = 0;
        public int? MessageParentId { get; set; }
        public int MessageTypeId { get; set; }
        public string Subject { get; set; }
        public string HtmlMessage { get; set; }
        public bool Shown { get; set; }
        public bool Archived { get; set; }
        public bool IsSystemMessage { get; set; } = true;
        public bool Published { get; set; }
        public int? GuestId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string ParrentMessage { get; set; }
        public string ParentMessageSubject { get; set; }
        public string MessageTypeTranslation { get; set; }
        public string GuestEmail { get; set; }
    }

}