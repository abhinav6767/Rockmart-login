using System;
using System.Collections.Generic;

namespace Rockmart_login.Entities
{
    public partial class Query
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public string? QueryDesc { get; set; }
        public string? SenderPhone { get; set; }
        public string? SenderEmail { get; set; }
        public DateTime? Doq { get; set; }

        public virtual Business Reciever { get; set; } = null!;
        public virtual Business Sender { get; set; } = null!;
    }
}
