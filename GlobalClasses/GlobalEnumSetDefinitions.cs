using EasyITSystemCenter.GlobalClasses;
using System.Collections.ObjectModel;

namespace EasyITSystemCenter.GlobalClasses {


    //TODO move ALL to mixed Enums
    internal class SystemLocalEnumSets {

        /// <summary>
        /// Client Setting Offline enum of Languages
        /// </summary>
        public static ObservableCollection<TranslateSet> Languages = new ObservableCollection<TranslateSet>() {
                                                                new TranslateSet() { Name = "System", Value = "system" },
                                                                new TranslateSet() { Name =  "czech", Value = "cs-CZ" },
                                                                new TranslateSet() { Name = "English", Value = "en-US" }
                                                             };
        /// <summary>
        /// Client Setting offline enum Updater States
        /// </summary>
        public static ObservableCollection<TranslateSet> UpdateVariants = new ObservableCollection<TranslateSet>() {
                                                                new TranslateSet() { Name = "never", Value = "never" },
                                                                new TranslateSet() { Name = "showInfo", Value = "showInfo"},
                                                                new TranslateSet() { Name ="automaticDownload", Value = "automaticDownload"},
                                                                new TranslateSet() { Name ="automaticInstall", Value = "automaticInstall"}
                                                             };
        /// <summary>
        /// Client Setting offline enum Menu Groups
        /// </summary>
        public static ObservableCollection<TranslateSet> ConfigTypes = new ObservableCollection<TranslateSet>() {
                                                                new TranslateSet() { Name ="connection", Value = "connection" },
                                                                new TranslateSet() { Name ="system", Value = "system"},
                                                                new TranslateSet() { Name ="appearance", Value = "appearance"},
                                                                new TranslateSet() { Name ="behavior", Value = "behavior"},
                                                             };

        /// <summary>
        /// TODO FOR MOVE TO MICRODIAL
        /// </summary>


        public static ObservableCollection<TranslateSet> MenuTypes = new ObservableCollection<TranslateSet>() {
            new TranslateSet() { Name = "Dial", Value = "Dial" },
            new TranslateSet() { Name = "View", Value = "View" },
            new TranslateSet() { Name = "Agenda", Value = "Agenda" }
        };

        public static ObservableCollection<TranslateSet> ProcessTypes = new ObservableCollection<TranslateSet>() {
            new TranslateSet() { Name = "ServerUrl", Value = "ServerUrl" },
            new TranslateSet() { Name = "WINcmd", Value = "WINcmd" },
            new TranslateSet() { Name = "WebUrl", Value = "WebUrl" },
            new TranslateSet() { Name = "GithubUrl", Value = "GithubUrl" },
            new TranslateSet() { Name = "SystemUrl", Value = "SystemUrl" },
            new TranslateSet() { Name = "SearchUrl", Value = "SearchUrl },
            new TranslateSet() { Name = "NugetUrl", Value = "NugetUrl" },
            new TranslateSet() { Name = "DocsUrl", Value = "DocsUrl" },
            new TranslateSet() { Name = "PowerScript", Value = "PowerScript" },
            new TranslateSet() { Name = "NodeJs", Value = "NodeJs" },
            new TranslateSet() { Name = "Python", Value = "Python" },
            new TranslateSet() { Name = "PHP", Value = "PHP" }
        };

        public static ObservableCollection<TranslateSet> HealthCheckTypes = new ObservableCollection<TranslateSet>() {
            new TranslateSet() { Name = "driveSize", Value = "driveSize" },
            new TranslateSet() { Name = "processMemory", Value = "processMemory" },
            new TranslateSet() { Name = "allocatedMemory", Value = "allocatedMemory" },
            new TranslateSet() { Name = "folderExist", Value = "folderExist" },

            new TranslateSet() { Name = "ping", Value = "ping" },
            new TranslateSet() { Name = "tcpPort", Value = "tcpPort" },
            new TranslateSet() { Name = "serverUrlPath", Value = "serverUrlPath" },
            new TranslateSet() { Name = "urlPath", Value = "urlPath" },

            new TranslateSet() { Name = "mssqlConnection", Value = "mssqlConnection" },
            new TranslateSet() { Name = "mysqlConnection", Value = "mysqlConnection" },
            new TranslateSet() { Name = "oracleConnection", Value = "oracleConnection" },
            new TranslateSet() { Name = "postgresConnection", Value = "postgresConnection" },
        };

        public static ObservableCollection<TranslateSet> PagePartType = new ObservableCollection<TranslateSet>() {
            new TranslateSet() { Name = "HeaderPreCss", Value = "HeaderPreCss" },
            new TranslateSet() { Name = "HeaderPreScripts", Value = "HeaderPreScripts" },
            new TranslateSet() { Name = "HeaderPostScripts", Value = "HeaderPostScripts" },
            new TranslateSet() { Name = "Body", Value = "Body" },
            new TranslateSet() { Name = "Footer", Value = "Footer" }
        };
    }
}