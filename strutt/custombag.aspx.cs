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
    public partial class custombag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["proid"] != null)
                {
                    long product_id = Convert.ToInt64(Request.QueryString["proid"].ToString());
                    this.BindProduct(product_id);

                    this.BindCustomBug(278969);

                    if (Request.QueryString["orddetid"] != null)
                    {
                        long order_detail_id = Convert.ToInt64(Request.QueryString["orddetid"].ToString());
                        this.BindCustomBug(order_detail_id);
                    }
                }
                //else if (Request.QueryString["orddetid"] != null)
                //{
                    
                //}
            }
        }

        protected void btnAddtoCart_Click(object sender, EventArgs e)
        {
            string val;
            long product_id = Convert.ToInt64(Request.QueryString["proid"].ToString());

            val = txtLetter.Text;
            val = hfStyle.Value;
            val = hfColor.Value;
            UpdateCart(product_id);
            Response.Redirect("~/cart.aspx");
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

        private bool UpdateCart(long product_id)
        {
            Boolean blnMatch = false;
            DataTable dtCart = (DataTable)Session["Cart"];

            foreach (DataRow row in dtCart.Rows)
            {
                if (int.Parse(row["product_id"].ToString()) == product_id)
                {
                    blnMatch = true;
                    break;
                }
            }
            if (!blnMatch)
            {
                AddToShoppingCart(product_id);
            }

            // Update Custom bag Param and Price
            string customTagVal = "";
            float xpoint = 430, ypoint = 435;
            foreach (DataRow row in dtCart.Rows)
            {
                if (int.Parse(row["product_id"].ToString()) == product_id)
                {
                    customTagVal = "Letter:" + txtLetter.Text + "," + "Style:" + hfStyle.Value + "," + "Color:" + hfColor.Value;

                    if (!string.IsNullOrEmpty(hfXPoint.Value))
                        xpoint = Convert.ToSingle(hfXPoint.Value);
                    if (!string.IsNullOrEmpty(hfYPoint.Value))
                        ypoint = Convert.ToSingle(hfYPoint.Value);

                    row["custom_bag_param"] = customTagVal;
                    row["custom_bag_price"] = Convert.ToSingle(System.Configuration.ConfigurationManager.AppSettings["custombagcharge"]);
                    
                    row["x_point"] = xpoint;
                    row["y_point"] = ypoint;
                    Session["Cart"] = dtCart;
                    break;
                }
            }
            return true;
        }


        private bool AddToShoppingCart(long product_id)
        {
            Boolean blnMatch = false;
            DataTable dtCart = (DataTable)Session["Cart"];
            foreach (DataRow row in dtCart.Rows)
            {
                if (long.Parse(row["product_id"].ToString()) == product_id)
                {
                    row["quantity"] = (int)row["quantity"] + 1;

                    Decimal price = Convert.ToDecimal(row["sale_price"]);
                    Decimal discount = Convert.ToDecimal(row["discount"]);
                    Decimal UnitPriceOnDiscount = discount > 0 ? price - (price * discount / 100) : price;

                    row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
                    Session["Cart"] = dtCart;
                    blnMatch = true;
                    break;
                }
            }
            if (!blnMatch)
            {
                if (ViewState["ProductDetails"] != null)
                {
                    DataRow drCart = dtCart.NewRow();
                    DataTable dt = (DataTable)ViewState["ProductDetails"];
                    drCart["product_id"] = product_id;
                    drCart["product_name"] = dt.Rows[0]["product_name"].ToString();
                    drCart["thumb_image"] = "images/Product/Thumb/" + dt.Rows[0]["thumb_image"].ToString();
                    drCart["menu_name"] = dt.Rows[0]["menu_name"].ToString();
                    drCart["sub_menu_name"] = dt.Rows[0]["sub_menu_name"].ToString();
                    drCart["child_name"] = dt.Rows[0]["child_name"].ToString();
                    drCart["weight"] = dt.Rows[0]["weight"].ToString();
                    drCart["gendertype"] = dt.Rows[0]["gendertype"];

                    drCart["size"] = dt.Rows[0]["size"].ToString();

                    if (Session["size"] != null)
                    {
                        drCart["size"] = Session["size"].ToString();
                    }

                    drCart["color_name"] = dt.Rows[0]["color_name"].ToString();
                    Decimal price = Convert.ToDecimal(dt.Rows[0]["Price"].ToString());
                    Decimal discount = Convert.ToDecimal(dt.Rows[0]["discount"].ToString());
                    Decimal TotalPrice = discount > 0 ? price - (price * discount / 100) : price;

                    Session["sale_discount"] = String.Format("{0:0.00}", (price - TotalPrice));

                    drCart["sale_price"] = price;
                    drCart["discount"] = discount;
                    drCart["coupon_discount"] = 0;
                    drCart["custom_bag_price"] = 0;
                    drCart["quantity"] = 1;
                    drCart["Total"] = Convert.ToInt32(1) * TotalPrice;
                    dtCart.Rows.Add(drCart);
                }
                Session["Cart"] = dtCart;
            }
            return true;
        }

        private void BindCustomBug(long order_detail_id)
        {
            //order_handler orderHandler = new order_handler();
            //DataSet dsOrder = orderHandler.get_order_customtag(order_detail_id);
            //string customBagParam = "";

            //if (dsOrder != null && dsOrder.Tables.Count > 0)
            //{
            //    DataTable dt = dsOrder.Tables[0];
            //    if (dt.Rows[0]["custom_bag_param"] != DBNull.Value)
            //    {
            //        customBagParam = dt.Rows[0]["custom_bag_param"].ToString();

            //        txtLetter.Text = customBagParam.Split(',').GetValue(0).ToString().Remove(0, 7);
            //        hfStyle.Value = customBagParam.Split(',').GetValue(1).ToString().Remove(0, 6);
            //        hfColor.Value = customBagParam.Split(',').GetValue(2).ToString().Remove(0, 6);

            //        hfXPoint.Value = dt.Rows[0]["x_point"].ToString();
            //        hfYPoint.Value = dt.Rows[0]["y_point"].ToString();
            //    }
            //}
        }
    }
}