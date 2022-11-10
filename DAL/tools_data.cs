using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class tools_data
    {
        #region color 
        public Int32 insert_update_color(Int32 color_id, string color_name, string color_code)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_color", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@color_id", SqlDbType.Int).Value = color_id;
                cmd.Parameters.Add("@color_name", SqlDbType.VarChar).Value = color_name;
                cmd.Parameters.Add("@color_code", SqlDbType.VarChar).Value = color_code;
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
        public DataSet get_color(Int32? color_id,byte? is_active)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
                 new SqlParameter("@color_id", (color_id == null ? DBNull.Value : (object)color_id)),
                 new SqlParameter("@is_active", (is_active == null ? DBNull.Value : (object)is_active)),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_color", parameters);
            return ds;
        }
        public bool delete_color(Int64 color_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@color_id", color_id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_color", parameters);
            return resultValue > 0 ? true : false;
        }
        #endregion


        public Int32 insert_update_lookbook(Int32 lookbookId, string description, string image)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_lookbook", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@lookbookId", SqlDbType.Int).Value = lookbookId;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
                cmd.Parameters.Add("@image", SqlDbType.VarChar).Value = image;
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

        public DataSet get_lookbook(Int32? lookbookId, byte? is_active)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@lookbookId", (lookbookId == null ? DBNull.Value : (object)lookbookId)),
                 new SqlParameter("@is_active", (is_active == null ? DBNull.Value : (object)is_active)),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_lookbook", parameters);
            return ds;
        }

        public bool delete_lookbook(Int64 lookbookId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@lookbookId", lookbookId)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_lookbook", parameters);
            return resultValue > 0 ? true : false;
        }


        public Int32 insert_update_corporate(Int32 corporateId, string description, string image)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_corporate", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@corporateId", SqlDbType.Int).Value = corporateId;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
                cmd.Parameters.Add("@image", SqlDbType.VarChar).Value = image;
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
        public DataSet get_corporate(Int32? corporateId, byte? is_active)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@lookbookId", (corporateId == null ? DBNull.Value : (object)corporateId)),
                 new SqlParameter("@is_active", (is_active == null ? DBNull.Value : (object)is_active)),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_corporate", parameters);
            return ds;
        }

        public bool delete_corporate(Int64 corporateId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@corporateId", corporateId)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_corporate", parameters);
            return resultValue > 0 ? true : false;
        }

        #region material 
        public Int32 insert_update_material(Int32 material_id, string material_name)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_material", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@material_id", SqlDbType.Int).Value = material_id;
                cmd.Parameters.Add("@material_name", SqlDbType.VarChar).Value = material_name;
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
        public DataSet get_material(Int32 material_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@material_id", material_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_material", parameters);
            return ds;
        }
        public bool delete_material(Int64 material_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@material_id", material_id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_material", parameters);
            return resultValue > 0 ? true : false;
        }
        #endregion

        #region coupon 
        public Int32 insert_update_coupon(Int32 coupon_code_id, Int32 menu_id, string menu_name, Int32 sub_menu_id, string sub_menu_name, Int32 child_menu_id, string child_name, string coupon_code, Int32 price, string sender_email, string reciever_email, string created_by, string updated_by,byte? gendertype)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_update_coupon", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@coupon_code_id", SqlDbType.Int).Value = coupon_code_id;
                cmd.Parameters.Add("@menu_id", SqlDbType.Int).Value = menu_id;
                cmd.Parameters.Add("@menu_name", SqlDbType.VarChar).Value = menu_name;
                cmd.Parameters.Add("@sub_menu_id", SqlDbType.Int).Value = sub_menu_id;
                cmd.Parameters.Add("@sub_menu_name", SqlDbType.VarChar).Value = sub_menu_name;
                cmd.Parameters.Add("@child_menu_id", SqlDbType.Int).Value = child_menu_id;
                cmd.Parameters.Add("@child_name", SqlDbType.VarChar).Value = child_name;
                cmd.Parameters.Add("@coupon_code", SqlDbType.VarChar).Value = coupon_code;
                cmd.Parameters.Add("@price", SqlDbType.Int).Value = price;
                cmd.Parameters.Add("@sender_email", SqlDbType.VarChar).Value = sender_email;
                cmd.Parameters.Add("@reciever_email", SqlDbType.VarChar).Value = reciever_email;
                cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = created_by;
                cmd.Parameters.Add("@updated_by", SqlDbType.VarChar).Value = updated_by;
                if (gendertype.HasValue)
                    cmd.Parameters.Add("@gendertype", SqlDbType.TinyInt).Value = gendertype;
                else
                    cmd.Parameters.Add("@gendertype", SqlDbType.TinyInt).Value = DBNull.Value;

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
        public DataSet get_coupon(Int32 coupon_code_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@coupon_code_id", coupon_code_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_coupon", parameters);
            return ds;
        }
        public bool delete_coupon(Int32 coupon_code_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@coupon_code_id", coupon_code_id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_coupon", parameters);
            return resultValue > 0 ? true : false;
        }
        #endregion
    }
}
