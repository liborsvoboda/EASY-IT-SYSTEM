using System;
using System.Windows.Media.Imaging;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class SystemModuleList {
        public int Id { get; set; } = 0;
        public string ModuleType { get; set; } = null;
        public string Name { get; set; } = null;
        public string FolderPath { get; set; } = null;
        public string FileName { get; set; } = null;
        public string StartupCommand { get; set; }
        public string Description { get; set; }
        public string ForegroundColor { get; set; } = null;
        public string BackgroundColor { get; set; } = null;
        public string IconName { get; set; } = null;
        public string IconColor { get; set; } = null;
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }

        public BitmapImage BitmapImage { get; set; }
        public string ModuleTypeName { get; set; }
    }
}