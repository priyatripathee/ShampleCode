using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Web.UI.HtmlControls;

namespace strutt
{
    /// <summary>
    /// Added smid to disable personalized button by Hetal Patel on 05-03-2020
    /// </summary>
    public partial class productdetails : System.Web.UI.Page
    {
        public string GenderType { get; set; }
        long matchid = 0;
        long proid = 0;
        int totalReview = 0;
        byte gid = 0;
        HtmlMeta meta = new HtmlMeta();
        long smid = 0;

        private Int64 product_id
        {
            get
            {
                if (ViewState["product_id"] != null)
                {
                    return Convert.ToInt64(ViewState["product_id"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value == 0)
                {
                    ViewState["product_id"] = null;
                }
                else
                {
                    ViewState["product_id"] = value;
                }
            }
        }

        #region load event...
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CustomerLoginDetails"] != null)
            {
                //aWishlist.Visible = false;
                // lbtnAddWishlist.Visible = true;
            }
            
            if (!IsPostBack)
            {
                if (Request.QueryString["proid"] != null)
                {

                    //product_id = Convert.ToInt64(Request.QueryString["proid"].ToString());
                    long parameterId = 0;
                    if (!long.TryParse(Request.QueryString["proid"].ToString(), out parameterId))
                        Response.Redirect("~/default.aspx");

                    product_id = parameterId;
                    product_handler productHandler = new product_handler();
                    DataSet dsProducts = productHandler.get_product_details(product_id);
                    DataTable dt = dsProducts.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    else
                    {
                        this.BindProduct(product_id);
                        // this.BindProductSize(product_id);
                        this.BindReviews(product_id);
                        //this.BindExtraImage(product_id); Comment by chandni
                        this.productindex(product_id);
                        this.productview(product_id);
                    }
                }
            }

            string FacebookPixelCode = @"
                fbq('track', 'ViewContent', {
                content_ids: ['" + product_id.ToString() + @"'],
                content_type: 'product',
                value: " + lblSalePrice.Text + @",
                currency: 'INR'
                });";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "FacebookPixelCode", FacebookPixelCode, true);

            getText();
        }
        #endregion

        #region navigation/Zoomer/Related Product...
        /// <summary>
        /// Added sub menu "The Native Batua" condition to disable personalized button by Hetal Patel on 05-03-2020
        /// </summary>
        /// <param name="product_id"></param>
        private void BindProduct(long product_id)
        {

            int stock = 0;
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_product_details(product_id);


            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];

                if (dt.Rows[0]["sub_menu_id"].Equals(2020))       //Only for "hand-made-masks"
                    litRetunText.Text = "Returns within 7 days from delivery in case of manufacturing defects only. We are not accepting returns for masks since we care for your safety. kindly read all information on sizes and description carefully before placing the order.";
                else
                    litRetunText.Text = "Returns within 7 days from delivery.Exchange or return is not applicable for bundled products and for products with an MRP above INR 5,000/-. Manufacturing warranty remaining the same, Please do read Returns and exchange section for further clarity.";

                ViewState["ProductDetails"] = dt;

                this.Page.Title = dt.Rows[0]["meta_title"].ToString();
                this.Page.MetaDescription = dt.Rows[0]["meta_description"].ToString();


                rpt_naviCategory.DataSource = dt;
                rpt_naviCategory.DataBind();

                rpt_naviSubCategory.DataSource = dt;
                rpt_naviSubCategory.DataBind();

                //rpt_naviCldCategory.DataSource = dt;
                //rpt_naviCldCategory.DataBind();

                lblProNameNavigation.Text = dt.Rows[0]["product_name"].ToString();

                meta.Name = "buy online:" + lblProNameNavigation.Text;
                Page.Header.Controls.Add(meta); 

                lblProductName.Text = dt.Rows[0]["product_name"].ToString();
                lblProName.Text = dt.Rows[0]["product_name"].ToString();//In Breadcum display Product name
                
                //hfProId.Value = dt.Rows[0]["rating"].ToString();
                //lblColor.Text = dt.Rows[0]["color_name"].ToString();

                //lblPrice.Text = dt.Rows[0]["price"].ToString();
                decimal discount = Convert.ToDecimal(dt.Rows[0]["discount"].ToString());
                //lblDiscount.Text = String.Format("{0:0}", discount) + "% OFF";
                lblSalePrice.Text = dt.Rows[0]["sale_price"].ToString();
                lblProName.Text = dt.Rows[0]["product_name"].ToString();// Added by chandni
                lblSalePrice2.Text = dt.Rows[0]["sale_price"].ToString();// Added by chandni
                decimal SalesPrice = Convert.ToDecimal(lblSalePrice.Text);
                if (SalesPrice > 5000)
                {
                    Session["SalesPrice"] = SalesPrice;
                    litRetunText.Text = "Manufacturing warranty of 1 year for any manufacturing defects. This product is eligible for replacement only and cannot be returned for refund.";
                    litWarrantyText.Text = "Strutt offers a 1 year replacement warranty for any manufacturing defects.";

                }

                else
                    litWarrantyText.Text = "Strutt offers a 90 day Replacement warranty in case of any Manufacturing Defect.";

                if (discount > 0)
                    lblDiscPrice.Text = "Rs. " + dt.Rows[0]["price"].ToString();

                //if (discount == 0)
                //{
                //   // spDiscount.Visible = false;
                //    //spPrice.Visible = false;
                //}
                //else
                //{
                //    spDiscount.Visible = true;
                //    spPrice.Visible = true;
                //    spPrice.Attributes.Add("class", "orginal-price");
                //}


                //lblSize.Text = dt.Rows[0]["size"].ToString();
                //if (dt.Rows[0]["full_description"].ToString().Length > 150)
                //    lblDescrpition.Text = dt.Rows[0]["full_description"].ToString().Substring(0, 150) + "...";
                //else
                //    lblDescrpition.Text = dt.Rows[0]["full_description"].ToString();
                lblPDFullDescription.Text = dt.Rows[0]["full_description"].ToString();
                lblFeatures.Text = dt.Rows[0]["features"].ToString();
                lblPDMeterial.Text = dt.Rows[0]["material_name"].ToString();
                lblPDSize.Text = dt.Rows[0]["size"].ToString();
                lblPDColor.Text = dt.Rows[0]["color_name"].ToString();
                lblPDWeight.Text = dt.Rows[0]["weight"].ToString();
                smid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());
                lblPDMeterial2.Text = dt.Rows[0]["material_name"].ToString();
                lblPDSize2.Text = dt.Rows[0]["size"].ToString();
                lblPDColor2.Text = dt.Rows[0]["color_name"].ToString();
                //if (Convert.ToInt32(dt.Rows[0]["quantity"]) > 0 && Convert.ToInt32(dt.Rows[0]["quantity"]) < 5)
                //{
                //    lblQty.Text = "Hurry! only 5 products left";
                //}

