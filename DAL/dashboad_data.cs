using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class dashboad_data
    {
        public DataTable get_monthly_ordertotal(DateTime CurrentDate)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@CurrentDate", CurrentDate )
                };
                DataTable table = SqlHelper.ExecuteParamerizedSelectCommand(Connection.ConnstruttDB, "dashboard_count", CommandType.StoredProcedure, parameters);
                return table;
            }
            catch
            {
                return null;
            }
        }

        public DataTable get_categorywise_total(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@from_date", FromDate),
                    new SqlParameter("@to_date", ToDate),
                };
                DataTable table = SqlHelper.ExecuteParamerizedSelectCommand(Connection.ConnstruttDB, "dashboard_category_count", CommandType.StoredProcedure,parameters );
                return table;
            
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetChartData()
        {
            try
            {
                DataTable table = SqlHelper.ExecuteSelectCommand(Connection.ConnstruttDB, "dashboard_monthly_count",CommandType.StoredProcedure );
                return table;
            }
            catch
            {
                return null;
            }
        }

        public DataSet getOrderFeedbackComment()
        {
            try
            {
                DataSet table = SqlHelper.ExecuteDataset(Connection.ConnstruttDB, "dashboard_OrderFeedbackComment");
                return table ;
            }
            catch
            {
                return null;
            }
        }
        

    }
}
