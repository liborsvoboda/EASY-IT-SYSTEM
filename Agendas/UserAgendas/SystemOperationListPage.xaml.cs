using ControlzEx.Standard;
using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalGenerators;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;


namespace EasyITSystemCenter.Pages {

    public partial class SystemOperationListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ServerToolPanelDefinitionList selectedRecord = new ServerToolPanelDefinitionList();

        private List<SolutionMixedEnumList> mixedEnumTypesList = new List<SolutionMixedEnumList>();
        private List<SolutionOperationList> solutionOperationList = new List<SolutionOperationList>();
        public List<Image> animatedIconList = new List<Image>();

        public SystemOperationListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            _ = LoadDataList();
            SetRecord(false);
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                mixedEnumTypesList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/OperationTypes", App.UserData.Authentification.Token);
                solutionOperationList = await CommunicationManager.GetApiRequest<List<SolutionOperationList>>(ApiUrls.EasyITCenterSolutionOperationList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                //Generate Menu Panel
                TabMenuList.Items.Clear();
                mixedEnumTypesList.ForEach(async operationType => {
                    try {
                        WrapPanel tabMenuPanel = new WrapPanel() { Name = "wp_" + operationType.Id.ToString(), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        TabItem tabMenu = new TabItem() { Name = Regex.Replace(operationType.Name, @"[^a-zA-Z]", "_"), Header = await DBOperations.DBTranslation(operationType.Name), Content = tabMenuPanel };
                        TabMenuList.Items.Add(tabMenu);
                    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                });

                foreach (SolutionOperationList panel in solutionOperationList) {
                    var toolPanel = new Tile() {
                        Tag = panel.Id.ToString(),
                        Name = Regex.Replace(panel.Name, @"[^a-zA-Z]", "_"),
                        Title = await DBOperations.DBTranslation(panel.Name),
                        Background = (Brush)new BrushConverter().ConvertFrom("#2B91C4"),
                        Width = 200,
                        Height = 200,
                        Margin = new Thickness(3),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Top,
                        Cursor = Cursors.Hand,
                        FontWeight = FontWeights.DemiBold,
                        HorizontalTitleAlignment = HorizontalAlignment.Left,
                        TitleFontSize = 20,
                        VerticalTitleAlignment = VerticalAlignment.Bottom,
                        ClickMode = ClickMode.Press,
                        ToolTip = (!string.IsNullOrWhiteSpace(panel.Description)) ? panel.Description : null
                    };

                    BitmapImage spinner = new BitmapImage();
                    switch (panel.InheritedOperationTypes) {
                        case "DbSpDirectOperation":
                            spinner = IconMaker.Icon((Color)ColorConverter.ConvertFromString("#C48C2B"), App.SystemSvgIconList.FirstOrDefault(a => a.Name.ToLower() == "spinner").SvgIconPath);
                            break;

                        case "DbSpConfigOperation":
                            spinner = IconMaker.Icon((Color)ColorConverter.ConvertFromString("#C48C2B"), App.SystemSvgIconList.FirstOrDefault(a => a.Name.ToLower() == "spinner2").SvgIconPath);
                            break;

                        case "ServerApiGetRequest":
                            spinner = IconMaker.Icon((Color)ColorConverter.ConvertFromString("#C48C2B"), App.SystemSvgIconList.FirstOrDefault(a => a.Name.ToLower() == "spinner4").SvgIconPath);
                            break;

                        case "ServerApiConfigRequest":
                            spinner = IconMaker.Icon((Color)ColorConverter.ConvertFromString("#C48C2B"), App.SystemSvgIconList.FirstOrDefault(a => a.Name.ToLower() == "spinner5").SvgIconPath);
                            break;

                        default:
                            spinner = IconMaker.Icon((Color)ColorConverter.ConvertFromString("#C48C2B"), App.SystemSvgIconList.FirstOrDefault(a => a.Name.ToLower() == "spinner3").SvgIconPath);
                            break;
                    }

                    double iconSize = 120;//Get StoryBoard Type From Animation Library
                    Image icon = new Image() { Width = iconSize, Height = iconSize, Source = spinner, VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch };
                    icon = (Image)EffectLibrary.GetAnimationEffect(icon, AnimationLibrary.RotationAndSizeEffect, EffectTypes.RotationEffect, TriggerTypes.OnMouseMove, 360, 2);

                    animatedIconList.Add(icon); toolPanel.Content = icon; toolPanel.Click += ToolPanelListPage_Click;
                    ((WrapPanel)TabMenuList.FindChild<TabItem>(Regex.Replace(solutionOperationList.Where(a => a.Id == int.Parse(toolPanel.Tag.ToString())).First().InheritedOperationTypes, @"[^a-zA-Z]", "_")).Content).Children.Add(toolPanel);
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void ToolPanelListPage_Click(object sender, RoutedEventArgs e) {
            MainWindow.progressRing = Visibility.Visible;
            SolutionOperationList selectedPanel = solutionOperationList.Where(a => a.Id == int.Parse(((Tile)sender).Tag.ToString())).FirstOrDefault();
            List<CustomMessageList> messageResponse = null;
            List<GenericValue> jsonResponse = null; string json = null;

            try { //Request
                switch (selectedPanel.InheritedOperationTypes) {
                    case "DbSpDirectOperation":
                        if (selectedPanel.InheritedApiResultTypes == "Message") {
                            messageResponse = await CommunicationManager.GetApiRequest<List<CustomMessageList>>(ApiUrls.ServerApi, "DatabaseServices/SpProcedure/Message/" + selectedPanel.InputData, App.UserData.Authentification.Token);
                            json = messageResponse[0].MessageList;
                        }
                        else {
                            jsonResponse = await CommunicationManager.GetApiRequest<List<GenericValue>>(ApiUrls.ServerApi, "DatabaseServices/SpProcedure/File/" + selectedPanel.InputData, App.UserData.Authentification.Token);
                            JavaScriptSerializer serializer = new JavaScriptSerializer(); json = "{";
                            jsonResponse.ForEach(key => {
                                DeserializedJson jsonObject = serializer.Deserialize<DeserializedJson>(key.Value);
                                bool isNumber = int.TryParse(jsonObject.Value, out int res);
                                json += (json != "{" ? "," : "") + "\"" + jsonObject.Key + "\":" + (!isNumber ? "\"" + jsonObject.Value + "\"" : jsonObject.Value);
                            }); json += "}";
                        }
                        break;
                    case "DbSpConfigOperation":
                        json = await CommunicationManager.ApiManagerGetRequest(UrlSourceTypes.EicWebServerAuth, ApiUrls.ServerApi + "/DatabaseServices/SpGetProcedureParams/" + selectedPanel.InputData);
                        break;
                    case "ServerApiGetRequest":
                        json = await CommunicationManager.ApiManagerGetRequest(UrlSourceTypes.EicWebServerAuth, selectedPanel.InputData);
                        break;
                    case "ServerApiConfigRequest":
                        break;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.progressRing = Visibility.Hidden;
            try {
               
                //Response
                switch (selectedPanel.InheritedApiResultTypes) {
                    case "Message":
                        if (string.IsNullOrWhiteSpace(json)) { await MainWindow.ShowMessageOnMainWindow(true, await DBOperations.DBTranslation("EmptyResponse")); }
                        else { await MainWindow.ShowMessageOnMainWindow(false, json); }
                        break;

                    case "File":
                        try {
                            if (string.IsNullOrWhiteSpace(json)) { await MainWindow.ShowMessageOnMainWindow(true, await DBOperations.DBTranslation("EmptyResponse")); }
                            else {
                                SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".*", Filter = "All files |*.*", Title = Resources["fileOpenDescription"].ToString() };
                                if (dlg.ShowDialog() == true) { FileOperations.WriteToFile(dlg.FileName, json); }
                            }
                        } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                        break;
                    case "DataSet":
                        try {
                            if (string.IsNullOrWhiteSpace(json)) { await MainWindow.ShowMessageOnMainWindow(true, await DBOperations.DBTranslation("EmptyResponse")); }
                            else {
                                //TODO Generate popup from DATASET

                            }
                        } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                        break;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void SetRecord(bool? showForm = null) {
            MainWindow.DataGridSelected = MainWindow.DataGridSelectedIdListIndicator = false; MainWindow.dataGridSelectedId = 0; MainWindow.DgRefresh = false;
            dataViewSupport.FormShown = true;
        }


        private void BtnRunPopup_Click(object sender, RoutedEventArgs e) {

        }


        private void BtnCancelPopup_Click(object sender, RoutedEventArgs e) {
            pop_configOperationRequest.Visibility = Visibility.Hidden;
        }

    }
}