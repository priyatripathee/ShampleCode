using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class city_data
    {

        public Int32 insert_update_city(Int64 city_id, Int64 state_id,string city_name, bool is_active)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_city", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@city_id", SqlDbType.BigInt).Value = city_id;
                cmd.Parameters.Add("@state_id", SqlDbType.BigInt).Value = state_id;
                cmd.Parameters.Add("@city_name", SqlDbType.VarChar).Value = city_name;
                cmd.Parameters.Add("@is_active", SqlDbType.Bit).Value = is_active;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }

        public DataSet get_city(Int64? city_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@city_id", (city_id == null ? DBNull.Value : (object)city_id))
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_city", parameters);
            return ds;
        }
    }
}
