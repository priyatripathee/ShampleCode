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
    public partial class admin_main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                
                if (Session["AdminUserID"] != null)
                {
                    lblAdminName.Text = string.Format("Welcome, {0}", Session["AdminUserID"].ToString());
                    //string Role = Session["Role"].ToString();
                    lblname.Text = Session["AdminUserID"].ToString();
                    img1.ImageUrl = "~/Admin/images/" + Session["ProfileImage"];
                    Image1.ImageUrl = "~/Admin/images/" + Session["ProfileImage"];
                        //if (Session["Developer"] != null)
                        //{
                        //    //if (Role == user_role.Oprator.ToString())
                        //    //{
                        //    //  //  divPermission.Visible = true;
                        //    //}
                        //    //else
                        //    //{
                        //    //    //divPermission.Visible = false;
                        //    //}
                        //}
                        //else
                        //{
                        //    //string MyUrl = HttpContext.Current.Request.Url.AbsolutePath;

                        //    string lastPart = "";
                        //    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                        //    lastPart = url.Split('/').Last().Replace("-", " ");
                        //    if (lastPart == "superadmin.aspx")
                        //    {
                        //        Response.Redirect("../account/Login.aspx");
                        //    }
                        //    else if (lastPart == "allmenuname.aspx")
                        //    {
                        //        Response.Redirect("../account/Login.aspx");
                        //    }
                        //    else if (lastPart == "permission.aspx")
                        //    {
                        //        Response.Redirect("../account/Login.aspx");
                        //    }
                        //    else if (lastPart == "role.aspx")
                        //    {
                        //        Response.Redirect("../account/Login.aspx");
                        //    }
                        //}
                    }
                else
                {
                    Response.Redirect("../account/Login.aspx");
                }
            }
        }
        protected void lbtnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../account/Login.aspx");
        }
    }
}