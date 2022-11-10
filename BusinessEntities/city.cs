using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class city
    {
        public Int64 city_id { get; set; }
        public Int64 state_id { get; set; }
        public string city_name { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }

    }
}
