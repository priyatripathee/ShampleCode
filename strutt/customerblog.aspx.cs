using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using BusinessEntities;
using System.Data;
namespace strutt
{
    public partial class customerblog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCustomerReview();
            }
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
    }
}