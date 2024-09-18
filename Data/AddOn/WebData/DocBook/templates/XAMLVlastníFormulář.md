<UserControl
    x:Class="GoldenSystem.Pages.TemplateSettingsPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mah="http://metro.mahapps.com/winfx/xaml/controls"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    d:DesignHeight="500"
    d:DesignWidth="600"
    Tag="Setting"
    mc:Ignorable="d">
    <UserControl.Resources>
        <Thickness x:Key="ControlMargin">0 5 0 0</Thickness>
        <Style
            x:Key="NormalCaseColumnHeader"
            BasedOn="{StaticResource MetroDataGridColumnHeader}"
            TargetType="{x:Type DataGridColumnHeader}">
            <Setter Property="mah:ControlsHelper.ContentCharacterCasing" Value="Normal" />
        </Style>
    </UserControl.Resources>

    <Grid Name="configuration">
        <Grid
            Width="Auto"
            Height="Auto"
            Margin="0,0,0,0"
            HorizontalAlignment="Stretch"
            VerticalAlignment="Stretch"
            ForceCursor="False"
            ShowGridLines="False">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="300" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
                <RowDefinition />
                <RowDefinition Height="110" />
            </Grid.RowDefinitions>

            <Label
                x:Name="lbl_apiAddress"
                Grid.Row="0"
                Grid.Column="0"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <TextBox
                x:Name="txt_apiAddress"
                Grid.Row="0"
                Grid.Column="1"
                Margin="0,2,160,2"
                mah:TextBoxHelper.ClearTextButton="true"
                mah:TextBoxHelper.IsWaitingForData="True"
                TextChanged="ApiAddress_TextChanged"
                ToolTip="Default is http://127.0.0.1:5000" />

            <Button
                x:Name="btnApiTest"
                Grid.Row="0"
                Grid.Column="1"
                Width="150"
                Height="25"
                Margin="0,2,5,0"
                Padding="5,4,5,5"
                HorizontalAlignment="Right"
                VerticalAlignment="Top"
                
                Click="BtnApiTest_Click"
                IsEnabled="False"
                Style="{DynamicResource AccentedSquareButtonStyle}">
                <Button.ToolTip>
                    <TextBlock>
                        Test API connection<LineBreak /></TextBlock>
                </Button.ToolTip>
            </Button>

            <Label
                x:Name="lbl_serviceName"
                Grid.Row="1"
                Grid.Column="0"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <TextBox
                x:Name="txt_serviceName"
                Grid.Row="1"
                Grid.Column="1"
                Margin="0,2,0,2"
                mah:TextBoxHelper.ClearTextButton="true"
                mah:TextBoxHelper.IsWaitingForData="True"
                mah:TextBoxHelper.Watermark=""
                ToolTip="Service name from windows processes" />

            <Label
                x:Name="lbl_writeToLog"
                Grid.Row="2"
                Grid.Column="0"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <CheckBox
                x:Name="chb_WritetoLog"
                Grid.Row="2"
                Grid.Column="1"
                VerticalAlignment="Center" />

            <Label
                x:Name="lbl_serverCheckIntervalSec"
                Grid.Row="3"
                Grid.Column="0"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <mah:NumericUpDown
                x:Name="txt_serverCheckIntervalSec"
                Grid.Row="3"
                Grid.Column="1"
                Margin="0,2,0,2"
                HorizontalContentAlignment="Left"
                mah:TextBoxHelper.ClearTextButton="true"
                mah:TextBoxHelper.Watermark=""
                Maximum="99"
                Minimum="1" />

            <Label
                x:Name="lbl_topMost"
                Grid.Row="4"
                Grid.Column="0"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <CheckBox
                x:Name="chb_topMost"
                Grid.Row="4"
                Grid.Column="1"
                VerticalAlignment="Center" />

            <Label
                x:Name="lbl_activeNewInputDefault"
                Grid.Row="5"
                Grid.Column="0"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <CheckBox
                x:Name="chb_activeNewInputDefault"
                Grid.Row="5"
                Grid.Column="1"
                VerticalAlignment="Center" />

            <Label
                x:Name="lbl_defaultLanguage"
                Grid.Row="6"
                Grid.Column="0"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <ComboBox
                Name="cb_defaultLanguage"
                Grid.Row="6"
                Grid.Column="1"
                Margin="0,2,0,2"
                HorizontalAlignment="Stretch"
                VerticalAlignment="Center"
                mah:TextBoxHelper.ClearTextButton="false"
                DisplayMemberPath="Name"
                IsEnabled="true"
                ItemsSource="{Binding Path=Languages}"
                SelectedValuePath="Value" />

            <Label
                x:Name="lbl_showVerticalPanel"
                Grid.Row="7"
                Grid.Column="0"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <CheckBox
                x:Name="chb_showVerticalPanel"
                Grid.Row="7"
                Grid.Column="1"
                VerticalAlignment="Center" />

            <Label
                x:Name="lbl_powerBuilderPath"
                Grid.Row="8"
                Grid.Column="0"
                Width="140"
                HorizontalAlignment="Right"
                HorizontalContentAlignment="Right" />
            <TextBox
                x:Name="txt_powerBuilderPath"
                Grid.Row="8"
                Grid.Column="1"
                Margin="0,2,85,0"
                mah:TextBoxHelper.SelectAllOnFocus="True"
                mah:TextBoxHelper.Watermark="Enter path with PowerBuilder installed exe file"
                IsEnabled="False" />
            <Button
                x:Name="btn_browse"
                Grid.Row="8"
                Grid.Column="1"
                Width="80"
                Height="25"
                Margin="0,2,0,0"
                HorizontalAlignment="Right"
                VerticalAlignment="Top"
                
                Click="BtnBrowse_Click"
                Content="Browse"
                Style="{DynamicResource AccentedSquareButtonStyle}">
                <Button.ToolTip>
                    <TextBlock>Select the PowerBuilder installed exe file</TextBlock>
                </Button.ToolTip>
            </Button>

            <Button
                x:Name="btn_save"
                Grid.Row="10"
                Grid.Column="0"
                Width="200"
                Height="40"
                Margin="44,21,0,44"
                HorizontalAlignment="Left"
                VerticalAlignment="Bottom"
                
                Click="BtnSave_Click"
                Style="{DynamicResource AccentedSquareButtonStyle}" />
            <Button
                x:Name="btn_restart"
                Grid.Row="10"
                Grid.Column="1"
                Width="200"
                Height="40"
                Margin="44,21,44,44"
                HorizontalAlignment="Right"
                VerticalAlignment="Bottom"
                Click="BtnRestart_Click"
                Style="{DynamicResource AccentedSquareButtonStyle}" />
        </Grid>
    </Grid>
