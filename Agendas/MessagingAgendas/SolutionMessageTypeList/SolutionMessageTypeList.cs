using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SolutionMessageTypeList {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Variables { get; set; } = null;
        public bool AnswerAllowed { get; set; }
        public bool IsSystemOnly { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Translation { get; set; }
    }

}