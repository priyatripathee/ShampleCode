using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BLL;
using System.Data;
using DAL;

namespace strutt.Admin
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserID"] == null)
                    Response.Redirect("../account/Login.aspx");

                if (Session["AdminUserID"] != null)
                {
                    Email = Session["AdminUserID"].ToString();
                }
                else
                {
                    // Response.Redirect("~/login.aspx");
                }
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }


            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            admin_data_handler adminHandler = new admin_data_handler();
            admin Admin = new admin();

            BusinessEntities.admin adminData = new BusinessEntities.admin();
            //adminData.user_name = Session["AdminUserID"].ToString();
            // adminData.password = scty.Encryptdata(txtoldpassword.Text);
            // int retFlag = adminHandler.get_adminlogin(Admin);

            //if (retFlag == 0)
            //{
            Admin.user_name = Session["AdminUserID"].ToString();
            string encryptData = security.Encryptdata(txtoldpassword.Text);
            Admin.password = encryptData;

            encryptData = security.Encryptdata(txtnewpassword.Text);
            Admin.newpassword = encryptData;

            long changeData = adminHandler.change_admin_password(Admin);
            if (changeData == 1)
                lblMsg.Text = "Your password has been changed successfully.";
            else
            {
                lblMsg.Text = "Invalid username or password.!";
                lblMsg.CssClass = "red";
            }
        }
    }
}