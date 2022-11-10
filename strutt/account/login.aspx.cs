using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BLL;
using System.Data;

namespace strutt.account
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlAdminRole.Focus();
                //BindUserRoles();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            HiddenField hfimg = new HiddenField();
            admin_data_handler adminDataHandler = new admin_data_handler();
            //string strpassword = security.Encryptdata(txtPassword.Text);
            string strpassword = txtPassword.Text;
            BusinessEntities.admin admin = new BusinessEntities.admin();
            admin.user_name = txtUserId.Text.Trim();
            admin.password = strpassword;
            admin.userType = Convert.ToInt32(ddlAdminRole.SelectedItem.Value);
            
            if (Page.IsValid)
            {
                DataTable userRow = adminDataHandler.get_adminlogin(admin);
                if (userRow == null || userRow.Rows.Count == 0)
                {
                    lblMsg.Text = "Invalid UserId/Password";
                    txtPassword.Focus();
                }
                else
                {
                    Session["AdminUserID"] = txtUserId.Text.Trim();
                    //Session["Password"] = txtPassword.Text.Trim();
                    if (userRow.Rows[0]["profile_image"] != DBNull.Value)
                        Session["ProfileImage"] = userRow.Rows[0]["profile_image"].ToString();
                    else
                        Session["ProfileImage"] = "noimage.jpg";

                    Session["Role"] = ddlAdminRole.SelectedItem.Text;
                    //string role = Session["Role"].ToString();
                    //if ("Developer" == role)
                    //{
                    //    Session["Developer"] = "Developer";
                    //}
                    //else
                    //{
                    //    Session["Developer"] = null;
                    //}

                    Response.Redirect("../Admin/dashboard.aspx");
                }
            }
        }

        private void BindUserRoles()
        {
            //ddlAdminRole.Items.Add("Select Role");
            ////get enum items to get the respective enum value 
            //string[] enumNames = Enum.GetNames(typeof(user_role));
            //foreach (string item in enumNames)
            //{
            //    //get the enum item value
            //    int value = (int)Enum.Parse(typeof(user_role), item);
            //    ListItem listItem = new ListItem(item, value.ToString());
            //    ddlAdminRole.Items.Add(listItem);
            //}
        }
    }
}