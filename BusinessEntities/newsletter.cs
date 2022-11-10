using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class newsletter
    {
        public long news_letter_id { get; set; }
        public string email { get; set; }
        public string url { get; set; }
        public string code { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
    }
}
