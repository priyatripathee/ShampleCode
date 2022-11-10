using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BusinessEntities;
using System.Data;
using System.Data.SqlClient;


namespace strutt.Admin
{
    public partial class pendingorder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["fdate"] != null)
                    txtfromdate.Text = Request.QueryString["fdate"];
                else
                    txtfromdate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");

                if (Request.QueryString["tdate"] != null)
                    txtfromdate.Text = Request.QueryString["tdate"];
                else
                    txttodate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");

                if (Request.QueryString["status"] != null)
                    ddlEmailSent.SelectedValue = Request.QueryString["status"];
                else
                    ddlEmailSent.SelectedIndex = 0;

                txttodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtfromdate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.bindtempOrderStatus();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }


        private void bindtempOrderStatus()
        {
            DateTime? Fromdate = null;
            DateTime? Todate = null;
            Boolean? Emailsent = null;


            if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
            {
                Fromdate = Convert.ToDateTime(txtfromdate.Text);
                Todate = Convert.ToDateTime(txttodate.Text + " 23:59:59");
            }
            if (!string.IsNullOrEmpty(ddlEmailSent.SelectedValue))
                Emailsent = Convert.ToBoolean(Convert.ToInt16(ddlEmailSent.SelectedValue));

            temp_cart_handler tempcartHandler = new temp_cart_handler();
            DataSet ds = new DataSet();
            ds = tempcartHandler.get_temp_customer(Fromdate, Todate, Emailsent);

            if (ds != null && ds.Tables.Count > 0)
            {

                DataTable dt = ds.Tables[0];
                lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                rpttemp.DataSource = dt;
                rpttemp.DataBind();
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "data not founds this date. Please search other date.";
                rpttemp.DataSource = null;
                rpttemp.DataBind();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindtempOrderStatus();

        }

        protected void btnSentEmail_Click(object sender, EventArgs e)
        {
            SendEmailadmin();
        }

        private void SendEmailadmin()
        {
            temp_cart_handler tempcartHandler = new temp_cart_handler();
            DataSet ds = new DataSet();
            ds = tempcartHandler.get_temp_customer(Convert.ToDateTime(txtfromdate.Text), Convert.ToDateTime(txttodate.Text + " 23:59:59"), false);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DAL.Utility.SendPendingOrder(dt.Rows[i]["email_id"].ToString());
                }
            }
        }
    }

}



   
