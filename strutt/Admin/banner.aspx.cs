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
    public partial class banner : System.Web.UI.Page
    {
        Int32 bannerId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindBanner();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindBanner()
        {
            
            banner_handler bannerHandler = new banner_handler();
            DataSet ds = bannerHandler.get_banner(null,null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    grdBanner.DataSource = dt;
                    grdBanner.DataBind();
                }
                else
                {
                    grdBanner.DataSource = null;
                    grdBanner.DataBind();
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

            
            if (ViewState["BannerId"] != null)
            {
                bannerId = Convert.ToInt32(ViewState["BannerId"].ToString());
            }

            banner_handler bannerHandler = new banner_handler();
            banner Banner = new banner();
            BusinessEntities.banner bnnerData = new BusinessEntities.banner();



            bnnerData.banner_id = bannerId;
            bnnerData.title = txtTitle.Text;
            bnnerData.image = LargeNoImage;
            bnnerData.url_path = txtURL.Text;
            bnnerData.type = Convert.ToByte(ddltype.SelectedValue);
            bnnerData.order_by = Convert.ToByte(txtOrderby.Text);
            bool result = bannerHandler.insert_update_banner(bnnerData, ref returnMessage);

            if (result)
            {

                if (ViewState["BannerId"] != null)
                {
                    lblMsg.Text = "Updated Successfully.";
                    lblMsg.Text = returnMessage;
                }
                else
                {
                    lblMsg.Text = "Saved Successfully.";
                    lblMsg.Text = returnMessage;
                }
                this.BindBanner();
            }
            lblMsg.Text = returnMessage;
            ViewState["BannerId"] = null;
            btnSubmit.Text = "Submit";
            txtTitle.Text = string.Empty;
            txtURL.Text = string.Empty;
            ddltype.SelectedValue = string.Empty;
            lblLargeImg.Text = string.Empty;
            txtOrderby.Text = string.Empty;
            imgLarge.ImageUrl = "images/noImage.jpg";
            Upload_LargeImages.Dispose();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["BannerId"] = null;
            btnSubmit.Text = "Submit";
            txtTitle.Text = string.Empty;
            txtURL.Text = string.Empty;
            lblLargeImg.Text = string.Empty;
            ddltype.SelectedValue = string.Empty;
            txtOrderby.Text = string.Empty;
            imgLarge.ImageUrl = "~/images/noImage.jpg";
            Upload_LargeImages.Dispose();
            this.BindBanner();
        }

        protected void grdBanner_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string returnMessage = string.Empty;
            string imageName = string.Empty;

            Int64 bannerId = Convert.ToInt32(grdBanner.DataKeys[e.RowIndex].Values["banner_id"].ToString());
            string bannerName = grdBanner.DataKeys[e.RowIndex].Values["title"].ToString();
            banner_handler bannerHandler = new banner_handler();
            bool delete = bannerHandler.delete_banner(bannerId, ref imageName, ref returnMessage);
            if (delete)
            {
                string imagepath = Server.MapPath("~//images/Banner//" + imageName);
                FileInfo file = new FileInfo(imagepath);
                if (file.Exists)
                {
                    file.Delete();
                }

                this.BindBanner();
            }

            lblMsg.Text = returnMessage;
        }

        protected void grdBanner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            object lType = DBNull.Value;
            if (e.CommandName == "EditRecored")
            {
                Int32 bannerId = Convert.ToInt32(e.CommandArgument);
                banner_handler bannerHandler = new banner_handler();
                DataSet ds = bannerHandler.get_banner(bannerId,null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTitle.Focus();
                        ViewState["BannerId"] = bannerId;
                        txtTitle.Text = dt.Rows[0]["title"].ToString();
                        lblLargeImg.Text = dt.Rows[0]["image"].ToString();
                        imgLarge.ImageUrl = "~/images/Banner/" + dt.Rows[0]["image"].ToString();
                        txtURL.Text = dt.Rows[0]["url_path"].ToString();
                        if (!ddltype.SelectedValue.Equals("0"))
                            lType = Convert.ToByte(ddltype.SelectedValue);
                        if (dt.Rows[0]["type"] != DBNull.Value)
                        {
                            ddltype.SelectedValue = dt.Rows[0]["type"].ToString();
                        }
                        ddltype.SelectedValue = dt.Rows[0]["type"].ToString();
                        txtOrderby.Text = dt.Rows[0]["order_by"].ToString();
                        btnSubmit.Text = "Update";
                    }   
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "banner_id", "tbl_banner");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "banner_id", "tbl_banner");
            }
            this.BindBanner();
        }

        protected void grdBanner_RowDataBound(object sender, GridViewRowEventArgs e)
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