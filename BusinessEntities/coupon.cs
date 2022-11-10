using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class coupon
    {
        public int coupon_code_id { get; set; }
        public long menu_id { get; set; }
        public string menu_name { get; set; }
        public long sub_menu_id { get; set; }
        public string sub_menu_name { get; set; }
        public long child_menu_id { get; set; }
        public string child_name { get; set; }
        public string coupon_code { get; set; }
        public int price { get; set; }
        public string sender_email { get; set; }
        public string reciever_email { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        
    }
}
