using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalGenerators;
using EasyITSystemCenter.GlobalOperations;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace EasyITSystemCenter.Pages {

    public partial class SolutionStaticFileListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static SolutionStaticFileList selectedRecord = new SolutionStaticFileList();

        private List<SolutionStaticFileList> solutionStaticFileList = new List<SolutionStaticFileList>();
        private List<SolutionWebsiteList> solutionWebsiteList = new List<SolutionWebsiteList>();
        private List<SystemSvgIconList> systemSvgIconList = new List<SystemSvgIconList>();
        private string actualPath = "/"; private string SelectedFile = null; private string SelectedFolder = null;

        public SolutionStaticFileListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            _ = LoadDataList();
        }

        //change datasource
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                solutionWebsiteList = await CommunicationManager.GetApiRequest<List<SolutionWebsiteList>>(ApiUrls.SolutionWebsiteList, null, App.UserData.Authentification.Token);
                systemSvgIconList = await CommunicationManager.GetApiRequest<List<SystemSvgIconList>>(ApiUrls.EasyITCenterSystemSvgIconList, null, App.UserData.Authentification.Token);
                solutionStaticFileList = await CommunicationManager.GetApiRequest<List<SolutionStaticFileList>>(ApiUrls.SolutionStaticFileList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
              
                cb_websiteName.ItemsSource = solutionWebsiteList; cb_websiteName.SelectedItem = solutionWebsiteList.FirstOrDefault();
                solutionStaticFileList = solutionStaticFileList.OrderBy(a => a.FileNamePath).ToList();

                await GenerateActualFolderView();                

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }




        private async Task<bool> GenerateActualFolderView() {
            MainWindow.ProgressRing = Visibility.Visible;

            //Prepare actual Folder List
            TabMenuList.Items.Clear();SelectedFile = null;SelectedFolder = null;
            List<string> folders = new List<string>() { "Actual" };
            solutionStaticFileList.ForEach(file => {
            if (file.FileNamePath.StartsWith("/" + solutionWebsiteList[0].WebsiteName)) {
                if (!folders.Contains(file.FileNamePath.Substring(1 + solutionWebsiteList[0].WebsiteName.Length + actualPath.Length).Split('/')[0])
                && file.FileNamePath.Substring(1 + solutionWebsiteList[0].WebsiteName.Length + actualPath.Length).Split('/').Length > 1
                    ) { folders.Add(file.FileNamePath.Substring(1 + solutionWebsiteList[0].WebsiteName.Length + actualPath.Length).Split('/')[0]); }
                }
            });

            //Generate Folder Menu
            folders.ForEach(path => {
                try {
                    WrapPanel tabMenuPanel = new WrapPanel() { Name = "wp_" + path, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };
                    TabItem tabMenu = new TabItem() { Name = Regex.Replace(path, @"[^a-zA-Z]", "_"), Header = path, Content = tabMenuPanel };
                    
                    TabMenuList.Items.Add(tabMenu);
                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            });

            //Generate File Tiles
            foreach (SolutionStaticFileList file in solutionStaticFileList.Where(a => a.FileNamePath.Substring(1 + solutionWebsiteList[0].WebsiteName.Length + actualPath.Length).Split('/').Length <= 2)) {
                var toolPanel = new Tile() {
                    Tag = file.Id.ToString(),
                    Name = Regex.Replace(file.FileNamePath.Split('/').Last(), @"[^a-zA-Z]", "_"),
                    Title = file.FileNamePath.Split('/').Last(),
                    Background = (Brush)new BrushConverter().ConvertFrom("#2B91C4"),
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(3),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Cursor = Cursors.Hand,
                    FontWeight = FontWeights.DemiBold,
                    HorizontalTitleAlignment = HorizontalAlignment.Left,
                    TitleFontSize = 20,
                    VerticalTitleAlignment = VerticalAlignment.Bottom,
                    ClickMode = ClickMode.Press,
                    ToolTip = "Size: " + (file.Content != null ? file.Content.Length : 0) * (1 / (1024 * 1024)) + " Mb" + Environment.NewLine + file.TimeStamp.ToLocalTime() + Environment.NewLine + "Path: " + file.FileNamePath
                };

                BitmapImage image = new BitmapImage();
                image = IconMaker.Icon((Color)ColorConverter.ConvertFromString("#C48C2B"), (systemSvgIconList.FirstOrDefault(a => a.Name.Contains(toolPanel.Name.Split('.').Last())) != null ? systemSvgIconList.FirstOrDefault(a => a.Name.Contains(toolPanel.Name.Split('.').Last())).SvgIconPath :systemSvgIconList.FirstOrDefault().SvgIconPath));
                Image icon = new Image() { Width = 50, Height = 50, Source = image }; toolPanel.Content = icon;

                toolPanel.Click += ToolPanelListPage_Click;

                string calculatePath = file.FileNamePath.Substring(1 + solutionWebsiteList[0].WebsiteName.Length + actualPath.Length).Split('/').Length <= 1 ? "Actual" : file.FileNamePath.Substring(1 + solutionWebsiteList[0].WebsiteName.Length + actualPath.Length).Split('/')[0];
                ((WrapPanel)TabMenuList.FindChild<TabItem>(Regex.Replace(calculatePath, @"[^a-zA-Z]", "_")).Content).Children.Add(toolPanel);
            }

            lbl_path.Content = await DBOperations.DBTranslation("Actual Path") + ": " + actualPath;
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }


        private async void ToolPanelListPage_Click(object sender, RoutedEventArgs e) {
            var selectedPanel = solutionStaticFileList.Where(a => a.Id == int.Parse(((Tile)sender).Tag.ToString())).FirstOrDefault();
            SelectedFile = selectedPanel.FileNamePath.Split('/').Last();
            lbl_selected.Content = await DBOperations.DBTranslation("Selected File") + ": " + selectedPanel.FileNamePath.Split('/').Last();
            btn_deleteSelectedFolder.IsEnabled = false;btn_deleteSelectedFile.IsEnabled = btn_exportSelectedFile.IsEnabled = true;
        }




            //change dataset save method
            private async void BtnApply_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;


              
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.SolutionStaticFileList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.SolutionStaticFileList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new SolutionStaticFileList();
                    await LoadDataList();
                }
                else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }




        private void BtnCreateNewFolder_Click(object sender, RoutedEventArgs e) {

        }

        private void BtnDeleteSelectedFolder_Click(object sender, RoutedEventArgs e) {

        }

        private void BtnInsertNewFiles_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog {
                    DefaultExt = ".*",
                    Filter = "All files |*.*",
                    Title = Resources["fileOpenDescription"].ToString(), 
                    Multiselect = true
                };
                if (dlg.ShowDialog() == true) {
                    selectedRecord.MimeType = MimeMapping.GetMimeMapping(dlg.FileName);
                    selectedRecord.Content = System.IO.File.ReadAllBytes(dlg.FileName);
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnDeleteSelectedFile_Click(object sender, RoutedEventArgs e) {

        }

        private async void TabMenuSelectionChanged(object sender, SelectionChangedEventArgs e) => TabMenuSeledted(sender);
        private async void TabMenuSelectionClick(object sender, MouseButtonEventArgs e) => TabMenuSeledted(sender);
        private async void TabMenuSeledted(object sender) {
            if ((TabItem)((TabControl)sender).SelectedItem != null) {
                lbl_selected.Content = await DBOperations.DBTranslation("Selected Folder") + ": " + ((TabItem)((TabControl)sender).SelectedItem).Name;
                SelectedFolder = ((TabItem)((TabControl)sender).SelectedItem).Name;
                btn_deleteSelectedFile.IsEnabled = btn_exportSelectedFile.IsEnabled = false; btn_deleteSelectedFolder.IsEnabled = true;
            }
        }

        private void BtnExportSelectedFile_Click(object sender, RoutedEventArgs e) {
            try {
                SaveFileDialog dlg = new SaveFileDialog { DefaultExt =  ".*", Filter = "All files |*.*", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) { FileOperations.ByteArrayToFile(SelectedFile, solutionStaticFileList.FirstOrDefault(a => a.FileNamePath.EndsWith(SelectedFile)).Content); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }
    }
}