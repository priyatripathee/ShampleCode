using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class menu_data
    {
        public Int32 insert_update_menu(Int32 menu_id, string menu_name, string menu_url,string meta_title,string meta_keywords,string meta_description)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_menu", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@menu_id", SqlDbType.BigInt).Value = menu_id;
                cmd.Parameters.Add("@menu_name", SqlDbType.VarChar).Value = menu_name;
                cmd.Parameters.Add("@menu_url", SqlDbType.VarChar).Value = menu_url;
                cmd.Parameters.Add("@meta_title", SqlDbType.VarChar).Value = meta_title;
                cmd.Parameters.Add("@meta_keywords", SqlDbType.VarChar).Value = meta_keywords;
                cmd.Parameters.Add("@meta_description", SqlDbType.VarChar).Value = meta_description;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_menu(Int32 menu_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@menu_id", menu_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_menu", parameters);
            return ds;
        }
        public bool delete_menu(Int32 menu_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@menu_id", menu_id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_menu", parameters);
            return resultValue > 0 ? true : false;
        }

        public Int32 insert_update_menu_sub(Int32 sub_menu_id, Int32 menu_id, string sub_menu_name, string sub_menu_url,byte? gendertype, short? orderby, bool is_new, string desc_header, string desc_footer, string meta_title, string meta_keywords, string meta_description)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_menu_sub", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@sub_menu_id", SqlDbType.BigInt).Value = sub_menu_id;
                cmd.Parameters.Add("@menu_id", SqlDbType.BigInt).Value = menu_id;
                cmd.Parameters.Add("@sub_menu_name", SqlDbType.VarChar).Value = sub_menu_name;
                cmd.Parameters.Add("@sub_menu_url", SqlDbType.VarChar).Value = sub_menu_url;

                cmd.Parameters.Add("@gendertype", SqlDbType.VarChar).Value = (gendertype == null ? DBNull.Value : (object)gendertype);
                cmd.Parameters.Add("@orderby", SqlDbType.SmallInt).Value = (orderby == null ? DBNull.Value : (object)orderby);
                cmd.Parameters.Add("@is_new", SqlDbType.Bit).Value = is_new;
                cmd.Parameters.Add("@desc_header", SqlDbType.VarChar).Value = desc_header;
                cmd.Parameters.Add("@desc_footer", SqlDbType.VarChar).Value = desc_footer;
                cmd.Parameters.Add("@meta_title", SqlDbType.VarChar).Value = meta_title;
                cmd.Parameters.Add("@meta_keywords", SqlDbType.VarChar).Value = meta_keywords;
                cmd.Parameters.Add("@meta_description", SqlDbType.VarChar).Value = meta_description;
                //if(gendertype == null)
                //    cmd.Parameters.Add("@gendertype", SqlDbType.TinyInt).Value = DBNull.Value;
                //else
                //    cmd.Parameters.Add("@gendertype", SqlDbType.TinyInt).Value = gendertype;


                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_menu_sub(Int32 menu_id, Int32 sub_menu_id, byte? gendertype)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@menu_id", menu_id),
                new SqlParameter("@sub_menu_id", sub_menu_id),
                new SqlParameter("@gendertype", gendertype),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_menu_sub", parameters);
            return ds;
        }
        public bool delete_menu_sub(Int32 sub_menu_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@sub_menu_id", sub_menu_id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_menu_sub", parameters);
            return resultValue > 0 ? true : false;
        }

        public Int32 insert_update_menu_child(Int32 child_menu_id, Int32 sub_menu_id, string child_name, string child_menu_url)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_menu_child", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@child_menu_id", SqlDbType.BigInt).Value = child_menu_id;
                cmd.Parameters.Add("@sub_menu_id", SqlDbType.BigInt).Value = sub_menu_id;
                cmd.Parameters.Add("@child_name", SqlDbType.VarChar).Value = child_name;
                cmd.Parameters.Add("@child_menu_url", SqlDbType.VarChar).Value = child_menu_url;
                SqlParameter retPram = new SqlParameter("@return_value", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }
        public DataSet get_menu_child(Int32 sub_menu_id, Int32 child_menu_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@sub_menu_id", sub_menu_id),
                new SqlParameter("@child_menu_id", child_menu_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_menu_child", parameters);
            return ds;
        }
        public bool delete_menu_child(Int32 child_menu_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@child_menu_id", child_menu_id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_menu_child", parameters);
            return resultValue > 0 ? true : false;
        }

    }
}
