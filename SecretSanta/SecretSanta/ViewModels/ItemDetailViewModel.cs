using System;

using SecretSanta.Models;

namespace SecretSanta.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Participant Item { get; set; }
        public ItemDetailViewModel(Participant item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
