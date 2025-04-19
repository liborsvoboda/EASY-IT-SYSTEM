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

    public partial class WebDocumentationCodeLibraryListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static WebDocumentationCodeLibraryList selectedRecord = new WebDocumentationCodeLibraryList();

        private List<WebDocumentationCodeLibraryList> documentationCodeLibraryList = new List<WebDocumentationCodeLibraryList>();
        private int FoundedPositionIndex = 0; private int ReplacePositionIndex = 0;

        public WebDocumentationCodeLibraryListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                try {
                    _ = FormOperations.TranslateFormFields(ListForm);
                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

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
                documentationCodeLibraryList = await CommunicationManager.GetApiRequest<List<WebDocumentationCodeLibraryList>>(ApiUrls.EasyITCenterWebDocumentationCodeLibraryList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                DgListView.ItemsSource = documentationCodeLibraryList;
                DgListView.Items.Refresh();
                lb_dataList.ItemsSource = documentationCodeLibraryList;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(e => {
                    string headername = e.Header.ToString();
                    if (headername == "Value") e.Header = Resources["name"].ToString();
                    else if (headername == "Description") e.Header = Resources["description"].ToString();
                    else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                    else if (headername == "MdContent") e.Visibility = Visibility.Hidden;
                    else if (headername == "HtmlContent") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    WebDocumentationCodeLibraryList user = e as WebDocumentationCodeLibraryList;
                    return user.Name.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.MdContent) && user.MdContent.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new WebDocumentationCodeLibraryList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (WebDocumentationCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (WebDocumentationCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterWebDocumentationCodeLibraryList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (WebDocumentationCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (WebDocumentationCodeLibraryList)DgListView.SelectedItem; }
            else { selectedRecord = new WebDocumentationCodeLibraryList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;

            txt_name.Text = selectedRecord.Name;
            txt_description.Text = selectedRecord.Description;
            md_editor.Text = selectedRecord.MdContent;

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, false);

        private async void BtnSaveClose_Click(object sender, RoutedEventArgs e) => await SaveRecord(true, false);

        private async void BtnSaveAsNew_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, true);

        private async void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (WebDocumentationCodeLibraryList)DgListView.SelectedItem : new WebDocumentationCodeLibraryList();
            await LoadDataList();
            SetRecord(false);
        }

        private async Task<bool> SaveRecord(bool closeForm, bool asNew) {
            try {
                MainWindow.ProgressRing = Visibility.Visible;

                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) && !asNew ? txt_id.Value : 0);
                selectedRecord.Name = txt_name.Text;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.MdContent = md_editor.Text;
                selectedRecord.HtmlContent = html_htmlContent.Browser.GetCurrentHtml();

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterWebDocumentationCodeLibraryList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterWebDocumentationCodeLibraryList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0 && asNew) { await LoadDataList(); }
                if (closeForm) { await LoadDataList(); selectedRecord = new WebDocumentationCodeLibraryList(); SetRecord(false); }
                if (dBResult.RecordCount == 0) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
            return true;
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = ".md", Filter = "Markdown files |*.md|All files (*.*)|*.*", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) {
                    md_editor.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName));
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void CodeEditorChanged(object sender, EventArgs e) {
            EASYTools.MarkdownToHtml.Markdown markdown = new EASYTools.MarkdownToHtml.Markdown();
            html_htmlContent.Browser.OpenDocument(markdown.Transform("<HEAD><META content=text/html;utf-8 http-equiv=content-type></HEAD>" + md_editor.Text));
        }

        private void DataListDoubleClick(object sender, MouseButtonEventArgs e) {
            if (lb_dataList.SelectedItems.Count > 0) {
                selectedRecord = ((WebDocumentationCodeLibraryList)lb_dataList.SelectedItem);
                SetRecord(true);
            }
        }

        private async void BtnOpenInBrowser_Click(object sender, RoutedEventArgs e) {
            await SaveRecord(false, false);
            SystemOperations.StartExternalProccess("weburl", App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebDocLibraryPreview")) + "/" + txt_id.Value.ToString());
        }

        private void HighlightCodeChanged(object sender, SelectionChangedEventArgs e) {
            md_editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString());
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
            ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref md_editor, (bool)chb_caseSensitiveIgnore.IsChecked);
            if (FoundedPositionIndex == 0) { btn_searchText.IsEnabled = false; }
        }

        private void CodeReplaceClick(object sender, RoutedEventArgs e) {
            ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref md_editor, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked);
            if (ReplacePositionIndex == 0) { btn_codeReplace.IsEnabled = false; }
        }
    }
}