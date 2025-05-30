﻿using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyITSystemCenter.Pages {

    public partial class SolutionOperationListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionOperationList selectedRecord = new SolutionOperationList();

        private List<SolutionMixedEnumList> mixedEnumTypesList = new List<SolutionMixedEnumList>();
        private List<SolutionMixedEnumList> mixedEnumResultTypesList = new List<SolutionMixedEnumList>();
        private List<SolutionOperationList> SolutionOperationLists = new List<SolutionOperationList>();

        public SolutionOperationListPage() {
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
                SolutionOperationLists = await CommunicationManager.GetApiRequest<List<SolutionOperationList>>(ApiUrls.EasyITCenterSolutionOperationList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                mixedEnumTypesList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/OperationTypes", App.UserData.Authentification.Token);
                mixedEnumResultTypesList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/ApiResultTypes", App.UserData.Authentification.Token);

                mixedEnumTypesList.ForEach(async tasktype => { tasktype.Translation = await DBOperations.DBTranslation(tasktype.Name); });
                mixedEnumResultTypesList.ForEach(async tasktype => { tasktype.Translation = await DBOperations.DBTranslation(tasktype.Name); });
                SolutionOperationLists.ForEach(operation => {
                    operation.TypeNameTranslation = mixedEnumTypesList.First(a => a.Name == operation.InheritedOperationTypes).Translation;
                    operation.ResultTypeTranslation = mixedEnumResultTypesList.First(a => a.Name == operation.InheritedApiResultTypes).Translation;
                });

                DgListView.ItemsSource = SolutionOperationLists;
                DgListView.Items.Refresh();
                cb_InheritedTypeName.ItemsSource = mixedEnumTypesList;
                cb_inheritedResultTypeName.ItemsSource = mixedEnumResultTypesList;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "typenametranslation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "name".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "description".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "resulttypetranslation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                    else if (headername == "active".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 2; }
                    else if (headername == "timestamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "userid".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "inputdata".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "InheritedOperationTypes".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "InheritedApiResultTypes".ToLower()) e.Visibility = Visibility.Hidden;
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
            selectedRecord = new SolutionOperationList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SolutionOperationList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SolutionOperationList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterSolutionOperationList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SolutionOperationList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SolutionOperationList)DgListView.SelectedItem; }
            else { selectedRecord = new SolutionOperationList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);

                selectedRecord.InheritedOperationTypes = ((SolutionMixedEnumList)cb_InheritedTypeName.SelectedItem).Name;
                selectedRecord.Name = txt_name.Text;
                selectedRecord.InputData = txt_inputData.Text;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.InheritedApiResultTypes = ((SolutionMixedEnumList)cb_inheritedResultTypeName.SelectedItem).Name;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSolutionOperationList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSolutionOperationList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SolutionOperationList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SolutionOperationList)DgListView.SelectedItem : new SolutionOperationList();
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;
            try {
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                cb_InheritedTypeName.SelectedItem = (selectedRecord.Id == 0) ? mixedEnumTypesList.FirstOrDefault() : mixedEnumTypesList.First(a => a.Name == selectedRecord.InheritedOperationTypes);
                txt_name.Text = selectedRecord.Name;
                txt_inputData.Text = selectedRecord.InputData;
                txt_description.Text = selectedRecord.Description;

                cb_inheritedResultTypeName.SelectedItem = (selectedRecord.Id == 0) ? mixedEnumResultTypesList.FirstOrDefault() : mixedEnumResultTypesList.First(a => a.Name == selectedRecord.InheritedApiResultTypes);
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
        }
    }
}