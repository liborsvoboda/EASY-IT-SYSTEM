
Přidání Vazby Formuláře na Mikro číselník

- mění se název skupiny zde: SystemModules
- Definice v Hlavičce  
private List<SolutionMixedEnumList> mixedEnumTypesList = new List<SolutionMixedEnumList>();

- Do LoadDataList:
mixedEnumTypesList = await CommApi.GetApiRequest<List<SolutionMixedEnumList>>(ApiUrls.EasyITCenterSolutionMixedEnumList, "ByGroup/SystemModules", App.UserData.Authentification.Token);
mixedEnumTypesList.ForEach(async item => { item.Translation = await DBOperations.DBTranslation(item.Name); });
cb_moduleType.ItemsSource = mixedEnumTypesList;

ukázka Implementace např.: SystemModuleListPage  

----

Vlastní Implementace Code Editoru 
HighLight, Search, Replace, Přepnutí mezi Editory,

private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e) {
    try {
        OpenFileDialog dlg = new OpenFileDialog() { DefaultExt = ".html", Filter = "Html files |*.html; *.cshtml; *.js; *.css|All files (*.*)|*.*", Title = Resources["fileOpenDescription"].ToString() };
        if (dlg.ShowDialog() == true) {
            if ((bool)EditorSelector.IsChecked) { html_htmlContent.Browser.OpenDocument(File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName))); }
            else { txt_codeContent.Text = File.ReadAllText(dlg.FileName, FileOperations.FileDetectEncoding(dlg.FileName)); }
        }
    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
}

private async void BtnOpenInBrowser_Click(object sender, RoutedEventArgs e) {
    await SaveRecord(false, false);
    SystemOperations.StartExternalProccess(SystemLocalEnumSets.ProcessTypes.First(a => a.Value.ToLower() == "url").Value, App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebBuilderCodePreview")) + "/" + txt_id.Value.ToString());
}

private async Task<bool> SaveRecord(bool closeForm, bool asNew) {
    try {
        MainWindow.ProgressRing = Visibility.Visible;
        DBResultMessage dBResult;
        selectedRecord.Id = (int)((txt_id.Value != null) && !asNew ? txt_id.Value : 0);
        selectedRecord.Name = txt_name.Text;
        selectedRecord.Description = txt_description.Text;

        if ((bool)EditorSelector.IsChecked) { selectedRecord.Content = html_htmlContent.Browser.GetCurrentHtml(); }
        else { selectedRecord.Content = txt_codeContent.Text; }

        selectedRecord.UserId = App.UserData.Authentification.Id;
        selectedRecord.TimeStamp = DateTimeOffset.Now.DateTime;

        string json = JsonConvert.SerializeObject(selectedRecord);
        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        if (selectedRecord.Id == 0) {
            dBResult = await CommApi.PutApiRequest(ApiUrls.EasyITCenterWebCodeLibraryList, httpContent, null, App.UserData.Authentification.Token);
        }
        else { dBResult = await CommApi.PostApiRequest(ApiUrls.EasyITCenterWebCodeLibraryList, httpContent, null, App.UserData.Authentification.Token); }

        if (dBResult.RecordCount > 0) { await LoadDataList(); }
        if (closeForm) { selectedRecord = new WebCodeLibraryList(); SetRecord(false); }
        if (dBResult.RecordCount == 0) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + dBResult.ErrorMessage); }
    } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
    MainWindow.ProgressRing = Visibility.Hidden;
    return true;
}

private void DataListDoubleClick(object sender, MouseButtonEventArgs e) {
    if (lb_dataList.SelectedItems.Count > 0) { selectedRecord = (WebCodeLibraryList)lb_dataList.SelectedItem; }
    dataViewSupport.SelectedRecordId = selectedRecord.Id;
    SetRecord(true);
}

private void HighlightCodeChanged(object sender, SelectionChangedEventArgs e) {
    txt_codeContent.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString());
}

private void EditorSelectorStatus(object sender, RoutedEventArgs e) {
    if ((bool)EditorSelector.IsChecked) {
        html_htmlContent.Browser.OpenDocument(selectedRecord.Content);
        html_htmlContent.Visibility = Visibility.Visible;
        txt_codeContent.Visibility = Visibility.Hidden;
        code_selector.Visibility = Visibility.Hidden;
    }
    else {
        txt_codeContent.Text = selectedRecord.Content;
        code_selector.Visibility = Visibility.Visible;
        txt_codeContent.Visibility = Visibility.Visible;
        html_htmlContent.Visibility = Visibility.Hidden;
    }
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
    ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref txt_codeContent, (bool)chb_caseSensitiveIgnore.IsChecked);
    if (FoundedPositionIndex == 0) { btn_searchText.IsEnabled = false; }
}

private void CodeReplaceClick(object sender, RoutedEventArgs e) {
    ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref txt_codeContent, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked);
    if (ReplacePositionIndex == 0) { btn_codeReplace.IsEnabled = false; }
}

----

