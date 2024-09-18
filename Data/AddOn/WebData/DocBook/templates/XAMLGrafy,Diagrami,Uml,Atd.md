<UserControl
    x:Class="GoldenSystem.Pages.GraphsPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mah="http://metro.mahapps.com/winfx/xaml/controls"
    xmlns:bh="http://schemas.microsoft.com/xaml/behaviors"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks"
    xmlns:local="clr-namespace:GoldenSystem.Pages"
    xmlns:lvc="clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:xctk="http://schemas.xceed.com/wpf/xaml/toolkit"
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
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <WrapPanel
            Grid.Row="0" Grid.Column="0"
            Width="{Binding Path=ActualWidth, ElementName=Settings}"
            Height="{Binding Path=ActualHeight, ElementName=Settings}"
            HorizontalAlignment="Stretch" VerticalAlignment="Stretch">
            <DockPanel HorizontalAlignment="Stretch" VerticalAlignment="Stretch">

                <DockPanel
                    Width="500" Height="500" Margin="20">
                    <lvc:CartesianChart LegendLocation="Right" Series="{Binding SeriesCollection}">
                        <lvc:CartesianChart.AxisY>
                            <lvc:Axis Title="Sales" LabelFormatter="{Binding YFormatter}" />
                        </lvc:CartesianChart.AxisY>
                        <lvc:CartesianChart.AxisX>
                            <lvc:Axis Title="Month" Labels="{Binding Labels}" />
                        </lvc:CartesianChart.AxisX>
                    </lvc:CartesianChart>
                </DockPanel>

                <DockPanel
                    Width="500" Height="500" Margin="10">
                    <lvc:PieChart
                        DataClick="Chart_OnDataClick"
                        DataTooltip="{x:Null}"
                        Hoverable="False" LegendLocation="Bottom">
                        <lvc:PieChart.Series>
                            <lvc:PieSeries
                                Title="Maria" DataLabels="True"
                                LabelPoint="{Binding PointLabel}"
                                Values="3" />
                            <lvc:PieSeries
                                Title="Charles" DataLabels="True"
                                LabelPoint="{Binding PointLabel}"
                                Values="4" />
                            <lvc:PieSeries
                                Title="Frida" DataLabels="True"
                                LabelPoint="{Binding PointLabel}"
                                Values="6" />
                            <lvc:PieSeries
                                Title="Frederic" DataLabels="True"
                                LabelPoint="{Binding PointLabel}"
                                Values="2" />
                        </lvc:PieChart.Series>
                    </lvc:PieChart>
                </DockPanel>

                <DockPanel
                    Width="500" Height="500" Margin="10">
                    <Button Margin="10" Click="ChangeValueOnClick">Update</Button>
                    <lvc:AngularGauge
                        Grid.Row="1" FontSize="16" FontWeight="Bold" Foreground="White" FromValue="50"
                        LabelsStep="50" SectionsInnerRadius=".5" TicksForeground="White" TicksStep="25" ToValue="250"
                        Wedge="300"
                        Value="{Binding Value}">
                        <lvc:AngularGauge.Sections>
                            <lvc:AngularSection
                                Fill="#F8A725" FromValue="50" ToValue="200" />
                            <lvc:AngularSection
                                Fill="#FF3939" FromValue="200" ToValue="250" />
                        </lvc:AngularGauge.Sections>
                    </lvc:AngularGauge>
                </DockPanel>
            </DockPanel>
        </WrapPanel>
    </Grid>
</UserControl>
----------------------------------------------------------------------------------------------------------------

using GoldenSystem.Classes;
using GoldenSystem.GlobalOperations;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GoldenSystem.Pages {

    public partial class GraphsPage : UserControl, INotifyPropertyChanged {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static Classes.BasicCalendarList selectedRecord = new Classes.BasicCalendarList();

        public GraphsPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, JsonConvert.DeserializeObject<Language>(App.Setting.DefaultLanguage).Value);

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Title = "Series 4",
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeground = System.Windows.Media.Brushes.Gray
            });

            //modifying any series values will also animate and update the chart
            SeriesCollection[3].Values.Add(5d);

            //Second Graph
            PointLabel = chartPoint =>
              string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            Value = 160;

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private double _value;
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint) {
            var chart = (PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series.Cast<PieSeries>()) {
                series.PushOut = 0;
            }

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        public double Value {
            get { return _value; }
            set {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private void ChangeValueOnClick(object sender, RoutedEventArgs e) {
            Value = new Random().Next(50, 250);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}