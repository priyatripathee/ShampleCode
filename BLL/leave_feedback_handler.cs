using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;


namespace BLL
{
   public class leave_feedback_handler
    {
        private leave_feedback_data leavefeedbackData { get; set; }

        public leave_feedback_handler()
        {
            leavefeedbackData = new leave_feedback_data();
        }
        public DataSet get_leavefeedback(Int64? LeaveFeedbackId,DateTime? FromDate, DateTime? ToDate)
        {
            return leavefeedbackData.get_leavefeedback(LeaveFeedbackId,FromDate, ToDate);
        }
    }
}
