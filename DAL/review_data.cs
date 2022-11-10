using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class review_data
    {
        public bool insert_reviews(BusinessEntities.review _review)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
		        {
                    new SqlParameter("@product_id", _review.product_id),
                    new SqlParameter("@user_name", _review.user_name),
                    new SqlParameter("@rating", _review.rating),
                    new SqlParameter("@title", _review.title),
                    new SqlParameter("@reviews", _review.reviews)
		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_insert_reviews", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update_reviews(BusinessEntities.review _review)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
		        {
                    new SqlParameter("@review_id", _review.review_id),
                    new SqlParameter("@user_name", _review.user_name),
                    new SqlParameter("@rating", _review.rating),
                    new SqlParameter("@title", _review.title),
                    new SqlParameter("@reviews", _review.reviews)
		        };

                int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_update_reviews", parameters);
                return resultValue > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet get_product_review(Int64 review_id, Int32 Flag)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@review_id", review_id),
                new SqlParameter("@Flag", Flag)
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_product_review", parameters);
            return ds;
        }

        public bool delete_product_review(Int64 review_id)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {
			    new SqlParameter("@review_id", review_id)
		    };

            int resultValue = SqlHelper.ExecuteNonQuery(Connection.ConnstruttDB, "pr_delete_product_review", parameters);
            return resultValue > 0 ? true : false;
        }
    }
}
