using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyITSystemCenter.GlobalOperations {

    /// <summary>
    /// Centralised DBFunctions as Load DB Congig, System Dials (Language, Params) Another Db
    /// functions As Saving System Loging, Language Dictionary Autofiling
    /// </summary>
    internal class DBOperations {

        /// <summary>
        /// Startup Load System Parameters ,Languages, System Controlling, Server Setting
        /// </summary>
        public static async void LoadStartupDBData() {
            try {
                App.SolutionMixedEnumList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, null, null);
                App.IgnoredExceptionList = await CommunicationManager.GetApiRequest<List<SystemIgnoredExceptionList>>(ApiUrls.EasyITCenterSystemIgnoredExceptionList, null, null);
                App.LanguageList = await CommunicationManager.GetApiRequest<List<SystemTranslationList>>(ApiUrls.EasyITCenterSystemTranslationList, null, null);
                App.UserParameterList = await CommunicationManager.GetApiRequest<List<SystemParameterList>>(ApiUrls.EasyITCenterUserParameterList, null, null);
                App.ReloadLanguageListRequested = false;
            } catch { }
        }

        /// <summary>
        /// Reload SYSTEM Dials When System Sleep For Non Blocking Work Was Problem For More Startup
        /// Saving and Reloading with Language List
        /// </summary>
        public static async Task<bool> LoadWaitingDataInSleepMode() {
            if (App.ReloadLanguageListRequested) { App.LanguageList = await CommunicationManager.GetApiRequest<List<SystemTranslationList>>(ApiUrls.EasyITCenterSystemTranslationList, null, App.UserData.Authentification.Token); }

            App.ReloadLanguageListRequested = false;
            return true;
        }

        /// <summary>
        /// Centralised Method for Refresh All UserData params, for correct App running. Thinking
        /// for remove and new Load Actualy limited by DebugingHelpSetting Itr user After Succes
        /// User Login
        /// </summary>
        public static async Task<bool> LoadOrRefreshUserData() {
            App.SolutionMixedEnumList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, null, null);
            App.IgnoredExceptionList = await CommunicationManager.GetApiRequest<List<SystemIgnoredExceptionList>>(ApiUrls.EasyITCenterSystemIgnoredExceptionList, null, App.UserData.Authentification.Token);
            App.LanguageList = await CommunicationManager.GetApiRequest<List<SystemTranslationList>>(ApiUrls.EasyITCenterSystemTranslationList, null, App.UserData.Authentification.Token);
            App.UserParameterList = await CommunicationManager.GetApiRequest<List<SystemParameterList>>(ApiUrls.EasyITCenterUserParameterList, App.UserData.Authentification.Id.ToString(), App.UserData.Authentification.Token);
            App.SystemModuleList = await CommunicationManager.GetApiRequest<List<SystemModuleList>>(ApiUrls.SystemModuleList, null, App.UserData.Authentification.Token);
            SetNonUserDataOnSuccessStartUp();
            return true;
        }

        /// <summary>
        /// SYSTEM: Set NonUser Startup Data for Correct Prepare System Its for All Status Possible
        /// - NODB,OS,IS,Network,etc. For check ANY possible problems out of System
        /// </summary>
        private static async void SetNonUserDataOnSuccessStartUp() {
            try {
                if (App.UserParameterList != null) {
                    //System Logger Socket MainData
                    App.SystemLoggerWebSocketMonitor.Url = App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value.ToLower().Replace("https:", "wss:").Replace("http:", "ws:") + await DataOperations.ParameterCheck("SystemLoggerServerWebSocketUrl");
                    App.SystemLoggerWebSocketMonitor.ShowSystemLogger = bool.Parse(await DataOperations.ParameterCheck("SystemLoggerAvailable"));
                    if (App.SystemLoggerWebSocketMonitor.ShowSystemLogger) { MainWindow.ShowSystemLogger = System.Windows.Visibility.Visible; }

                    App.appRuntimeData.AppClientSettings.Remove("sys_imDeveloper");
                    App.appRuntimeData.AppClientSettings.Add("sys_imDeveloper", bool.Parse(await DataOperations.ParameterCheck("ImDeveloper")).ToString());
                }
            } catch { }
        }

        /// <summary>
        /// Save Exception to DB Fail List (System Log) Write to System Logger
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="logLevel"></param>
        public static async void SaveSystemFailMessage(string message, string logLevel) {
            if (MainWindow.ServiceRunning && MainWindow.UserLogged && App.UserParameterList.Any()) {
                if (string.IsNullOrWhiteSpace(message)) return;

                SolutionFailList SolutionFailList = new SolutionFailList() { InheritedLogMonitorType = "DesktopSystem", UserId = App.UserData.Authentification.Id, UserName = App.UserData.UserName, LogLevel = logLevel, Message = message, TimeStamp = DateTimeOffset.Now.DateTime };
                string json = JsonConvert.SerializeObject(SolutionFailList);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSolutionFailList, httpContent, null, App.UserData.Authentification.Token);
            }
        }

        /// <summary>
        /// Centralised Method for Translating by DB Dictionary Service insert the news words for
        /// translate (After translate request) to Database Automaticaly with Empty Translate.
        /// Service return translate if is possible or requested word send back CamelCase ignored
        /// Default Language: CZ
        /// </summary>
        /// <param name="systemName">  </param>
        /// <param name="notCreateNew"></param>
        /// <param name="comaList">    </param>
        /// <param name="lang">        </param>
        /// <returns></returns>
        public static async Task<string> DBTranslation(string systemName, bool notCreateNew = false, bool comaList = false, string lang = null) {
            bool dictionaryUpdated = false;
            string result = "", translated = "";
            if (comaList) {
                systemName.Split(',').ToList().ForEach(async word => {
                    try {
                        if (!string.IsNullOrWhiteSpace(word)) {
                            if (!App.ReloadLanguageListRequested && App.LanguageList?.Where(a => a.SystemName?.ToLower() == word.ToLower()).Count() == 0) {
                                if (!notCreateNew && App.UserData.Authentification != null) {
                                    dictionaryUpdated = true; App.ReloadLanguageListRequested = true;
                                    SystemTranslationList newWord = new SystemTranslationList() { SystemName = word, DescriptionCz = "", DescriptionEn = "", UserId = 1 };
                                    string json = JsonConvert.SerializeObject(newWord);
                                    StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                                    await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSystemTranslationList, httpContent, null, App.UserData.Authentification.Token);
                                }
                                result += word + ",";
                            }
                            else {
                                translated = ((App.appRuntimeData.appStartupLanguage == "cs-CZ" && lang == null) || lang == "cz") ? App.LanguageList.Where(a => a.SystemName.ToLower() == word.ToLower()).Select(a => a.DescriptionCz).FirstOrDefault() : App.LanguageList.Where(a => a.SystemName.ToLower() == word.ToLower()).Select(a => a.DescriptionEn).FirstOrDefault();
                                result += (string.IsNullOrWhiteSpace(translated) ? word : translated) + ",";
                            }
                        }
                    } catch { result += word + ","; }
                });
            }
            else {
                try {
                    if (!string.IsNullOrWhiteSpace(systemName) && App.LanguageList?.Where(a => a.SystemName?.ToLower() == systemName.ToLower()).Count() == 0) {
                        if (!App.ReloadLanguageListRequested && !notCreateNew && App.UserData.Authentification != null) {
                            dictionaryUpdated = true; App.ReloadLanguageListRequested = true;

                            SystemTranslationList newWord = new SystemTranslationList() { SystemName = systemName, DescriptionCz = "", DescriptionEn = "", UserId = 1 };
                            string json = JsonConvert.SerializeObject(newWord);
                            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                            await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSystemTranslationList, httpContent, null, App.UserData.Authentification.Token);
                        }
                        result = systemName;
                    }
                    else if (!string.IsNullOrWhiteSpace(systemName)) {
                        translated = ((App.appRuntimeData.appStartupLanguage == "cs-CZ" && lang == null) || lang == "cz") ? App.LanguageList.Where(a => a.SystemName.ToLower() == systemName.ToLower()).Select(a => a.DescriptionCz).FirstOrDefault() : App.LanguageList.Where(a => a.SystemName.ToLower() == systemName.ToLower()).Select(a => a.DescriptionEn).FirstOrDefault();
                        result = string.IsNullOrWhiteSpace(translated) ? systemName : translated;
                    }
                } catch { result = systemName; }
            }

            if (dictionaryUpdated && !notCreateNew) {
                App.LanguageList = await CommunicationManager.GetApiRequest<List<SystemTranslationList>>(ApiUrls.EasyITCenterSystemTranslationList, null, App.UserData.Authentification.Token);
                App.ReloadLanguageListRequested = false;
            }

            return result.Length == 0 ? systemName : result;
        }


        /// <summary>
        /// Global Inherited Load Function
        /// Loads the inherited data list By ColumnName
        /// Its must be InheritedGroupName 
        /// 'Inherited' is removed and GroupName Is Loaded
        /// </summary>
        /// <param name="groupName">The group name.</param>
        /// <returns>A Task.</returns>
        public async static Task<List<SolutionMixedEnumList>> LoadInheritedDataList(string groupName) {
            List<SolutionMixedEnumList> solutionMixedEnumList = new List<SolutionMixedEnumList>();
            try {
                if (groupName.Length > 0) {
                    solutionMixedEnumList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/" + DataOperations.IgnoreCaseReplaceString(groupName,"Inherited"), App.UserData.Authentification.Token);
                    solutionMixedEnumList.ForEach(async item => { item.Translation = await DBTranslation(item.Name); });
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            return solutionMixedEnumList;
        }
    }
}