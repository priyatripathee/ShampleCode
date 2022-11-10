using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class temp_customer
    {
        public Int32 temp_customer_id { get; set; }
        public string email_id { get; set; }
        public string name { get; set; }
        public string contact_number { get; set; }
        public string address { get; set; }
        public string land_mark { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pin_code { get; set; }
        public bool email_sent { get; set; }

        public string customer_medium { get; set; }
        public DateTime created_date { get; set; }
    }
}
