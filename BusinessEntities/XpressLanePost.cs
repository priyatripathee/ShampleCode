using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class XpressLanePost
    {

        public string merchantsuccessurl { get; set; }
        public string merchantcarturl { get; set; }
        public string merchantid { get; set; }
        public string secretkey { get; set; }
        public string merchantorderid { get; set; }
        public string orderdate { get; set; }
        public string grandtotal { get; set; }
        public string preshiptotal { get; set; }
        public string coupon_code { get; set; }
        public string cgst { get; set; }
        public decimal coupondiscount { get; set; }
        public int? discount { get; set; }
        public string subtotal { get; set; }
        public string total { get; set; }
        public string currency { get; set; }
        public List<OrderitemList> orderitems { get; set; }

    }
    public class OrderitemList
    {
        public string productname { get; set; }
        public string sku { get; set; }
        public string productdescription { get; set; }
        public string unitprice { get; set; }
        public int? discountamount { get; set; }
        public int? discountunitprice { get; set; }
        public string originalprice { get; set; }
        public string actualprice { get; set; }
        public string productimage { get; set; }
        public int? merchantproductid { get; set; }
        public int? quantity { get; set; }



    }


}

