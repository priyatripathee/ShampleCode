using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;

namespace strutt
{
    public partial class review : System.Web.UI.Page
    {
        string Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetCustomerReview();
            }
        }
        private void GetCustomerReview()
        {
            customerreview_handler customerreviewHandler = new customerreview_handler();
            DataSet ds = customerreviewHandler.get_customerreviw(null, null, true);
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

        protected void btnreview_Click(object sender, EventArgs e)
        {
            //if (Session["CustomerLoginDetails"] != null)
            //{
            //    Email = Session["CustomerLoginDetails"].ToString();
            //}
            //else
            //{
            //    Response.Redirect("~/login.aspx?url=account/addblog.aspx");
            //}

            Response.Redirect("~/account/addreview.aspx");
        }
    }
}