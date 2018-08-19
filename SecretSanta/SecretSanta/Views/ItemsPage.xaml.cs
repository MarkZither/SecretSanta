﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SecretSanta.Models;
using SecretSanta.Views;
using SecretSanta.ViewModels;

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
            var item = args.SelectedItem as Participant;
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
            var mark = viewModel.Items.Single(x => x.Name.Equals("Mark"));
            var cai = viewModel.Items.Single(x => x.Name.Equals("Caitriona"));
            var clive = viewModel.Items.Single(x => x.Name.Equals("Clive"));
            var mum = viewModel.Items.Single(x => x.Name.Equals("Mum"));
            var sam = viewModel.Items.Single(x => x.Name.Equals("Sam"));
            var car = viewModel.Items.Single(x => x.Name.Equals("Caroline"));
            var stef = viewModel.Items.Single(x => x.Name.Equals("Stef"));
            var di = viewModel.Items.Single(x => x.Name.Equals("Diane"));
            var umark = viewModel.Items.Single(x => x.Name.Equals("Uncle Mark"));
            Dictionary<Participant, Participant> banned = new Dictionary<Participant, Participant>();
            banned.Add(mark, cai);
            banned.Add(cai, mark);
            banned.Add(clive, sam);
            banned.Add(sam, clive);
            banned.Add(stef, car);
            banned.Add(car, stef);
            banned.Add(di, umark);
            banned.Add(umark, di);
            try
            {
                var res = SecretSanta.Services.SecretSantaGenerator.Generate(viewModel.Items, banned);
                StringBuilder sb = new StringBuilder();
                foreach (var item in res)
                {
                    sb.AppendLine($"{item.Key.Name} ==> {item.Value.Name}");
                }
                await DisplayAlert("Generated Santa matches", sb.ToString(), "Cancel");
            }
            catch(ApplicationException aex) when (aex.Message.Equals("No valid santa list can be generated"))
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
    }
}