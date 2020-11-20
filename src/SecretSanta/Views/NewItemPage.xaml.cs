using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SecretSanta.Models;

namespace SecretSanta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public ParticipantDTO Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new ParticipantDTO
            {
                Name = "Mark",
                Email = "mark@test.com"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}