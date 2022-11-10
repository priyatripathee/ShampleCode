using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace strutt.api
{
    public class PickerOrderModel
    {
        //Request Param

        public string auth_token { get; set; }
        public string item_name { get; set; }

        public List<item_list> Item_List { get; set; }
        public class item_list
        {
            public decimal price { get; set; }
            public string item_name { get; set; }
            public int quantity { get; set; }
            public string sku { get; set; }
            public int item_tax_percentage { get; set; }
        }

        public string from_name { get; set; }
        public string from_email { get; set; }
        public string from_phone_number { get; set; }
        public string from_address { get; set; }
        public string from_pincode { get; set; }
        public string pickup_gstin { get; set; }
        public string to_name { get; set; }
        public string to_email { get; set; }
        public string to_phone_number { get; set; }
        public string to_pincode { get; set; }
        public string to_address { get; set; }
        public int quantity { get; set; }
        public decimal invoice_value { get; set; }
        public decimal cod_amount { get; set; }
        public string client_order_id { get; set; }
        public int item_breadth { get; set; }

        public int item_height { get; set; }
        public int item_length { get; set; }
        public decimal item_weight { get; set; }
        public int item_tax_percentage { get; set; }
        public string invoice_number { get; set; }
        public int total_discount { get; set; }

        public int shipping_charge { get; set; }
        public int transaction_charge { get; set; }
        public int giftwrap_charge { get; set; }
        public string ewaybill_no { get; set; }
        public bool has_dg { get; set; }
        public bool is_reverse { get; set; }
        public bool has_surface { get; set; }
        public bool next_day_delivery { get; set; }

        //Response Param

        public bool success { get; set; }
        public string order_id { get; set; }
        public int order_pk { get; set; }
        public string tracking_id { get; set; }
        public string manifest_link { get; set; }
        public string routing_code { get; set; }
        public string clientorder_id { get; set; }
        public string courier { get; set; }
        public string dispatch_mode { get; set; }

        public List<child_waybill_list> childwaybill_list { get; set; }
        public class child_waybill_list
        {

        }


    }
}