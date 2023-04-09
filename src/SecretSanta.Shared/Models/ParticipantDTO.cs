using System;

namespace SecretSanta.Models
{
    public record ParticipantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Suggestions { get; set; }
        public int? BannedParticipantId { get; set; }
    }
}