using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Xaml.Behaviors.Layout;
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

    public partial class SolutionSchedulerListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionSchedulerList selectedRecord = new SolutionSchedulerList();

        private List<SolutionMixedEnumList> timeIntervalTypeList = new List<SolutionMixedEnumList>();
        private List<SolutionMixedEnumList> solutionScheduledTypeList = new List<SolutionMixedEnumList>();
        private List<SolutionSchedulerList> solutionSchedulerLists = new List<SolutionSchedulerList>();

        public SolutionSchedulerListPage() {
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

                solutionScheduledTypeList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/SchedulerDial", App.UserData.Authentification.Token);
                timeIntervalTypeList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/TimeIntervalDial", App.UserData.Authentification.Token);
                solutionSchedulerLists = await CommunicationManager.GetApiRequest<List<SolutionSchedulerList>>(ApiUrls.EasyITCenterSolutionSchedulerList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                timeIntervalTypeList.ForEach(async timeType => { timeType.Translation = await DBOperations.DBTranslation(timeType.Name); });
                solutionScheduledTypeList.ForEach(async tasktype => { tasktype.Translation = await DBOperations.DBTranslation(tasktype.Name); });
                solutionSchedulerLists.ForEach(scheduleTask => {
                    scheduleTask.GroupNameTranslation = solutionScheduledTypeList.First(a => a.Name == scheduleTask.InheritedGroupName).Translation;
                    scheduleTask.IntervalTypeTranslation = timeIntervalTypeList.First(a => a.Name == scheduleTask.InheritedIntervalType).Translation;
                });

                DgListView.ItemsSource = solutionSchedulerLists;
                DgListView.Items.Refresh();
                cb_inheritedGroupName.ItemsSource = solutionScheduledTypeList;
                cb_inheritedIntervalType.ItemsSource = timeIntervalTypeList;

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "groupnametranslation") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "name") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "sequence") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "email") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                    else if (headername == "interval") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }
                    else if (headername == "intervaltypetranslation") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 6; }
                    else if (headername == "startnowonly") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 7; }
                    else if (headername == "startat") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 8; }
                    else if (headername == "finishat") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 9; }
                    else if (headername == "active") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                    else if (headername == "timestamp") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "id") e.DisplayIndex = 0;
                    else if (headername == "inheritedgroupname") e.Visibility = Visibility.Hidden;
                    else if (headername == "inheritedintervaltype") e.Visibility = Visibility.Hidden;
                    else if (headername == "userid") e.Visibility = Visibility.Hidden;
                    else if (headername == "inheritedtaskname") e.Visibility = Visibility.Hidden;
                    else if (headername == "data") e.Visibility = Visibility.Hidden;
                    else if (headername == "description") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SolutionSchedulerList user = e as SolutionSchedulerList;
                    return user.InheritedGroupName.ToLower().Contains(filter.ToLower())
                    || user.Name.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Email) && user.Email.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Data) && user.Data.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new SolutionSchedulerList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SolutionSchedulerList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SolutionSchedulerList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterSolutionSchedulerList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SolutionSchedulerList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SolutionSchedulerList)DgListView.SelectedItem; }
            else { selectedRecord = new SolutionSchedulerList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);

                selectedRecord.InheritedGroupName = ((SolutionMixedEnumList)cb_inheritedGroupName.SelectedItem).Name;
                selectedRecord.Name = txt_name.Text;
                selectedRecord.Sequence = int.Parse(txt_sequence.Value.ToString());
                selectedRecord.Email = txt_email.Text;
                selectedRecord.Data = txt_data.Text;
                selectedRecord.Description = txt_description.Text;

                selectedRecord.StartNowOnly = (bool)chb_startNowOnly.IsChecked;
                selectedRecord.StartAt = !(bool)chb_startNowOnly.IsChecked || dp_startAt.Value == null ? DateTimeOffset.Now.DateTime : dp_startAt.Value;
                selectedRecord.FinishAt = dp_finishAt.Value == null ? DateTimeOffset.Now.DateTime : dp_finishAt.Value;
                selectedRecord.Interval = int.Parse(txt_interval.Value.ToString());
                selectedRecord.InheritedIntervalType = ((SolutionMixedEnumList)cb_inheritedIntervalType.SelectedItem).Name;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSolutionSchedulerList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSolutionSchedulerList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SolutionSchedulerList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SolutionSchedulerList)DgListView.SelectedItem : new SolutionSchedulerList();
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            try {
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                cb_inheritedGroupName.SelectedItem = (selectedRecord.Id == 0) ? solutionScheduledTypeList.FirstOrDefault() : solutionScheduledTypeList.First(a => a.Name == selectedRecord.InheritedGroupName);
                txt_name.Text = selectedRecord.Name;
                txt_sequence.Value = selectedRecord.Sequence;
                txt_email.Text = selectedRecord.Email;
                txt_data.Text = selectedRecord.Data;
                txt_description.Text = selectedRecord.Description;

                chb_startNowOnly.IsChecked = selectedRecord.StartNowOnly;
                dp_startAt.Value = selectedRecord.StartAt;
                dp_finishAt.Value = selectedRecord.FinishAt;
                txt_interval.Value = selectedRecord.Interval;
                cb_inheritedIntervalType.SelectedItem = (selectedRecord.Id == 0) ? timeIntervalTypeList.FirstOrDefault() : timeIntervalTypeList.First(a => a.Name == selectedRecord.InheritedIntervalType);
                chb_active.IsChecked = (selectedRecord.Id == 0) ? bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value) : selectedRecord.Active;

                if (showForm != null && showForm == true) {
                    MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                    ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
                    StartNowOnly(); LoadSubDataList();
                }
                else {
                    MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                    ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }


        //change subdatasource
        public async void LoadSubDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<SolutionSchedulerProcessList> solutionSchedulerProcessList = new List<SolutionSchedulerProcessList>();
            try {
                DgSubListView.ItemsSource = null;
                solutionSchedulerProcessList = await CommunicationManager.GetApiRequest<List<SolutionSchedulerProcessList>>(ApiUrls.EasyITCenterSolutionSchedulerProcessList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
 
                DgSubListView.ItemsSource = solutionSchedulerProcessList;
                DgSubListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
        }

        private async void DgSubListView_Translate(object sender, EventArgs ex) {
            ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                try {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "processcrashed") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "processcompleted") { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }

                    else if (headername == "timestamp") { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "id") e.DisplayIndex = 0;

                    else if (headername == "processlog") e.Visibility = Visibility.Hidden;
                    else if (headername == "processdata") e.Visibility = Visibility.Hidden;
                    else if (headername == "scheduledtaskid") e.Visibility = Visibility.Hidden;
                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            });
        }

        private void DgSubListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgSubListView.SelectedItems.Count > 0) {
                txt_processData.Text =  ((SolutionSchedulerProcessList)DgSubListView.SelectedItem).ProcessData;
                txt_processLog.Text = ((SolutionSchedulerProcessList)DgSubListView.SelectedItem).ProcessLog;
            }
        }

        private void DgSubListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgSubListView.SelectedItems.Count > 0) {
                txt_processData.Text = ((SolutionSchedulerProcessList)DgSubListView.SelectedItem).ProcessData;
                txt_processLog.Text = ((SolutionSchedulerProcessList)DgSubListView.SelectedItem).ProcessLog;
            }
        }


        private void StartNowOnlyChanged(object sender, RoutedEventArgs e) => StartNowOnly();
        private void StartNowOnly() {
            if (dataViewSupport.FormShown) {
                dp_startAt.IsEnabled = !(bool)chb_startNowOnly.IsChecked; if ((bool)chb_startNowOnly.IsChecked) { dp_startAt.Value = null; }
                dp_finishAt.IsEnabled = !(bool)chb_startNowOnly.IsChecked; if ((bool)chb_startNowOnly.IsChecked) { dp_finishAt.Value = null; }
                txt_interval.IsEnabled = !(bool)chb_startNowOnly.IsChecked; if ((bool)chb_startNowOnly.IsChecked) { txt_interval.Value = 0; }
                cb_inheritedIntervalType.IsEnabled = !(bool)chb_startNowOnly.IsChecked;
            }
        }


        private async void ComboBoxShowToolTipOnChange(object sender, SelectionChangedEventArgs e) {
            if (((ComboBox)sender).SelectedValue != null) {
                var descriptionInfo = ((ComboBox)sender).ItemsSource.OfType<SolutionMixedEnumList>().ToList().Where(a => a.Name == ((ComboBox)sender).SelectedValue.ToString()).First().Description;
                if (!string.IsNullOrWhiteSpace(descriptionInfo)) {
                    ((ComboBox)sender).ToolTip = await DBOperations.DBTranslation("ImportantInfo") + Environment.NewLine + descriptionInfo;
                }
            }
        }

    }
}