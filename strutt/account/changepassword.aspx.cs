using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using BLL;
using BusinessEntities;
using DAL;

namespace strutt.account
{
    public partial class changepassword : System.Web.UI.Page
    {
        string Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CustomerLoginDetails"] != null)
                {
                    Email = Session["CustomerLoginDetails"].ToString();
                }
                else
                {
                    Response.Redirect("~/login.aspx");
                }

                //if (Session["CustomerLoginDetails"] != null)
                //{
                //}
                //else
                //{
                //    Response.Redirect("http://localhost:4349/default.aspx", false);
                //}
            }
        }

        #region ---------- customer change password--------------

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            customer_handler customerHandler = new customer_handler();
            customer Customer = new customer();

            BusinessEntities.customer customerData = new BusinessEntities.customer();
            customerData.email_id = Session["CustomerLoginDetails"].ToString();
            customerData.password = security.Encryptdata(txtoldpassword.Text);
            int retFlag = customerHandler.get_customer_login(customerData);

            if (retFlag == 0)
            {
                string strnewpassword = security.Encryptdata(txtnewpassword.Text);
                Customer.email_id = Session["CustomerLoginDetails"].ToString();
                Customer.password = txtoldpassword.Text;
                Customer.newpassword = strnewpassword;
                Customer.password = strnewpassword;
                long changeData = customerHandler.change_user_password(Customer);
                if (changeData > 0)
                {
                    lblMsg.Text = "Your password has been changed.";
                }
            }
            else
            {
                lblMsg.Text = "The current password is incorrect !";
            }
        }
        #endregion
    }
}