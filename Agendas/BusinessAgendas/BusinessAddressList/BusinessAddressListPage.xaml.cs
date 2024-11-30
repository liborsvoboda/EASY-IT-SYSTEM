using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static XamlMath.Rendering.Transformations.Transformation;

namespace EasyITSystemCenter.Pages {

    public partial class BusinessAddressListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static BusinessAddressList selectedRecord = new BusinessAddressList();

        private List<SolutionMixedEnumList> inheritedAddressTypesList = new List<SolutionMixedEnumList>();


        public BusinessAddressListPage() {
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
            try {

                DgListView.ItemsSource = await CommunicationManager.GetApiRequest<List<BusinessAddressList>>(ApiUrls.EasyITCenterBusinessAddressList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                inheritedAddressTypesList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/AddressTypes", App.UserData.Authentification.Token);
                inheritedAddressTypesList.ForEach(async item => item.Translation = await DBOperations.DBTranslation(item.Name));

                cb_addressType.ItemsSource = inheritedAddressTypesList;

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }


            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(e => {
                string headername = e.Header.ToString();
                if (headername == "CompanyName") e.Header = Resources["companyName"].ToString();
                else if (headername == "ContactName") e.Header = Resources["contactName"].ToString();
                else if (headername == "Street") e.Header = Resources["street"].ToString();
                else if (headername == "City") e.Header = Resources["city"].ToString();
                else if (headername == "PostCode") e.Header = Resources["postCode"].ToString();
                else if (headername == "Phone") e.Header = Resources["phone"].ToString();
                else if (headername == "Email") e.Header = Resources["email"].ToString();
                else if (headername == "BankAccount") e.Header = Resources["bankAccount"].ToString();
                else if (headername == "Ico") e.Header = Resources["ico"].ToString();
                else if (headername == "Dic") e.Header = Resources["dic"].ToString();
                else if (headername == "Default") { e.Header = Resources["defaultAddress"].ToString(); e.DisplayIndex = DgListView.Columns.Count - 6; }
                else if (headername == "UnlockCode") { e.Header = Resources["unlockCode"].ToString(); e.DisplayIndex = DgListView.Columns.Count - 5; }
                else if (headername == "UnlockActivationHit") { e.Header = Resources["unlockActivationHit"].ToString(); e.DisplayIndex = DgListView.Columns.Count - 4; }
                else if (headername == "Active") { e.Header = Resources["active"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }

                else if (headername == "Id") e.DisplayIndex = 0;
                else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                else if (headername == "AddressType") e.Visibility = Visibility.Hidden;
                else if (headername == "LicenseId") e.DisplayIndex = 0;
            });
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    DataRowView search = e as DataRowView;
                    return search.ObjectToJson().ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new BusinessAddressList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (BusinessAddressList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (BusinessAddressList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterBusinessAddressList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (BusinessAddressList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (BusinessAddressList)DgListView.SelectedItem;
            }
            else { selectedRecord = new BusinessAddressList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.CompanyName = txt_companyName.Text;
                selectedRecord.ContactName = txt_contactName.Text;
                selectedRecord.Street = txt_street.Text;
                selectedRecord.City = txt_city.Text;
                selectedRecord.PostCode = txt_postCode.Text;
                selectedRecord.Phone = txt_phone.Text;
                selectedRecord.Email = txt_email.Text;
                selectedRecord.BankAccount = txt_bankAccount.Text;
                selectedRecord.AddressType = ((SolutionMixedEnumList)cb_addressType.SelectedItem).Name;
                selectedRecord.Ico = txt_ico.Text;
                selectedRecord.Dic = txt_dic.Text;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterBusinessAddressList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterBusinessAddressList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new BusinessAddressList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (BusinessAddressList)DgListView.SelectedItem : new BusinessAddressList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private void SetRecord(bool? showForm = null, bool copy = false) {
            try {
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;
                txt_companyName.Text = selectedRecord.CompanyName;
                txt_contactName.Text = selectedRecord.ContactName;
                txt_street.Text = selectedRecord.Street;
                txt_city.Text = selectedRecord.City;
                txt_postCode.Text = selectedRecord.PostCode;
                txt_phone.Text = selectedRecord.Phone;
                txt_email.Text = selectedRecord.Email;
                txt_bankAccount.Text = selectedRecord.BankAccount;
                cb_addressType.SelectedValue = (selectedRecord.AddressType != null) ? inheritedAddressTypesList.FirstOrDefault(a => a.Name == selectedRecord.AddressType).Name : inheritedAddressTypesList.FirstOrDefault().Name;
                txt_ico.Text = selectedRecord.Ico;
                txt_dic.Text = selectedRecord.Dic;

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