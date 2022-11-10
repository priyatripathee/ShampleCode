using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BusinessEntities;
using System.Data;
using System.Data.SqlClient;

namespace strutt
{
    public partial class wishlist : System.Web.UI.Page
    {
        string Email = "";
        decimal maxprice = 0;
        long ProductId = 0;
        bool stock = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CustomerLoginDetails"] != null)
                {
                    Email = Session["CustomerLoginDetails"].ToString();
                    BindWishlist(Email);
                }
                else
                {
                    Response.Redirect("~/login.aspx");
                }
            }
        }

        private void BindWishlist(string Email)
        {
            wishlist_handler wishlistHandler = new wishlist_handler();
            DataSet ds = wishlistHandler.get_wishlist_recommendation(0, Email, 2);
            if (ds != null && ds.Tables.Count > 0)
            {

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ViewState["ProductDetails"] = dt;

                    //pnlWishlist.Visible = true;
                    //divCartContinue.Visible = false;
                    lblTotal.Text = dt.Rows.Count.ToString();
                    rptWishList.DataSource = dt;
                    rptWishList.DataBind();
                }
                else
                {
                    lblMessage.Text = "No Item Found.";
                }
            }
            else
            {
                rptWishList.DataSource = null;
                rptWishList.DataBind();
                lblMessage.Text = "No Item Found.";
            }

           //lblMessage.Text = "Your item Added Sucessefully.";
      
        }

        protected void rptLatestProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            bool stock = false;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnAddToCart = (Button)e.Item.FindControl("btnAddToCart");
                Label lblStock = (Label)e.Item.FindControl("lblStock");
                HiddenField instock = (HiddenField)e.Item.FindControl("hfieldLatestProduct");
                stock = Convert.ToBoolean(instock.Value);

                if (stock == true)
                {
                    lblStock.Visible = false;
                    btnAddToCart.Visible = true;
                }
                if (stock == false)
                {
                    btnAddToCart.Visible = false;
                    lblStock.Visible = true;
                    lblStock.Text = "Out of Stock";
                }
            }
        }

        protected void rptWishList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                ProductId = Convert.ToInt64(e.CommandArgument.ToString());

                wishlist_handler wishlistHandler = new wishlist_handler();
                bool delete = wishlistHandler.DeleteWishlist(ProductId);
                if (delete)
                {
                    if (Session["CustomerLoginDetails"] != null)
                    {
                        Email = Session["CustomerLoginDetails"].ToString();
                        BindWishlist(Email);
                        lblMessage.Text = "Your item Remove Sucessefully.";
                    }
                }
            }
            if (e.CommandName == "addtocard")
            {
                ProductId = Convert.ToInt64(e.CommandArgument.ToString());

                bool result = this.AddToShoppingCart();
                if (result)
                {
                    //Response.Redirect("../../../view-cart");
                    if (Request.QueryString["cproid"] != null)
                    {
                        Response.Redirect("~/cart.aspx");
                    }
                    else
                    {
                        //Response.Redirect("../../../view-cart");
                        Response.Redirect("~/cart.aspx");
                        lblMessage.Text = "Your item Added Sucessefully.";
                    }
                  
                }

            }
            bool stock = false;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnAddToCart = (Button)e.Item.FindControl("btnAddToCart");
                Label lblStock = (Label)e.Item.FindControl("lblStock");
                HiddenField instock = (HiddenField)e.Item.FindControl("hfieldLatestProduct");
                stock = Convert.ToBoolean(instock.Value);

                if (stock == true)
                {
                    lblStock.Visible = false;
                    btnAddToCart.Visible = true;
                }
                if (stock == false)
                {
                    btnAddToCart.Visible = false;
                    lblStock.Visible = true;
                    lblStock.Text = "Out of Stock";
                }
            }
        }

        private bool AddToShoppingCart()
        {
            Boolean blnMatch = false;
            DataTable dtCart = (DataTable)Session["Cart"];
            foreach (DataRow row in dtCart.Rows)
            {
                if (int.Parse(row["product_id"].ToString()) == ProductId)
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
                    // DataTable dt = (DataTable)ViewState["ProductDetails"];
                    DataRow item = ((DataTable)ViewState["ProductDetails"]).Select("product_id=" + ProductId).FirstOrDefault();

                    drCart["product_id"] = ProductId;
                    drCart["product_name"] = item["product_name"].ToString();
                    if (Session["procust"] != null)
                    {
                        drCart["thumb_image"] = "images/OrderImages/" + Session["procust"].ToString();
                    }
                    else
                    {
                        drCart["thumb_image"] = "images/Product/Thumb/" + item["thumb_image"].ToString();
                    }
                    drCart["menu_name"] = item["menu_name"].ToString();
                    drCart["sub_menu_name"] = item["sub_menu_name"].ToString();
                    drCart["child_name"] = item["child_name"].ToString();
                    drCart["weight"] = item["weight"].ToString();
                    if (drCart["gendertype"] != DBNull.Value)
                    {
                        drCart["gendertype"] = item["gendertype"].ToString();
                    }
                    drCart["size"] = item["size"].ToString();

                    if (Session["size"] != null)
                    {
                        drCart["size"] = Session["size"].ToString();
                    }

                    drCart["color_name"] = item["color_name"].ToString();
                    Decimal price = Convert.ToDecimal(item["Price"].ToString());
                    Decimal discount = Convert.ToDecimal(item["discount"].ToString());
                    Decimal TotalPrice = discount > 0 ? price - (price * discount / 100) : price;
                    drCart["sale_price"] = price;
                    drCart["discount"] = discount;
                    drCart["coupon_discount"] = 0;
                    drCart["custom_bag_price"] = 0;
                    drCart["shipping_price"] = 0;
                    drCart["quantity"] = 1;
                    drCart["Total"] = Convert.ToInt32(1) * TotalPrice;
                    dtCart.Rows.Add(drCart);
                }
                Session["Cart"] = dtCart;
               
            }
            return true;
        }
    }
}