using System;
using System.Collections.Generic;

namespace SecretSanta.Models
{
	public interface IParticipantRepository
    {
		void Add(Participant item);
		void Update(Participant item);
        Participant Remove(int key);
        Participant Get(int id);
		IEnumerable<Participant> GetAll();
		IEnumerable<Participant> GetAllForLoggedInUser(Guid UserId);
	}
}
