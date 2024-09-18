using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace EasyITSystemCenter.Pages {

    /// <summary>
    /// Template Page For internet pages document, pictures, text and and much more file formats
    /// opened in WebViewer
    /// </summary>
    public partial class TemplateWebViewPage : UserControl {

        /// <summary>
        /// Standartized declaring static page data for global vorking with pages
        /// </summary>
        public static DataViewSupport dataViewSupport = new DataViewSupport();

        public static TemplateClassList selectedRecord = new TemplateClassList();

        /// <summary>
        /// Initialize page with loading Dictionary and direct show example file
        /// </summary>
        public TemplateWebViewPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                webBrowser.EnsureCoreWebView2Async();
                webBrowser.Source = new Uri(Path.Combine("https://kliknetezde.cz"));

                MainWindow.ProgressRing = System.Windows.Visibility.Visible;
            } catch (Exception ex) { }
            MainWindow.ProgressRing = System.Windows.Visibility.Hidden;
        }

      
    }
}