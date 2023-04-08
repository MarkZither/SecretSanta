using System;
using System.Collections.Generic;

using SecretSanta.Models;
using SecretSanta.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

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