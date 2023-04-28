using Microsoft.UI.Xaml;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InFi_Console.Views.InFiEyePages.BansWindows
{
    public sealed partial class AllBansWindow : Window
    {
        private App app = (Application.Current as App);
        public HttpClient client = new();

        public AllBansWindow()
        {
            InitializeComponent();
            client.DefaultRequestHeaders.Add("Authorization", app.Token);
        }

        public record Charater
        {
            public string Id { get; set; }
            public string UserId { get; set; }
            public string BanCode { get; set; }
            public string BannedBy { get; set; }
            public string GuildId { get; set; }
            public DateTime BannedAt { get; set; }
        }

        public ObservableCollection<Charater> Charaters { get; set; } = new();

        private async void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            await FetchBansAndUpdateDataGrid();
            RefreshButton.IsEnabled = true;
        }

        private async void RefreshButtonClicked(object sender, RoutedEventArgs e)
        {
            RefreshButton.IsEnabled = false;
            await FetchBansAndUpdateDataGrid();
            RefreshButton.IsEnabled = true;
        }

        private async Task FetchBansAndUpdateDataGrid()
        {
            BansDataGrid.Visibility = Visibility.Collapsed;
            FetchingProgressRing.Visibility = Visibility.Visible;

            HttpResponseMessage response = await client.GetAsync(
                new Uri("https://3rqbpqtcic.execute-api.us-east-2.amazonaws.com/Prod/bans"));

            if (response.IsSuccessStatusCode)
            {
                Charaters.Clear();
                string responseBody = await response.Content.ReadAsStringAsync();
                var dataArray = JArray.Parse(responseBody);

                BansDataGrid.Visibility = Visibility.Visible;

                foreach (JObject data in dataArray.Cast<JObject>())
                {
                    Charater charater = new()
                    {
                        Id = data["id"].ToString(),
                        UserId = data["user_id"].ToString(),
                        BanCode = data["ban_code"].ToString(),
                        BannedBy = data["banned_by"].ToString(),
                        GuildId = data["guild_id"].ToString(),
                    };
                    DateTime BannedAt = Convert.ToDateTime(data["banned_at"].ToString());
                    TimeZoneInfo localZone = TimeZoneInfo.Local;
                    charater.BannedAt = TimeZoneInfo.ConvertTimeFromUtc(BannedAt, localZone);

                    Charaters.Add(charater);
                }
            }
            FetchingProgressRing.Visibility = Visibility.Collapsed;
        }
    }
}
