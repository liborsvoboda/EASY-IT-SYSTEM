using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace EasyITSystemCenter.SystemHelper {

    public class SystemUpdater {

        public static async void CheckUpdate(bool directUpdate) {
            MainWindow metroWindow = (MainWindow)Application.Current.MainWindow;

            if (string.IsNullOrWhiteSpace(App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_updateUrl").Value)) { await MainWindow.ShowMessageOnMainWindow(false, metroWindow.Resources["updateUrlNotSet"].ToString()); }
            Tuple<Version, string> updateResult = await LastUpdate();

            if (string.IsNullOrWhiteSpace(updateResult.Item2) && directUpdate) { await MainWindow.ShowMessageOnMainWindow(false, metroWindow.Resources["noneUpdateAvailable"].ToString()); }
            else if (updateResult.Item2 == "update") {
                if (App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_automaticUpdate").Value == "showInfo" || directUpdate) {
                    MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false,
                        metroWindow.Resources["actualVersion"].ToString() + ": " + App.appRuntimeData.AppVersion.ToString() + Environment.NewLine +
                        metroWindow.Resources["newVersionisAvaiable"].ToString() + ": " + updateResult.Item1.ToString(), true);
                    if (result == MessageDialogResult.Affirmative) { DownloadUpdate(updateResult.Item1.ToString(), true); }
                    else { MainWindow.ProgressRing = Visibility.Hidden; }
                }
                else if (App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_automaticUpdate").Value == "automaticDownload") { DownloadUpdate(updateResult.Item1.ToString(), false); }
                else if (App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_automaticUpdate").Value == "automaticInstall") { DownloadUpdate(updateResult.Item1.ToString(), true); }
            }
            else {
                await MainWindow.ShowMessageOnMainWindow(false, updateResult.Item2?.ToString(), false);
            }
        }

        private static async Task<Tuple<Version, string>> LastUpdate() {
            try {
                HttpClient httpClient = new HttpClient();
                Stream resultStream = await httpClient.GetStreamAsync(App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_updateUrl").Value);

                string theLine = null;
                Version updateVersion = App.appRuntimeData.AppVersion;

                StreamReader theStreamReader = new StreamReader(resultStream);
                while ((theLine = theStreamReader.ReadLine()) != null) {
                    if (theLine.Split('_')[0].ToLower() == App.appRuntimeData.appName.ToLower() && Version.Parse(theLine.Split('_')[1].Remove(theLine.Split('_')[1].Length - 4, 4)) > updateVersion) { updateVersion = Version.Parse(theLine.Split('_')[1].Remove(theLine.Split('_')[1].Length - 4, 4)); }
                }

                if (updateVersion > App.appRuntimeData.AppVersion) { return new Tuple<Version, string>(updateVersion, "update"); }
                else { return new Tuple<Version, string>(updateVersion, null); }
            } catch (Exception ex) { return new Tuple<Version, string>(App.appRuntimeData.AppVersion, ex.Message); }
        }

        private static async void DownloadUpdate(string version, bool installNow) {
            MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;

            try {
                WebClient webClient = new WebClient();

                webClient.DownloadProgressChanged += (sender, args) => {
                    metroWindow.Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(delegate () {
                        MainWindow.ProgressRing = args.ProgressPercentage > 0 && args.ProgressPercentage < 99 ? Visibility.Visible : Visibility.Hidden;
                        MainWindow.DownloadShow = args.ProgressPercentage > 0 && args.ProgressPercentage < 99 ? 200 : 0;
                        MainWindow.DownloadStatus = args.ProgressPercentage;
                    }));
                };

                webClient.DownloadFileCompleted += (sender, args) => { DownloadCompleted(version, installNow); };
                await webClient.DownloadFileTaskAsync(new Uri(App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_updateUrl").Value.Remove(App.appRuntimeData.AppClientSettings.First(b => b.Key == "sys_updateUrl").Value.Length - 6, 6) + App.appRuntimeData.appName + "_" + version + ".exe"), Path.Combine(App.appRuntimeData.updateFolder, App.appRuntimeData.appName + "_" + version + ".exe"));
            } catch (Exception ex) {
                //await MainWindow.ShowMessageOnMainWindow(true, ex.Message + Environment.NewLine + ex.StackTrace);
                MainWindow.ProgressRing = Visibility.Hidden;
            }
        }

        private static async void DownloadCompleted(string version, bool installNow) {
            MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
            try {
                if (new FileInfo(Path.Combine(App.appRuntimeData.updateFolder, App.appRuntimeData.appName + "_" + version + ".exe")).Length > 1048576) {
                    if (installNow) { InstallUpdate(Path.Combine(App.appRuntimeData.updateFolder, App.appRuntimeData.appName + "_" + version + ".exe")); }
                    else {
                        MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false,
                                    metroWindow.Resources["newVersionIsDownloaded"].ToString() + ": " + version, true);
                        if (result == MessageDialogResult.Affirmative) { InstallUpdate(Path.Combine(App.appRuntimeData.updateFolder, App.appRuntimeData.appName + "_" + version + ".exe")); }
                        else { MainWindow.ProgressRing = Visibility.Hidden; }
                    }
                }
                else {   //Update file is under 1MB go next without info
                    metroWindow.Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(delegate () { MainWindow.ProgressRing = Visibility.Hidden; MainWindow.DownloadShow = 0; MainWindow.DownloadStatus = 0; }));
                }
            } catch (Exception ex) {
                //await MainWindow.ShowMessageOnMainWindow(true, ex.Message + Environment.NewLine + ex.StackTrace);
                MainWindow.ProgressRing = Visibility.Hidden;
            }
        }

        private static async void InstallUpdate(string path) {
            try {
                var psi = new ProcessStartInfo() { FileName = path, Verb = (Environment.OSVersion.Version.Major >= 6) ? "runas" : "" };
                Process.Start(psi);
                App.AppQuitRequest(true);
            } catch (Exception ex) {
                //await MainWindow.ShowMessageOnMainWindow(true, ex.Message + Environment.NewLine + ex.StackTrace);
                MainWindow.ProgressRing = Visibility.Hidden;
            }
        }


    }
}