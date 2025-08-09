using EasyITSystemCenter.GlobalClasses;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static IronPython.Modules._ast;

namespace EasyITSystemCenter.GlobalOperations {

    /// <summary>
    /// Centralised System Functions for work with Types, methods, Formats, Logic, supported methods
    /// </summary>
    internal class SystemOperations {

        /// <summary>
        /// Settings Local Application Translation dictionaries (Resources Files) for Pages Will be
        /// replaced by DBDictionary, but for Offline Running must be possible
        /// </summary>
        /// <param name="Resources">      </param>
        /// <param name="languageDefault"></param>
        /// <returns></returns>
        public static ResourceDictionary SetLanguageDictionary(ResourceDictionary Resources, string languageDefault) {
            if (languageDefault == "system") languageDefault = Thread.CurrentThread.CurrentCulture.ToString();
            App.appRuntimeData.appStartupLanguage = languageDefault;

            ResourceDictionary dict = new ResourceDictionary();
            switch (languageDefault) {
                case "en-US":
                    dict.Source = new Uri("..\\Languages\\StringResources.xaml", UriKind.Relative);
                    break;

                case "cs-CZ":
                    dict.Source = new Uri("..\\Languages\\StringResources.cs-CZ.xaml", UriKind.Relative);
                    break;
            }

            Resources.MergedDictionaries.Add(dict);
            return Resources;
        }


        /// <summary>
        /// Get Language Code 
        /// Used By Tools Automatic Setting for tools
        /// </summary>
        /// <param name="languageDefault"></param>
        /// <returns></returns>
        public static string GetLanguageCode(string languageDefault) {
            if (languageDefault == "system") languageDefault = Thread.CurrentThread.CurrentCulture.ToString();
            App.appRuntimeData.appStartupLanguage = languageDefault;
            switch (languageDefault)
            {
                case "en-US":
                    languageDefault = "en";
                    break;

                case "cs-CZ":
                    languageDefault = "cs";
                    break;
            }
            return languageDefault;
        }

        /// <summary>
        /// Email Sender for send Direct Email by Server Configuration for Testing
        /// </summary>
        /// <param name="message">The message.</param>
        public static void SendMailWithServerSetting(string message) {
            /*
             try {
                 MailMessage Email = new MailMessage() { From = new MailAddress(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPLoginUsername.ToString()).Value) };
                 Email.To.Add(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerServiceEmailAddress.ToString()).Value);
                 Email.Subject = "EasyITCenter Emailer";
                 Email.Body = message;
                 SmtpClient MailClient = new SmtpClient(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPServerAddress.ToString()).Value, int.Parse(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPPort.ToString()).Value)) {
                     Credentials = new NetworkCredential(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPLoginUsername.ToString()).Value, App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPLoginPassword.ToString()).Value),
                     EnableSsl = bool.Parse(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPSslIsEnabled.ToString()).Value),
                     Host = App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPServerAddress.ToString()).Value,
                     Port = int.Parse(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPPort.ToString()).Value)
                 };
                 MailClient.Send(Email);
             } catch (Exception) { }
            */
        }

        /// <summary>
        /// System Mail sending
        /// </summary>
        /// <param name="address"></param>
        /// <param name="subject"></param>
        /// <param name="body">   </param>
        /// <param name="attach"> </param>
        public static void SendMailWithMailTo(string address, string subject, string body, string attach) {
            string mailto =
                $"mailto:{address}?Subject={subject}&Body={body}&Attach={attach}";
            Process.Start(mailto);
        }

