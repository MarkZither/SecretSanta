using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Blazor {
    public class NewParticipant {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
