using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
   public class customerreview_handler
    {

        private customerreview_data customerReviewData { get; set; }
        public customerreview_handler()
        {
            customerReviewData = new customerreview_data();
        }
        public DataSet get_customerreviw(Int32? id, Guid? customer_id,bool? is_active)
        {
            return customerReviewData.get_customerreviw(id,customer_id,is_active);
        }
        public DataSet get_customer_review_Top5(Int32? id, Guid? customer_id, bool? is_active)
        {
            return customerReviewData.get_customer_review_Top5(id, customer_id, is_active);
        }
        public bool delete_customerreview(Int32 id ,ref string imageName, ref string returnMessage)
        {
            return customerReviewData.delete_customerreview(id, ref imageName, ref returnMessage);
        }

        public DataSet get_customerreviwone(object customerreviewId)
        {
            throw new NotImplementedException();
        }

        public DataSet get_customerreviwone(Int32 id)
        {
            return customerReviewData.get_customerreviwone(id);
        }
        public DataSet get_customercomment(Int32? id, Int32 customer_review_id)
        {
            return customerReviewData.get_customercomment(id, customer_review_id);
        }
        public Int32 insert_update_customerComment(Int32 customer_review_id,  Guid customer_id, string comment, bool is_active)
        {
            return customerReviewData.insert_update_customerComment(customer_review_id, customer_id, comment, is_active);
        }

        public Int32 insert_update_customerreview(int id,Guid? customer_id, string image_name, string title, Int32? count_view, string description,string description_short, bool is_active)
        {
            return customerReviewData.insert_update_customerreview(id,customer_id, image_name, title, count_view, description, description_short, is_active);
        }
    }
}
