using System;
using System.Collections.Generic;

namespace Rockmart_login.Entities
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ItemQuantity { get; set; }
        public int ItemCode { get; set; }

        public virtual Item ItemCodeNavigation { get; set; } = null!;
        public virtual Business User { get; set; } = null!;
    }
}
