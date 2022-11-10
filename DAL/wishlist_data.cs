using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BusinessEntities;

namespace DAL
{
    public class wishlist_data
    {
        public bool InsertWishList(wishlist wishlist)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
		        {
                    new SqlParameter("@wishlist_id", wishlist.wishlist_id),
                    new SqlParameter("@product_id", wishlist.product_id),
                    new SqlParameter("@email_id", wishlist.email_id)
		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_wishlist", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_wishlist_recommendation(Int64 wishlist_id, string email_id, Int32 Flag)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@wishlist_id", wishlist_id),
                new SqlParameter("@email_id", email_id),
                new SqlParameter("@Flag", Flag)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_wishlist_recommendation", parameters);
            return ds;
        }

        public bool DeleteWishlist(Int64 product_id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
		        {
                    new SqlParameter("@product_id", product_id)			       
		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_wishlist", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
