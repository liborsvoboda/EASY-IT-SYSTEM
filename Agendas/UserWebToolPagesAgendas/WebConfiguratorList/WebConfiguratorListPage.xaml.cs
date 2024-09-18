using EasyITSystemCenter.Api;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using System.ComponentModel.Design;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Search;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Folding;
using EasyITSystemCenter.GlobalStyles;
using Newtonsoft.Json;
using MahApps.Metro.Controls.Dialogs;
using System.Net.Http;
using System.Windows.Media.Animation;
using Catel.Collections;



namespace EasyITSystemCenter.Pages {

    public partial class WebConfiguratorListPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static WebConfiguratorList selectedRecord = new WebConfiguratorList();

        private List<WebConfiguratorList> webConfiguratorList = new List<WebConfiguratorList>();
        private List<DocSrvDocTemplateList> docSrvDocTemplateList = new List<DocSrvDocTemplateList>();
        private List<WebCodeLibraryList> webCodeLibraryList = new List<WebCodeLibraryList>();
        private List<SolutionMixedEnumList> solutionMixedEnumList = new List<SolutionMixedEnumList>();
        Storyboard AnimatedSearchInput;

        CompletionWindow completionWindow;
        string currentFileName;
        string lightThemeName = App.appRuntimeData.AppClientSettings.First(a => a.Key == "appe_toolLightThemeName").Value;
        string darkThemeName = App.appRuntimeData.AppClientSettings.First(a => a.Key == "appe_toolDarkThemeName").Value;


        public WebConfiguratorListPage() {

            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);
            InitializeTextMarkerService();

