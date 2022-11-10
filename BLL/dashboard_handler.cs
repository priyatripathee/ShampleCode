    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BusinessEntities;
using System.Data;

namespace BLL
{
    public class dashboard_handler
    {
        private dashboad_data Dashboard_Handler { get; set; }

        public DataTable get_monthly_ordertotal(DateTime CurrentDate)
        {
            Dashboard_Handler = new dashboad_data();
            return Dashboard_Handler.get_monthly_ordertotal(CurrentDate); 
        }

        public DataTable get_categorywise_total(DateTime? FromDate,DateTime? ToDate)
        {
            Dashboard_Handler = new dashboad_data();
            return Dashboard_Handler.get_categorywise_total (FromDate,ToDate );
        }
        
        public DataTable GetChartData()
        {
            Dashboard_Handler = new dashboad_data();
            return Dashboard_Handler.GetChartData();
        }

        public DataSet get_orderFeedbackCOmment()
        {
            Dashboard_Handler = new dashboad_data();
            return Dashboard_Handler.getOrderFeedbackComment();
        }

    }
}
