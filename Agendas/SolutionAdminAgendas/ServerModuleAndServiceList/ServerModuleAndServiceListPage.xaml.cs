using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using ICSharpCode.AvalonEdit.Highlighting;
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

    public partial class ServerModuleAndServiceListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ServerModuleAndServiceList selectedRecord = new ServerModuleAndServiceList();

        private List<SolutionMixedEnumList> solutionMixedEnumList = new List<SolutionMixedEnumList>();
        private List<SolutionMixedEnumList> solutionMixedEnumListLayoutTypes = new List<SolutionMixedEnumList>();
        private List<ServerModuleAndServiceList> ServerModuleAndServiceList = new List<ServerModuleAndServiceList>();
        private List<SolutionUserRoleList> userRoleList = new List<SolutionUserRoleList>();

        public ServerModuleAndServiceListPage() {
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
                solutionMixedEnumList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/ModulePageTypes", App.UserData.Authentification.Token);
                solutionMixedEnumListLayoutTypes = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/WebLayoutTypes", App.UserData.Authentification.Token);
                userRoleList = await CommunicationManager.GetApiRequest<List<SolutionUserRoleList>>(ApiUrls.EasyITCenterSolutionUserRoleList, null, App.UserData.Authentification.Token);
                ServerModuleAndServiceList = await CommunicationManager.GetApiRequest<List<ServerModuleAndServiceList>>(ApiUrls.ServerModuleAndServiceList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                solutionMixedEnumList.ForEach(async tasktype => { tasktype.Translation = await DBOperations.DBTranslation(tasktype.Name); });
                solutionMixedEnumListLayoutTypes.ForEach(async layoutType => { layoutType.Translation = await DBOperations.DBTranslation(layoutType.Name); });
                userRoleList.ForEach(async role => { role.Translation = await DBOperations.DBTranslation(role.SystemName); });
                ServerModuleAndServiceList.ForEach(module => {
                    module.PageTypeTranslation = solutionMixedEnumList.FirstOrDefault(a => a.Name == module.InheritedPageType).Translation;
                    try { module.LayoutTypeTranslation = module.InheritedLayoutType == null ? module.InheritedLayoutType : solutionMixedEnumListLayoutTypes.FirstOrDefault(a => a.Name == module.InheritedLayoutType)?.Translation; } catch { }
                });

            DgListView.ItemsSource = ServerModuleAndServiceList;
                DgListView.Items.Refresh();
                cb_inheritedPageType.ItemsSource = solutionMixedEnumList;
                cb_allowedRoles.ItemsSource = userRoleList.OrderBy(a => a.Translation).ToList();
                cb_inheritedLayoutType.ItemsSource = solutionMixedEnumListLayoutTypes.OrderBy(a => a.Translation).ToList();

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "name".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "pagetypetranslation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "layoutTypeTranslation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "urlsubpath".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "restrictedaccess".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                    else if (headername == "redirecttologinservice".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }
                    else if (headername == "redirectpathonerror".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 6; }
                    else if (headername == "isloginmodule".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 7; }
                    else if (headername == "allowedroles".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 8; }
                    else if (headername == "active".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 7; }
                    else if (headername == "timestamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "id".ToLower()) e.DisplayIndex = 0;

                    else if (headername == "inheritedLayoutType".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "userid".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "description".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "optionalconfiguration".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "razorpagesetting".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "customhtmlcontent".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "razorpageallowed".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "pathsetallowed".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "restrictionsetallowed".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "htmlsetallowed".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "redirectsetallowed".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "inheritedpagetype".ToLower()) e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    ServerModuleAndServiceList search = e as ServerModuleAndServiceList;
                    return search.Name.ToLower().Contains(filter.ToLower())
                    || search.PageTypeTranslation.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.UrlSubPath) && search.UrlSubPath.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.OptionalConfiguration) && search.OptionalConfiguration.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.RedirectPathOnError) && search.RedirectPathOnError.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.Description) && search.Description.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new ServerModuleAndServiceList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (ServerModuleAndServiceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (ServerModuleAndServiceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.ServerModuleAndServiceList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (ServerModuleAndServiceList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (ServerModuleAndServiceList)DgListView.SelectedItem; } else { selectedRecord = new ServerModuleAndServiceList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);

                selectedRecord.InheritedPageType = ((SolutionMixedEnumList)cb_inheritedPageType.SelectedItem).Name;
                selectedRecord.Name = txt_name.Text;
                selectedRecord.InheritedLayoutType = ((SolutionMixedEnumList)cb_inheritedLayoutType.SelectedItem).Name;

                selectedRecord.Description = txt_description.Text;
                selectedRecord.UrlSubPath = txt_urlSubPath.Text;
                selectedRecord.OptionalConfiguration = txt_optionalConfiguration.Text;

                selectedRecord.AllowedRoles = "";
                for (int i = 0; i < cb_allowedRoles.SelectedItems.Count; i++) { selectedRecord.AllowedRoles += ((SolutionUserRoleList)cb_allowedRoles.SelectedItems[i]).SystemName + ","; }

                selectedRecord.IsLoginModule = (bool)chb_isLoginModule.IsChecked;
                selectedRecord.RestrictedAccess = (bool)chb_restrictedAccess.IsChecked;
                selectedRecord.RedirectPathOnError = txt_redirectPathOnError.Text;
                selectedRecord.CustomHtmlContent = CustomHtmlContent.Text;

                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.ServerModuleAndServiceList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.ServerModuleAndServiceList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new ServerModuleAndServiceList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (ServerModuleAndServiceList)DgListView.SelectedItem : new ServerModuleAndServiceList();
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;
		
		try {cb_inheritedPageType.SelectedItem = (selectedRecord.Id == 0) ? solutionMixedEnumList.FirstOrDefault() : solutionMixedEnumList.FirstOrDefault(a => a.Name == selectedRecord.InheritedPageType);
            txt_name.Text = selectedRecord.Name;
            cb_inheritedLayoutType.SelectedItem = (selectedRecord.Id == 0) ? solutionMixedEnumListLayoutTypes.FirstOrDefault() : solutionMixedEnumListLayoutTypes.FirstOrDefault(a => a.Name == selectedRecord.InheritedLayoutType);
            txt_description.Text = selectedRecord.Description;

            txt_urlSubPath.Text = selectedRecord.UrlSubPath;
            txt_optionalConfiguration.Text = selectedRecord.OptionalConfiguration;

            cb_allowedRoles.SelectedItems.Clear();
            if (!string.IsNullOrWhiteSpace(selectedRecord.AllowedRoles))
                selectedRecord.AllowedRoles.Split(',').ToList().ForEach(role => { if (!string.IsNullOrEmpty(role)) cb_allowedRoles.SelectedItems.Add(userRoleList.First(a => a.SystemName == role)); });

            chb_isLoginModule.IsChecked = selectedRecord.IsLoginModule;
            chb_restrictedAccess.IsChecked = selectedRecord.RestrictedAccess;
            txt_redirectPathOnError.Text = selectedRecord.RedirectPathOnError;
            CustomHtmlContent.Text = selectedRecord.CustomHtmlContent;

            chb_active.IsChecked = (selectedRecord.Id == 0) ? bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value) : selectedRecord.Active;
		} catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
		
            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
                //setAllowedFields();
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private void HighlightCodeChanged(object sender, SelectionChangedEventArgs e) {
            CustomHtmlContent.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString());
        }


        /* TODO Implement FOR SYSTEM Modules
        private void setAllowedFields() {
            txt_urlSubPath.IsEnabled = selectedRecord.PathSetAllowed;
            chb_restrictedAccess.IsEnabled = selectedRecord.RedirectSetAllowed;
            CustomHtmlContent.IsEnabled = selectedRecord.HtmlSetAllowed;
            txt_redirectPathOnError.IsEnabled = selectedRecord.RedirectSetAllowed;
        }
        */

        private void RestrictedAccessChanged(object sender, RoutedEventArgs e) {
            if (dataViewSupport.FormShown) {
                txt_redirectPathOnError.Text = null;
                txt_redirectPathOnError.IsEnabled = !(bool)chb_restrictedAccess.IsChecked;
            }
        }

    }
}