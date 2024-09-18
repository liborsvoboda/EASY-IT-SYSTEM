using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using SimpleBrowser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyITSystemCenter.Pages {

    public partial class BasicCurrencyListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static BasicCurrencyList selectedRecord = new BasicCurrencyList();

        public BusinessExtendedExchangeRateList selectedSubRecord = new BusinessExtendedExchangeRateList();

        public BasicCurrencyListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            _ = FormOperations.TranslateFormFields(ListForm);

            _ = LoadDataList();
            SetRecord(false);
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try { if (MainWindow.serviceRunning) DgListView.ItemsSource = await CommunicationManager.GetApiRequest<List<BasicCurrencyList>>(ApiUrls.EasyITCenterBasicCurrencyList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token); } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private async void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                string headername = e.Header.ToString().ToLower();
                if (headername == "Value".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                else if (headername == "ExchangeRate".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); }
                else if (headername == "Fixed".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); }
                else if (headername == "Description".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); }
                else if (headername == "Default".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = DgListView.Columns.Count - 3; }
                else if (headername == "Active".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }

                else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
            });
        }


        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    DataRowView search = e as DataRowView;
                    return search.ToJson().ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new BasicCurrencyList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (BasicCurrencyList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (BasicCurrencyList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBasicCurrencyList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (BasicCurrencyList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (BasicCurrencyList)DgListView.SelectedItem;
            }
            else { selectedRecord = new BasicCurrencyList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.Name = txt_name.Text;
                selectedRecord.ExchangeRate = (decimal)txt_exchangeRate.Value;
                selectedRecord.Fixed = (bool)chb_fixed.IsChecked;
                selectedRecord.Description = tb_description.Text;
                selectedRecord.Default = (bool)chb_default.IsChecked;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBasicCurrencyList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterBasicCurrencyList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new BasicCurrencyList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (BasicCurrencyList)DgListView.SelectedItem : new BasicCurrencyList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;
            txt_name.Text = selectedRecord.Name;
            txt_exchangeRate.Value = (double)selectedRecord.ExchangeRate;
            chb_fixed.IsChecked = selectedRecord.Fixed;
            tb_description.Text = selectedRecord.Description;
            chb_default.IsChecked = selectedRecord.Default;
            chb_active.IsChecked = (selectedRecord.Id == 0) ? bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value) : selectedRecord.Active;

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
                if (!selectedRecord.Fixed) { SubListView.Visibility = Visibility.Visible; LoadSubDataList(); } else { SubListView.Visibility = Visibility.Hidden; }
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        //change subdatasource
        public async void LoadSubDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<BusinessExchangeRateList> exchangeRateList = new List<BusinessExchangeRateList>();
            List<BusinessExtendedExchangeRateList> BusinessExtendedExchangeRateList = new List<BusinessExtendedExchangeRateList>();
            try {
                if (MainWindow.serviceRunning) exchangeRateList = await CommunicationManager.GetApiRequest<List<BusinessExchangeRateList>>(ApiUrls.EasyITCenterBusinessExchangeRateList, null, App.UserData.Authentification.Token);
                exchangeRateList.Where(a => a.CurrencyId == selectedRecord.Id).ToList().ForEach(record => {
                    BusinessExtendedExchangeRateList item = new BusinessExtendedExchangeRateList() {
                        Id = record.Id,
                        CurrencyId = record.CurrencyId,
                        Value = record.Value,
                        ValidFrom = record.ValidFrom,
                        ValidTo = record.ValidTo,
                        Description = record.Description,
                        UserId = record.UserId,
                        Active = record.Active,
                        TimeStamp = record.TimeStamp,
                        Currency = selectedRecord.Name
                    };
                    BusinessExtendedExchangeRateList.Add(item);
                });
                DgSubListView.ItemsSource = BusinessExtendedExchangeRateList;
                DgSubListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
        }

        private void DgSubListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(e => {
                string headername = e.Header.ToString();
                if (headername == "Currency") e.Header = Resources["currency"].ToString();
                else if (headername == "Value") e.Header = Resources["value"].ToString();
                else if (headername == "ValidFrom") e.Header = Resources["validFrom"].ToString();
                else if (headername == "ValidTo") e.Header = Resources["validTo"].ToString();
                else if (headername == "Description") e.Header = Resources["description"].ToString();
                else if (headername == "Active") { e.Header = Resources["active"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                else if (headername == "Id") e.DisplayIndex = 0;
                else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                else if (headername == "CurrencyId") e.Visibility = Visibility.Hidden;
            });
        }

        private void Fixed_Checked(object sender, RoutedEventArgs e) => SubListView.Visibility = Visibility.Hidden;

        private void Fixed_UnChecked(object sender, RoutedEventArgs e) {
            SubListView.Visibility = Visibility.Visible; LoadSubDataList();
        }
    }
}