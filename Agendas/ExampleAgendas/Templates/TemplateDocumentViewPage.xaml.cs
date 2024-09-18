using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace EasyITSystemCenter.Pages {

    /// <summary>
    /// Template Page For View document, pictures, text and and much more file formats opened in WebViewer
    /// </summary>
    public partial class TemplateDocumentViewPage : UserControl {

        /// <summary>
        /// Standartized declaring static page data for global vorking with pages
        /// </summary>
        public static DataViewSupport dataViewSupport = new DataViewSupport();

        public static TemplateClassList selectedRecord = new TemplateClassList();

        /// <summary>
        /// Initialize page with loading Dictionary and direct show example file
        /// </summary>
        public TemplateDocumentViewPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            webBrowser.EnsureCoreWebView2Async();
            webBrowser.Source = new System.Uri(Path.Combine(App.appRuntimeData.startupPath, "Data","Images", "groupware-solution.png"));
            //dv_docuentViewer.Document = new XpsDocument(Path.Combine(App.appRuntimeData.startupPath, "Data", "xpsDocument.xps"), FileAccess.Read).GetFixedDocumentSequence();
        }
    }
}