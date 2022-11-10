using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class blog
    {
        public Int64 blog_id { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
    }
}
