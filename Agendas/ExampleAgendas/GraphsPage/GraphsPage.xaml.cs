using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EasyITSystemCenter.Pages {

    public partial class GraphsPage : UserControl, INotifyPropertyChanged {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static BasicCalendarList selectedRecord = new BasicCalendarList();

        private bool _mariaSeriesVisibility;
        private bool _charlesSeriesVisibility;
        private bool _johnSeriesVisibility;

        public GraphsPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            MariaSeriesVisibility = true;
            CharlesSeriesVisibility = true;
            JohnSeriesVisibility = false;
            SeriesValues = GetData();

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
            SeriesCollection.Add(new LineSeries {
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

            var r = new Random();
            var Values = new Dictionary<string, double>();

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

        public ChartValues<double> SeriesValues { get; set; }

        private ChartValues<double> GetData() {
            var r = new Random();
            var trend = 100;
            var values = new ChartValues<double>();

            for (var i = 0; i < 160; i++) {
                var seed = r.NextDouble();
                if (seed > .8) trend += seed > .9 ? 50 : -50;
                values.Add(trend + r.Next(0, 10));
            }

            return values;
        }

        public bool MariaSeriesVisibility {
            get { return _mariaSeriesVisibility; }
            set {
                _mariaSeriesVisibility = value;
                OnPropertyChanged("MariaSeriesVisibility");
            }
        }

        public bool CharlesSeriesVisibility {
            get { return _charlesSeriesVisibility; }
            set {
                _charlesSeriesVisibility = value;
                OnPropertyChanged("CharlesSeriesVisibility");
            }
        }

        public bool JohnSeriesVisibility {
            get { return _johnSeriesVisibility; }
            set {
                _johnSeriesVisibility = value;
                OnPropertyChanged("JohnSeriesVisibility");
            }
        }
    }
}