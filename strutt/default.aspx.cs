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
    public partial class _default : System.Web.UI.Page
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
                //this.BindBanner();
                BindHandBag();
                BindDuffleBag();
                BindLaptopBag();
                //BindOverNightBag();

                //this.BindDuffle();
                //BindLaptop();
                //BindOverNight();
                //BindAccessories();
                GetHomeBanner();
                GetInstaFollow();
                BindBlog();
                GetLatestArrivals();
                AddToWishlist();
                getText();
                LoadMenu();
                DynamicLabel();
                GetTopReview();
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
                    btnlogin.ForeColor = System.Drawing.Color.Green;
                    btnlogin.ToolTip = string.Format("Welcome, {0}", Session["CutomerName"].ToString());
                }
                else if (Session["CustomerLoginDetails"] != null)
                {
                    btnlogin.ForeColor = System.Drawing.Color.Green;
                    btnlogin.ToolTip = string.Format("Welcome,{0}", Session["CustomerLoginDetails"].ToString());
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
        // Comment by Chandni.
        //private void BindBanner()
        //{
        //    banner_handler bannerHandler = new banner_handler();
        //    DataSet dsBanner = bannerHandler.get_banner(0);
        //    if (dsBanner != null && dsBanner.Tables.Count > 0)
        //    {
        //        DataTable dt = dsBanner.Tables[0];
        //        DataView dvIsActive = new DataView(dt);
        //        dvIsActive.RowFilter = "is_active=1";
        //        rptBanner.DataSource = dvIsActive.ToTable();
        //        rptBanner.DataBind();
        //    }
        //}
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
                    banner1.Attributes["href"] = dt.Rows[0]["url_path"].ToString();
                    banner2.Attributes["href"] = dt.Rows[1]["url_path"].ToString();
                    banner3.Attributes["href"] = dt.Rows[2]["url_path"].ToString();
                    banner4.Attributes["href"] = dt.Rows[3]["url_path"].ToString();
                    banner5.Attributes["href"] = dt.Rows[4]["url_path"].ToString();
                    lblBName1.Text = dt.Rows[0]["title"].ToString();
                    lblBName2.Text = dt.Rows[1]["title"].ToString();
                    lblBName3.Text = dt.Rows[2]["title"].ToString();
                    lblBName4.Text = dt.Rows[3]["title"].ToString();
                    lblBName5.Text = dt.Rows[4]["title"].ToString();
                    lblBanner1.ImageUrl = "~//images/Banner/" + dt.Rows[0]["image"].ToString();
                    lblBanner2.ImageUrl = "~//images/Banner/" + dt.Rows[1]["image"].ToString();
                    lblBanner3.ImageUrl = "~//images/Banner/" + dt.Rows[2]["image"].ToString();
                    lblBanner4.ImageUrl = "~//images/Banner/" + dt.Rows[3]["image"].ToString();
                    lblBanner5.ImageUrl = "~//images/Banner/" + dt.Rows[4]["image"].ToString();
                    lbltitle1.Text = dt.Rows[0]["title"].ToString();
                    lbltitle2.Text = dt.Rows[1]["title"].ToString();
                    lbltitle3.Text = dt.Rows[2]["title"].ToString();
                    lbltitle4.Text = dt.Rows[3]["title"].ToString();
                    lbltitle5.Text = dt.Rows[4]["title"].ToString();
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
        private void BindDuffleBag()
        {
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_new_home_product_new(1, null);
            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];
                rptHandBag.DataSource = dt;
                rptHandBag.DataBind();
            }
        }
        //Comment by Chandni
        //protected void rptHandBag_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
        //        long ProductId = Convert.ToInt64(proctId.Value);
        //        AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

        //        review_handler reviewHandler = new review_handler();
        //        DataSet ds = reviewHandler.get_product_review(ProductId, 3);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
        //            }
        //        }
        //    }
        //}
        //Comment by Chandni

        private void BindHandBag()
        {
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_new_home_product_new(1002, null);
            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];
                rptDuffleBag.DataSource = dt;
                rptDuffleBag.DataBind();
            }
        }
        //Comment by Chandni
        //protected void rptDuffleBag_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
        //        long ProductId = Convert.ToInt64(proctId.Value);
        //        AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

        //        review_handler reviewHandler = new review_handler();
        //        DataSet ds = reviewHandler.get_product_review(ProductId, 3);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
        //            }
        //        }

        //    }
        //}
        //Comment by Chandni
        private void BindLaptopBag()
        {
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_new_home_product_new(2002, null);
            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];
                rptLaptopBag.DataSource = dt;
                rptLaptopBag.DataBind();
            }
        }
        //Comment by Chandni
        //protected void rptLaptopBag_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
        //        long ProductId = Convert.ToInt64(proctId.Value);

        //        AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

        //        review_handler reviewHandler = new review_handler();
        //        DataSet ds = reviewHandler.get_product_review(ProductId, 3);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
        //            }
        //        }

        //    }
        //}
        //Comment by Chandni
        //---------------BindOverNightBag------------//
        //private void BindOverNightBag()
        //{
        //    product_handler productHandler = new product_handler();
        //    DataSet dsProducts = productHandler.get_new_home_product(3);
        //    if (dsProducts != null && dsProducts.Tables.Count > 0)
        //    {
        //        DataTable dt = dsProducts.Tables[0];
        //        rptovernightBag.DataSource = dt;
        //        rptovernightBag.DataBind();
        //    }
        //}

        //protected void rptovernightBag_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
        //        long ProductId = Convert.ToInt64(proctId.Value);
        //        AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

        //        review_handler reviewHandler = new review_handler();
        //        DataSet ds = reviewHandler.get_product_review(ProductId, 3);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
        //            }
        //        }

        //    }
        //}
        //---------------BindOverNightBag------------//

        //---------------BindDuffle------------------//
        //private void BindDuffle()
        //{
        //    product_handler productHandler = new product_handler();
        //    //DataSet dsProducts = productHandler.get_latest_arriwals_product(1, 2, 27);  //local
        //    DataSet dsProducts = productHandler.get_latest_arriwals_product(1, 2, 2);
        //    if (dsProducts != null && dsProducts.Tables.Count > 0)
        //    {
        //        DataTable dtDuffleOne = dsProducts.Tables[0];
        //        if (dtDuffleOne.Rows.Count > 0)
        //        {
        //            rptduffleone.DataSource = dtDuffleOne;
        //            rptduffleone.DataBind();
        //        }
        //        DataTable dtDuffleOther = dsProducts.Tables[1];
        //        if (dtDuffleOther.Rows.Count > 0)
        //        {
        //            rptduffleOther.DataSource = dtDuffleOther;
        //            rptduffleOther.DataBind();
        //        }

        //    }
        //}

        //---------------BindDuffle------------------//
        protected void rptduffleone_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // bool stock = false;
            // decimal discount;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //HtmlGenericControl divOutofStock = e.Item.FindControl("divOutofStock") as HtmlGenericControl;
                //HtmlGenericControl thumstock = e.Item.FindControl("thumbbestseller") as HtmlGenericControl;
                //HiddenField instock = (HiddenField)e.Item.FindControl("hfieldLatestProduct");
                //stock = Convert.ToBoolean(instock.Value);

                //if (stock == true)
                //{
                //    thumstock.Attributes.Add("class", "product-thumbnail");
                //    divOutofStock.Visible = false;
                //    divOutofStock.Attributes.Add("class", "product-thumbnail");
                //}
                //if (stock == false)
                //{
                //    thumstock.Attributes.Add("class", "product-thumbnail out-of-stock");
                //    divOutofStock.Visible = true;
                //    divOutofStock.Attributes.Add("class", "btn-outofstock");
                //}

                //HtmlGenericControl spOldPrice_latest = e.Item.FindControl("spOldPrice_latest") as HtmlGenericControl;
                //HtmlGenericControl divdiscount = e.Item.FindControl("divdiscount") as HtmlGenericControl;
                //HiddenField Hdiscount = (HiddenField)e.Item.FindControl("hfieldDiscount");
                //discount = Convert.ToDecimal(Hdiscount.Value);

                //if (discount == 0)
                //{
                //    divdiscount.Visible = false;
                //    spOldPrice_latest.Visible = false;
                //}
                //else
                //{
                //    divdiscount.Visible = true;
                //    spOldPrice_latest.Visible = true;
                //}



                HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
                long ProductId = Convert.ToInt64(proctId.Value);
                AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

                review_handler reviewHandler = new review_handler();
                DataSet ds = reviewHandler.get_product_review(ProductId, 3);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
                        //lbltxt.Text = dt.Rows.Count + "user(s) have rated this article";
                    }
                }


                LinkButton lbtnAddWishlist1 = (LinkButton)e.Item.FindControl("lbtnAddWishlist1");

            }
        }
        protected void rptduffleOther_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
                long ProductId = Convert.ToInt64(proctId.Value);
                AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

                review_handler reviewHandler = new review_handler();
                DataSet ds = reviewHandler.get_product_review(ProductId, 3);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
                    }
                }


                LinkButton lbtnAddWishlist1 = (LinkButton)e.Item.FindControl("lbtnAddWishlist1");

            }
        }

        //private void BindLaptop()
        //{
        //    product_handler productHandler = new product_handler();
        //    DataSet dsProducts = productHandler.get_latest_arriwals_product(1, 1, 1);
        //    if (dsProducts != null && dsProducts.Tables.Count > 0)
        //    {
        //        DataTable dtLaptopOne = dsProducts.Tables[0];
        //        if (dtLaptopOne.Rows.Count > 0)
        //        {
        //            rptLaptopOne.DataSource = dtLaptopOne;
        //            rptLaptopOne.DataBind();
        //        }
        //        DataTable dtLaptopOther = dsProducts.Tables[1];
        //        if (dtLaptopOther.Rows.Count > 0)
        //        {
        //            rptLaptopOther.DataSource = dtLaptopOther;
        //            rptLaptopOther.DataBind();
        //        }

        //    }
        //}

        protected void rptLaptopOne_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
                long ProductId = Convert.ToInt64(proctId.Value);
                AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

                review_handler reviewHandler = new review_handler();
                DataSet ds = reviewHandler.get_product_review(ProductId, 3);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
                    }
                }

            }
        }

        protected void rptLaptopOther_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
                long ProductId = Convert.ToInt64(proctId.Value);
                AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

                review_handler reviewHandler = new review_handler();
                DataSet ds = reviewHandler.get_product_review(ProductId, 3);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
                    }
                }

            }
        }

        //private void BindOverNight()
        //{
        //    product_handler productHandler = new product_handler();
        //    // DataSet dsProducts = productHandler.get_latest_arriwals_product(1, 3, 1); //local
        //    DataSet dsProducts = productHandler.get_latest_arriwals_product(1, 3, 11);
        //    if (dsProducts != null && dsProducts.Tables.Count > 0)
        //    {
        //        DataTable dtOverNight = dsProducts.Tables[0];
        //        if (dtOverNight.Rows.Count > 0)
        //        {
        //            rptovernightOne.DataSource = dtOverNight;
        //            rptovernightOne.DataBind();
        //        }
        //        DataTable dtdtOverNightOther = dsProducts.Tables[1];
        //        if (dtdtOverNightOther.Rows.Count > 0)
        //        {
        //            rptovernightOther.DataSource = dtdtOverNightOther;
        //            rptovernightOther.DataBind();
        //        }

        //    }
        //}

        protected void rptovernightOne_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
                long ProductId = Convert.ToInt64(proctId.Value);
                AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

                review_handler reviewHandler = new review_handler();
                DataSet ds = reviewHandler.get_product_review(ProductId, 3);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
                    }
                }

            }
        }

        protected void rptovernightOther_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
                long ProductId = Convert.ToInt64(proctId.Value);
                AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

                review_handler reviewHandler = new review_handler();
                DataSet ds = reviewHandler.get_product_review(ProductId, 3);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
                    }
                }

            }
        }

        //private void BindAccessories()
        //{
        //    product_handler productHandler = new product_handler();
        //    //DataSet dsProducts = productHandler.get_latest_arriwals_product(3, 13, 0);  //local
        //    DataSet dsProducts = productHandler.get_latest_arriwals_product(3, 18, 34);
        //    if (dsProducts != null && dsProducts.Tables.Count > 0)
        //    {
        //        DataTable dtAccessoriesOne = dsProducts.Tables[0];
        //        if (dtAccessoriesOne.Rows.Count > 0)
        //        {
        //            rptaccessoriesOne.DataSource = dtAccessoriesOne;
        //            rptaccessoriesOne.DataBind();
        //        }
        //        DataTable dtAccessoriesOther = dsProducts.Tables[1];
        //        if (dtAccessoriesOther.Rows.Count > 0)
        //        {
        //            rptaccessoriesOther.DataSource = dtAccessoriesOther;
        //            rptaccessoriesOther.DataBind();
        //        }

        //    }
        //}

        protected void rptaccessoriesOne_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
                long ProductId = Convert.ToInt64(proctId.Value);
                AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

                review_handler reviewHandler = new review_handler();
                DataSet ds = reviewHandler.get_product_review(ProductId, 3);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
                    }
                }

            }
        }

        protected void rptaccessoriesOther_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
                long ProductId = Convert.ToInt64(proctId.Value);
                AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

                review_handler reviewHandler = new review_handler();
                DataSet ds = reviewHandler.get_product_review(ProductId, 3);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
                    }
                }

            }
        }



        //protected void rptLatestArrivals_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
        //        long ProductId = Convert.ToInt64(proctId.Value);
        //        AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

        //        review_handler reviewHandler = new review_handler();
        //        DataSet ds = reviewHandler.get_product_review(ProductId, 3);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
        //            }
        //        }
        //    }
        //}

        private void GetLatestArrivals()
        {
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_new_home_product_new(null, null);
            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];
                rptLatestArrivals.DataSource = dt;
                rptLatestArrivals.DataBind();
            }
        }
        //private void GetLatestArrivalsee()
        //{
        //    product_handler productHandler = new product_handler();
        //    DataSet dsProducts = productHandler.get_new_home_product_new(null, null);
        //    rptLatestArrivals.DataSource = dsProducts.Tables[0];
        //    rptLatestArrivals.DataBind();
        //}
        protected void rptLatestArrivals_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == "addtocart")
            //{
            //    product_id = Convert.ToInt64(e.CommandArgument.ToString());
            //    AddToShoppingCart();
            //    product_id = 0;
            //}
            // Chandni Added this code
            if (e.CommandName == "latest")
            {
                GetLatestArrivals();
                product_id = Convert.ToInt64(e.CommandArgument.ToString());
                product_handler productHandler = new product_handler();
                DataSet dsProducts = productHandler.get_product_details(product_id);
                if (dsProducts != null && dsProducts.Tables.Count > 0)
                {
                    DataTable dt = dsProducts.Tables[0];

                    ViewState["ProductDetails"] = dt;
                    lblProductName.Text = dt.Rows[0]["product_name"].ToString();
                    lblPDMeterial.Text = dt.Rows[0]["material_name"].ToString();
                    lblPDSize.Text = dt.Rows[0]["size"].ToString();
                    lblPDColor.Text = dt.Rows[0]["color_name"].ToString();
                    //lblDiscPrice.Text = dt.Rows[0]["price"].ToString();
                    lblSalePrice.Text = dt.Rows[0]["sale_price"].ToString();
                    lblSortDesc.Text = dt.Rows[0]["short_description"].ToString();
                    smid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

                    DataTable dtCart = (DataTable)Session["Cart"];
                    long matchid = 0;
                    long proid = 0;
                    int stock = 0;

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
                                //btnbuynow.Enabled = true;
                                break;
                            }
                            else
                            {
                                stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
                                if (stock <= 0)
                                {

                                    btnAddToCart.Enabled = false;
                                    btnAddToCart.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                                    //btnbuynow.CssClass = "btn btn--lg btn--black";
                                    //btnCustomBug.Enabled = false;
                                    lblStock.Visible = true;
                                    lblStock.Text = "Out of Stock";
                                    // divaleadyadd.Visible = false;
                                }
                                else
                                {

                                    if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                                    {
                                        //btnCustomBug.Enabled = false;
                                    }
                                    else
                                    {
                                        //btnCustomBug.Enabled = true;
                                    }
                                    btnAddToCart.Enabled = true;
                                    //btnbuynow.Enabled = true;
                                    lblStock.Visible = true;
                                    lblStock.Text = "In Stock";
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
                            //btnbuynow.Enabled = false;
                            // btnCustomBug.Enabled = false;
                            lblStock.Visible = true;
                            // divaleadyadd.Visible = false;
                        }
                        else
                        {
                            if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                            {
                                //btnCustomBug.Enabled = false;
                            }
                            else
                            {
                                // btnCustomBug.Enabled = true;
                            }

                            btnAddToCart.Enabled = true;
                            // btnbuynow.Enabled = true;

                            lblStock.Visible = true;
                            lblStock.Text = "In Stock";

                            // divaleadyadd.Visible = false;

                        }
                    }
                    DataTable dtimgZoom = dsProducts.Tables[1];
                    if (dtimgZoom.Rows.Count > 0)
                    {
                        rptimgZoomLarge.Visible = true;
                        rptimgZoomLarge.DataSource = dtimgZoom;
                        rptimgZoomLarge.DataBind();
                    }

                    BindReviews(product_id);
                }
                // Chandni Added this code
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
            }
        }
        private void BindReviews(long review_id)
        {
            int totalReview = 0;
            review_handler reviewHandler = new review_handler();
            DataSet ds = reviewHandler.get_product_review(review_id, 3);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //Rvp_ShowReview.DataSource = dt;
                    //Rvp_ShowReview.DataBind();

                    //ratingShow.Visible = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalReview += Convert.ToInt32(dt.Rows[i]["rating"].ToString());
                    }

                    float average = Convert.ToSingle(totalReview) / Convert.ToSingle(dt.Rows.Count);
                    // ratingShow.CurrentRating = Convert.ToInt16(Math.Ceiling(average));
                    hfProId.Value = Convert.ToInt32(average).ToString();
                    //lblReview.Text = average.ToString("0.00") + " | " + dt.Rows.Count + " Customer Review(s)";
                    litReviewHead.Text = "View Review";
                }
                else
                {
                    //ratingShow.Visible = false;
                    litReviewHead.Text = "No Review";
                }
            }
        }
        //All Product



        //--------------BindBlog----------//
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
        //--------------BindBlog----------//


        //private void BindProductReview()
        //{
        //    review_handler reviewHandler = new review_handler();
        //    DataSet ds = reviewHandler.get_product_review(0, 4);
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        rptReview.DataSource = dt;
        //        rptReview.DataBind();
        //    }
        //    else
        //    {
        //        rptReview.DataSource = null;
        //        rptReview.DataBind();
        //    }
        //}
        //---------------BindProductReview-----------//
        #region Cart

        //protected void dlCart_ItemDataCommond(object source, RepeaterCommandEventArgs e)
        //{
        //    int qty = 1;
        //    int txtqty = 1;
        //    string proname = "";
        //    lblQtyMsg.Visible = false;

        //    if (e.CommandName == "add" || e.CommandName == "minus")
        //    {
        //        TextBox txtQty = (TextBox)e.Item.FindControl("txtquantity");
        //        if (e.CommandName == "add")
        //            txtQty.Text = (Convert.ToInt16(txtQty.Text) + 1).ToString();
        //        else
        //            txtQty.Text = (Convert.ToInt16(txtQty.Text) - 1).ToString();

        //        if (Convert.ToInt32(txtQty.Text) <= 0)
        //        {
        //            txtQty.Text = "1";
        //            lblQtyMsg.Text = "Hi, Quantity value must be more then zero.";
        //            lblQtyMsg.Visible = true;
        //            return;
        //        }

        //        int ProductID = Convert.ToInt32(e.CommandArgument);
        //        DataTable dtCart = (DataTable)Session["Cart"];
        //        foreach (DataRow row in dtCart.Rows)
        //        {
        //            Session["qty"] = Convert.ToInt32(txtQty.Text);
        //            if (int.Parse(row["product_id"].ToString()) == ProductID)
        //            {

        //                row["quantity"] = Convert.ToInt32(txtQty.Text);

        //                Decimal Price = Convert.ToDecimal(row["sale_price"]);
        //                Decimal discount = Convert.ToDecimal(row["discount"]);
        //                Decimal UnitPriceOnDiscount = discount > 0 ? Price - (Price * discount / 100) : Price;
        //                product_handler productsHandler = new product_handler();
        //                DataSet dsqty = productsHandler.get_search_quantity(ProductID);
        //                if (dsqty != null && dsqty.Tables.Count > 0)
        //                {
        //                    DataTable dtqty = dsqty.Tables[0];
        //                    if (dtqty.Rows.Count > 0)
        //                    {
        //                        qty = Convert.ToInt32(dtqty.Rows[0]["quantity"].ToString());
        //                        txtqty = Convert.ToInt32(txtQty.Text);
        //                        proname = dtqty.Rows[0]["product_name"].ToString();

        //                        if (qty >= txtqty)
        //                        {
        //                            row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
        //                            Session["Cart"] = dtCart;
        //                            AddToShoppingCart();
        //                            lblQtyMsg.Text = "";
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            row["quantity"] = qty;
        //                            row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
        //                            Session["Cart"] = dtCart;
        //                            AddToShoppingCart();
        //                            lblQtyMsg.Text = "Hi, only <b>" + qty + "</b> quantity of <b>" + proname + "</b> is available at this time.";
        //                            lblQtyMsg.Visible = true;
        //                            break;
        //                        }
        //                    }
        //                    else
        //                    {

        //                    }
        //                }
        //                AddToShoppingCart();
        //            }
        //        }

        //    }
        //    else if (e.CommandName == "Remove")
        //    {
        //        if (Session["Cart"] != null)
        //        {
        //            DataTable dt = (DataTable)Session["Cart"];
        //            if (dt.Rows.Count > 0)
        //                dt.Rows.RemoveAt(e.Item.ItemIndex);

        //            Session["Cart"] = dt;
        //            AddToShoppingCart();
        //        }
        //    }

        //    // divCart.Attributes.Add("class", "speed-in");
        //}

        protected void dlCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hf = e.Item.FindControl("hfImgUrl") as HiddenField;
            if (hf != null)
            {
                string val = hf.Value;
                Image img = e.Item.FindControl("Image1") as Image;
                img.ImageUrl = "~/images/Product/Thumb/" + val.Substring(val.LastIndexOf('/'));
                //img.ImageUrl = "~/image" + val + ".jpg";
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            this.AddToShoppingCart();
            Response.Redirect("~/cart.aspx?pid=" + product_id.ToString());
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

            //foreach (DataRow row in dtCart.Rows)
            //{
            //    if (int.Parse(row["product_id"].ToString()) == product_id)
            //    {
            //        row["quantity"] = (int)row["quantity"] + 1;

            //        Decimal price = Convert.ToDecimal(row["sale_price"]);
            //        Decimal discount = Convert.ToDecimal(row["discount"]);
            //        Decimal UnitPriceOnDiscount = discount > 0 ? price - (price * discount / 100) : price;

            //        row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
            //        Session["Cart"] = dtCart;
            //        blnMatch = true;
            //        break;
            //    }
            //}
            //if (!blnMatch)
            //{
            //    if (product_id != 0)
            //    {
            //        product_handler productHandler = new product_handler();
            //        DataTable dt = productHandler.get_product_details(product_id).Tables[0];

            //        DataRow drCart = dtCart.NewRow();
            //        drCart["product_id"] = product_id;
            //        drCart["product_name"] = dt.Rows[0]["product_name"].ToString();
            //        drCart["thumb_image"] = "images/Product/Thumb/" + dt.Rows[0]["thumb_image"].ToString();
            //        drCart["menu_name"] = dt.Rows[0]["menu_name"].ToString();
            //        drCart["sub_menu_name"] = dt.Rows[0]["sub_menu_name"].ToString();
            //        drCart["child_name"] = dt.Rows[0]["child_name"].ToString();
            //        drCart["weight"] = dt.Rows[0]["weight"].ToString();
            //        drCart["gendertype"] = dt.Rows[0]["gendertype"].ToString();

            //        drCart["size"] = dt.Rows[0]["size"].ToString();

            //        if (Session["size"] != null)
            //        {
            //            drCart["size"] = Session["size"].ToString();
            //        }

            //        drCart["color_name"] = dt.Rows[0]["color_name"].ToString();
            //        Decimal price = Convert.ToDecimal(dt.Rows[0]["Price"].ToString());
            //        Decimal discount = Convert.ToDecimal(dt.Rows[0]["discount"].ToString());
            //        Decimal TotalPrice = discount > 0 ? price - (price * discount / 100) : price;

            //        Session["sale_discount"] = String.Format("{0:0.00}", (price - TotalPrice));

            //        drCart["sale_price"] = price;
            //        drCart["discount"] = discount;
            //        drCart["quantity"] = 1;
            //        drCart["Total"] = Convert.ToInt32(1) * TotalPrice;
            //        dtCart.Rows.Add(drCart);
            //    }
            //    Session["Cart"] = dtCart;
            //}

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
        //protected void lbtnApply_Click(object sender, EventArgs e)
        //{
        //    long proId = Convert.ToInt64(Session["product_id"]);
        //    long catid = 0;
        //    long subcatid = 0;

        //    product_handler productHandler = new product_handler();
        //    DataSet ds = productHandler.get_search_productId(proId);
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            catid = Convert.ToInt64(dt.Rows[0]["menu_id"].ToString());
        //            subcatid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());
        //        }
        //    }
        //    DataSet dscpn = new DataSet();
        //    dscpn = productHandler.get_coupon_code(txtCouponCode.Text, catid, subcatid, 0);
        //    if (dscpn != null && dscpn.Tables.Count > 0)
        //    {
        //        DataTable dt = dscpn.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            long cId = Convert.ToInt64(dt.Rows[0]["menu_id"].ToString());
        //            long SbId = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

        //            string SndEmail = dt.Rows[0]["sender_email"].ToString();
        //            string RecEmail = dt.Rows[0]["reciever_email"].ToString();

        //            Session["couponCodeName"] = txtCouponCode.Text;


        //            if (RecEmail != null || RecEmail != "")///////////////////////refer friends
        //            {
        //                Session["sndemail"] = SndEmail.ToString();
        //                Session["recemail"] = RecEmail.ToString();
        //            }
        //            else
        //            {
        //                Session["sndemail"] = null;
        //                Session["recemail"] = null;
        //            }


        //            if (cId == catid && SbId == subcatid)   //caregory wise coupon code
        //            {
        //                lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
        //                lblMsgCoupon.Text = "apply coupon code";
        //                Session["couponcodeprice"] = dt.Rows[0]["price"].ToString();
        //                AddToShoppingCart();
        //            }
        //            else if (cId == 0 && SbId == 0)         //all product used coupon code
        //            {
        //                lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
        //                lblMsgCoupon.Text = "apply coupon code";
        //                Session["couponcodeprice"] = dt.Rows[0]["price"].ToString();
        //                AddToShoppingCart();
        //            }
        //            else
        //            {
        //                lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
        //                lblMsgCoupon.Text = "Coupon Code is Invalid.";
        //                Session["couponcodeprice"] = null;
        //                AddToShoppingCart();
        //            }
        //        }
        //        else
        //        {
        //            lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
        //            lblMsgCoupon.Text = "Coupon Code is Invalid.";
        //            Session["couponcodeprice"] = null;
        //            AddToShoppingCart();
        //        }

        //        AddToShoppingCart();
        //    }

        //    divCart.Attributes.Add("class", "speed-in");
        //}

        #endregion

        //    #region login/logout
        //protected void btnLogin_Click(object sender, EventArgs e)
        //{
        //    customer_handler customerHandler = new customer_handler();
        //    security scty = new security();
        //    string strpassword = scty.Encryptdata(txtLoginPwd.Text);
        //    BusinessEntities.customer customerData = new BusinessEntities.customer();
        //    customerData.email_id = txtLoginEmail.Text.Trim();
        //    customerData.password = strpassword;
        //    //customerData.password = txtLoginPwd.Text;

        //    if (Page.IsValid)
        //    {
        //        int retFlag = customerHandler.get_customer_login(customerData);
        //        if (retFlag == 0)
        //        {
        //            lblLoginMsg.Text = "Login Failed, invalid UserId/Password";
        //            txtLoginPwd.Focus();

        //            HtmlControl control = FindControl("loginfade") as HtmlControl;
        //            control.Attributes["class"] = "black_overlay";
        //            loginfade.Style["display"] = "block";
        //            login.Style["display"] = "block";

        //            signupfade.Style["display"] = "none";
        //            signup.Style["display"] = "none";

        //            forgotPasswordfade.Style["display"] = "none";
        //            forgotopen.Style["display"] = "none";

        //            myaccount.Visible = false;
        //            loginid.Visible = true;
        //            signout.Visible = true;
        //            logout.Visible = false;


        //            // liloginwishlist.Visible = true;
        //            //liwishlist.Visible = false;


        //        }
        //        else
        //        {
        //            string path = HttpContext.Current.Request.Url.AbsolutePath;
        //            string url = HttpContext.Current.Request.Url.PathAndQuery;

        //            Session["CustomerLoginDetails"] = txtLoginEmail.Text.Trim();
        //            Session["UserPassword"] = txtLoginPwd.Text;
        //            DataSet ds = customerHandler.check_customer_loginid(txtLoginEmail.Text);
        //            if (ds != null && ds.Tables.Count > 0)
        //            {
        //                DataTable dt = ds.Tables[0];
        //                if (dt.Rows.Count > 0)
        //                {
        //                    Session["CustomerLoginId"] = dt.Rows[0]["customer_id"].ToString();
        //                    lblLoginName.Text = "Welcome, " + dt.Rows[0]["customer_name"].ToString();
        //                    Session["CutomerName"] = dt.Rows[0]["customer_name"];
        //                }
        //                else
        //                {

        //                }
        //            }

        //            loginfade.Style["display"] = "none";
        //            login.Style["display"] = "none";

        //            forgotPasswordfade.Style["display"] = "none";
        //            forgotopen.Style["display"] = "none";

        //            signupfade.Style["display"] = "none";
        //            signup.Style["display"] = "none";

        //            myaccount.Visible = true;
        //            loginid.Visible = false;
        //            signout.Visible = false;
        //            logout.Visible = true;

        //            //liloginwishlist.Visible = false;
        //            //liwishlist.Visible = true;


        //        }
        //    }
        //}
        //protected void lbtnLogOut_Click(object sender, EventArgs e)
        //{
        //    Session.Clear();
        //    Session.Abandon();
        //    myaccount.Visible = false;
        //    loginid.Visible = true;
        //    signout.Visible = true;
        //    logout.Visible = false;

        //    //liloginwishlist.Visible = true;
        //    //liwishlist.Visible = false;
        //    Response.Redirect("/", false);
        //}
        // #endregion

        // #region forgotpassword
        //protected void btnForgotPassword_Click(object sender, EventArgs e)
        //{
        //    security scty = new security();
        //    string uniqueCode = string.Empty;
        //    uniqueCode = Convert.ToString(System.Guid.NewGuid());
        //    customer_handler forgetpassword = new customer_handler();
        //    BusinessEntities.customer Customer = new BusinessEntities.customer();

        //    Customer.email_id = txtForgotEmailId.Text;

        //    int retflag = forgetpassword.check_customer_emailid(Customer);
        //    if (retflag == 1)
        //    {
        //        DataSet ds = forgetpassword.check_customer_loginid(txtForgotEmailId.Text);
        //        if (ds.Tables[0] != null)
        //        {
        //            DataTable dt = ds.Tables[0];

        //            if (dt.Rows.Count > 0)
        //            {
        //                string pass = dt.Rows[0]["password"].ToString();
        //                string user = dt.Rows[0]["customer_name"].ToString();

        //                string strpassword = scty.Decryptdata(pass);

        //                try
        //                {
        //                    //MailMessage mail = new MailMessage();
        //                    //mail.BodyFormat = MailFormat.Html;
        //                    //mail.Priority = MailPriority.High;
        //                    //mail.BodyEncoding = Encoding.UTF8;
        //                    //mail.To = txtForgotEmailId.Text.Trim();
        //                    //mail.Cc = "";
        //                    //mail.From = txtForgotEmailId.Text.Trim();
        //                    //mail.Subject = "Password Recovery";
        //                    //string msgbody1;
        //                    //msgbody1 = "<tr><td>";
        //                    //msgbody1 += "<table border='0' cellpadding='0' cellspacing='0' style='width: 86%;'>";
        //                    //msgbody1 += "<tr><td>";
        //                    //msgbody1 += "</td>";
        //                    //msgbody1 += "<td>" + "Your password has been reset,  " + user + "<br /><br />Your new password is : " + strpassword + "<br /><br /> " + "<a href=" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "> <img src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/logo.png' /></a>" + "<br /><br />ThankYou !</td></tr>";
        //                    //msgbody1 += "</table></td></tr>";
        //                    //msgbody1 += "<tr><td></td></tr></table></td></tr>";
        //                    //msgbody1 += "</table>";
        //                    //msgbody1 += "</td></tr>";
        //                    //mail.Body = msgbody1;

        //                    //string smtpEmail = "happiness@shoptosurprise.com";
        //                    //string smtpPassword = "vishu0894";
        //                    //string smtpAddress = "smtp.gmail.com";
        //                    //string smtpPort = "465";
        //                    //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
        //                    //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpAddress);
        //                    //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", smtpPort);
        //                    //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
        //                    //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", smtpEmail);
        //                    //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", smtpPassword);
        //                    //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "True");
        //                    //SmtpMail.Send(mail);

        //                    //DAL.Utility.sendEmail("STRUTT - Password Recovery", msgbody1, "", "", txtForgotEmailId.Text.Trim(), null);
        //                    //lblStatus.Text = "Password is sent to you email id,you can now <a href='Default.aspx'>Login</a>";

        //                    DAL.Utility.SendForgotPasswordMail(user, strpassword, txtForgotEmailId.Text.Trim());
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Password is sent to you Email Address.');", true);
        //                }
        //                catch (Exception ex)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(" + ex + ");", true);
        //                }
        //            }
        //            else
        //            {
        //                //lblStatus.Text = "Please enter valid email address or username";
        //            }
        //        }

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please check you email id');", true);
        //    }
        //}
        //#endregion






        //protected void btnNewsletter_click(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    String p = generatecode();
        //    newsletter_handler NewsletterEmail = new newsletter_handler();
        //    BusinessEntities.newsletter Newsletter = new newsletter();

        //    Newsletter.email = txtNewletter.Text;
        //    Newsletter.url = p;
        //    Newsletter.code = Session["code"].ToString();
        //    Session["p"] = p;

        //    bool result = NewsletterEmail.insert_update_newsletter_footer(Newsletter);
        //    if (result)
        //    {
        //        txtNewletter.Text = "";
        //        lblNewsletterMsg.Text = "subscribed successfully.";
        //    }
        //    else
        //    {

        //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Response.Redirect("~/PageNotFound.aspx");
        //    //}
        //}

        #region getnerate code

        public string generatecode()
        {
            string numbers = "1234567890";
            string characters = numbers;
            characters += numbers;
            int length = 10;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            Session["code"] = otp;
            string M = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/" + "unsubscribe-" + otp + ".html";
            return M;
        }
        #endregion

        private void LoadMenuold()     //Unused
        {
            //menu_handler lordmenuHandler = new menu_handler();
            //DataTable dt = lordmenuHandler.get_menu_sub(0, 0, null).Tables[0];

            //DataColumn colGender = new DataColumn("gender", typeof(string));
            ////if (Request.QueryString["gid"] != null && Request.QueryString["gid"].Equals("1"))
            ////    colGender.DefaultValue = "men";
            ////else
            ////    colGender.DefaultValue = "women";

            ////colGender.DefaultValue = GenderType;
            //dt.Columns.Add(colGender);

            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "menu_id = 1 AND is_active=1";
            //rptMenu1.DataSource = dv;
            //rptMenu1.DataBind();
            ////dv.RowFilter = "menu_id = 2 AND is_active=1";
            ////rptMenu2.DataSource = dv;
            //// rptMenu2.DataBind();
            //dv.RowFilter = "menu_id = 1002 AND is_active=1"; //1002 On Live menu Id
            //rptMenu3.DataSource = dt;
            //rptMenu3.DataBind();
            ////dv.RowFilter = "menu_id = 4 AND is_active=1";
            //// rptMenu4.DataSource = dt;
            ////rptMenu4.DataBind();
        }
        private void LoadMenu()
        {
            menu_handler lordmenuHandler = new menu_handler();
            DataTable dt = lordmenuHandler.get_menu_sub(0, 0, gid).Tables[0];

            DataColumn colGender = new DataColumn("gender", typeof(string));
            //if (Request.QueryString["gid"] != null && Request.QueryString["gid"].Equals("1"))
            //    colGender.DefaultValue = "men";
            //else
            //    colGender.DefaultValue = "women";

            colGender.DefaultValue = GenderType;
            dt.Columns.Add(colGender);

            DataView dv = dt.DefaultView;
            dv.RowFilter = "menu_id = 2013 AND is_active=1";        // Kalpesh: Don't change this menu_id = 1 (This is Live database ID)
            //rptMenu1.DataSource = dv;
            //rptMenu1.DataBind();
            //rptMenuM1.DataSource = dv;
            //rptMenuM1.DataBind();

            dv.RowFilter = "menu_id = 2014 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2002 (This is Live database ID)
            rptMenu2.DataSource = dv;
            rptMenu2.DataBind();

            //rptMenuM2.DataSource = dv;
            //rptMenuM2.DataBind();

            dv.RowFilter = "menu_id = 2015 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2005 (This is Live database ID)
            rptMenu5.DataSource = dv;
            rptMenu5.DataBind();

            rptMenuM3.DataSource = dv;
            rptMenuM3.DataBind();

            dv.RowFilter = "menu_id = 2016 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2006 (This is Live database ID)
            rptMenu6.DataSource = dv;
            rptMenu6.DataBind();

            rptMenuM4.DataSource = dv;
            rptMenuM4.DataBind();

            dv.RowFilter = "menu_id = 2017 AND is_active=1";        // Kalpesh: Don't change this menu_id = 1002 (This is Live database ID)
            rptMenu3.DataSource = dt;
            rptMenu3.DataBind();


            rptMenuM5.DataSource = dt;
            rptMenuM5.DataBind();

            dv.RowFilter = "menu_id = 2018 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2004 (This is Live database ID)
            rptMenu4.DataSource = dt;
            rptMenu4.DataBind();

            rptMenuM6.DataSource = dt;
            rptMenuM6.DataBind();


            dv.RowFilter = "menu_id = 2019 AND is_active=1";
            rptMenu7.DataSource = dv;
            rptMenu7.DataBind();
            

            //dv.RowFilter = "menu_id = 4 AND is_active=1";
            //rptMenu4.DataSource = dt;
            //rptMenu4.DataBind();

            //dv.RowFilter = "menu_id = 3 AND is_active=1";
            //rptMenu3.DataSource = dt;
            //rptMenu3.DataBind();
        }
        // Chandni Added this code
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
        // Chandni Added this code
        protected void rptHandBag_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "qviewhand")
            {
                BindHandBag();
                product_id = Convert.ToInt64(e.CommandArgument.ToString());
                product_handler productHandler = new product_handler();
                DataSet dsProducts = productHandler.get_product_details(product_id);
                if (dsProducts != null && dsProducts.Tables.Count > 0)
                {
                    DataTable dt = dsProducts.Tables[0];

                    ViewState["ProductDetails"] = dt;
                    lblProductName.Text = dt.Rows[0]["product_name"].ToString();

                    //lblDiscPrice.Text = dt.Rows[0]["price"].ToString();
                    lblSalePrice.Text = dt.Rows[0]["sale_price"].ToString();
                    lblSortDesc.Text = dt.Rows[0]["short_description"].ToString();
                    smid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

                    DataTable dtCart = (DataTable)Session["Cart"];
                    long matchid = 0;
                    long proid = 0;
                    int stock = 0;
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
                                //btnbuynow.Enabled = true;
                                break;
                            }
                            else
                            {
                                stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
                                if (stock <= 0)
                                {

                                    btnAddToCart.Enabled = false;
                                    btnAddToCart.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                                    //btnbuynow.CssClass = "btn btn--lg btn--black";
                                    //btnCustomBug.Enabled = false;
                                    lblStock.Visible = true;
                                    lblStock.Text = "Out of Stock";
                                    // divaleadyadd.Visible = false;
                                }
                                else
                                {

                                    if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                                    {
                                        //btnCustomBug.Enabled = false;
                                    }
                                    else
                                    {
                                        //btnCustomBug.Enabled = true;
                                    }
                                    btnAddToCart.Enabled = true;
                                    //btnbuynow.Enabled = true;
                                    lblStock.Visible = true;
                                    lblStock.Text = "In Stock";
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
                            //btnbuynow.Enabled = false;
                            // btnCustomBug.Enabled = false;
                            lblStock.Visible = true;
                            // divaleadyadd.Visible = false;
                        }
                        else
                        {
                            if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                            {
                                //btnCustomBug.Enabled = false;
                            }
                            else
                            {
                                // btnCustomBug.Enabled = true;
                            }

                            btnAddToCart.Enabled = true;
                            // btnbuynow.Enabled = true;

                            lblStock.Visible = true;
                            lblStock.Text = "In Stock";

                            // divaleadyadd.Visible = false;

                        }
                    }
                    DataTable dtimgZoom = dsProducts.Tables[1];
                    if (dtimgZoom.Rows.Count > 0)
                    {
                        rptimgZoomLarge.Visible = true;
                        rptimgZoomLarge.DataSource = dtimgZoom;
                        rptimgZoomLarge.DataBind();
                    }
                    this.BindReviews(product_id);
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
            }
        }

        protected void rptDuffleBag_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "quickduffle")
            {
                BindDuffleBag();
                product_id = Convert.ToInt64(e.CommandArgument.ToString());
                product_handler productHandler = new product_handler();
                DataSet dsProducts = productHandler.get_product_details(product_id);
                if (dsProducts != null && dsProducts.Tables.Count > 0)
                {
                    DataTable dt = dsProducts.Tables[0];

                    ViewState["ProductDetails"] = dt;
                    lblProductName.Text = dt.Rows[0]["product_name"].ToString();

                    //lblDiscPrice.Text = dt.Rows[0]["price"].ToString();
                    lblSalePrice.Text = dt.Rows[0]["sale_price"].ToString();
                    lblSortDesc.Text = dt.Rows[0]["short_description"].ToString();
                    lblStock.Text = dt.Rows[0]["in_stock"].ToString();
                    smid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

                    DataTable dtCart = (DataTable)Session["Cart"];
                    long matchid = 0;
                    long proid = 0;
                    int stock = 0;
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
                                //btnbuynow.Enabled = true;
                                break;
                            }
                            else
                            {
                                stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
                                if (stock <= 0)
                                {

                                    btnAddToCart.Enabled = false;
                                    btnAddToCart.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                                    //btnbuynow.CssClass = "btn btn--lg btn--black";
                                    //btnCustomBug.Enabled = false;
                                    lblStock.Visible = true;
                                    lblStock.Text = "Out of Stock";
                                    // divaleadyadd.Visible = false;
                                }
                                else
                                {

                                    if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                                    {
                                        //btnCustomBug.Enabled = false;
                                    }
                                    else
                                    {
                                        //btnCustomBug.Enabled = true;
                                    }
                                    btnAddToCart.Enabled = true;
                                    //btnbuynow.Enabled = true;
                                    lblStock.Visible = true;
                                    lblStock.Text = "In Stock";
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
                            //btnbuynow.Enabled = false;
                            // btnCustomBug.Enabled = false;
                            lblStock.Visible = true;
                            // divaleadyadd.Visible = false;
                        }
                        else
                        {
                            if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                            {
                                //btnCustomBug.Enabled = false;
                            }
                            else
                            {
                                // btnCustomBug.Enabled = true;
                            }

                            btnAddToCart.Enabled = true;
                            // btnbuynow.Enabled = true;

                            lblStock.Visible = true;
                            lblStock.Text = "In Stock";

                            // divaleadyadd.Visible = false;

                        }
                    }
                    DataTable dtimgZoom = dsProducts.Tables[1];
                    if (dtimgZoom.Rows.Count > 0)
                    {
                        rptimgZoomLarge.Visible = true;
                        rptimgZoomLarge.DataSource = dtimgZoom;
                        rptimgZoomLarge.DataBind();
                    }
                }
                this.BindReviews(product_id);
            }
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        }

        protected void rptLaptopBag_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "quickleptop")
            {
                BindLaptopBag();
                product_id = Convert.ToInt64(e.CommandArgument.ToString());
                product_handler productHandler = new product_handler();
                DataSet dsProducts = productHandler.get_product_details(product_id);
                if (dsProducts != null && dsProducts.Tables.Count > 0)
                {
                    DataTable dt = dsProducts.Tables[0];

                    ViewState["ProductDetails"] = dt;
                    lblProductName.Text = dt.Rows[0]["product_name"].ToString();

                    //lblDiscPrice.Text = dt.Rows[0]["price"].ToString();
                    lblSalePrice.Text = dt.Rows[0]["sale_price"].ToString();
                    lblSortDesc.Text = dt.Rows[0]["short_description"].ToString();
                    smid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

                    DataTable dtCart = (DataTable)Session["Cart"];
                    long matchid = 0;
                    long proid = 0;
                    int stock = 0;
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
                                //btnbuynow.Enabled = true;
                                break;
                            }
                            else
                            {
                                stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
                                if (stock <= 0)
                                {

                                    btnAddToCart.Enabled = false;
                                    btnAddToCart.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
                                    //btnbuynow.CssClass = "btn btn--lg btn--black";
                                    //btnCustomBug.Enabled = false;
                                    lblStock.Visible = true;
                                    lblStock.Text = "Out of Stock";
                                    // divaleadyadd.Visible = false;
                                }
                                else
                                {

                                    if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                                    {
                                        //btnCustomBug.Enabled = false;
                                    }
                                    else
                                    {
                                        //btnCustomBug.Enabled = true;
                                    }
                                    btnAddToCart.Enabled = true;
                                    //btnbuynow.Enabled = true;
                                    lblStock.Visible = true;
                                    lblStock.Text = "In Stock";
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
                            //btnbuynow.Enabled = false;
                            // btnCustomBug.Enabled = false;
                            lblStock.Visible = true;
                            // divaleadyadd.Visible = false;
                        }
                        else
                        {
                            if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
                            {
                                //btnCustomBug.Enabled = false;
                            }
                            else
                            {
                                // btnCustomBug.Enabled = true;
                            }

                            btnAddToCart.Enabled = true;
                            // btnbuynow.Enabled = true;

                            lblStock.Visible = true;
                            lblStock.Text = "In Stock";

                            // divaleadyadd.Visible = false;

                        }
                    }
                    DataTable dtimgZoom = dsProducts.Tables[1];
                    if (dtimgZoom.Rows.Count > 0)
                    {
                        rptimgZoomLarge.Visible = true;
                        rptimgZoomLarge.DataSource = dtimgZoom;
                        rptimgZoomLarge.DataBind();
                    }
                    this.BindReviews(product_id);
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
            }
        }

        // Chandni Added this code
        //Show Popup Detail
        //protected void ShowPopup(object sender, EventArgs e)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        //}
        //Login Code added by Chandni
        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            customer_handler customerHandler = new customer_handler();
            string strpassword = security.Encryptdata(txtLoginPwd.Text);
            BusinessEntities.customer customerData = new BusinessEntities.customer();
            customerData.email_id = txtLoginEmail.Text.Trim();
            customerData.password = strpassword;

            if (Page.IsValid)
            {
                int retFlag = customerHandler.get_customer_login(customerData);
                if (retFlag == 0)
                {
                    lblLoginMsg.Text = "Login Failed, invalid UserId/Password";
                    txtLoginPwd.Focus();
                }
                else
                {
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    string url = HttpContext.Current.Request.Url.PathAndQuery;

                    Session["CustomerLoginDetails"] = txtLoginEmail.Text.Trim();
                    DataSet ds = customerHandler.check_customer_loginid(txtLoginEmail.Text);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            Session["CustomerLoginId"] = dt.Rows[0]["customer_id"].ToString();
                            Session["CutomerName"] = dt.Rows[0]["customer_name"];
                            Session["customerDetailsId"] = dt.Rows[0]["customer_id"].ToString();
                            Session["loginType"] = "register";
                            GetTempShoppingCart();
                        }
                    }
                    if (Request.QueryString["url"] == null)
                        Response.Redirect("~/category.aspx");
                    else
                        Response.Redirect(Request.QueryString["url"]);
                }
            }
        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Guid customer_id = Guid.Empty;
            customer_handler customerHandler = new customer_handler();
            customer Customer = new customer();

            string strpassword = security.Encryptdata(txtsignoutPassword.Text);



            Customer.customer_name = txtsignoutUserName.Text;
            Customer.email_id = txtsignoutEmail.Text;
            Customer.password = strpassword;
            Customer.contact_number = txtsignoutMobile.Text;

            bool result = customerHandler.insert_update_customer_login_details(Customer, ref customer_id);

            if (result)
            {


                Session["CustomerEmail"] = txtsignoutEmail.Text.Trim();
                Session["UserName"] = txtsignoutUserName.Text.Trim();
                Session["Mobile"] = txtsignoutMobile.Text;

                //// 5: Identify API - On new user registration.
                //string identifyapi = @"wigzo(""identify"", { email: " + Session["CustomerEmail"].ToString() + ",phone: " + Session["Mobile"].ToString() + ",fullName: " + Session["UserName"].ToString() + @" }); ";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "identifyapi", identifyapi, true);

                DataSet ds = customerHandler.check_customer_loginid(txtsignoutEmail.Text);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Session["CustomerLoginId"] = dt.Rows[0]["customer_id"].ToString();
                        Session["CutomerName"] = dt.Rows[0]["customer_name"];
                        Session["loginType"] = "register";
                    }
                }

                DAL.Utility.SendSignupMail(txtsignoutUserName.Text.Trim(), txtsignoutEmail.Text.Trim(), txtsignoutPassword.Text.Trim(), txtsignoutEmail.Text.Trim());

                if (Request.QueryString["url"] == null)
                    Response.Redirect("~/category.aspx");
                else
                    Response.Redirect(Request.QueryString["url"]);
            }
        }
        protected void btnFbGoogle_Click(object sender, EventArgs e)
        {
            //security scty = new security();
            customer_handler customerHandler = new customer_handler();
            BusinessEntities.customer customer = new BusinessEntities.customer();

            customer.customer_name = hfpersonaName.Value;
            if (string.IsNullOrEmpty(hfreceiveremail.Value))
                customer.email_id = hffbid.Value;
            else
                customer.email_id = hfreceiveremail.Value;

            DataSet ds = customerHandler.check_guest_loginid(customer.email_id);
            bool result = false;
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    customer.customer_id = Guid.Parse(dt.Rows[0]["customer_id"].ToString());

                    Session["CustomerLoginDetails"] = customer.email_id;
                    Session["customerDetailsId"] = customer.customer_id.ToString();
                    Session["loginType"] = hfLoginType.Value;
                    bindDeliveryDetails(Session["CustomerLoginDetails"].ToString());
                    BillingDetails();
                }
                else
                {
                    Guid customer_id = Guid.NewGuid();
                    customer.customer_id = customer_id;

                    if (hfLoginType.Value.Equals("fb"))
                        result = customerHandler.insert_update_facebook_customer(customer, ref customer_id);
                    else if (hfLoginType.Value.Equals("google"))
                        result = customerHandler.insert_update_google_customer(customer, ref customer_id);

                    if (result)
                    {
                        Session["CustomerLoginDetails"] = customer.email_id;
                        Session["customerDetailsId"] = customer_id.ToString();
                        Session["loginType"] = hfLoginType.Value;

                        bindDeliveryDetails(Session["CustomerLoginDetails"].ToString());
                        BillingDetails();
                    }
                }
            }

            if (Request.QueryString["url"] == null)
                Response.Redirect("~/category.aspx");
            else
                Response.Redirect(Request.QueryString["url"]);
        }
        protected void lbtnForgotPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLoginEmail.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter email address.";
                return;
            }
            customer_handler customerHandler = new customer_handler();
            customer Customer = new customer();
            string loginType;

            DataSet ds = customerHandler.get_customer_login_details(txtLoginEmail.Text.Trim());

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    // loginType = item["login_type"].ToString();
                    loginType = ds.Tables[0].Rows[0]["login_type"].ToString();

                    if (loginType == "register")
                    {
                        string pwd = security.Decryptdata(ds.Tables[0].Rows[0]["password"].ToString());

                        DAL.Utility.SendForgotPasswordMail(txtLoginEmail.Text, pwd, ds.Tables[0].Rows[0]["email_id"].ToString());

                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Account detail sent you on your email address. Please verify.";
                        return;
                    }
                }

                loginType = ds.Tables[0].Rows[0]["login_type"].ToString();
                if (loginType == "fb")
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Your login type is Facebook user. Please login with Facebook account.";
                }
                else if (loginType == "google")
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Your login type is google user. Please login with Google account.";
                }
                else if (loginType == "guest")
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Your login type is guest user. Please register before login.";
                }
            }
        }
        private bool GetTempShoppingCart()
        {
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
            temp_cart_handler productIDHandler = new temp_cart_handler();
            DataSet dsProductIds = productIDHandler.get_temp_customer_cart(txtLoginEmail.Text.Trim());
            int product_id;

            if (dsProductIds != null && dsProductIds.Tables.Count > 0)
            {
                DataTable dt = dsProductIds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    product_id = int.Parse(dt.Rows[i]["product_id"].ToString());
                    BindProduct(product_id);
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
                            DataRow drCart = dtCart.NewRow();
                            DataTable dtt = (DataTable)ViewState["ProductDetails"];
                            drCart["product_id"] = product_id;
                            drCart["product_name"] = dtt.Rows[0]["product_name"].ToString();
                            drCart["thumb_image"] = "images/Product/Thumb/" + dtt.Rows[0]["thumb_image"].ToString();
                            drCart["menu_name"] = dtt.Rows[0]["menu_name"].ToString();
                            drCart["sub_menu_name"] = dtt.Rows[0]["sub_menu_name"].ToString();
                            drCart["child_name"] = dtt.Rows[0]["child_name"].ToString();
                            drCart["weight"] = dtt.Rows[0]["weight"].ToString();
                            drCart["gendertype"] = dtt.Rows[0]["gendertype"];

                            drCart["size"] = dtt.Rows[0]["size"].ToString();

                            if (Session["size"] != null)
                            {
                                drCart["size"] = Session["size"].ToString();
                            }

                            drCart["color_name"] = dtt.Rows[0]["color_name"].ToString();
                            Decimal price = Convert.ToDecimal(dtt.Rows[0]["Price"].ToString());
                            Decimal discount = Convert.ToDecimal(dtt.Rows[0]["discount"].ToString());
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
                    }
                }
                Session["Cart"] = dtCart;
            }
            return true;
        }
        private void bindDeliveryDetails(string CustomerDataEmailId)
        {
            customer_handler customerHandler = new customer_handler();
            DataSet ds = customerHandler.get_customer_login_details(CustomerDataEmailId);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Session["customerDetailsId"] = dt.Rows[0]["customer_id"].ToString();
                    Session["CutomerName"] = dt.Rows[0]["customer_name"].ToString();
                    hfpersonaName.Value = dt.Rows[0]["customer_name"].ToString();
                    hfpersonamobile.Value = dt.Rows[0]["contact_number"].ToString();
                    //txtreceiveremail.Text = dt.Rows[0]["email_id"].ToString();
                    //hfreceiveremail.Value = dt.Rows[0]["email_id"].ToString();
                    //txtpersonaldetailsName.Text = dt.Rows[0]["customer_name"].ToString();
                    //txtpersonalPhone.Text = dt.Rows[0]["contact_number"].ToString();
                    //txtpersonalEmail.Text = dt.Rows[0]["email_id"].ToString();
                    //this.PanelLogin();

                    //lbl1Login.Text = "Signed in as " + dt.Rows[0]["email_id"].ToString();
                }
            }
        }
        protected void BillingDetails()
        {
            bool result = false;
            Guid customer_id = Guid.Empty;
            //if (Session["cusDetailsId"] != null)
            if (Session["customerDetailsId"] != null)
            {
                //customer_id = Guid.Parse(Session["cusDetailsId"].ToString());
                customer_id = Guid.Parse(Session["customerDetailsId"].ToString());
            }
            //security scty = new security();
            customer_handler customerHandler = new customer_handler();
            BusinessEntities.customer customer = new BusinessEntities.customer();

            customer.email_id = Session["CustomerLoginDetails"].ToString();
            customer.customer_name = hfpersonaName.Value;
            customer.contact_number = hfpersonamobile.Value;

            result = customerHandler.update_guest_customer(customer, ref customer_id);
            if (result)
            {
                if (Session["CustomerData"] != null)
                {
                    DataTable dtCustomerData = (DataTable)Session["CustomerData"];
                    DataRow drCustomerData = dtCustomerData.NewRow();
                    drCustomerData["customer_id"] = Guid.Parse(customer_id.ToString());
                    Session["CustomerLoginDetails"] = customer.email_id;
                    drCustomerData["full_name"] = customer.customer_name;
                    Session["CutomerName"] = customer.customer_name;
                    drCustomerData["contact_number"] = customer.contact_number;
                    dtCustomerData.Rows.Add(drCustomerData);

                    Session["CustomerData"] = dtCustomerData;

                    //PanelLogin(); //close Pick The Most Convenient Option and open Billing Details.
                    //this.GetBillingDetails();
                }
            }
            else
            {
                //lblSignupMsg.Text = "This email/mobile is already registered, please choose another one.";
            }
            //Billing();   //close Billing Details and open Delivery Details.
            //PanelAddress();
            //BindReceiverAddressData(Guid.Parse(Session["customerDetailsId"].ToString()));
        }
        private void BindProduct(int product_id)
        {
            product_handler productHandler = new product_handler();
            DataSet dsProducts = productHandler.get_product_details(product_id);
            if (dsProducts != null && dsProducts.Tables.Count > 0)
            {
                DataTable dt = dsProducts.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ViewState["ProductDetails"] = dt;
                }
                DataTable dtCart = (DataTable)Session["Cart"];
            }
        }
        //Login code added by chandni
        protected void lbtngo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtname.Text))
                Response.Redirect("~/category.aspx?s=" + txtname.Text.Trim());
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
        private void getText()
        {
            try
            {
                DataTable dt = (DataTable)Session["PageLabel"];
                if (dt.Rows.Count > 0)
                {
                    lblHomeText.Text = dt.Select("label_name='Home Text'")[0]["label_value"].ToString();
                    lblMarquee1.Text = dt.Select("label_name='Marquee1'")[0]["label_value"].ToString();
                    lblMarquee2.Text = dt.Select("label_name='Marquee2'")[0]["label_value"].ToString();
                }
            }
            catch (Exception)
            {
                //If any error then ignore
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
    }
}



