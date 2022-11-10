using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class admin_data_handler
    {
        private admin_data adminData { get; set; }
        public admin_data_handler()
        {
            adminData = new admin_data();
        }

        public long insert_update_superadmin(admin Admin)
        {
            return adminData.insert_update_superadmin(Admin);
        }

        public long insert_update_admin(Int32 adminId, string userName, string password, String fnmae, string lname, int isactive, string createdBy, string img, int type)
        {
            return adminData.insert_update_admin(adminId, userName, password, fnmae, lname, isactive, createdBy, img, type);
        }
        public DataTable get_superadmin(string user_name)
        {
            return adminData.get_superadmin(user_name);
        }

        public DataTable get_permission_name(Int32 admin_id, Int32 Flag)
        {
            return adminData.get_permission_name(admin_id, Flag);
        }

        public bool delete_superadmin(int admin_id)
        {
            return adminData.delete_superadmin(admin_id);
        }

        public DataTable get_adminlogin(admin Admin)
        {
            return adminData.get_adminlogin(Admin);
        }

        public int change_admin_password(admin _change)
        {
            return adminData.change_admin_password(_change);
        }

        public DataTable get_user_type(Int64? admin_id)
        {
            adminData = new admin_data();
            return adminData.get_user_type(admin_id);
        }
        public DataSet get_admin(Int32 adminId)
        {
            return adminData.get_admin(adminId);
        }
        public DataTable get_companydetails(int companyid)
        {
            return adminData.get_companydetails(companyid);
        }
    }
}
