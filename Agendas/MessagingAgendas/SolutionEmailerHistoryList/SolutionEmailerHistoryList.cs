using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionEmailerHistoryList {
        public int Id { get; set; } = 0;
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
    }

}