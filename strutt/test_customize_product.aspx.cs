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
    public partial class test_customize_product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["proid"] != null)
                {
                    long product_id = Convert.ToInt64(Request.QueryString["proid"].ToString());
                    this.BindProduct(product_id);  
                }

                //image.Attributes.Add("style", "background: url('images/Product/Large/L_3.jpg') no-repeat center center");
            }
        }
        private void BindProduct(long product_id)
        {
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_product_details(product_id);

            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];
                ViewState["ProductDetails"] = dsProducts.Tables[0];

                lblProductName.Text = dt.Rows[0]["product_name"].ToString();
                lblSalePrice.Text = dt.Rows[0]["sale_price"].ToString();
                image.Attributes.Add("style", "background: url('images/Product/Large/" + dt.Rows[0]["thumb_image"].ToString() + "') no-repeat center center");
            }
        }
    }
}