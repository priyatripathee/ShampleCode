using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;



namespace DAL
{
  public  class pagelabel_data
    {

        public Int32 insert_update_pagelabel(Int32 label_id, string page_name, string label_name, string label_value,DateTime modified_date,string modified_by)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_pagelabel", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@label_id", SqlDbType.Int).Value = label_id;
                cmd.Parameters.Add("@page_name", SqlDbType.VarChar).Value = page_name;
                cmd.Parameters.Add("@label_name", SqlDbType.VarChar).Value = label_name;
                cmd.Parameters.Add("@label_value", SqlDbType.VarChar).Value = label_value;
                cmd.Parameters.Add("@modified_date", SqlDbType.DateTime).Value = modified_date;
                cmd.Parameters.Add("@modified_by", SqlDbType.VarChar).Value = modified_by;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_pagelabel(Int32? label_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@label_id", (label_id == null ? DBNull.Value : (object)label_id)),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_pagelabel", parameters);
            return ds;
        }
        public bool delete_pagelabel(Int32 label_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@label_id", label_id)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_pagelabel", parameters);
            return resultValue > 0 ? true : false;
        }
    }
}
