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

namespace EasyITSystemCenter.Pages {

    public partial class SolutionEmailTemplateListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionEmailTemplateList selectedRecord = new SolutionEmailTemplateList();

        private List<SolutionEmailTemplateList> emailTemplateList = new List<SolutionEmailTemplateList>();
        private List<SolutionLanguageList> systemLanguageList = new List<SolutionLanguageList>();
        private List<SolutionMixedEnumList> inheritedEmailTypes = new List<SolutionMixedEnumList>();

        public SolutionEmailTemplateListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            //ObservableCollection<TranslateSet> Templates = new ObservableCollection<TranslateSet>() {
            //                                                    new TranslateSet() { Name = Resources["verification"].ToString(), Value = "verification" },
            //                                                    new TranslateSet() { Name = Resources["registration"].ToString(), Value = "registration"},
            //                                                    new TranslateSet() { Name = Resources["resetPassword"].ToString(), Value = "resetPassword"}                                                             };

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
                inheritedEmailTypes = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/EmailTypes", App.UserData.Authentification.Token);
                emailTemplateList = await CommunicationManager.GetApiRequest<List<SolutionEmailTemplateList>>(ApiUrls.EasyITCenterSolutionEmailTemplateList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                systemLanguageList = await CommunicationManager.GetApiRequest<List<SolutionLanguageList>>(ApiUrls.EasyITCenterSolutionLanguageList, null, App.UserData.Authentification.Token);

                inheritedEmailTypes.ForEach(async item => item.Translation = await DBOperations.DBTranslation(item.Name));
                systemLanguageList.ForEach(async language => {
                    language.Translation = await DBOperations.DBTranslation(language.SystemName);
                });

                emailTemplateList.ForEach(async template => {
                    template.TemplateNameTranslation = await DBOperations.DBTranslation(template.TemplateName);
                    template.SystemLanguageTranslation = systemLanguageList.First(a => a.Id == template.SystemLanguageId).Translation;
                });

                cb_templateName.ItemsSource = inheritedEmailTypes;
                DgListView.ItemsSource = emailTemplateList;
                DgListView.Items.Refresh();
                cb_systemLanguage.ItemsSource = systemLanguageList;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "TemplateNameTranslation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "Subject".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "SystemLanguageTranslation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "SystemLanguageId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "TemplateName".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "Email".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "Variables".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
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
            selectedRecord = new SolutionEmailTemplateList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SolutionEmailTemplateList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SolutionEmailTemplateList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterSolutionEmailTemplateList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SolutionEmailTemplateList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SolutionEmailTemplateList)DgListView.SelectedItem; } else { selectedRecord = new SolutionEmailTemplateList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);

                selectedRecord.SystemLanguageId = ((SolutionLanguageList)cb_systemLanguage.SelectedItem).Id;
                selectedRecord.TemplateName = (string)cb_templateName.SelectedValue;
                selectedRecord.Variables = txt_variables.Text;

                selectedRecord.Subject = txt_subject.Text;
                selectedRecord.Email = html_email.Browser.GetCurrentHtml();

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSolutionEmailTemplateList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSolutionEmailTemplateList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SolutionEmailTemplateList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SolutionEmailTemplateList)DgListView.SelectedItem : new SolutionEmailTemplateList();
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            try { 

                txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                cb_systemLanguage.SelectedItem = (selectedRecord.Id == 0) ? systemLanguageList.FirstOrDefault() : systemLanguageList.FirstOrDefault(a => a.Id == selectedRecord.SystemLanguageId);
                cb_templateName.SelectedValue = txt_id.Value == 0 ? "" : selectedRecord.TemplateName;
                txt_variables.Text = selectedRecord.Variables;

                txt_subject.Text = selectedRecord.Subject;
                html_email.HtmlContent = selectedRecord.Email;

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

        private void TemplateNameChanged(object sender, SelectionChangedEventArgs e) {
            switch (cb_templateName.SelectedValue) {
                case "Verification":
                    txt_variables.Text = "[verifyCode]";
                    break;

                case "Registration":
                    txt_variables.Text = "[firstname],[lastname],[email],[password]";
                    break;

                case "ResetPassword":
                    txt_variables.Text = "[firstname],[lastname],[email],[password]";
                    break;

                default:
                    txt_variables.Text = "";
                    break;
            }
        }
    }
}