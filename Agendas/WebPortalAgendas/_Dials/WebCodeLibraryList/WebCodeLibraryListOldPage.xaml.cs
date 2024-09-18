using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using ICSharpCode.AvalonEdit.Highlighting;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyITSystemCenter.Pages {

    public partial class WebCodeLibraryListOldPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static WebCodeLibraryList selectedRecord = new WebCodeLibraryList();

        private List<WebCodeLibraryList> webCodeLibraryList = new List<WebCodeLibraryList>();
        private int FoundedPositionIndex = 0; private int ReplacePositionIndex = 0;

        public WebCodeLibraryListOldPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);

                lb_id.Header = DBOperations.DBTranslation("id").GetAwaiter().GetResult();
                lb_name.Header = DBOperations.DBTranslation("name").GetAwaiter().GetResult();

                html_htmlContent.HtmlContentDisableInitialChange = true;
                html_htmlContent.Toolbar.SetSourceMode(true);
                html_htmlContent.Browser.ToggleSourceEditor(html_htmlContent.Toolbar, true);

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
                webCodeLibraryList = await CommunicationManager.GetApiRequest<List<WebCodeLibraryList>>(ApiUrls.EasyITCenterWebCodeLibraryList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                DgListView.ItemsSource = webCodeLibraryList;
                lb_dataList.ItemsSource = webCodeLibraryList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(e => {
                    string headername = e.Header.ToString();
                    if (headername == "Value") { e.Header = Resources["fname"].ToString(); e.DisplayIndex = 1; }
                    else if (headername == "Description") e.Header = Resources["description"].ToString();
                    else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                    else if (headername == "HtmlContent") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    WebCodeLibraryList user = e as WebCodeLibraryList;
                    return user.Name.ToLower().Contains(filter.ToLower())
                    || user.Content.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new WebCodeLibraryList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (WebCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (WebCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterWebCodeLibraryList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (WebCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (WebCodeLibraryList)DgListView.SelectedItem; }
            else { selectedRecord = new WebCodeLibraryList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, false);

        private async void BtnSaveClose_Click(object sender, RoutedEventArgs e) => await SaveRecord(true, false);

        private async void BtnSaveAsNew_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, true);

        private async void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (WebCodeLibraryList)DgListView.SelectedItem : new WebCodeLibraryList();
            await LoadDataList();
            SetRecord(false);
        }

        private async void SetRecord(bool? showForm = null, bool copy = false) {
            EditorSelector.IsChecked = bool.Parse(await DataOperations.ParameterCheck("WebBuilderHtmlEditorIsDefault"));
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;

            lb_dataList.SelectedItem = selectedRecord.Id == 0 ? null : selectedRecord;
            txt_name.Text = selectedRecord.Name;
            txt_description.Text = selectedRecord.Description;

            if ((bool)EditorSelector.IsChecked) { html_htmlContent.Browser.OpenDocument(selectedRecord.Content); }
            else { txt_codeContent.Text = selectedRecord.Content; }

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = ".html", Filter = "Html files |*.html; *.cshtml; *.js; *.css|All files (*.*)|*.*", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) {
                    if ((bool)EditorSelector.IsChecked) { html_htmlContent.Browser.OpenDocument(File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName))); }
                    else { txt_codeContent.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName)); }
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private async void BtnOpenInBrowser_Click(object sender, RoutedEventArgs e) {
            await SaveRecord(false, false);
            SystemOperations.StartExternalProccess(SystemLocalEnumSets.ProcessTypes.First(a => a.Value.ToLower() == "url").Value, App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebBuilderCodePreview")) + "/" + txt_id.Value.ToString());
        }

        private async Task<bool> SaveRecord(bool closeForm, bool asNew) {
            try {
                MainWindow.ProgressRing = Visibility.Visible;
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) && !asNew ? txt_id.Value : 0);
                selectedRecord.Name = txt_name.Text;
                selectedRecord.Description = txt_description.Text;

                if ((bool)EditorSelector.IsChecked) { selectedRecord.Content = html_htmlContent.Browser.GetCurrentHtml(); }
                else { selectedRecord.Content = txt_codeContent.Text; }

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterWebCodeLibraryList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterWebCodeLibraryList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) { await LoadDataList(); }
                if (closeForm) { selectedRecord = new WebCodeLibraryList(); SetRecord(false); }
                if (dBResult.RecordCount == 0) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
            return true;
        }

        private void DataListDoubleClick(object sender, MouseButtonEventArgs e) {
            if (lb_dataList.SelectedItems.Count > 0) { selectedRecord = (WebCodeLibraryList)lb_dataList.SelectedItem; }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void HighlightCodeChanged(object sender, SelectionChangedEventArgs e) {
            txt_codeContent.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString());
        }

        private void EditorSelectorStatus(object sender, RoutedEventArgs e) {
            if ((bool)EditorSelector.IsChecked) {
                html_htmlContent.Browser.OpenDocument(selectedRecord.Content);
                html_htmlContent.Visibility = Visibility.Visible;
                txt_codeContent.Visibility = Visibility.Hidden;
                code_selector.Visibility = Visibility.Hidden;
            }
            else {
                txt_codeContent.Text = selectedRecord.Content;
                code_selector.Visibility = Visibility.Visible;
                txt_codeContent.Visibility = Visibility.Visible;
                html_htmlContent.Visibility = Visibility.Hidden;
            }
        }

        private void CaseSensitiveChange(object sender, RoutedEventArgs e) {
            if (dataViewSupport.FormShown && btn_searchText != null) { btn_searchText.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0; }
        }

        private void CodeSearchTextChanged(object sender, TextChangedEventArgs e) {
            btn_searchText.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0; SearchTextInEditor();
        }

        private void SearchText_Click(object sender, RoutedEventArgs e) {
            SearchTextInEditor();
        }

        private void SelectedOnlyChange(object sender, RoutedEventArgs e) {
            if (dataViewSupport.FormShown && btn_codeReplace != null) { btn_codeReplace.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0; }
        }

        private void CodeReplaceTextChanged(object sender, TextChangedEventArgs e) {
            btn_codeReplace.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0;
        }

        private void SearchTextInEditor() {
            ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref txt_codeContent, (bool)chb_caseSensitiveIgnore.IsChecked);
            if (FoundedPositionIndex == 0) { btn_searchText.IsEnabled = false; }
        }

        private void CodeReplaceClick(object sender, RoutedEventArgs e) {
            ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref txt_codeContent, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked);
            if (ReplacePositionIndex == 0) { btn_codeReplace.IsEnabled = false; }
        }
    }
}