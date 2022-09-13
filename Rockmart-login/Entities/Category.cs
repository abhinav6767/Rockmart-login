using System;
using System.Collections.Generic;

namespace Rockmart_login.Entities
{
    public partial class Category
    {
        public Category()
        {
            ItemDetails = new HashSet<ItemDetail>();
        }

        public int Id { get; set; }
        public string? ItemCategory { get; set; }

        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
    }
}
