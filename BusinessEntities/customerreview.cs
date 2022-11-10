using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class customerreview
    {
        public Int32 id { get; set; }
        public Guid customer_id { get; set; }
        public string image_name { get; set; }
        public string title { get; set; }
        public string count_view { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public string createddate { get; set; }
    }
}
