using EASYTools.Calendar;
using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;

namespace EasyITSystemCenter.Pages {

    public partial class BasicCalendarListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static GlobalClasses.BasicCalendarList selectedRecord = new GlobalClasses.BasicCalendarList();

        private new string Language = App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value;
        private List<string> Months = new List<string>();

        public BasicCalendarListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            if (Language == "system") { Language = Thread.CurrentThread.CurrentCulture.ToString(); }
            switch (Language) {
                case "cs-CZ":
                    Months = new List<string> { "Leden", "Únor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
                    break;

                case "en-US":
                    Months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                    break;
            }

            cboMonth.ItemsSource = Months;
            for (int i = -10; i < 10; i++) { cboYear.Items.Add(DateTime.Today.AddYears(i).Year); }

            cboMonth.SelectedItem = Months.FirstOrDefault(w => w.ToUpper() == DateTime.Today.ToString("MMMM").ToUpper());
            cboYear.SelectedItem = DateTime.Today.Year;

            _ = LoadDataList();
            cboMonth.SelectionChanged += (o, e) => RefreshCalendar();
            cboYear.SelectionChanged += (o, e) => RefreshCalendar();
            SetRecord(false);
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            List<GlobalClasses.BasicCalendarList> Data = new List<GlobalClasses.BasicCalendarList>();
            try {
                Data = await CommunicationManager.GetApiRequest<List<GlobalClasses.BasicCalendarList>>(ApiUrls.EasyITCenterBasicCalendarList, App.UserData.Authentification.Id.ToString(), App.UserData.Authentification.Token);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            foreach (GlobalClasses.BasicCalendarList record in Data) {
                foreach (Day calendarDay in Calendar.Days) {
                    if (calendarDay.Date == record.Date && record.Notes.Length > 0) {
                        calendarDay.Notes = record.Notes;
                    }
                }
            }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private async void RefreshCalendar() {
            if (cboYear.SelectedItem == null) return;
            if (cboMonth.SelectedItem == null) return;

            int year = (int)cboYear.SelectedItem;
            int month = cboMonth.SelectedIndex + 1;
            DateTime targetDate = new DateTime(year, month, 1);

            Calendar.BuildCalendar(targetDate);
            await LoadDataList();
        }

        private async void Calendar_DayChanged(object sender, DayChangedEventArgs e) {
            selectedRecord = new GlobalClasses.BasicCalendarList() {
                UserId = App.UserData.Authentification.Id,
                Date = e.Day.Date,
                Notes = e.Day.Notes,
                TimeStamp = DateTimeOffset.Now.DateTime
            };

            string json = JsonConvert.SerializeObject(selectedRecord);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            DBResultMessage dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterBasicCalendarList, httpContent, null, App.UserData.Authentification.Token);
        }

        private void SetRecord(bool? showForm = null) {
            MainWindow.DataGridSelected = MainWindow.DataGridSelectedIdListIndicator = false; MainWindow.dataGridSelectedId = 0; MainWindow.DgRefresh = false;
            dataViewSupport.FormShown = true;
        }
    }
}