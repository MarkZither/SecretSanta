using System;

using SecretSanta.Models;
using SecretSanta.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SecretSanta.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}