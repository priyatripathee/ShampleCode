using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class feedback
    {
        public int id { get; set; }
        public Guid customer_id { get; set; }
        public  byte rating { get; set; }
        public string category { get; set; }
        public string suggestion { get; set; }
        public DateTime createddate { get; set; }
    }
}
