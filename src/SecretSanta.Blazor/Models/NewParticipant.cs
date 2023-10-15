using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SecretSanta.Blazor {
    public class NewParticipant {
        [SetsRequiredMembers]
        public NewParticipant(string name, string email) {
            Name = name;
            Email = email;
        }

        [Required]
        public required string Name { get; set; } = string.Empty;
        [Required]
        public required string Email { get; set; } = string.Empty;
    }
}
