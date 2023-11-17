using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using SecretSanta.Models;

namespace SecretSanta.Services
{
	public class AzureDataStore : IDataStore<ParticipantDTO>
	{
		HttpClient client;
		IEnumerable<ParticipantDTO> items;
		int maxRetryAttempts = 3;
		TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(2);
		AsyncRetryPolicy retryPolicy;

		public AzureDataStore()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
			retryPolicy = Policy
				.Handle<HttpRequestException>()
				.WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);

			items = new List<ParticipantDTO>();
		}

		public async Task<IEnumerable<ParticipantDTO>> GetItemsAsync(bool forceRefresh = false) {
			NetworkAccess accessType = Connectivity.Current.NetworkAccess;
			if (forceRefresh && accessType == NetworkAccess.Internet) {
				await retryPolicy.ExecuteAsync(async () => {
					var json = await client.GetStringAsync($"api/Participant");

					items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ParticipantDTO>>(json));
				});
			}

			return items;
		}

		public async Task<ParticipantDTO> GetItemAsync(int id)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (id > 0 && accessType == NetworkAccess.Internet)
			{
				var json = await client.GetStringAsync($"api/item/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<ParticipantDTO>(json));
			}

			return null;
		}

		public async Task<bool> AddItemAsync(ParticipantDTO item)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (item == null || !(accessType == NetworkAccess.Internet))
            {
                return false;
            }

			var serializedItem = JsonConvert.SerializeObject(item);

			var response = await client.PostAsync($"api/participant", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateItemAsync(ParticipantDTO item)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (item == null || item.Id == 0 || !(accessType == NetworkAccess.Internet))
            {
                return false;
            }

			var serializedItem = JsonConvert.SerializeObject(item);
			var buffer = Encoding.UTF8.GetBytes(serializedItem);
			var byteContent = new ByteArrayContent(buffer);

			var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteItemAsync(int id)
		{
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (!(accessType == NetworkAccess.Internet))
            {
                return false;
            }

			var response = await client.DeleteAsync($"api/item/{id}");

			return response.IsSuccessStatusCode;
		}
	}
}