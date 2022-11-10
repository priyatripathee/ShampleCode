using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BusinessEntities;
using System.Data;
using System.Web.UI.HtmlControls;

namespace strutt
{
    public partial class default1 : System.Web.UI.Page
    {
        HtmlMeta meta = new HtmlMeta();
        long smid = 0;//Added by chandni
        byte? gid = null; //Added by chandni
        public byte GenderType { get; set; }//Added by chandni
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetHomeBanner();
                GetInstaFollow();
                getText();
                DynamicLabel();
                GetTopReview();
                BindBlog();
                AddToWishlist();
                //LoadMenu();   // Unused
                if (Request.QueryString["unsb"] != null)
                {
                    long unsub = Convert.ToInt64(Request.QueryString["unsb"].ToString());
                    //Unsubscribe(unsub);
                }
                txtSearch.Attributes.Add("autocomplete", "off");
            }

            if (Session["CustomerLoginDetails"] != null)
            {
                if (Session["CutomerName"] != null)
                {
                    divAfterLogin.Visible = true;
                    lblCustName.Text = string.Format("Welcome,{0}", Session["CutomerName"].ToString());

                    btnlogin.ForeColor = System.Drawing.Color.Green;
                    btnlogin.ToolTip = string.Format("Welcome, {0}", Session["CutomerName"].ToString());

                    aLogin.Visible = false;
                    aOrderDetails.Visible = true;
                }
                else if (Session["CustomerLoginDetails"] != null)
                {
                    lblCustName.Text = string.Format("Welcome,{0}", Session["CustomerLoginDetails"].ToString());
                    btnlogin.ForeColor = System.Drawing.Color.Green;
                    btnlogin.ToolTip = string.Format("Welcome,{0}", Session["CustomerLoginDetails"].ToString());

                    aLogin.Visible = false;
                    aOrderDetails.Visible = true;
                }
                //linkLogout.Visible = true;
                //linkLogin.Visible = false;
            }
            else
            {
                // linkLogout.Visible = false;
                //linkLogin.Visible = true;
            }
            Page.LoadComplete += new EventHandler(Page_LoadComplete);
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            AddToShoppingCart();
        }


        private void DynamicLabel()
        {
            //CMS details for verious Page
            pagelabel_handler pagelabelHandler = new pagelabel_handler();
            DataSet ds = pagelabelHandler.get_pagelabel(null);
            Session["PageLabel"] = ds.Tables[0];    //This is DataTable
        }
        private void GetHomeBanner()
        {
            banner_handler bannerHandler = new banner_handler();
            DataSet ds = bannerHandler.get_banner(null, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count >= 5)
                // //if (dt.Rows.Count >= 0)
                {
                    //////banner1.Attributes["href"] = dt.Rows[0]["url_path"].ToString();
                    //////banner2.Attributes["href"] = dt.Rows[1]["url_path"].ToString();
                    //////banner3.Attributes["href"] = dt.Rows[2]["url_path"].ToString();
                    //////banner4.Attributes["href"] = dt.Rows[3]["url_path"].ToString();
                    //////banner5.Attributes["href"] = dt.Rows[4]["url_path"].ToString();
                    //////lblBName1.Text = dt.Rows[0]["title"].ToString();
                    //////lblBName2.Text = dt.Rows[1]["title"].ToString();
                    //////lblBName3.Text = dt.Rows[2]["title"].ToString();
                    //////lblBName4.Text = dt.Rows[3]["title"].ToString();
                    //////lblBName5.Text = dt.Rows[4]["title"].ToString();
                    //////lblBanner1.ImageUrl = "~//images/Banner/" + dt.Rows[0]["image"].ToString();
                    //////lblBanner2.ImageUrl = "~//images/Banner/" + dt.Rows[1]["image"].ToString();
                    //////lblBanner3.ImageUrl = "~//images/Banner/" + dt.Rows[2]["image"].ToString();
                    //////lblBanner4.ImageUrl = "~//images/Banner/" + dt.Rows[3]["image"].ToString();
                    //////lblBanner5.ImageUrl = "~//images/Banner/" + dt.Rows[4]["image"].ToString();
                    //////lbltitle1.Text = dt.Rows[0]["title"].ToString();
                    //////lbltitle2.Text = dt.Rows[1]["title"].ToString();
                    //////lbltitle3.Text = dt.Rows[2]["title"].ToString();
                    //////lbltitle4.Text = dt.Rows[3]["title"].ToString();
                    //////lbltitle5.Text = dt.Rows[4]["title"].ToString();
                }
            }

        }
        private void GetInstaFollow()
        {
            banner_handler bannerHandler = new banner_handler();
            DataSet dsBanner = bannerHandler.get_banner(null, 1);
            if (dsBanner != null && dsBanner.Tables.Count > 0)
            {
                DataTable dt = dsBanner.Tables[0];
                DataView dvIsActive = new DataView(dt);
                rptBanner.DataSource = dvIsActive.ToTable();
                rptBanner.DataBind();
            }
        }
        private void getText()
        {
            try
            {
                if (Session["PageLabel"] != null)
                {
                    DataTable dt = (DataTable)Session["PageLabel"];
                    if (dt.Rows.Count > 0)
                    {
                        lblHomeText.Text = dt.Select("label_name='Home Text'")[0]["label_value"].ToString();
                        lblMarquee1.Text = dt.Select("label_name='Marquee1'")[0]["label_value"].ToString();
                        lblMarquee2.Text = dt.Select("label_name='Marquee2'")[0]["label_value"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                //If any error then ignore
            }
        }

        private void BindBlog()
        {
            blog_handler blogHandler = new blog_handler();
            DataSet ds = blogHandler.get_bog(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 3; i <= dt.Rows.Count - 1; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        dr.Delete();
                    }
                    rptBlog.DataSource = dt;
                    rptBlog.DataBind();
                }
                else
                {
                    rptBlog.DataSource = null;
                    rptBlog.DataBind();
                }
            }
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
                    for (int i = 3; i <= dt.Rows.Count - 1; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        dr.Delete();
                    }
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

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
                Response.Redirect("~/category.aspx?s=" + txtSearch.Text.Trim());
        }

        private void AddToShoppingCart()
        {

            //DataTable dtCart = (DataTable)Session["Cart"];
            decimal ShippingCharge = 0;
            Boolean blnMatch = false;

            if (Session["Cart"] == null)
                return;

            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart != null && dtCart.Rows.Count > 0)
                lblCartCount.Text = dtCart.Rows.Count.ToString();
            else
                lblCartCount.Text = "0";

            if (dtCart != null && dtCart.Rows.Count > 0)
            {
                Decimal amount = (from DataRow drCart in dtCart.AsEnumerable()
                                  where drCart.RowState != DataRowState.Deleted
                                  select (Convert.ToDecimal(drCart["Total"]))).Sum();

                Decimal CouponAmount = 0;
                if (Session["couponcodeprice"] != null)
                    CouponAmount = GetCouponAmount(amount, Convert.ToDecimal(Session["couponcodeprice"].ToString()));

                if (amount <= 750)
                    ShippingCharge = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["shippingcharge"]);

                decimal totalAmount = (from DataRow drCart in dtCart.AsEnumerable()
                                       where drCart.RowState != DataRowState.Deleted
                                       select (Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();

                decimal totalDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                                         where drCart.RowState != DataRowState.Deleted
                                         select ((Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["discount"]) / 100) * Convert.ToDecimal(drCart["quantity"]))).Sum();
                totalDiscount = totalDiscount + CouponAmount;

                decimal grandTotal = totalAmount + ShippingCharge - totalDiscount;

                lblCartCount.Text = dtCart.Rows.Count.ToString();
                litSubTotalAmt.Text = String.Format("{0:0.00}", totalAmount + ShippingCharge);

                litTotalDiscount.Text = String.Format("{0:0.00}", totalDiscount);

                litTotalAmt.Text = String.Format("{0:0.00}", grandTotal);


                rptCartPc.DataSource = dtCart;
                rptCartPc.DataBind();

                // rptCartMob.DataSource = dtCart; Comment by Chandni
                // rptCartMob.DataBind();Comment by Chandni
                //litTotalAmt2.Text = String.Format("{0:0.00}", grandTotal);Comment by Chandni
                //lblCartCount2.Text = dtCart.Rows.Count.ToString();Comment by Chandni
            }
            else
            {
                litTotalAmt.Text = "0.00";
                litSubTotalAmt.Text = "0.00";
                litTotalDiscount.Text = "0.00";
                lblCartCount.Text = "0";
                rptCartPc.DataSource = dtCart;
                rptCartPc.DataBind();

                //litTotalAmt2.Text = "0.00";Comment by Chandni
                //lblCartCount2.Text = "0";Comment by Chandni
                //rptCartMob.DataSource = dtCart; Comment by Chandni
                //rptCartMob.DataBind();Comment by Chandni
            }
        }

        private Decimal GetCouponAmount(Decimal amount, Decimal coupcode)
        {
            Decimal CA = amount - (amount * coupcode) / 100;
            Decimal TCA = amount - CA;
            return TCA;
        }
        private void AddToWishlist()
        {
            if (Session["CustomerLoginDetails"] == null)

                return;

            wishlist_handler wishlistHandler = new wishlist_handler();
            DataSet ds = wishlistHandler.get_wishlist_recommendation(0, Session["CustomerLoginDetails"].ToString(), 2);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                lblWishlist.Text = ds.Tables[0].Rows.Count.ToString();
            else
                lblWishlist.Text = "0";
            // Response.Redirect("wishlist.aspx");
        }

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
                    //SendEmailadmin(); Comment by Chandni
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please login before add item in Wish list.')", true);
            }
        }
        protected void rptCartPc_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                if (Session["Cart"] != null)
                {
                    DataTable dt = (DataTable)Session["Cart"];
                    if (dt.Rows.Count > 0)
                        dt.Rows.RemoveAt(e.Item.ItemIndex);

                    Session["Cart"] = dt;
                }
            }
        }
    }
}



