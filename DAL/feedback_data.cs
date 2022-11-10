using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class feedback_data
    {
        public Int32 insert_feedback(Guid customer_id, byte rating, string category, string suggestion)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_feedback", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@customer_id", SqlDbType.UniqueIdentifier).Value = customer_id;
                cmd.Parameters.Add("@rating", SqlDbType.TinyInt).Value = rating;
                cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = category;
                cmd.Parameters.Add("@suggestion", SqlDbType.VarChar).Value = suggestion;

                SqlParameter retPram = new SqlParameter("@id", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);

                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_feedback(DateTime? FromDate, DateTime? ToDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@from_date", FromDate),
                new SqlParameter("@to_date", ToDate),
                //new SqlParameter("@customer_id", (customer_id == null ? DBNull.Value : (object)customer_id)),
                //new SqlParameter("@rating", rating),
                //new SqlParameter("@category",category),
                //new SqlParameter("@suggestion",suggestion),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_feedback", parameters);
            return ds;
        }
    }
}
