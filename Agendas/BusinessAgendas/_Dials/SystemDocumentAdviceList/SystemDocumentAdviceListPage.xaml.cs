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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyITSystemCenter.Pages {

    public partial class SystemDocumentAdviceListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SystemDocumentAdviceList selectedRecord = new SystemDocumentAdviceList();

        private List<BusinessBranchList> branchList = new List<BusinessBranchList>();
        private List<SystemDocumentTypeList> documentTypeList = new List<SystemDocumentTypeList>();

        public SystemDocumentAdviceListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<SystemDocumentAdviceList> documentAdviceList = new List<SystemDocumentAdviceList>();
            try {
                cb_branch.ItemsSource = branchList = await CommunicationManager.GetApiRequest<List<BusinessBranchList>>(ApiUrls.EasyITCenterBusinessBranchList, null, App.UserData.Authentification.Token);
                documentAdviceList = await CommunicationManager.GetApiRequest<List<SystemDocumentAdviceList>>(ApiUrls.EasyITCenterSystemDocumentAdviceList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                documentTypeList = await CommunicationManager.GetApiRequest<List<SystemDocumentTypeList>>(ApiUrls.EasyITCenterSystemDocumentTypeList, null, App.UserData.Authentification.Token);

                documentTypeList.ForEach(async record => { record.Translation = await DBOperations.DBTranslation(record.SystemName); });
                documentAdviceList.ForEach(async record => {
                    record.Branch = branchList.First(a => a.Id == record.BranchId).CompanyName;
                    record.DocumentTypeTranslation = await DBOperations.DBTranslation(record.DocumentType);
                });

                cb_documentType.ItemsSource = documentTypeList;
                DgListView.ItemsSource = documentAdviceList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private async void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach( async e => {
                string headername = e.Header.ToString().ToLower();
                if (headername == "documenttypetranslation") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                else if (headername == "branch") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                else if (headername == "prefix") e.Header = await DBOperations.DBTranslation(headername);
                else if (headername == "number") e.Header = await DBOperations.DBTranslation(headername);
                else if (headername == "startdate") { e.Header = await DBOperations.DBTranslation(headername); (e as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy"; e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "enddate") { e.Header = await DBOperations.DBTranslation(headername); (e as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy"; e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "active") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                else if (headername == "timestamp") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }

                else if (headername == "id") e.DisplayIndex = 0;
                else if (headername == "userid") e.Visibility = Visibility.Hidden;
                else if (headername == "branchid") e.Visibility = Visibility.Hidden;
                else if (headername == "documenttype") e.Visibility = Visibility.Hidden;
            });
        }

        //change filter fields
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SystemDocumentAdviceList report = e as SystemDocumentAdviceList;
                    return report.DocumentTypeTranslation.ToLower().Contains(filter.ToLower())
                    || report.Branch.ToLower().Contains(filter.ToLower())
                    || report.Prefix.ToLower().Contains(filter.ToLower())
                    || report.Number.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void NewRecord() {
            selectedRecord = new SystemDocumentAdviceList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SystemDocumentAdviceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SystemDocumentAdviceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterSystemDocumentAdviceList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SystemDocumentAdviceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (SystemDocumentAdviceList)DgListView.SelectedItem;
            }
            else { selectedRecord = new SystemDocumentAdviceList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.BranchId = ((BusinessBranchList)cb_branch.SelectedItem).Id;
                selectedRecord.DocumentType = ((SystemDocumentTypeList)cb_documentType.SelectedItem).SystemName;
                selectedRecord.Prefix = txt_prefix.Text;
                selectedRecord.Number = txt_number.Text;
                selectedRecord.StartDate = (DateTime)dp_startDate.Value;
                selectedRecord.EndDate = (DateTime)dp_endDate.Value;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                selectedRecord.Branch = null;
                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSystemDocumentAdviceList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSystemDocumentAdviceList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SystemDocumentAdviceList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SystemDocumentAdviceList)DgListView.SelectedItem : new SystemDocumentAdviceList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private void SetRecord(bool? showForm = null, bool copy = false) {
            try { 
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                cb_branch.Text = selectedRecord.Branch;
                cb_documentType.Text = selectedRecord.DocumentTypeTranslation;
                txt_prefix.Text = selectedRecord.Prefix;
                txt_number.Text = selectedRecord.Number;
                dp_startDate.Value = (selectedRecord.Id == 0) ? (DateTime)dp_startDate.Value : selectedRecord.StartDate;
                dp_endDate.Value = (selectedRecord.Id == 0) ? (DateTime)dp_endDate.Value : selectedRecord.EndDate;
                chb_active.IsChecked = selectedRecord.Active;

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
    }
}