using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BLL;
namespace strutt
{
    public class Common
    {
        /// <summary>
        /// Add to cart direct
        /// </summary>
        /// <param name="product_id">PK of product table</param>
        /// <returns>0: Success, 1: Unexpected Error, 2: out of stock</returns>
        public static short AddToShoppingCart(long product_id)
        {
            Boolean blnMatch = false;
            try
            {

                DataTable dtCart = (DataTable)HttpContext.Current.Session["Cart"];
                //Start: Added By Hetal Patel on 21-08-2020 For Live Site issue resolution
                if (dtCart == null)
                {
                    DataTable dtCart1 = new DataTable("Cart");
                    dtCart1.Columns.Add("menu_name", typeof(string));
                    dtCart1.Columns.Add("sub_menu_name", typeof(string));
                    dtCart1.Columns.Add("child_name", typeof(string));
                    dtCart1.Columns.Add("product_id", typeof(Int64));
                    dtCart1.Columns.Add("product_name", typeof(string));
                    dtCart1.Columns.Add("thumb_image", typeof(string));
                    dtCart1.Columns.Add("weight", typeof(string));
                    dtCart1.Columns.Add("size", typeof(string));
                    dtCart1.Columns.Add("color_name", typeof(string));
                    dtCart1.Columns.Add("sale_price", typeof(decimal));
                    dtCart1.Columns.Add("discount", typeof(decimal));
                    dtCart1.Columns.Add("coupon_discount", typeof(decimal));
                    dtCart1.Columns.Add("quantity", typeof(Int32));
                    dtCart1.Columns.Add("Total", typeof(decimal));
                    dtCart1.Columns.Add("shipping_price", typeof(decimal));
                    dtCart1.Columns.Add("gendertype", typeof(byte));
                    dtCart1.Columns.Add("custom_bag_param", typeof(string));
                    dtCart1.Columns.Add("custom_bag_price", typeof(decimal));
                    dtCart1.Columns.Add("x_point", typeof(float));
                    dtCart1.Columns.Add("y_point", typeof(float));
                    dtCart1.Columns["quantity"].DefaultValue = 1;
                    dtCart1.Columns["product_id"].Unique = true;
                    HttpContext.Current.Session["Cart"] = dtCart1;
                    dtCart = dtCart1;
                }
                //End: Added By Hetal Patel on 21-08-2020 For Live Site issue resolution

                foreach (DataRow row in dtCart.Rows)
                {

                    if (int.Parse(row["product_id"].ToString()) == product_id)
                    {
                        row["quantity"] = (int)row["quantity"] + 1;

                        decimal price = Convert.ToDecimal(row["sale_price"]);
                        decimal discount = Convert.ToDecimal(row["discount"]);
                        decimal UnitPriceOnDiscount = discount > 0 ? price - (price * discount / 100) : price;

                        row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
                        HttpContext.Current.Session["Cart"] = dtCart;
                        blnMatch = true;
                        break;
                    }

                }

                if (!blnMatch)
                {
                    product_handler productHandler = new product_handler();
                    DataTable dt = productHandler.get_product_details(product_id).Tables[0];

                    if (dt.Rows[0]["in_stock"].ToString().ToLower() != "true")
                        return 2;

                    DataRow drCart = dtCart.NewRow();
                    drCart["product_id"] = product_id;
                    drCart["product_name"] = dt.Rows[0]["product_name"].ToString();
                    drCart["thumb_image"] = "images/Product/Thumb/" + dt.Rows[0]["thumb_image"].ToString();
                    drCart["menu_name"] = dt.Rows[0]["menu_name"].ToString();
                    drCart["sub_menu_name"] = dt.Rows[0]["sub_menu_name"].ToString();
                    drCart["child_name"] = dt.Rows[0]["child_name"].ToString();
                    drCart["weight"] = dt.Rows[0]["weight"].ToString();
                    drCart["gendertype"] = dt.Rows[0]["gendertype"];

                    drCart["size"] = dt.Rows[0]["size"].ToString();
                    if (HttpContext.Current.Session["size"] != null)
                        drCart["size"] = HttpContext.Current.Session["size"].ToString();

                    drCart["color_name"] = dt.Rows[0]["color_name"].ToString();
                    decimal price = Convert.ToDecimal(dt.Rows[0]["Price"].ToString());
                    decimal discount = Convert.ToDecimal(dt.Rows[0]["discount"].ToString());
                    decimal TotalPrice = discount > 0 ? price - (price * discount / 100) : price;

                    HttpContext.Current.Session["sale_discount"] = String.Format("{0:0.00}", (price - TotalPrice));

                    drCart["sale_price"] = price;
                    drCart["discount"] = discount;
                    drCart["coupon_discount"] = 0;
                    drCart["custom_bag_price"] = 0;
                    drCart["quantity"] = 1;
                    drCart["Total"] = Convert.ToInt32(1) * TotalPrice;
                    dtCart.Rows.Add(drCart);

                    HttpContext.Current.Session["Cart"] = dtCart;
                }
                return 0;

            }
            catch (Exception)
            {
                return 1;
            }
        }

