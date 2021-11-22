using System;
using SecretSanta.Models;

namespace SecretSanta.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ParticipantDTO Item { get; set; }
        public ItemDetailViewModel()
        {
            Item = new ParticipantDTO();
        }
        public ItemDetailViewModel(ParticipantDTO item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
