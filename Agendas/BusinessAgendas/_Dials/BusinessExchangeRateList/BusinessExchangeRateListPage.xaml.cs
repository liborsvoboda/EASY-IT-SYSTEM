using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
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

// This is Template ListView + UserForm
namespace EasyITSystemCenter.Pages {

    public partial class BusinessExchangeRateListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static BusinessExtendedExchangeRateList selectedRecord = new BusinessExtendedExchangeRateList();

        private List<BasicCurrencyList> CurrencyList = new List<BasicCurrencyList>();

        public BusinessExchangeRateListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            //translate fields in detail form
            try {
                _ = FormOperations.TranslateFormFields(ListForm);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<BusinessExchangeRateList> ExchangeRateList = new List<BusinessExchangeRateList>();
            List<BusinessExtendedExchangeRateList> BusinessExtendedExchangeRateList = new List<BusinessExtendedExchangeRateList>();
            try {
                CurrencyList = await CommunicationManager.GetApiRequest<List<BasicCurrencyList>>(ApiUrls.EasyITCenterBasicCurrencyList, null, App.UserData.Authentification.Token);
                ExchangeRateList = await CommunicationManager.GetApiRequest<List<BusinessExchangeRateList>>(ApiUrls.EasyITCenterBusinessExchangeRateList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                ExchangeRateList.ForEach(record => {
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
                        Currency = CurrencyList.Where(a => a.Id == record.CurrencyId).First().Name
                    };
                    BusinessExtendedExchangeRateList.Add(item);
                });
                cb_currency.ItemsSource = CurrencyList.Where(a => a.Fixed == false).ToList();
                DgListView.ItemsSource = BusinessExtendedExchangeRateList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(e => {
                    string headername = e.Header.ToString();
                    if (headername == "Currency") e.Header = Resources["currency"].ToString();
                    else if (headername == "ValidFrom") { e.Header = Resources["validFrom"].ToString(); (e as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy"; }
                    else if (headername == "ValidTo") { e.Header = Resources["validTo"].ToString(); (e as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy"; }
                    else if (headername == "Value") e.Header = Resources["value"].ToString();
                    else if (headername == "Description") e.Header = Resources["description"].ToString();
                    else if (headername == "Active") { e.Header = Resources["active"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                    else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                    else if (headername == "CurrencyId") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        //change filter fields
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    BusinessExtendedExchangeRateList user = e as BusinessExtendedExchangeRateList;
                    return user.Currency.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new BusinessExtendedExchangeRateList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (BusinessExtendedExchangeRateList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (BusinessExtendedExchangeRateList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBusinessExchangeRateList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (BusinessExtendedExchangeRateList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (BusinessExtendedExchangeRateList)DgListView.SelectedItem;
            }
            else { selectedRecord = new BusinessExtendedExchangeRateList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.CurrencyId = ((BasicCurrencyList)cb_currency.SelectedItem).Id;
                selectedRecord.Value = (decimal)txt_value.Value;
                selectedRecord.ValidFrom = dp_validFrom.Value;
                selectedRecord.ValidTo = dp_validTo.Value;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                selectedRecord.Currency = null;
                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBusinessExchangeRateList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterBusinessExchangeRateList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new BusinessExtendedExchangeRateList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (BusinessExtendedExchangeRateList)DgListView.SelectedItem : new BusinessExtendedExchangeRateList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private void SetRecord(bool? showForm = null, bool copy = false) {
            SetSubListsNonActiveOnNewItem(selectedRecord.Id == 0);
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;
            cb_currency.Text = selectedRecord.Currency;
            txt_value.Value = (double)selectedRecord.Value;
            dp_validFrom.Value = selectedRecord.ValidFrom;
            dp_validTo.Value = selectedRecord.ValidTo;
            txt_description.Text = selectedRecord.Description;
            chb_active.IsChecked = (selectedRecord.Id == 0) ? bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value) : selectedRecord.Active;

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private void SetSubListsNonActiveOnNewItem(bool newItem) {
            if (newItem) {
                cb_currency.ItemsSource = CurrencyList.Where(a => a.Active).ToList();
            }
            else { cb_currency.ItemsSource = CurrencyList; }
        }
    }
}