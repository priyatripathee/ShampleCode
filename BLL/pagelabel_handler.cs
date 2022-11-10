using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;
namespace BLL
{
   public class pagelabel_handler
    {
        private  pagelabel_data pagelableData { get; set; }
        public  pagelabel_handler()
        {
            pagelableData = new pagelabel_data();
        }
        public Int32 insert_update_pagelabel(Int32 label_id, string page_name, string lable_name,string label_value, DateTime modified_date, string modified_by)
        {
            return pagelableData.insert_update_pagelabel(label_id, page_name, lable_name, label_value, modified_date, modified_by);
        }
        public DataSet get_pagelabel(Int32? label_id)
        {
            return pagelableData.get_pagelabel(label_id);
        }

        public bool delete_pagelabel(Int32 label_id)
        {
            return pagelableData.delete_pagelabel(label_id);
        }
    }
}
