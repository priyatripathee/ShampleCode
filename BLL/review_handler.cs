using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class review_handler
    {
        private review_data reviewData { get; set; }
        public review_handler()
        {
            reviewData = new review_data();
        }

        public bool insert_reviews(BusinessEntities.review _review)
        {
            return reviewData.insert_reviews(_review);
        }

        public bool update_reviews(BusinessEntities.review _review)
        {
            return reviewData.update_reviews(_review);
        }
        public DataSet get_product_review(Int64 review_id, Int32 Flag)
        {
            return reviewData.get_product_review(review_id, Flag);
        }
        public bool delete_product_review(Int64 review_id)
        {
            return reviewData.delete_product_review(review_id);
        }
    }
}
