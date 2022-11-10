using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class leave_feedback_data
    {
        public DataSet get_leavefeedback(Int64? LeaveFeedbackId, DateTime? FromDate, DateTime? ToDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                //new SqlParameter("@LeaveFeedbackId", LeaveFeedbackId),
                new SqlParameter("@LeaveFeedbackId", (LeaveFeedbackId == null ? DBNull.Value : (object)LeaveFeedbackId)),
                new SqlParameter("@from_date", (FromDate == null ? DBNull.Value : (object)FromDate)),
                new SqlParameter("@to_date", (ToDate == null ? DBNull.Value : (object)ToDate)),
            };
            DataSet ds = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "pr_get_leavefeedback", parameters);
            return ds;
        }
    }
}
