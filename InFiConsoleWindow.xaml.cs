using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;

namespace InFi_Console
{
    public sealed partial class InFiConsoleWindow : Window
    {
        public InFiConsoleWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(TitleBar);

            NavigationView.SelectedItem = NavigationView.MenuItems.OfType<NavigationViewItem>().First();
        }

        private void MainWindowActivated(object sender, WindowActivatedEventArgs e)
        {
            ContentFrame.Navigate(
                typeof(Views.InFiEyeMain),
                null,
                new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo()
                );
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                ContentFrame.Navigate(typeof(Views.SettingsMain), null, args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.Tag != null))
            {
                Type page = Type.GetType(args.InvokedItemContainer.Tag.ToString());
                ContentFrame.Navigate(
                    page,
                    null,
                    args.RecommendedNavigationTransitionInfo
                    );
            }
        }

        private void Navigation_BackRequest(object sender, NavigationViewBackRequestedEventArgs e)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationView.IsBackEnabled = ContentFrame.CanGoBack;

            NavigationView.Header = ((NavigationViewItem)NavigationView.SelectedItem)?.Content?.ToString();
        }
    }

    public class PagePath
    {
        public string Name { get; set; }
    }
}
