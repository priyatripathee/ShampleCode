using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class blog_data
    {
        public bool insert_update_blog(blog _blog, ref string returnMessage)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
		        {
			        new SqlParameter("@blog_id", _blog.blog_id),
                    new SqlParameter("@title", _blog.title),
                    new SqlParameter("@name", _blog.name),
                    new SqlParameter("@description", _blog.description),
                    new SqlParameter("@Image", _blog.image)

		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_blog", parameters);
                if (resultValue > 0)
                {
                    returnMessage = "Blog saved successfully";
                    return true;
                }
                else
                {
                    returnMessage = "Error occurred";
                    return false;
                }
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    returnMessage = "Blog title already exists. Try another title name.";
                    return false;
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
        
        public DataSet get_bog(Int64 blog_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@blog_id", blog_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_bog", parameters);
            return ds;
        }
        public bool delete_blog(long blog_id, ref string imageName, ref string returnMessage)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_delete_blog", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@blog_id", blog_id);
                SqlParameter retPram = new SqlParameter("@ReturnImageName", SqlDbType.VarChar, 200);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                int resultValue = cmd.ExecuteNonQuery();
                imageName = retPram.Value.ToString();
                con.Close();
                if (resultValue > 0)
                {
                    returnMessage = "Blog deleted successfully";
                    return true;
                }
                else
                {
                    returnMessage = "Error occurred";
                    return false;
                }
            }
            catch (SqlException se)
            {
                if (se.Number == 547)
                {
                    returnMessage = "Blog can't be deleted because it referenced with other resources.";
                    return false;
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


        public Int32 insert_update_blogComment(Int32 blog_id, Guid customer_id, string comment, bool is_active)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_customer_blog_comment", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@blog_id", SqlDbType.BigInt).Value = blog_id;
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

        public DataSet get_customerblogcomment(Int32? id, Int32 blog_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", (id == null ? DBNull.Value : (object)id)),
                new SqlParameter("@blog_id",blog_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_customer_blog_comment_SelectAll", parameters);
            return ds;
        }
    }
}
