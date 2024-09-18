using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using HelixToolkit.Wpf;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

// This is Template Is Customized For Show 3D STL Object
namespace EasyITSystemCenter.Pages {

    public partial class TemplateSTLPage : UserControl {

        /// <summary>
        /// Standartized declaring static page data for global vorking with pages
        /// </summary>
        public static DataViewSupport dataViewSupport = new DataViewSupport();

        public static TemplateClassList selectedRecord = new TemplateClassList();

        public TemplateSTLPage() {
            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            ModelVisual3D device3D = new ModelVisual3D();
            device3D.Content = Display3d(Path.Combine(App.appRuntimeData.startupPath, "Data", "Media","Track.stl")).GetAwaiter().GetResult();
            viewPort3d.Children.Add(device3D);
        }

        private async Task<Model3D> Display3d(string model) {
            Model3D device = null;
            try {
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
                ModelImporter import = new ModelImporter();
                device = import.Load(model);
            } catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, "Exception Error : " + ex.Message + Environment.NewLine + ex.StackTrace); }
            return device;
        }

    }
}