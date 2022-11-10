using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class receiver_address_data
    {
        public bool insert_customer_address(receiver_address receiverAddress)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
		        {
			        new SqlParameter("@customer_id", receiverAddress.customer_details_id),
                    new SqlParameter("@CustomerDetailID", receiverAddress.customer_id),
                    new SqlParameter("@full_name", receiverAddress.full_name),
                    new SqlParameter("@contact_number", receiverAddress.contact_number),
                    new SqlParameter("@email_id", receiverAddress.email_id),
                    new SqlParameter("@address", receiverAddress.address),
                    new SqlParameter("@land_mark", receiverAddress.land_mark),
                    new SqlParameter("@city", receiverAddress.city),
                    new SqlParameter("@state", receiverAddress.state),
                    new SqlParameter("@pin_code", receiverAddress.pin_code),
                    new SqlParameter("@message", receiverAddress.message)
		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_customer_address", parameters);
                return resultValue > 0 ? true : false;
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

        public DataSet delete_save_customer_address(long customer_details_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@customer_details_id", customer_details_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_delete_save_customer_address", parameters);
            return ds;
        }

        public DataSet get_save_customer_address(long customer_details_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@customer_details_id", customer_details_id)
		    };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_save_customer_address", parameters);
            return ds;
        }
    }
}
