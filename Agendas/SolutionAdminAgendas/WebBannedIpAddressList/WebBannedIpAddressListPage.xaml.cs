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

namespace EasyITSystemCenter.Pages {

    public partial class WebBannedIpAddressListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static WebBannedIpAddressList selectedRecord = new WebBannedIpAddressList();

        private List<string> webVisitIpList = new List<string>();
        private string IpAddress = null;
        private string LastIpAddressCorrectSearch = ""; private bool messageShown = false;

        public WebBannedIpAddressListPage() {
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
                DgListView.ItemsSource = await CommunicationManager.GetApiRequest<List<WebBannedIpAddressList>>(ApiUrls.EasyITCenterWebBannedIpAddressList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                webVisitIpList = await CommunicationManager.GetApiRequest<List<string>>(ApiUrls.EasyITCenterWebVisitIpList, "Distinct", App.UserData.Authentification.Token);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(e => {
                    string headername = e.Header.ToString();
                    if (headername == "IpAddress") e.Header = Resources["ipAddress"].ToString();
                    else if (headername == "Description") e.Header = Resources["description"].ToString();
                    else if (headername == "Active") { e.Header = Resources["active"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                    else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    WebBannedIpAddressList user = e as WebBannedIpAddressList;
                    return user.IpAddress.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new WebBannedIpAddressList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (WebBannedIpAddressList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (WebBannedIpAddressList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterWebBannedIpAddressList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (WebBannedIpAddressList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (WebBannedIpAddressList)DgListView.SelectedItem; }
            else { selectedRecord = new WebBannedIpAddressList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.IpAddress = txt_ipAddress.Text;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterWebBannedIpAddressList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterWebBannedIpAddressList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new WebBannedIpAddressList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (WebBannedIpAddressList)DgListView.SelectedItem : new WebBannedIpAddressList();
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;
            txt_ipAddress.Text = selectedRecord.IpAddress;
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

        //Selection

        private void SelectGotFocus(object sender, RoutedEventArgs e) => UpdateIpAddressSearchResults();

        private async void UpdateIpAddressSearchResults() {
            try {
                lv_ipAddressSearchResults.Visibility = Visibility.Visible;
                List<string> tempIpListList = webVisitIpList.Where(a => a.Contains(!string.IsNullOrWhiteSpace(txt_ipAddress.Text) ? txt_ipAddress.Text.ToLower() : "")).ToList();
                if (tempIpListList.Count() == 0 && !messageShown) {
                    messageShown = true;
                    var result = await MainWindow.ShowMessageOnMainWindow(false, Resources["ipAddressNotExist"].ToString());
                    if (result == MessageDialogResult.Affirmative) { messageShown = false; }
                    txt_ipAddress.Text = LastIpAddressCorrectSearch; txt_ipAddress.CaretIndex = txt_ipAddress.Text.Length;
                }
                else {
                    lv_ipAddressSearchResults.ItemsSource = tempIpListList;
                    if (lv_ipAddressSearchResults.Items.Count == 1) {
                        lv_ipAddressSearchResults.SelectedItem = lv_ipAddressSearchResults.Items[0];
                        SetIpAddress();
                    }
                    else { lv_ipAddressSearchResults.Visibility = Visibility.Visible; }
                    LastIpAddressCorrectSearch = txt_ipAddress.Text;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void IpAddress_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up && lv_ipAddressSearchResults.Visibility == Visibility.Visible) { lv_ipAddressSearchResults.Focus(); }
            if (e.Key == Key.Down && lv_ipAddressSearchResults.Visibility == Visibility.Visible) { lv_ipAddressSearchResults.Focus(); }
            if (e.Key != Key.Down && e.Key != Key.Up && e.Key != Key.Enter && lv_ipAddressSearchResults.Visibility == Visibility.Visible) { txt_ipAddress.Focus(); }
        }

        private void SelectIpAddress_Enter(object sender, KeyEventArgs e) {
            if ((e.Key == Key.Enter) && lv_ipAddressSearchResults.Visibility == Visibility.Visible) { SetIpAddress(); }
        }

        private void MouseSelectIpAddress_Click(object sender, MouseButtonEventArgs e) => SetIpAddress();

        private void SetIpAddress() {
            if (lv_ipAddressSearchResults.SelectedIndex > -1) {
                txt_ipAddress.Text = (string)lv_ipAddressSearchResults.SelectedItem;
                lbl_description.Focus();
            }
            lv_ipAddressSearchResults.Visibility = Visibility.Hidden;
        }
    }
}