using System;
using System.Collections.Generic;

namespace Rockmart_login.Entities
{
    public partial class Item
    {
        public Item()
        {
            Carts = new HashSet<Cart>();
            ItemDetails = new HashSet<ItemDetail>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string ItemName { get; set; } = null!;
        public int? ItemQuantity { get; set; }
        public int? ItemPrice { get; set; }

        public virtual Business User { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
