using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class order
    {

        // product table
        public long product_id { get; set; }
        public string ship_via { get; set; }
        public string ship_id { get; set; }
        public string freight { get; set; }

        //order table
        public Int64 order_id { get; set; }
        public Guid customer_id { get; set; }
        public Guid order_number { get; set; }
        public string sale_price { get; set; }
        public string price { get; set; }
        public string discount_price { get; set; }
        public string shipping_price { get; set; }
        public string coupon_price { get; set; }
        public string coupon_code { get; set; }
        public string payment_response { get; set; }

        //order detail table
        public int quantity { get; set; }
        public Int64 order_detail_id { get; set; }
        public string order_status { get; set; }
        public string reason_for_cancellation { get; set; }
        public string Comments { get; set; }
        public string custom_bag_price { get; set; }
        public string custom_bag_param { get; set; }
        public float x_point { get; set; }
        public float y_point { get; set; }
        public int Flag { get; set; }

        //public long GiftProductId { get; set; }

        public string user_name { get; set; }
        public string TotalPrice { get; set; }
        public string thumb_image { get; set; }
        public string PaymentThrough { get; set; }
        public string contact_number { get; set; }
        public string address { get; set; }
        public string land_mark { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pin_code { get; set; }
        public string message { get; set; }
        public bool in_stock { get; set; }

        //------------------ Leave Feedback
        public string order_date { get; set; }
        public string product_name { get; set; }
        public long LeaveFeedbackId { get; set; }
        public string email_id { get; set; }
        public int rating { get; set; }
        public bool ItemArrived { get; set; }
        public bool ItemDescribed { get; set; }
        public bool DepartureOnTime { get; set; }
        public string FeedbackComment { get; set; }
        // raise complain

        //public long RaiseId { get; set; }
        //public string LoginId { get; set; }
        //public string Name { get; set; }
        //public string Contact { get; set; }
        //public string ComplainType { get; set; }
        //public string Details { get; set; }

        //--------------------raise a complaint

        public long raise_id { get; set; }
        public string login_id { get; set; }
        public string complain_type { get; set; }
        public string details { get; set; }

        public Guid XpressMerchantorder_id { get; set; }

        public string manifest_link { get; set; }
    }
}
