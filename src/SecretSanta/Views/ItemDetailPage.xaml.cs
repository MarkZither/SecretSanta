using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SecretSanta.Models;
using SecretSanta.ViewModels;

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