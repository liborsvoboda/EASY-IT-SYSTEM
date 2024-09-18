using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;


namespace EasyITSystemCenter.Pages {
    /// <summary>
    /// Interaction logic for AccentStyleWindow.xaml
    /// </summary>
    public partial class TabItemPage : MetroTabItem {
        public static readonly DependencyProperty ColorsProperty
            = DependencyProperty.Register(
                nameof(Colors),
                typeof(List<KeyValuePair<string, Color>>),
                typeof(TabItemPage));

        public List<KeyValuePair<string, Color>> Colors
        {
            get => (List<KeyValuePair<string, Color>>)GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        public TabItemPage()
        {
         

            this.DataContext = this;

            this.Colors = typeof(Colors)
                .GetProperties()
                .Where(prop => typeof(Color).IsAssignableFrom(prop.PropertyType))
                .Select(prop => new KeyValuePair<String, Color>(prop.Name, (Color)prop.GetValue(null)))
                .ToList();

            //var appTheme = ThemeManager.Current.DetectTheme(Application.Current);
            //ThemeManager.Current.ChangeTheme(this, appTheme);
        }

    
    }
}