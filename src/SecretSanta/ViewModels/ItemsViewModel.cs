using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using SecretSanta.Models;
using SecretSanta.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;

namespace SecretSanta.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<ParticipantDTO> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<ParticipantDTO>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            WeakReferenceMessenger.Default.Register<ParticipantDTO>(this, async (r, m) =>
            {
                var _item = m;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}