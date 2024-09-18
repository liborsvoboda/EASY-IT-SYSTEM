using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.OSHelpers;
using System;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace EasyITSystemCenter.Pages {

    public partial class SystemMonitorPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static BasicCalendarList selectedRecord = new BasicCalendarList();

        private readonly CPUUsage CPU = new CPUUsage();
        private readonly RAMUsage RAM = new RAMUsage();
        private readonly Timer _timer = new Timer();

        public SystemMonitorPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);
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
            lock (this) {
                CPU.Update();
                RAM.Update();
                this.Dispatcher.Invoke(() => {
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