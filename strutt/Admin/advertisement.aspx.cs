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
    public partial class advertisement : System.Web.UI.Page
    {
        Int32 category_link_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindAdvertisement();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindAdvertisement()
        {

            banner_handler bannerHandler = new banner_handler();
            DataSet ds = bannerHandler.get_category_link(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    grdAdvertisement.DataSource = dt;
                    grdAdvertisement.DataBind();
                }
                else
                {
                    grdAdvertisement.DataSource = null;
                    grdAdvertisement.DataBind();
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
                Upload_LargeImages.SaveAs(Server.MapPath("~/images/Banner/") + fileName + "_" + strbannerUploadTime + ext);
                LargeNoImage = fileName + "_" + strbannerUploadTime + ext;
            }
            else
            {
                LargeNoImage = lblLargeImg.Text.ToString();
            }


            if (ViewState["category_link_id"] != null)
            {
                category_link_id = Convert.ToInt32(ViewState["category_link_id"].ToString());
            }

            banner_handler bannerHandler = new banner_handler();
            banner Banner = new banner();
            BusinessEntities.banner AdsData = new BusinessEntities.banner();

            AdsData.category_link_id = category_link_id;
            AdsData.title = txtTitle.Text;
            AdsData.image = LargeNoImage;
            AdsData.category_link_url = txtURL.Text;
            AdsData.created_by = "admin";
            AdsData.updated_by = "admin";

            bool result = bannerHandler.insert_update_Category_link(AdsData, ref returnMessage);

            if (result)
            {

                if (ViewState["category_link_id"] != null)
                {
                    //lblMsg.Text = "Updated Successfully.";
                    lblMsg.Text = returnMessage;
                }
                else
                {
                    //lblMsg.Text = "Saved Successfully.";
                    lblMsg.Text = returnMessage;
                }
                this.BindAdvertisement();
            }
            lblMsg.Text = returnMessage;
            ViewState["category_link_id"] = null;
            btnSubmit.Text = "Submit";
            txtTitle.Text = string.Empty;
            txtURL.Text = string.Empty;
            lblLargeImg.Text = string.Empty;
            imgLarge.ImageUrl = "images/noImage.jpg";
            Upload_LargeImages.Dispose();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["category_link_id"] = null;
            btnSubmit.Text = "Submit";
            txtTitle.Text = string.Empty;
            txtURL.Text = string.Empty;
            lblLargeImg.Text = string.Empty;
            imgLarge.ImageUrl = "~/images/noImage.jpg";
            Upload_LargeImages.Dispose();
            this.BindAdvertisement();
        }

        protected void grdAdvertisement_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string returnMessage = string.Empty;
            string imageName = string.Empty;

            Int32 category_link_id = Convert.ToInt32(grdAdvertisement.DataKeys[e.RowIndex].Values["category_link_id"].ToString());
            string bannerName = grdAdvertisement.DataKeys[e.RowIndex].Values["title"].ToString();
            banner_handler bannerHandler = new banner_handler();
            bool delete = bannerHandler.delete_category_link(category_link_id, ref imageName, ref returnMessage);
            if (delete)
            {
                string imagepath = Server.MapPath("~//images/Banner//" + imageName);
                FileInfo file = new FileInfo(imagepath);
                if (file.Exists)
                {
                    file.Delete();
                }

                this.BindAdvertisement();
            }

            lblMsg.Text = returnMessage;
        }

        protected void grdAdvertisement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                Int32 category_link_id = Convert.ToInt32(e.CommandArgument);
                banner_handler bannerHandler = new banner_handler();
                DataSet ds = bannerHandler.get_category_link(category_link_id);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTitle.Focus();
                        ViewState["category_link_id"] = category_link_id;
                        txtTitle.Text = dt.Rows[0]["title"].ToString();
                        lblLargeImg.Text = dt.Rows[0]["image"].ToString();
                        imgLarge.ImageUrl = "~/images/Banner/" + dt.Rows[0]["image"].ToString();
                        txtURL.Text = dt.Rows[0]["category_link_url"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "category_link_id", "tbl_category_link");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "category_link_id", "tbl_category_link");
            }
            this.BindAdvertisement();
        }

        protected void grdAdvertisement_RowDataBound(object sender, GridViewRowEventArgs e)
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