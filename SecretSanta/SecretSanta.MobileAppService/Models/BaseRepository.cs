using System;
using SQLite;
using System.IO;
using System.Linq;
using Dapper;
using SQLiteNetExtensions.Extensions;

namespace SecretSanta.Models
{
    public class BaseRepository
    {
        public SQLiteConnection _connection;

        public static string DbFile
        {
            get
            {
                return Environment.CurrentDirectory + "\\db\\secretsanta.db";
            }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection(DbFile);
        }

        public BaseRepository()
        {
            if (File.Exists(DbFile))
            {
                //File.Delete(DbFile);
            }
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }
            _connection = SimpleDbConnection();
        }

        internal static void CreateDatabase()
        {
            using (var db = SimpleDbConnection())
            {
                db.CreateTable<Participant>();
                db.CreateTable<History>();
                
                var part1 = new Participant()
                {
                    Name = "testb"
                };
                var s = db.Insert(part1);
                var t = db.Insert(new Participant()
                {
                    Name = "testc"
                });
                var u = db.Insert(new History()
                {
                    GifterId = 1,
                    RecipientId = 2,
                    MatchDate = DateTime.Now
                });
                var gifter1 = new History()
                {
                    GifterId = 2,
                    RecipientId = 1,
                    MatchDate = DateTime.Now
                };
                var v = db.Insert(gifter1);
                part1.HistoryGifters = new System.Collections.Generic.List<History> { gifter1 };
                part1.HistoryRecipients = new System.Collections.Generic.List<History> { gifter1 };
                db.UpdateWithChildren(part1);
            }
        }
    }
}
