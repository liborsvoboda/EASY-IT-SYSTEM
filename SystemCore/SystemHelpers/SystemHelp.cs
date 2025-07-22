using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace EasyITSystemCenter.SystemHelper {

    public static class SystemHelp {
        public static RoutedCommand MyHelpCommand = new RoutedCommand("MyHelpCommand", typeof(FrameworkElement), new InputGestureCollection() { new KeyGesture(Key.F1) });

        static SystemHelp() {
            CommandManager.RegisterClassCommandBinding(typeof(FrameworkElement),
             new CommandBinding(MyHelpCommand,
              new ExecutedRoutedEventHandler(Executed),
              new CanExecuteRoutedEventHandler(CanExecute)));
        }

        #region Filename

        public static readonly DependencyProperty FilenameProperty =
         DependencyProperty.RegisterAttached("Filename", typeof(string), typeof(SystemHelp));

        public static string GetFilename(DependencyObject d) {
            return (string)d.GetValue(FilenameProperty);
        }

        public static void SetFilename(DependencyObject d, string value) {
            d.SetValue(FilenameProperty, value);
        }

        #endregion Filename

        #region Keyword

        public static readonly DependencyProperty KeywordProperty =
         DependencyProperty.RegisterAttached("Keyword", typeof(string), typeof(SystemHelp));

        public static string GetKeyword(DependencyObject d) {
            return (string)d.GetValue(KeywordProperty);
        }

        public static void SetKeyword(DependencyObject d, string value) {
            d.SetValue(KeywordProperty, value);
        }

        #endregion Keyword

        #region Command

        public static readonly DependencyProperty CommandProperty =
         DependencyProperty.RegisterAttached("Command", typeof(string), typeof(SystemHelp));

        public static string GetCommand(DependencyObject d) {
            return (string)d.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject d, string value) {
            d.SetValue(CommandProperty, value);
        }

        #endregion Command

        #region Helpers

        private static void CanExecute(object sender, CanExecuteRoutedEventArgs args) {
            if (Mouse.DirectlyOver is DependencyObject el) {
                string fileName = FindFilename(el);
                if (!string.IsNullOrEmpty(fileName))
                    args.CanExecute = true;
            }
        }

        private static void Executed(object sender, ExecutedRoutedEventArgs args) {
            // Call ShowHelp.
            DependencyObject mouseover = Mouse.DirectlyOver as DependencyObject;
            string keyword = FindKeyword(mouseover);
            string command = FindCommand(mouseover);
            if (!string.IsNullOrEmpty(command)) {
                if (command != "_ExitHelpMode") {
                    Window window = new Window() {
                        Title = "Help",
                        Icon = new BitmapImage(new Uri("pack://application:,,,/Common;component/Images/Help-icon.png")),
                        ShowInTaskbar = false,               // don't show the dialog on the taskbar
                        Topmost = true,                      // ensure we're Always On Top
                        ResizeMode = ResizeMode.CanResize,   // remove excess caption bar buttons
                        Owner = Application.Current.MainWindow,
                        Content = command,
                        Width = Application.Current.MainWindow.Width / 2,
                        Height = Application.Current.MainWindow.Height / 2
                    };
                    window.ShowDialog();
                }
            }
            else if (!string.IsNullOrEmpty(keyword)) {
                string helpFileName = FindFilename(mouseover);
                System.Windows.Forms.Help.ShowHelp(null, helpFileName, "start.htm#_" + keyword);
            }
        }

        private static string FindCommand(DependencyObject sender) {
            if (sender != null) {
                string command = GetCommand(sender);
                if (!string.IsNullOrEmpty(command))
                    return command;

                DependencyObject parent;
                if (sender is Visual || sender is Visual3D) {
                    parent = VisualTreeHelper.GetParent(sender);
                }
                else {
                    parent = LogicalTreeHelper.GetParent(sender);
                }
                return FindCommand(parent);
            }
            return null;
        }

        private static string FindKeyword(DependencyObject sender) {
            if (sender != null) {
                string keyword = GetKeyword(sender);
                if (!string.IsNullOrEmpty(keyword))
                    return keyword;

                DependencyObject parent;
                if (sender is Visual || sender is Visual3D) {
                    parent = VisualTreeHelper.GetParent(sender);
                }
                else {
                    parent = LogicalTreeHelper.GetParent(sender);
                }
                return FindKeyword(parent);
            }
            return null;
        }

        private static string FindFilename(DependencyObject sender) {
            if (sender != null) {
                string fileName = GetFilename(sender);
                if (!string.IsNullOrEmpty(fileName))
                    return fileName;

                DependencyObject parent;
                if (sender is Visual || sender is Visual3D) {
                    parent = VisualTreeHelper.GetParent(sender);
                }
                else {
                    parent = LogicalTreeHelper.GetParent(sender);
                }
                if (parent == null && sender as FrameworkElement != null) {
                    parent = (sender as FrameworkElement).Parent;
                }
                return FindFilename(parent);
            }
            return null;
        }

        #endregion Helpers
    }
}