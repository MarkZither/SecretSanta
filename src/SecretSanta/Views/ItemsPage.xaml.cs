using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SecretSanta.Models;
using SecretSanta.Views;
using SecretSanta.ViewModels;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System.Diagnostics;
using System.Threading;

namespace SecretSanta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ParticipantDTO;
            if (item == null)
            {
                return;
            }

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        async void SantaItem_Clicked(object sender, EventArgs e)
        {
            /*var mark = viewModel.Items.Single(x => x.Name.Equals("Mark"));
            var cai = viewModel.Items.Single(x => x.Name.Equals("Caitriona"));
            var clive = viewModel.Items.Single(x => x.Name.Equals("Clive"));
            var mum = viewModel.Items.Single(x => x.Name.Equals("Ann"));
            var sam = viewModel.Items.Single(x => x.Name.Equals("Sam"));
            var car = viewModel.Items.Single(x => x.Name.Equals("Caroline"));
            var stef = viewModel.Items.Single(x => x.Name.Equals("Stef"));
            var di = viewModel.Items.Single(x => x.Name.Equals("Dianne"));
            var umark = viewModel.Items.Single(x => x.Name.Equals("Uncle Mark"));*/
            List<KeyValuePair<ParticipantDTO, ParticipantDTO>> banned = new List<KeyValuePair<ParticipantDTO, ParticipantDTO>>();
            Dictionary<ParticipantDTO, ParticipantDTO> banned2 = new Dictionary<ParticipantDTO, ParticipantDTO>();
            /*banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(mark, cai));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(cai, mark));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(clive, sam));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(sam, clive));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(stef, car));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(car, stef));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(di, umark));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(umark, di));
            // add anyone who was the same for last 2 years
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(clive, mum));*/
            foreach (var item in viewModel.Items.Where(x => x.BannedParticipantId.HasValue))
            {
                var recip = viewModel.Items.Single(x => x.Id.Equals(item.BannedParticipantId));
                banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(item, recip));
            }
            try
            {
                var res = SecretSanta.Services.SecretSantaGenerator.Generate(viewModel.Items, banned);
                StringBuilder sb = new StringBuilder();
                foreach (var item in res)
                {
                    var sent = await viewModel.MessagingDataStore.AddItemAsync(new Message() { GifterId = item.Key.Id, RecipientId = item.Value.Id });
                    sb.AppendLine($"{item.Key.Name} ==> {item.Value.Name}");
                }
                //await DisplayAlert("Generated Santa matches", sb.ToString(), "Cancel");
                await DisplayAlert("Generated Santa matches", "It is a Secret!", "Ok");
            }
            catch (ApplicationException aex) when (aex.Message.Equals("No valid santa list can be generated"))
            {
                await DisplayAlert("Generated Santa matches", aex.Message, "Cancel");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var authResult = await WebAuthenticator.AuthenticateAsync(
                new Uri("https://localhost:5001/connect/authorize"),
                new Uri("markzithersecretsanta://"));

            var accessToken = authResult?.AccessToken;

            OidcClient oidcClient = CreateOidcClient();
            LoginResult loginResult = await oidcClient.LoginAsync(new LoginRequest());
        }

        private OidcClient CreateOidcClient()
        {
            var options = new OidcClientOptions
            {
                Authority = "https://localhost:5001",// _authorityUrl,
                ClientId = "demo_api_client", //_clientId,
                Scope = "", //_scope,
                RedirectUri = "markzithersecretsanta://callback", //_redirectUrl,
                //ClientSecret = _clientSecret,
                Browser = new WebAuthenticatorBrowser()
            };

            var oidcClient = new OidcClient(options);
            return oidcClient;
        }
    }
    internal class WebAuthenticatorBrowser : IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options,
               CancellationToken cancellationToken = default)
        {
            try
            {
                WebAuthenticatorResult authResult =
                    await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl),
                          new Uri(options.EndUrl));
                var authorizeResponse = ToRawIdentityUrl(options.EndUrl, authResult);

                return new BrowserResult
                {
                    Response = authorizeResponse
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new BrowserResult()
                {
                    ResultType = BrowserResultType.UnknownError,
                    Error = ex.ToString()
                };
            }
        }

        public string ToRawIdentityUrl(string redirectUrl, WebAuthenticatorResult result)
        {
            IEnumerable<string> parameters =
                 result.Properties.Select(pair => $"{pair.Key}={pair.Value}");
            var values = string.Join("&", parameters);

            return $"{redirectUrl}#{values}";
        }
    }
}