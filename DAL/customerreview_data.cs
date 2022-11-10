using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
   public class customerreview_data
    {
        public DataSet get_customerreviw(Int32? id, Guid? customer_id, bool? is_active)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", (id == null ? DBNull.Value : (object)id)),
                new SqlParameter("@customer_id", (customer_id == null ? DBNull.Value : (object)customer_id)),
                new SqlParameter("@is_active", (is_active == null ? DBNull.Value : (object)is_active))
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_customer_review_SelectAll", parameters);
            return ds;
        }
        public DataSet get_customer_review_Top5(Int32? id, Guid? customer_id, bool? is_active)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", (id == null ? DBNull.Value : (object)id)),
                new SqlParameter("@customer_id", (customer_id == null ? DBNull.Value : (object)customer_id)),
                new SqlParameter("@is_active", (is_active == null ? DBNull.Value : (object)is_active))
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_customer_review_Top5", parameters);
            return ds;
        }
        public DataSet get_customerreviwone(Int32? id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", (id == null ? DBNull.Value : (object)id))
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_customer_review_selectOne", parameters);
            return ds;
        }


        public DataSet get_customercomment(Int32? id, Int32 customer_review_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", (id == null ? DBNull.Value : (object)id)),
                new SqlParameter("@customer_review_id",(customer_review_id == null ? DBNull.Value : (object)customer_review_id))
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_customer_review_comment_SelectAll", parameters);
            return ds;
        }
        public Int32 insert_update_customerComment(Int32 customer_review_id, Guid customer_id, string comment,bool is_active)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_customer_review_comment", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@customer_review_id", SqlDbType.BigInt).Value = customer_review_id;
                cmd.Parameters.Add("@customer_id", SqlDbType.UniqueIdentifier).Value = customer_id;
                cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = comment;
                cmd.Parameters.Add("@is_active", SqlDbType.Bit).Value = is_active;
                SqlParameter retPram = new SqlParameter("@id", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public Int32 insert_update_customerreview(int id, Guid? customer_id, string image_name, string title, Int32? count_view,string 
            description,string description_short, bool is_active)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_customerreview", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                cmd.Parameters.Add("@customer_id", SqlDbType.UniqueIdentifier).Value = customer_id;
                cmd.Parameters.Add("@image_name", SqlDbType.VarChar).Value = image_name;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.Add("@count_view", SqlDbType.BigInt).Value = count_view;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
                cmd.Parameters.Add("@description_short", SqlDbType.VarChar).Value = description_short;
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

        public bool delete_customerreview(Int32 id, ref string imageName, ref string returnMessage)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_delete_customer_review", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@id", id);
                SqlParameter retPram = new SqlParameter("@ReturnImageName", SqlDbType.VarChar, 200);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                int resultValue = cmd.ExecuteNonQuery();
                imageName = retPram.Value.ToString();
                con.Close();
                if (resultValue > 0)
                {
                    returnMessage = "Customer review deleted successfully";
                    return true;
                }
                else
                {
                    returnMessage = "Error occurred";
                    return false;
                }
            }
            
            catch (Exception)
            {
                returnMessage = "Error occurred";
                return false;
            }
        }
    }
}
