using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;
using System.Net.Http;

namespace InFi_Console
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            console_window = new InFiConsoleWindow();
            console_window.Activate();

            if (console_window.Content is FrameworkElement fe)
                fe.Loaded += (ss, ee) => ShowLogin(console_window.Content.XamlRoot);
        }

        protected async void ShowLogin(XamlRoot xamlRoot)
        {
            LoginDialog loginDialog = new() { XamlRoot = xamlRoot, };
            ContentDialogResult dialogResult = await loginDialog.ShowAsync();

            if (dialogResult != ContentDialogResult.Primary)
                console_window.Close();
            Token = loginDialog.Token;
        }

        internal string Token;
        private Window console_window;
        protected HttpClient HttpClient { get; } = new HttpClient();
    }
}
