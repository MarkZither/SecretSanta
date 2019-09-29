using System;
using System.Collections.Generic;

namespace SecretSanta.Models
{
	public interface IHistoryRepository
    {
		void Add(History item);
		void Update(History item);
        History Remove(int key);
        History Get(int id);
		IEnumerable<History> GetAll();
	}
}
