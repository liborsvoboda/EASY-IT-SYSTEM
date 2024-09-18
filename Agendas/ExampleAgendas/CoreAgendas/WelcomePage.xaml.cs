using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Animation;

namespace EasyITSystemCenter.Pages {

    public partial class WelcomePage : MetroWindow {
        private List<SolutionMottoList> MottoList = new List<SolutionMottoList>();
        private readonly string MottoListPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data", "Mottos");

        public WelcomePage() {
            InitializeComponent();
            Loaded += MainManagerOnOnLoaded;
            Show();
        }

        private async void MainManagerOnOnLoaded(object sender, RoutedEventArgs e) {
            try {
                var lines = File.ReadLines(MottoListPath);
                foreach (string line in lines) { if (!string.IsNullOrWhiteSpace(line)) { MottoList.Add(new SolutionMottoList() { SystemName = line }); } }
                App.LanguageList = await CommunicationManager.GetApiRequest<List<SystemTranslationList>>(ApiUrls.EasyITCenterSystemTranslationList, null, null);
                MottoList = await CommunicationManager.GetApiRequest<List<SolutionMottoList>>(ApiUrls.EasyITCenterSolutionMottoList, null, null);
            } catch { }

            try {
                labelMotto.Content = await DBOperations.DBTranslation(MottoList[int.Parse(Math.Truncate(0.001 * new Random().Next(0, (MottoList.Count - 1) * 1000)).ToString())].SystemName, true);
                Storyboard endAnimation = (Storyboard)FindResource("ProgressAnimation");
                BeginStoryboard(endAnimation);
                endAnimation = (Storyboard)FindResource("LogoAnimation");
                BeginStoryboard(endAnimation);
                endAnimation = (Storyboard)FindResource("StartAnimation");
                BeginStoryboard(endAnimation);
            } catch { Close(); }
        }

        private void Storyboard_Completed(object sender, EventArgs e) => Close();
    }
}