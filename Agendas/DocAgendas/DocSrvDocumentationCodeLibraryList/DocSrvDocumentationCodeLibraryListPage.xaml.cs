using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.GlobalStyles;
using MahApps.Metro.Controls.Dialogs;
using Markdig.Renderers.Docx;
using Markdig;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Win32;
using Newtonsoft.Json;
using Pek.Markdig.HighlightJs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Markdig.Wpf;
using Markdown = Markdig.Markdown;


namespace EasyITSystemCenter.Pages {

    public partial class DocSrvDocumentationCodeLibraryListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static DocSrvDocumentationCodeLibraryList selectedRecord = new DocSrvDocumentationCodeLibraryList();

        private List<DocSrvDocumentationCodeLibraryList> documentationCodeLibraryList = new List<DocSrvDocumentationCodeLibraryList>();

        public DocSrvDocumentationCodeLibraryListPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            try {
                try {
                    _ = FormOperations.TranslateFormFields(ListForm);
                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                LoadParameters();

                new MarkdownPipelineBuilder().UseEmphasisExtras().UseAbbreviations().UseAdvancedExtensions().UseBootstrap()
                .UseDiagrams().UseEmphasisExtras().UseEmojiAndSmiley(true).UseDefinitionLists().UseTableOfContent().UseTaskLists()
                .UseSupportedExtensions().UseSmartyPants().UsePipeTables().UseMediaLinks().UseMathematics().UseListExtras().UseHighlightJs()
                .UseGridTables().UseGlobalization().UseGenericAttributes().UseFootnotes().UseFooters().UseSyntaxHighlighting().UseFigures().Build();

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
   
            _ = LoadDataList();
            SetRecord(false);
        }

        private async void LoadParameters() {
            DgListView.RowHeight = int.Parse(await DataOperations.ParameterCheck("DocumentationAgendasFormsRowHeight"));
        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {
                documentationCodeLibraryList = await CommunicationManager.GetApiRequest<List<DocSrvDocumentationCodeLibraryList>>(ApiUrls.EasyITCenterDocSrvDocumentationCodeLibraryList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);

                DgListView.ItemsSource = documentationCodeLibraryList;
                DgListView.Items.Refresh();
                lb_dataList.ItemsSource = documentationCodeLibraryList;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        private void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(e => {
                    string headername = e.Header.ToString();
                    if (headername == "Value") e.Header = Resources["name"].ToString();
                    else if (headername == "Description") e.Header = Resources["description"].ToString();
                    else if (headername == "TimeStamp") { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id") e.DisplayIndex = 0;
                    else if (headername == "UserId") e.Visibility = Visibility.Hidden;
                    else if (headername == "MdContent") e.Visibility = Visibility.Hidden;
                    else if (headername == "HtmlContent") e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    DocSrvDocumentationCodeLibraryList user = e as DocSrvDocumentationCodeLibraryList;
                    return user.Name.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.Description) && user.Description.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(user.MdContent) && user.MdContent.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        public void NewRecord() {
            selectedRecord = new DocSrvDocumentationCodeLibraryList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (DocSrvDocumentationCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (DocSrvDocumentationCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterDocSrvDocumentationCodeLibraryList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (DocSrvDocumentationCodeLibraryList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (DocSrvDocumentationCodeLibraryList)DgListView.SelectedItem; }
            else { selectedRecord = new DocSrvDocumentationCodeLibraryList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(false);
        }

        private void SetRecord(bool? showForm = null, bool copy = false) {
            txt_id.Value = (copy) ? 0 : selectedRecord.Id;

            try {

                txt_name.Text = selectedRecord.Name;
                txt_description.Text = selectedRecord.Description;
                md_editor.Text = selectedRecord.MdContent;

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            if (showForm != null && showForm == true) {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = false;
                ListView.Visibility = Visibility.Hidden; ListForm.Visibility = Visibility.Visible; dataViewSupport.FormShown = true;
            }
            else {
                MainWindow.DataGridSelected = true; MainWindow.DataGridSelectedIdListIndicator = selectedRecord.Id != 0; MainWindow.dataGridSelectedId = selectedRecord.Id; MainWindow.DgRefresh = true;
                ListForm.Visibility = Visibility.Hidden; ListView.Visibility = Visibility.Visible; dataViewSupport.FormShown = showForm == null && !bool.Parse(App.appRuntimeData.AppClientSettings.First(a => a.Key.ToLower() == "beh_closeformaftersave".ToLower()).Value);
            }
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, false);

        private async void BtnSaveClose_Click(object sender, RoutedEventArgs e) => await SaveRecord(true, false);

        private async void BtnSaveAsNew_Click(object sender, RoutedEventArgs e) => await SaveRecord(false, true);

        private async void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (DocSrvDocumentationCodeLibraryList)DgListView.SelectedItem : new DocSrvDocumentationCodeLibraryList();
            await LoadDataList();
            SetRecord(false);
        }

        private async Task<bool> SaveRecord(bool closeForm, bool asNew) {
            try
            {
                MainWindow.ProgressRing = Visibility.Visible;

                DBResultMessage dBResult;
                selectedRecord.Id = (int)((txt_id.Value != null) && !asNew ? txt_id.Value : 0);
                selectedRecord.Name = txt_name.Text;
                selectedRecord.Description = txt_description.Text;
                selectedRecord.MdContent = md_editor.Text;
                selectedRecord.HtmlContent = Markdown.ToHtml(mdViewer.Markdown);
                
                selectedRecord.UserId = App.UserData.Authentification.Id;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterDocSrvDocumentationCodeLibraryList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.EasyITCenterDocSrvDocumentationCodeLibraryList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0 && asNew) { await LoadDataList(); }
                if (closeForm) { await LoadDataList(); selectedRecord = new DocSrvDocumentationCodeLibraryList(); SetRecord(false); }
                if (dBResult.RecordCount == 0) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden;
            return true;
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = ".md", Filter = "Markdown files |*.md|All files (*.*)|*.*", Title = Resources["fileOpenDescription"].ToString() };
                if (dlg.ShowDialog() == true) {
                    md_editor.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName));
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        private void Md_editor_TextChanged(object sender, TextChangedEventArgs e) {
            mdViewer.Markdown = md_editor.Text;
 
        }

        private void OpenHyperlink(object sender, ExecutedRoutedEventArgs e) {
            Process.Start(e.Parameter.ToString());
        }

        private void ClickOnImage(object sender, ExecutedRoutedEventArgs e) {
            MessageBox.Show($"URL: {e.Parameter}");
        }


        private void DataListDoubleClick(object sender, MouseButtonEventArgs e) {
            if (lb_dataList.SelectedItems.Count > 0) {
                selectedRecord = ((DocSrvDocumentationCodeLibraryList)lb_dataList.SelectedItem);
                SetRecord(true);
            }
        }

        private async void BtnOpenInBrowser_Click(object sender, RoutedEventArgs e) {
            await SaveRecord(false, false);
            SystemOperations.StartExternalProccess("weburl", App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebDocLibraryPreview")) + "/" + txt_id.Value.ToString());
        }

        private void BtnExportDocx_Click(object sender, RoutedEventArgs e) {
            var document = DocxTemplateHelper.Standard;
            var styles = new DocumentStyles();
            var renderer = new DocxDocumentRenderer(document, styles, NullLogger<DocxDocumentRenderer>.Instance);
            var pipeline = new MarkdownPipelineBuilder().UseEmphasisExtras().UseAbbreviations().UseAdvancedExtensions().UseBootstrap()
                .UseDiagrams().UseEmphasisExtras().UseEmojiAndSmiley(true).UseDefinitionLists().UseTableOfContent().UseTaskLists()
                .UseSupportedExtensions().UseSmartyPants().UsePipeTables().UseMediaLinks().UseMathematics().UseListExtras().UseHighlightJs()
                .UseGridTables().UseGlobalization().UseGenericAttributes().UseFootnotes().UseFooters().UseSyntaxHighlighting().UseFigures().Build();
            object exportedFile = Markdown.Convert(mdViewer.Markdown, renderer, pipeline);

            SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".docx", Filter = "Word files |*.docx", Title = Resources["fileOpenDescription"].ToString() };
            if (dlg.ShowDialog() == true) { ((DocxDocumentRenderer)exportedFile).Document.SaveAs(dlg.FileName); }
        }

        private void BtnExportHtml_Click(object sender, RoutedEventArgs e) {
            string exportedFile = Markdown.ToHtml(mdViewer.Markdown);
            SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".html", Filter = "Html files |*.html", Title = Resources["fileOpenDescription"].ToString() };
            if (dlg.ShowDialog() == true) { FileOperations.ByteArrayToFile(dlg.FileName, System.Text.Encoding.UTF8.GetBytes(exportedFile)); }
        }

        private void BtnExportMd_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".md", Filter = "Md files |*.md", Title = Resources["fileOpenDescription"].ToString() };
            if (dlg.ShowDialog() == true) { FileOperations.ByteArrayToFile(dlg.FileName, System.Text.Encoding.UTF8.GetBytes(mdViewer.Markdown)); }
        }

    }

}