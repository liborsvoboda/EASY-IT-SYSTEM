<UserControl
    x:Class="GoldenSystem.Pages.ServerToolPanelListPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mah="http://metro.mahapps.com/winfx/xaml/controls"
    xmlns:bh="http://schemas.microsoft.com/xaml/behaviors"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks"
    xmlns:local="clr-namespace:GoldenSystem.Pages"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:xctk="http://schemas.xceed.com/wpf/xaml/toolkit"
    Name="View"
    HorizontalAlignment="Stretch"
    VerticalAlignment="Stretch"
    d:DesignHeight="600"
    d:DesignWidth="900"
    Foreground="White"
    Tag="View"
    mc:Ignorable="d">

    <Grid
        Margin="0" HorizontalAlignment="Stretch" VerticalAlignment="Stretch">
        <Grid.Background>
            <RadialGradientBrush>
                <GradientStop Offset="0" Color="#FF313131" />
                <GradientStop Offset="1" Color="#FF181818" />
            </RadialGradientBrush>
        </Grid.Background>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>

        <ScrollViewer
            Grid.Row="0" Grid.Column="0"
            Width="{Binding ElementName=Settings, Path=ActualWidth}"
            Height="{Binding ElementName=Settings, Path=ActualHeight}"
            HorizontalAlignment="Stretch" VerticalAlignment="Stretch" HorizontalContentAlignment="Left" VerticalContentAlignment="Top" HorizontalScrollBarVisibility="Disabled"
            VerticalScrollBarVisibility="Auto">

            <TabControl
                x:Name="TabMenuList" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" HorizontalContentAlignment="Left" VerticalContentAlignment="Top"
                Background="Transparent" FontSize="10" TabStripPlacement="Left" />
        </ScrollViewer>
    </Grid>
</UserControl>

----------------------------------------------------------------------------------------------------------------
using ControlzEx.Standard;
using GoldenSystem.Api;
using GoldenSystem.Classes;
using GoldenSystem.GlobalGenerators;
using GoldenSystem.GlobalOperations;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
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


namespace GoldenSystem.Pages {

    public partial class ServerToolPanelListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static ServerToolPanelDefinitionList selectedRecord = new ServerToolPanelDefinitionList();

        private List<ServerToolPanelDefinitionList> ServerToolPanelDefinitionList = new List<ServerToolPanelDefinitionList>();
        private List<SystemSvgIconList> SystemSvgIconList = new List<SystemSvgIconList>();
        private List<ServerToolTypeList> ServerToolTypeList = new List<ServerToolTypeList>();


        public ServerToolPanelListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, JsonConvert.DeserializeObject<Language>(App.Setting.DefaultLanguage).Value);

            _ = LoadDataList();
            SetRecord(false);
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                ServerToolTypeList = await CommApi.GetApiRequest<List<ServerToolTypeList>>(ApiUrls.GoldenSystemServerToolTypeList, null, App.UserData.Authentification.Token);
                ServerToolPanelDefinitionList = await CommApi.GetApiRequest<List<ServerToolPanelDefinitionList>>(ApiUrls.GoldenSystemServerToolPanelDefinitionList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                SystemSvgIconList = await CommApi.GetApiRequest<List<SystemSvgIconList>>(ApiUrls.GoldenSystemSystemSvgIconList, null, App.UserData.Authentification.Token);

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
                        panel.BitmapImage = IconMaker.Icon((Color)ColorConverter.ConvertFromString(panel.IconColor), SystemSvgIconList.FirstOrDefault(a => a.Name == panel.IconName).SvgIconPath);
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

                    toolPanel.Content = icon;
                    toolPanel.Click += ToolPanelListPage_Click;
                    ((WrapPanel)TabMenuList.FindChild<TabItem>(Regex.Replace(ServerToolPanelDefinitionList.Where(a => a.Id == int.Parse(toolPanel.Tag.ToString())).First().ToolTypeName, @"[^a-zA-Z]", "_")).Content).Children.Add(toolPanel);
                }

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private void ToolPanelListPage_Click(object sender, RoutedEventArgs e) {
            var selectedPanel = ServerToolPanelDefinitionList.Where(a => a.Id == int.Parse(((Tile)sender).Tag.ToString())).FirstOrDefault();
            SystemOperations.StartExternalProccess(selectedPanel.Type, (selectedPanel.Type == "EDCservice" ? App.Setting.ApiAddress : "" ) + selectedPanel.Command);
        }

        private void SetRecord(bool showForm) {
            MainWindow.DataGridSelected = MainWindow.DataGridSelectedIdListIndicator = false; MainWindow.dataGridSelectedId = 0; MainWindow.DgRefresh = false;
            dataViewSupport.FormShown = true;
        }


    }
}