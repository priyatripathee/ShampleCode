using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data.Sql;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
//using Newtonsoft.Json;



namespace strutt.Admin
{
    public partial class dashboard : System.Web.UI.Page
    {
        dashboard_handler d_handler = new dashboard_handler();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {

                lbl_uname.Text = Session["AdminUserID"].ToString();
                Label lbl_Role = new Label();
                lbl_Role.Text = Session["Role"].ToString();

                if (Session["AdminUserID"] != null)
                {
                    //if (Session["Oprator"] != null)
                    //{
                    //    if (Role == user_role.Oprator.ToString())
                    //    {
                    //        //  divPermission.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        //divPermission.Visible = false;
                    //    }
                    //}
                    
                            lbl_uname.Text = string.Format("{0}", Session["AdminUserID"].ToString());
                            img1.ImageUrl = "~/Admin/images/" + Session["ProfileImage"];
                            lbl_type.Text = string.Format("{0}", Session["Role"]).ToString();
                            Response.Write(Session["AdminUserID"]);
                            string Role = Session["Role"].ToString();

                            DataTable dt = new DataTable();
                            dt = d_handler.get_monthly_ordertotal(DateTime.Now);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                lbl_ord.Text = dt.Rows[0]["OrderLastMonthTotal"].ToString();
                                lbl_ord_weekly.Text = dt.Rows[0]["OrderLastWeekTotal"].ToString();
                                lbl_order_yesterday.Text = dt.Rows[0]["OrderLastDayTotal"].ToString();
                                lbl_ord_today.Text = dt.Rows[0]["OrderTodayTotal"].ToString();

                                lbl_monthly_value.Text = dt.Rows[0]["CurrentMonthTotal"].ToString();
                                lbl_weekly_value.Text = dt.Rows[0]["valueLastWeekTotal"].ToString();
                                lbl_yesterday_value.Text = dt.Rows[0]["ValueyesterdayTotal"].ToString();
                                lbl_today_value.Text = dt.Rows[0]["ValueTodayTotal"].ToString();

                                lbl_chart_amount.Text = dt.Rows[0]["ChartValue"].ToString();
                                lbl_chart_Quantity.Text = dt.Rows[0]["ChartQuantity"].ToString();

                                lbl_monthly_delivered.Text = dt.Rows[0]["MonthlyDelivered"].ToString();
                                lbl_total_delivered.Text = dt.Rows[0]["TotalDelivered"].ToString();
                                lbl_today_delivered.Text = dt.Rows[0]["TodayDelivered"].ToString();

                                lbl_total_rto.Text = dt.Rows[0]["TotalRTO"].ToString();
                                lbl_Monthly_rto.Text = dt.Rows[0]["MonthlyRTO"].ToString();
                                lbl_Today_rto.Text = dt.Rows[0]["TodayRTO"].ToString();

                                if (dt.Rows[0]["oldestConfirmed"] == DBNull.Value)
                                    lbl_oldest_confirmed.Text = "-";
                                else
                                    lbl_oldest_confirmed.Text = Convert.ToDateTime(dt.Rows[0]["oldestConfirmed"]).ToString("dd/MM/yyyy");

                                if (dt.Rows[0]["oldestDispach"] == DBNull.Value)
                                    lbl_oldestdispach.Text = "-";
                                else
                                    lbl_oldestdispach.Text = Convert.ToDateTime(dt.Rows[0]["oldestDispach"]).ToString("dd/MM/yyyy");

                                lbl_total_cancellation.Text = dt.Rows[0]["TotalCancellation"].ToString();
                                lbl_cancellation.Text = dt.Rows[0]["MonthlyCancellation"].ToString();
                                lbl_cancellation_today.Text = dt.Rows[0]["TodayCancellation"].ToString();

                                lbl_total_user.Text = dt.Rows[0]["TotalUser"].ToString();
                                lbl_totol_user.Text = dt.Rows[0]["NewUser"].ToString();

                                lbl_prepaid.Text = dt.Rows[0]["TotalPrepaidPayments"].ToString();
                                lbl_today_Prepaid.Text = dt.Rows[0]["TodayPrepaidPayments"].ToString();
                                lbl_COD.Text = dt.Rows[0]["TotalCODPayments"].ToString();
                                lbl_today_COD.Text = dt.Rows[0]["TodayCODPayments"].ToString();

                                lbl_lastmonth.Text = dt.Rows[0]["valueLastMonth"].ToString();
                                lbl_curentmonth.Text = dt.Rows[0]["ValueCurrentMonthTotal"].ToString(); 
                            }
                            DataSet ds = d_handler.get_orderFeedbackCOmment();

                            repRecentProduct.DataSource = ds.Tables[0];
                            repRecentProduct.DataBind();

                            repComment.DataSource = ds.Tables[1];
                            repComment.DataBind();

                            repfeedback.DataSource = ds.Tables[2];
                            repfeedback.DataBind();

                            GetChartData();
                            fillMonthRange();

                            //dt = d_handler.get_categorywise_total();
                            BindCategoryRep();
                            //rptCategoryTotal.DataSource = dt;
                            //rptCategoryTotal.DataBind();
                        }
                      
