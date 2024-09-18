using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class TemplateClassList {
        public int Id { get; set; } = 0;
        public string SystemName { get; set; } = null;
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public partial class TemplateClassListWithParrent {
        public int Id { get; set; } = 0;
        public int ParentId { get; set; }
        public string SystemName { get; set; } = null;
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public string ParentName { get; set; } = null;
    }

    public partial class ExtendedTemplateClassList : TemplateClassList {
        public string Currency { get; set; }
        public string TotalCurrency { get; set; }

        public ExtendedTemplateClassList() {
        }

        public ExtendedTemplateClassList(BasicItemList ch) {
            foreach (var prop in ch.GetType().GetProperties()) { this.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(ch, null), null); }
        }
    }

    public partial class TemplateClassListWithLocalTranslation {
        public int Id { get; set; } = 0;
        public string SystemName { get; set; } = null;
        public string Description { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Translation { get; set; } = null;
    }
}