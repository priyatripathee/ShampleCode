using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using BusinessEntities;
using System.IO;
using DAL;
namespace strutt.Admin
{
    public partial class addcustomerreview : System.Web.UI.Page
    {
       Int32 custReviewId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindCustomerReview();
                if (Session["Role"].ToString() == "Admin" )
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }


        private void BindCustomerReview()
        {

            customerreview_handler customerHandler = new customerreview_handler();
            DataSet ds = customerHandler.get_customerreviw(null, null, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = dt.Rows.Count.ToString();
                    grdcustomerReview.DataSource = dt;
                    grdcustomerReview.DataBind();
                }
                else
                {
                    grdcustomerReview.DataSource = null;
                    grdcustomerReview.DataBind();
                }
            }
        }
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{

        //    string LargeNoImage = "noImage.jpg";
        //    string returnMessage = string.Empty;
        //    string strbannerUploadTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        //    if (Upload_LargeImages.HasFile)
        //    {
        //        string fileName = Path.GetFileNameWithoutExtension(Upload_LargeImages.FileName);
        //        string ext = System.IO.Path.GetExtension(Upload_LargeImages.FileName);
        //        Upload_LargeImages.SaveAs(Server.MapPath("~/images/Review/") + fileName + "_" + strbannerUploadTime + ext);
        //        LargeNoImage = fileName + "_" + strbannerUploadTime + ext;
        //    }
        //    else
        //    {
        //        LargeNoImage = lblLargeImg.Text.ToString();
        //    }


        //    if (ViewState["custReviewId"] != null)
        //    {
        //        custReviewId = Convert.ToInt32(ViewState["custReviewId"].ToString());
        //    }

        //    Guid? myGuidVar = null;
        //    int? countView = 0;

        //    customerreview_handler customerHandler = new customerreview_handler();
        //    int result = customerHandler.insert_update_customerreview(custReviewId,myGuidVar, LargeNoImage, txtTitle.Text, countView, txtDescription.Text,
        //      chkIsApporved.Checked);
        //    if (result == -1)
        //    {
        //        lblMsg.ForeColor = System.Drawing.Color.Red;
        //        lblMsg.Text = "Sorry, " + txtTitle.Text + " " + helper_data.getMessage("msgAlreadyExist");
        //        return;
        //    }
        //    if (result > 0)
        //    {
        //        if (ViewState["custReviewId"] != null)
        //        {
        //            lblMsg.ForeColor = System.Drawing.Color.Green;
        //           lblMsg.Text = txtTitle.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
        //        }
        //        else
        //        {
        //            lblMsg.ForeColor = System.Drawing.Color.Green;
        //           lblMsg.Text = txtTitle.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
        //        }
        //        this.BindCustomerReview();
        //    }
            
        //    lblMsg.Text = returnMessage;
        //    ViewState["custReviewId"] = null;
        //    btnSubmit.Text = "Submit";
        //    txtTitle.Text = string.Empty;
        //    txtDescription.Text = string.Empty;
        //    chkIsApporved.Checked = false;
        //    lblLargeImg.Text = string.Empty;
        //    imgLarge.ImageUrl = "images/noImage.jpg";
        //    Upload_LargeImages.Dispose();
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        //protected void grdcustomerReview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    string returnMessage = string.Empty;
        //    string imageName = string.Empty;

        //    Int32 custReviewId = Convert.ToInt32(grdcustomerReview.DataKeys[e.RowIndex].Values["id"].ToString());
        //    string bannerName = grdcustomerReview.DataKeys[e.RowIndex].Values["title"].ToString();
        //    customerreview_handler customerHandler = new customerreview_handler();
        //    bool delete = customerHandler.delete_customerreview(custReviewId, ref imageName, ref returnMessage);
        //    if (delete)
        //    {
        //        string imagepath = Server.MapPath("~//images/Review//" + imageName);
        //        FileInfo file = new FileInfo(imagepath);
        //        if (file.Exists)
        //        {
        //            file.Delete();
        //        }

        //        this.BindCustomerReview();
        //    }
        //}

        protected void grdcustomerReview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "EditRecored")
            //{
            //    Int32 custReviewId = Convert.ToInt32(e.CommandArgument);
            //    customerreview_handler customerHandler = new customerreview_handler();
            //    DataSet ds = customerHandler.get_customerreviw(custReviewId, null, true);
            //    if (ds != null && ds.Tables.Count > 0)
            //    {
            //        DataTable dt = ds.Tables[0];
            //        if (dt.Rows.Count > 0)
            //        {
            //            txtTitle.Focus();
            //            ViewState["custReviewId"] = custReviewId;
            //            txtTitle.Text = dt.Rows[0]["title"].ToString();
            //            lblLargeImg.Text = dt.Rows[0]["image_name"].ToString();
            //            txtDescription.Text = dt.Rows[0]["description"].ToString();
            //            chkIsApporved.Checked = Convert.ToBoolean(dt.Rows[0]["is_active"]);
            //            imgLarge.ImageUrl = "~/images/Review/" + dt.Rows[0]["image_name"].ToString();
            //            btnSubmit.Text = "Update";
            //        }
            //    }
            //}
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "id", "tbl_customer_review");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "id", "tbl_customer_review");
            }
            this.BindCustomerReview();
        }

        protected void grdcustomerReview_RowDataBound(object sender, GridViewRowEventArgs e)
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