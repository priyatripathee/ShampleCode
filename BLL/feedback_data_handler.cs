using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class feedback_data_handler
    {
        private feedback_data feedbackData { get; set; }
        public feedback_data_handler()
        {
            feedbackData = new feedback_data();
        }

        public int insert_feedback(Guid customer_id, byte rating, string category, string suggestion)
        {
            return feedbackData.insert_feedback(customer_id, rating, category, suggestion);
        }
        public DataSet get_feedback(DateTime? FromDate, DateTime? ToDate)
        {
            return feedbackData.get_feedback(FromDate, ToDate);
        }
    }
}