</UserControl>
----------------------------------------------------------------------------------------------------------------

using GoldenSystem.Api;
using GoldenSystem.Classes;
using GoldenSystem.GlobalOperations;
using Microsoft.Win32;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

// This is Template Is Customized For Load and Save data without List (Settings) is folded from standartized Methods
namespace GoldenSystem.Pages {

    public partial class TemplateSettingsPage : UserControl {

        /// <summary>
        /// Standartized declaring static page data for global vorking with pages
        /// </summary>
        public static DataViewSupport dataViewSupport = new DataViewSupport();

        public static TemplateClassList selectedRecord = new TemplateClassList();

        /// <summary>
        /// Define Collection For Combobox
        /// </summary>
        public ObservableCollection<Language> Languages = new ObservableCollection<Language>() {
                                                                new Language() { Name = "System", Value = "system" },
                                                                new Language() { Name = "Czech", Value = "cs-CZ" },
                                                                new Language() { Name = "English", Value = "en-US" }
                                                             };

        /// <summary>
        /// Initialize page with loading Dictionary and start loding data
        /// Manual work needed Translate All XAML fields by Resources
        /// Runned on start
        ///
        /// </summary>
        public TemplateSettingsPage() {
            InitializeComponent();
            Language defaultLanguage = JsonConvert.DeserializeObject<Language>(App.Setting.DefaultLanguage);
            _ = SystemOperations.SetLanguageDictionary(Resources, defaultLanguage.Value);
            try
            {
                lbl_apiAddress.Content = Resources["apiAddress"].ToString();
                lbl_serviceName.Content = Resources["serviceName"].ToString();
                lbl_writeToLog.Content = Resources["logging"].ToString();
                lbl_serverCheckIntervalSec.Content = Resources["serverCheckIntervalSec"].ToString();
                lbl_topMost.Content = Resources["topMost"].ToString();
                lbl_activeNewInputDefault.Content = Resources["activeNewInputDefault"].ToString();
                lbl_defaultLanguage.Content = Resources["defaultLanguage"].ToString();
                lbl_showVerticalPanel.Content = Resources["showVerticalPanel"].ToString();
                lbl_powerBuilderPath.Content = Resources["powerBuilderPath"].ToString();

                btn_browse.Content = Resources["browse"].ToString();
                btn_restart.Content = Resources["restart"].ToString();
                btn_save.Content = Resources["btn_save"].ToString();
                btnApiTest.Content = Resources["apiConnectionTest"].ToString();
            }
            catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            //data
            txt_apiAddress.Text = App.Setting.ApiAddress;
            chb_WritetoLog.IsChecked = App.Setting.WriteToLog;
            txt_serverCheckIntervalSec.Value = App.Setting.ServerCheckIntervalSec * 0.001;
            chb_topMost.IsChecked = App.Setting.TopMost;
            chb_activeNewInputDefault.IsChecked = App.Setting.ActiveNewInputDefault;
            txt_powerBuilderPath.Text = App.Setting.ReportingPath;

            cb_defaultLanguage.ItemsSource = Languages;

            int index = 0;
            cb_defaultLanguage.Items.SourceCollection.Cast<Language>().ToList().ForEach(language => { if (language.Name == defaultLanguage.Name) { cb_defaultLanguage.SelectedIndex = index; } index++; });
        }

