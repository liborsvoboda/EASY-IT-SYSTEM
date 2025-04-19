using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace EasyITSystemCenter.Pages {

    public partial class WebCodeEditorPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static TemplateClassList selectedRecord = new TemplateClassList();


        private ServerModuleAndServiceList systemWebCodeEditor = new ServerModuleAndServiceList();
        private List<ServerModuleAndServiceList> serverModuleAndServiceList = new List<ServerModuleAndServiceList>();
        private int FoundedPositionIndex = 0; private int ReplacePositionIndex = 0;

        public WebCodeEditorPage() {

            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                _ = FormOperations.TranslateFormFields(ListForm);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }


            _ = LoadDataList();

        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                serverModuleAndServiceList = await CommunicationManager.GetApiRequest<List<ServerModuleAndServiceList>>(ApiUrls.ServerModuleAndServiceList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                systemWebCodeEditor = serverModuleAndServiceList.FirstOrDefault(a => a.Name == "SystemWebEditors");
                if (systemWebCodeEditor == null) { await MainWindow.ShowMessageOnMainWindow(true, "Missing HtmlBodyOnly Module: 'SystemWebEditors'"); }
                else {
                    WebCodeEditor.Text = systemWebCodeEditor.CustomHtmlContent; 
                }    

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }


        private void HighlightCodeChanged(object sender, SelectionChangedEventArgs e) {
            WebCodeEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString());
        }

        private void CaseSensitiveChange(object sender, RoutedEventArgs e) {
            if (dataViewSupport.FormShown && btn_searchText != null) { btn_searchText.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0; }
        }

        private void CodeSearchTextChanged(object sender, TextChangedEventArgs e) {
            btn_searchText.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0; SearchTextInEditor();
        }

        private void SearchText_Click(object sender, RoutedEventArgs e) {
            SearchTextInEditor();
        }

        private void SelectedOnlyChange(object sender, RoutedEventArgs e) {
            if (dataViewSupport.FormShown && btn_codeReplace != null) { btn_codeReplace.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0; }
        }

        private void CodeReplaceTextChanged(object sender, TextChangedEventArgs e) {
            btn_codeReplace.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0;
        }

        private void SearchTextInEditor() {
            ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref WebCodeEditor, (bool)chb_caseSensitiveIgnore.IsChecked);
            if (FoundedPositionIndex == 0) { btn_searchText.IsEnabled = false; }
        }

        private void CodeReplaceClick(object sender, RoutedEventArgs e) {
            ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref WebCodeEditor, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked);
            if (ReplacePositionIndex == 0) { btn_codeReplace.IsEnabled = false; }
        }

        private async void BtnOpenInBrowser_Click(object sender, RoutedEventArgs e) {
            try {
                systemWebCodeEditor.CustomHtmlContent = WebCodeEditor.Text;
                string json = JsonConvert.SerializeObject(systemWebCodeEditor);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                DBResultMessage dBResult = await CommunicationManager.PostApiRequest(ApiUrls.ServerModuleAndServiceList, httpContent, null, App.UserData.Authentification.Token);

                if (dBResult.RecordCount > 0) {
                    SystemOperations.StartExternalProccess("weburl", App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + systemWebCodeEditor.UrlSubPath);
                } else { await MainWindow.ShowMessageOnMainWindow(false, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = ".*", Filter = "All files (*.*)|*.*", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) {
                    WebCodeEditor.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName));
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }
    }
}