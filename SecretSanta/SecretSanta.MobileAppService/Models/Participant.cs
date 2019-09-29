using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Models
{
    public class Participant
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Suggestions { get; set; }

        [OneToMany(/*inverseProperty: "RecipientId", */CascadeOperations = CascadeOperation.All)]
        public List<History> HistoryRecipients { get; set; }
        [OneToMany(/*inverseProperty: "GifterId", */CascadeOperations = CascadeOperation.All)]
        public List<History> HistoryGifters { get; set; }
    }
}
