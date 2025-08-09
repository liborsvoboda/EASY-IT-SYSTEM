using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using MenuItem = EasyITSystemCenter.Interact.Model.MenuItem;

namespace EasyITSystemCenter.Pages {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class InteractUserSystemPage : UserControl
    {
        private readonly Interact.Navigation.NavigationServiceEx navigationServiceEx;

        public InteractUserSystemPage()
        {
            this.InitializeComponent();

            this.navigationServiceEx = new Interact.Navigation.NavigationServiceEx();
            this.navigationServiceEx.Navigated += this.NavigationServiceEx_OnNavigated;
            this.ham_UserMenuControl.Content = this.navigationServiceEx.Frame;

            // Navigate to the home page.
            this.Loaded += (sender, args) => this.navigationServiceEx.Navigate(new Uri("GenericSystem/Views/InteractMainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (e.InvokedItem is MenuItem menuItem && menuItem.IsNavigation)
            {
                this.navigationServiceEx.Navigate(menuItem.NavigationDestination);
            }
        }

        private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
        {
            // select the menu item
            this.ham_UserMenuControl.SetCurrentValue(HamburgerMenu.SelectedItemProperty,
                this.ham_UserMenuControl.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(x => x.NavigationDestination == e.Uri));
            this.ham_UserMenuControl.SetCurrentValue(HamburgerMenu.SelectedOptionsItemProperty,
                this.ham_UserMenuControl
                    .OptionsItems
                    .OfType<MenuItem>()
                    .FirstOrDefault(x => x.NavigationDestination == e.Uri));

            //or when using the NavigationType on menu item
            this.ham_UserMenuControl.SetCurrentValue(HamburgerMenu.SelectedItemProperty, this.ham_UserMenuControl
                    .Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(x => x.NavigationType == e.Content?.GetType()));
            this.ham_UserMenuControl.SetCurrentValue(HamburgerMenu.SelectedOptionsItemProperty, this.ham_UserMenuControl
                    .OptionsItems
                    .OfType<MenuItem>()
                    .FirstOrDefault(x => x.NavigationType == e.Content?.GetType()));

            // update back button
            this.GoBackButton.SetCurrentValue(VisibilityProperty, this.navigationServiceEx.CanGoBack ? Visibility.Visible : Visibility.Collapsed);
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            this.navigationServiceEx.GoBack();
        }
    }
}