        /// <summary>
        /// Customized GET Call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnApiTest_Click(object sender, RoutedEventArgs e) {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    string json = await httpClient.GetStringAsync(txt_apiAddress.Text + "/" + ApiUrls.GoldenSystemBackendCheck + "/Db");
                    await MainWindow.ShowMessageOnMainWindow(false, json);
                }
                catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + ex.StackTrace); }
            }
        }

        #region helper methods

        private class ExtensionsItem {

            public ExtensionsItem() {
                ident = null;
                isTrue = false;
                asn1OctetString = null;
            }

            public DerObjectIdentifier ident { get; set; }
            public bool isTrue { get; set; }
            public Asn1OctetString asn1OctetString { get; set; }
        }

        #endregion helper methods

        /// <summary>
        /// Standartized method for Direct Save Record
        /// Manual work needed, set correct data set for SubRecord
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            App.Setting.ApiAddress = txt_apiAddress.Text;
            App.Setting.WriteToLog = (bool)chb_WritetoLog.IsChecked;
            App.Setting.ServerCheckIntervalSec = (double)txt_serverCheckIntervalSec.Value * 1000;
            App.Setting.TopMost = (bool)chb_topMost.IsChecked;
            App.Setting.ActiveNewInputDefault = (bool)chb_activeNewInputDefault.IsChecked;
            App.Setting.DefaultLanguage = JsonConvert.SerializeObject((Language)cb_defaultLanguage.SelectedItem);
            App.Setting.ThemeName = App.Setting.ThemeName;
            App.Setting.AccentName = App.Setting.AccentName;
            App.Setting.ReportingPath = txt_powerBuilderPath.Text;

            if (FileOperations.SaveSettings()) { await MainWindow.ShowMessageOnMainWindow(false, Resources["savingSuccessfull"].ToString()); }
        }

        private void ApiAddress_TextChanged(object sender, TextChangedEventArgs e) => btnApiTest.IsEnabled = txt_apiAddress.Text.Length > 0;

        private void BtnRestart_Click(object sender, RoutedEventArgs e) => App.AppRestart();

        private void BtnBrowse_Click(object sender, RoutedEventArgs e) {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog
                { DefaultExt = ".exe", Filter = "Exe files |*.exe", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) { txt_powerBuilderPath.Text = dlg.FileName; }
            }
            catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }
    }
}