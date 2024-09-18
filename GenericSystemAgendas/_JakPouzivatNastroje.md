# XAML  Nastoje a Jejich Definice   
- xmlns:xctk="http://schemas.xceed.com/wpf/xaml/toolkit"
- xmlns:html="clr-namespace:EASYTools.HTMLFullEditor;assembly=EASYTools.HTMLFullEditor"
- xmlns:code="http://icsharpcode.net/sharpdevelop/avalonedit"
- xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks"
- xmlns:md="clr-namespace:EASYTools.MarkdownToFlow;assembly=EASYTools.MarkdownToFlow" 
- xmlns:child="clr-namespace:MahApps.Metro.SimpleChildWindow;assembly=MahApps.Metro.SimpleChildWindow"
- xmlns:lvc="clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf"


1. Frameworky mus definovat Hlavicku
- xmlns="https://github.com/avaloniaui"

2. Local  pro Binding
- xmlns:local="clr-namespace:CustomStyle;assembly=CustomStyle" 

Standard
- xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
- xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
- xmlns:mah="http://metro.mahapps.com/winfx/xaml/controls"
- xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
- xmlns:behaviors="http://schemas.microsoft.com/xaml/behaviors"
- xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"


---   
# AvalonEdit

1. XAML Selector Jazya
<ListBox
    x:Name="code_selector" Grid.Row="4" Grid.Column="1" Width="100" HorizontalAlignment="Left"
    SelectionChanged="HighlightCodeChanged">
    <ListBoxItem Content="JavaScript" />
    <ListBoxItem Content="HTML" />
    <ListBoxItem Content="C#" />
    <ListBoxItem Content="XML" />
    <ListBoxItem Content="Java" />
    <ListBoxItem Content="C++" />
</ListBox>

2. Implementace Editoru
    <code:TextEditor
    x:Name="md_editor" Grid.Row="0" Grid.Column="0" Margin="0,2,0,2" FontFamily="Consolas"
    FontSize="10pt" HorizontalScrollBarVisibility="Auto" ShowLineNumbers="True" SyntaxHighlighting="JavaScript" TextChanged="CodeEditorChanged"
    VerticalScrollBarVisibility="Auto" />

3. Zmena Jazyka
-    private void HighlightCodeChanged(object sender, SelectionChangedEventArgs e) {
        md_editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(((ListBoxItem)code_selector.SelectedValue).Content.ToString());
    }

4. Vyhledavani a Replace
-  if (dataViewSupport.FormShown && btn_searchText != null) { btn_searchText.IsEnabled = true; FoundedPositionIndex = ReplacePositionIndex = 0; }
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
            ToolsOperations.AvalonEditorFindText(txt_codeSearch.Text, ref FoundedPositionIndex, ref md_editor, (bool)chb_caseSensitiveIgnore.IsChecked);
            if (FoundedPositionIndex == 0) { btn_searchText.IsEnabled = false; }
        }

        private void CodeReplaceClick(object sender, RoutedEventArgs e) {
            ToolsOperations.AvalonEditorReplaceText(txt_codeSearch.Text, txt_codeReplace.Text, ref ReplacePositionIndex, ref md_editor, (bool)chb_caseSensitiveIgnore.IsChecked, (bool)chb_selectedOnly.IsChecked);
            if (ReplacePositionIndex == 0) { btn_codeReplace.IsEnabled = false; }
        }

5. Otevreni v prohliyeci
-   private async void BtnOpenInBrowser_Click(object sender, RoutedEventArgs e) {
    await SaveRecord(false, false);
    SystemOperations.StartExternalProccess(SystemLocalEnumSets.ProcessTypes.First(a => a.Value.ToLower() == "url").Value, App.appRuntimeData.AppClientSettings.First(b => b.Key == "conn_apiAddress").Value + (await DataOperations.ParameterCheck("WebDocPreview")) + "/" + txt_id.Value.ToString());
}

---

# MarkDownFlow 
1. Nasayeni XAML
    <UserControl.Resources>
        <!--<md:Markdown
            x:Key="Markdown"
            DocumentStyle="{StaticResource DocumentStyle}"
            Heading1Style="{StaticResource H1Style}"
            Heading2Style="{StaticResource H2Style}"
            Heading3Style="{StaticResource H3Style}"
            Heading4Style="{StaticResource H4Style}"
            ImageStyle="{StaticResource ImageStyle}"
            LinkStyle="{StaticResource LinkStyle}"
            SeparatorStyle="{StaticResource SeparatorStyle}"
            TableHeaderStyle="{StaticResource TableHeaderStyle}"
            TableStyle="{StaticResource TableStyle}" />

        <md:TextToFlowDocumentConverter x:Key="TextToFlowDocumentConverter" Markdown="{StaticResource Markdown}" />-->
    </UserControl.Resources>



