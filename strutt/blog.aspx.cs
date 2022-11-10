using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;


namespace strutt
{
    public partial class blog1 : System.Web.UI.Page
    {
        string Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetBlog();
            }
        }
        private void GetBlog()
        {
            blog_handler BlogHandler = new blog_handler();
            DataSet ds = BlogHandler.get_bog(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rpt_blog.DataSource = dt;
                    rpt_blog.DataBind();
                }
                else
                {
                    rpt_blog.DataSource = null;
                    rpt_blog.DataBind();
                }
            }
        }
        //protected void btnBlog_Click(object sender, EventArgs e)
        //{
        //    if (Session["CustomerLoginDetails"] != null)
        //    {
        //        Email = Session["CustomerLoginDetails"].ToString();
        //    }
        //    else
        //    {
        //        Response.Redirect("~/login.aspx");
        //    }
        //    Response.Redirect("~/account/addblog.aspx");
        //}
    }
}