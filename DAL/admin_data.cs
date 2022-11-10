using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class admin_data
    {
        public long insert_update_superadmin(admin Admin)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_insert_update_superadmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@admin_id", Admin.admin_id);
                cmd.Parameters.AddWithValue("@user_name", Admin.user_name);
                cmd.Parameters.AddWithValue("@password", Admin.password);
                cmd.Parameters.AddWithValue("@first_name", Admin.first_name);
                cmd.Parameters.AddWithValue("@last_name", Admin.last_name);
                cmd.Parameters.AddWithValue("@permission", Admin.permission);
                cmd.Parameters.AddWithValue("@created_by", Admin.created_by);
                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                cmd.ExecuteNonQuery();
                long resultValue = Convert.ToInt32(retPram.Value);
                cmd.Parameters.Clear();
                con.Close();
                return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable get_superadmin(string user_name)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@user_name", user_name)
                };
                DataTable table = SqlHelper.ExecuteParamerizedSelectCommand(Connection.ConnstruttDB, "pr_get_superadmin", CommandType.StoredProcedure, parameters);
                return table;
            }
            catch
            {
                return null;
            }
        }


        public DataTable get_permission_name(Int32 admin_id, Int32 Flag)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@admin_id", admin_id),
                    new SqlParameter("@Flag", Flag)
                };
                DataTable table = SqlHelper.ExecuteParamerizedSelectCommand(Connection.ConnstruttDB, "pr_get_permission_name", CommandType.StoredProcedure, parameters);
                return table;
            }
            catch
            {
                return null;
            }
        }

        public DataTable get_user_type(Int64? admin_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@admin_id", (admin_id == null ? DBNull.Value : (object)admin_id))
                };
                DataTable table = SqlHelper.ExecuteParamerizedSelectCommand(Connection.ConnstruttDB, "pr_get_admin", CommandType.StoredProcedure, parameters);
                return table;
            }
            catch
            {
                return null;
            }
        }
        public bool delete_superadmin(int admin_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@admin_id", admin_id)
                };
                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_superadmin", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable get_adminlogin(admin Admin)
        {
            //SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            //SqlCommand cmd = new SqlCommand("pr_get_adminlogin", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@user_name", Admin.user_name),
                    new SqlParameter("@password", Admin.password),
                    new SqlParameter("@userType", Admin.userType)
                };
                DataTable table = SqlHelper.ExecuteParamerizedSelectCommand(Connection.ConnstruttDB, "pr_get_adminlogin", CommandType.StoredProcedure, parameters);
                return table;

                //cmd.Parameters.AddWithValue("@user_name", Admin.user_name);
                //cmd.Parameters.AddWithValue("@password", Admin.password);
                //cmd.Parameters.AddWithValue("@userType ", Admin.userType );
                //SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                //retPram.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(retPram);
                //con.Open();
                //cmd.ExecuteNonQuery();
                //int resultValue = Convert.ToInt32(retPram.Value);
                //con.Close();
                //return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int change_admin_password(admin _change)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_change_admin_password", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@user_name", _change.user_name);
                cmd.Parameters.AddWithValue("@old_password", _change.password);
                cmd.Parameters.AddWithValue("@new_password", _change.newpassword);
                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                cmd.ExecuteNonQuery();
                int resultValue = Convert.ToInt32(retPram.Value);
                con.Close();
                return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int AdminChangePassword(Admin _Admin, string newPassword)
        //{
        //    SqlConnection con = new SqlConnection(Connection.ConnS2SDB);
        //    SqlCommand cmd = new SqlCommand("prAdminChangePassword", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@UserName", _Admin.UserName);
        //        cmd.Parameters.AddWithValue("@NewPassword", newPassword);
        //        cmd.Parameters.AddWithValue("@Image", _Admin.Image);
        //        cmd.Parameters.AddWithValue("@Role", _Admin.Role);
        //        SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
        //        retPram.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(retPram);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        int resultValue = Convert.ToInt32(retPram.Value);
        //        con.Close();
        //        return resultValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public long insert_update_admin(Int32 adminId, string userName, string password, String fnmae, string lname, int isactive, string createdBy, string img, int type)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_admin", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@admin_id", SqlDbType.Int).Value = adminId;
                cmd.Parameters.AddWithValue("@user_name", SqlDbType.VarChar).Value = userName;
                cmd.Parameters.AddWithValue("@password", SqlDbType.VarChar).Value = password;
                cmd.Parameters.AddWithValue("@first_name", SqlDbType.VarChar).Value = fnmae;
                cmd.Parameters.AddWithValue("@last_name", SqlDbType.VarChar).Value = lname;
                cmd.Parameters.AddWithValue("@is_active", SqlDbType.Int).Value = isactive;
                cmd.Parameters.AddWithValue("@created_by", SqlDbType.VarChar).Value = createdBy;
                cmd.Parameters.AddWithValue("@profile_img", SqlDbType.VarChar).Value = img;
                cmd.Parameters.AddWithValue("@user_type", SqlDbType.Int).Value = type;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }


        public DataSet get_admin(Int32 admin_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@admin_id", admin_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_admin", parameters);
            return ds;
        }
        public DataTable get_companydetails(int companyid)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", companyid)
                };
                DataTable table = SqlHelper.ExecuteParamerizedSelectCommand(Connection.ConnstruttDB, "pr_getcompny_details", CommandType.StoredProcedure, parameters);
                return table;
            }
            catch
            {
                return null;
            }
        }

    }
}
