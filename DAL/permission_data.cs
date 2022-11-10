using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class permission_data
    {
        public Int32 insert_update_permission_menu(Int32 id, string title, string name, string page_name)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_permission_menu", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@page_name", SqlDbType.VarChar).Value = page_name;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_permission_menu(Int32 id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@id", id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_permission_menu", parameters);
            return ds;
        }
        public bool delete_permission_menu(Int32 id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@id", id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_permission_menu", parameters);
            return resultValue > 0 ? true : false;
        }

        public Int32 insert_permission(Int32 permission_id, Int32 admin_id, Int32 id)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_permission", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@permission_id", SqlDbType.Int).Value = permission_id;
                cmd.Parameters.Add("@admin_id", SqlDbType.Int).Value = admin_id;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }

        public DataSet get_permission_by_id(Int32 admin_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@admin_id", admin_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_permission_by_id", parameters);
            return ds;
        }
    }
}
