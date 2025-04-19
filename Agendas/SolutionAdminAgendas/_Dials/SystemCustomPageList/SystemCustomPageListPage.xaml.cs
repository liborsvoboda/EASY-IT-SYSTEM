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

    public partial class SystemCustomPageListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SystemCustomPageList selectedRecord = new SystemCustomPageList();


        private List<SystemCustomPageList> systemCustomPageList = new List<SystemCustomPageList>();
        private List<SolutionMixedEnumList> inheritedFormType = new List<SolutionMixedEnumList>();
        private List<SolutionMixedEnumList> inheritedSystemApiCallType = new List<SolutionMixedEnumList>();
        private List<SystemTranslatedTableList> systemTranslatedTableList = new List<SystemTranslatedTableList>();
        private List<GenericDataList> systemTableList = new List<GenericDataList>();
        private List<GenericDataList> tableSchema = new List<GenericDataList>();

        public SystemCustomPageListPage() {
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
                inheritedSystemApiCallType = await DBOperations.LoadInheritedDataList("SystemApiCallType");
                inheritedSystemApiCallType.ForEach(async apiCallType => { apiCallType.Translation = await DBOperations.DBTranslation(apiCallType.Name); });

                systemTableList = await CommunicationManager.GetApiRequest<List<GenericDataList>>(ApiUrls.ServerApi, "DatabaseServices/SpGetTableList", App.UserData.Authentification.Token);
                inheritedFormType = await DBOperations.LoadInheritedDataList("FormType");
                inheritedFormType.ForEach(async agendaType => { agendaType.Translation = await DBOperations.DBTranslation(agendaType.Name); });

                systemCustomPageList = await CommunicationManager.GetApiRequest<List<SystemCustomPageList>>(ApiUrls.EasyITCenterSystemCustomPageList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);


                systemTableList.ForEach(async table => {
                    systemTranslatedTableList.Add(new SystemTranslatedTableList() { TableName = table.Data, Translate = await DBOperations.DBTranslation(table.Data) });
                });

                cb_InheritedFormType.ItemsSource = inheritedFormType;
                cb_DbtableName.ItemsSource = systemTranslatedTableList.OrderBy(a => a.Translate).ToList();
                cb_InheritedSystemApiCallType.ItemsSource = inheritedSystemApiCallType.OrderBy(a => a.Name).ToList();
                DgListView.ItemsSource = systemCustomPageList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "PageName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "InheritedFormType".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "IsInteractAgenda".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "IsSystemUrl".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                    else if (headername == "IsServerUrl".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }
                    else if (headername == "IsWebServer".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 6; }
                    else if (headername == "StartupUrl".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 7; }
                    else if (headername == "StartupSubFolder".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 8; }
                    else if (headername == "StartupCommand".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 9; }
                    else if (headername == "DevModeEnabled".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 10; }
                    else if (headername == "ShowHelpTab".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 11; }
                    else if (headername == "InheritedHelpTabSourceType".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 12; }
                    else if (headername == "HelpTabUrl".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 13; }
                    else if (headername == "DbtableName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 14; }
                    else if (headername == "ColumnName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 15; }
                    else if (headername == "SetWebDataJscriptCmd".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 16; }
                    else if (headername == "GetWebDataJscriptCmd".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 17; }
                    else if (headername == "UseIIOverDom".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 18; }
                    else if (headername == "DomhtmlElementName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 19; }
                    else if (headername == "InheritedSetName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 20; }

                    else if (headername == "Description".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 8; }
                    else if (headername == "Active".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }

                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;

                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }



        public void Filter(string filter) {
            try {
                bool status = false;
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SystemCustomPageList search = e as SystemCustomPageList;
                    return search.ObjectToJson().ToLower().Contains(filter.ToLower());
                }; } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }



        public void NewRecord() {
            selectedRecord = new SystemCustomPageList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SystemCustomPageList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SystemCustomPageList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterSystemCustomPageList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SystemCustomPageList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }


        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SystemCustomPageList)DgListView.SelectedItem; }
            else { selectedRecord = new SystemCustomPageList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }


        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.InheritedFormType = ((SolutionMixedEnumList)cb_InheritedFormType.SelectedItem).Name;

                selectedRecord.PageName = txt_pageName.Text;
                selectedRecord.Description = txt_description.Text;

                selectedRecord.IsInteractAgenda = (bool)chb_IsInteractAgenda.IsChecked;
                selectedRecord.IsSystemUrl = (bool)chb_IsSystemUrl.IsChecked;
                selectedRecord.IsServerUrl = (bool)chb_isServerUrl.IsChecked;

                selectedRecord.DevModeEnabled = (bool)chb_devModeEnabled.IsChecked;
                selectedRecord.ShowHelpTab = (bool)chb_showHelpTab.IsChecked;

                selectedRecord.InheritedSystemApiCallType = cb_InheritedSystemApiCallType.SelectedItem == null ? null : ((SolutionMixedEnumList)cb_InheritedSystemApiCallType.SelectedItem).Name;
                selectedRecord.StartupUrl = txt_startupUrl.Text;
                selectedRecord.HelpTabUrl = txt_helpTabUrl.Text;

                selectedRecord.InheritedSetName = txt_InheritedSetName.Text;
                selectedRecord.IsOwnServerUrl = (bool)chb_IsOwnServerUrl.IsChecked;
                selectedRecord.StartupSubFolder = txt_startupSubFolder.Text;
                selectedRecord.StartupCommand = txt_startupCommand.Text;

                selectedRecord.DbtableName = cb_DbtableName.SelectedItem == null ? null : ((SystemTranslatedTableList)cb_DbtableName.SelectedItem).TableName;
                selectedRecord.ColumnName = cb_ColumnName.SelectedItem == null ? null : ((GenericDataList)cb_ColumnName.SelectedItem).Data;


                selectedRecord.UseIooverDom = (bool)chb_UseIooverDom.IsChecked;
                selectedRecord.DomhtmlElementName = txt_DomhtmlElementName.Text;
                selectedRecord.GetWebDataJscriptCmd = txt_GetWebDataJscriptCmd.Text;
                selectedRecord.SetWebDataJscriptCmd = txt_SetWebDataJscriptCmd.Text;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSystemCustomPageList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSystemCustomPageList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SystemCustomPageList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) {

                await MainWindow.ShowMessageOnMainWindow(true, SystemOperations.GetExceptionMessagesAll(autoEx), false);
                App.ApplicationLogging(autoEx);
            }
            MainWindow.ProgressRing = Visibility.Hidden;
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SystemCustomPageList)DgListView.SelectedItem : new SystemCustomPageList();
            SetRecord(false);
        }


        private async void SetRecord(bool? showForm = null, bool copy = false) {

            try {
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;
                cb_InheritedFormType.SelectedItem = (selectedRecord.Id == 0) ? inheritedFormType.FirstOrDefault() : inheritedFormType.FirstOrDefault(a => a.Name == selectedRecord.InheritedFormType);

                txt_pageName.Text = selectedRecord.PageName;
                txt_description.Text = selectedRecord.Description;

                chb_IsSystemUrl.IsChecked = selectedRecord.IsSystemUrl;
                chb_isServerUrl.IsChecked = selectedRecord.IsServerUrl;
                chb_IsInteractAgenda.IsChecked = selectedRecord.IsInteractAgenda;

                chb_devModeEnabled.IsChecked = selectedRecord.DevModeEnabled;
                chb_showHelpTab.IsChecked = selectedRecord.ShowHelpTab;

                cb_InheritedSystemApiCallType.SelectedItem = (selectedRecord.Id == 0) ? inheritedSystemApiCallType.FirstOrDefault() : inheritedSystemApiCallType.FirstOrDefault(a => a.Name == selectedRecord.InheritedHelpTabSourceType);
                txt_startupUrl.Text = selectedRecord.StartupUrl;
                txt_helpTabUrl.Text = selectedRecord.HelpTabUrl;

                cb_DbtableName.SelectedItem = selectedRecord.DbtableName == null ? null : systemTranslatedTableList.FirstOrDefault(a => a.TableName == selectedRecord.DbtableName);
                cb_ColumnName.SelectedValue = selectedRecord.ColumnName;

                chb_UseIooverDom.IsChecked = selectedRecord.UseIooverDom;
                txt_DomhtmlElementName.Text = selectedRecord.DomhtmlElementName;
                txt_GetWebDataJscriptCmd.Text = selectedRecord.GetWebDataJscriptCmd;
                txt_SetWebDataJscriptCmd.Text = selectedRecord.SetWebDataJscriptCmd;

                txt_InheritedSetName.Text = selectedRecord.InheritedSetName;
                txt_startupSubFolder.Text = selectedRecord.StartupSubFolder;
                txt_startupCommand.Text = selectedRecord.StartupCommand;

                chb_active.IsChecked = (selectedRecord.Id == 0) ? bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value) : selectedRecord.Active;

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
            MainWindow.ProgressRing = Visibility.Hidden;
        }


        private void IsServerEnabled(object sender, RoutedEventArgs e) {
            gb_serverSetting.IsEnabled = (bool)chb_IsOwnServerUrl.IsChecked;
            if ((bool)chb_IsOwnServerUrl.IsChecked) { chb_IsSystemUrl.IsChecked = chb_isServerUrl.IsChecked = false; gb_startupSetting.IsEnabled = true; }
            else if (!(bool)chb_IsSystemUrl.IsChecked && !(bool)chb_isServerUrl.IsChecked && !(bool)chb_IsOwnServerUrl.IsChecked) { gb_startupSetting.IsEnabled = false; }
        }

        private void InteractAgendaSelected(object sender, RoutedEventArgs e) {
            gb_interaktSetting.IsEnabled = (bool)chb_IsInteractAgenda.IsChecked;
        }


        private async void TableSelected(object sender, SelectionChangedEventArgs e) {
            if (((ComboBox)sender).SelectedIndex > -1) {
                tableSchema = await CommunicationManager.GetApiRequest<List<GenericDataList>>(ApiUrls.ServerApi, $"DatabaseServices/SpGetTableSchema/{((SystemTranslatedTableList)cb_DbtableName.SelectedItem).TableName}", App.UserData.Authentification.Token);
                cb_ColumnName.ItemsSource = tableSchema;
                if (selectedRecord.Id != 0) { cb_ColumnName.SelectedValue = selectedRecord.ColumnName; }
            }
        }


        private void UrlTypeSelected(object sender, RoutedEventArgs e) {
            gb_startupSetting.IsEnabled = (bool)chb_IsSystemUrl.IsChecked || (bool)chb_isServerUrl.IsChecked || (bool)chb_IsOwnServerUrl.IsChecked ? true : false;

            if (((CheckBox)sender).Name == "chb_IsSystemUrl" && (bool)chb_IsSystemUrl.IsChecked) { chb_isServerUrl.IsChecked = chb_IsOwnServerUrl.IsChecked = false; }
            else if (((CheckBox)sender).Name == "chb_isServerUrl" && (bool)chb_isServerUrl.IsChecked) { chb_IsSystemUrl.IsChecked = chb_IsOwnServerUrl.IsChecked = false; }
        }

        private void DomElementSelected(object sender, RoutedEventArgs e) {
            txt_SetWebDataJscriptCmd.IsEnabled = txt_GetWebDataJscriptCmd.IsEnabled = !(bool)chb_UseIooverDom.IsChecked;
            txt_DomhtmlElementName.IsEnabled = (bool)chb_UseIooverDom.IsChecked;
        }

        private void IsHelpTabEnabled(object sender, RoutedEventArgs e) {
            txt_helpTabUrl.IsEnabled = cb_InheritedSystemApiCallType.IsEnabled = (bool)chb_showHelpTab.IsChecked;
        }
    }
}