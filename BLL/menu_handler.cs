using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class menu_handler
    {
        private menu_data menuData { get; set; }
        public menu_handler()
        {
            menuData = new menu_data();
        }
        public Int32 insert_update_menu(Int32 menu_id, string menu_name, string menu_url, string meta_title, string meta_keywords, string meta_description)
        {
            return menuData.insert_update_menu(menu_id, menu_name, menu_url, meta_title, meta_keywords, meta_description);
        }
        public DataSet get_menu(Int32 menu_id)
        {
            return menuData.get_menu(menu_id);
        }
        public bool delete_menu(Int32 menu_id)
        {
            return menuData.delete_menu(menu_id);
        }


        public Int32 insert_update_menu_sub(Int32 sub_menu_id, Int32 menu_id, string sub_menu_name, string sub_menu_url, byte? gendertype, short? orderby, bool is_new, string desc_header, string desc_footer, string meta_title, string meta_keywords, string meta_description)
        {
            return menuData.insert_update_menu_sub(sub_menu_id, menu_id, sub_menu_name, sub_menu_url, gendertype, orderby, is_new, desc_header, desc_footer, meta_title, meta_keywords, meta_description);
        }
        public DataSet get_menu_sub(Int32 menu_id, Int32 sub_menu_id, byte? gendertype)
        {
            return menuData.get_menu_sub(menu_id,sub_menu_id,gendertype);
        }
        public bool delete_menu_sub(Int32 sub_menu_id)
        {
            return menuData.delete_menu_sub(sub_menu_id);
        }

        public Int32 insert_update_menu_child(Int32 child_menu_id, Int32 sub_menu_id, string child_name, string child_menu_url)
        {
            return menuData.insert_update_menu_child(child_menu_id, sub_menu_id, child_name, child_menu_url);
        }
        public DataSet get_menu_child(Int32 sub_menu_id, Int32 child_menu_id)
        {
            return menuData.get_menu_child(sub_menu_id, child_menu_id);
        }
        public bool delete_menu_child(Int32 child_menu_id)
        {
            return menuData.delete_menu_child(child_menu_id);
        }
    }
}
