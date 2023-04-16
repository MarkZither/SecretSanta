using System;

namespace SecretSanta.Models
{
    public record ParticipantDTO
    {
        public ParticipantDTO() {
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Suggestions { get; set; }
        public int? BannedParticipantId { get; set; }
    }
}