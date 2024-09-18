using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyITSystemCenter.Pages {

    public partial class LicSrvUsedLicenseListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ExtendedLicSrvUsedLicenseList selectedRecord = new ExtendedLicSrvUsedLicenseList();

        public LicSrvUsedLicenseListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            _ = LoadDataList();
            SetRecord();
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<BasicItemList> itemList = new List<BasicItemList>();
            List<BusinessAddressList> addressList = new List<BusinessAddressList>();
            List<LicSrvUsedLicenseList> LicSrvUsedLicenseLists = new List<LicSrvUsedLicenseList>();
            List<ExtendedLicSrvUsedLicenseList> ExtendedLicSrvUsedLicenseLists = new List<ExtendedLicSrvUsedLicenseList>();
            try { if (MainWindow.serviceRunning) addressList = await CommunicationManager.GetApiRequest<List<BusinessAddressList>>(ApiUrls.EasyITCenterBusinessAddressList, null, App.UserData.Authentification.Token); } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            try { if (MainWindow.serviceRunning) itemList = await CommunicationManager.GetApiRequest<List<BasicItemList>>(ApiUrls.EasyITCenterBasicItemList, null, App.UserData.Authentification.Token); } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            try { if (MainWindow.serviceRunning) LicSrvUsedLicenseLists = await CommunicationManager.GetApiRequest<List<LicSrvUsedLicenseList>>(ApiUrls.EasyITCenterLicSrvUsedLicenseList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token); } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            try {
                LicSrvUsedLicenseLists.ForEach(record => {
                    ExtendedLicSrvUsedLicenseList item = new ExtendedLicSrvUsedLicenseList() {
                        Id = record.Id,
                        IpAddress = record.IpAddress,
                        UnlockCode = record.UnlockCode,
                        AlgorithmName = record.AlgorithmName,
                        PartNumber = record.PartNumber,
                        License = record.License,
                        TimeStamp = record.TimeStamp,
                        ItemId = record.ItemId,
                        AddressId = record.AddressId,
                        ItemName = itemList.Where(a => a.Id == record.ItemId).First().Name,
                        CompanyName = addressList.Where(a => a.Id == record.AddressId).First().CompanyName
                    };
                    ExtendedLicSrvUsedLicenseLists.Add(item);
                });

                DgListView.ItemsSource = ExtendedLicSrvUsedLicenseLists;
                DgListView.Items.Refresh();
            } catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + ex.Message); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(e => {
                string headername = e.Header.ToString();
                if (headername == "Id") e.DisplayIndex = 0;
                else if (headername == "Item") e.Header = Resources["item"].ToString();
                else if (headername == "PartNumber") { e.Header = Resources["partNumber"].ToString(); e.DisplayIndex = 1; }
                else if (headername == "CompanyName") e.Header = Resources["companyName"].ToString();
                else if (headername == "ItemName") e.Header = Resources["itemName"].ToString();
                else if (headername == "UnlockCode") e.Header = Resources["unlockCode"].ToString();
                else if (headername == "AlgorithmName") { e.Header = Resources["algorithmName"].ToString(); e.DisplayIndex = 2; }
                else if (headername == "License") e.Header = Resources["license"].ToString();
                else if (headername == "Activated") e.Header = Resources["activated"].ToString();
                else if (headername == "TimeStamp") e.Header = Resources["activateDate"].ToString();
                else if (headername == "IpAddress") e.Header = Resources["ipAddress"].ToString();
                else if (headername == "Active") { e.Header = Resources["active"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                else if (headername == "Id") e.DisplayIndex = 0;
                else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                else if (headername == "ItemId") e.Visibility = Visibility.Hidden;
                else if (headername == "AddressId") e.Visibility = Visibility.Hidden;
            });
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (ExtendedLicSrvUsedLicenseList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord();
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (ExtendedLicSrvUsedLicenseList)DgListView.SelectedItem; }
            else { selectedRecord = new ExtendedLicSrvUsedLicenseList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord();
        }

        //change filter fields
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    ExtendedLicSrvUsedLicenseList LicenseList = e as ExtendedLicSrvUsedLicenseList;
                    return LicenseList.PartNumber.ToLower().Contains(filter.ToLower())
                    || LicenseList.ItemName.ToLower().Contains(filter.ToLower())
                    || LicenseList.License.ToLower().Contains(filter.ToLower())
                    || LicenseList.AlgorithmName.ToLower().Contains(filter.ToLower())
                    || LicenseList.CompanyName.ToLower().Contains(filter.ToLower())
                    || LicenseList.TimeStamp.Value.ToShortDateString().ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(LicenseList.IpAddress) && LicenseList.IpAddress.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(LicenseList.TimeStamp.Value.ToString()) && LicenseList.TimeStamp.Value.ToUniversalTime().ToString().ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        //change dataset prepare for working
        private void SetRecord() {
            MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = false; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
            ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = false;
        }
    }
}