using SQLite;
using System;
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
    }
}
