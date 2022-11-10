using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class wishlist
    {
        public long wishlist_id { get; set; }
        public long product_id { get; set; }
        public string email_id { get; set; }
    }
}