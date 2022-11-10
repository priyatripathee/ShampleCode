using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using BusinessEntities;
namespace strutt
{
    public partial class forgotpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GetUserAndSendLink();
        }

        public string GetUserAndSendLink()
        {
            string customerId, customerName;
            customer_handler customerHandler = new customer_handler();

            DataSet ds = customerHandler.get_customer_login_details(txtEmail.Text.Trim());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                customerId = ds.Tables[0].Rows[0]["customer_id"].ToString();
                customerName = ds.Tables[0].Rows[0]["customer_name"].ToString();
                
                if (string.IsNullOrEmpty(customerName))
                    customerName = txtEmail.Text.Trim();
                DAL.Utility.SendForgotPasswordMail(customerName,
                    string.Format(System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/resetpassword.aspx?id={0}&key={1}",txtEmail.Text.Trim(), customerId), txtEmail.Text);
                lblLoginMsg.Text = "Forgot password link sent to your registered email. Please verify your email.";
                lblLoginMsg.CssClass = "text-green";
                lblLoginMsg.Visible = true;
            }
            else
            {
                lblLoginMsg.Text = "Invalid Email / Username.";
                lblLoginMsg.CssClass = "text-red";
                txtEmail.Focus();
            }
            return "";
        }
    }
}