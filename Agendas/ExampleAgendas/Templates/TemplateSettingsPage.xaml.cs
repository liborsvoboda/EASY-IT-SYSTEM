using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

// This is Template Is Customized For Load and Save data without List (Settings) is folded from
// standartized Methods
namespace EasyITSystemCenter.Pages {

    public partial class TemplateSettingsPage : UserControl {

        /// <summary>
        /// Standartized declaring static page data for global vorking with pages
        /// </summary>
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static TemplateClassList selectedRecord = new TemplateClassList();


        /// <summary>
        /// Initialize page with loading Dictionary and start loding data Manual work needed
        /// Translate All XAML fields by Resources Runned on start
        /// </summary>
        public TemplateSettingsPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                lbl_apiAddress.Content = Resources["conn_apiAddress"].ToString();
                lbl_serviceName.Content = Resources["serviceName"].ToString();
                lbl_writeToLog.Content = Resources["logging"].ToString();
                lbl_serverCheckIntervalSec.Content = Resources["serverCheckIntervalSec"].ToString();
                lbl_topMost.Content = Resources["beh_topMost"].ToString();
                lbl_activeNewInputDefault.Content = Resources["beh_activeNewInputDefault"].ToString();
                lbl_defaultLanguage.Content = Resources["sys_defaultLanguage"].ToString();
                lbl_showVerticalPanel.Content = Resources["showVerticalPanel"].ToString();
                lbl_powerBuilderPath.Content = Resources["powerBuilderPath"].ToString();

                btn_browse.Content = Resources["browse"].ToString();
                btn_restart.Content = Resources["restart"].ToString();
                btnApiTest.Content = Resources["apiConnectionTest"].ToString();
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            //data
            txt_apiAddress.Text = App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value;
            chb_WritetoLog.IsChecked = bool.Parse(App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_writeToLog").Value);
            txt_serverCheckIntervalSec.Value = int.Parse(App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_serverCheckInterval").Value) * 0.001;
            chb_topMost.IsChecked = bool.Parse(App.appRuntimeData.AppClientSettings.First(b => b.Key == "beh_topMost").Value);
            chb_activeNewInputDefault.IsChecked = bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key == "beh_activeNewInputDefault").Value);
            txt_powerBuilderPath.Text = App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_reportingPath").Value;

            cb_defaultLanguage.ItemsSource = SystemLocalEnumSets.Languages;

          
        }

        /// <summary>
        /// Customized GET Call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private async void BtnApiTest_Click(object sender, RoutedEventArgs e) {
            using (HttpClient httpClient = new HttpClient()) {
                try {
                    string json = await httpClient.GetStringAsync(txt_apiAddress.Text + "/" + ApiUrls.BackendCheckService + "/Db");
                    await MainWindow.ShowMessageOnMainWindow(false, json);
                } catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + ex.StackTrace); }
            }
        }


        private void ApiAddress_TextChanged(object sender, TextChangedEventArgs e) => btnApiTest.IsEnabled = txt_apiAddress.Text.Length > 0;

        private void BtnRestart_Click(object sender, RoutedEventArgs e) => App.AppRestart();

        private void BtnBrowse_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog { DefaultExt = ".exe", Filter = "Exe files |*.exe", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) { txt_powerBuilderPath.Text = dlg.FileName; }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }
    }
}