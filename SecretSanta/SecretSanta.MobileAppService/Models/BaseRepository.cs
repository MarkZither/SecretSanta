using System;
using SQLite;
using System.IO;
using System.Linq;
using Dapper;

namespace SecretSanta.Models
{
    public class BaseRepository
    {
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
            
        }

        internal static void CreateDatabase()
        {
            using (var db = SimpleDbConnection())
            {
                db.CreateTable<Participant>();
                var s = db.Insert(new Participant()
                {
                    Name = "testc"
                });
            }
        }
    }
}
