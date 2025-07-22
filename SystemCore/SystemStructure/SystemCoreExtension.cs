using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace EasyITSystemCenter {

    internal abstract class AppExtension : App {

        internal static T GetParentOfType<T>(Visual visual) where T : Visual {
            DependencyObject parent = visual;
            do {
                parent = VisualTreeHelper.GetParent(parent);
            } while (parent != null && !(parent is T));

            return parent as T;
        }

        public override string ToString() {
            if (this == null) {
                return string.Empty;
            }
            else {
                return Convert.ToString(this);
            }
        }
    }
}