        /// <summary>
        /// Automatic Increase version System Ideal for small systems with more release in 1 day
        /// Increase Windows Correct 3 position for Widows Installation In Debug is increase last 4 position
        /// </summary>
        /// <returns></returns>
        public static bool IncreaseFileVersionBuild() {
            if (Debugger.IsAttached) {
                try {
                    var fi = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.GetDirectories("Properties")[0].GetFiles("AssemblyInfo.cs")[0];
                    var ve = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                    string ol = ve.FileMajorPart.ToString() + "." + ve.FileMinorPart.ToString() + "." + ve.FileBuildPart.ToString() + "." + ve.FilePrivatePart.ToString();

                    AppVersion appVersion = new AppVersion() { Major = ve.FileMajorPart, Minor = ve.FileMinorPart, Build = ve.FileBuildPart, Private = ve.FilePrivatePart + 1 };
                    string ne = appVersion.Major.ToString() + "." + appVersion.Minor.ToString() + "." + appVersion.Build.ToString() + "." + appVersion.Private.ToString();

                    File.WriteAllText(fi.FullName, File.ReadAllText(fi.FullName).Replace("[assembly: AssemblyVersion(\"" + ol + "\")]", "[assembly: AssemblyVersion(\"" + ne + "\")]"));
                    File.WriteAllText(fi.FullName, File.ReadAllText(fi.FullName).Replace("[assembly: AssemblyFileVersion(\"" + ol + "\")]", "[assembly: AssemblyFileVersion(\"" + ne + "\")]"));
                    return true;
                } catch { return false; }
            }
            else {
                try {
                    var fi = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.GetDirectories("Properties")[0].GetFiles("AssemblyInfo.cs")[0];
                    var ve = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                    string ol = ve.FileMajorPart.ToString() + "." + ve.FileMinorPart.ToString() + "." + ve.FileBuildPart.ToString() + "." + ve.FilePrivatePart.ToString();

                    bool moveNext = false;
                    AppVersion appVersion = new AppVersion() { Major = ve.FileMajorPart, Minor = ve.FileMinorPart, Build = ve.FileBuildPart, Private = ve.FilePrivatePart };
                    if (appVersion.Build + 1 > 99) { appVersion.Build = 0; moveNext = true; } else { appVersion.Build++; }
                    if (appVersion.Minor + 1 > 99) { appVersion.Minor = 0; moveNext = true; } else if (moveNext) { appVersion.Minor++; moveNext = false; }
                    if (moveNext) { appVersion.Major++; }

                    string ne = appVersion.Major.ToString() + "." + appVersion.Minor.ToString() + "." + appVersion.Build.ToString() + "." + appVersion.Private.ToString();
                    File.WriteAllText(fi.FullName, File.ReadAllText(fi.FullName).Replace("[assembly: AssemblyVersion(\"" + ol + "\")]", "[assembly: AssemblyVersion(\"" + ne + "\")]"));
                    File.WriteAllText(fi.FullName, File.ReadAllText(fi.FullName).Replace("[assembly: AssemblyFileVersion(\"" + ol + "\")]", "[assembly: AssemblyFileVersion(\"" + ne + "\")]"));
                    return true;
                } catch { return false; }
            }
        }

        /// <summary>
        /// Mining All Exception Information For Central System Logger Ignore Some selected Fails is
        /// possible by Ignored Exception Settings
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="msgCount"> </param>
        /// <returns></returns>
        public static async Task<string> GetExceptionMessages(Exception exception, int msgCount = 1) {
            if (exception != null && App.IgnoredExceptionList.Any() && App.IgnoredExceptionList.FirstOrDefault(a => a.ErrorNumber == exception.HResult.ToString() && a.Active == true) != null && !bool.Parse(await DataOperations.ParameterCheck("DeactivateIgnoredException"))) return null;
            return exception != null ? string.Format("{0}: {1}\n{2}", msgCount, (exception.TargetSite?.ReflectedType?.FullName + Environment.NewLine + exception.Message + Environment.NewLine + exception.HResult.ToString() + Environment.NewLine + exception.StackTrace + Environment.NewLine), GetExceptionMessages(exception.InnerException, ++msgCount)) : string.Empty;
        }

        /// <summary>
        /// Mining All Exception Information For Local System Logger EveryTime Show All fails for
        /// Best Developing On Expert Level
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="msgCount"> </param>
        /// <returns></returns>
        public static string GetExceptionMessagesAll(Exception exception, int msgCount = 1) {
            return exception != null ? string.Format("{0}: {1}\n{2}", msgCount, (exception.TargetSite?.ReflectedType?.FullName + Environment.NewLine + exception.Message + Environment.NewLine + exception.HResult.ToString() + Environment.NewLine + exception.StackTrace + Environment.NewLine), GetExceptionMessagesAll(exception.InnerException, ++msgCount)) : string.Empty;
        }

