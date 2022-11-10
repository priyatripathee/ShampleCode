using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;

namespace strutt.Admin
{
    public partial class feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindFeedback();
                txttodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtfromdate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
            
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindFeedback();
        }
        private void BindFeedback()
        {
            DateTime? Fromdate = null;
            DateTime? Todate = null;
            if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
            {
                Fromdate = Convert.ToDateTime(txtfromdate.Text);
                Todate = Convert.ToDateTime(txttodate.Text + " 23:59:59");
            }
            feedback_data_handler feedbackHandler = new feedback_data_handler();
            DataSet ds = feedbackHandler.get_feedback(Fromdate, Todate);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    grdFeedback.DataSource = dt;
                    grdFeedback.DataBind();
                }
                else
                {
                    grdFeedback.DataSource = null;
                    grdFeedback.DataBind();
                }
            }
        }
    }
}