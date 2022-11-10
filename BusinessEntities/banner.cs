using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class banner
    {
        public Int32 banner_id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string url_path { get; set; }
        public byte type { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
        public byte order_by { get; set; }

        public Int32 category_link_id { get; set; }
        public string category_link_url { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
    }
}
