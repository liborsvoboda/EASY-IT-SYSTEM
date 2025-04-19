using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Markdig;
using Markdig.Wpf;
using Pek.Markdig.HighlightJs;


namespace EasyITSystemCenter.Pages {

    public partial class MltEicSrvWebAppPage : UserControl {

        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static TemplateClassList selectedRecord = new TemplateClassList();

        SystemCustomPageList systemCustomPageList = new SystemCustomPageList();
        //private string serverUrl = $"http://127.0.0.1:" + SystemOperations.GetSystemLocalhostFreePort().ToString() +"/";

        public MltEicSrvWebAppPage() {
            try {

                InitializeComponent();
                
                _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);
                if (!App.appRuntimeData.webServerRunning) {
                    _ = MainWindow.ShowMessageOnMainWindow(true, DBOperations.DBTranslation("WebServerNotRunningCheckClientConfiguration").GetAwaiter().GetResult());
                }
                else { IsEnabled = false; IsEnabledChanged += (s, e) => { if (IsEnabled) { _ = LoadDataList(); } }; }

                new MarkdownPipelineBuilder().UseEmphasisExtras().UseAbbreviations().UseAdvancedExtensions().UseBootstrap()
                .UseDiagrams().UseEmphasisExtras().UseEmojiAndSmiley(true).UseDefinitionLists().UseTableOfContent().UseTaskLists()
                .UseSupportedExtensions().UseSmartyPants().UsePipeTables().UseMediaLinks().UseMathematics().UseListExtras().UseHighlightJs()
                .UseGridTables().UseGlobalization().UseGenericAttributes().UseFootnotes().UseFooters().UseSyntaxHighlighting().UseFigures().Build();

            } catch (Exception ex) { }
            MainWindow.ProgressRing = Visibility.Hidden;
        }


        //change datasource
        public async Task<bool> LoadDataList() {
            List<SolutionUserList> userList = new List<SolutionUserList>();
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                systemCustomPageList = await CommunicationManager.GetApiRequest<SystemCustomPageList>(ApiUrls.EasyITCenterSystemCustomPageList, this.Uid.ToString(), App.UserData.Authentification.Token);
                webBrowser.CoreWebView2InitializationCompleted += WebBrowser_CoreWebView2InitializationCompleted;
                await webBrowser.EnsureCoreWebView2Async(App.appRuntimeData.WebViewEnvironment);

                //TODO udelat vyber na markdown viewer nebo browser
                if (systemCustomPageList.ShowHelpTab && systemCustomPageList.InheritedSystemApiCallType.Contains("ApiUrl")) { await helpWebBrowser.EnsureCoreWebView2Async(App.appRuntimeData.WebViewEnvironment); }

                _ = FormOperations.TranslateFormFields(ListView);
                
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        //INIT SPA Solution + Help 
        private async void WebBrowser_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e) {
            try {
              

                //TODO IS OWN SERVER Variant
                webBrowser.CoreWebView2.Navigate(systemCustomPageList.IsServerUrl ? App.appRuntimeData.AppClientSettings.First(a => a.Key == "conn_apiAddress").Value +
                    (systemCustomPageList.StartupUrl.StartsWith("/") ? systemCustomPageList.StartupUrl : "/" + systemCustomPageList.StartupUrl)
                    : systemCustomPageList.IsSystemUrl ? App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_localWebServerUrl").Value + systemCustomPageList.StartupUrl
                    : systemCustomPageList.StartupUrl
                    );

                if (systemCustomPageList.DevModeEnabled) { webBrowser.CoreWebView2.Settings.AreDevToolsEnabled = true; } 
                else { webBrowser.CoreWebView2.Settings.AreDevToolsEnabled = false; }


                if (systemCustomPageList.ShowHelpTab && systemCustomPageList.HelpTabUrl != null) {

                    switch (systemCustomPageList.InheritedSystemApiCallType.ToLower())
                    {
                        case "eicserverapiurl":
                            ti_helpUrl.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                            helpWebBrowser.CoreWebView2.Navigate(App.appRuntimeData.AppClientSettings.First(a => a.Key == "conn_apiAddress").Value + $"{(systemCustomPageList.HelpTabUrl.StartsWith("/") ? systemCustomPageList.HelpTabUrl : "/" + systemCustomPageList.HelpTabUrl)}");
                            break;
                        case "esbsystemhelpurl":
                            ti_helpUrl.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                            helpWebBrowser.CoreWebView2.Navigate(App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_localWebServerUrl").Value + systemCustomPageList.HelpTabUrl);
                            break;
                        case "publicwebhelpurl":
                            ti_helpUrl.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                            helpWebBrowser.CoreWebView2.Navigate(systemCustomPageList.HelpTabUrl);
                            break;
                        case "EicServerAuthGetApiContent":
                            ti_helpDoc.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                            helpDocument.SetCurrentValue(MarkdownViewer.MarkdownProperty, await CommunicationManager.ApiManagerGetRequest(UrlSourceTypes.EicWebServerAuth, systemCustomPageList.HelpTabUrl));
                            break;
                        case "EicServerGetApiContent":
                            ti_helpDoc.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                            helpDocument.SetCurrentValue(MarkdownViewer.MarkdownProperty, await CommunicationManager.ApiManagerGetRequest(UrlSourceTypes.EicWebServer, systemCustomPageList.HelpTabUrl));
                            break;
                        case "EsbSystemGetApiContent":
                            ti_helpDoc.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                            helpDocument.SetCurrentValue(MarkdownViewer.MarkdownProperty, await CommunicationManager.ApiManagerGetRequest(UrlSourceTypes.EsbWebServer, systemCustomPageList.HelpTabUrl));
                            //helpDocument.Markdown = File.ReadAllText(Path.Combine(App.appRuntimeData.webDataUrlPath) + FileOperations.ConvertSystemFilePathFromUrl(systemCustomPageList.HelpTabUrl));
                            break;
                        case "PublicWebGetApiContent":
                            ti_helpDoc.SetCurrentValue(VisibilityProperty, Visibility.Visible);
                            helpDocument.SetCurrentValue(MarkdownViewer.MarkdownProperty, await CommunicationManager.ApiManagerGetRequest(UrlSourceTypes.WebUrl, systemCustomPageList.HelpTabUrl));
                            break;
                    }
                }

                webBrowser.CoreWebView2.ContextMenuRequested += async delegate (object sender1, CoreWebView2ContextMenuRequestedEventArgs args) {
                    IList<CoreWebView2ContextMenuItem> menuList = args.MenuItems;
                    CoreWebView2ContextMenuItem openConsole = webBrowser.CoreWebView2.Environment.CreateContextMenuItem(await DBOperations.DBTranslation("openConsole"), null, CoreWebView2ContextMenuItemKind.Command); openConsole.CustomItemSelected += openConsoleSelected;
                    CoreWebView2ContextMenuItem openTaskManager = webBrowser.CoreWebView2.Environment.CreateContextMenuItem(await DBOperations.DBTranslation("printPage"), null, CoreWebView2ContextMenuItemKind.Command); openTaskManager.CustomItemSelected += openTaskManagerSelected;
                    
                    menuList.Add(openConsole);
                    menuList.Add(openTaskManager);
                };


            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
        }
        private void openConsoleSelected(object sender, object e) { webBrowser.CoreWebView2.OpenDevToolsWindow(); }
        private void openTaskManagerSelected(object sender, object e) { webBrowser.CoreWebView2.ShowPrintUI(); }

    }
}