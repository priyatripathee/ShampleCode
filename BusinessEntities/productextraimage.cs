using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class productextraimage
    {

        public Int64 product_image_extra_id { get; set; }
        public Int32 product_id { get; set; }
        public string thumb_image { get; set; }
        public bool is_active { get; set; }
        public Byte image_order { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
    }
}
