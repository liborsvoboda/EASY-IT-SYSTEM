using EASYTools.SqlConnectionDialog;
using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// This is Template ListView + UserForm
namespace EasyITSystemCenter.Pages {

    public partial class SystemReportQueueListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SystemReportQueueList selectedRecord = new SystemReportQueueList();

        private string connectionString = null;
        private List<GenericDataList> systemTableList = new List<GenericDataList>();
        private List<SystemTranslatedTableList> systemTranslatedTableList = new List<SystemTranslatedTableList>();
        private List<SystemReportQueueList> reportQueueList = new List<SystemReportQueueList>();
        private bool reportSupportForListOnly = true;

        public SystemReportQueueListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            LoadParameters();
            _ = LoadDataList();
            SetRecord(false);
        }

        private async void LoadParameters() {
            DgListView.RowHeight = double.Parse(await DataOperations.ParameterCheck("ReportSqlRowHeight"));
            reportSupportForListOnly = bool.Parse(await DataOperations.ParameterCheck("ReportSupportForListOnly"));
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                reportQueueList = await CommunicationManager.GetApiRequest<List<SystemReportQueueList>>(ApiUrls.EasyITCenterSystemReportQueueList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                systemTableList = await CommunicationManager.GetApiRequest<List<GenericDataList>>(ApiUrls.ServerApi, "DatabaseServices/SpGetTableList", App.UserData.Authentification.Token);


                systemTranslatedTableList.Clear();
                systemTableList.ForEach(async table => {
                    systemTranslatedTableList.Add(new SystemTranslatedTableList() { TableName = table.Data, Translate = await DBOperations.DBTranslation(table.Data) });
                });
                reportQueueList.ForEach(rq => { rq.TranslatedTableName = systemTranslatedTableList.FirstOrDefault(a => a.TableName == rq.TableName).Translate; });

                cb_tableName.ItemsSource = systemTranslatedTableList.OrderBy(a => a.Translate);
                DgListView.ItemsSource = reportQueueList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        // set translate columns in listView
        private void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(e => {
                    string headername = e.Header.ToString();
                    if (headername == "Value") { e.Header = Resources["fname"].ToString(); e.DisplayIndex = 1; }
                    else if (headername == "TranslatedTableName") { e.Header = Resources["tableName"].ToString(); e.DisplayIndex = 2; }
                    else if (headername == "Sequence") { e.Header = Resources["sequence"].ToString(); e.DisplayIndex = 3; }
                    else if (headername == "Sql") e.Header = Resources["sqlCommand"].ToString();
                    else if (headername == "SearchColumnList") e.Header = Resources["searchColumnList"].ToString();
                    else if (headername == "SearchFilterIgnore") { e.Header = Resources["searchFilterIgnore"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                    else if (headername == "RecIdFilterIgnore") { e.Header = Resources["recIdFilterIgnore"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; }
                    else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "Filter") e.Visibility = Visibility.Hidden;
                    else if (headername == "Search") e.Visibility = Visibility.Hidden;
                    else if (headername == "RecId") e.Visibility = Visibility.Hidden;
                    else if (headername == "TableName") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        //change filter fields
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SystemReportQueueList user = e as SystemReportQueueList;
                    return user.Name.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Sql) && user.Sql.ToLower().Contains(filter.ToLower())
                    || user.TranslatedTableName.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.SearchColumnList) && user.SearchColumnList.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new SystemReportQueueList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SystemReportQueueList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SystemReportQueueList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterSystemReportQueueList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                _ = LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SystemReportQueueList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) {
                selectedRecord = (SystemReportQueueList)DgListView.SelectedItem;
                dataViewSupport.SelectedRecordId = selectedRecord.Id;
                SetRecord(false);
            }
            else {
                selectedRecord = new SystemReportQueueList();
                SetRecord(false);
            }
        }

        //change dataset save method
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.Name = txt_name.Text;
                selectedRecord.Sequence = int.Parse(txt_sequence.Value.ToString());
                selectedRecord.Sql = txt_sql.Text;
                selectedRecord.TableName = ((SystemTranslatedTableList)cb_tableName.SelectedItem).TableName;
                selectedRecord.SearchColumnList = txt_searchColumnList.Text;
                selectedRecord.SearchFilterIgnore = (bool)chb_searchFilterIgnore.IsChecked;
                selectedRecord.RecIdFilterIgnore = (bool)chb_recIdFilterIgnore.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterSystemReportQueueList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterSystemReportQueueList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SystemReportQueueList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SystemReportQueueList)DgListView.SelectedItem : new SystemReportQueueList();
            SetRecord(false);
        }

        //change dataset prepare for working
        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;

            txt_name.Text = selectedRecord.Name;
            txt_sequence.Value = selectedRecord.Sequence;
            txt_sql.Text = selectedRecord.Sql;

            cb_tableName.Text = (selectedRecord.Id == 0) ? null : selectedRecord.TranslatedTableName;
            txt_searchColumnList.Text = selectedRecord.SearchColumnList;
            chb_searchFilterIgnore.IsChecked = selectedRecord.SearchFilterIgnore;
            chb_recIdFilterIgnore.IsChecked = selectedRecord.RecIdFilterIgnore;

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e) {
            var factory = new ConnectionStringFactory();
            connectionString = factory.BuildConnectionString();

            if (!string.IsNullOrWhiteSpace(connectionString)) {
                btn_execute.IsEnabled = true;
            }
        }

        private void Connection_StateChange(object sender, StateChangeEventArgs e) {
            if (e.CurrentState == ConnectionState.Open) {
                btn_connect.Content = Resources["disconnect"].ToString(); btn_execute.IsEnabled = true;
            }
            else { btn_connect.Content = Resources["connectSql"].ToString(); }
        }

        private async void Execute_Click(object sender, RoutedEventArgs e) {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                SqlCommand cmd;
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    cmd = new SqlCommand(txt_sql.Text, connection);
                    DataTable table = new DataTable();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    table.Load(cmd.ExecuteReader());
                    DgSubListView.ItemsSource = table.DefaultView;
                    DgSubListView.Items.Refresh();
                }
                MainWindow.ProgressRing = Visibility.Visible;
            } catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, ex.Message + Environment.NewLine + ex.StackTrace); } finally { MainWindow.ProgressRing = Visibility.Hidden; }
        }
    }
}