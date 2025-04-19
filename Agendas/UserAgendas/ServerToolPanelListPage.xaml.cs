using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalGenerators;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EasyITSystemCenter.Pages {

    public partial class ServerToolPanelListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ServerToolPanelDefinitionList selectedRecord = new ServerToolPanelDefinitionList();

        private List<ServerToolPanelDefinitionList> ServerToolPanelDefinitionList = new List<ServerToolPanelDefinitionList>();
        private List<ServerToolTypeList> ServerToolTypeList = new List<ServerToolTypeList>();

        public ServerToolPanelListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            _ = LoadDataList();
            SetRecord(false);
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                ServerToolTypeList = await CommunicationManager.GetApiRequest<List<ServerToolTypeList>>(ApiUrls.EasyITCenterServerToolTypeList, null, App.UserData.Authentification.Token);
                ServerToolPanelDefinitionList = await CommunicationManager.GetApiRequest<List<ServerToolPanelDefinitionList>>(ApiUrls.EasyITCenterServerToolPanelDefinitionList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                //Generate Menu Panel
                if (ServerToolTypeList.Any()) { TabMenuList.Items.Clear(); }
                ServerToolTypeList.ForEach(async toolType => {
                    try {
                        string translation = await DBOperations.DBTranslation(toolType.Name);
                        WrapPanel tabMenuPanel = new WrapPanel() { Name = "wp_" + toolType.Id.ToString(), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                        TabItem tabMenu = new TabItem() { Name = Regex.Replace(toolType.Name, @"[^a-zA-Z]", "_"), Header = translation, Content = tabMenuPanel };
                        TabMenuList.Items.Add(tabMenu);
                    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                });

                foreach (ServerToolPanelDefinitionList panel in ServerToolPanelDefinitionList) {
                    panel.ToolTypeName = Regex.Replace(ServerToolTypeList.FirstOrDefault(a => a.Id == panel.ToolTypeId).Name, @"[^a-zA-Z]", "_");
                    try {
                        panel.BitmapImage = IconMaker.Icon((Color)ColorConverter.ConvertFromString(panel.IconColor), App.SystemSvgIconList.FirstOrDefault(a => a.Name == panel.IconName).SvgIconPath);
                    } catch { }

                    var toolPanel = new Tile() {
                        Tag = panel.Id.ToString(),
                        Name = Regex.Replace(panel.SystemName, @"[^a-zA-Z]", "_"),
                        Title = await DBOperations.DBTranslation(panel.SystemName),
                        Background = (Brush)new BrushConverter().ConvertFromString(panel.BackgroundColor),
                        Width = 200,
                        Height = 200,
                        Margin = new Thickness(3),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Cursor = Cursors.Hand,
                        FontWeight = FontWeights.DemiBold,
                        HorizontalTitleAlignment = HorizontalAlignment.Left,
                        TitleFontSize = 20,
                        VerticalTitleAlignment = VerticalAlignment.Bottom,
                        ClickMode = ClickMode.Press,
                        ToolTip = (!string.IsNullOrWhiteSpace(panel.Description)) ? panel.Description : null
                    }; 
                    Image icon = new Image() { Width = 120, Height = 120, Source = panel.BitmapImage };
                    icon = (Image)EffectLibrary.GetAnimationEffect(icon, AnimationLibrary.RotationAndSizeEffect, EffectTypes.SizePulseEffect, TriggerTypes.OnMouseMove, 120, 2);

                    toolPanel.Content = icon;
                    toolPanel.Click += ToolPanelListPage_Click;
                    ((WrapPanel)TabMenuList.FindChild<TabItem>(Regex.Replace(ServerToolPanelDefinitionList.Where(a => a.Id == int.Parse(toolPanel.Tag.ToString())).First().ToolTypeName, @"[^a-zA-Z]", "_")).Content).Children.Add(toolPanel);
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private void ToolPanelListPage_Click(object sender, RoutedEventArgs e) {
            var selectedPanel = ServerToolPanelDefinitionList.Where(a => a.Id == int.Parse(((Tile)sender).Tag.ToString())).FirstOrDefault();
            SystemOperations.StartExternalProccess(selectedPanel.Type, (selectedPanel.Type == "ServerUrl" ? App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value : "") + selectedPanel.Command);
        }

        private void SetRecord(bool? showForm = null) {
            MainWindow.DataGridSelected = MainWindow.DataGridSelectedIdListIndicator = false; MainWindow.dataGridSelectedId = 0; MainWindow.DgRefresh = false;
            dataViewSupport.FormShown = true;
        }
    }
}