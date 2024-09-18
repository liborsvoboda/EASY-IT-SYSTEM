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
using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;

namespace EasyITSystemCenter.Pages {

    public partial class SolutionEmailerHistoryListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionEmailerHistoryList selectedRecord = new SolutionEmailerHistoryList();


        private List<SolutionEmailerHistoryList> EmailerHistoryList = new List<SolutionEmailerHistoryList>();
        private bool changeEditorView = false;

        public SolutionEmailerHistoryListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {

                try {
                    _ = FormOperations.TranslateFormFields(ListForm);
                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                //html_htmlMessage.HtmlContentDisableInitialChange = true;
                //html_htmlMessage.Toolbar.SetSourceMode(true);
                //html_htmlMessage.Browser.ToggleSourceEditor(html_htmlMessage.Toolbar, true);

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                EmailerHistoryList = await CommunicationManager.GetApiRequest<List<SolutionEmailerHistoryList>>(ApiUrls.EasyITCenterSolutionEmailerHistoryList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                DgListView.ItemsSource = EmailerHistoryList;
                DgListView.Items.Refresh();
                
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }


            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "Recipient".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "Subject".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "Status".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }

                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }

                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "Email".ToLower()) e.Visibility = Visibility.Hidden;

                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SolutionEmailerHistoryList filterColumns = e as SolutionEmailerHistoryList;
                    return filterColumns.Subject.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(filterColumns.Recipient) && filterColumns.Recipient.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(filterColumns.Email) && filterColumns.Email.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new SolutionEmailerHistoryList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SolutionEmailerHistoryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SolutionEmailerHistoryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterSolutionEmailerHistoryList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SolutionEmailerHistoryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SolutionEmailerHistoryList)DgListView.SelectedItem; } else { selectedRecord = new SolutionEmailerHistoryList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                if (!html_htmlMessage.Browser.IsInSourceEditMode) { html_htmlMessage.Browser.ToggleSourceEditor(html_htmlMessage.Toolbar, true); changeEditorView = true; } else { changeEditorView = false; }

                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSolutionEmailerHistoryList, httpContent, null, App.UserData.Authentification.Token);
                } else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSolutionEmailerHistoryList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SolutionEmailerHistoryList();
                    await LoadDataList();
                    SetRecord(false);
                } else { await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            if (changeEditorView) { html_htmlMessage.Browser.ToggleSourceEditor(html_htmlMessage.Toolbar, false);changeEditorView = false; }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SolutionEmailerHistoryList)DgListView.SelectedItem : new SolutionEmailerHistoryList();
            SetRecord(false);
        }

        private void SetRecord(bool showForm, bool copy = false) {
            try {
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                txt_recipients.Text = selectedRecord.Recipient;
                txt_subject.Text = selectedRecord.Subject;
                html_htmlMessage.Browser.OpenDocument(selectedRecord.Email);
                txt_status.Text = selectedRecord.Status;

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            } else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

       
    }
}