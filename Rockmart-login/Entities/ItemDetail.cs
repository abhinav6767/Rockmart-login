using System;
using System.Collections.Generic;

namespace Rockmart_login.Entities
{
    public partial class ItemDetail
    {
        public int Id { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemImage { get; set; }
        public int ItemCategory { get; set; }
        public int ItemCode { get; set; }

        public virtual Category ItemCategoryNavigation { get; set; } = null!;
        public virtual Item ItemCodeNavigation { get; set; } = null!;
    }
}
