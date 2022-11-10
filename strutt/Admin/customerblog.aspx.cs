using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using System.Data;

namespace strutt.Admin
{
   
    public partial class customerblog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");
            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindCustomerReview(); 
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
        private void BindCustomerReview()
        {
            customerreview_handler customerHandler = new customerreview_handler();
            //Guid customer_id = new Guid(Session["customerDetailsId"].ToString());
            DataSet ds = customerHandler.get_customerreviw(null,null,null);
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
                lblMsg.Text = "Deleted Successfully.";
                this.BindCustomerReview();
            }
        }
        protected void grdcustomerReview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "id", "tbl_customer_review");
                this.BindCustomerReview();
                grdcustomerReview.Focus();
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "id", "tbl_customer_review");
                this.BindCustomerReview();
                grdcustomerReview.Focus();
            }
        }
    }
}