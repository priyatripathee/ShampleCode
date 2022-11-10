using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class XpressLaneResponse
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
        public byte? coupondiscount { get; set; }    // from int to byte
        public int? discount { get; set; }
        public string subtotal { get; set; }
        public string total { get; set; }
        public string currency { get; set; }
        public List<OrderitemsList> orderitems { get; set; }

        //Start : Read json form Xpresslane Response

        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string billing_firstname { get; set; }
        public string billing_lastname { get; set; }
        public string billing_street_1 { get; set; }
        public string billing_street_2 { get; set; }
        public string billing_city { get; set; }
        public string billing_region { get; set; }
        public string billing_countryid { get; set; }
        public string billing_postcode { get; set; }
        public string billing_telephone { get; set; }
        public string shipping_firstname { get; set; }
        public string shipping_lastname { get; set; }
        public string shipping_street_1 { get; set; }
        public string shipping_street_2 { get; set; }
        public string shipping_city { get; set; }
        public string shipping_region { get; set; }
        public string shipping_countryid { get; set; }
        public string shipping_postcode { get; set; }
        public string shipping_telephone { get; set; }
        public string shipping_code { get; set; }
        public string shipping_title { get; set; }
        public byte? shipping_price { get; set; }
        public string xpresslane_payment_status { get; set; }
        public string xpresslane_txn_id { get; set; }
        public string totalAmount { get; set; }
        public decimal coupon_discount { get; set; }
        public string paymentmode { get; set; }

        //End : Read json form Xpresslane Response
    }

    public class OrderitemsList
    {
        public string productname { get; set; }
        public string sku { get; set; }
        public string productdescription { get; set; }
        public string unitprice { get; set; }
        public byte? discountamount { get; set; } // from int to byte
        public byte? discountunitprice { get; set; } // from int to byte
        public string originalprice { get; set; }
        public string actualprice { get; set; }
        public string productimage { get; set; }
        public int? merchantproductid { get; set; }
        public byte? quantity { get; set; } // from int to byte

        //Start : Read json form Xpresslane Response

        public int? id { get; set; }
        public string deliverydate { get; set; }
        public string shippingdate { get; set; }
        public string productbrand { get; set; }
        public string size { get; set; }
        public string discountpercent { get; set; }
        public string shippingcharge { get; set; }
        public string servicecharge { get; set; }
        public string servicetax { get; set; }
        public string vat { get; set; }
        public string customfield1 { get; set; }
        public string customfield2 { get; set; }
        //End : Read json form Xpresslane Response

    }



    //public class ShippingoptionsList
    //{
    //    public string shippingcode { get; set; }
    //    public string shippingname { get; set; }
    //    public string shippingprice { get; set; }
    //}


}

