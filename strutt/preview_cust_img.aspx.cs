using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace strutt
{
    public partial class preview_cust_img : System.Web.UI.Page
    {
        long product_id = 0;
        long ordDetails_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["orddetid"] != null)
                {
                    //product_id = Convert.ToInt64(Request.QueryString["proid"].ToString());
                    ordDetails_id = Convert.ToInt64(Request.QueryString["orddetid"].ToString());
                    this.BindCustDetails(ordDetails_id);
                }
            }
        }
        private void BindCustDetails(long CustDtlId)
        {
            order_handler orderHandler = new order_handler();
            DataSet dsOrder = orderHandler.get_preview_order_custom(CustDtlId);

            if (dsOrder != null && dsOrder.Tables.Count > 0)
            {
                DataTable dt = dsOrder.Tables[0];
                if (dt.Rows[0]["custom_bag_param"] != DBNull.Value)
                {
                    imgCustom.ImageUrl = dt.Rows[0]["thumb_image"].ToString();
                    lblProductName.Text = dt.Rows[0]["product_name"].ToString();
                    lblCustDetails.Text = dt.Rows[0]["custom_bag_param"].ToString();
                    lblQty.Text = dt.Rows[0]["quantity"].ToString();
                    lblCustPrice.Text = dt.Rows[0]["custom_bag_price"].ToString();
                    lblSalePrice.Text = dt.Rows[0]["sale_price"].ToString();
                    lblTotalPrice.Text = dt.Rows[0]["total_price"].ToString();

                }
            }
        }
    }
}