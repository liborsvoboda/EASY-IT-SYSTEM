<UserControl
    x:Class="GoldenSystem.Pages.TemplateVideoPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mah="http://metro.mahapps.com/winfx/xaml/controls"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:xceed="clr-namespace:WPFMediaKit.DirectShow.MediaPlayers;assembly=WPFMediaKit"
    Width="Auto"
    Height="Auto"
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
                <ColumnDefinition />
                <ColumnDefinition />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="*" />
                <RowDefinition Height="30" />
                <RowDefinition Height="50" />
                <RowDefinition Height="Auto" />
            </Grid.RowDefinitions>

            <MediaElement
                x:Name="me_videPlayer"
                Grid.Row="0"
                Grid.RowSpan="2"
                Grid.ColumnSpan="2"
                Margin="0"
                VerticalAlignment="Top"
                LoadedBehavior="Manual"
                MediaEnded="VidePlayerMediaEnded"
                ScrubbingEnabled="True"
                Stretch="UniformToFill"
                UnloadedBehavior="Stop"
                Volume="50" />
            <StackPanel
                Grid.Row="2"
                Grid.Column="0"
                Grid.ColumnSpan="2">
                <WrapPanel
                    x:Name="wp_timeLine"
                    Margin="0"
                    HorizontalAlignment="Stretch">
                    <Slider
                        x:Name="s_timelineSlider"
                        Width="{Binding Path=ActualWidth, ElementName=wp_timeLine}"
                        Margin="0"
                        HorizontalContentAlignment="Stretch"
                        AllowDrop="True"
                        IsMoveToPointEnabled="True"
                        Thumb.DragCompleted="SliProgress_DragCompleted"
                        Thumb.DragStarted="SliProgress_DragStarted"
                        TickFrequency="2" />
                </WrapPanel>
            </StackPanel>

            <StackPanel
                Grid.Row="3"
                Grid.Column="0"
                HorizontalAlignment="Left">
                <WrapPanel Margin="0" HorizontalAlignment="Left">
                    <Button
                        x:Name="btn_play"
                        Height="40"
                        Margin="20,0"
                        Padding="20,10"
                        Click="Btn_playClick"
                        Style="{DynamicResource AccentedSquareButtonStyle}" />
                    <Button
                        x:Name="btn_pause"
                        Height="40"
                        Margin="30,0"
                        Padding="20,10"
                        Click="Btn_pauseClick"
                        Style="{DynamicResource AccentedSquareButtonStyle}" />
                    <Button
                        x:Name="btn_stop"
                        Height="40"
                        Margin="30,0"
                        Padding="20,10"
                        Click="Btn_stopClick"
                        Style="{DynamicResource AccentedSquareButtonStyle}" />
                </WrapPanel>
            </StackPanel>

            <StackPanel
                Grid.Row="3"
                Grid.Column="1"
                HorizontalAlignment="Right">
                <Slider
                    Name="volumeSlider"
                    Width="200"
                    Height="40"
                    Margin="10,0"
                    HorizontalAlignment="Right"
                    VerticalAlignment="Center"
                    HorizontalContentAlignment="Right"
                    Maximum="1"
                    Minimum="0"
                    ValueChanged="ChangeMediaVolume"
                    Value="1" />
            </StackPanel>
        </Grid>
    </Grid>
</UserControl>

----------------------------------------------------------------------------------------------------------------

using GoldenSystem.Classes;
using GoldenSystem.GlobalOperations;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using System;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

// This is Template Is Customized For Play Video
namespace GoldenSystem.Pages {

    public partial class TemplateVideoPage : UserControl {

        /// <summary>
        /// Standartized declaring static page data for global vorking with pages
        /// </summary>
        public static DataViewSupport dataViewSupport = new DataViewSupport();

        public static TemplateClassList selectedRecord = new TemplateClassList();

        private bool userIsDraggingSlider = false;
        private readonly Timer timer1Sec = new Timer() { Enabled = false, Interval = 1000 };

        public TemplateVideoPage() {
            InitializeComponent();
            Language defaultLanguage = JsonConvert.DeserializeObject<Language>(App.Setting.DefaultLanguage);
            _ = SystemOperations.SetLanguageDictionary(Resources, defaultLanguage.Value);

            btn_play.Content = Resources["play"].ToString();
            btn_pause.Content = Resources["pause"].ToString();
            btn_stop.Content = Resources["stop"].ToString();

            timer1Sec.Elapsed += Timer1sec_Elapsed;
            me_videPlayer.Source = new Uri(Path.Combine(App.startupPath, "Data", "speed.mp4"));
        }

        private void Timer1sec_Elapsed(object sender, ElapsedEventArgs e) {
            this.Invoke(() =>
            {
                if (me_videPlayer.NaturalDuration.HasTimeSpan & !userIsDraggingSlider)
                {
                    s_timelineSlider.Minimum = 0;
                    s_timelineSlider.Maximum = me_videPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                    s_timelineSlider.Value = me_videPlayer.Position.TotalSeconds;
                }
            });
        }

        private void Btn_playClick(object sender, RoutedEventArgs e) {
            me_videPlayer.Play();
            timer1Sec.Enabled = true;
            InitializePropertyValues();
        }

        private void Btn_pauseClick(object sender, RoutedEventArgs e) {
            me_videPlayer.Pause();
            timer1Sec.Enabled = false;
        }

        private void Btn_stopClick(object sender, RoutedEventArgs e) {
            me_videPlayer.Stop();
            me_videPlayer.Position = TimeSpan.FromSeconds(0);
            timer1Sec.Enabled = false;
        }

        private void SliProgress_DragStarted(object sender, DragStartedEventArgs e) {
            userIsDraggingSlider = true;
        }

        private void SliProgress_DragCompleted(object sender, DragCompletedEventArgs e) {
            userIsDraggingSlider = false;
            me_videPlayer.Position = TimeSpan.FromSeconds(s_timelineSlider.Value);
        }

        private void VidePlayerMediaEnded(object sender, EventArgs e) {
            me_videPlayer.Stop();
            timer1Sec.Enabled = false;
        }

        private void InitializePropertyValues() {
            me_videPlayer.Volume = (double)volumeSlider.Value;
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args) {
            me_videPlayer.Volume = (double)volumeSlider.Value;
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
    }
}