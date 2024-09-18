<UserControl
    x:Class="GoldenSystem.Pages.TemplateSTLPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mah="http://metro.mahapps.com/winfx/xaml/controls"
    xmlns:HelixToolkit="clr-namespace:HelixToolkit.Wpf;assembly=HelixToolkit.Wpf"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    Width="Auto"
    Height="Auto"
    d:DesignHeight="500"
    d:DesignWidth="600"
    Tag="Setting"
    mc:Ignorable="d">
    <UserControl.Resources>
        <Thickness x:Key="ControlMargin">0 5 0 0</Thickness>
        <Style
            x:Key="NormalCaseColumnHeader"
            BasedOn="{StaticResource MetroDataGridColumnHeader}"
            TargetType="{x:Type DataGridColumnHeader}">
            <Setter Property="mah:ControlsHelper.ContentCharacterCasing" Value="Normal" />
        </Style>
    </UserControl.Resources>

    <Grid Name="configuration">
        <Grid
            Width="Auto" Height="Auto" Margin="0,0,0,0" HorizontalAlignment="Stretch" VerticalAlignment="Stretch"
            ForceCursor="False" ShowGridLines="False">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="300" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
            </Grid.RowDefinitions>

            <HelixToolkit:HelixViewport3D
                x:Name="viewPort3d" Grid.Row="0" Grid.RowSpan="2" Grid.ColumnSpan="2" Margin="0"
                HorizontalContentAlignment="Stretch" Background="Gray" CameraRotationMode="Trackball" IsHeadLightEnabled="True" IsMoveEnabled="True"
                IsTopBottomViewOrientedToFrontBack="False" IsViewCubeEdgeClicksEnabled="True" RotateAroundMouseDownPoint="False" ShowCameraInfo="True" ShowFrameRate="True"
                ShowViewCube="True" ViewCubeHeight="700" ViewCubeVerticalPosition="Top" ViewCubeWidth="150" ZoomAroundMouseDownPoint="True"
                ZoomExtentsWhenLoaded="True">

                <HelixToolkit:HelixViewport3D.Camera>
                    <PerspectiveCamera
                        FarPlaneDistance="1000" FieldOfView="57" LookDirection="0,-330,0" NearPlaneDistance="0.01" Position="0,330,60"
                        UpDirection="0,0,1" />
                </HelixToolkit:HelixViewport3D.Camera>
                <HelixToolkit:SunLight />
            </HelixToolkit:HelixViewport3D>
        </Grid>
    </Grid>
</UserControl>

----------------------------------------------------------------------------------------------------------------

using GoldenSystem.Classes;
using GoldenSystem.GlobalOperations;
using HelixToolkit.Wpf;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

// This is Template Is Customized For Show 3D STL Object
namespace GoldenSystem.Pages {

    public partial class TemplateSTLPage : UserControl {

        /// <summary>
        /// Standartized declaring static page data for global vorking with pages
        /// </summary>
        public static DataViewSupport dataViewSupport = new DataViewSupport();

        public static TemplateClassList selectedRecord = new TemplateClassList();

        public TemplateSTLPage() {
            InitializeComponent();
            Language defaultLanguage = JsonConvert.DeserializeObject<Language>(App.Setting.DefaultLanguage);
            _ = SystemOperations.SetLanguageDictionary(Resources, defaultLanguage.Value);

            ModelVisual3D device3D = new ModelVisual3D();
            device3D.Content = Display3d(Path.Combine(App.startupPath, "Data", "Track.stl")).GetAwaiter().GetResult();
            viewPort3d.Children.Add(device3D);
        }

        private async Task<Model3D> Display3d(string model) {
            Model3D device = null;
            try
            {
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
                ModelImporter import = new ModelImporter();
                device = import.Load(model);
            }
            catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + ex.Message + Environment.NewLine + ex.StackTrace); }
            return device;
        }

        #region helper methods

        private class ExtensionsItem {

            public ExtensionsItem() {
                ident = null;
                isTrue = false;
                asn1OctetString = null;
            }

            public DerObjectIdentifier ident { get; set; }
            public bool isTrue { get; set; }
            public Asn1OctetString asn1OctetString { get; set; }
        }

        #endregion helper methods
    }
}