        [WebMethod(EnableSession = true)]
        public int AddWishList(int id)
        {
            if (HttpContext.Current.Session == null)
                return 0;

            if (HttpContext.Current.Session["CustomerLoginDetails"] != null)
            {
                wishlist_handler wishlistHandler = new wishlist_handler();
                BusinessEntities.wishlist wishlist = new BusinessEntities.wishlist();

                wishlist.product_id = id;
                wishlist.email_id = HttpContext.Current.Session["CustomerLoginDetails"].ToString();

                bool resultReview = wishlistHandler.InsertWishList(wishlist);
                if (resultReview)
                {
                    SendEmailadmin(id);
                    return 1;
                }
                else
                    return -1;
            }
            return 0;
        }

        private static void SendEmailadmin(int ProductId)
        {
            string msgbody = "";
            string imgThumb, ProductUrl;

            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_product_details(ProductId);
            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];
                if (Convert.ToInt32(dt.Rows[0]["quantity"]) > 0)
                {
                    return;
                }

                msgbody = @"<tr><td valign='top' style='padding:40px; margin:0px;'>
                <table border='0' cellpadding='0' cellspacing='0' style='width: 86%;'>
                <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>Hi Admin</p>
                <tr>
                <td><p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'> New item added in Wishlist by " + HttpContext.Current.Session["CutomerName"].ToString() + ", which quantity is zero(0)." + @"</p></td></tr>
                </table></td></tr>
                <tr><td>
                <table  width='100%' border='0'>
                   <tr>
                        <td colspan='3' style='border-top:1px solid #7CD5F3'>&nbsp;</td>
                   </tr>
                   <tr> 
                    <td width='17%'>Image</td>
                    <td width='52%'>Product Name</td>
                    <td width='13%'>Quantity</td>
                   </tr>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dt.Rows[i]["thumb_image"].ToString().Replace(" ", "%20");
                    ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dt.Rows[i]["product_id"].ToString();
                    msgbody += @"<tr>
                            <td width='17%'><img style='height:90px;' src='" + imgThumb + @"'/></td>
                            <td width='52%' style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:center'><a target='_blank' style='color:#FB641B' href='" + ProductUrl + "'>" + dt.Rows[i]["product_name"].ToString() + @"</a></td>
                            <td width='13%'  style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:center'>" + dt.Rows[i]["quantity"].ToString() + @"</td>
                    </tr>";
                }
                msgbody += @"</table></td></tr>";

                DAL.Utility.sendEmail("Wishlist Item Quantity is Zero ", msgbody, null, null, System.Configuration.ConfigurationManager.AppSettings["emailCc"], null);
            }
        }
    }
}