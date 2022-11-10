using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class permission_handler
    {
        private permission_data obj_permission { get; set; }
        public permission_handler()
        {
            obj_permission = new permission_data();
        }

        public Int32 insert_update_permission_menu(Int32 id, string title, string name, string page_name)
        {
            return obj_permission.insert_update_permission_menu(id, title, name, page_name);
        }

        public DataSet get_permission_menu(Int32 id)
        {
            return obj_permission.get_permission_menu(id);
        }

        public bool delete_permission_menu(Int32 id)
        {
            return obj_permission.delete_permission_menu(id);
        }

        public Int32 insert_permission(Int32 permission_id, Int32 admin_id, Int32 id)
        {
            return obj_permission.insert_permission(permission_id, admin_id, id);
        }
        public DataSet get_permission_by_id(Int32 admin_id)
        {
            return obj_permission.get_permission_by_id(admin_id);
        }
    }
}