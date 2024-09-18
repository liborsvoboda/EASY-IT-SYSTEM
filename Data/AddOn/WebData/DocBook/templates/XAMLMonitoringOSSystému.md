<UserControl
    x:Class="GoldenSystem.Pages.SystemMonitorPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:GoldenSystem="clr-namespace:GoldenSystem"
    xmlns:behaviors="http://schemas.microsoft.com/xaml/behaviors"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:lvc="clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:wpf="clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf"
    Name="Settings"
    HorizontalAlignment="Stretch"
    VerticalAlignment="Stretch"
    d:DesignHeight="500"
    d:DesignWidth="600"
    Foreground="White"
    Tag="Settings"
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
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="2*" />
            <ColumnDefinition Width="1*" />
        </Grid.ColumnDefinitions>

        <!--  SystemInfo Part  -->
        <DockPanel
            Grid.Row="0" Grid.RowSpan="2" Grid.Column="0" Width="Auto" Height="Auto"
            Margin="0,0,0,0" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" />

        <!--  Stats Part  -->
        <Grid
            Grid.Row="0" Grid.RowSpan="2" Grid.Column="1">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="1*" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="1*" />
                <RowDefinition Height="1*" />
            </Grid.RowDefinitions>
            <DockPanel
                Grid.Row="0" Grid.Column="0" Margin="0,30,0,0">
                <Label
                    Name="_cpuLabel" Margin="5" HorizontalAlignment="Center" DockPanel.Dock="Bottom" FontFamily="Consolas"
                    FontSize="16" Foreground="White">
                    CPU
                </Label>
                <lvc:AngularGauge
                    Name="_cpuDial" Margin="20,10,20,0" LabelsStep="10"
                    TicksForeground="{DynamicResource HighlightBrush}"
                    TicksStep="5">
                    <lvc:AngularGauge.Sections>
                        <lvc:AngularSection
                            Fill="#fffafa" FromValue="0" ToValue="100" />
                    </lvc:AngularGauge.Sections>
                </lvc:AngularGauge>
            </DockPanel>
            <DockPanel Grid.Row="1" Grid.Column="0">
                <Label
                    Name="_ramLabel" Margin="5" HorizontalAlignment="Center" DockPanel.Dock="Bottom" FontFamily="Consolas"
                    FontSize="16" Foreground="White">
                    RAM
                </Label>
                <lvc:AngularGauge
                    Name="_ramDial" Margin="20,10,20,0" LabelsStep="10"
                    TicksForeground="{DynamicResource HighlightBrush}"
                    TicksStep="5">
                    <lvc:AngularGauge.Sections>
                        <lvc:AngularSection
                            Fill="#fffafa" FromValue="0" ToValue="100" />
                    </lvc:AngularGauge.Sections>
                </lvc:AngularGauge>
            </DockPanel>
        </Grid>
        <!--  Enf Of Stats Part  -->
    </Grid>
</UserControl>

----------------------------------------------------------------------------------------------------------------

using GoldenSystem.Classes;
using GoldenSystem.GlobalOperations;
using GoldenSystem.OSHelpers;
using Newtonsoft.Json;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace GoldenSystem.Pages {

    public partial class SystemMonitorPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static Classes.BasicCalendarList selectedRecord = new Classes.BasicCalendarList();

        private readonly CPUUsage CPU = new CPUUsage();
        private readonly RAMUsage RAM = new RAMUsage();
        private readonly Timer _timer = new Timer();

        public SystemMonitorPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, JsonConvert.DeserializeObject<Language>(App.Setting.DefaultLanguage).Value);
            _timer.Enabled = true;
            _timer.Interval = 250;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) => Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e) {
            _timer.Enabled = false;
            _timer.Stop();
            _cpuDial.Sections.Clear();
            _cpuDial.Dispatcher.DisableProcessing();
        }

        private static double Clamp(double val, double min, double max) {
            if (val < min)
                return min;
            else if (val > max)
                return max;
            return val;
        }

        private double ToGBFromMBytes(Int64 bytes) {
            return (double)bytes / 1024.0;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            lock (this)
            {
                CPU.Update();
                RAM.Update();
                this.Dispatcher.Invoke(() =>
                {
                    var cpuVal = Clamp(CPU.CPU, 0.0, 100.0);
                    var ramVal = Clamp(RAM.Memory, 0.0, 100.0);

                    _cpuDial.Value = cpuVal;
                    _ramDial.Value = ramVal;

                    _cpuLabel.Content = string.Format("CPU {0} %", cpuVal.ToString("N2"));
                    _ramLabel.Content = string.Format("RAM {0}/{1} GB", ToGBFromMBytes(RAM.UsedBytes).ToString("N2"), ToGBFromMBytes(RAM.TotalBytes).ToString("N2"));
                });
            }
        }


      
    }
}