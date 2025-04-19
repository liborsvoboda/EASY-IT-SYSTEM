using DocumentFormat.OpenXml.Packaging;
using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Search;
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

    public partial class WebGlobalPageBlockListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static WebGlobalPageBlockList selectedRecord = new WebGlobalPageBlockList();

        private List<SolutionMixedEnumList> pagePartType = new List<SolutionMixedEnumList>();
        private List<WebGlobalPageBlockList> WebGlobalPageBlockList = new List<WebGlobalPageBlockList>();
        private int FoundedPositionIndex = 0; private int ReplacePositionIndex = 0;

        public WebGlobalPageBlockListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);

                LoadParameters();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        public class TextEditorSearchable : TextEditor {
            public TextEditorSearchable() : base(new TextArea()) {
                SearchPanel.Install(TextArea);
            }
        }

        private async void LoadParameters() {
            DgListView.RowHeight = int.Parse(await DataOperations.ParameterCheck("WebAgendasFormsRowHeight"));
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                pagePartType = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/PagePartType", App.UserData.Authentification.Token);
                WebGlobalPageBlockList = await CommunicationManager.GetApiRequest<List<WebGlobalPageBlockList>>(ApiUrls.EasyITCenterWebGlobalPageBlockList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                pagePartType.ForEach(async ppType => { ppType.Translation = await DBOperations.DBTranslation(ppType.Name); });

                GuestHtmlContent.Header = await DBOperations.DBTranslation(GuestHtmlContent.Name);
                UserHtmlContent.Header = await DBOperations.DBTranslation(UserHtmlContent.Name);
                AdminHtmlContent.Header = await DBOperations.DBTranslation(AdminHtmlContent.Name);
                ProviderHtmlContent.Header = await DBOperations.DBTranslation(ProviderHtmlContent.Name);

               
                cb_pagePartType.ItemsSource = pagePartType; 
                DgListView.ItemsSource = WebGlobalPageBlockList;
                DgListView.Items.Refresh();
                lb_dataList.ItemsSource = WebGlobalPageBlockList;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString();
                    if (headername == "Value") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "PagePartType") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "Sequence") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "RewriteLowerLevel") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                    else if (headername == "Active") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }
                    else if (headername == "TimeStamp") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                    else if (headername == "GroupId") e.Visibility = Visibility.Hidden;
                    else if (headername == "Description") e.Visibility = Visibility.Hidden;
                    else if (headername == "GuestHtmlContent") e.Visibility = Visibility.Hidden;
                    else if (headername == "UserHtmlContent") e.Visibility = Visibility.Hidden;
                    else if (headername == "AdminHtmlContent") e.Visibility = Visibility.Hidden;
                    else if (headername == "ProviderHtmlContent") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    WebGlobalPageBlockList user = e as WebGlobalPageBlockList;
                    return user.Name.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.GuestHtmlContent) && user.GuestHtmlContent.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.UserHtmlContent) && user.UserHtmlContent.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.AdminHtmlContent) && user.AdminHtmlContent.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.ProviderHtmlContent) && user.ProviderHtmlContent.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new WebGlobalPageBlockList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (WebGlobalPageBlockList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (WebGlobalPageBlockList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterWebGlobalPageBlockList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (WebGlobalPageBlockList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (WebGlobalPageBlockList)DgListView.SelectedItem; }
            else { selectedRecord = new WebGlobalPageBlockList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            //SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, false);

        private async void BtnSaveClose_Click(object sender, RoutedEventArgs e) => await SaveRecord(true, false);

        private async void BtnSaveAsNew_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, true);

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (WebGlobalPageBlockList)DgListView.SelectedItem : new WebGlobalPageBlockList();
            SetRecord(false);
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = ".html", Filter = "Html files |*.html|All files (*.*)|*.*", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) {
                    if (GuestHtmlContent.IsSelected) { GuestHtmlContentEditor.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName)); }
                    if (UserHtmlContent.IsSelected) { UserHtmlContentEditor.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName)); }
                    if (AdminHtmlContent.IsSelected) { AdminHtmlContentEditor.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName)); }
                    if (ProviderHtmlContent.IsSelected) { ProviderHtmlContentEditor.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName)); }
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private async void BtnOpenInBrowser_Click(object sender, RoutedEventArgs e) {
            await SaveRecord(false, false);
            SystemOperations.StartExternalProccess("weburl", App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebBuilderGlobalBodyBlockPreview")) + "/" + txt_id.Value.ToString());
        }

        private async void SetRecord(bool? showForm = null, bool copy = false) {
            try {
                GuestHtmlContent.IsSelected = true;

                txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                cb_pagePartType.SelectedItem = (selectedRecord.Id == 0) ? pagePartType.First() : pagePartType.First(a => a.Name == selectedRecord.InheritedPagePartType);

                txt_sequence.Value = selectedRecord.Sequence;
                txt_name.Text = selectedRecord.Name;
                txt_description.Text = selectedRecord.Description;

                chb_active.IsChecked = selectedRecord.Active;

                GuestHtmlContentEditor.Text = selectedRecord.GuestHtmlContent;
                UserHtmlContentEditor.Text = selectedRecord.UserHtmlContent;
                AdminHtmlContentEditor.Text = selectedRecord.AdminHtmlContent;
                ProviderHtmlContentEditor.Text = selectedRecord.ProviderHtmlContent;

                if (showForm != null && showForm == true) {
                    MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                    ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
                }
                else {
                    MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                    ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private async Task<bool> SaveRecord(bool closeForm, bool asNew) {
            try {
                MainWindow.ProgressRing = Visibility.Visible;

                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) && !asNew ? txt_id.Value : 0);
                selectedRecord.InheritedPagePartType = ((SolutionMixedEnumList)cb_pagePartType.SelectedItem).Name;
                selectedRecord.Sequence = int.Parse(txt_sequence.Value.ToString());

                selectedRecord.Name = txt_name.Text;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.Active = chb_active.IsChecked.Value;

                selectedRecord.GuestHtmlContent = GuestHtmlContentEditor.Text;
                selectedRecord.UserHtmlContent = UserHtmlContentEditor.Text;
                selectedRecord.AdminHtmlContent = AdminHtmlContentEditor.Text;
                selectedRecord.ProviderHtmlContent = ProviderHtmlContentEditor.Text;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterWebGlobalPageBlockList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterWebGlobalPageBlockList, httpContent, null, App.UserData.Authentification.Token); }

                if (closeForm) { await LoadDataList(); selectedRecord = new WebGlobalPageBlockList(); SetRecord(false); }
                if (dBResult.RecordCount == 0) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
            return true;
        }

        private void HighlightCodeChanged(object sender, SelectionChangedEventArgs e) {
            if (GuestHtmlContent.IsSelected) { GuestHtmlContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString()); }
            if (UserHtmlContent.IsSelected) { UserHtmlContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString()); }
            if (AdminHtmlContent.IsSelected) { AdminHtmlContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString()); }
            if (ProviderHtmlContent.IsSelected) { ProviderHtmlContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString()); }
        }

        //Include Template on end in Editor
        private void DataListDoubleClick(object sender, MouseButtonEventArgs e) {
            if (lb_dataList.SelectedItems.Count > 0) { selectedRecord = (WebGlobalPageBlockList)lb_dataList.SelectedItem; }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private async void BtnOpenMinifiTool_Click(object sender, RoutedEventArgs e) {
            SystemOperations.StartExternalProccess("weburl", App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebBuilderExternalAutoMinifiTool")));
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
            if (GuestHtmlContent.IsSelected) { ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref GuestHtmlContentEditor, (bool)chb_caseSensitiveIgnore.IsChecked); }
            if (UserHtmlContent.IsSelected) { ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref UserHtmlContentEditor, (bool)chb_caseSensitiveIgnore.IsChecked); }
            if (AdminHtmlContent.IsSelected) { ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref AdminHtmlContentEditor, (bool)chb_caseSensitiveIgnore.IsChecked); }
            if (ProviderHtmlContent.IsSelected) { ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref ProviderHtmlContentEditor, (bool)chb_caseSensitiveIgnore.IsChecked); }
            if (FoundedPositionIndex == 0) { btn_searchText.IsEnabled = false; }
        }

        private void CodeReplaceClick(object sender, RoutedEventArgs e) {
            if (GuestHtmlContent.IsSelected) { ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref GuestHtmlContentEditor, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked); }
            if (UserHtmlContent.IsSelected) { ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref UserHtmlContentEditor, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked); }
            if (AdminHtmlContent.IsSelected) { ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref AdminHtmlContentEditor, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked); }
            if (ProviderHtmlContent.IsSelected) { ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref ProviderHtmlContentEditor, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked); }
            if (ReplacePositionIndex == 0) { btn_codeReplace.IsEnabled = false; }
        }
    }
}