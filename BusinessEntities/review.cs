using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class review
    {
        public long review_id { get; set; }
        public long product_id { get; set; }
        public string user_name { get; set; }
        public long rating { get; set; }
        public string title { get; set; }
        public string reviews { get; set; }

    }
}
