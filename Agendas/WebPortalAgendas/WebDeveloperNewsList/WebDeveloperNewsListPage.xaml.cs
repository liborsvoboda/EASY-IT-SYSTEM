using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebView2.DevTools.Dom;

namespace EasyITSystemCenter.Pages {

    public partial class WebDeveloperNewsListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static WebDeveloperNewsList selectedRecord = new WebDeveloperNewsList();

        private List<WebDeveloperNewsList> webDeveloperNewsList = new List<WebDeveloperNewsList>();


        private WebView2DevToolsContext devToolsContext;
        private string WebBrowserData = ""; private bool DataLoaded = false; private int stepCount = 0;
        private readonly static Timer WebBrowserTimer = new Timer() { Enabled = false, Interval = 1000 };


        public WebDeveloperNewsListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);
                webBrowser.CoreWebView2InitializationCompleted += WebBrowser_CoreWebView2InitializationCompleted;
                WebBrowserTimer.Elapsed += WebMenuListPage_Elapsed;


                LoadParameters();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        private async void LoadParameters() {
            DgListView.RowHeight = int.Parse(await DataOperations.ParameterCheck("DialsFormsRowHeight"));
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                await webBrowser.EnsureCoreWebView2Async(App.appRuntimeData.WebViewEnvironment);
                webDeveloperNewsList = await CommunicationManager.GetApiRequest<List<WebDeveloperNewsList>>(ApiUrls.EasyITCenterWebDeveloperNewsList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                DgListView.ItemsSource = webDeveloperNewsList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString();
                    if (headername == "Title") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "Active") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "TimeStamp") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                    else if (headername == "Description") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    WebDeveloperNewsList user = e as WebDeveloperNewsList;
                    return user.Title.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new WebDeveloperNewsList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (WebDeveloperNewsList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (WebDeveloperNewsList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterWebDeveloperNewsList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (WebDeveloperNewsList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (WebDeveloperNewsList)DgListView.SelectedItem; } else { selectedRecord = new WebDeveloperNewsList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.Title = txt_title.Text;
                selectedRecord.Description = await JSGetDataFromBrowser();

                selectedRecord.Active = chb_active.IsChecked.Value;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterWebDeveloperNewsList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterWebDeveloperNewsList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new WebDeveloperNewsList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (WebDeveloperNewsList)DgListView.SelectedItem : new WebDeveloperNewsList();
            SetRecord(false);
        }

        private async void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;
            txt_title.Text = selectedRecord.Title;
            chb_active.IsChecked = selectedRecord.Active;

            WebBrowserData = selectedRecord.Description; DataLoaded = false; await JSSetDataFromBrowser(WebBrowserData);

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }


        //-----------------------------------------------------------
        //WebView2 Control Code





        private async void WebBrowser_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e) {
            try
            {
                devToolsContext = await webBrowser.CoreWebView2.CreateDevToolsContextAsync();
                webBrowser.NavigationCompleted += WebBrowser_NavigationCompleted;
                webBrowser.CoreWebView2.Navigate(App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_localWebServerUrl").Value + "DocumentEditor/SunEditor");
                webBrowser.CoreWebView2.Settings.AreDevToolsEnabled = false;
            } catch { }
        }


        private async void WebMenuListPage_Elapsed(object sender, ElapsedEventArgs e) {
            await this.Invoke(async () =>
            {
                if (!DataLoaded && stepCount < 5)
                {
                    bool result = await JSSetDataFromBrowser(WebBrowserData, false); if (!result) { WebBrowserTimer.Enabled = true; }
                    else { WebBrowserTimer.Enabled = false; MainWindow.ProgressRing = Visibility.Hidden; }
                }
                else if (DataLoaded) { MainWindow.ProgressRing = Visibility.Hidden; }
                else if (!DataLoaded && stepCount >= 5)
                {
                    MainWindow.ProgressRing = Visibility.Hidden; WebBrowserTimer.Enabled = false; stepCount = 0;
                    await MainWindow.ShowMessageOnMainWindow(true, "LoadingError: " + await DBOperations.DBTranslation("DataCannotBeWritten"));
                }
            });
        }


        private async void WebBrowser_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e) {
            MainWindow.ProgressRing = Visibility.Visible;
            if (!DataLoaded) {
                bool result = await JSSetDataFromBrowser(selectedRecord.Description);
                if (!result) { WebBrowserTimer.Enabled = true; }
            }
            else { MainWindow.ProgressRing = Visibility.Hidden; }
        }


        /// <summary>
        /// WebView2 Global JsScript Setting Method 
        /// Using Default: SetWebViewToEditorData({inputHtml})
        /// TODO AutoUpdate/LandServer,Load From File - Insert Metro Menu Over Script
        /// </summary>
        /// <param name="inputHtml"></param>
        /// <param name="isNewRequest"></param>
        /// <returns></returns>
        private async Task<bool> JSSetDataFromBrowser(string inputHtml, bool isNewRequest = true) {
            stepCount = isNewRequest ? 0 : stepCount; DataLoaded = isNewRequest ? false : DataLoaded;
            if (webBrowser.CoreWebView2 == null) { await webBrowser.EnsureCoreWebView2Async(App.appRuntimeData.WebViewEnvironment); }
            devToolsContext = await webBrowser.CoreWebView2.CreateDevToolsContextAsync();
            try
            {
                if (!DataLoaded && stepCount < 5)
                {
                    string result = await webBrowser.ExecuteScriptAsync($"SetWebViewToEditorData({JsonConvert.SerializeObject(inputHtml)})");
                    if (result == "true") { DataLoaded = true; }
                    else { stepCount += 1; WebBrowserTimer.Enabled = true; MainWindow.ProgressRing = Visibility.Visible; }
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            return DataLoaded ? true : false;
        }


        /// <summary>
        /// WebView2 Global JsScript Setting Method 
        /// Using Default: GetWebViewFromEditorData({inputHtml})
        /// </summary>
        /// <returns></returns>
        private async Task<string> JSGetDataFromBrowser() {
            string check = null;
            if (webBrowser.CoreWebView2 == null) { await webBrowser.EnsureCoreWebView2Async(App.appRuntimeData.WebViewEnvironment); }
            devToolsContext = await webBrowser.CoreWebView2.CreateDevToolsContextAsync();
            try {
                check = Regex.Unescape(await webBrowser.CoreWebView2.ExecuteScriptAsync($"GetWebViewFromEditorData()")).Remove(0, 1);
                check = check.Remove(check.Length - 1, 1);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            return check;
        }
    }
}