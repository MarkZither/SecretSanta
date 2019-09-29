using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SecretSanta.Models;

namespace SecretSanta.Services
{
    public class MockMessagingDataStore : IDataStore<Message>
    {
        List<Message> items;

        public MockMessagingDataStore()
        {
            items = new List<Message>();
            var mockItems = new List<Message>
            {
                new Message { Id = 1, GifterId = 1, RecipientId = 2},
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Message item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Message item)
        {
            var _item = items.Where((Message arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Message arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Message> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Message>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}