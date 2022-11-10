using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;


namespace DAL
{
    public class temp_cart_data
    {
        public Int32 insert_temp_cart(BusinessEntities.temp_cart _cart)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_temp_cart", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@temp_customer_id", SqlDbType.Int).Value = _cart.temp_customer_id;
                cmd.Parameters.Add("@product_id", SqlDbType.BigInt).Value = _cart.product_id;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = _cart.quantity;

                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                cmd.ExecuteNonQuery();
                int resultValue = Convert.ToInt32(retPram.Value);
                cn.Close();
                return resultValue;
            }
        }

        public Int32 insert_temp_customer(BusinessEntities.temp_customer _customer)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_insert_temp_customer", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = _customer.email_id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = _customer.name;
                cmd.Parameters.Add("@contact_number", SqlDbType.NVarChar).Value = _customer.contact_number;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = _customer.address;
                cmd.Parameters.Add("@land_mark", SqlDbType.NVarChar).Value = _customer.land_mark;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = _customer.city;
                cmd.Parameters.Add("@state", SqlDbType.NVarChar).Value = _customer.state;
                cmd.Parameters.Add("@country", SqlDbType.NVarChar).Value = _customer.country;
                cmd.Parameters.Add("@pin_code", SqlDbType.NVarChar).Value = _customer.pin_code;
                cmd.Parameters.Add("@email_count", SqlDbType.Bit).Value = _customer.email_sent;
                cmd.Parameters.Add("@customer_medium", SqlDbType.NVarChar).Value = _customer.customer_medium;

                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                cn.Open();
                cmd.ExecuteNonQuery();
                int resultValue = Convert.ToInt32(retPram.Value);
                cn.Close();
                return resultValue;
            }
        }

        public Int32 update_temp_customer(BusinessEntities.temp_customer _customer)
        {
            using (SqlConnection cn = new SqlConnection(Connection.ConnstruttDB))
            {
                SqlCommand cmd = new SqlCommand("pr_update_temp_customer", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = _customer.email_id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = _customer.name;
                cmd.Parameters.Add("@contact_number", SqlDbType.NVarChar).Value = _customer.contact_number;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = _customer.address;
                cmd.Parameters.Add("@land_mark", SqlDbType.NVarChar).Value = _customer.land_mark;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = _customer.city;
                cmd.Parameters.Add("@state", SqlDbType.NVarChar).Value = _customer.state;
                cmd.Parameters.Add("@country", SqlDbType.NVarChar).Value = _customer.country;
                cmd.Parameters.Add("@pin_code", SqlDbType.NVarChar).Value = _customer.pin_code;

                cn.Open();
                int resultValue = cmd.ExecuteNonQuery();
                cn.Close();
                return resultValue;
            }
        }

        public bool delete_temp_cart_customer(string email_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@email_id", email_id)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_temp_customer", parameters);
            return resultValue > 0 ? true : false;
        }

        public DataSet get_temp_customer(DateTime? from_date, DateTime? to_date, bool? email_sent)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@from_date", (from_date == null ? DBNull.Value : (object)from_date)),
                new SqlParameter("@to_date", (to_date == null ? DBNull.Value : (object)to_date)),
                new SqlParameter("@email_sent", (email_sent == null ? DBNull.Value : (object)email_sent)),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_temp_customer", parameters);
            return ds;
        }

        public DataSet get_temp_customer_cart(string email_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                            new SqlParameter("@email_id", email_id),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_Get_temp_customer_cart", parameters);
            return ds;
        }

    }
}