                //if (Session["product_id"] != null)
                //{
                DataTable dtCart = (DataTable)Session["Cart"];
                matchid = Convert.ToInt64(dt.Rows[0]["product_id"].ToString());

                if (dtCart != null && dtCart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCart.Rows.Count; i++)
                    {
                        proid = Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString());
                        if (matchid == proid)
                        {
                            btnAddToCart.Enabled = true;
                            lblStock.Visible = false;
                            btnbuynow.Enabled = true;
                            btnCustomBug.Enabled = true;
                            break;
                        }
                        else
                        {
                            stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
                            if (stock <= 0)
                            {

                                btnAddToCart.Enabled = false;
                                btnAddToCart.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                                btnAddToCart2.Enabled = false;
                                btnAddToCart2.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                                btnbuynow.Enabled = false;
                                btnbuynow.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                                btnCustomBug.Enabled = false;
                                lblStock.Visible = false;
                                lblStock.Text = "Out of Stock";
                                lblStock2.Visible = false;
                                lblStock2.Text = "Out of Stock";
                                // divaleadyadd.Visible = false;
                            }
                            else
                            {

                                if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                                {
                                    btnCustomBug.Enabled = false;
                                }
                                else
                                {
                                    btnCustomBug.Enabled = true;
                                }
                                btnAddToCart.Enabled = true;
                                btnbuynow.Enabled = true;
                                lblStock.Visible = true;
                                lblStock.Text = "In Stock";
                                lblStock2.Visible = true;
                                lblStock2.Text = "In Stock";

                                // divaleadyadd.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
                    if (stock <= 0)
                    {

                        btnAddToCart.Enabled = false;
                        btnAddToCart.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                        btnAddToCart2.Enabled = false;
                        btnAddToCart2.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                        btnbuynow.Enabled = false;
                        btnbuynow.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                        btnCustomBug.Enabled = false;
                        lblStock.Visible = true;
                        lblStock.Text = "In Stock";
                        lblStock2.Visible = true;
                        lblStock2.Text = "In Stock";
                        // divaleadyadd.Visible = false;
                    }
                    else
                    {
                        if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                        {
                            btnCustomBug.Enabled = false;
                        }
                        else
                        {
                            btnCustomBug.Enabled = true;
                        }

                        btnAddToCart.Enabled = true;
                        btnAddToCart2.Enabled = true;
                        btnbuynow.Enabled = true;

                        //lblStock.Visible = false;
                        lblStock.Visible = true;
                        lblStock.Text = "In Stock";
                        lblStock2.Visible = true;
                        lblStock2.Text = "In Stock";
                        // divaleadyadd.Visible = false;

                    }
                }
                //}
                //else
                //{
                //    stock = Convert.ToBoolean(dt.Rows[0]["in_stock"].ToString());

                //    if (stock == false)
                //    {
                //       // btnAddToCart.Visible = false;
                //        //btnBuyNow.Visible = false;
                //       // lblStock.Visible = true;
                //       // lblStock.Text = "Out of Stock";
                //    }
                //    else
                //    {
                //       // btnAddToCart.Visible = true;
                //        //btnBuyNow.Visible = true;
                //       // lblStock.Visible = false;
                //       // divaleadyadd.Visible = false;
                //    }
                //}

                DataTable dtimgZoom = dsProducts.Tables[1];
                if (dtimgZoom.Rows.Count > 0)
                {
                    rptimgZoom.Visible = true;
                    rptimgZoom.DataSource = dtimgZoom;
                    rptimgZoom.DataBind();

                    rptimgZoomLarge.Visible = true;
                    rptimgZoomLarge.DataSource = dtimgZoom;
                    rptimgZoomLarge.DataBind();

                    rptRelatedZoom2.Visible = true; // Added by Chandni 12-Jan-2020
                    rptRelatedZoom2.DataSource = dtimgZoom;
                    rptRelatedZoom2.DataBind();


                }

                DataTable dtRelatedProduct = dsProducts.Tables[2];
                if (dtRelatedProduct.Rows.Count > 0)
                {
                    rptRelatedProduct.DataSource = dtRelatedProduct;
                    rptRelatedProduct.DataBind();
                }
            }
        }

