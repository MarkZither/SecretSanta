﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Models
{
    public class History
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required]
        [ForeignKey(typeof(Participant), Name = "GifterId")]
        public int GifterId { get; set; }
        [Required]
        [ForeignKey(typeof(Participant), Name = "RecipientId")]
        public int RecipientId { get; set; }
        public DateTime MatchDate { get; set; }

        [ManyToOne("GifterId")]
        public Participant Gifter { get; set; }
    }
}
