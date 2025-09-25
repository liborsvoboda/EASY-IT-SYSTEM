using EasyITSystemCenter.Api;
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
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyITSystemCenter.Pages {

    public partial class SystemParameterListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SystemParameterList selectedRecord = new SystemParameterList();

        private List<SolutionMixedEnumList> inheritedDataType = new List<SolutionMixedEnumList>();
        private List<SystemParameterList> parameterList = new List<SystemParameterList>();

        public SystemParameterListPage() {
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
            List<SolutionUserList> userList = new List<SolutionUserList>();
            MainWindow.ProgressRing = Visibility.Visible;
            try
            {

                inheritedDataType = await DBOperations.LoadInheritedDataList("DataType");
                parameterList = await CommunicationManager.GetApiRequest<List<SystemParameterList>>(ApiUrls.EasyITCenterUserParameterList, App.UserData.Authentification.Id.ToString(), App.UserData.Authentification.Token);
                userList = await CommunicationManager.GetApiRequest<List<SolutionUserList>>(ApiUrls.EasyITCenterSolutionUserList, null, App.UserData.Authentification.Token);

                parameterList.ForEach(async param => {
                    param.Translation = await DBOperations.DBTranslation(param.SystemName);
                    param.User = userList.Where(a => a.Id == param.UserId).Select(a => a.Username).FirstOrDefault();
                });

                cb_type.ItemsSource = inheritedDataType.OrderBy(a=>a.Name);
                DgListView.ItemsSource = parameterList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private async void DgListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                string headername = e.Header.ToString().ToLower();
                if (headername == "SystemName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                else if (headername == "Translation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                else if (headername == "Value".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "Type".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "Description".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "User".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
            });
        }

        //change filter fields
        public void Filter(string filter) {
            try
            {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    DataRowView search = e as DataRowView;
                    return search.ObjectToJson().ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }


        public void NewRecord() {
            selectedRecord = new SystemParameterList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SystemParameterList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SystemParameterList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterUserParameterList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SystemParameterList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (SystemParameterList)DgListView.SelectedItem;
            }
            else { selectedRecord = new SystemParameterList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.SystemName = txt_systemName.Text;
                selectedRecord.Value = txt_value.Text;
                selectedRecord.InheritedDataType = ((SolutionMixedEnumList)cb_type.SelectedItem).Name;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                selectedRecord.User = null;
                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterUserParameterList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterUserParameterList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    // Refresh User Params
                    App.UserParameterList = await CommunicationManager.GetApiRequest<List<SystemParameterList>>(ApiUrls.EasyITCenterUserParameterList, null, null);

                    selectedRecord = new SystemParameterList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SystemParameterList)DgListView.SelectedItem : new SystemParameterList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private void SetRecord(bool? showForm = null, bool copy = false) {
            try {
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                desc_translation.Content = selectedRecord.Translation;
                txt_systemName.Text = selectedRecord.SystemName;
                txt_value.Text = selectedRecord.Value;
                cb_type.Text = selectedRecord.InheritedDataType;
                txt_description.Text = selectedRecord.Description;

                //Only for Admin: Owner/UserId Selection Allow Editing Server Default Values
                if (App.UserData.Authentification.Role == "Admin" || selectedRecord.UserId == App.UserData.Authentification.Id)
                    btn_save.IsEnabled = true;
                else btn_save.IsEnabled = false;

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

        private void Value_TextChanged(object sender, TextChangedEventArgs e) => btn_save.IsEnabled = false;
        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e) => btn_save.IsEnabled = false;
        private void Check_Click(object sender, RoutedEventArgs e) {
            try {
                btn_save.IsEnabled = false;
                var test = ProgrammaticOperations.ConvertStringTypeValueToSpecTypeByKnownTypeName((string)cb_type.SelectedValue, txt_value.Text);
                btn_save.IsEnabled = test.Item2 && (App.UserData.Authentification.Role == "Admin" || selectedRecord.UserId == App.UserData.Authentification.Id);
                lbl_paramCheckResult.Content = (string)test.Item1;
            } catch { }
        }
    }
}