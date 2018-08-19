using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using SQLite;
using System.Data;
using System.IO;
using System.Linq;

namespace SecretSanta.Models
{
	public class ParticipantRepository : BaseRepository, IParticipantRepository
    {
        private SQLiteConnection _connection;

        public ParticipantRepository()
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }
            _connection = SimpleDbConnection();
        }

        public Participant Get(int id)
		{
			return _connection.Table<Participant>().Single(x => x.Id.Equals(id));
		}

		public IEnumerable<Participant> GetAll()
		{
            var query = _connection.Table<Participant>();
            return query;
		}

		public void Add(Participant participant)
		{
            var s = _connection.Insert(participant);
		}

		public Participant Find(int id)
		{
            Participant participant = _connection.Table<Participant>().Single(x => x.Id.Equals(id));
            return participant;
		}

		public Participant Remove(int id)
		{
            Participant participant = _connection.Table<Participant>().Single(x => x.Id.Equals(id));
            _connection.Delete(participant);

			return participant;
		}

		public void Update(Participant participant)
		{
            _connection.Update(participant);
		}
	}
}
