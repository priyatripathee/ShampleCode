using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using DAL;


namespace strutt
{
    public partial class blogdetail : System.Web.UI.Page
    {
        int blogId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] != null)
            {
                blogId = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
            GetBlog();
            GetBlogCommnet();
        }
        private void GetBlog()
        {
            blog_handler BlogHandler = new blog_handler();
            DataSet ds = BlogHandler.get_bog(blogId);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbltitle.Text = dt.Rows[0]["title"].ToString();
                    lblImage.ImageUrl = "~/images/BlogImages/" + dt.Rows[0]["image"].ToString();
                    lblCreateDate.Text = dt.Rows[0]["created_date"].ToString();
                    //lblCountView.Text = dt.Rows[0]["total_review"].ToString();
                    lblDescription.Text = dt.Rows[0]["description"].ToString();
                    //litTotalComment.Text = dt.Rows[0]["total_review"].ToString();
                    lblCustomer.Text = dt.Rows[0]["name"].ToString();
                    hdBlogId.Value = dt.Rows[0]["blog_id"].ToString();
                }
                // Get commnet
               // customerreviewHandler.get_customerComment(customerreviewId);
            }
        }
        //private void GetCommnet()
        //{
        //    blog_handler BlogHandler = new blog_handler();
        //    DataSet ds = BlogHandler.get_bog(blogId);

        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            rpt_Blog.DataSource = dt;
        //            rpt_Blog.DataBind();
        //        }
        //        else
        //        {
        //            rpt_Blog.DataSource = null;
        //            rpt_Blog.DataBind();
        //        }
        //    }
        //}

        private void GetBlogCommnet()
        {
            blog_handler BlogHandler = new blog_handler();
            DataSet ds = BlogHandler.get_customerblogcomment(null, blogId);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rpt_BlogComment.DataSource = dt;
                    rpt_BlogComment.DataBind();
                }
                else
                {
                    rpt_BlogComment.DataSource = null;
                    rpt_BlogComment.DataBind();
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["customerDetailsId"] == null)
            {
                lblMsg.Text = "Please login, before add commnet.";
                comment.Visible = true;
                return;
            }
            Guid customer_id = new Guid(Session["customerDetailsId"].ToString());

            blog_handler BlogHandler = new blog_handler();
            int result = BlogHandler.insert_update_blogComment(blogId, customer_id, txtComment.Text, false);
            lblMg.Text = "Save Successfully.";
            btnSubmit.Text = "Submit";
            txtComment.Text = string.Empty;
            GetBlogCommnet();
        }
    }
}