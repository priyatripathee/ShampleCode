using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class leave_feedback
    {
        public Int64 LeaveFeedbackId { get; set; }
        public Int64 OrderId { get; set; }
        public string EmailId { get; set; }
        public Int32 Rating { get; set; }
        public bool ItemArrived { get; set; }
        public bool ItemDescribed { get; set; }
        public bool DepartureOnTime { get; set; }
        public string Comment { get; set; }
        public DateTime created_date { get; set; }
    }
}
