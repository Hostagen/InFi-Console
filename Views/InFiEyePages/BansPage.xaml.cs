// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace InFi_Console.Views.InFiEyePages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BansPage : Page
    {
        public BansPage()
        {
            InitializeComponent();
        }

        private void GetAllBansSettingsCardClicked(object sender, RoutedEventArgs e)
        {
            Window window = new BansWindows.AllBansWindow();
            window.Activate();
        }
    }
}
