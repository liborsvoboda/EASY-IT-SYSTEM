using EasyITSystemCenter.GlobalOperations;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


namespace EasyITSystemCenter.SystemHelper {

    /// <summary>
    /// Webview2 Add Installer
    /// </summary>
    public class WebView2AutoInstaller {

        /// <summary>
        /// Check WebView2 runtime is installed and download then install it.
        /// This Is Startup IO.PAth Exceprion from Init WebView2 Without Object
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckAndInstallWebView() {
            try {
                string version = CoreWebView2Environment.GetAvailableBrowserVersionString();
                if (!string.IsNullOrEmpty(version)) { return true; }
            } catch {}

            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("WebViewNotInstalledInstallItQuestion"), true);
            if (result == MessageDialogResult.Affirmative) { await InstallWebView(); }
            return true;
        }


        /// <summary>
        /// Startup Install Missing WebView2
        /// </summary>
        /// <returns></returns>
        private async static Task<bool> InstallWebView() {
            try {
                var process = Process.Start(new ProcessStartInfo() {
                    FileName = Path.Combine(App.appRuntimeData.startupPath, "Data", "Install", "MicrosoftEdgeWebview2Setup.exe"),
                    Arguments = $"/install", Verb = "runas", UseShellExecute = true
                });

                process.WaitForExit();
            } catch (Exception e) {
                await MainWindow.ShowMessageOnMainWindow(true, await DBOperations.DBTranslation("WebViewCannotBeInstalledCheckRights") + Environment.NewLine + e.Message, true);
            }
            return true;
        }
    }
}
