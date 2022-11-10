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
using System.Text.RegularExpressions;
namespace strutt.account
{
    public partial class addreview : System.Web.UI.Page
    {
        Int32 custReviewId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session["customerDetailsId"]!= null)
                {
                    BindCustomerReview();
                }
                else
                {
                    Response.Redirect("~/login.aspx");
                }
                lblMessage.Visible = true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
       
        private void BindCustomerReview()
        {

            customerreview_handler customerHandler = new customerreview_handler();
            Guid customer_id = new Guid(Session["customerDetailsId"].ToString());
            DataSet ds = customerHandler.get_customerreviw(null, customer_id, true);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
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
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            //string strPath = "", strFileName = "", strFullPath = "", strLogo = "";
            //string LargeNoImage = "noImage.jpg";
            //string returnMessage = string.Empty;
            string fileName = "noImage.jpg";

           string shorttext = DAL.Utility.HtmlToPlainText(txtDescription.Text);
            if (shorttext.Length > 99)
                shorttext = shorttext.Substring(0, 99);
           
            if (Upload_Blog.HasFile)
            {
                string strbannerUploadTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                string ext = System.IO.Path.GetExtension(Upload_Blog.FileName);
                fileName = strbannerUploadTime + ext;

                Upload_Blog.SaveAs(Server.MapPath("~/images/Review/") + fileName);
            }
            

            if (ViewState["custReviewId"] != null)
            {
                custReviewId = Convert.ToInt32(ViewState["custReviewId"].ToString());
            }

            // Guid? myGuidVar = null;
            int? countView = 0;
            Guid customer_id = new Guid(Session["customerDetailsId"].ToString());
            customerreview_handler customerHandler = new customerreview_handler();
            int result = customerHandler.insert_update_customerreview(custReviewId, customer_id, fileName, txtTitle.Text, countView, txtDescription.Text, shorttext,
              true);
            if (result == -1)
            {
                //lblMessage.ForeColor = System.Drawing.Color.Red;
                //lblMsg.Text = "Sorry, " + txtTitle.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {
                if (ViewState["custReviewId"] != null)
                {
                    //lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Blog update successfully.";
                    lblMessage.Visible = true;
                   // lblMessage.Text = txtTitle.Text + " " + DAL.helper_data.getMessage("Updated Successfully");
                }
                else
                {
                   // lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Blog save successfully.";
                    lblMessage.Visible = true;
                   // lblMessage.Text = txtTitle.Text + " " + DAL.helper_data.getMessage("Saved Successfully");
                }
                this.BindCustomerReview();
            }

            //lblMessage.Text = returnMessage;
            ViewState["custReviewId"] = null;
            lbtnSubmit.Text = "Submit";
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            lblLargeImg.Text = string.Empty;
            imgBlog.ImageUrl = "images/noImage.jpg";
            Upload_Blog.Dispose();
        }

        protected void grdcustomerReview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string returnMessage = string.Empty;
            string imageName = string.Empty;

            Int32 custReviewId = Convert.ToInt32(grdcustomerReview.DataKeys[e.RowIndex].Values["id"].ToString());
            string bannerName = grdcustomerReview.DataKeys[e.RowIndex].Values["title"].ToString();
            customerreview_handler customerHandler = new customerreview_handler();
            bool delete = customerHandler.delete_customerreview(custReviewId, ref imageName, ref returnMessage);
            if (delete)
            {
                string imagepath = Server.MapPath("~//images/Review//" + imageName);
                FileInfo file = new FileInfo(imagepath);
                if (file.Exists)
                {
                    file.Delete();
                }

                this.BindCustomerReview();
            }
        }
        protected void grdcustomerReview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                Int32 custReviewId = Convert.ToInt32(e.CommandArgument);
                customerreview_handler customerHandler = new customerreview_handler();
                DataSet ds = customerHandler.get_customerreviw(custReviewId, null, null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTitle.Focus();
                        ViewState["custReviewId"] = custReviewId;
                        txtTitle.Text = dt.Rows[0]["title"].ToString();
                        lblLargeImg.Text = dt.Rows[0]["image_name"].ToString();
                        txtDescription.Text = dt.Rows[0]["description"].ToString();
                        // chkIsApporved.Checked = Convert.ToBoolean(dt.Rows[0]["is_active"]);
                        imgBlog.ImageUrl = "~/images/Review/" + dt.Rows[0]["image_name"].ToString();
                        lbtnSubmit.Text = "Update";
                    }
                }
            }
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
    }
}