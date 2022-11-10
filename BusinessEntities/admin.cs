using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class admin
    {
        public Int32 admin_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string newpassword { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string permission { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public string profile_img { get; set; }
        public int userType { get; set; }

    }
}
