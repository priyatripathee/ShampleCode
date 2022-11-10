using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class customer_details_data
    {

        public bool insert_update_customer_login_details(customer _Customer, ref Guid customer_id)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
                SqlCommand cmd = new SqlCommand("pr_insert_update_customer_login_details", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@customer_id", _Customer.customer_id);
                cmd.Parameters.AddWithValue("@customer_name", _Customer.customer_name);
                cmd.Parameters.AddWithValue("@email_id", _Customer.email_id);
                cmd.Parameters.AddWithValue("@password", _Customer.password);
                cmd.Parameters.AddWithValue("@contact_number", _Customer.contact_number);
                SqlParameter retPram1 = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, 50);
                retPram1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram1);
                con.Open();
                cmd.ExecuteNonQuery();
                customer_id = Guid.Parse(retPram1.Value.ToString());
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public int get_customer_login(customer CustomerLogin)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_get_customer_login", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@email_id", CustomerLogin.email_id);
                cmd.Parameters.AddWithValue("@password", CustomerLogin.password);
                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                cmd.ExecuteNonQuery();
                int resultValue = Convert.ToInt32(retPram.Value);
                con.Close();
                return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet check_customer_loginid(string email_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@email_id", email_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_check_customer_loginid", parameters);
            return ds;
        }

        public DataSet check_guest_loginid(string email_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@email_id", email_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_check_guest_loginid", parameters);
            return ds;
        }

        public bool delete_customer_address(Int64 customer_details_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@customer_details_id", customer_details_id)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_customer_address", parameters);
            return resultValue > 0 ? true : false;
        }

        public bool insert_customer_details(customer Customer)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@customer_id", Customer.customer_id),
                new SqlParameter("@fullName", Customer.full_name),
                new SqlParameter("@emailId", Customer.email_id),
                new SqlParameter("@contactNumber", Customer.contact_number),
                new SqlParameter("@address", Customer.address),
                new SqlParameter("@landMark", Customer.land_mark),
                new SqlParameter("@city", Customer.city),
                new SqlParameter("@state", Customer.state),
                new SqlParameter("@pinCode", Customer.pin_code)
            };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_customer_details", parameters);
            return resultValue > 0 ? true : false;
        }


        public long change_user_password(customer _change)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_change_user_password", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@email_id", _change.email_id);
                cmd.Parameters.AddWithValue("@OldPassword", _change.password);
                cmd.Parameters.AddWithValue("@NewPassword", _change.newpassword);
                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                cmd.ExecuteNonQuery();
                long resultValue = Convert.ToInt32(retPram.Value);
                cmd.Parameters.Clear();
                con.Close();
                return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_customer_address(Guid customer_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@customer_id", customer_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_customer_address", parameters);
            return ds;
        }

        public DataSet get_customer_login_details(string email_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@email_id", email_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_customer_login_details", parameters);
            return ds;
        }

        public bool insert_update_guest_customer(BusinessEntities.customer GuestCustomer, ref Guid customer_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
              {
               new SqlParameter("@customer_id", GuestCustomer.customer_id),
                        new SqlParameter("@email_id",GuestCustomer.email_id)
              };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_guest_customer", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool insert_update_facebook_customer(BusinessEntities.customer GuestCustomer, ref Guid customer_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
              {
               new SqlParameter("@customer_id", GuestCustomer.customer_id),
                        new SqlParameter("@email_id",GuestCustomer.email_id),
                        new SqlParameter("@customer_name",GuestCustomer.customer_name)
              };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_facebook_customer", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool insert_update_google_customer(BusinessEntities.customer GuestCustomer, ref Guid customer_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
              {
               new SqlParameter("@customer_id", GuestCustomer.customer_id),
                        new SqlParameter("@email_id",GuestCustomer.email_id),
                        new SqlParameter("@customer_name",GuestCustomer.customer_name)
              };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_update_google_customer", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int check_customer_emailid(customer _ForgetPassword)
        {
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_check_customer_emailid", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@email_id", _ForgetPassword.email_id);
                SqlParameter retPram = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retPram.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retPram);
                con.Open();
                cmd.ExecuteNonQuery();
                int resultValue = Convert.ToInt32(retPram.Value);
                con.Close();
                return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool update_guest_customer(BusinessEntities.customer GuestCustomer, ref Guid customer_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                  {
                        new SqlParameter("@customer_id", customer_id),
                        new SqlParameter("@customer_name",GuestCustomer.customer_name),
                        new SqlParameter("@email_id",GuestCustomer.email_id),
                        new SqlParameter("@contact_number",GuestCustomer.contact_number)
                  };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_guest_customer", parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet get_customer_detail_all(long product_id)

        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                //new SqlParameter("@from_date", FromDate),
                //new SqlParameter("@to_date", ToDate),
                //new SqlParameter("@order_status", (order_status == null ? DBNull.Value : (object)order_status)),
                //new SqlParameter("@email_id", (email_id == null ? DBNull.Value : (object)email_id)),
                 new SqlParameter("@product_id", @product_id)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_customer_detail_all", parameters);
            return ds;
        }

        //Start: Added by Hetal Patel for XpressLane Users

        public DataSet check_isexistxpresslanecustomer(string emailID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@email_id", @emailID)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "check_isexistxpresslanecustomer", parameters);
            return ds;
        }

        public bool insert_update_Xprsslane_customer(Guid customer_id, string email_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
              {
               new SqlParameter("@customer_id", customer_id),
                        new SqlParameter("@email_id", email_id)
              };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "insert_update_Xprsslane_customer", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //End: Added by Hetal Patel for XpressLane Users

    }
}