                        Session["currentMonth"] = lbl_curentmonth.Text;
                        Session["lastMonth"] = lbl_lastmonth.Text;
                    }
                }
        
        private void BindCategoryRep()
        {
            DateTime? Fromdate = null;
            DateTime? Todate = null;
            if (ddlDateRange.SelectedValue.Equals("0"))
            {
                if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                {
                    Fromdate = Convert.ToDateTime(txtfromdate.Text);
                    Todate = Convert.ToDateTime(txttodate.Text + " 23:59:59");
                }
            }
            else
            {
                string dateRange = ddlDateRange.SelectedValue;
                Fromdate = Convert.ToDateTime(dateRange.Split('|').GetValue(0).ToString());
                Todate = Convert.ToDateTime(dateRange.Split('|').GetValue(1) + " 23:59:59");
            }
            DataTable dt = new DataTable();
            dt = d_handler.get_categorywise_total(Fromdate,Todate);
            if (dt != null && dt.Rows.Count>0)
            {
                rptCategoryTotal.DataSource = dt;
                rptCategoryTotal.DataBind();
            }
              
        }
        
        private void fillMonthRange()
        {


            DateTime firstDate;
            DateTime secondDate;
            //ddlDateRange.Items.Add("select date range");

            firstDate = DateTime.Today;
            secondDate = DateTime.Now;
            ddlDateRange.Items.Add(new ListItem("Today (" + firstDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            firstDate = DateTime.Today.AddDays(-1);
            secondDate = DateTime.Today.AddDays(-1);
            ddlDateRange.Items.Add(new ListItem("Yesterday (" + firstDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            //for current week

            if (DateTime.Today.ToString("ddd") == "Sun")
            {
                firstDate = DateTime.Now;
                secondDate = firstDate.AddDays(6);
            }
            else
            {
                firstDate = DateTime.Today.AddDays((-1 *(int)(DateTime.Today.DayOfWeek)) +1);
                secondDate = firstDate.AddDays(6);
            }

            ddlDateRange.Items.Add(new ListItem("Current Week (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));


            secondDate = DateTime.Today.AddDays((-1 * (int)(DateTime.Today.DayOfWeek)));
            firstDate = secondDate.AddDays(-6);
            ddlDateRange.Items.Add(new ListItem("Last Week (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));


            firstDate = new DateTime(secondDate.Year, secondDate.Month, 1);
            secondDate = DateTime.Now;
            ddlDateRange.Items.Add(new ListItem("Current Month (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            firstDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1);
            secondDate = new DateTime(firstDate.Year, firstDate.Month, DateTime.DaysInMonth(firstDate.Year, firstDate.Month));
            ddlDateRange.Items.Add(new ListItem("Last Month (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            firstDate = DateTime.Now + TimeSpan.FromDays(-30);
            secondDate = DateTime.Now;
            ddlDateRange.Items.Add(new ListItem("Last 30 Days(" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            //ListItem lst = new ListItem("Custom Data Range", "0");
            //ddlDateRange.Items.Insert(ddlDateRange.Items.Count - 0, lst);

        }
        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindCategoryRep();
        }
        [WebMethod]
        public static string  GetChartData()
        {
            dashboard_handler d_handler = new dashboard_handler();

            DataTable dt = new DataTable();
            dt = d_handler.GetChartData();
            {
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;
            }
        }

        protected void repRecentProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)

            {
                Label lbl_status = e.Item.FindControl("lbl_status") as Label;

                if (lbl_status.Text == "Confirmed")
                {
                    lbl_status.CssClass = "label label-green";
                }

                else if (lbl_status.Text == "Scheduled")
                {
                    lbl_status.CssClass = "label label-info";
                }
                else if (lbl_status.Text == "Cancelled")
                {
                    lbl_status.CssClass = "label label-warning";
                }
                else if (lbl_status.Text == "Failed")
                {
                    lbl_status.CssClass = "label label-danger";
                }
                else 
                {
                    lbl_status.Text = "-------";
                    lbl_status.CssClass = "label label-success";
                }

            }
        }
    }
}