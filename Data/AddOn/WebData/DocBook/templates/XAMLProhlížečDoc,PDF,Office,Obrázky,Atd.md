<UserControl
    x:Class="GoldenSystem.Pages.TemplateDocumentViewPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mah="http://metro.mahapps.com/winfx/xaml/controls"
    xmlns:behaviors="clr-namespace:GoldenSystem.Pages"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:dragablz="clr-namespace:Dragablz;assembly=Dragablz"
    xmlns:globalization="clr-namespace:System.Globalization;assembly=mscorlib"
    xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
    xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks"
    xmlns:local="clr-namespace:GoldenSystem"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:sys="clr-namespace:System;assembly=mscorlib"
    xmlns:wpf="clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf"
    xmlns:xctk="http://schemas.xceed.com/wpf/xaml/toolkit"
    Name="Setting"
    HorizontalAlignment="Stretch"
    VerticalAlignment="Stretch"
    d:DesignHeight="500"
    d:DesignWidth="600"
    Tag="Setting"
    mc:Ignorable="d">

    <!--  Standartized Full Page Grid  -->
    <Grid
        Margin="0" HorizontalAlignment="Stretch" VerticalAlignment="Stretch"
        Background="{DynamicResource AccentColorBrush}">

        <!--  Limited for installed XPS support  -->
        <!--<DocumentViewer x:Name="dv_documentViewer" Width="{Binding Path=ActualWidth, ElementName=View}" Height="{Binding Path=ActualHeight, ElementName=View}" />-->
        <wpf:ChromiumWebBrowser
            x:Name="webViewer"
            Width="{Binding Path=ActualWidth, ElementName=View}"
            Height="{Binding Path=ActualHeight, ElementName=View}" />
    </Grid>
</UserControl>

----------------------------------------------------------------------------------------------------------------

using GoldenSystem.Classes;
using GoldenSystem.GlobalOperations;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Controls;

namespace GoldenSystem.Pages {

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
            _ = SystemOperations.SetLanguageDictionary(Resources, JsonConvert.DeserializeObject<Language>(App.Setting.DefaultLanguage).Value);
            webViewer.Address = Path.Combine(App.startupPath, "Data", "xpsDocument.xps");
            //dv_docuentViewer.Document = new XpsDocument(Path.Combine(App.startupPath, "Data", "xpsDocument.xps"), FileAccess.Read).GetFixedDocumentSequence();
        }
    }
}