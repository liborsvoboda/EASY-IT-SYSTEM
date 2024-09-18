<UserControl
    x:Class="GoldenSystem.Pages.TemplateWebViewPage"
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

        <Grid x:Name="ListView" Visibility="Visible">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="*" />
            </Grid.RowDefinitions>

            <wpf:ChromiumWebBrowser
                x:Name="webViewer" Grid.Row="0" Grid.Column="0" HorizontalAlignment="Stretch" VerticalAlignment="Stretch"
                IsEnabled="True" />
        </Grid>

    </Grid>
</UserControl>
----------------------------------------------------------------------------------------------------------------

using CefSharp;
using GoldenSystem.Classes;
using GoldenSystem.GlobalOperations;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Controls;

namespace GoldenSystem.Pages {

    /// <summary>
    /// Template Page For internet pages document, pictures, text and and much more file formats opened in WebViewer
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
            _ = SystemOperations.SetLanguageDictionary(Resources, JsonConvert.DeserializeObject<Language>(App.Setting.DefaultLanguage).Value);

            try {
                webViewer.BeginInit();
                webViewer.Address = Path.Combine("http://localhost:8000/");
                webViewer.FrameLoadStart += WebViewer_FrameLoadStart;
                webViewer.FrameLoadEnd += WebViewer_FrameLoadEnd;
                webViewer.LifeSpanHandler = new MyCustomLifeSpanHandler();
            } catch (Exception ex) { }
        }

        private void WebViewer_FrameLoadStart(object sender, FrameLoadStartEventArgs e) {
            MainWindow.ProgressRing = System.Windows.Visibility.Visible;
        }

        private void WebViewer_FrameLoadEnd(object sender, FrameLoadEndEventArgs e) {
            MainWindow.ProgressRing = System.Windows.Visibility.Hidden;
        }

        private class MyCustomLifeSpanHandler : ILifeSpanHandler {

            public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser) {
                return true;
            }

            public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser) {
            }

            public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser) {
            }

            /// <summary>
            /// Block open New Solo Window Frame as popup
            /// </summary>
            /// <param name="chromiumWebBrowser"></param>
            /// <param name="browser"></param>
            /// <param name="frame"></param>
            /// <param name="targetUrl"></param>
            /// <param name="targetFrameName"></param>
            /// <param name="targetDisposition"></param>
            /// <param name="userGesture"></param>
            /// <param name="popupFeatures"></param>
            /// <param name="windowInfo"></param>
            /// <param name="browserSettings"></param>
            /// <param name="noJavascriptAccess"></param>
            /// <param name="newBrowser"></param>
            /// <returns></returns>
            public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser) {
                browser.MainFrame.LoadUrl(targetUrl);
                newBrowser = null;
                return true;
            }
        }
    }
}