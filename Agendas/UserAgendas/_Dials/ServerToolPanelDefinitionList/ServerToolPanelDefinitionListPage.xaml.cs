using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalGenerators;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EasyITSystemCenter.Pages {

    public partial class ServerToolPanelDefinitionListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ServerToolPanelDefinitionList selectedRecord = new ServerToolPanelDefinitionList();

        public static ServerToolPanelDefinitionList generatedIcon = new ServerToolPanelDefinitionList() {
            SystemName = "",IconName = "star", IconColor = "RoyalBlue", BackgroundColor = "DarkSeaGreen", Description = "",
            BitmapImage = new BitmapImage(DataResources.GetImageResource("no_photo.png"))
        };

        private List<ServerToolPanelDefinitionList> toolPanelDefinitionList = new List<ServerToolPanelDefinitionList>();
        private List<SolutionMixedEnumList> inheritedToolLinkType = new List<SolutionMixedEnumList>();
        private List<ServerToolTypeList> toolTypeList = new List<ServerToolTypeList>();

        private bool pageLoaded = false;

        public ServerToolPanelDefinitionListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);

                LoadParameters();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            _ = LoadDataList();
            SetRecord(false);
        }

        private async void LoadParameters() {
            DgListView.RowHeight = int.Parse(await DataOperations.ParameterCheck("DialsFormsRowHeight"));
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                inheritedToolLinkType = await DBOperations.LoadInheritedDataList("ToolLinkType");
                inheritedToolLinkType.ForEach(async timeType => { timeType.Translation = await DBOperations.DBTranslation(timeType.Name); });


                toolPanelDefinitionList = await CommunicationManager.GetApiRequest<List<ServerToolPanelDefinitionList>>(ApiUrls.EasyITCenterServerToolPanelDefinitionList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                //svgIconList = await CommApi.GetApiRequest<List<SystemSvgIconList>>(ApiUrls.EasyITCenterSystemSvgIconList, null, App.UserData.Authentification.Token);
                toolTypeList = await CommunicationManager.GetApiRequest<List<ServerToolTypeList>>(ApiUrls.EasyITCenterServerToolTypeList, null, App.UserData.Authentification.Token);



                lv_iconViewer.Items.Clear();
                App.SystemSvgIconList.ForEach(icon => {
                    try {
                        icon.BitmapImage = IconMaker.Icon(Colors.RoyalBlue, icon.SvgIconPath);
                    } catch { }
                    lv_iconViewer.Items.Add(icon);
                });

                toolPanelDefinitionList.ForEach(async item => {
                    item.Translation = await DBOperations.DBTranslation(item.SystemName);
                    item.ToolTypeName = toolTypeList.First(a => a.Id == item.ToolTypeId).Name;
                });
                cb_type.ItemsSource = inheritedToolLinkType;
                cb_toolType.ItemsSource = toolTypeList;
                DgListView.ItemsSource = toolPanelDefinitionList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "SystemName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "Translation".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "ToolTypeName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "Type".ToLower()) e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "Command".ToLower()) e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "Description".ToLower()) e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "ToolTypeId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "BitmapImage".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "IconName".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "IconColor".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "BackgroundColor".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "SvgIconPath".ToLower()) e.Visibility = Visibility.Hidden;
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
            selectedRecord = new ServerToolPanelDefinitionList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (ServerToolPanelDefinitionList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (ServerToolPanelDefinitionList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterServerToolPanelDefinitionList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (ServerToolPanelDefinitionList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (ServerToolPanelDefinitionList)DgListView.SelectedItem; }
            else { selectedRecord = new ServerToolPanelDefinitionList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.SystemName = txt_systemName.Text;

                selectedRecord.ToolTypeId = ((ServerToolTypeList)cb_toolType.SelectedItem).Id;
                selectedRecord.InheritedToolLinkType = ((SolutionMixedEnumList)cb_type.SelectedItem).Name;
                selectedRecord.Command = txt_command.Text;
                selectedRecord.IconName = generatedIcon.IconName;
                selectedRecord.IconColor = xct_iconColor.SelectedColorText;
                selectedRecord.BackgroundColor = xct_backgroundColor.SelectedColorText;
                selectedRecord.Description = txt_description.Text;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterServerToolPanelDefinitionList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterServerToolPanelDefinitionList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new ServerToolPanelDefinitionList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (ServerToolPanelDefinitionList)DgListView.SelectedItem : new ServerToolPanelDefinitionList();
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;

            txt_systemName.Text = generatedIcon.SystemName = selectedRecord.SystemName;
            generatedIcon.IconName = selectedRecord.IconName;

            cb_toolType.SelectedItem = (selectedRecord.Id == 0 || toolTypeList.Count == 0) ? toolTypeList.FirstOrDefault() : toolTypeList.First(a => a.Id == selectedRecord.ToolTypeId);
            cb_type.SelectedItem = (selectedRecord.Id == 0) ? inheritedToolLinkType.FirstOrDefault() : inheritedToolLinkType.First(a => a.Value == selectedRecord.Type);
            txt_command.Text = selectedRecord.Command;

            if (selectedRecord.Id != 0) { generatedIcon.IconColor = selectedRecord.IconColor; xct_iconColor.SelectedColor = (Color)ColorConverter.ConvertFromString(selectedRecord.IconColor); }
            if (selectedRecord.Id != 0) { generatedIcon.BackgroundColor = selectedRecord.BackgroundColor; xct_backgroundColor.SelectedColor = (Color)ColorConverter.ConvertFromString(selectedRecord.BackgroundColor); }

            txt_description.Text = generatedIcon.Description = selectedRecord.Description;

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
                pageLoaded = true; GeneratedIconChanged();
            }
            else {
                pageLoaded = false;
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private void IconSelect_Click(object sender, SelectionChangedEventArgs e) {
            try {
                if (((ListView)sender).SelectedItems.Count > 0) {
                    generatedIcon.IconName = ((SystemSvgIconList)((ListView)sender).SelectedItem).Name;
                    GeneratedIconChanged();
                }
            } catch { }
        }

        private void ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e) {
            if (((Xceed.Wpf.Toolkit.ColorPicker)sender).Name == "xct_iconColor") { generatedIcon.IconColor = ((Xceed.Wpf.Toolkit.ColorPicker)sender).SelectedColorText; }
            else if (((Xceed.Wpf.Toolkit.ColorPicker)sender).Name == "xct_backgroundColor") { generatedIcon.BackgroundColor = ((Xceed.Wpf.Toolkit.ColorPicker)sender).SelectedColorText; }
            GeneratedIconChanged();
        }

        private void TextChanged(object sender, TextChangedEventArgs e) {
            if (((TextBox)sender).Name == "txt_systemName") {
                generatedIcon.SystemName = ((TextBox)sender).Text;
            }
            else if (((TextBox)sender).Name == "txt_command") {
                generatedIcon.Command = ((TextBox)sender).Text;
            }
            else if (((TextBox)sender).Name == "txt_description") { generatedIcon.Description = ((TextBox)sender).Text; }

            GeneratedIconChanged();
        }

        private async void GeneratedIconChanged() {
            try {
                if (pageLoaded) {
                    t_generatedIcon.Title = await DBOperations.DBTranslation(generatedIcon.SystemName, true);
                    t_generatedIcon.Foreground = (Brush)new BrushConverter().ConvertFromString(generatedIcon.IconColor);
                    t_generatedIcon.Background = (Brush)new BrushConverter().ConvertFromString(generatedIcon.BackgroundColor);
                    t_generatedIcon.ToolTip = generatedIcon.Description;
                    i_generatedIcon.Source = string.IsNullOrWhiteSpace(generatedIcon.IconName) ? new BitmapImage(DataResources.GetImageResource("no_photo.png"))
                        : IconMaker.Icon((Color)ColorConverter.ConvertFromString(generatedIcon.IconColor), App.SystemSvgIconList.Where(a => a.Name == generatedIcon.IconName).Select(a => a.SvgIconPath).First());
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCommandTest_Click(object sender, RoutedEventArgs e) {
            SystemOperations.StartExternalProccess(((TranslateSet)cb_type.SelectedItem).Value, (((TranslateSet)cb_type.SelectedItem).Value == "ServerUrl" ? App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value : "") + txt_command.Text);
        }
    }
}