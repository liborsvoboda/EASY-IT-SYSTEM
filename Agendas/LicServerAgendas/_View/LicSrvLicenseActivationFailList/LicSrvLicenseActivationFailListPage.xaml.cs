using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyITSystemCenter.Pages {

    public partial class LicSrvLicenseActivationFailListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static LicSrvLicenseActivationFailList selectedRecord = new LicSrvLicenseActivationFailList();

        public LicSrvLicenseActivationFailListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            _ = LoadDataList();
            SetRecord();
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                if (MainWindow.serviceRunning) {
                    DgListView.ItemsSource = await CommunicationManager.GetApiRequest<List<LicSrvLicenseActivationFailList>>(ApiUrls.EasyITCenterLicSrvLicenseActivationFailList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(e => {
                string headername = e.Header.ToString();
                if (headername == "UnlockCode") { e.Header = Resources["unlockCode"].ToString(); }
                else if (headername == "IpAddress") e.Header = Resources["ipAddress"].ToString();
                else if (headername == "PartNumber") e.Header = Resources["partNumber"].ToString();
                else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                else if (headername == "Id") e.DisplayIndex = 0;
                else if (headername == "UserId") e.Visibility = Visibility.Hidden;
            });
        }

        //change filter fields
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    LicSrvLicenseActivationFailList user = e as LicSrvLicenseActivationFailList;
                    if (
                       user.Id.ToString().ToLower().Contains(filter.ToLower())
                    || user.IpAddress.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.UnlockCode) && user.UnlockCode.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.PartNumber) && user.PartNumber.ToLower().Contains(filter.ToLower())
                    ) return true;
                    return false;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (LicSrvLicenseActivationFailList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord();
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (LicSrvLicenseActivationFailList)DgListView.SelectedItem; }
            else { selectedRecord = new LicSrvLicenseActivationFailList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //change dataset prepare for working
        private void SetRecord() {
            MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = false; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
            ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = false;
        }
    }
}