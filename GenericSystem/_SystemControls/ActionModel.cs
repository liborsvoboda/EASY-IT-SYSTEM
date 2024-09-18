using System;
using System.Collections.ObjectModel;
using MahApps.Metro.IconPacks;
using EasyITSystemCenter;
using EasyITSystemCenter.Pages;

namespace EasyITSystemCenter.Interact.Model
{
    public class ActionModel : BindableBase
    {
        public ObservableCollection<MenuItem> Menu { get; } = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> OptionsMenu { get; } = new ObservableCollection<MenuItem>();

        public ActionModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.BugSolid },
                Label = "Bugs",
                NavigationType = typeof(InteractBugsPage),
                NavigationDestination = new Uri("GenericSystem/Views/InteractBugsPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.UserSolid },
                Label = "User",
                NavigationType = typeof(InteractUserPage),
                NavigationDestination = new Uri("GenericSystem/Views/InteractUserPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.RulerCombinedSolid },
                Label = "Break",
                NavigationType = typeof(InteractBreakPage),
                NavigationDestination = new Uri("GenericSystem/Views/InteractBreakPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.FontAwesomeBrands },
                Label = "Awesome",
                NavigationType = typeof(InteractAwesomePage),
                NavigationDestination = new Uri("GenericSystem/Views/InteractAwesomePage.xaml", UriKind.RelativeOrAbsolute)
            });

            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.ServicestackBrands },
                Label = "Settings",
                NavigationType = typeof(InteractSettingsPage),
                NavigationDestination = new Uri("GenericSystem/Views/InteractSettingsPage.xaml", UriKind.RelativeOrAbsolute)
            });
            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.JsBrands },
                Label = "About",
                NavigationType = typeof(InteractAboutPage),
                NavigationDestination = new Uri("GenericSystem/Views/InteractAboutPage.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}