--- 

# HTMLFullEditor

1. Implmentace XAML
<html:HtmlEditor
    x:Name="html_description" Grid.Row="2" Grid.RowSpan="2" Grid.Column="0" Grid.ColumnSpan="2"
    Margin="0,2,0,2" BorderThickness="0.5"
    HtmlContent="{Binding SourceCode}" />


2. Vrátí HTML obsah
- selectedRecord.HtmlContent = html_htmlContent.Browser.GetCurrentHtml();

3. Naèítá Obsah
//html_htmlContent.Browser.OpenDocument(markdown.Transform(md_editor.Text));

4. Zmena Editoru
    private void CodeEditorChanged(object sender, EventArgs e) {
        EASYTools.MarkdownToHtml.Markdown markdown = new EASYTools.MarkdownToHtml.Markdown();
        html_htmlContent.Browser.OpenDocument(markdown.Transform("<HEAD><META content=text/html;utf-8 http-equiv=content-type></HEAD>" + md_editor.Text));
    }

5. Nastaveni A prehozeni Pohledu CODE/HTML
-  html_htmlMessage.HtmlContentDisableInitialChange = true;
   html_htmlMessage.Toolbar.SetSourceMode(true);
   html_htmlMessage.Browser.ToggleSourceEditor(html_htmlMessage.Toolbar, true);

---

# MarkDig Stejne pro EIC&ESB
1. Implmementace Nástroje
- XAML
    xmlns:md="clr-namespace:Markdig.Wpf;assembly=Markdig.Wpf"

<md:MarkdownViewer x:Name="mdViewer" Grid.Row="0" Grid.Column="1" />

2. Definice enginu
- new MarkdownPipelineBuilder().UseEmphasisExtras().UseAbbreviations().UseAdvancedExtensions().UseBootstrap()
.UseDiagrams().UseEmphasisExtras().UseEmojiAndSmiley().UseDefinitionLists().UseTableOfContent().UseTaskLists()
.UseSupportedExtensions().UseSmartyPants().UsePipeTables().UseMediaLinks().UseMathematics().UseListExtras().UseHighlightJs()
.UseGridTables().UseGlobalization().UseGenericAttributes().UseFootnotes().UseFooters().UseSyntaxHighlighting().UseFigures().Build();

3. Vraceni MarkDown
-   selectedRecord.MdContent = md_editor.Text;

4. Vraceni Html
- selectedRecord.HtmlContent = Markdig.Markdown.ToHtml(mdViewer.Markdown);

5. Implpementace Testu Odkazu
- XAML
 
    <FrameworkElement.CommandBindings>
        <CommandBinding Command="{x:Static md:Commands.Hyperlink}" Executed="OpenHyperlink" />
        <CommandBinding Command="{x:Static md:Commands.Image}" Executed="ClickOnImage" />
    </FrameworkElement.CommandBindings>

- CS
    private void OpenHyperlink(object sender, ExecutedRoutedEventArgs e) {
        Process.Start(e.Parameter.ToString());
    }

    private void ClickOnImage(object sender, ExecutedRoutedEventArgs e) {
        MessageBox.Show($"URL: {e.Parameter}");
    }

6. Exporty HTML,Docs,Md
- XAML
    <Button
        x:Name="ExportDocx" Grid.Row="10" Grid.Column="0" Width="80" Height="40"
        Margin="5" Padding="5" HorizontalAlignment="Left" VerticalAlignment="Top"
        Click="BtnExportDocx_Click" Content="Export Docx" Cursor="Hand" />

    <Button
        x:Name="ExportHtml"
        Grid.Row="10" Grid.Column="0"
        Width="80" Height="40" VerticalAlignment="Top"
        Margin="5" Padding="5" HorizontalAlignment="Center"
        Click="BtnExportHtml_Click" Content="Export Html" Cursor="Hand" />

    <Button
        x:Name="ExportMd" Grid.Row="10" Grid.Column="0"
        Width="80" Height="40" VerticalAlignment="Top"
        Margin="5" Padding="5" HorizontalAlignment="Right"
        Click="BtnExportMd_Click" Content="Export Md" Cursor="Hand" />