        /// <summary>
        /// SYSTEM Advanced Filter Conversion for API return existing filter for saving to string in
        /// selected Page
        /// </summary>
        /// <param name="filterBox"></param>
        /// <returns></returns>
        public static string FilterToString(ComboBox filterBox) {
            string advancedFilter = null;
            int index = 0;
            try {
                foreach (WrapPanel filterItem in filterBox.Items) {
                    if (index > 0) {
                        if (filterItem.Name.Split('_')[0] == "condition") {
                            index = 0;
                            foreach (var item in filterItem.Children) {
                                if (index == 1) { advancedFilter += "[!]" + ((Label)item).Content; }
                                if (index > 1) { advancedFilter += "{!}" + ((Label)item).Content; }
                                index++;
                            }
                        }
                        else if (filterItem.Name.Split('_')[0] == "filter") {
                            advancedFilter += "[!]" + ((ComboBox)filterItem.Children[0]).SelectionBoxItem;
                            advancedFilter += "{!}" + ((ComboBox)filterItem.Children[2]).SelectionBoxItem;
                            advancedFilter += "{!}'" + ((TextBox)filterItem.Children[3]).Text + "'";
                        }
                        index = 1;
                    }
                    index++;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            return advancedFilter;
        }

        /// <summary>
        /// Its Solution for this is a solution for demanding and multiplied servers Or Running
        /// SHARP and Test System By One Backend Server Service API Urls with Namespaces in Name are
        /// for Backend model with More Same Database Schemas Backend Databases count in One Server
        /// Service is Unlimited
        /// </summary>
        /// <param name="stringForRemoveNamespace"></param>
        public static string RemoveAppNamespaceFromString(string stringForRemoveNamespace) {
            string appNameSpace = MethodBase.GetCurrentMethod().DeclaringType.Namespace.Split('.')[0];
            return stringForRemoveNamespace.Replace(appNameSpace, string.Empty);
        }

        /// <summary>
        /// Generate Random String with defined length
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string RandomString(int length) {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// System External Process Starter for Conrtalized Using Return the processId when is
        /// started or null
        ///
        /// TODO
        /// - create process Monitor
        /// - save the monitored procceses to System Monitor
        /// - must be refactored actual status
        /// </summary>
        /// <param name="type">          </param>
        /// <param name="processCommand"></param>
        /// <param name="startupPath">   The startup path.</param>
        /// <param name="arguments">     The arguments.</param>
        /// <returns></returns>
        public static int? StartExternalProccess(string type, string processCommand, string startupPath = null, string arguments = null) {
            try {
                Process tmpProcess = new Process();
                switch (type.ToLower()) {
                    case "wincmd":
                    case "powerscript":
                    case "nodejs":
                    case "python":
                    case "php":
                        ProcessStartInfo info = new ProcessStartInfo() {
                            FileName = processCommand,
                            WorkingDirectory = startupPath,
                            Arguments = arguments,
                            LoadUserProfile = true,
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            Verb = (Environment.OSVersion.Version.Major >= 6) ? "runas" : "",
                        };
                        tmpProcess.StartInfo = info;
                        tmpProcess.Start();
                        break;

                    case "weburl":
                    case "githuburl":
                    case "serverUrl":
                    case "systemUrl":
                    case "searchurl":
                    case "nugeturl":
                    case "docsurl":
                        tmpProcess = Process.Start(processCommand);
                        break;

                    case "EDCservice":
                        tmpProcess = Process.Start(processCommand);
                        break;

                    default: break;
                }

                return tmpProcess?.Id; ;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            return null;
        }

        /// <summary>
        /// Change First Character of String
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string FirstLetterToLower(string str) {
            if (str == null) return null;
            if (str.Length > 1)
                return char.ToLower(str[0]) + str.Substring(1);
            return str.ToLower();
        }

        
        /// <summary>
        /// System Port Checker
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool PortInUse(int port) {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints) {
                if (endPoint.Port == port) {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }


        /// <summary>
        /// Get First Free Port For Start Local Web Server
        /// Used By reactive Web Server
        /// </summary>
        /// <returns></returns>
        public static int? GetSystemLocalhostFreePort() {
            int? freePort = null;
            int startPort = 10000;

            while (freePort == null) {
                if (!PortInUse(startPort)) {
                    freePort = startPort;
                }
                else {
                    freePort = null;
                }
            }
            return freePort;
        }
    }
}