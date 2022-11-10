using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BusinessEntities;
using System.Data;

namespace strutt
{
    public partial class resetpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null || Request.QueryString["key"] == null)
            {
                lblMsg.Text = "Invalid user. Please contact us.";
                return;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UpdatePassword();
        }
        private bool UpdatePassword()
        {
            //string customerId;
            customer_handler customerHandler = new customer_handler();
            customer Customer = new customer();
            BusinessEntities.customer customerData = new BusinessEntities.customer();
            customerData.email_id = Request.QueryString["id"];
            customerData.password = security.Encryptdata(txtPassword.Text);

            //customerId = scty.Decryptdata(Request.QueryString["key"]);
            int retFlag = customerHandler.get_customer_login(customerData);

            if (retFlag == 0 || retFlag > 0)
            {
                string strnewpassword = security.Encryptdata(txtPassword.Text);
                string strConpassword = security.Encryptdata(txtConfirmPassword.Text);
                Customer.email_id = Request.QueryString["id"];
                Customer.customer_id = Guid.Parse(Request.QueryString["key"]);
                Customer.password = txtPassword.Text;
                Customer.newpassword = strnewpassword;
                Customer.password = strConpassword;
                long changeData = customerHandler.change_user_password(Customer);
                if (changeData > 0)
                {
                    lblMsg.Text = "Your password has been changed.";
                    lblMsg.CssClass = "text-green";
                }
            }
            else
            {
                lblMsg.Text = "Your are not registerd";
            }
            return true;
        }
        
    }
}