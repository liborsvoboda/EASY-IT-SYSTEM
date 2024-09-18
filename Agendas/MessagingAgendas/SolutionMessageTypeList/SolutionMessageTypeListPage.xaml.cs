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

    public partial class SolutionMessageTypeListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionMessageTypeList selectedRecord = new SolutionMessageTypeList();

        private List<SolutionMessageTypeList> MessageTypeList = new List<SolutionMessageTypeList>();

        public SolutionMessageTypeListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                MessageTypeList = await CommunicationManager.GetApiRequest<List<SolutionMessageTypeList>>(ApiUrls.SolutionMessageTypeList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                MessageTypeList.ForEach(async messageType => { messageType.Translation = await DBOperations.DBTranslation(messageType.Name); });
                    DgListView.ItemsSource = MessageTypeList;
                    DgListView.Items.Refresh();


            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "name") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "translation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); ; e.DisplayIndex = 2; }
                    else if (headername == "variables".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); ; e.DisplayIndex = 3; }
                    else if (headername == "description".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); ; e.DisplayIndex = 4; }
                    else if (headername == "AnswerAllowed".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); ; e.DisplayIndex = 5; }
                    else if (headername == "isSystemOnly".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); ; e.DisplayIndex = 6; }

                    else if (headername == "timestamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); ; e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; } 
                    
                    else if (headername == "id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "userid".ToLower()) e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SolutionMessageTypeList filterColumns = e as SolutionMessageTypeList;
                    return filterColumns.Name.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(filterColumns.Variables) && filterColumns.Variables.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(filterColumns.Description) && filterColumns.Description.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new SolutionMessageTypeList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SolutionMessageTypeList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SolutionMessageTypeList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.SolutionMessageTypeList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SolutionMessageTypeList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SolutionMessageTypeList)DgListView.SelectedItem; } else { selectedRecord = new SolutionMessageTypeList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.Name = txt_name.Text;
                selectedRecord.Variables = txt_variables.Text;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.AnswerAllowed = (bool)chb_answerAllowed.IsChecked;
                selectedRecord.IsSystemOnly = (bool)chb_isSystemOnly.IsChecked;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.SolutionMessageTypeList, httpContent, null, App.UserData.Authentification.Token);
                } else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.SolutionMessageTypeList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SolutionMessageTypeList();
                    await LoadDataList();
                    SetRecord(false);
                } else { await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SolutionMessageTypeList)DgListView.SelectedItem : new SolutionMessageTypeList();
            SetRecord(false);
        }

        private void SetRecord(bool showForm, bool copy = false) {
            try { 
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;
                txt_name.Text = selectedRecord.Name;
                txt_variables.Text = selectedRecord.Variables;
                txt_description.Text = selectedRecord.Description;
                chb_answerAllowed.IsChecked = selectedRecord.AnswerAllowed;
                chb_isSystemOnly.IsChecked = selectedRecord.IsSystemOnly;

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            if (showForm) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            } else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = false;
            }
        }
    }
}