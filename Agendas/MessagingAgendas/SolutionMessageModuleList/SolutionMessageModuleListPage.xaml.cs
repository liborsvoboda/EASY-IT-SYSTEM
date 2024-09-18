using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using EasyITSystemCenter.GlobalClasses;

namespace EasyITSystemCenter.Pages {

    public partial class SolutionMessageModuleListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionMessageModuleList selectedRecord = new SolutionMessageModuleList();

        private List<SolutionUserList> solutionUserList = new List<SolutionUserList>();
        private List<SolutionMessageTypeList> messageTypeList = new List<SolutionMessageTypeList>();
        private List<SolutionMessageModuleList> messageModuleList = new List<SolutionMessageModuleList>();
        private bool changeEditorView = false;


        public SolutionMessageModuleListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {

                try {
                    _ = FormOperations.TranslateFormFields(ListForm);
                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                messageTypeList = await CommunicationManager.GetApiRequest<List<SolutionMessageTypeList>>(ApiUrls.SolutionMessageTypeList, null, App.UserData.Authentification.Token);
                solutionUserList = await CommunicationManager.GetApiRequest<List<SolutionUserList>>(ApiUrls.EasyITCenterSolutionUserList, null, App.UserData.Authentification.Token);
                messageModuleList = await CommunicationManager.GetApiRequest<List<SolutionMessageModuleList>>(ApiUrls.SolutionMessageModuleList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                messageTypeList.ForEach(async messageType => { messageType.Translation = await DBOperations.DBTranslation(messageType.Name); });

                messageModuleList.ForEach(async message => {
                    message.ParrentMessage = message.MessageParentId != null ? messageModuleList.FirstOrDefault(a => a.Id == message.MessageParentId).HtmlMessage : null;
                    message.ParentMessageSubject = message.MessageParentId != null ? messageModuleList.FirstOrDefault(a => a.Id == message.MessageParentId).Subject : null;
                    message.MessageTypeTranslation = await DBOperations.DBTranslation(messageTypeList.FirstOrDefault(a => a.Id == message.MessageTypeId).Name);
                    message.GuestEmail = message.MessageParentId != null ? solutionUserList.First(a => a.Id == messageModuleList.FirstOrDefault(b => b.Id == message.MessageParentId).GuestId).Username : null;
                });

                DgListView.ItemsSource = messageModuleList;
                DgListView.Items.Refresh();
                cb_forGuest.ItemsSource = solutionUserList;
                cb_messageType.ItemsSource = messageTypeList;
                
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }


            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "IsSystemMessage".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "Shown".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "MessageTypeTranslation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "Subject".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                    else if (headername == "ParentMessageSubject".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }
                    else if (headername == "Archived".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 6; }
                    else if (headername == "Published".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 7; }
                    else if (headername == "GuestEmail".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 8; }

                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }

                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "MessageTypeId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "HtmlMessage".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "GuestId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "ParrentMessage".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "MessageParentId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "Level".ToLower()) e.Visibility = Visibility.Hidden;

                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SolutionMessageModuleList search = e as SolutionMessageModuleList;
                    return search.Subject.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.HtmlMessage) && search.HtmlMessage.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.ParentMessageSubject) && search.ParentMessageSubject.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new SolutionMessageModuleList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SolutionMessageModuleList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SolutionMessageModuleList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.SolutionMessageModuleList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SolutionMessageModuleList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SolutionMessageModuleList)DgListView.SelectedItem; } else { selectedRecord = new SolutionMessageModuleList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                if (!html_htmlMessage.Browser.IsInSourceEditMode) { html_htmlMessage.Browser.ToggleSourceEditor(html_htmlMessage.Toolbar, true); changeEditorView = true; } else { changeEditorView = false; }

                selectedRecord.MessageTypeId = (int)cb_messageType.SelectedValue;
                selectedRecord.Subject = txt_subject.Text;

                selectedRecord.HtmlMessage = html_htmlMessage.Browser.GetCurrentHtml();
                selectedRecord.Archived = (bool)chb_archive.IsChecked;
                selectedRecord.Shown = selectedRecord.Archived ? true : false;
                selectedRecord.Published = (bool)chb_publish.IsChecked;
                selectedRecord.IsSystemMessage = true;

                
                if (selectedRecord.GuestId == null) { selectedRecord.GuestId = selectedRecord.Id != 0 ? selectedRecord.GuestId : (int?)cb_forGuest.SelectedValue; }

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.SolutionMessageModuleList, httpContent, null, App.UserData.Authentification.Token);
                } else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.SolutionMessageModuleList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SolutionMessageModuleList();
                    await LoadDataList();
                    SetRecord(false);
                } else { await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            if (changeEditorView) { html_htmlMessage.Browser.ToggleSourceEditor(html_htmlMessage.Toolbar, false);changeEditorView = false; }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SolutionMessageModuleList)DgListView.SelectedItem : new SolutionMessageModuleList();
            SetRecord(false);
        }

        private void SetRecord(bool showForm, bool copy = false) {
            try {
                    txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                    if (txt_id.Value == 0) { cb_messageType.ItemsSource = messageTypeList.Where(a => !a.IsSystemOnly); }
                    cb_messageType.SelectedItem = txt_id.Value == 0 ? messageTypeList.FirstOrDefault() : messageTypeList.FirstOrDefault(a => a.Id == selectedRecord.MessageTypeId);
                    cb_messageType.IsEnabled = txt_id.Value == 0;

                    txt_subject.Text = selectedRecord.Subject;
                    chb_publish.IsChecked = selectedRecord.Published; if (selectedRecord.Published) { btn_save.IsEnabled = false; }

                    cb_forGuest.SelectedValue = solutionUserList.FirstOrDefault(a => a.Id == selectedRecord.GuestId);
                    if (selectedRecord.IsSystemMessage && txt_id.Value != 0 && messageTypeList.FirstOrDefault(a => a.Id == selectedRecord.MessageTypeId).AnswerAllowed == true) {
                        cb_forGuest.SelectedItem = solutionUserList.FirstOrDefault(a => a.Id == selectedRecord.GuestId); cb_forGuest.IsEnabled = false;
                        lbl_forGuest.Visibility = cb_forGuest.Visibility = Visibility.Visible; btn_save.IsEnabled = false;
                    }
                    else { lbl_forGuest.Visibility = cb_forGuest.Visibility = Visibility.Hidden; }

                    if (messageTypeList.Any()) { txt_variables.Text = txt_id.Value == 0 ? messageTypeList.FirstOrDefault().Variables : messageTypeList.FirstOrDefault(a => a.Id == selectedRecord.MessageTypeId)?.Variables; }
                    chb_archive.IsChecked = selectedRecord.Archived;

                    if (selectedRecord.MessageParentId != null) {
                        txt_parentMessageSubject.Text = messageModuleList.FirstOrDefault(a => a.Id == selectedRecord.MessageParentId).Subject;
                        txt_parentMessageSubject.Visibility = Visibility.Visible;
                    }
                    html_htmlMessage.Browser.OpenDocument(selectedRecord.HtmlMessage);

                    if (!selectedRecord.IsSystemMessage && txt_id.Value != 0 && messageTypeList.FirstOrDefault(a => a.Id == selectedRecord.MessageTypeId).AnswerAllowed == true) { btn_doAnswer.Visibility = Visibility.Visible; btn_doAnswer.IsEnabled = true; }
                    else { btn_doAnswer.Visibility = Visibility.Hidden; }

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            if (showForm) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            } else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = false;
            }
        }

        private void MessageTypeChanged(object sender, SelectionChangedEventArgs e) {
            if (dataViewSupport.FormShown) {
                txt_variables.Text = messageTypeList.FirstOrDefault(a => a.Id == (int)cb_messageType.SelectedValue).Variables;

                if (txt_id.Value == 0 && ((SolutionMessageTypeList)((ComboBox)sender).SelectedItem).IsSystemOnly) { }
                else if (txt_id.Value == 0 && ((SolutionMessageTypeList)((ComboBox)sender).SelectedItem).Name.ToLower() == "private") {
                    lbl_forGuest.Visibility = cb_forGuest.Visibility = Visibility.Visible; cb_forGuest.IsEnabled = true;
                    cb_forGuest.SelectedItem = cb_forGuest.Items[0]; chb_publish.IsChecked = true;
                }
                else if (txt_id.Value == 0 && ((SolutionMessageTypeList)((ComboBox)sender).SelectedItem).Name.ToLower() == "newsletter") {
                    btn_doAnswer.Visibility = lbl_forGuest.Visibility = cb_forGuest.Visibility = Visibility.Hidden; chb_publish.IsChecked = false;
                }
                else if (txt_id.Value == 0 && ((SolutionMessageTypeList)((ComboBox)sender).SelectedItem).Name.ToLower() == "discussionForum") {
                    btn_doAnswer.Visibility = lbl_forGuest.Visibility = cb_forGuest.Visibility = Visibility.Hidden; chb_publish.IsChecked = false;
                }
                else if (txt_id.Value != 0 && ((SolutionMessageTypeList)((ComboBox)sender).SelectedItem).Name.ToLower() == "private") {
                    btn_doAnswer.Visibility = Visibility.Visible; chb_publish.IsChecked = true;
                    lbl_forGuest.Visibility = cb_forGuest.Visibility = Visibility.Hidden;
                }
            }
        }

        private void BtnDoAnswer_Click(object sender, RoutedEventArgs e) {
            html_htmlMessage.Browser.OpenDocument(null);
            selectedRecord.Level += 1; selectedRecord.MessageParentId = (int)txt_id.Value; txt_id.Value = 0;
            btn_save.IsEnabled = true;btn_doAnswer.IsEnabled = false;
        }
        private void PublishChanged(object sender, RoutedEventArgs e) => btn_save.IsEnabled = true;

        private async void BtnShowPreview_Click(object sender, RoutedEventArgs e) {
            using (HttpClient httpClient = new HttpClient()) {
                try {
                    MainWindow.ProgressRing = Visibility.Visible;
                    if (!html_htmlMessage.Browser.IsInSourceEditMode) { html_htmlMessage.Browser.ToggleSourceEditor(html_htmlMessage.Toolbar, true); changeEditorView = true; } else { changeEditorView = false; }
                    
                    var mesageData = new List<Dictionary<string, string>> {
                       new Dictionary<string, string> { { "HtmlTitle", txt_subject.Text } },
                       new Dictionary<string, string> { { "HtmlContent", html_htmlMessage.Browser.GetCurrentHtml() } },
                    };
                    StringContent httpContent = new StringContent(JsonConvert.SerializeObject(mesageData), System.Text.Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserData.Authentification.Token);
                    HttpResponseMessage jsonResult = await httpClient.PostAsync(App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebNewsLetterPreviewPostAPI")), httpContent);
                    DBResultMessage dBResult = JsonConvert.DeserializeObject<DBResultMessage>(await jsonResult.Content.ReadAsStringAsync());
                    MainWindow.ProgressRing = Visibility.Hidden;

                    if (dBResult.Status.ToLower() != "success") { await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("ParameterWebNewsLetterPreviewPostAPIerror") + Environment.NewLine + dBResult.ErrorMessage); }
                    else { SystemOperations.StartExternalProccess(SystemLocalEnumSets.ProcessTypes.First(a => a.Value.ToLower() == "url").Value, App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebNewsLetterPreviewPostAPI"))); }
                } catch (Exception ex) {
                    MainWindow.ProgressRing = Visibility.Hidden;
                    await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + ex.Message + Environment.NewLine + ex.StackTrace);
                }
                if (changeEditorView) { html_htmlMessage.Browser.ToggleSourceEditor(html_htmlMessage.Toolbar, false); changeEditorView = false; }
            }
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = ".html", Filter = "Html files |*.html|All files (*.*)|*.*", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) {
                    html_htmlMessage.Browser.OpenDocument(File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName)));
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }
    }
}