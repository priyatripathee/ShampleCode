using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class category_handler
    {
        private category_data categoryData { get; set; }
        public category_handler()
        {
            categoryData = new category_data();
        }

        public Int32 insert_update_Category(Int64 category_id, string name, Int32 parent_id, string category_url, string image, string created_by, string updated_by)
        {
            return categoryData.insert_update_Category(category_id, name, parent_id, category_url, image, created_by, updated_by);
        }

        public DataSet get_category(Int64 category_id)
        {
            return categoryData.get_category(category_id);
        }

        public bool delete_category(Int64 category_id)
        {
            return categoryData.delete_category(category_id);
        }
    }
}
