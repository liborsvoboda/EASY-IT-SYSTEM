using System.ComponentModel;
using System.Windows.Input;

namespace EasyITSystemCenter.GlobalOperations {

    internal class HardwareOperations {

        /// <summary>
        /// Application Keyboard controller
        /// </summary>
        /// <param name="e"></param>
        public static void ApplicationKeyboardMaping(KeyEventArgs e) {
            if (e.Key == Key.F1) { System.Windows.Forms.Help.ShowHelp(null, "Manual\\index.chm"); e.Handled = true; }
            else if (Keyboard.IsKeyDown(Key.Q) && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) { MainWindow.MainWindow_Closing(null, new CancelEventArgs()); Mouse.OverrideCursor = null; e.Handled = true; }
            else if (Keyboard.IsKeyDown(Key.R) && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) { Mouse.OverrideCursor = null; e.Handled = true; }
            else if (Keyboard.IsKeyDown(Key.C) && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) { Mouse.OverrideCursor = null; e.Handled = true; }
            else if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) { System.Windows.Forms.Clipboard.Clear(); }
        }
    }
}