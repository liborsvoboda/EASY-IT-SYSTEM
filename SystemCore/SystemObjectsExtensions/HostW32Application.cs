using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;


namespace EasyITSystemCenter {

    public class WinControlHost : HwndHost {
        internal const int
            WsChild = 0x40000000,
            WsVisible = 0x10000000,
            LbsNotify = 0x00000001,
            HostId = 0x00000002,
            ListboxId = 0x00000001,
            WsVscroll = 0x00200000,
            WsBorder = 0x00800000;
        const int GWL_STYLE = -16;
        const int GWL_EXSTYLE = -20;
        const uint WS_CHILD = 0x40000000;
        const uint WS_EX_APPWINDOW = 0x00040000;

        private readonly int _hostHeight;
        private readonly int _hostWidth;
        public IntPtr childHost;

        public WinControlHost(double height, double width) {
            _hostHeight = (int)height;
            _hostWidth = (int)width;
        }

        public void ConvertToChildWindow(HandleRef hwndParent) {
            var style = GetWindowLong(childHost, GWL_STYLE); style |= WS_CHILD; SetWindowLong(childHost, GWL_STYLE, style);
            var styleEx = GetWindowLong(childHost, GWL_EXSTYLE); styleEx &= ~WS_EX_APPWINDOW; SetWindowLong(childHost, GWL_EXSTYLE, styleEx);
            SetParent(childHost, hwndParent.Handle);
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongW", SetLastError = true)]
        public static extern uint SetWindowLong(IntPtr hwnd, int index, uint newLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongW", SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwdNewParent);


        protected override HandleRef BuildWindowCore(HandleRef hwndParent) {
            //_hwndHost = IntPtr.Zero;
            //_hwndHost = CreateWindowEx(0, "static", "", WsChild | WsVisible, 0, 0, _hostHeight, _hostWidth, hwndParent.Handle, (IntPtr)HostId, IntPtr.Zero, 0);
            //return new HandleRef(this, _hwndHost);
            return new HandleRef(this, childHost);
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            handled = false; return IntPtr.Zero;
        }

        protected override void DestroyWindowCore(HandleRef hwnd) {
            DestroyWindow(hwnd.Handle);
        }

        //PInvoke declarations
        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateWindowEx(int dwExStyle, string lpszClassName, string lpszWindowName, int style,
            int x, int y, int width, int height, IntPtr hwndParent, IntPtr hMenu, IntPtr hInst, [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        internal static extern bool DestroyWindow(IntPtr hwnd);
    }
}