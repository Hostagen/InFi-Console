using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace InFi_Console.Views
{
    public sealed partial class InFiEyeMain : Page
    {

        public InFiEyeMain()
        {
            InitializeComponent();
        }

        private void BansSettingsCardClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InFiEyePages.BansPage));
        }
    }
}