        private void productindex(long product_id)   //For : Browse abandonment code (a)
        {
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_product_details(product_id);


            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];

                string indexingapi = @"wigzo(""index"", {
""canonicalUrl"": """ + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + Request.RawUrl + @""",
""title"": """ + dt.Rows[0]["product_name"] + @""",
""description"": """ + dt.Rows[0]["product_name"].ToString() + @""",    
""price"": """ + dt.Rows[0]["price"] + @""",
""productId"": """ + Request.QueryString["proid"] + @""",
""image"": """ + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dt.Rows[0]["thumb_image"] + @""",
""language"":""en"" }) ";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "indexingapi", indexingapi, true);
            }

        }
        //private void BindExtraImage(long product_id)
        //{
        //    product_handler productHandler = new product_handler();
        //    DataSet dsProducts = productHandler.get_product_image_extra(product_id);


        //    if (dsProducts != null && dsProducts.Tables.Count > 0)
        //    {
        //        DataTable dt = dsProducts.Tables[0];
        //        if (dt.Rows.Count < 6)
        //        {
        //            divExtraImage.Visible = false;
        //            return;
        //        }

        //        if (dt.Rows.Count > 0)
        //        {
        //            if (dt.Rows.Count > 0 && dt.Rows[0]["thumb_image"] != null)
        //                Image1.ImageUrl = "images/ExtraImage/" + dt.Rows[0]["thumb_image"].ToString();
        //            if (dt.Rows.Count > 1 && dt.Rows[1]["thumb_image"] != null)
        //                Image2.ImageUrl = "images/ExtraImage/" + dt.Rows[1]["thumb_image"].ToString();
        //            if (dt.Rows.Count > 2 && dt.Rows[2]["thumb_image"] != null)
        //                Image3.ImageUrl = "images/ExtraImage/" + dt.Rows[2]["thumb_image"].ToString();
        //            if (dt.Rows.Count > 3 && dt.Rows[3]["thumb_image"] != null)
        //                Image4.ImageUrl = "images/ExtraImage/" + dt.Rows[3]["thumb_image"].ToString();
        //            if (dt.Rows.Count > 4 && dt.Rows[4]["thumb_image"] != null)
        //                Image5.ImageUrl = "images/ExtraImage/" + dt.Rows[4]["thumb_image"].ToString();
        //            if (dt.Rows.Count > 5 && dt.Rows[5]["thumb_image"] != null)
        //                Image6.ImageUrl = "images/ExtraImage/" + dt.Rows[5]["thumb_image"].ToString();
        //            if (dt.Rows.Count > 6 && dt.Rows[6]["thumb_image"] != null)
        //                Image1.ImageUrl = "images/ExtraImage/" + dt.Rows[6]["thumb_image"].ToString();
        //        }
        //    }
        //}
        protected void rptRelatedProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // decimal discount;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl spOldPrice = e.Item.FindControl("spOldPrice") as HtmlGenericControl;
                HtmlGenericControl divdiscount = e.Item.FindControl("divdiscount") as HtmlGenericControl;
                HiddenField Hdiscount = (HiddenField)e.Item.FindControl("hfieldDiscount");
                //discount = Convert.ToDecimal(Hdiscount.Value);

                //if (discount == 0)
                //{
                //    divdiscount.Visible = false;
                //    spOldPrice.Visible = false;
                //}
                //else
                //{
                //    divdiscount.Visible = true;
                //    spOldPrice.Visible = true;
                //    spOldPrice.Attributes.Add("class", "price-old");
                //}
            }
        }

        #endregion

        protected void lbtnPersonatize_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/customize_product.aspx?proid=" + product_id.ToString());
        }

        #region Add To Cart...

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            //this.AddToShoppingCart();
            //Response.Redirect("~/cart.aspx?pid=" + product_id.ToString());

            this.AddToShoppingCart();
            Response.Redirect("~/proceedtopayment.aspx");
        }

        private void productview(long product_id)   //For : Browse abandonment code (2-A)
        {
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_product_details(product_id);


            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];

                string productview = @"
                    wigzo(""track"", ""productview"", """ + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + Request.RawUrl + @""");";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "productview", productview, true);
            }

        }

        private bool AddToShoppingCart()
        {

            long product_id = Convert.ToInt64(Request.QueryString["proid"].ToString());
            Boolean blnMatch = false;
            DataTable dtCart = (DataTable)Session["Cart"];
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
                Session["Cart"] = dtCart1;
                dtCart = dtCart1;
            }
            //End: Added By Hetal Patel on 21-08-2020 For Live Site issue resolution

            foreach (DataRow row in dtCart.Rows)
            {

                if (int.Parse(row["product_id"].ToString()) == product_id)
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

                    DataTable dt = (DataTable)ViewState["ProductDetails"];


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

        #endregion


        #region Pending Wishlist...
        protected void lbtnAddWishlist_Click(object sender, EventArgs e)
        {
            if (Session["CustomerLoginDetails"] != null)
            {
                wishlist_handler wishlistHandler = new wishlist_handler();
                BusinessEntities.wishlist wishlist = new BusinessEntities.wishlist();

                //wishlist.WishlistId=
                wishlist.product_id = product_id;
                wishlist.email_id = Session["CustomerLoginDetails"].ToString();

                bool resultReview = wishlistHandler.InsertWishList(wishlist);
                if (resultReview == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('adding item to wishlist Successfully')", true);
                    SendEmailadmin();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please login before add item in Wish list.')", true);
            }
        }


        #endregion
        public void SendEmailadmin()
        {
            string msgbody = "";
            string imgThumb, ProductUrl;

            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_product_details(product_id);
            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];
                if (Convert.ToInt32(dt.Rows[0]["quantity"]) > 0)
                {
                    return;
                }

                lblProductName.Text = dt.Rows[0]["product_name"].ToString();

                msgbody = @"<tr><td valign='top' style='padding:40px; margin:0px;'>
                <table border='0' cellpadding='0' cellspacing='0' style='width: 86%;'>
                <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>Hi Admin</p>
                <tr>
                <td><p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'> New item added in Wishlist by " + Session["CutomerName"].ToString() + ", which quantity is zero(0)." + @"</p></td></tr>
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
                    //double ItemAmount = Convert.ToDouble(dt.Rows[i]["quantity"]) * Convert.ToDouble(dt.Rows[i]["sale_price"]);
                    imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dt.Rows[i]["thumb_image"].ToString().Replace(" ", "%20");
                    ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dt.Rows[i]["product_id"].ToString();

                    msgbody += @"<tr>
                            <td width='17%'><img style='height:90px;' src='" + imgThumb + @"'/></td>
                            <td width='52%' style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:center'><a target='_blank' style='color:#FB641B' href='" + ProductUrl + "'>" + dt.Rows[i]["product_name"].ToString() + @"</a></td>
                            <td width='13%'  style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:center'>" + dt.Rows[i]["quantity"].ToString() + @"</td>
 1                   </tr>";
                }
                msgbody += @"</table></td></tr>";

                DAL.Utility.sendEmail("Wishlist Item Quantity is Zero ", msgbody, null, null, System.Configuration.ConfigurationManager.AppSettings["emailCc"], null);
            }
        }
        #region Reviews...

        private void BindReviews(long review_id)
        {
            review_handler reviewHandler = new review_handler();
            DataSet ds = reviewHandler.get_product_review(review_id, 3);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Rvp_ShowReview.DataSource = dt;
                    Rvp_ShowReview.DataBind();

                    //ratingShow.Visible = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalReview += Convert.ToInt32(dt.Rows[i]["rating"].ToString());
                        
                    }

                    float average = Convert.ToSingle(totalReview) / Convert.ToSingle(dt.Rows.Count);
                    //ratingShow.CurrentRating = Convert.ToInt16(Math.Ceiling(average));
                    //ratingShowp.CurrentRating = Convert.ToInt16(Math.Ceiling(average));
                   // lblReview.Text = average.ToString("0.00") + " | " + dt.Rows.Count + " Customer Review(s)";
                    //lblReview2.Text = average.ToString("0.00") + " | " + dt.Rows.Count + " Customer Review(s)";
                    hfProId.Value = Convert.ToInt32(average).ToString();
                    hfPro2Id.Value = Convert.ToInt32(average).ToString();
                    litReviewHead.Text = "View Review";
                }
                else
                {
                    //ratingShow.Visible = false;
                    litReviewHead.Text = "No Review";
                }
            }
        }
        protected void Btn_Reviews_Click(object sender, EventArgs e)
        {
            review_handler reviewHandler = new review_handler();
            BusinessEntities.review review = new BusinessEntities.review();
            review.product_id = Convert.ToInt64(product_id.ToString());
            review.user_name = txtUserName.Text;
            review.rating = ratingControl.CurrentRating;
            review.title = txtTitle.Text;
            review.reviews = txtReview.Text;

            if (ratingControl.CurrentRating == 0)
            {
                lbltxt.Text = "Plese select rating";
            }
            else
            {
                bool resultReview = reviewHandler.insert_reviews(review);
                if (resultReview == true)
                {
                    lblValMes.Visible = true;
                    lblValMes.Text = "Your review submited Successfully.";
                    txtUserName.Text = string.Empty;
                    txtemail.Text = string.Empty;
                    txtTitle.Text = string.Empty;
                    txtReview.Text = string.Empty;
                    lbltxt.Text = string.Empty;
                }
                else
                {
                    lblValMes.Visible = true;
                    lblValMes.Text = "Review Failure. Error during submit review.";
                }
            }

        }

        #endregion
        protected void btnCheckPincode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCheckPincode.Text))
            {
                litCheckPincodeMsg.ForeColor = System.Drawing.Color.Red;
                litCheckPincodeMsg.Text = "Enter Pincode";
                litCheckPincodeMsg.CssClass = "err";
                return;
            }

            pincode_handler pincodeHandler = new pincode_handler();
            if (!pincodeHandler.check_Pincode_COD(txtCheckPincode.Text))
            {
                litCheckPincodeMsg.ForeColor = System.Drawing.Color.Red;
                litCheckPincodeMsg.Text = "Not available";
                litCheckPincodeMsg.CssClass = "err";
            }
            else
            {
                litCheckPincodeMsg.ForeColor = System.Drawing.Color.Green; ;
                litCheckPincodeMsg.Text = "Available";
                litCheckPincodeMsg.CssClass = "msg";
            }
            if (pincodeHandler.check_Pincode_COD(txtCheckPincode.Text))
            {

            }
        }

        protected void btnbuynow_Click(object sender, EventArgs e)
        {
            this.AddToShoppingCart();
            Response.Redirect("~/proceedtopayment.aspx");
        }


        protected void btnCustomBug_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/custombag.aspx");
            Response.Redirect("~/custombag.aspx?proid=" + ViewState["product_id"].ToString());
        }
        protected void rptRelatedProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "quickrelated")
            {
                BindProduct(product_id);
            }
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        }
        // Code Added by Chandni 12-Jan-2020
        //protected void rptRelatedProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "quickrelated")
        //    {
        //        product_id = Convert.ToInt64(e.CommandArgument.ToString());
        //        product_handler productHandler = new product_handler();
        //        DataSet dsProducts = productHandler.get_product_details(product_id);
        //        if (dsProducts != null && dsProducts.Tables.Count > 0)
        //        {
        //            DataTable dt = dsProducts.Tables[0];

        //            ViewState["ProductDetails"] = dt;
        //            lblProductName2.Text = dt.Rows[0]["product_name"].ToString();

        //            //lblDiscPrice.Text = dt.Rows[0]["price"].ToString();
        //            lblSalePrice2.Text = dt.Rows[0]["sale_price"].ToString();
        //            lblStock2.Text = dt.Rows[0]["in_stock"].ToString();
        //            smid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

        //            DataTable dtCart = (DataTable)Session["Cart"];
        //            long matchid = 0;
        //            long proid = 0;
        //            int stock = 0;
        //            matchid = Convert.ToInt64(dt.Rows[0]["product_id"].ToString());

        //            if (dtCart != null && dtCart.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dtCart.Rows.Count; i++)
        //                {
        //                    proid = Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString());
        //                    if (matchid == proid)
        //                    {
        //                        btnAddToCart.Enabled = true;
        //                        lblStock2.Visible = false;
        //                        //btnbuynow.Enabled = true;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
        //                        if (stock <= 0)
        //                        {

        //                            btnAddToCart.Enabled = false;
        //                            // btnbuynow.Enabled = false;
        //                            //btnbuynow.CssClass = "btn btn--lg btn--black";
        //                            //btnCustomBug.Enabled = false;
        //                            lblStock2.Visible = false;
        //                            // lblStock.Text = "Out of Stock";
        //                            // divaleadyadd.Visible = false;
        //                        }
        //                        else
        //                        {

        //                            if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
        //                            {
        //                                //btnCustomBug.Enabled = false;
        //                            }
        //                            else
        //                            {
        //                                //btnCustomBug.Enabled = true;
        //                            }
        //                            btnAddToCart.Enabled = true;
        //                            //btnbuynow.Enabled = true;
        //                            lblStock2.Visible = false;
        //                            // divaleadyadd.Visible = false;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
        //                if (stock <= 0)
        //                {

        //                    btnAddToCart.Enabled = false;
        //                    //btnbuynow.Enabled = false;
        //                    // btnCustomBug.Enabled = false;
        //                    lblStock2.Visible = true;
        //                    // lblStock.Text = "Out of Stock";
        //                    // divaleadyadd.Visible = false;
        //                }
        //                else
        //                {
        //                    if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
        //                    {
        //                        //btnCustomBug.Enabled = false;
        //                    }
        //                    else
        //                    {
        //                        // btnCustomBug.Enabled = true;
        //                    }

        //                    btnAddToCart.Enabled = true;
        //                    // btnbuynow.Enabled = true;

        //                    lblStock2.Visible = false;
        //                    // divaleadyadd.Visible = false;

        //                }
        //            }
        //            DataTable dtimgZoom = dsProducts.Tables[1];
        //            if (dtimgZoom.Rows.Count > 0)
        //            {
        //                rptimgZoomLarge2.Visible = true;
        //                rptimgZoomLarge2.DataSource = dtimgZoom;
        //                rptimgZoomLarge2.DataBind();
        //            }
        //        }
        //    }
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        //}
        protected void lbtnWishlist_Click(object sender, EventArgs e)
        {
            if (Session["CustomerLoginDetails"] != null)
            {
                wishlist_handler wishlistHandler = new wishlist_handler();
                BusinessEntities.wishlist wishlist = new BusinessEntities.wishlist();

                //wishlist.WishlistId=
                wishlist.product_id = product_id;
                wishlist.email_id = Session["CustomerLoginDetails"].ToString();

                bool resultReview = wishlistHandler.InsertWishList(wishlist);
                if (resultReview == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('adding item to wishlist Successfully')", true);
                    //SendEmailadmin(); Comment by Chandni
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please login before add item in Wish list.')", true);
            }
        }

        protected void btnAddToCart2_Click(object sender, EventArgs e)
        {
            this.AddToShoppingCart();
            Response.Redirect("~/cart.aspx?pid=" + product_id.ToString());
        }
        // Code Added by Chandni 12-Jan-2020

        private void getText()
        {
            try
            {
                DataTable dt = (DataTable)Session["PageLabel"];
                if (dt.Rows.Count > 0)
                {
                    lblTax.Text = dt.Select("label_name='Label Product Details For Tax'")[0]["label_value"].ToString();
                    lblCOD.Text = dt.Select("label_name='Label Product Details For COD'")[0]["label_value"].ToString();
                    lblDiscount.Text = dt.Select("label_name='Label Product Details For Discount'")[0]["label_value"].ToString();
                    lblGet.Text = dt.Select("label_name='Label Product Details For Get Extra'")[0]["label_value"].ToString();
                }
            }
            catch (Exception)
            {
                //If any error then ignore
            }
        }
    }
}