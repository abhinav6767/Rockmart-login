using System;
using System.Collections.Generic;

namespace Rockmart_login.Entities
{
    public partial class Business
    {
        public Business()
        {
            Carts = new HashSet<Cart>();
            Items = new HashSet<Item>();
            Orders = new HashSet<Order>();
            QueryRecievers = new HashSet<Query>();
            QuerySenders = new HashSet<Query>();
        }

        public int Id { get; set; }
        public string BusinessUsername { get; set; } = null!;
        public string? BusinessName { get; set; }
        public string? BusinessAddress { get; set; }
        public string Password { get; set; } = null!;
        public string GstNumber { get; set; } = null!;
        public string? BusinessEmail { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Query> QueryRecievers { get; set; }
        public virtual ICollection<Query> QuerySenders { get; set; }
    }
}
