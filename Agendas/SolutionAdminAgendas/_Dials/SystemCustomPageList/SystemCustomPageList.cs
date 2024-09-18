using System;


namespace EasyITSystemCenter.GlobalClasses {

    public partial class SystemCustomPageList {

        public int Id { get; set; } = 0;
        public string InheritedFormType { get; set; } = null;
        public string PageName { get; set; } = null;
        public string Description { get; set; }
        public bool IsInteractAgenda { get; set; }
        public bool IsSystemUrl { get; set; }
        public bool IsServerUrl { get; set; }
        public bool IsOwnServerUrl { get; set; }
        public bool DevModeEnabled { get; set; }
        public bool ShowHelpTab { get; set; }
        public string InheritedHelpTabSourceType { get; set; }
        public string StartupUrl { get; set; }
        public string HelpTabUrl { get; set; }
        public string DbtableName { get; set; }
        public string ColumnName { get; set; }
        public string InheritedSetName { get; set; }
        public bool UseIooverDom { get; set; }
        public string DomhtmlElementName { get; set; }
        public string SetWebDataJscriptCmd { get; set; }
        public string GetWebDataJscriptCmd { get; set; }
        public string StartupSubFolder { get; set; }
        public string StartupCommand { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}