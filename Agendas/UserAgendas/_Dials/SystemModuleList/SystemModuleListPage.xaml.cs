using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalGenerators;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EasyITSystemCenter.Pages {

    public partial class SystemModuleListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SystemModuleList selectedRecord = new SystemModuleList();

        public static SystemModuleList generatedIcon = new SystemModuleList() {
            Name = "",
            IconName = "star",
            IconColor = "RoyalBlue",
            BackgroundColor = "DarkSeaGreen",
            Description = ""
            ,
            BitmapImage = new BitmapImage(DataResources.GetImageResource())
        };

        private List<SystemModuleList> systemModuleList = new List<SystemModuleList>();
        private List<SolutionMixedEnumList> mixedEnumTypesList = new List<SolutionMixedEnumList>();

        private bool pageLoaded = false;

        public SystemModuleListPage() {

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

                mixedEnumTypesList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/SystemModules", App.UserData.Authentification.Token);
                systemModuleList = await CommunicationManager.GetApiRequest<List<SystemModuleList>>(ApiUrls.SystemModuleList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                lv_iconViewer.Items.Clear();
                App.SystemSvgIconList.ForEach(icon => {
                    try {
                        icon.BitmapImage = IconMaker.Icon(Colors.RoyalBlue, icon.SvgIconPath);
                    } catch { }
                    lv_iconViewer.Items.Add(icon);
                });

                mixedEnumTypesList.ForEach(async item => { item.Translation = await DBOperations.DBTranslation(item.Name); });
                systemModuleList.ForEach(item => { item.ModuleTypeName = mixedEnumTypesList.First(a=>a.Name == item.ModuleType).Translation; });


                cb_moduleType.ItemsSource = mixedEnumTypesList;
                DgListView.ItemsSource = systemModuleList;
                DgListView.Items.Refresh();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "ModuleTypeName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "Value".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "FileName".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "StartupCommand".ToLower()) e.Header = await DBOperations.DBTranslation(headername);
                    else if (headername == "TimeStamp".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    
                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "ModuleType".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "Description".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "ForegroundColor".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "BackgroundColor".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "BackgroundColor".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "IconName".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "IconColor".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "BitmapImage".ToLower()) e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    SystemModuleList user = e as SystemModuleList;
                    return user.Name.ToLower().Contains(filter.ToLower())
                    || user.ModuleTypeName.ToLower().Contains(filter.ToLower())
                    || user.FileName.ToLower().Contains(filter.ToLower())
                    || user.StartupCommand.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower());
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new SystemModuleList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (SystemModuleList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (SystemModuleList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.SystemModuleList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (SystemModuleList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (SystemModuleList)DgListView.SelectedItem; }
            else { selectedRecord = new SystemModuleList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                selectedRecord.Name = txt_name.Text;

                selectedRecord.ModuleType = ((SolutionMixedEnumList)cb_moduleType.SelectedItem).Name;
                selectedRecord.FolderPath = txt_folderPath.Text;
                selectedRecord.FileName = txt_fileName.Text;
                selectedRecord.StartupCommand = txt_startupCommand.Text;
                selectedRecord.Description = txt_description.Text;

                selectedRecord.IconName = generatedIcon.IconName;
                selectedRecord.ForegroundColor = xct_foregroundColor.SelectedColorText;
                selectedRecord.BackgroundColor = xct_backgroundColor.SelectedColorText;
                selectedRecord.IconColor = xct_iconColor.SelectedColorText;

                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.SystemModuleList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.SystemModuleList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SystemModuleList();
                    await LoadDataList();
                    SetRecord(null);
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (SystemModuleList)DgListView.SelectedItem : new SystemModuleList();
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;

            txt_name.Text = generatedIcon.Name = selectedRecord.Name;
            generatedIcon.IconName = selectedRecord.IconName;

            txt_folderPath.Text = selectedRecord.FolderPath;
            txt_fileName.Text = selectedRecord.FileName;
            txt_startupCommand.Text = selectedRecord.StartupCommand;
            txt_description.Text = selectedRecord.Description;

            cb_moduleType.SelectedItem = (selectedRecord.Id == 0 || mixedEnumTypesList.Count == 0) ? mixedEnumTypesList.FirstOrDefault() : mixedEnumTypesList.First(a => a.Name == selectedRecord.ModuleType);
            txt_startupCommand.Text = selectedRecord.StartupCommand;

            if (selectedRecord.Id != 0) { generatedIcon.ForegroundColor = selectedRecord.ForegroundColor; xct_foregroundColor.SelectedColor = (Color)ColorConverter.ConvertFromString(selectedRecord.ForegroundColor); }
            if (selectedRecord.Id != 0) { generatedIcon.BackgroundColor = selectedRecord.BackgroundColor; xct_backgroundColor.SelectedColor = (Color)ColorConverter.ConvertFromString(selectedRecord.BackgroundColor); }
            if (selectedRecord.Id != 0) { generatedIcon.IconColor = selectedRecord.IconColor; xct_iconColor.SelectedColor = (Color)ColorConverter.ConvertFromString(selectedRecord.IconColor); }

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
            if (((TextBox)sender).Name == "txt_name") {
                generatedIcon.Name = ((TextBox)sender).Text;
            }
            else if (((TextBox)sender).Name == "txt_startupCommand") {
                generatedIcon.StartupCommand = ((TextBox)sender).Text;
            }
            else if (((TextBox)sender).Name == "txt_description") { generatedIcon.Description = ((TextBox)sender).Text; }

            GeneratedIconChanged();
        }

        private async void GeneratedIconChanged() {
            if (pageLoaded) {
                t_generatedIcon.Title = await DBOperations.DBTranslation(generatedIcon.Name, true);
                t_generatedIcon.Foreground = (Brush)new BrushConverter().ConvertFromString(generatedIcon.IconColor);
                t_generatedIcon.Background = (Brush)new BrushConverter().ConvertFromString(generatedIcon.BackgroundColor);
                t_generatedIcon.ToolTip = generatedIcon.Description;
                i_generatedIcon.Source = string.IsNullOrWhiteSpace(generatedIcon.IconName) ? new BitmapImage(DataResources.GetImageResource())
                    : IconMaker.Icon((Color)ColorConverter.ConvertFromString(generatedIcon.IconColor), App.SystemSvgIconList.Where(a => a.Name == generatedIcon.IconName).Select(a => a.SvgIconPath).First());
            }
        }

    }
}