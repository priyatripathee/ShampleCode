using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class banner_data
    {

        public bool insert_update_banner(banner _banner, ref string returnMessage)
        {
            //try
            //{
                SqlParameter[] parameters = new SqlParameter[]
		        {
			        new SqlParameter("@banner_id", _banner.banner_id),
                    new SqlParameter("@title", _banner.title),
                    new SqlParameter("@Image", _banner.image),
                    new SqlParameter("@url_path", _banner.url_path),
                    new SqlParameter("@type", _banner.type),
                    new SqlParameter("@order_by", _banner.order_by)

                };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_banner", parameters);
                if (resultValue > 0)
                {
                    returnMessage = "Banner saved successfully";
                    return true;
                }
                else
                {
                    returnMessage = "Error occurred";
                    return false;
                }
            //}
            //catch (SqlException se)
            //{
            //    if (se.Number == 2627)
            //    {
            //        returnMessage = "Banner title already exists. Try another title name.";
            //        return false;
            //    }
            //    else
            //    {
            //        returnMessage = "Error occurred";
            //        return false;
            //    }
            //}
            //catch (Exception)
            //{
            //    returnMessage = "Error occurred";
            //    return false;
            //}
        }
        public DataSet get_banner(Int32? banner_id, byte? type)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@banner_id", (banner_id == null ? DBNull.Value : (object)banner_id)),
                 new SqlParameter("@type", (type == null ? DBNull.Value : (object)type))
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_banner", parameters);
            return ds;
        }
        //public DataSet get_banner(Int32? banner_id)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //         new SqlParameter("@banner_id", (banner_id == null ? DBNull.Value : (object)banner_id)),
        //    };
        //    DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_banner", parameters);
        //    return ds;
        //}
        public bool delete_banner(long banner_id, ref string imageName, ref string returnMessage)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_delete_banner", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@banner_id", banner_id);
                SqlParameter retPram = new SqlParameter("@ReturnImageName", SqlDbType.VarChar, 200);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                int resultValue = cmd.ExecuteNonQuery();
                imageName = retPram.Value.ToString();
                con.Close();
                if (resultValue > 0)
                {
                    returnMessage = "Banner deleted successfully";
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
                    returnMessage = "Banner can't be deleted because it referenced with other resources.";
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

        public bool insert_update_Category_link(banner _advertisment, ref string returnMessage)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
		        {
			        new SqlParameter("@category_link_id", _advertisment.category_link_id),
                    new SqlParameter("@title", _advertisment.title),
                    new SqlParameter("@Image", _advertisment.image),
                    new SqlParameter("@category_link_url", _advertisment.category_link_url),
                    new SqlParameter("@created_by", _advertisment.created_by),
                    new SqlParameter("@updated_by", _advertisment.updated_by)

		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_Category_link", parameters);
                if (resultValue > 0)
                {
                    returnMessage = "Advertisment saved successfully";
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
                    returnMessage = "Advertisment title already exists. Try another title name.";
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
        public DataSet get_category_link(Int32 category_link_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@category_link_id", category_link_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_category_link", parameters);
            return ds;
        }
        public bool delete_category_link(Int32 category_link_id, ref string imageName, ref string returnMessage)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_delete_category_link", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@category_link_id", category_link_id);
                SqlParameter retPram = new SqlParameter("@ReturnImageName", SqlDbType.VarChar, 200);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                int resultValue = cmd.ExecuteNonQuery();
                imageName = retPram.Value.ToString();
                con.Close();
                if (resultValue > 0)
                {
                    returnMessage = "Advertisment deleted successfully";
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
                    returnMessage = "Advertisment can't be deleted because it referenced with other resources.";
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
    }
}
