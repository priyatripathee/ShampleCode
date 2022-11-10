using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class pagelabel
    {
        public Int32 label_id { get; set; }
        public string page_name { get; set; }
        public string lable_name { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }
    }
}
