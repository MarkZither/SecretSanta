using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SecretSanta.Models;

namespace SecretSanta.Services
{
    public class MockDataStore : IDataStore<Participant>
    {
        List<Participant> items;

        public MockDataStore()
        {
            items = new List<Participant>();
            var mockItems = new List<Participant>
            {
                new Participant { Id = 1, Name = "Mark",  Email="This is an item description." },
                new Participant { Id = 2, Name = "Caitriona", Email="This is an item description." },
                new Participant { Id = 3, Name = "Mum",  Email="This is an item description." },
                new Participant { Id = 4, Name = "Clive", Email="This is an item description." },
                new Participant { Id = 5, Name = "Sam",  Email="This is an item description." },
                new Participant { Id = 6, Name = "Caroline",  Email="This is an item description." },
                new Participant { Id = 7, Name = "Stef",  Email="This is an item description." },
                new Participant { Id = 8, Name = "Diane",  Email="This is an item description." },
                new Participant { Id = 9, Name = "Uncle Mark",  Email="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Participant item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Participant item)
        {
            var _item = items.Where((Participant arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Participant arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Participant> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Participant>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}