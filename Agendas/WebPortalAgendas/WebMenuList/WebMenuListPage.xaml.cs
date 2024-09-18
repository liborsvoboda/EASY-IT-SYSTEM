using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Web.WebView2.Core;
using WebView2.DevTools.Dom;
using System.Timers;
using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using ColorCode.Compilation.Languages;


namespace EasyITSystemCenter.Pages {

    public partial class WebMenuListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static WebMenuList selectedRecord = new WebMenuList();

        private List<WebMenuList> WebMenuList = new List<WebMenuList>();
        private List<WebGroupMenuList> parentClassList = new List<WebGroupMenuList>();
        private List<WebCodeLibraryList> webCodeLibraryList = new List<WebCodeLibraryList>();
        private int FoundedPositionIndex = 0; private int ReplacePositionIndex = 0;


        private WebView2DevToolsContext devToolsContext;
        private string WebBrowserData = ""; private bool DataLoaded = false; private int stepCount = 0;
        private readonly static Timer WebBrowserTimer = new Timer() { Enabled = false, Interval = 1000 };


        public WebMenuListPage() {
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
            DgListView.RowHeight = int.Parse(await DataOperations.ParameterCheck("WebAgendasFormsRowHeight"));
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                await webBrowser.EnsureCoreWebView2Async(App.appRuntimeData.WebViewEnvironment);

                parentClassList = await CommunicationManager.GetApiRequest<List<WebGroupMenuList>>(ApiUrls.EasyITCenterWebGroupMenuList, null, App.UserData.Authentification.Token);
                WebMenuList = await CommunicationManager.GetApiRequest<List<WebMenuList>>(ApiUrls.EasyITCenterWebMenuList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                webCodeLibraryList = await CommunicationManager.GetApiRequest<List<WebCodeLibraryList>>(ApiUrls.EasyITCenterWebCodeLibraryList, null, App.UserData.Authentification.Token);
                WebMenuList.ForEach(user => { user.GroupName = parentClassList.First(a => a.Id == user.GroupId).Name; });

                DgListView.ItemsSource = WebMenuList;
                DgListView.Items.Refresh();
                cb_parentId.ItemsSource = parentClassList;
                lb_dataList.ItemsSource = webCodeLibraryList;

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }


        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "Value".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "GroupName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "Sequence".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "UserMenu".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                    else if (headername == "AdminMenu".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }
                    else if (headername == "Default".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 6; }
                    else if (headername == "Active".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 7; }
                    else if (headername == "Description".ToLower()) e.Header = await DBOperations.DBTranslation(headername);

                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "GroupId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "HtmlContent".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "UserIPAddress".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "MenuClass".ToLower()) e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    WebMenuList search = e as WebMenuList;
                    return search.Name.ToLower().Contains(filter.ToLower())
                    || search.GroupName.ToLower().Contains(filter.ToLower())
                    || search.HtmlContent.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.Description) && search.Description.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new WebMenuList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (WebMenuList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (WebMenuList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterWebMenuList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (WebMenuList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (WebMenuList)DgListView.SelectedItem; }
            else { selectedRecord = new WebMenuList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            //SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, false);
        private async void BtnSaveClose_Click(object sender, RoutedEventArgs e) => await SaveRecord(true, false);
        private async void BtnSaveAsNew_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, true);

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (WebMenuList)DgListView.SelectedItem : new WebMenuList();
            SetRecord(false);
        }


        private async void SetRecord(bool? showForm = null, bool copy = false) {
            try {
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                txt_sequence.Value = selectedRecord.Sequence;
                cb_parentId.SelectedItem = (selectedRecord.Id == 0) ? parentClassList.FirstOrDefault() : parentClassList.FirstOrDefault(a => a.Id == selectedRecord.GroupId);

                txt_name.Text = selectedRecord.Name;
                txt_menuClass.Text = selectedRecord.MenuClass;
                txt_description.Text = selectedRecord.Description;
                chb_userMenu.IsChecked = selectedRecord.UserMenu;
                chb_adminMenu.IsChecked = selectedRecord.AdminMenu;
                chb_default.IsChecked = selectedRecord.Default;
                chb_active.IsChecked = selectedRecord.Active;

                WebBrowserData = selectedRecord.HtmlContent; DataLoaded = false; await JSSetDataFromBrowser(WebBrowserData);

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private async Task<bool> SaveRecord(bool closeForm, bool asNew) {
            try {
                MainWindow.ProgressRing = Visibility.Visible;

                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) && !asNew ? txt_id.Value : 0);

                selectedRecord.Sequence = int.Parse(txt_sequence.Value.ToString());
                selectedRecord.GroupId = cb_parentId.SelectedItem == null ? 0 : ((WebGroupMenuList)cb_parentId.SelectedItem).Id;

                selectedRecord.Name = txt_name.Text;
                selectedRecord.MenuClass = txt_menuClass.Text;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.UserMenu = chb_userMenu.IsChecked.Value;
                selectedRecord.AdminMenu = chb_adminMenu.IsChecked.Value;
                selectedRecord.Active = chb_active.IsChecked.Value;
                selectedRecord.Default = chb_default.IsChecked.Value;

                selectedRecord.HtmlContent = await JSGetDataFromBrowser();
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterWebMenuList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterWebMenuList, httpContent, null, App.UserData.Authentification.Token); }

                if (closeForm) { await LoadDataList(); selectedRecord = new WebMenuList(); SetRecord(false); }
                if (dBResult.RecordCount == 0) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
            return true;
        }

        //Include Template on end in Editor
        private void DataListDoubleClick(object sender, MouseButtonEventArgs e) {
            if (lb_dataList.SelectedItems.Count > 0) {
                //if ((bool)EditorSelector.IsChecked) { html_htmlContent.Editor.Text += ((WebCodeLibraryList)lb_dataList.SelectedItem).Content; }
                //else { txt_codeContent.Text += ((WebCodeLibraryList)lb_dataList.SelectedItem).Content; }
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
                if (!DataLoaded && stepCount < 5) {
                    bool result = await JSSetDataFromBrowser(WebBrowserData, false); if (!result) { WebBrowserTimer.Enabled = true; }
                    else { WebBrowserTimer.Enabled = false; MainWindow.ProgressRing = Visibility.Hidden; }
                } else if (DataLoaded) { MainWindow.ProgressRing = Visibility.Hidden; }
                else if (!DataLoaded && stepCount >= 5) {
                    MainWindow.ProgressRing = Visibility.Hidden; WebBrowserTimer.Enabled = false; stepCount = 0;
                    await MainWindow.ShowMessageOnMainWindow(true, "LoadingError: " + await DBOperations.DBTranslation("DataCannotBeWritten"));
                }
            });
        }


        private async void WebBrowser_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e) {
            MainWindow.ProgressRing = Visibility.Visible;
            if (!DataLoaded) { bool result = await JSSetDataFromBrowser(selectedRecord.HtmlContent);
                if (!result) { WebBrowserTimer.Enabled = true; }
            } else { MainWindow.ProgressRing = Visibility.Hidden; }
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
            try { if (!DataLoaded && stepCount < 5) {
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
            try { check = Regex.Unescape(await webBrowser.CoreWebView2.ExecuteScriptAsync($"GetWebViewFromEditorData()")).Remove(0, 1);
                check = check.Remove(check.Length - 1, 1);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            return check;
        }
    }
}