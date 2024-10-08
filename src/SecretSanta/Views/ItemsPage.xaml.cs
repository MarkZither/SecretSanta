﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecretSanta.Models;
using SecretSanta.Views;
using SecretSanta.ViewModels;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System.Diagnostics;
using System.Threading;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace SecretSanta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;
        private int count = 0;

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
        private void OnCounterClicked(object sender, EventArgs e) {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        async void SantaItem_Clicked(object sender, EventArgs e)
        {
            var mark = viewModel.Items.Single(x => x.Name.Equals("Mark"));
            var cai = viewModel.Items.Single(x => x.Name.Equals("Caitríona"));
            var clive = viewModel.Items.Single(x => x.Name.Equals("Clive"));
            var mum = viewModel.Items.Single(x => x.Name.Equals("Ann"));
            var sam = viewModel.Items.Single(x => x.Name.Equals("Sam"));
            var car = viewModel.Items.Single(x => x.Name.Equals("Caroline"));
            var stef = viewModel.Items.Single(x => x.Name.Equals("Stef"));
            var di = viewModel.Items.Single(x => x.Name.Equals("Dianne"));
            var umark = viewModel.Items.Single(x => x.Name.Equals("Uncle Mark"));
            /*var Christophe = viewModel.Items.Single(x => x.Name.Equals("Christophe Siuda"));
            var Henni = viewModel.Items.Single(x => x.Name.Equals("Henni Wolter"));
            var Thomas = viewModel.Items.Single(x => x.Name.Equals("Thomas Wolter"));
            var Marina = viewModel.Items.Single(x => x.Name.Equals("Marina Wolter"));
            var Deike = viewModel.Items.Single(x => x.Name.Equals("Deike Wolter"));
            var Krispin = viewModel.Items.Single(x => x.Name.Equals("Krispin Wolter"));
            var Heiner = viewModel.Items.Single(x => x.Name.Equals("Heiner Porrmann"));
            var Marita = viewModel.Items.Single(x => x.Name.Equals("Marita Porrmann"));
            var Stefan = viewModel.Items.Single(x => x.Name.Equals("Stefan Porrmann"));
            var Martin = viewModel.Items.Single(x => x.Name.Equals("Martin Porrmann"));*/
            List<KeyValuePair<ParticipantDTO, ParticipantDTO>> banned = new List<KeyValuePair<ParticipantDTO, ParticipantDTO>>();
            Dictionary<ParticipantDTO, ParticipantDTO> banned2 = new Dictionary<ParticipantDTO, ParticipantDTO>();
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(mark, cai));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(cai, mark));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(clive, sam));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(sam, clive));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(stef, car));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(car, stef));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(di, umark));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(umark, di));
            // add anyone who was the same for last 2 years
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(clive, mum));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(clive, mum));

            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(car, cai));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(cai, car));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(cai, mum));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(mum, stef));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(stef, mum));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(clive, car));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(car, clive));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(sam, mark));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(mark, sam));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(stef, sam));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(sam, stef));

            /*banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Henni, Thomas));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Henni, Marina));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Henni, Deike));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Henni, Krispin));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Henni, Christophe));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Christophe, Henni));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Thomas, Marina));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Thomas, Deike));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Thomas, Krispin));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Thomas, Henni));            
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Marina, Thomas));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Marina, Deike));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Marina, Krispin));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Marina, Henni));            
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Deike, Thomas));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Deike, Marina));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Deike, Krispin));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Deike, Henni));            
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Krispin, Thomas));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Krispin, Marina));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Krispin, Deike));
            banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Krispin, Henni));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Heiner, Marita));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Heiner, Stefan));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Heiner, Martin));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Marita, Heiner));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Marita, Stefan));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Marita, Martin));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Martin, Heiner));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Martin, Marita));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Martin, Stefan));            
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Stefan, Heiner));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Stefan, Marita));
            //banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(Stefan, Martin));
            */
            foreach (var item in viewModel.Items.Where(x => x.BannedParticipantId.HasValue))
            {
                var recip = viewModel.Items.Single(x => x.Id.Equals(item.BannedParticipantId));
                banned.Add(new KeyValuePair<ParticipantDTO, ParticipantDTO>(item, recip));
            }
            try
            {
                var tries = 10;
                var tried = 0;
                while (tried < tries)
                {
                    try
                    {
                        var res = SecretSanta.Shared.SecretSantaGenerator.Generate(viewModel.Items, banned);
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in res)
                        {
                            var sent = await viewModel.MessagingDataStore.AddItemAsync(new Message() { GifterId = item.Key.Id, RecipientId = item.Value.Id });
                            sb.AppendLine($"{item.Key.Name} ==> {item.Value.Name}");
                        }
                        //await DisplayAlert("Generated Santa matches", sb.ToString(), "Cancel");
                        await DisplayAlert("Generated Santa matches", "It is a Secret!", "Ok");
                        break;
                    }
                    catch (Exception)
                    {
                        if (tried == tries)
                        {
                            throw;
                        }
                    }
                    tried++;
                }
                
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
            try
            {
                var authResult = await WebAuthenticator.AuthenticateAsync(
                    new Uri("https://localhost:5001/connect/authorize"),
                    new Uri("markzithersecretsanta://callback"));

                var accessToken = authResult?.AccessToken;
            }
            catch(Exception ex)
            {
                Debug.Write(ex);
            }

            OidcClient oidcClient = CreateOidcClient();
            var loginRequest = new LoginRequest();
            LoginResult loginResult = await oidcClient.LoginAsync(loginRequest);
        }

        private OidcClient CreateOidcClient()
        {
            var options = new OidcClientOptions
            {
                Authority = "https://localhost:5001",// _authorityUrl,
                ClientId = "demo_native_client", //_clientId,
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