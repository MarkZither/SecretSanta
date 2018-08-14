using System;
using System.Collections.Generic;

namespace SecretSanta.Models
{
	public interface IParticipantRepository
    {
		void Add(Participant item);
		void Update(Participant item);
        Participant Remove(string key);
        Participant Get(string id);
		IEnumerable<Participant> GetAll();
	}
}
