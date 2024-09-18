using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using ICSharpCode.WpfDesign.Designer.Services;
using ICSharpCode.WpfDesign.Designer.Xaml;
using ICSharpCode.WpfDesign.XamlDom;
using System.Xml;
using System.Text;
using EasyITSystemCenter.Tools;

//WebView Has Problem
namespace EasyITSystemCenter.Pages {


    public partial class XamlCodeEditorPage : UserControl {

        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static TemplateClassList selectedRecord = new TemplateClassList();

        private static string xaml = @"<Grid 
xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
mc:Ignorable=""d""
x:Value=""rootElement"" Background=""White""></Grid>";

        public XamlCodeEditorPage() {

             try {
                InitializeComponent();
                _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

                var loadSettings = new XamlLoadSettings();
                loadSettings.DesignerAssemblies.Add(this.GetType().Assembly);

                using (var xmlReader = XmlReader.Create(new StringReader(xaml))) {
                    designSurface.LoadDesigner(xmlReader, loadSettings);
                }





            } catch (Exception ex) { }

        }

        private void lstControls_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var item = lstControls.SelectedItem as ToolBoxItem;
            if (item != null) {
                var tool = new CreateComponentTool(item.Type);
                designSurface.DesignPanel.Context.Services.Tool.CurrentTool = tool;
            }
        }

        private void lstControls_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            var item = lstControls.SelectedItem as ToolBoxItem;
            if (item != null) {
                var tool = new CreateComponentTool(item.Type);
                designSurface.DesignPanel.Context.Services.Tool.CurrentTool = tool;
            }
        }

        private void Export() {
            var sb = new StringBuilder();
            using (var xmlWriter = new XamlXmlWriter(sb)) {
                designSurface.SaveDesigner(xmlWriter);
            }
            var xamlCode = sb.ToString();
        }

    }
}