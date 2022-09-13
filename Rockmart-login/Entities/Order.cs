using System;
using System.Collections.Generic;

namespace Rockmart_login.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ItemQuantity { get; set; }
        public DateTime? Dfo { get; set; }
        public int ItemCode { get; set; }

        public virtual Item ItemCodeNavigation { get; set; } = null!;
        public virtual Business User { get; set; } = null!;
    }
}
