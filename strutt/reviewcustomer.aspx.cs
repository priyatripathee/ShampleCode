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
    public partial class reviewcustomer : System.Web.UI.Page
    {
        int customerreviewId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (Request.QueryString["id"] != null)
            {
                customerreviewId = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
            GetCustomerReview();
            GetCommnet();
            GetTopReview();
        }
        private void GetTopReview()
        {
            customerreview_handler customerreviewHandler = new customerreview_handler();
            DataSet ds = customerreviewHandler.get_customer_review_Top5(null, null, true);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rptTop5.DataSource = dt;
                    rptTop5.DataBind();
                }
                else
                {
                    rptTop5.DataSource = null;
                    rptTop5.DataBind();
                }
            }
        }



        private void GetCustomerReview()
        {
            customerreview_handler customerreviewHandler = new customerreview_handler();
            DataSet ds = customerreviewHandler.get_customerreviwone(customerreviewId);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbltitle.Text = dt.Rows[0]["title"].ToString();
                    lblBName.Text = dt.Rows[0]["title"].ToString();
                    lblImage.ImageUrl = "~/images/Review/" + dt.Rows[0]["image_name"].ToString();
                    //lblCreateDate.Text = dt.Rows[0]["createddate"].ToString("dd-MMM-yyyy");
                    lblCreateDate.Text = dt.Rows[0]["createddate"].ToString();
                    lblCountView.Text = dt.Rows[0]["count_view"].ToString();
                    lblDescription.Text = dt.Rows[0]["description"].ToString();
                    litTotalComment.Text = dt.Rows[0]["count_view"].ToString();
                    lblCustomer.Text = dt.Rows[0]["customer_name"].ToString();
                    hdreviewId.Value = dt.Rows[0]["id"].ToString();
                }

                // Get commnet
                //customerreviewHandler.get_customerComment(customerreviewId);
               
            }
        }
        private void GetCommnet()
        {
            customerreview_handler customerreviewHandler = new customerreview_handler();
            DataSet ds = customerreviewHandler.get_customercomment(null,customerreviewId);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rpt_customerreview.DataSource = dt;
                    rpt_customerreview.DataBind();
                }
                else
                {
                    rpt_customerreview.DataSource = null;
                    rpt_customerreview.DataBind();
                }
            }

            }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (Session["customerDetailsId"] == null)
            {
                lblMsg.Text = "Please login, before add commnet.";
                review.Visible = true;
                return;
            }
            Guid customer_id = new Guid(Session["customerDetailsId"].ToString());

            customerreview_handler customerreviewHandler = new customerreview_handler();
            int result = customerreviewHandler.insert_update_customerComment(customerreviewId, customer_id, txtComment.Text,false);
            lblMg.Text = "Save Successfully.";
            btnSubmit.Text = "Submit";
            txtComment.Text = string.Empty;
            GetCommnet();
        }
    }
}
