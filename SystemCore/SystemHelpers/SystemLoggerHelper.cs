using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace EasyITSystemCenter.SystemHelper {

    /// <summary>
    /// System Logger WebSocket Monitor Controller Class Definition For Centralized Using
    /// </summary>
    public partial class SystemLoggerWebSocketClass {
        public string Url { get; set; } = null;
        public ClientWebSocket WebSocketListener;
        public bool ShowSystemLogger = false;
    }

    /// <summary>
    /// System Logger Helper Defined Central WebSocket Monitor Controller
    /// </summary>
    public partial class OWinApiMiddleWare {

        public OWinApiMiddleWare() {
        }

        public static async void SystemLoggerWebSocketMonitorOnOff() {
            try {
                var test = App.SystemLoggerWebSocketMonitor.WebSocketListener;
                if (App.SystemLoggerWebSocketMonitor.ShowSystemLogger && ((MainWindow)App.Current.MainWindow).ServerLoggerSource) {
                    var buffer = new byte[1024 * 10];
                    App.SystemLoggerWebSocketMonitor.WebSocketListener = new ClientWebSocket();
                    await App.SystemLoggerWebSocketMonitor.WebSocketListener.ConnectAsync(new Uri(App.SystemLoggerWebSocketMonitor.Url), CancellationToken.None);
                    var receiveResult = await App.SystemLoggerWebSocketMonitor.WebSocketListener.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    while (!receiveResult.CloseStatus.HasValue) {
                        SystemLoggerWebSocketListener_OnMessage(Encoding.UTF8.GetString(buffer));
                        receiveResult = await App.SystemLoggerWebSocketMonitor.WebSocketListener.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    }
                    DisposeSystemLoggerWebSocketMonitor();
                }
                else { DisposeSystemLoggerWebSocketMonitor(); }
            } catch { DisposeSystemLoggerWebSocketMonitor(); }
        }

        private static void SystemLoggerWebSocketListener_OnMessage(string message) {
            try {
                App.Current.Dispatcher.Invoke(delegate () {
                    ((MainWindow)App.Current.MainWindow).SystemLogger = message;
                });
            } catch { }
        }

        private static void DisposeSystemLoggerWebSocketMonitor() {
            App.SystemLoggerWebSocketMonitor.WebSocketListener?.Dispose();
        }
    }
}