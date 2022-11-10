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
using DAL;

namespace strutt.Admin
{
    public partial class addviewlookbook : System.Web.UI.Page
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
                this.BindLookbook();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindLookbook()
        {
            tools_handler toolsHandler = new tools_handler();
            DataSet ds = toolsHandler.get_lookbook(0, 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    gvLookbook.DataSource = dt;
                    gvLookbook.DataBind();
                }
                else
                {
                    gvLookbook.DataSource = null;
                    gvLookbook.DataBind();
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
                Upload_LargeImages.SaveAs(Server.MapPath("~/images/LookbookImages/") + fileName + "_" + strbannerUploadTime + ext);
                LargeNoImage = fileName + "_" + strbannerUploadTime + ext;
            }
            else
            {
                LargeNoImage = lblLargeImg.Text.ToString();
            }


            if (ViewState["blogId"] != null)
            {
                blogId = Convert.ToInt32(ViewState["blogId"].ToString());
                if (ViewState["imgName"] != null && !string.IsNullOrEmpty(ViewState["imgName"].ToString()))
                {
                    string imagepath = Server.MapPath("~//images/LookbookImages//" + ViewState["imgName"].ToString());
                    FileInfo file = new FileInfo(imagepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
            }

            tools_handler toolsHandler = new tools_handler();
            int result = toolsHandler.insert_update_lookbook(blogId, txtDescription.Text, LargeNoImage);
            if (result > 0)
            {
                if (ViewState["blogId"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Lookbook " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Lookbook " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.BindLookbook();
            }
            lblMsg.Text = returnMessage;
            ViewState["blogId"] = null;
            btnSubmit.Text = "Submit";
            txtDescription.Text = string.Empty;
            lblLargeImg.Text = string.Empty;
            imgLarge.ImageUrl = "images/noImage.jpg";
            Upload_LargeImages.Dispose();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["blogId"] = null;
            btnSubmit.Text = "Submit";
            txtDescription.Text = string.Empty;
            lblLargeImg.Text = string.Empty;
            imgLarge.ImageUrl = "~/images/noImage.jpg";
            Upload_LargeImages.Dispose();
            this.BindLookbook();
        }

        protected void gvLookbook_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 lid = Convert.ToInt32(gvLookbook.DataKeys[e.RowIndex].Values["lookbookId"].ToString());
            tools_handler toolsHandler = new tools_handler();
            bool delete = toolsHandler.delete_lookbook(lid);
            if (delete)
            {
                this.BindLookbook();
                lblMsg.Text = "Lookbook " + helper_data.getMessage("msgDeleteSuccessfully");
                gvLookbook.Focus();
            }
        }

        protected void gvLookbook_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        ViewState["blogId"] = blogId;
                        txtDescription.Text = dt.Rows[0]["description"].ToString();
                        lblLargeImg.Text = dt.Rows[0]["image"].ToString();
                        imgLarge.ImageUrl = "~/images/LookbookImages/" + dt.Rows[0]["image"].ToString();
                        btnSubmit.Text = "Update";

                        ViewState["imgName"] = dt.Rows[0]["image"].ToString();
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "lookbookId", "tbl_lookbook");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = DAL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "lookbookId", "tbl_lookbook");
            }
            this.BindLookbook();
        }

        protected void gvLookbook_RowDataBound(object sender, GridViewRowEventArgs e)
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