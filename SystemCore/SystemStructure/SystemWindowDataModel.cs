using Dragablz;
using EasyITSystemCenter.GlobalOperations;
using EasyITSystemCenter.Pages;
using EasyITSystemCenter.Properties;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace EasyITSystemCenter.SystemStructure {

    public class SystemWindowDataModel {
        private IInterTabClient _interTabClient = new DefaultInterTabClient();
        private readonly ObservableCollection<SystemTabs> _tabContents = new ObservableCollection<SystemTabs>();

        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<SystemTheme> AppThemes { get; set; }
        private SystemWindowDataModel mwm;

        /// <summary>
        /// Its Solution For MultiInstance Application If The InterTab Is Enabled Can be Dragged Tab
        /// To the New Application
        /// </summary>
        public SystemWindowDataModel() {
            _interTabClient = new DefaultInterTabClient();
            LoadTheme();
            mwm = this;
        }

        #region Theme

        public void LoadTheme() {
            // create accent color menu items for the demo
            this.AccentColors = ThemeManager.Accents.Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush }).ToList();

            // create metro theme color menu items for the demo
            this.AppThemes = ThemeManager.AppThemes.Select(
                    a =>
                        new SystemTheme() {
                            Name = a.Name,
                            BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                            ColorBrush = a.Resources["WhiteColorBrush"] as Brush
                        })
                .ToList();

            AppTheme theme = ThemeManager.AppThemes.FirstOrDefault(t => t.Name.Equals(Settings.Default.apper_themeName));
            Accent accent = ThemeManager.Accents.FirstOrDefault(a => a.Name.Equals(Settings.Default.apper_accentName));

            if ((theme != null) && (accent != null)) {
                ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
            }
        }

        public static void SaveTheme() {
            Settings.Default.Height = Application.Current.MainWindow.Height;
            Settings.Default.Width = Application.Current.MainWindow.Width;
            Settings.Default.Save();
        }

        #endregion Theme

        public ObservableCollection<SystemTabs> TabContents {
            get { return _tabContents; }
        }

        public IInterTabClient InterTabClient {
            get { return _interTabClient; }
        }

        public static Func<object> NewItemFactory {
            get { return () => new SystemTabs(null, "SupportPage", new SupportPage(), "Setting"); }
        }





        //    /// <summary>
        //    /// Gets the app inter tab client.
        //    /// </summary>
        //    public IInterTabClient AppInterTabClient {
        //        get { return _interTabClient; }
        //    }

        //    public class InterTabClient : IInterTabClient {
        //        private IInterTabClient _interTabClient = new DefaultInterTabClient();

        //        private static Task HandleOverlayOnShow(MetroDialogSettings settings, MetroWindow window) {
        //            return (settings == null || settings.AnimateShow ? window.ShowOverlayAsync() : Task.Factory.StartNew(() => window.Dispatcher.Invoke(new System.Action(() => window.ShowOverlay()))));
        //        }

        //        public virtual INewTabHost<Window> GetNewHost(IInterTabClient interTabClient, object partition, TabablzControl source) {
        //            if (source == null) throw new ArgumentNullException("source");
        //            var sourceWindow = Window.GetWindow(source);
        //            if (sourceWindow == null) throw new ApplicationException("Unable to ascertain source window.");

        //            MetroWindow newAppInstance = new MainWindow();
        //            newAppInstance.Show();
        //            var vm = IoC.Get<SystemWindowDataModel>();
        //            ViewModelBinder.Bind(vm, newWindow, null);

        //            newWindow.Dispatcher.Invoke(new System.Action(() => { }), DispatcherPriority.DataBind);

        //            var newTabablzControl = newAppInstance.LogicalTreeDepthFirstTraversal().OfType<TabablzControl>().FirstOrDefault();
        //            if (newTabablzControl == null) throw new ApplicationException("Unable to ascertain tab control.");

        //            if (newTabablzControl.ItemsSource == null)
        //                newTabablzControl.Items.Clear();

        //            return new NewTabHost<Window>(newAppInstance, newTabablzControl);
        //        }

        //        public virtual TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window) {
        //            return TabEmptiedResponse.CloseWindowOrLayoutBranch;
        //        }
        //    }
        //}

        //static class Extensions {
        //    public static IEnumerable<object> LogicalTreeDepthFirstTraversal(this DependencyObject node) {
        //        if (node == null) yield break;
        //        yield return node;

        //        foreach (var child in LogicalTreeHelper.GetChildren(node).OfType<DependencyObject>()
        //            .SelectMany(depObj => depObj.LogicalTreeDepthFirstTraversal()))
        //            yield return child;
        //    }
        //}
    }
    
}