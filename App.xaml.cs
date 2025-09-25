using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.Pages;
using EasyITSystemCenter.SystemHelper;
using EasyITSystemCenter.SystemStructure;
using log4net.Config;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace EasyITSystemCenter {

    public partial class App : Application {

        /// <summary>
        /// Tilt Document types definitions
        /// </summary>
        public static ExtendedReceiptList TiltReceiptDoc = new ExtendedReceiptList(); public static ExtendedCreditNoteList TiltCreditDoc = new ExtendedCreditNoteList(); public static ExtendedOutgoingInvoiceList TiltInvoiceDoc = new ExtendedOutgoingInvoiceList(); public static BusinessIncomingOrderList TiltOrderDoc = new BusinessIncomingOrderList();
        public static BusinessOfferList TiltOfferDoc = new BusinessOfferList(); public static List<DocumentItemList> TiltDocItems = new List<DocumentItemList>(); public static string tiltTargets = TiltTargets.None.ToString();


        /// <summary>
        /// Global Application Startup Settings Central Parameters / Languages / User / Configure
        /// TODO must centralize to Global APP class
        /// </summary>
        public static AppRuntimeData appRuntimeData = new AppRuntimeData();


        /// <summary>
        /// System Core Needs Runtime Data For Working
        /// //TODO move to RuntimeData
        /// </summary>
        public static log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static List<SystemMenuList> SystemMenuList = null;
        public static List<SystemCustomPageList> SystemCustomList = null;
        public static List<SystemSvgIconList> SystemSvgIconList = null;
        public static List<SystemModuleList> SystemModuleList = null;
        public static List<SolutionMixedEnumList> SolutionMixedEnumList = null;
        public static List<SystemParameterList> UserParameterList = null;
        public static List<SystemTranslationList> LanguageList = null;
        public static List<SystemIgnoredExceptionList> IgnoredExceptionList = new List<SystemIgnoredExceptionList>();


        public static SystemLoggerWebSocketClass SystemLoggerWebSocketMonitor = new SystemLoggerWebSocketClass();
        public static UserData UserData = new UserData();

        public static bool ReloadLanguageListRequested = false;

        /// <summary>
        /// Application Global Exceptions Controls Definitions
        /// </summary>
        public App() {

            FileOperations.LoadSettings();
            SystemOperations.SetLanguageDictionary(Resources, appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);
            //RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.Default;
            log4net.GlobalContext.Properties["RollingFileAppender"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Assembly.GetEntryAssembly().GetName().FullName.Split(',')[0], "Log4NetApplication.log");
            _ = XmlConfigurator.Configure();
            Dispatcher.UnhandledException += App_DispatcherUnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            System.Windows.Forms.Application.ThreadException += WinFormApplication_ThreadException;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // enabled ssl connections
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            DBOperations.LoadStartupDBData();
        }

        /// <summary>
        /// Connected Starting Video
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e) {

            base.OnStartup(e);
            if (!bool.Parse(appRuntimeData.AppClientSettings.First(a => a.Key == "beh_hideStartVideo").Value))
            {
                MetroWindow startupAnimation = new WelcomePage();
                MainWindow = startupAnimation;
                startupAnimation.Closing += StartupAnimation_Closing;
            }
            else { StartupAnimation_Closing(new object(), new CancelEventArgs()); }
        }

        private void StartupAnimation_Closing(object sender, CancelEventArgs e) {
            MetroWindow mainView = new MainWindow();
            MainWindow = mainView;
            mainView.Show(); mainView.Focus();

            PrepareStartupTools();
        }

        /// <summary>
        /// System Restart Controller
        /// </summary>
        public static void AppRestart() {
            SystemWindowDataModel.SaveTheme();
            Process.Start(Assembly.GetEntryAssembly().EscapedCodeBase);
            ApplicationQuit();
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// System or Quit
        /// </summary>
        /// <param name="silent"></param>
        public static async void AppQuitRequest(bool silent) {
            if (!silent)
            {
                MetroWindow metroWindow = Current.MainWindow as MetroWindow;
                MetroDialogSettings settings = new MetroDialogSettings() { AffirmativeButtonText = metroWindow.Resources["yes"].ToString(), NegativeButtonText = metroWindow.Resources["no"].ToString() };
                MessageDialogResult result = metroWindow.ShowModalMessageExternal(metroWindow.Resources["closeAppTitle"].ToString(), metroWindow.Resources["closeAppQuestion"].ToString(), MessageDialogStyle.AffirmativeAndNegative, settings);
                if (result == MessageDialogResult.Affirmative) ApplicationQuit();
            }
            else { ApplicationQuit(); }
        }


        private void PrepareStartupTools() {
            try { StartupLocaslWebServer();
            } catch (Exception autoEx) { ApplicationLogging(autoEx); }
        }


        /// <summary>
        /// Local Web Server Controller
        /// </summary>
        private static void StartupLocaslWebServer() {
            try
            {
                if (bool.Parse(appRuntimeData.AppClientSettings.First(a => a.Key == "sys_localWebServerEnabled").Value))
                {
                    moduleApp = WebApp.Start<Startup>(url: appRuntimeData.AppClientSettings.First(a => a.Key == "sys_localWebServerUrl").Value);
                    appRuntimeData.webServerRunning = true;
                }
            } catch { }
        }
        public static IDisposable moduleApp { get; set; }


        /// <summary>
        /// MainWindow Closing Handler for Cleaning TempData, disable AddOns / Tool and Third Party
        /// Software Closing Third Party processes
        /// </summary>
        private static void ApplicationQuit() {
            try
            {
                MainWindow MainWindow = (MainWindow)Current.MainWindow;
                MainWindow.AppSystemTimer.Enabled = false;
                if (MainWindow.vncProcess != null && !MainWindow.vncProcess.HasExited) { MainWindow.vncProcess.Kill(); };
            } catch { }

            try
            {
                if (moduleApp != null) { moduleApp.Dispose(); }
            } catch { }

            try { FileOperations.ClearFolder(appRuntimeData.reportFolder); } catch { }
            try { FileOperations.ClearFolder(appRuntimeData.galleryFolder); } catch { }
            try { FileOperations.ClearFolder(appRuntimeData.tempFolder); } catch { }

            SystemWindowDataModel.SaveTheme();
            Current.Shutdown();
        }

        /// <summary>
        /// Full Application System logging Running If is AppSystemTimer is Enabled for disable
        /// other processes exceptions Full Application logging to file if enabled and to DB for
        /// solving by Developers Supported Custom Message Here Is Filling Local System Logger for
        /// Developers Logging to Database Are All non Developer working
        /// </summary>
        /// <param name="ex">           </param>
        /// <param name="customMessage"></param>
        public static void ApplicationLogging(Exception ex, string customMessage = null) {
            try
            {
                Current?.Dispatcher.Invoke(async () =>
                {
                    if (Current.MainWindow != null && Current.MainWindow.Name == "XamlMainWindow" && UserData.Authentification != null)
                    {
                        if (string.IsNullOrWhiteSpace(customMessage))
                        {
                            if (!bool.Parse(appRuntimeData.AppClientSettings.First(a => a.Key == "sys_imDeveloper").Value)) DBOperations.SaveSystemFailMessage(await SystemOperations.GetExceptionMessages(ex), nameof(log.Error));

                            if (
                                (SystemLoggerWebSocketMonitor.ShowSystemLogger && !((MainWindow)Current.MainWindow).ServerLoggerSource && ((MainWindow)Current.MainWindow).RunReleaseMode)
                                || (bool.Parse(appRuntimeData.AppClientSettings.First(a => a.Key == "sys_imDeveloper").Value) && !((MainWindow)Current.MainWindow).ServerLoggerSource && SystemLoggerWebSocketMonitor.ShowSystemLogger)
                            ) { ((MainWindow)Current.MainWindow).SystemLogger = SystemOperations.GetExceptionMessagesAll(ex).ToString(); }
                        }
                        else
                        {
                            if (!bool.Parse(appRuntimeData.AppClientSettings.First(a => a.Key == "sys_imDeveloper").Value)) DBOperations.SaveSystemFailMessage(customMessage, nameof(log.Error));

                            if (
                                (SystemLoggerWebSocketMonitor.ShowSystemLogger && !((MainWindow)Current.MainWindow).ServerLoggerSource && ((MainWindow)Current.MainWindow).RunReleaseMode)
                                || (bool.Parse(appRuntimeData.AppClientSettings.First(a => a.Key == "sys_imDeveloper").Value) && !((MainWindow)Current.MainWindow).ServerLoggerSource && SystemLoggerWebSocketMonitor.ShowSystemLogger)
                            ) { ((MainWindow)Current.MainWindow).SystemLogger = customMessage; }
                        }
                        
                    }
                });
            } catch { }
        }

        /// <summary>
        /// FullSystem Logging Every Exception types are monitored for maximize correct running all
        /// processes, System addOns, systems, communications, threads, network All detail of
        /// application system add all used possibilities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e) {
            if (!e.Exception.StackTrace.Contains("TabPanelOnSelectionChange") && !e.Exception.StackTrace.Contains("ControlzEx")) ApplicationLogging(e.Exception);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
            e.Handled = true;
            //MessageBox.Show(e.Exception.Message + "\n" + e.Exception.StackTrace?.ToString() + "\n" + "Application must be close !!!", "CurrentDomain UnhandledException", MessageBoxButton.OK, MessageBoxImage.Error);
            //if (UserData.Authentification != null) { SystemConfiguration.CrashReporterSettings._ReportCrash.Send(e.Exception); }
            ApplicationLogging(e.Exception);
            if (SystemConfiguration.CrashReporterSettings._ReportCrash != null && SystemConfiguration.CrashReporterSettings._ReportCrash.IsQuit) { Current.Shutdown(-1); }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            Exception exception = e.ExceptionObject as Exception;
            //MessageBox.Show(exception.Message + "\n" + exception.StackTrace?.ToString() + "\n" + "Application must be close !!!", "CurrentDomain UnhandledException", MessageBoxButton.OK, MessageBoxImage.Error);
            ApplicationLogging(exception);
            if (e.IsTerminating) Environment.Exit(1);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
            //MessageBox.Show(e.Exception.Message, "TaskScheduler UnobservedTaskException", MessageBoxButton.OK, MessageBoxImage.Error);
            ApplicationLogging(e.Exception); e.SetObserved();
        }

        private void WinFormApplication_ThreadException(object sender, ThreadExceptionEventArgs e) {
            //if (UserData.Authentification != null) { SystemConfiguration.CrashReporterSettings._ReportCrash.Send(e.Exception); }
            ApplicationLogging(e.Exception);
        }

        /// <summary>
        /// Keyboard Pointer to Central Keyboard Reaction Definitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void RootAppKeyDownController(object sender, KeyEventArgs e) {
            if (!bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_disableSystemHotKeys").Value))
            { HardwareOperations.ApplicationKeyboardMaping(e); }
        }
    }
}