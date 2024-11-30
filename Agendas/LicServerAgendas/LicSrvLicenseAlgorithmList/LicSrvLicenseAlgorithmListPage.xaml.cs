using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using EASYTools.SqlConnectionDialog;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyITSystemCenter.Pages {

    public partial class LicSrvLicenseAlgorithmListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static LicSrvLicenseAlgorithmList selectedRecord = new LicSrvLicenseAlgorithmList();

        private List<BasicItemList> ItemList = new List<BasicItemList>();
        private List<BusinessAddressList> AddressList = new List<BusinessAddressList>();

        private string connectionString = null;

        public LicSrvLicenseAlgorithmListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            //translate fields in detail form
            lbl_id.Content = Resources["id"].ToString();
            lbl_address.Content = Resources["companyName"].ToString();
            lbl_name.Content = Resources["fname"].ToString();
            lbl_algorithm.Content = Resources["algorithm"].ToString();
            lbl_description.Content = Resources["description"].ToString();
            lbl_active.Content = Resources["active"].ToString();
            lbl_validFrom.Content = Resources["validFrom"].ToString();
            lbl_validTo.Content = Resources["validTo"].ToString();
            lbl_item.Content = Resources["item"].ToString();
            lbl_limitActive.Content = Resources["limitActive"].ToString();
            lbl_activationLimit.Content = Resources["activationLimit"].ToString();
            lbl_usedCount.Content = Resources["usedCount"].ToString();

            btn_connect.Content = Resources["connectSql"].ToString();
            btn_execute.Content = Resources["execute"].ToString();
            btn_save.Content = Resources["btn_save"].ToString();
            btn_cancel.Content = Resources["btn_cancel"].ToString();

            _ = LoadDataList();
            SetRecord(false);
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                cb_address.ItemsSource = AddressList = await CommunicationManager.GetApiRequest<List<BusinessAddressList>>(ApiUrls.EasyITCenterBusinessAddressList, null, App.UserData.Authentification.Token);
                cb_item.ItemsSource = ItemList = await CommunicationManager.GetApiRequest<List<BasicItemList>>(ApiUrls.EasyITCenterBasicItemList, null, App.UserData.Authentification.Token);

                List<LicSrvLicenseAlgorithmList> licenseAlgorithmList = new List<LicSrvLicenseAlgorithmList>(); List<ExtendedLicenseAlgorithmList> extendedLicenseAlgorithmList = new List<ExtendedLicenseAlgorithmList>();
                if (MainWindow.serviceRunning) licenseAlgorithmList = await CommunicationManager.GetApiRequest<List<LicSrvLicenseAlgorithmList>>(ApiUrls.EasyITCenterLicSrvLicenseAlgorithmList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                licenseAlgorithmList.ForEach(record => {
                    ExtendedLicenseAlgorithmList item = new ExtendedLicenseAlgorithmList() {
                        Id = record.Id,
                        AddressId = record.AddressId,
                        ItemId = record.ItemId,
                        Name = record.Name,
                        ValidFrom = record.ValidFrom,
                        ValidTo = record.ValidTo,
                        Algorithm = record.Algorithm,
                        Description = record.Description,
                        LimitActive = record.LimitActive,
                        ActivationLimit = record.ActivationLimit,
                        UsedCount = record.UsedCount,
                        Active = record.Active,
                        UserId = record.UserId,
                        TimeStamp = record.TimeStamp,
                        PartNumber = ItemList.Where(a => a.Id == record.ItemId).Select(a => a.PartNumber).FirstOrDefault().ToString(),
                        ItemName = ItemList.Where(a => a.Id == record.ItemId).Select(a => a.Name).FirstOrDefault().ToString(),
                        CompanyName = AddressList.Where(a => a.Id == record.AddressId).Select(a => a.CompanyName).FirstOrDefault().ToString()
                    };
                    extendedLicenseAlgorithmList.Add(item);
                });

                DgListView.ItemsSource = extendedLicenseAlgorithmList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(e => {
                string headername = e.Header.ToString();
                if (headername == "Value") { e.Header = Resources["fname"].ToString(); e.DisplayIndex = 1; }
                else if (headername == "ValidFrom") { e.Header = Resources["validFrom"].ToString(); (e as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy"; }
                else if (headername == "ValidTo") { e.Header = Resources["validTo"].ToString(); (e as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy"; }
                else if (headername == "Algorithm") { e.Header = Resources["algorithm"].ToString(); e.Visibility = Visibility.Hidden; }
                else if (headername == "Description") e.Header = Resources["description"].ToString();
                else if (headername == "LimitActive") e.Header = Resources["limitActive"].ToString();
                else if (headername == "ActivationLimit") e.Header = Resources["activationLimit"].ToString();
                else if (headername == "UsedCount") { e.Header = Resources["usedCount"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "PartNumber") { e.Header = Resources["partNumber"].ToString(); e.DisplayIndex = 2; }
                else if (headername == "ItemName") { e.Header = Resources["itemName"].ToString(); e.DisplayIndex = 3; }
                else if (headername == "CompanyName") { e.Header = Resources["companyName"].ToString(); e.DisplayIndex = 4; }
                else if (headername == "Active") { e.Header = Resources["active"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 5; }
                else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                else if (headername == "Id") e.DisplayIndex = 0;
                else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                else if (headername == "Algorithm") { e.Visibility = Visibility.Hidden; }
                else if (headername == "ItemId") e.Visibility = Visibility.Hidden;
                else if (headername == "AddressId") e.Visibility = Visibility.Hidden;
            });
        }

        //change filter fields
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    LicSrvLicenseAlgorithmList user = e as LicSrvLicenseAlgorithmList;
                    return user.Name.ToLower().Contains(filter.ToLower())
                    || user.Algorithm.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new LicSrvLicenseAlgorithmList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (LicSrvLicenseAlgorithmList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (LicSrvLicenseAlgorithmList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterLicSrvLicenseAlgorithmList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (LicSrvLicenseAlgorithmList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (LicSrvLicenseAlgorithmList)DgListView.SelectedItem;
            }
            else { selectedRecord = new LicSrvLicenseAlgorithmList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.AddressId = ((BusinessAddressList)cb_address.SelectedItem).Id;
                selectedRecord.ItemId = ((BasicItemList)cb_item.SelectedItem).Id;
                selectedRecord.Name = txt_name.Text;
                selectedRecord.ValidFrom = dp_validFrom.Value;
                selectedRecord.ValidTo = dp_validTo.Value;
                selectedRecord.Algorithm = txt_algorithm.Text;
                selectedRecord.Description = tb_description.Text;
                selectedRecord.LimitActive = (bool)chb_limitActive.IsChecked;
                selectedRecord.ActivationLimit = (int?)txt_activationLimit.Value;
                selectedRecord.UsedCount = (int)txt_usedCount.Value;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterLicSrvLicenseAlgorithmList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterLicSrvLicenseAlgorithmList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new LicSrvLicenseAlgorithmList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (LicSrvLicenseAlgorithmList)DgListView.SelectedItem : new LicSrvLicenseAlgorithmList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private void SetRecord(bool? showForm = null, bool copy = false) {
            SetSubListsNonActiveOnNewItem(selectedRecord.Id == 0);
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;
            cb_address.Text = AddressList.Where(a => a.Id == selectedRecord.AddressId).Select(a => a.CompanyName).FirstOrDefault();
            txt_name.Text = selectedRecord.Name;
            dp_validFrom.Value = selectedRecord.ValidFrom;
            dp_validTo.Value = selectedRecord.ValidTo;
            txt_algorithm.Text = selectedRecord.Algorithm;
            tb_description.Text = selectedRecord.Description;
            chb_limitActive.IsChecked = selectedRecord.LimitActive;
            txt_activationLimit.Value = selectedRecord.ActivationLimit;
            txt_usedCount.Value = selectedRecord.UsedCount;
            chb_active.IsChecked = (selectedRecord.Id == 0) ? bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value) : selectedRecord.Active;
            cb_item.Text = ItemList.Where(a => a.Id == selectedRecord.ItemId).Select(a => a.Name).FirstOrDefault();

            lbl_resultSQL.Content = null;
            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e) {
            var factory = new ConnectionStringFactory();
            connectionString = factory.BuildConnectionString();

            if (!string.IsNullOrWhiteSpace(connectionString)) {
                btn_execute.IsEnabled = true;
            }
        }

        private void Connection_StateChange(object sender, StateChangeEventArgs e) {
            if (e.CurrentState == ConnectionState.Open) {
                btn_connect.Content = Resources["disconnect"].ToString(); btn_execute.IsEnabled = true;
            }
            else { btn_connect.Content = Resources["connectSql"].ToString(); }
        }

        private async void Execute_Click(object sender, RoutedEventArgs e) {
            try {
                SqlCommand cmd;
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    cmd = new SqlCommand(txt_algorithm.Text, connection);
                    lbl_resultSQL.Content = cmd.ExecuteScalar().ToString();
                }
            } catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, ex.Message + Environment.NewLine + ex.StackTrace); }
        }

        private void LimitActive_Checked(object sender, RoutedEventArgs e) => txt_activationLimit.IsEnabled = true;

        private void LimitActive_Unchecked(object sender, RoutedEventArgs e) {
            txt_activationLimit.Value = null; txt_activationLimit.IsEnabled = false;
        }

        private void CopyToClipClick(object sender, MouseButtonEventArgs e) {
            if (string.IsNullOrWhiteSpace(lbl_resultSQL.Content.ToString())) { Clipboard.SetDataObject(lbl_resultSQL.Content.ToString()); }
        }

        private void SetSubListsNonActiveOnNewItem(bool newItem) {
            if (newItem) {
                cb_address.ItemsSource = AddressList.Where(a => a.Active).ToList();
                cb_item.ItemsSource = ItemList.Where(a => a.Active).ToList();
            }
            else { cb_address.ItemsSource = AddressList; cb_item.ItemsSource = ItemList; }
        }
    }
}