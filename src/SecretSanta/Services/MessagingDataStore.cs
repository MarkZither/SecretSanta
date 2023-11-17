using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SecretSanta.Models;

namespace SecretSanta.Services
{
	public class MessagingDataStore : IDataStore<Message>
	{
		HttpClient client;
		IEnumerable<Message> items;

		public MessagingDataStore()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

			items = new List<Message>();
		}

		public async Task<IEnumerable<Message>> GetItemsAsync(bool forceRefresh = false)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (forceRefresh && accessType == NetworkAccess.Internet)
			{
				var json = await client.GetStringAsync($"api/Messaging");
				items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Message>>(json));
			}

			return items;
		}

		public async Task<Message> GetItemAsync(int id)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (id > 0 && accessType == NetworkAccess.Internet)
			{
				var json = await client.GetStringAsync($"api/Messaging/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Message>(json));
			}

			return await Task.FromResult(new Message());
		}

		public async Task<bool> AddItemAsync(Message item)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (item == null || !(accessType == NetworkAccess.Internet))
            {
                return false;
            }

			var serializedItem = JsonConvert.SerializeObject(item);

			var response = await client.PostAsync($"api/Messaging", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateItemAsync(Message item)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (item == null || item.Id == 0 || !(accessType == NetworkAccess.Internet))
            {
                return false;
            }

			var serializedItem = JsonConvert.SerializeObject(item);
			var buffer = Encoding.UTF8.GetBytes(serializedItem);
			var byteContent = new ByteArrayContent(buffer);

			var response = await client.PutAsync(new Uri($"api/Messaging/{item.Id}"), byteContent);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteItemAsync(int id)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (!(accessType == NetworkAccess.Internet))
            {
                return false;
            }

			var response = await client.DeleteAsync($"api/Messaging/{id}");

			return response.IsSuccessStatusCode;
		}
	}
}