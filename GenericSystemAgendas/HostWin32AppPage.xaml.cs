using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace EasyITSystemCenter.Pages {

    public partial class HostWin32AppPage : UserControl {
        public static DataViewSupport dataViewSupport = new DataViewSupport();
        public static BasicCalendarList selectedRecord = new BasicCalendarList();

        private event EventHandler ShowAppModuleChanged = delegate { };
        private SystemModuleList showAppModule = null;
        public SystemModuleList ShowAppModule {
            get => showAppModule;
            set {
                showAppModule = value;
                ShowAppModuleApplication();
                ShowAppModuleChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct HWND__ { public int unused; }

        private WinControlHost WinControl;


        public HostWin32AppPage() {

            InitializeComponent();
            _ = SystemOperations.SetLanguageDictionary(Resources, App.appRuntimeData.AppClientSettings.First(a => a.Key == "sys_defaultLanguage").Value);

            MainWindow.ProgressRing = Visibility.Visible;

            this.SizeChanged += new SizeChangedEventHandler(OnSizeChanged);
            this.Loaded += new RoutedEventHandler(OnVisibleChanged);
            this.SizeChanged += new SizeChangedEventHandler(OnResize);

            try {

            } catch (Exception ex) { App.ApplicationLogging(ex); }

            MainWindow.ProgressRing = Visibility.Hidden;
        }

        private async void ShowAppModuleApplication() {

            //Window mainWindow = Application.Current.MainWindow;
            //HwndSource mainHandler = (HwndSource)PresentationSource.FromVisual(mainWindow);
            //Process.Start(Path.Combine(App.appRuntimeData.startupPath, "Data", "AddOn", "AppData", ShowAppModule.FolderPath, ShowAppModule.FileName), ShowAppModule.FileName);
            //var childHandler = Process.GetCurrentProcess().MainWindowHandle;

            //WinControl = new WinControlHost(WinHostElement.ActualHeight, WinHostElement.ActualWidth); 
            //WinControl.childHost = childHandler;
            //WinHostElement.Child = WinControl;
            //WinControl.ConvertToChildWindow(mainHandler.CreateHandleRef());

            if (_iscreated == false) {
                _iscreated = true;
                _appWin = IntPtr.Zero;

                try {
                    var procInfo = new ProcessStartInfo(Path.Combine(App.appRuntimeData.startupPath, "Data", "AddOn", "AppData", ShowAppModule.FolderPath, ShowAppModule.FileName));
                    procInfo.WorkingDirectory = Path.GetDirectoryName(Path.Combine(App.appRuntimeData.startupPath, "Data", "AddOn", "AppData", ShowAppModule.FolderPath));
                    _childp = Process.Start(procInfo);
                    _childp.WaitForInputIdle();
                    _appWin = _childp.MainWindowHandle;
                } catch (Exception ex) { Debug.Print(ex.Message + "Error");}

                var helper = new WindowInteropHelper(Window.GetWindow(this.AppContainer));
                SetParent(_appWin, helper.Handle);
                SetWindowLongA(_appWin, GWL_STYLE, WS_VISIBLE);
                MoveWindow(_appWin, 0, 100, (int)this.ActualWidth, (int)this.ActualHeight, true);




            }
        }

        ~HostWin32AppPage() {
            this.Dispose();
        }
        /// <summary>
        /// Track if the application has been created
        /// </summary>
        private bool _iscreated = false;

        /// <summary>
        /// Track if the control is disposed
        /// </summary>
        private bool _isdisposed = false;

        /// <summary>
        /// Handle to the application Window
        /// </summary>
        IntPtr _appWin;

        private Process _childp;

        /// <summary>
        /// The name of the exe to launch
        /// </summary>
        private string exeName = "";

        public string ExeName {
            get {
                return exeName;
            }
            set {
                exeName = value;
            }
        }


        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true,
             CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        private static extern long GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        public static extern int SetWindowLongA([InAttribute()] IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;


        /// <summary>
        /// Force redraw of control when size changes
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnSizeChanged(object s, SizeChangedEventArgs e) {
            this.InvalidateVisual();
        }


        /// <summary>
        /// Create control when visibility changes
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnVisibleChanged(object s, RoutedEventArgs e) {
            if (_iscreated == false) {
                _iscreated = true;
                _appWin = IntPtr.Zero;

                try {
                    var procInfo = new ProcessStartInfo(this.exeName);
                    procInfo.WorkingDirectory = Path.GetDirectoryName(this.exeName);
                    _childp = Process.Start(procInfo);
                    _childp.WaitForInputIdle();
                    _appWin = _childp.MainWindowHandle;
                } catch (Exception ex) {
                    Debug.Print(ex.Message + "Error");
                }

                var helper = new WindowInteropHelper(Window.GetWindow(this.AppContainer));
                SetParent(_appWin, helper.Handle);
                SetWindowLongA(_appWin, GWL_STYLE, WS_VISIBLE);
                MoveWindow(_appWin, 0, 100, (int)this.ActualWidth, (int)this.ActualHeight, true);
            }
        }

        /// <summary>
        /// Update display of the executable
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnResize(object s, SizeChangedEventArgs e) {
            if (this._appWin != IntPtr.Zero) {
                MoveWindow(_appWin, 0, 100, (int)this.ActualWidth, (int)this.ActualHeight, true);
            }
        }

        protected virtual void Dispose(bool disposing) {
            if (!_isdisposed) {
                if (disposing) {
                    if (_iscreated && _appWin != IntPtr.Zero && !_childp.HasExited) {
                        _childp.Kill();
                        _appWin = IntPtr.Zero;
                    }
                }
                _isdisposed = true;
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}