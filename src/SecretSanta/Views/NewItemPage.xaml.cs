using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SecretSanta.Models;
using SecretSanta.ViewModels;

namespace SecretSanta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public NewItemPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemDetailViewModel();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", viewModel.Item);
            await Navigation.PopModalAsync();
        }
    }
}