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
using System.Xml;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using System.ComponentModel.Design;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Search;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Folding;



namespace EasyITSystemCenter.Pages {

    public partial class UniversalCodeEditorPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static TemplateClassList selectedRecord = new TemplateClassList();

        private List<DocSrvDocTemplateList> docSrvDocTemplateList = new List<DocSrvDocTemplateList>();

        CompletionWindow completionWindow;
        string currentFileName;
        string lightThemeName = App.appRuntimeData.AppClientSettings.First(a => a.Key == "appe_toolLightThemeName").Value;
        string darkThemeName = App.appRuntimeData.AppClientSettings.First(a => a.Key == "appe_toolDarkThemeName").Value;


        public UniversalCodeEditorPage() {

            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);
            InitializeTextMarkerService();
            try {
                highlightingComboBox.ItemsSource = HighlightingManager.Instance.HighlightingDefinitions;
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            try { 
                this.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
                propertyGridComboBox.SelectedIndex = 2;
                codeEditor.Options.HighlightCurrentLine = true;

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

        }

        public async Task<bool> LoadDataList() {
            MainWindow.ProgressRing = Visibility.Visible;
            try {

                docSrvDocTemplateList = await CommunicationManager.GetApiRequest<List<DocSrvDocTemplateList>>(ApiUrls.EasyITCenterDocSrvDocTemplateList, (dataViewSupport.AdvancedFilter == null) ? null : "Filter/" + WebUtility.UrlEncode(dataViewSupport.AdvancedFilter.Replace("[!]", "").Replace("{!}", "")), App.UserData.Authentification.Token);
                cb_templates.ItemsSource = docSrvDocTemplateList.OrderBy(a => a.GroupId).ThenBy(a=>a.Sequence).ToList();

            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
            MainWindow.ProgressRing = Visibility.Hidden; return true;
        }



        private void newFile_Click(object sender, RoutedEventArgs e) {
            lbl_openedFile.Text = "undefined";
            currentFileName = codeEditor.Text = null;
        }



        void openFileClick(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog() ?? false) {
                currentFileName = dlg.FileName;
                lbl_openedFile.Text = dlg.SafeFileName;
                codeEditor.Load(dlg.FileName);
                codeEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(currentFileName));

            }
        }



        void saveFileClick(object sender, EventArgs e) {
            if (currentFileName == null) {
                SaveFileDialog dlg = new SaveFileDialog();
                if (dlg.ShowDialog() ?? false) {
                    currentFileName = dlg.FileName;
                    lbl_openedFile.Text = dlg.SafeFileName;
                } else { return; }
            }
            codeEditor.Save(currentFileName);
        }


        private void saveAsFileClick(object sender, RoutedEventArgs e) {
            SaveFileDialog dlg = new SaveFileDialog(); //dlg.DefaultExt = ".txt";
            if (dlg.ShowDialog() ?? false) {
                lbl_openedFile.Text = currentFileName = dlg.FileName;
            }
            else { return; }
        }

        void propertyGridComboBoxSelectionChanged(object sender, RoutedEventArgs e) {
            if (propertyGrid == null)
                return;
            switch (propertyGridComboBox.SelectedIndex) {
                case 0:
                    propertyGrid.SelectedObject = codeEditor;
                    break;
                case 2:
                    propertyGrid.SelectedObject = codeEditor.Options;
                    break;
                case 1:
                    propertyGrid.SelectedObject = codeEditor.TextArea;
                    break;
            }
        }

        void codeEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e) {
            // open code completion after the user has pressed dot:
            if (e.Text == ".") {
                completionWindow = new CompletionWindow(codeEditor.TextArea);
                // provide AvalonEdit with the data:
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                data.Add(new MyCompletionData("Item1"));
                data.Add(new MyCompletionData("Item2"));
                data.Add(new MyCompletionData("Item3"));
                data.Add(new MyCompletionData("Another item"));
                completionWindow.Show();
                completionWindow.Closed += delegate {
                    completionWindow = null;
                };
            }
        }

        void codeEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e) {
            if (e.Text.Length > 0 && completionWindow != null) {
                if (!char.IsLetterOrDigit(e.Text[0])) {
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // do not set e.Handled=true - we still want to insert the character that was typed
        }

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
                        case "C++":
                        case "PHP":
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
            if (foldingStrategy is BraceFoldingStrategy) {
                ((BraceFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, codeEditor.Document);
            }
            if (foldingStrategy is XmlFoldingStrategy) {
                ((XmlFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, codeEditor.Document);
            }
        }
        #endregion



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

        void RemoveAllClick(object sender, RoutedEventArgs e) {
            textMarkerService.RemoveAll(m => true);
        }

        void RemoveSelectedClick(object sender, RoutedEventArgs e) {
            textMarkerService.RemoveAll(IsSelected);
        }

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
                lbl_openedFile.Text = "undefined";
                currentFileName = codeEditor.Text = ((DocSrvDocTemplateList)((ComboBox)sender).SelectedItem).Template;

                highlightingComboBox.SelectedIndex = -1;

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

                highlightingComboBox.SelectedItem = HighlightingManager.Instance.HighlightingDefinitions
                    .Where(a => a.Name == selectHighlight).FirstOrDefault();

                ((ComboBox)sender).SelectedIndex = -1;

                highlightingComboBox.SelectedItem = HighlightingManager.Instance.HighlightingDefinitions
                    .Where(a => a.Name == selectHighlight).FirstOrDefault();

            }
        }
    }

}
