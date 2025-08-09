using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;




namespace EasyITSystemCenter.Pages {

    /// <summary>
    /// //Local Web Server Web Template App Viewer
    /// </summary>
    public partial class LocSrvWebTmpViewerPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static BasicCalendarList selectedRecord = new BasicCalendarList();

        //Systém volá Otevření Webové Šablony
        private event EventHandler ShowWebModuleChanged = delegate { };
        private SystemModuleList showWebModule = null;

        public SystemModuleList ShowWebModule {
            get => showWebModule;
            set {
                showWebModule = value;
                ShowWebModuleInBrowser();
                ShowWebModuleChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        

        public LocSrvWebTmpViewerPage() {

            InitializeComponent();
            
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);
            _ = FormOperations.TranslateFormFields(ListView);
            MainWindow.ProgressRing = Visibility.Visible;

            try {
                if (!App.appRuntimeData.webServerRunning) {
                    webBrowser.Dispose();
                    _ = MainWindow.ShowMessageOnMainWindow(true, DBOperations.DBTranslation("WebServerNotRunningCheckClientConfiguration").GetAwaiter().GetResult());
                }
            } catch (Exception ex) { App.ApplicationLogging(ex); }
            MainWindow.ProgressRing = Visibility.Hidden;
        }

        private async void ShowWebModuleInBrowser() {
            if (App.appRuntimeData.webServerRunning) {
                MainWindow.ProgressRing = Visibility.Visible;

                webBrowser.Source = new Uri(App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_localWebServerUrl").Value + showWebModule.FolderPath);
                webBrowser.CoreWebView2InitializationCompleted += WebBrowser_CoreWebView2InitializationCompleted;
                
            } else { await MainWindow.ShowMessageOnMainWindow(true, await DBOperations.DBTranslation("WebServerNotRunningCheckClientConfiguration")); }
        }

        private async void WebBrowser_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e) {
            await webBrowser.EnsureCoreWebView2Async();
            webBrowser.CoreWebView2.Settings.AreDevToolsEnabled = webBrowser.CoreWebView2.Settings.AreDefaultContextMenusEnabled = webBrowser.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled =
                webBrowser.CoreWebView2.Settings.AreHostObjectsAllowed = webBrowser.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = webBrowser.CoreWebView2.Settings.IsNonClientRegionSupportEnabled =
                webBrowser.CoreWebView2.Settings.IsWebMessageEnabled = webBrowser.CoreWebView2.Settings.IsPinchZoomEnabled = webBrowser.CoreWebView2.Settings.IsGeneralAutofillEnabled =
                webBrowser.CoreWebView2.Settings.IsZoomControlEnabled = webBrowser.CoreWebView2.Settings.IsSwipeNavigationEnabled = webBrowser.CoreWebView2.Settings.IsStatusBarEnabled =
                webBrowser.CoreWebView2.Settings.IsScriptEnabled = true;
                

            webBrowser.CoreWebView2.ContextMenuRequested += async delegate (object sender1, CoreWebView2ContextMenuRequestedEventArgs args) {
                IList<CoreWebView2ContextMenuItem> menuList = args.MenuItems;
                CoreWebView2ContextMenuItem openConsole = webBrowser.CoreWebView2.Environment.CreateContextMenuItem(await DBOperations.DBTranslation("openConsole"), null, CoreWebView2ContextMenuItemKind.Command); openConsole.CustomItemSelected += openConsoleSelected;
                CoreWebView2ContextMenuItem openTaskManager = webBrowser.CoreWebView2.Environment.CreateContextMenuItem(await DBOperations.DBTranslation("printPage"), null, CoreWebView2ContextMenuItemKind.Command); openTaskManager.CustomItemSelected += openTaskManagerSelected;
                menuList.Add(openConsole);
                menuList.Add(openTaskManager);
            };
            MainWindow.ProgressRing = Visibility.Hidden;
        }
        private void openConsoleSelected(object sender, object e) { webBrowser.CoreWebView2.OpenDevToolsWindow(); }
        private void openTaskManagerSelected(object sender, object e) { webBrowser.CoreWebView2.ShowPrintUI(); }
    }
    
}