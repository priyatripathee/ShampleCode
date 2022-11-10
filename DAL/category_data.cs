using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class category_data
    {
        public Int32 insert_update_Category(Int64 category_id, string name, Int32 parent_id, string category_url, string image, string created_by, string updated_by)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_Category", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@category_id", SqlDbType.BigInt).Value = category_id;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@parent_id", SqlDbType.Int).Value = parent_id;
                cmd.Parameters.Add("@category_url", SqlDbType.VarChar).Value = category_url;
                cmd.Parameters.Add("@image", SqlDbType.VarChar).Value = image;
                cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = created_by;
                cmd.Parameters.Add("@updated_by", SqlDbType.VarChar).Value = updated_by;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                //int ret = Convert.ToInt32(retPram.Value);
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_category(Int64 category_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@category_id", category_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_category", parameters);
            return ds;
        }
        public bool delete_category(Int64 category_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@category_id", category_id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_category", parameters);
            return resultValue > 0 ? true : false;
        }
    }
}