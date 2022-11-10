using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class category
    {
        public Int32 pageIndex { get; set; }
        public Int32 PageSize { get; set; }
        public bool is_active { get; set; }
        public bool is_default { get; set; }
        public Int32 menu_id { get; set; }
        public Int32 sub_menu_id { get; set; }
        public Int32 Flag { get; set; }
    }
}
