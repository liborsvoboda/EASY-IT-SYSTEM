using System;

namespace EasyITSystemCenter.GlobalClasses {

    public partial class ServerHealthCheckTaskList {
        public int Id { get; set; } = 0;
        public string TaskName { get; set; } = null;
        public string Type { get; set; } = null;
        public string ServerDrive { get; set; } = null;
        public string FolderPath { get; set; } = null;
        public string DbSqlConnection { get; set; } = null;
        public string IpAddress { get; set; } = null;
        public string ServerUrlPath { get; set; } = null;
        public string UrlPath { get; set; } = null;
        public int? SizeMb { get; set; } = null;
        public int? Port { get; set; } = null;
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}