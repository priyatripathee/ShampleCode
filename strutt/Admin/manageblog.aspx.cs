using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using BusinessEntities;
using System.IO;

namespace strutt.Admin
{
    public partial class manageblog : System.Web.UI.Page
    {
        Int32 blogId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindBlog();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindBlog()
        {
            blog_handler blogHandler = new blog_handler();
            DataSet ds = blogHandler.get_bog(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvBlog.DataSource = dt;
                    gvBlog.DataBind();
                }
                else
                {
                    gvBlog.DataSource = null;
                    gvBlog.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string LargeNoImage = "noImage.jpg";
            string returnMessage = string.Empty;
            string strbannerUploadTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            if (Upload_LargeImages.HasFile)
            {
                string fileName = Path.GetFileNameWithoutExtension(Upload_LargeImages.FileName);
                string ext = System.IO.Path.GetExtension(Upload_LargeImages.FileName);
                Upload_LargeImages.SaveAs(Server.MapPath("~/images/BlogImages/") + fileName + "_" + strbannerUploadTime + ext);
                LargeNoImage = fileName + "_" + strbannerUploadTime + ext;
            }
            else
            {
                LargeNoImage = lblLargeImg.Text.ToString();
            }


            if (ViewState["blogId"] != null)
            {
                blogId = Convert.ToInt32(ViewState["blogId"].ToString());
                if(ViewState["imgName"] != null && !string.IsNullOrEmpty(ViewState["imgName"].ToString()))
                {
                    string imagepath = Server.MapPath("~//images/BlogImages//" + ViewState["imgName"].ToString());
                    FileInfo file = new FileInfo(imagepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
            }

            blog_handler blogHandler = new blog_handler();
            blog Blog = new blog();
            BusinessEntities.blog blogData = new BusinessEntities.blog();

            blogData.blog_id = blogId;
            blogData.title = txtTitle.Text;
            blogData.name = txtName.Text;
            blogData.description = txtDescription.Text;
            blogData.image = LargeNoImage;

            bool result = blogHandler.insert_update_blog(blogData, ref returnMessage);
            if (result)
            {
                if (ViewState["blogId"] != null)
                {
                    //lblMsg.Text = "Updated Successfully.";
                    lblMsg.Text = returnMessage;
                }
                else
                {
                    //lblMsg.Text = "Saved Successfully.";
                    lblMsg.Text = returnMessage;
                }
                this.BindBlog();
            }
            lblMsg.Text = returnMessage;
            ViewState["blogId"] = null;
            btnSubmit.Text = "Submit";
            txtTitle.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            lblLargeImg.Text = string.Empty;
            imgLarge.ImageUrl = "images/noImage.jpg";
            Upload_LargeImages.Dispose();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["blogId"] = null;
            btnSubmit.Text = "Submit";
            txtTitle.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            lblLargeImg.Text = string.Empty;
            imgLarge.ImageUrl = "~/images/noImage.jpg";
            Upload_LargeImages.Dispose();
            this.BindBlog();
        }

        protected void gvBlog_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string returnMessage = string.Empty;
            string imageName = string.Empty;

            Int64 blogId = Convert.ToInt32(gvBlog.DataKeys[e.RowIndex].Values["blog_id"].ToString());
            string bannerName = gvBlog.DataKeys[e.RowIndex].Values["title"].ToString();
            blog_handler blogHandler = new blog_handler();
            bool delete = blogHandler.delete_blog(blogId, ref imageName, ref returnMessage);
            if (delete)
            {
                string imagepath = Server.MapPath("~//images/BlogImages//" + imageName);
                FileInfo file = new FileInfo(imagepath);
                if (file.Exists)
                {
                    file.Delete();
                }

                this.BindBlog();
            }

            lblMsg.Text = returnMessage;
        }

        protected void gvBlog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                Int32 blogId = Convert.ToInt32(e.CommandArgument);
                blog_handler blogHandler = new blog_handler();
                DataSet ds = blogHandler.get_bog(blogId);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTitle.Focus();
                        ViewState["blogId"] = blogId;
                        txtTitle.Text = dt.Rows[0]["title"].ToString();
                        txtName.Text = dt.Rows[0]["name"].ToString();
                        txtDescription.Text = dt.Rows[0]["description"].ToString();
                        lblLargeImg.Text = dt.Rows[0]["image"].ToString();
                        imgLarge.ImageUrl = "~/images/BlogImages/" + dt.Rows[0]["image"].ToString();
                        btnSubmit.Text = "Update";

                        ViewState["imgName"] = dt.Rows[0]["image"].ToString();
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "blog_id", "tbl_blog");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "blog_id", "tbl_blog");
            }
            this.BindBlog();
        }

        protected void gvBlog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton lnk = (ImageButton)e.Row.FindControl("imgBtnActive");
                Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                if (lblstatus.Text == "False")
                {
                    lnk.CommandName = "Active";
                }
                if (lblstatus.Text == "True")
                {
                    lnk.CommandName = "Deactive";
                }
            }
        }
    }
}