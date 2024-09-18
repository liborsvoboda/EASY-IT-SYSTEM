using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

// This is Template Is Customized For Play Video
namespace EasyITSystemCenter.Pages {

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
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            btn_play.Content = Resources["play"].ToString();
            btn_pause.Content = Resources["pause"].ToString();
            btn_stop.Content = Resources["stop"].ToString();

            timer1Sec.Elapsed += Timer1sec_Elapsed;
            me_videPlayer.Source = new Uri(Path.Combine(App.appRuntimeData.startupPath, "Data" ,"Media","speed.mp4"));
        }

        private void Timer1sec_Elapsed(object sender, ElapsedEventArgs e) {
            this.Invoke(() => {
                if (me_videPlayer.NaturalDuration.HasTimeSpan & !userIsDraggingSlider) {
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

    }
}