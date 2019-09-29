using System;

namespace SecretSanta.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Suggestions { get; set; }
    }
}