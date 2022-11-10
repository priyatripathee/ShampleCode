using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class temp_cart
    {
        public Int32 order_id { get; set; }
        public Int32 temp_customer_id { get; set; }
        public Int64 product_id { get; set; }
        public Int32 quantity { get; set; }
        public DateTime created_date { get; set; }
    }
}
