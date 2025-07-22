using EasyITSystemCenter.Properties;
using EasyITSystemCenter.SystemHelper;
using MahApps.Metro;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace EasyITSystemCenter.SystemStructure {

    public class SystemTheme : AccentColorMenuData {

        protected override void ChangeTheme(object sender) {
            try {
                Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
                AppTheme appTheme = ThemeManager.GetAppTheme(this.Name);
                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
                SaveTheme(theme.Item2, appTheme);
            } catch { }
        }
    }

    public class AccentColorMenuData {

        protected virtual void ChangeTheme(object sender) {
            try {
                Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
                Accent accent = ThemeManager.GetAccent(this.Name);
                ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
                SaveTheme(accent, theme.Item1);
            } catch { }
        }

        protected void SaveTheme(Accent accent, AppTheme theme) {
            try {
                Settings.Default.apper_themeName = theme.Name;
                Settings.Default.apper_accentName = accent.Name;
                Settings.Default.Save();
            } catch { }
        }

        public AccentColorMenuData() {
            ChangeAccentCommand = new DelegateCommand<object>(this.ChangeAccent);
        }

        public string Name { get; set; }

        public string NameDisplay { get { return Regex.Replace(this.Name, "([a-z])([A-Z][a-z])", "$1 $2"); } }

        public Brush BorderColorBrush { get; set; }

        public Brush ColorBrush { get; set; }

        public DelegateCommand<object> ChangeAccentCommand { get; private set; }

        private void ChangeAccent(object parameter) {
            ChangeTheme(parameter);
        }
    }
}