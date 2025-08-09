using EASYTools.CrashReporter;

namespace EasyITSystemCenter.SystemConfiguration {

    /// <summary>
    /// Libreria condivisa
    /// </summary>
    public class CrashReporterSettings {

        /// <summary>
        /// FirstRun
        /// </summary>
        public static ReportCrash _ReportCrash = new ReportCrash() {
            /*
            FromEmail = App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPLoginUsername.ToString()).Value,

            ToEmail = App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerServiceEmailAddress.ToString()).Value,
            SMTPHost = App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPServerAddress.ToString()).Value,
            Port = int.Parse(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPPort.ToString()).Value),
            UserName = App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPLoginUsername.ToString()).Value,
            Password = App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPLoginPassword.ToString()).Value,
            EnableSSL = bool.Parse(App.ServerSetting.Find(a => a.Key == ServerSettingKeys.EmailerSMTPSslIsEnabled.ToString()).Value),
            */
        };
    }
}