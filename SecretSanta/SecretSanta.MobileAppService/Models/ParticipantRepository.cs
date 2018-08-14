using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace SecretSanta.Models
{
	public class ItemRepository : IParticipantRepository
    {
		private static ConcurrentDictionary<string, Participant> participants =
			new ConcurrentDictionary<string, Participant>();

		public ItemRepository()
		{
			Add(new Participant { Id = Guid.NewGuid().ToString(), Name = "Item 1", Email = "This is an item description." });
			Add(new Participant { Id = Guid.NewGuid().ToString(), Name = "Item 2", Email = "This is an item description." });
			Add(new Participant { Id = Guid.NewGuid().ToString(), Name = "Item 3", Email = "This is an item description." });
		}

		public Participant Get(string id)
		{
			return participants[id];
		}

		public IEnumerable<Participant> GetAll()
		{
			return participants.Values;
		}

		public void Add(Participant participant)
		{
            participant.Id = Guid.NewGuid().ToString();
            participants[participant.Id] = participant;
		}

		public Participant Find(string id)
		{
            Participant participant;
            participants.TryGetValue(id, out participant);

			return participant;
		}

		public Participant Remove(string id)
		{
            Participant participant;
            participants.TryRemove(id, out participant);

			return participant;
		}

		public void Update(Participant participant)
		{
            participants[participant.Id] = participant;
		}
	}
}
