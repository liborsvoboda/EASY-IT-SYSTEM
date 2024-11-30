using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xwt.WPFBackend;

namespace EasyITSystemCenter.Pages {

    public partial class SolutionTaskListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionTaskList selectedRecord = new SolutionTaskList();

        private List<SolutionMixedEnumList> inheritedTargetType = new List<SolutionMixedEnumList>();
        private List<SolutionMixedEnumList> inheritedStatusType = new List<SolutionMixedEnumList>();

        public SolutionTaskListPage() {
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
                inheritedTargetType = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/SolutionTargetType", App.UserData.Authentification.Token);
                inheritedStatusType = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/TaskStatusType", App.UserData.Authentification.Token);
                DgListView.ItemsSource = await CommunicationManager.GetApiRequest<List<SolutionTaskList>>(ApiUrls.EasyITCenterSolutionTaskList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                inheritedTargetType.ForEach(async tasktype => { tasktype.Translation = await DBOperations.DBTranslation(tasktype.Name); });
                inheritedStatusType.ForEach(async tasktype => { tasktype.Translation = await DBOperations.DBTranslation(tasktype.Name); });

                cb_inheritedTargetType.ItemsSource = inheritedTargetType;
                cb_inheritedStatusType.ItemsSource = inheritedStatusType;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "InheritedTargetType".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "InheritedStatusType".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "Message".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    
                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }

                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "ImageName".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "Image".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "AttachmentName".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "Attachment".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "Documentation".ToLower()) e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SolutionTaskList search = e as SolutionTaskList;
                    return search.InheritedTargetType.ToLower().Contains(filter.ToLower())
                    || search.InheritedStatusType.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.Message) && search.Message.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.Documentation) && search.Documentation.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new SolutionTaskList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SolutionTaskList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SolutionTaskList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterSolutionTaskList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SolutionTaskList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SolutionTaskList)DgListView.SelectedItem; }
            else { selectedRecord = new SolutionTaskList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.InheritedTargetType = ((SolutionMixedEnumList)cb_inheritedTargetType.SelectedItem).Name;
                selectedRecord.InheritedStatusType = ((SolutionMixedEnumList)cb_inheritedStatusType.SelectedItem).Name;

                selectedRecord.Message = txt_message.Text;
                selectedRecord.Documentation = txt_documentation.Text;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSolutionTaskList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSolutionTaskList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SolutionTaskList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SolutionTaskList)DgListView.SelectedItem : new SolutionTaskList();
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            try {
                txt_id.Value = (copy) ? 0 : selectedRecord.Id;
                cb_inheritedTargetType.SelectedItem = (selectedRecord.Id == 0) ? inheritedTargetType.FirstOrDefault() : inheritedTargetType.First(a => a.Name == selectedRecord.InheritedTargetType);
                cb_inheritedStatusType.SelectedItem = (selectedRecord.Id == 0) ? inheritedStatusType.FirstOrDefault() : inheritedStatusType.First(a => a.Name == selectedRecord.InheritedStatusType);

                txt_message.Text = selectedRecord.Message;
                txt_documentation.Text = selectedRecord.Documentation;

                btn_exportImage.IsEnabled = selectedRecord.Image != null ? true : false;
                img_image.Source = selectedRecord.Image != null ? MediaOperations.ByteToImage(selectedRecord.Image) : null;
                btn_exportAttachment.IsEnabled = selectedRecord.Attachment != null ? true : false;
                txt_attachment.Text = selectedRecord.AttachmentName;

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

        private void btnBrowse_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = ".*", Filter = "All files |*.*", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) {
                    txt_attachment.Text = selectedRecord.AttachmentName = dlg.FileName;
                    selectedRecord.Attachment = File.ReadAllBytes(dlg.FileName);
                    btn_exportAttachment.IsEnabled = true;
                }
                else { selectedRecord.AttachmentName = null; selectedRecord.Attachment = null; btn_exportAttachment.IsEnabled = false; }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private async void TakeScreenShot_Click(object sender, RoutedEventArgs e) {
            selectedRecord.ImageName = "SystemImage_" + DateTimeOffset.Now.Date.ToShortDateString() + ".png";
            selectedRecord.Image = await MediaOperations.SaveAppScreenShot((MainWindow)this.GetParentOrMainWindow(), true);
            img_image.Source = MediaOperations.ByteToImage(selectedRecord.Image);
            btn_exportImage.IsEnabled = true;
        }

        private void ExportImage_Click(object sender, RoutedEventArgs e) {
            try {
                SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".*", Filter = "All files |*.*", Title = Resources["fileOpenDescription"].ToString(), FileName = selectedRecord.ImageName };
                if (dlg.ShowDialog() == true) { FileOperations.ByteArrayToFile(dlg.FileName, selectedRecord.Image); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void ExportAttachment_Click(object sender, RoutedEventArgs e) {
            try {
                SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".*", Filter = "All files |*.*", Title = Resources["fileOpenDescription"].ToString(), FileName = selectedRecord.AttachmentName };
                if (dlg.ShowDialog() == true) { FileOperations.ByteArrayToFile(dlg.FileName, selectedRecord.Attachment); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }
    }
}