using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using SQLite;
using System.Data;
using System.IO;
using System.Linq;
using SQLiteNetExtensions.Extensions;
using StackExchange.Profiling;

namespace SecretSanta.Models
{
	public class ParticipantRepository : BaseRepository, IParticipantRepository
    {
        public ParticipantRepository() : base()
        {

        }

        public Participant Get(int id)
		{
			return _connection.Table<Participant>().Single(x => x.Id.Equals(id));
		}

		public IEnumerable<Participant> GetAll()
		{
			using (MiniProfiler.Current.Step("Getting participants from the database"))
			{
				var query = _connection.GetAllWithChildren<Participant>();
				return query;
			}
		}		
		
		public IEnumerable<Participant> GetAllForLoggedInUser(Guid UserId)
		{
			using (MiniProfiler.Current.Step("Getting participants for the logged in user from the database"))
			{
				var query = _connection.Query<Participant>("select * from Participant where upper(UserId) = ?", UserId.ToString().ToUpperInvariant());
				return query;
			}
		}

		public void Add(Participant participant)
		{
			participant.UserId = "71A32736-7B5B-4020-86F4-CCBF529802CE";
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
