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

    }
}