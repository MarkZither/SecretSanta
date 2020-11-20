using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SecretSanta.Models;

namespace SecretSanta.Services
{
    public class MockDataStore : IDataStore<ParticipantDTO>
    {
        List<ParticipantDTO> items;

        public MockDataStore()
        {
            items = new List<ParticipantDTO>();
            var mockItems = new List<ParticipantDTO>
            {
                new ParticipantDTO { Id = 1, Name = "Mark",  Email="This is an item description." },
                new ParticipantDTO { Id = 2, Name = "Caitriona", Email="This is an item description." },
                new ParticipantDTO { Id = 3, Name = "Mum",  Email="This is an item description." },
                new ParticipantDTO { Id = 4, Name = "Clive", Email="This is an item description." },
                new ParticipantDTO { Id = 5, Name = "Sam",  Email="This is an item description." },
                new ParticipantDTO { Id = 6, Name = "Caroline",  Email="This is an item description." },
                new ParticipantDTO { Id = 7, Name = "Stef",  Email="This is an item description." },
                new ParticipantDTO { Id = 8, Name = "Diane",  Email="This is an item description." },
                new ParticipantDTO { Id = 9, Name = "Uncle Mark",  Email="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(ParticipantDTO item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ParticipantDTO item)
        {
            var _item = items.Where((ParticipantDTO arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((ParticipantDTO arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<ParticipantDTO> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ParticipantDTO>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}