            try {
                _ = FormOperations.TranslateFormFields(ListForm);
                _ = FormOperations.TranslateFormFields(contextMenu);
                _ = FormOperations.TranslateFormFields(gd_codeLibraryForm);
                _ = FormOperations.TranslateSubObjectsNameToToolTip(toolBar);

                

                lb_id.Header = DBOperations.DBTranslation("id").GetAwaiter().GetResult();
                lb_name.Header = DBOperations.DBTranslation("name").GetAwaiter().GetResult();


                cb_selectCode.ItemsSource = HighlightingManager.Instance.HighlightingDefinitions;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            try {
                this.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
                codeEditor.Options.HighlightCurrentLine = true;
                codeEditor.Options.EnableTextDragDrop = true;
                codeEditor.Options.AllowScrollBelowDocument = true;
                codeEditor.Encoding = System.Text.Encoding.UTF8;
                codeEditor.LineNumbersForeground = (Brush)new BrushConverter().ConvertFromString("Green");
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }


            try {
                SearchPanel.Install(codeEditor);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            try {
                codeEditor.TextArea.TextEntering += codeEditor_TextArea_TextEntering;
                codeEditor.TextArea.TextEntered += codeEditor_TextArea_TextEntered;
                DispatcherTimer foldingUpdateTimer = new DispatcherTimer();
                foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
                foldingUpdateTimer.Tick += delegate { UpdateFoldings(); };
                foldingUpdateTimer.Start();

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            _ = LoadDataList();
            SetRecord(false);
        }

        
        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                solutionMixedEnumList = await CommunicationManager.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/CodeTypes", App.UserData.Authentification.Token);
                docSrvDocTemplateList = await CommunicationManager.GetApiRequest<List<DocSrvDocTemplateList>>(ApiUrls.EasyITCenterDocSrvDocTemplateList, null, App.UserData.Authentification.Token);
                webCodeLibraryList = await CommunicationManager.GetApiRequest<List<WebCodeLibraryList>>(ApiUrls.EasyITCenterWebCodeLibraryList, null, App.UserData.Authentification.Token);
                webConfiguratorList = await CommunicationManager.GetApiRequest<List<WebConfiguratorList>>(ApiUrls.EasyITCenterWebConfiguratorList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);


                DgListView.ItemsSource = webConfiguratorList;
                lb_dataList.ItemsSource = webCodeLibraryList.Where(a => !a.IsCompletion);
                cb_codeType.ItemsSource = solutionMixedEnumList;
                cb_selectTemplates.ItemsSource = docSrvDocTemplateList.OrderBy(a => a.GroupId).ThenBy(a=>a.Sequence).ToList();
                DgListView.Items.Refresh();


                AnimatedSearchInput = (Storyboard)FindResource("StoryBoardFlashEffect");
                AnimatedSearchInput.Begin(txt_search, true);
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }

        
        private async void DgListView_Translate(object sender, EventArgs ex) {
            try {
                ((DataGrid)sender).Columns.ToList().ForEach(async e => {
                    string headername = e.Header.ToString().ToLower();
                    if (headername == "Value".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 1; }
                    else if (headername == "IsStartupPage".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 2; }
                    else if (headername == "ServerUrl".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 3; }
                    else if (headername == "AuthRole".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 4; }
                    else if (headername == "AuthIgnore".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 5; }
                    else if (headername == "AuthRedirect".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 6; }
                    else if (headername == "AuthRedirectUrl".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 7; }
                    else if (headername == "IncludedIdList".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 8; }
                    else if (headername == "Active".ToLower()) { e.Header = await DBOperations.DBTranslation(headername); e.DisplayIndex = 9; }

                    else if (headername == "TimeStamp".ToLower()) { e.Header = Resources["timestamp"].ToString(); e.CellStyle = ProgramaticStyles.gridTextRightAligment; e.DisplayIndex = DgListView.Columns.Count - 1; }
                    else if (headername == "Id".ToLower()) e.DisplayIndex = 0;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "Description".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "HtmlContent".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "UserId".ToLower()) e.Visibility = Visibility.Hidden;
                    else if (headername == "HtmlContent".ToLower()) e.Visibility = Visibility.Hidden;
                });
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }

        
        public void Filter(string filter) {
            try {
                if (filter.Length == 0) { dataViewSupport.FilteredValue = null; DgListView.Items.Filter = null; return; }
                dataViewSupport.FilteredValue = filter;
                DgListView.Items.Filter = (e) => {
                    WebConfiguratorList search = e as WebConfiguratorList;
                    return search.Name.ToLower().Contains(filter.ToLower())
                    || search.HtmlContent.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.ServerUrl) && search.ServerUrl.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.AuthRedirectUrl) && search.AuthRedirectUrl.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.HtmlContent) && search.HtmlContent.ToLower().Contains(filter.ToLower())
                    || !string.IsNullOrEmpty(search.Description) && search.Description.ToLower().Contains(filter.ToLower())
                    ;
                };
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }
        
        public void NewRecord() {
            selectedRecord = new WebConfiguratorList();
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        public void EditRecord(bool copy) {
            selectedRecord = (WebConfiguratorList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true, copy);
        }

        public async void DeleteRecord() {
            selectedRecord = (WebConfiguratorList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            MessageDialogResult result = await MainWindow.ShowMessageOnMainWindow(false, await DBOperations.DBTranslation("deleteRecordQuestion") + " " + selectedRecord.Id.ToString(), true);
            if (result == MessageDialogResult.Affirmative) {
                DBResultMessage dBResult = await CommunicationManager.DeleteApiRequest(ApiUrls.EasyITCenterWebCodeLibraryList, selectedRecord.Id.ToString(), App.UserData.Authentification.Token);
                if (dBResult.RecordCount == 0) await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage);
                await LoadDataList(); SetRecord(false);
            }
        }

        private void DgListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (DgListView.SelectedItems.Count == 0) return;
            selectedRecord = (WebConfiguratorList)DgListView.SelectedItem;
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
            SetRecord(true);
        }

        private void DgListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (DgListView.SelectedItems.Count > 0) { selectedRecord = (WebConfiguratorList)DgListView.SelectedItem; }
            else { selectedRecord = new WebConfiguratorList(); }
            dataViewSupport.SelectedRecordId = selectedRecord.Id;
        }

        
        private async void SetRecord(bool? showForm = null, bool copy = false) {
           try {
                //txt_id.Value = (copy) ? 0 : selectedRecord.Id;

                //lb_dataList.SelectedItem = selectedRecord.Id == 0 ? null : selectedRecord;
                //txt_name.Text = selectedRecord.Name;
                //txt_description.Text = selectedRecord.Description;

                //if ((bool)EditorSelector.IsChecked) { html_htmlContent.Browser.OpenDocument(selectedRecord.Content); }
                //else { txt_codeContent.Text = selectedRecord.Content; }
               

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


        private async void BtnSave_Click(object sender, RoutedEventArgs e) {
            try {
               
                DBResultMessage dBResult;
                //selectedRecord.Id = (int)((txt_id.Value != null) ? txt_id.Value : 0);
                //selectedRecord.WebsiteName = txt_websiteName.Text;
                //selectedRecord.Description = txt_description.Text;

                //selectedRecord.MinimalReadAccessValue = ((SolutionUserRoleList)cb_minimalReadAccessValue.SelectedItem).MinimalAccessValue;
                //selectedRecord.MinimalWriteAccessValue = ((SolutionUserRoleList)cb_minimalWriteAccessValue.SelectedItem).MinimalAccessValue;

                //selectedRecord.UserId = App.UserData.Authentification.Id;
                //selectedRecord.Active = (bool)chb_active.IsChecked;
                selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

                string json = JsonConvert.SerializeObject(selectedRecord);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                if (selectedRecord.Id == 0) {
                    dBResult = await CommunicationManager.PutApiRequest(ApiUrls.SolutionWebsiteList, httpContent, null, App.UserData.Authentification.Token);
                }
                else { dBResult = await CommunicationManager.PostApiRequest(ApiUrls.SolutionWebsiteList, httpContent, null, App.UserData.Authentification.Token); }

                if (dBResult.RecordCount > 0) {
                    selectedRecord = new WebConfiguratorList();
                    await LoadDataList();
                    SetRecord(null);
                } else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }


        //Save Code Library Item
        private async void BtnSaveCodePopup_Click(object sender, RoutedEventArgs e) {
            try {
                DBResultMessage dBResult;
                WebCodeLibraryList codeItem = new WebCodeLibraryList() {
                    Id = 0, Name = txt_codeName.Text, Description = txt_description.Text,
                    Content = codeReview.Text, InheritedCodeType = ((SolutionMixedEnumList)cb_codeType.SelectedItem).Name.ToString(),
                    IsCompletion = true, UserId = App.UserData.Authentification.Id, TimeStamp = DateTimeOffset.Now.DateTime
                };
                string json = JsonConvert.SerializeObject(codeItem);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                dBResult = await CommunicationManager.PutApiRequest(ApiUrls.EasyITCenterWebCodeLibraryList, httpContent, null, App.UserData.Authentification.Token);
                if (dBResult.RecordCount > 0) {
                    webCodeLibraryList = await CommunicationManager.GetApiRequest<List<WebCodeLibraryList>>(ApiUrls.EasyITCenterWebCodeLibraryList, null, App.UserData.Authentification.Token);
                    pop_codeLibraryForm.IsOpen = false; 
                } else { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }
        private void BtnCloseCodePopup_Click(object sender, RoutedEventArgs e) => pop_codeLibraryForm.IsOpen = false;
        private void BtnCancel_Click(object sender, RoutedEventArgs e) {
            selectedRecord = (DgListView.SelectedItems.Count > 0) ? (WebConfiguratorList)DgListView.SelectedItem : new WebConfiguratorList();
            SetRecord(false);
        }


        private void DataListDoubleClick(object sender, MouseButtonEventArgs e) {
            if (lb_dataList.SelectedItems.Count > 0) {
                codeHelp.Text = ((WebCodeLibraryList)lb_dataList.SelectedItem).Content;
                ti_helpCode.IsSelected = true;
            }
        }








        //CODE EDITORS  
        #region Files Nuttons

        private void newFile_Click(object sender, RoutedEventArgs e) {
            lbl_fileName.Text = "undefined";
            currentFileName = codeEditor.Text = null;
        }

        void openFileClick(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog(); dlg.CheckFileExists = true;
            if (dlg.ShowDialog() ?? false) {
                currentFileName = dlg.FileName; lbl_fileName.Text = dlg.SafeFileName;
                codeEditor.Load(dlg.FileName);
                codeEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(currentFileName));
            }
        }

        void saveFileClick(object sender, EventArgs e) {
            if (currentFileName == null) {
                SaveFileDialog dlg = new SaveFileDialog();
                if (dlg.ShowDialog() ?? false) {
                    currentFileName = dlg.FileName;
                    lbl_fileName.Text = dlg.SafeFileName;
                }
                else { return; }
            }
            codeEditor.Save(currentFileName);
        }

        private void saveAsFileClick(object sender, RoutedEventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog(); //dlg.DefaultExt = ".txt";
            if (dlg.ShowDialog() ?? false) {
                lbl_fileName.Text = currentFileName = dlg.FileName;
            }
            else { return; }
        }

        private void miShowHelp_Click(object sender, RoutedEventArgs e) => ShowComlpetion();
        void codeEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e) {
            // open code completion after the user has pressed dot:
            if (e.Text == ".") { ShowComlpetion(); }
        }


        private void ShowComlpetion() {
            completionWindow = new CompletionWindow(codeEditor.TextArea); completionWindow.Width = 300;
            IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
            webCodeLibraryList.Where(a => a.IsCompletion).ToList().ForEach(codeHelp => { data.Add(new MyCompletionData(codeHelp.Content)); });
            if (data.Count > 0) { completionWindow.Show(); completionWindow.Closed += delegate { completionWindow = null; }; }
        }

        void codeEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e) {
            if (e.Text.Length > 0 && completionWindow != null) {
                if (!char.IsLetterOrDigit(e.Text[0])) { completionWindow.CompletionList.RequestInsertion(e); }
            }
        }
        #endregion Files Nuttons

        #region Folding
        FoldingManager foldingManager;
        object foldingStrategy;

        void HighlightingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
                codeEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ComboBox)sender).SelectedValue.ToString());
            if (codeEditor.SyntaxHighlighting == null) {
                foldingStrategy = null;
            }
            else {
                    switch (codeEditor.SyntaxHighlighting.Name) {
                        case "XML":
                            foldingStrategy = new XmlFoldingStrategy();
                            codeEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                            break;
                        case "C#":
                            codeEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(codeEditor.Options);
                            foldingStrategy = new BraceFoldingStrategy();
                        break;
                        case "Java":
                            codeEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(codeEditor.Options);
                            foldingStrategy = new BraceFoldingStrategy();
                            break;
                        default:
                            codeEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                            foldingStrategy = null;
                            break;
                    }
                    if (foldingStrategy != null) {
                        if (foldingManager == null)
                            foldingManager = FoldingManager.Install(codeEditor.TextArea);
                        UpdateFoldings();
                    }
                    else {
                        if (foldingManager != null) {
                            FoldingManager.Uninstall(foldingManager);
                            foldingManager = null;
                        }
                    }
                }
            }


        void UpdateFoldings() {
            if (foldingStrategy is BraceFoldingStrategy) { ((BraceFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, codeEditor.Document); }
            if (foldingStrategy is XmlFoldingStrategy) { ((XmlFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, codeEditor.Document); }
        }
        #endregion


        #region CodeEditor

        ITextMarkerService textMarkerService;

        void InitializeTextMarkerService() {
            var textMarkerService = new TextMarkerService(codeEditor.Document);
            codeEditor.TextArea.TextView.BackgroundRenderers.Add(textMarkerService);
            codeEditor.TextArea.TextView.LineTransformers.Add(textMarkerService);
            IServiceContainer services = (IServiceContainer)codeEditor.Document.ServiceProvider.GetService(typeof(IServiceContainer));
            if (services != null)
                services.AddService(typeof(ITextMarkerService), textMarkerService);
            this.textMarkerService = textMarkerService;
        }


        void RemoveAllClick(object sender, RoutedEventArgs e) => textMarkerService.RemoveAll(m => true);
        void RemoveSelectedClick(object sender, RoutedEventArgs e) => textMarkerService.RemoveAll(IsSelected);
        void AddMarkerFromSelectionClick(object sender, RoutedEventArgs e) {
            ITextMarker marker = textMarkerService.Create(codeEditor.SelectionStart, codeEditor.SelectionLength);
            marker.MarkerTypes = TextMarkerTypes.SquigglyUnderline;
            marker.MarkerColor = Colors.Red;
        }

        bool IsSelected(ITextMarker marker) {
            int selectionEndOffset = codeEditor.SelectionStart + codeEditor.SelectionLength;
            if (marker.StartOffset >= codeEditor.SelectionStart && marker.StartOffset <= selectionEndOffset)
                return true;
            if (marker.EndOffset >= codeEditor.SelectionStart && marker.EndOffset <= selectionEndOffset)
                return true;
            return false;
        }


        /// <summary>
        /// UniversalEditor Theme Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTheme_Click(object sender, RoutedEventArgs e) {

            if (btn_theme.Background == (Brush)new BrushConverter().ConvertFromString(lightThemeName)) {
                codeEditor.Background = (Brush)new BrushConverter().ConvertFromString(lightThemeName);
                btn_theme.Background = (Brush)new BrushConverter().ConvertFromString(darkThemeName);
            }
            else {
                codeEditor.Background = (Brush)new BrushConverter().ConvertFromString(darkThemeName);
                btn_theme.Background = (Brush)new BrushConverter().ConvertFromString(lightThemeName);
            }
        }


        /// <summary>
        /// Set Selected Template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateSelected(object sender, SelectionChangedEventArgs e) {
            if (((ComboBox)sender).SelectedIndex > -1) {
                lbl_fileName.Text = "undefined";
                currentFileName = codeEditor.Text = ((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Template;

                cb_selectCode.SelectedIndex = -1;

                string selectHighlight = "C#";
                if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("C# ")) { selectHighlight = "C#"; }
                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().Contains("CSHTML")) { selectHighlight = "C#"; }

                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("XAML ")) { selectHighlight = "XML"; }

                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("SP ")) { selectHighlight = "TSQL"; }
                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("MSSQL ")) { selectHighlight = "TSQL"; }
                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("TR ")) { selectHighlight = "TSQL"; }
                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("TBL ")) { selectHighlight = "TSQL"; }
                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("FN ")) { selectHighlight = "TSQL"; }

                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("JS ")) { selectHighlight = "JavaScript"; }
                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("CSS ")) { selectHighlight = "CSS"; }
                else if (((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Name.ToUpper().StartsWith("HTML ")) { selectHighlight = "HTML"; }

                cb_selectCode.SelectedItem = HighlightingManager.Instance.HighlightingDefinitions
                    .Where(a => a.Name == selectHighlight).FirstOrDefault();

                ((ComboBox)sender).SelectedIndex = -1;

                cb_selectCode.SelectedItem = HighlightingManager.Instance.HighlightingDefinitions
                    .Where(a => a.Name == selectHighlight).FirstOrDefault();

            }
        }

        #endregion CodeEditor

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) => toolBar.IsEnabled = TabMenuList.SelectedIndex == 0;
        private void txtSearchFocusChanged(object sender, RoutedEventArgs e) { if (txt_search.IsFocused) { AnimatedSearchInput.Stop(txt_search); } else { AnimatedSearchInput.Begin(txt_search,true); } }


        //Open And Preset Popup
        private void miInsertToCodeLibrary(object sender, RoutedEventArgs e) {
            try {
                if (codeEditor.SelectedText.Length > 0) {
                    cb_codeType.Text = cb_selectCode.SelectedValue.ToString() == "C#" ? "Csharp" : cb_selectCode.SelectedValue.ToString().ToLower().Contains("html")
                        ? "Html" : cb_selectCode.SelectedValue.ToString().ToLower().Contains("xml") ? "Xaml" : cb_selectCode.SelectedValue.ToString() == "JavaScript" ? "JavaScript"
                        : cb_selectCode.SelectedValue.ToString().ToLower().Contains("css") ? "Css" : cb_selectCode.SelectedValue.ToString() == "TSQL" ? "MSSQL" : "JavaScript";

                    codeReview.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(cb_selectCode.SelectedValue.ToString());
                    codeReview.Text = codeEditor.SelectedText;
                    pop_codeLibraryForm.IsOpen = true;
                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
        }


    }

}