- CS
    private void BtnExportDocx_Click(object sender, RoutedEventArgs e) {
    var document = DocxTemplateHelper.Standard;
    var styles = new DocumentStyles();
    var renderer = new DocxDocumentRenderer(document, styles, NullLogger<DocxDocumentRenderer>.Instance);
    var pipeline = new MarkdownPipelineBuilder().UseEmphasisExtras().UseAbbreviations().UseAdvancedExtensions().UseBootstrap()
        .UseDiagrams().UseEmphasisExtras().UseEmojiAndSmiley().UseDefinitionLists().UseTableOfContent().UseTaskLists()
        .UseSupportedExtensions().UseSmartyPants().UsePipeTables().UseMediaLinks().UseMathematics().UseListExtras().UseHighlightJs()
        .UseGridTables().UseGlobalization().UseGenericAttributes().UseFootnotes().UseFooters().UseSyntaxHighlighting().UseFigures().Build();
    object exportedFile = Markdig.Markdown.Convert(mdViewer.Markdown, renderer, pipeline);

    SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".docx", Filter = "Word files |*.docx", Title = Resources["fileOpenDescription"].ToString() };
    if (dlg.ShowDialog() == true) { ((DocxDocumentRenderer)exportedFile).Document.SaveAs(dlg.FileName); }
    }

    private void BtnExportHtml_Click(object sender, RoutedEventArgs e) {
    string exportedFile = Markdig.Markdown.ToHtml(mdViewer.Markdown);
    SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".html", Filter = "Html files |*.html", Title = Resources["fileOpenDescription"].ToString() };
    if (dlg.ShowDialog() == true) { FileOperations.ByteArrayToFile(dlg.FileName, System.Text.Encoding.UTF8.GetBytes(exportedFile)); }
    }

    private void BtnExportMd_Click(object sender, RoutedEventArgs e) {
    SaveFileDialog dlg = new SaveFileDialog { DefaultExt = ".md", Filter = "Md files |*.md", Title = Resources["fileOpenDescription"].ToString() };
    if (dlg.ShowDialog() == true) { FileOperations.ByteArrayToFile(dlg.FileName, System.Text.Encoding.UTF8.GetBytes(mdViewer.Markdown)); }
    }

---  

# Globální Pravidla a Èlenìní Jednotlivých Prvkù Øešení

Pøíprava na Pøevod Serveru i Systému do Podoby Globálního Konfigurátoru    
je v plném proudu Volného èasu Jediné Osoby.    
Výsledkem Budou Generátory MultiSystémù, MultiServerù, MultiWebù nejrùznìjších Typù   
Galerie, Prezentace, Dokumentace, Weové Stránky, Dotekové Panely, Portály     
Prohlížeèe, Editory, Knihovny, Aplikace, atd...    

Zde jsou vystìtlivky již Implemenotvaných použití a jejich Logik èi významù
Bohužel mì to neživí, takže vývoj probíhá stále jen ve volném èase...


 
**Obecná Legenda pro Generátory/Kongigurátory/Rozhraní**    

> Jaké má Použití:
>* Mlt - MUltiplikaèní konèící Page_Ident
>* Uni - Jednorázová Definice
>* Sys - Systémová Definice  

> Odkud se Bere nebo kdo jej využívá komu Spouží:
>* Ext  - Zdroj je Externí URL
>* Esb  - Zdroj bìží na stanì Systému 
>* Eic  - Zdroj bìží na stanì Serveru
>* Sha  - Sdílená Pro Server i Systém

> Co je Zaè:  
>* Gen - Systémový/Serverový Generátor = Definice 
>* Xml - Xaml Definice 
>* SPA - Statická Webová Definice  
>* API - Poskytovaná API

> Co je cílem: 
>* App - Win/Web Aplikace 
>* Web - Webová Stánka/Webový Systém/Web
>* Doc - Dokumentace
>* Fil - Soubor   
>* Tmp - Template 
>* Frm - Fomuláø + Zobrazení Dat
>* Wie - Zobrazení Dat
>* Set - Nastavení 
>* Cus - Vlastní Specifický
>* Med - Mediální typ: Prezentace,Galerie
>* Grp - Graf 
>* Piv - Controling Pivot Tabulka

>Pouze Pro Folrmul8øe a Agendy:

>* Form Formuláø a datový Pohled 
>* View Pouze datový Pohled 
>* Sett Nastavení, nìjaká unikátní Definice, Editor, Atd


>Ukonèení Pojmenování Formuláøù Systému:
>* PAGE - Každá Formuláøová Definice musí Konèit Page 
>* ClassPage_Ident - Ident muže být UTF8 Text - èíslo je matoucí

LocSrvWeb - Zobrazuje SystemModuly
Mlt Jsou Generator, Nebo Zorazovac stranky, druhy List Help
Mlt Maji Zdroje Server, System, ExternalUrl



