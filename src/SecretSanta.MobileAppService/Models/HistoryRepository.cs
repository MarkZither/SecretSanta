using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using SQLite;
using System.Data;
using System.IO;
using System.Linq;
using SQLiteNetExtensions.Extensions;

namespace SecretSanta.Models
{
	public class HistoryRepository : BaseRepository, IHistoryRepository
    {
        public HistoryRepository() : base()
        {
        }

        public History Get(int id)
		{
			return _connection.Table<History>().Single(x => x.Id.Equals(id));
		}

		public IEnumerable<History> GetAll()
		{
            var query = _connection.GetAllWithChildren<History>();
            return query;
		}

		public void Add(History history)
		{
            var s = _connection.Insert(history);
		}

		public History Find(int id)
		{
            History history = _connection.Table<History>().Single(x => x.Id.Equals(id));
            return history;
		}

		public History Remove(int id)
		{
            History history = _connection.Table<History>().Single(x => x.Id.Equals(id));
            _connection.Delete(history);

			return history;
		}

		public void Update(History history)
		{
            _connection.Update(history);
		}
	}
}
