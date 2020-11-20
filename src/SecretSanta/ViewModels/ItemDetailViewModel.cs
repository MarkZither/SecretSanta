using System;

using SecretSanta.Models;

namespace SecretSanta.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ParticipantDTO Item { get; set; }
        public ItemDetailViewModel(ParticipantDTO item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
