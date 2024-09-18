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

    public partial class ServerStaticOrMvcDefPathListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ServerStaticOrMvcDefPathList selectedRecord = new ServerStaticOrMvcDefPathList();

        private List<ServerStaticOrMvcDefPathList> ServerStaticOrMvcDefPathLists = new List<ServerStaticOrMvcDefPathList>();

        public ServerStaticOrMvcDefPathListPage() {
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
                ServerStaticOrMvcDefPathLists = await CommunicationManager.GetApiRequest<List<ServerStaticOrMvcDefPathList>>(ApiUrls.EasyITCenterServerStaticOrMvcDefPathList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                DgListView.ItemsSource = ServerStaticOrMvcDefPathLists;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private async void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                string headername = e.Header.ToString().ToLower();
                if (headername == "SystemName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                else if (headername == "WebRootPath".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                else if (headername == "AliasPath".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                else if (headername == "IsBrowsable".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                else if (headername == "IsStaticOrMvcDefOnly".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }

                else if (headername == "Active".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 6; }
                else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }

                else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                else if (headername == "Description".ToLower()) e.Visibility = Visibility.Hidden;
                else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
            });
        }

        //change filter fields
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    ServerStaticOrMvcDefPathList search = e as ServerStaticOrMvcDefPathList;
                    return search.SystemName.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.WebRootSubPath) && search.WebRootSubPath.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.Description) && search.Description.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.AliasPath) && search.AliasPath.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new ServerStaticOrMvcDefPathList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (ServerStaticOrMvcDefPathList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (ServerStaticOrMvcDefPathList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterServerStaticOrMvcDefPathList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
            SetRecord(false);
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (ServerStaticOrMvcDefPathList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (ServerStaticOrMvcDefPathList)DgListView.SelectedItem;
            }
            else { selectedRecord = new ServerStaticOrMvcDefPathList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.SystemName = txt_systemName.Text;
                selectedRecord.WebRootSubPath = txt_webRootSubPath.Text;

                selectedRecord.Description = txt_description.Text;
                selectedRecord.IsBrowsable = (bool)chb_isBrowsable.IsChecked;
                selectedRecord.IsStaticOrMvcDefOnly = (bool)chb_isStaticOrMvcDefOnly.IsChecked;

                selectedRecord.AliasPath = txt_aliasPath.Text;
                selectedRecord.Active = (bool)chb_active.IsChecked;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterServerStaticOrMvcDefPathList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterServerStaticOrMvcDefPathList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new ServerStaticOrMvcDefPathList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (ServerStaticOrMvcDefPathList)DgListView.SelectedItem : new ServerStaticOrMvcDefPathList();
            SetRecord(false);
        }


        private void SetRecord(bool? showForm = null, bool copy = false) {
            if (showForm != null && showForm == true) {
                try { 
                    txt_id.Value = (copy) ? 0 : selectedRecord.Id;
                    txt_systemName.Text = selectedRecord.SystemName;
                    txt_webRootSubPath.Text = selectedRecord.WebRootSubPath;

                    txt_description.Text = selectedRecord.Description;
                    chb_isBrowsable.IsChecked = selectedRecord.IsBrowsable;
                    chb_isStaticOrMvcDefOnly.IsChecked = selectedRecord.IsStaticOrMvcDefOnly;

                    txt_aliasPath.Text = selectedRecord.AliasPath;
                    chb_active.IsChecked = (selectedRecord.Id == 0) ? bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value) : selectedRecord.Active;

                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }


                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }


        private void enableAliasClick(object sender, RoutedEventArgs e) {
            if (dataViewSupport.FormShown) {
                txt_aliasPath.IsEnabled = (bool)chb_enableAlias.IsChecked;
            }
        }
    }
}