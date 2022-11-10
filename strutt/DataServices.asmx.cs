using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BLL;

namespace strutt
{
    /// <summary>
    /// Summary description for DataServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DataServices : System.Web.Services.WebService
    {
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

        [WebMethod]
        public string GetProducts(int cId, int scId, decimal minPrice, decimal maxPrice, string colors, string materials, string pName,
                                         bool isSales, bool isEx, bool isBest, byte? gid, string orderBy, int pageIndex)
        {
            int pageSize = 16;
            DataTable dt = null;
            product_handler filterProductsHandler = new product_handler();
            DataSet ds = filterProductsHandler.filter_productlist(cId, scId, 0, 0, minPrice, maxPrice, colors, materials, pName, isSales, isEx, isBest, gid, pageIndex, 16, orderBy, 16);

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataTable NewDt = dt.Copy();
                    NewDt.TableName = "product";

                    DataSet dsProduct = new DataSet("productlist");
                    dsProduct.Tables.Add(NewDt);

                    DataTable dtTemp = new DataTable("PageCount");
                    dtTemp.Columns.Add("PageCount");
                    dtTemp.Rows.Add();
                    dtTemp.Rows[0][0] = Math.Ceiling(Convert.ToSingle(NewDt.Rows[0]["TotalRec"]) / pageSize).ToString();
                    dsProduct.Tables.Add(dtTemp);

                    return dsProduct.GetXml();
                }
            }
            return "";
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

        [WebMethod]
        public string GetOneProduct(int id)
        {
            product_handler productHandler = new product_handler();
            DataSet ds = productHandler.get_product_details(id);

            if (ds != null && ds.Tables.Count > 0)
            {
                ds.Tables[0].Columns.Add(new DataColumn("rating", typeof(Int16)));
                ds.Tables[0].Rows[0]["rating"] = GetReviews(id);
                ds.DataSetName = "ProductData";
                ds.Tables[0].TableName = "Product";
                ds.Tables[1].TableName = "ProductImage";
                return ds.GetXml();
            }
            return "";
        }
        private short GetReviews(int product_id)
        {
            int totalReview = 0;
            review_handler reviewHandler = new review_handler();
            DataSet ds = reviewHandler.get_product_review(product_id, 3);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalReview += Convert.ToInt32(dt.Rows[i]["rating"].ToString());
                    }

                    float average = Convert.ToSingle(totalReview) / Convert.ToSingle(dt.Rows.Count);
                    return Convert.ToInt16(Math.Ceiling(average));
                }
            }
            return 0;
        }


        // Added by Chandni 18-Feb-2021 12:23 
        [WebMethod(EnableSession = true)]
        public bool AddToCart(int product_id,string size)
        {
            Boolean blnMatch = false;
            DataTable dtCart;
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["Cart"] == null)
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
            else
                dtCart = (DataTable)HttpContext.Current.Session["Cart"];

                foreach (DataRow row in dtCart.Rows)
            {
                if (int.Parse(row["product_id"].ToString()) == product_id)
                {
                    row["quantity"] = (int)row["quantity"] + 1;

                    Decimal price = Convert.ToDecimal(row["sale_price"]);
                    Decimal discount = Convert.ToDecimal(row["discount"]);
                    Decimal UnitPriceOnDiscount = discount > 0 ? price - (price * discount / 100) : price;

                    row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
                    HttpContext.Current.Session["Cart"] = dtCart;
                    blnMatch = true;
                    break;
                }
            }

            if (!blnMatch)
            {
                product_handler productHandler = new product_handler();
                DataSet dsProducts = productHandler.get_product_details(product_id);

                if (dsProducts != null && dsProducts.Tables.Count > 0)
                {
                    DataTable dt = dsProducts.Tables[0];

                    DataRow drCart = dtCart.NewRow();
                    drCart["product_id"] = product_id;
                    drCart["product_name"] = dt.Rows[0]["product_name"].ToString();
                    drCart["thumb_image"] = "images/Product/Thumb/" + dt.Rows[0]["thumb_image"].ToString();
                    drCart["menu_name"] = dt.Rows[0]["menu_name"].ToString();
                    drCart["sub_menu_name"] = dt.Rows[0]["sub_menu_name"].ToString();
                    drCart["child_name"] = dt.Rows[0]["child_name"].ToString();
                    drCart["weight"] = dt.Rows[0]["weight"].ToString();
                    drCart["gendertype"] = dt.Rows[0]["gendertype"];

                    if (string.IsNullOrEmpty(size))
                        drCart["size"] = dt.Rows[0]["size"].ToString();
                    else
                        drCart["size"] = size;

                    drCart["color_name"] = dt.Rows[0]["color_name"].ToString();
                    Decimal price = Convert.ToDecimal(dt.Rows[0]["Price"].ToString());
                    Decimal discount = Convert.ToDecimal(dt.Rows[0]["discount"].ToString());
                    Decimal TotalPrice = discount > 0 ? price - (price * discount / 100) : price;

                    //HttpContext.Current.Session["sale_discount"] = String.Format("{0:0.00}", (price - TotalPrice));

                    drCart["sale_price"] = price;
                    drCart["discount"] = discount;
                    drCart["coupon_discount"] = 0;
                    drCart["custom_bag_price"] = 0;
                    drCart["quantity"] = 1;
                    drCart["Total"] = Convert.ToInt32(1) * TotalPrice;
                    dtCart.Rows.Add(drCart);
                }

                HttpContext.Current.Session["Cart"] = dtCart;

            }
            string result;
            DataSet temp1 = new DataSet();   // ;= dtCart.DataSet;
            temp1.DataSetName = "Items";

            temp1.Tables.Add(dtCart);
            dtCart.TableName = "CartItems1";
            result = temp1.GetXml();

            return true;
        }
        // Added by Chandni 18-Feb-2021 12:23 
    }
}
