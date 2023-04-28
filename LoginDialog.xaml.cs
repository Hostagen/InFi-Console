using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace InFi_Console
{
    public sealed partial class LoginDialog : ContentDialog
    {
        public string Token;

        public LoginDialog()
        {
            InitializeComponent();
        }

        private void LoginButtonVisibility(bool boolean)
        {
            switch (boolean)
            {
                case true:
                    LoginButton.Visibility = Visibility.Visible;
                    LoggingProgressRing.Visibility = Visibility.Collapsed;
                    break;
                default:
                    LoginButton.Visibility = Visibility.Collapsed;
                    LoggingProgressRing.Visibility = Visibility.Visible;
                    break;
            }
        }

        private async void LoginButtonClicked(object sender, RoutedEventArgs e)
        {
            LoginResultTextBlock.Text = string.Empty;
            LoginResultTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            LoginResultTextBlock.Visibility = Visibility.Visible;

            switch (string.IsNullOrEmpty(InputIdTextBox.Text)
                && string.IsNullOrEmpty(InputPasswordBox.Password))
            {
                case true:
                    LoginResultTextBlock.Text = "Please enter your ID and Password.";
                    return;
                case false when string.IsNullOrEmpty(InputIdTextBox.Text):
                    LoginResultTextBlock.Text = "Please enter your ID.";
                    return;
                case false when string.IsNullOrEmpty(InputPasswordBox.Password):
                    LoginResultTextBlock.Text = " Please enter your Password.";
                    return;
                default:
                    LoginResultTextBlock.Visibility = Visibility.Collapsed;
                    break;
            }

            LoginButtonVisibility(false);

            StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    username = InputIdTextBox.Text,
                    password = InputPasswordBox.Password,
                }),
                Encoding.UTF8,
                "application/json");

            HttpClient httpClient = new();

            HttpResponseMessage response = await httpClient.PostAsync(
                new Uri("https://3rqbpqtcic.execute-api.us-east-2.amazonaws.com/Prod/auth/login"),
                jsonContent);

            if (response.IsSuccessStatusCode != true)
            {
                LoginButtonVisibility(true);
                LoginResultTextBlock.Visibility = Visibility.Visible;
                LoginResultTextBlock.Text = "ID or Password is Invalid. Please retry.";
                LoginResultTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }
            LoggingProgressRing.Visibility = Visibility.Collapsed;
            LoginInfoStackPanel.Visibility = Visibility.Collapsed;

            var responseBody = response.Content.ReadAsStringAsync().Result;
            Token = JObject.Parse(responseBody)["token"].ToString();

            LoginResultTextBlock.Visibility = Visibility.Visible;
            LoginResultTextBlock.Text = "Loggined Successfully!";
            IsPrimaryButtonEnabled = true;
        }
    }
}
