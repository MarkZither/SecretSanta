using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int GifterId { get; set; }
        public int RecipientId { get; set; }
    }
}
