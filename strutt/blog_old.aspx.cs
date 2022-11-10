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

namespace strutt
{
    public partial class blog : System.Web.UI.Page
    {
        long BlogId = 0;
        int Flag = 4;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["blgId"] != null)
                {
                    BlogId = Convert.ToInt64(Request.QueryString["blgId"].ToString());
                    Flag = 1;
                    BindBlog(BlogId);
                }
                else
                {
                    Flag = 4;
                    BindBlog(BlogId);
                }
                BindAllBlog();
            }
        }

        private void BindBlog(long BlogId)
        {
            blog_handler blogHandler = new blog_handler();
            DataSet ds = blogHandler.get_bog(BlogId);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                rptBlog.DataSource = dt;
                rptBlog.DataBind();
            }
        }

        private void BindAllBlog()
        {
            blog_handler blogHandler = new blog_handler();
            DataSet ds = blogHandler.get_bog(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                rptAllLeftBlog.DataSource = dt;
                rptAllLeftBlog.DataBind();
            }
        }

    }
}