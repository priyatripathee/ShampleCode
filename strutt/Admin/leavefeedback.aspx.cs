using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using BusinessEntities;
using System.IO;
using DAL;

namespace strutt.Admin
{
    public partial class leavefeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
                fillMonthRange();
               // txttodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
               // txtfromdate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            }
            this.Bindleavefeedback();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Bindleavefeedback();

        }
        private void Bindleavefeedback()
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
                Fromdate = Convert.ToDateTime(dateRange.Split('|').GetValue(0));
                Todate = Convert.ToDateTime(dateRange.Split('|').GetValue(1) + " 23:59:59");
            }
            leave_feedback_handler leavefeedbackHandler = new leave_feedback_handler();
            DataSet ds = leavefeedbackHandler.get_leavefeedback(null, Fromdate, Todate);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    grdleavefeedback.DataSource = dt;
                    grdleavefeedback.DataBind();
                }
                else
                {
                    grdleavefeedback.DataSource = null;
                    grdleavefeedback.DataBind();
                }
            }
        }

        private void fillMonthRange()
        {
            DateTime firstDate = DateTime.Today.AddDays((-1 * (int)(DateTime.Today.DayOfWeek)) + 1);
            DateTime secondDate = DateTime.Now;

            ddlDateRange.Items.Add(new ListItem("Current Week (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));


            secondDate = DateTime.Today.AddDays((-1 * (int)(DateTime.Today.DayOfWeek)));
            firstDate = secondDate.AddDays(-7);
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

            ListItem lst = new ListItem("Custom Data Range", "0");
            ddlDateRange.Items.Insert(ddlDateRange.Items.Count - 0, lst);

        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateRange.SelectedValue == "0")
            {
                pnlCustomDate.Visible = true;
                txtfromdate.Visible = true;
                txttodate.Visible = true;
            }

            else
            {
                pnlCustomDate.Visible = false;
            }
        }
    }
}
