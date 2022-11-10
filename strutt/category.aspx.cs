using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using BLL;
using BusinessEntities;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using System.Web.Services;
using System.Collections;

namespace strutt
{
    public partial class category : System.Web.UI.Page
    {
        public string GenderType { get; set; }

        #region value type
        string naviName = "";
        private int PageSize = 16;
        int PageIndex = 1;
        int RecordCount = 1;
        int totcount = 0;
        //bool flag = true;
        int status = 0;
        //bool is_active = true;
        //bool is_default = true;
        int mnId = 0;
        int smnId = 0;
        int cmnId = 0;
        //int Flag = 0;
        //int totalReview = 0;
        byte? gid = null;
        decimal minValue = 100;
        decimal maxValue = 50000;
        #endregion
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
        #region load event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                amountFrom.Value = "100";
                amountTo.Value = "50000";

                //if (Request.QueryString["mnid"] != null && Request.QueryString["smnid"] != null)
                //{
                //    Flag = 1;
                //    this.BindSubDetailsProducts(Flag, 1);
                //}
                //else if (Request.QueryString["mnid"] != null)
                //{
                //    Flag = 0;
                //    this.BindSubDetailsProducts(Flag, 1);
                //}
                //else
                //{
                //    Flag = 0;
                //    this.BindSubDetailsProducts(Flag, 1);
                //}


                //this.BindLatestProduct();

                if (Request.QueryString["sales"] != null)
                {
                    //divSubMenu.Visible = true;
                    hlSubcategory.Text = "Sales";

                }
                else if (Request.QueryString["exclusive"] != null)
                {
                   //divSubMenu.Visible = true;
                    hlSubcategory.Text = "Exclusive";
                }
                else if (Request.QueryString["bestseller"] != null)
                {
                    //divSubMenu.Visible = true;
                    hlSubcategory.Text = "Best Seller";
                }
                else if (Request.QueryString["discount"] != null)
                {
                    ViewState["OrderBy"] = "discount desc";
                }
                else if (Request.QueryString["bultgift"] != null)
                {
                    hlSubcategory.Text = "Bult Gift";
                }
                else
                {
                    divSubMenu.Visible = true;
                }
                LoadMenu();
                BindFilteredProducts(1, true);
                //GetProducts(1);
                BindReviews(product_id);
                // 8: Category view - Following API call implemented on the load of the category page.
                string categoryview = @"
                    wigzo(""track"", ""categoryview"",""" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + Request.RawUrl + @""");";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "categoryview", categoryview, true);
                DynamicLabel();
                getText();
                //TODO: need to verify
                ////if (Session["CustomerEmail"] != null || Session["UserName"] != null || Session["Mobile"] != null)
                ////{
                ////    string identifyapi = @"wigzo(""identify"", { email: " + Session["CustomerEmail"].ToString() + ",phone: " + Session["Mobile"].ToString() + ",fullName: " + Session["UserName"].ToString() + @" }); ";
                ////    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "identifyapi", identifyapi, true);
                ////}
                ////else
                ////{
                ////    string categoryview = @"
                ////    wigzo(""track"", ""categoryview"",""" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + Request.RawUrl + @""");";

                ////    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "categoryview", categoryview, true);
                ////}
            }
        }
        #endregion

        #region BindProduct
        //private void GetSearchProduct(int flag, int pageIndex)
        //{
        //    if (Request.QueryString["mnid"] != null && Request.QueryString["smnid"] != null)
        //    {
        //        ViewState["flag"] = flag;
        //        mnId = Convert.ToInt32(Request.QueryString["mnid"].ToString());
        //        smnId = Convert.ToInt32(Request.QueryString["smnid"].ToString());

        //        ViewState["Category"] = mnId;
        //        ViewState["SubCategory"] = smnId;
        //        ViewState["ChildCategory"] = null;
        //        ViewState["ProductTypeID"] = null;
        //    }
        //    else if (Request.QueryString["mnid"] != null)
        //    {
        //        ViewState["flag"] = flag;
        //        mnId = Convert.ToInt32(Request.QueryString["mnid"].ToString());

        //        ViewState["Category"] = mnId;
        //        ViewState["SubCategory"] = null;
        //        ViewState["ChildCategory"] = null;
        //        ViewState["ProductTypeID"] = null;
        //    }

        //    string constring = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constring))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("pr_search_product", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
        //            cmd.Parameters.AddWithValue("@PageSize", PageSize);
        //            cmd.Parameters.AddWithValue("@is_active", is_active);
        //            cmd.Parameters.AddWithValue("@is_default", is_default);
        //            cmd.Parameters.AddWithValue("@menu_id", mnId);
        //            cmd.Parameters.AddWithValue("@sub_menu_id", smnId);
        //            cmd.Parameters.AddWithValue("@Flag", Flag);
        //            cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
        //            cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
        //            con.Open();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //            {
        //                using (DataSet ds = new DataSet())
        //                {
        //                    sda.Fill(ds);
        //                    if (ds != null && ds.Tables.Count > 0)
        //                    {
        //                        DataTable dt = ds.Tables[0];
        //                        if (dt.Rows.Count > 0)
        //                        {
        //                            ulpaging.Visible = true;
        //                            divPageCount.Visible = true;
        //                            rptproduct.DataSource = dt;
        //                            rptproduct.DataBind();
        //                        }
        //                        else
        //                        {
        //                            // Response.Redirect("~/coming-soon");

        //                            //lblMsg.Visible = true;
        //                            //lblMsg.Text = "No records found.";
        //                            //divPageCount.Visible = false;
        //                            //ulpaging.Visible = false;
        //                        }
        //                        DataTable dtnavigation = ds.Tables[1];
        //                        if (dtnavigation.Rows.Count > 0)
        //                        {
        //                            if (mnId != 0 && smnId != 0)
        //                            {
        //                                naviName = dtnavigation.Rows[0]["sub_menu_name"].ToString().ToLower();
        //                                lblTitleName.Text = naviName.ToUpper().Substring(0, 1) + naviName.ToLower().Substring(1);
        //                                //lblCategoryName.Text = naviName.ToUpper().Substring(0, 1) + naviName.ToLower().Substring(1);


        //                                rpt_naviCategory.DataSource = dtnavigation;
        //                                rpt_naviCategory.DataBind();

        //                                rpt_naviSubCategory.DataSource = dtnavigation;
        //                                rpt_naviSubCategory.DataBind();
        //                            }
        //                            else if (mnId != 0 && smnId == 0)
        //                            {
        //                                naviName = dtnavigation.Rows[0]["menu_name"].ToString().ToLower();
        //                                lblTitleName.Text = naviName.ToUpper().Substring(0, 1) + naviName.ToLower().Substring(1);
        //                                //lblCategoryName.Text = naviName.ToUpper().Substring(0, 1) + naviName.ToLower().Substring(1);
        //                                rpt_naviCategory.DataSource = dtnavigation;
        //                                rpt_naviCategory.DataBind();
        //                            }
        //                            //rpt_naviCldCategory.DataSource = dtnavigation;
        //                            //rpt_naviCldCategory.DataBind();
        //                        }

        //                        DataTable dtMenu = ds.Tables[2];
        //                        if (dtMenu.Rows.Count > 0)
        //                        {

        //                            ulpaging.Visible = true;
        //                            divPageCount.Visible = true;
        //                            //repCategory.DataSource = dtMenu;
        //                            //repCategory.DataBind();
        //                        }

        //                    }
        //                    else
        //                    {
        //                        //Response.Redirect("~/coming-soon");
        //                        //lblMsg.Visible = true;
        //                        //lblMsg.Text = "No records found.";
        //                        //ulpaging.Visible = false;
        //                    }
        //                }
        //            }
        //            con.Close();
        //            int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
        //            this.PopulatePager(recordCount, pageIndex);

        //            //this.BindSubDetailsProducts(mnId, smnId, cmnId);
        //        }
        //    }
        //}

        //protected void rptproduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    bool stock = false;
        //    decimal discount;
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //HiddenField proctId = (HiddenField)e.Item.FindControl("hfieldproductid");
        //long ProductId = Convert.ToInt64(proctId.Value);
        //AjaxControlToolkit.Rating ratingShow = (AjaxControlToolkit.Rating)e.Item.FindControl("RatingLatest");

        //review_handler reviewHandler = new review_handler();
        //DataSet ds = reviewHandler.get_product_review(ProductId, 3);
        //if (ds != null && ds.Tables.Count > 0)
        //{
        //    DataTable dt = ds.Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        ratingShow.CurrentRating = Convert.ToInt32(dt.Rows[0]["rating"].ToString());
        //    }
        //}

        //HtmlGenericControl divOutofStock = e.Item.FindControl("divOutofStock") as HtmlGenericControl;
        //HtmlGenericControl thumstock = e.Item.FindControl("thumbbestseller") as HtmlGenericControl;
        //HiddenField instock = (HiddenField)e.Item.FindControl("hfieldStock");
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


        // HtmlGenericControl origPrice = e.Item.FindControl("origPrice") as HtmlGenericControl;
        // HtmlGenericControl saleprice = e.Item.FindControl("saleprice") as HtmlGenericControl;
        //HiddenField Hdiscount = (HiddenField)e.Item.FindControl("hfieldDiscount");
        // discount = Convert.ToDecimal(Hdiscount.Value);

        // if (discount == 0)
        // {
        //     saleprice.Visible = false;
        //    // origPrice.Visible = true;
        // }
        // else
        // {
        //    // saleprice.Visible = false;
        //     origPrice.Visible = true;
        // }
        //    }
        //}
        #endregion

        #region Bind Category/Color/Material
        //private void BindSubDetailsProducts_NotUsed(int flag, int pageIndex)
        //{
        //    bool IsSales = false, IsExclusive = false, IsBestSeller = false;
        //    //if (Request.QueryString["gid"] == "1")
        //    //    gid = Convert.ToInt32(Request.QueryString["gid"].ToString());

        //    if (rbtGendertype.SelectedValue != "")
        //        gid = Convert.ToByte(rbtGendertype.SelectedValue);

        //    if (Request.QueryString["mnid"] != null)
        //        mnId = Convert.ToInt32(Request.QueryString["mnid"].ToString());

        //    if (Request.QueryString["smnid"] != null)
        //        smnId = Convert.ToInt32(Request.QueryString["smnid"].ToString());

        //    if (Request.QueryString["sales"] != null)
        //        IsSales = true;

        //    if (Request.QueryString["exclusive"] != null)
        //        IsExclusive = true;

        //    if (Request.QueryString["bestseller"] != null)
        //        IsBestSeller = true;

        //    string productname = null;
        //    if (Request.QueryString["s"] != null)
        //        productname = Request.QueryString["s"];

        //    cmnId = 0;

        //    //ViewState["Products"] = null;
        //    ViewState["Price"] = null;
        //    ViewState["ColorIDs"] = null;
        //    ViewState["MaterialIDs"] = null;
        //    ViewState["ProductTypeID"] = null;

        //    if (ViewState["pageindex"] != null)
        //    {
        //        PageIndex = Convert.ToInt32(ViewState["pageindex"].ToString());
        //    }

        //    hfCategory.Value = mnId.ToString();
        //    hfSubCategory.Value = smnId.ToString();
        //    hfGnr.Value = (gid.HasValue ? gid.ToString() : "");

        //    product_handler productHandler = new product_handler();
        //    //DataSet ds = productHandler.filter_product(mnId, smnId, cmnId, productname, PageIndex, PageSize, IsSales, IsExclusive, IsBestSeller, gid, RecordCount);
        //    DataSet ds = productHandler.filter_product(mnId, smnId, cmnId, productname, PageIndex, PageSize, IsSales, IsExclusive, IsBestSeller, gid, RecordCount);
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        //DataTable dt = ds.Tables[0];
        //        //if (dt.Rows.Count > 0)
        //        //{
        //        //    totcount = Convert.ToInt32(dt.Rows[0]["total_count"].ToString());
        //        //    ulpaging.Visible = true;
        //        //    divPageCount.Visible = true;
        //        //}

        //        DataTable dtpro = ds.Tables[0];
        //        if (dtpro.Rows.Count > 0)
        //        {
        //            totcount = Convert.ToInt32(dtpro.Rows[0]["TotalRec"].ToString());

        //            lblMsg.Visible = false;
        //            DataView dv = new DataView(dtpro);
        //            DataTable dtproduct = dv.ToTable();
        //            //ViewState["Products"] = dtpro;
        //            rptproduct.DataSource = dv;
        //            rptproduct.DataBind();

        //            DataTable dtnavigation;
        //            if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
        //            {
        //                if (mnId != 0 && smnId != 0)
        //                {
        //                    dtnavigation = ds.Tables[4];
        //                    naviName = dtnavigation.Rows[0]["sub_menu_name"].ToString().ToLower();
        //                    //lblTitleName.Text = naviName.ToUpper().Substring(0, 1) + naviName.ToLower().Substring(1);

        //                    rpt_naviCategory.DataSource = ds.Tables[3];
        //                    rpt_naviCategory.DataBind();

        //                    hlSubcategory.Text = "› " + dv[0]["sub_menu_name"].ToString();
        //                    //rpt_naviSubCategory.DataSource = dtnavigation;
        //                    //rpt_naviSubCategory.DataBind();
        //                }
        //                else if (mnId != 0 && smnId == 0)
        //                {
        //                    dtnavigation = ds.Tables[3];
        //                    naviName = dtnavigation.Rows[0]["menu_name"].ToString().ToLower();
        //                    hlSubcategory.Text = "";
        //                    //lblTitleName.Text = naviName.ToUpper().Substring(0, 1) + naviName.ToLower().Substring(1);
        //                    rpt_naviCategory.DataSource = dtnavigation;
        //                    rpt_naviCategory.DataBind();
        //                }
        //            }

        //            //if (totcount > 0)
        //            //{
        //            //    ulpaging.Visible = true;
        //            //    divPageCount.Visible = true;
        //            //}

        //            //int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
        //            //this.PopulatePager(totcount, pageIndex);

        //            //if (totcount == dtpro.Rows.Count)
        //            //{
        //            //    //btnLoad.Visible = false;
        //            //}
        //            //else
        //            //{
        //            //    // btnLoad.Visible = true;
        //            //}
        //        }
        //        else
        //        {
        //            lblMsg.Visible = true;
        //            //lblMsg.Text = "No products found.";
        //            Response.Redirect("~/coming-soon");
        //        }

        //        // Bind Colors
        //        DataTable dtcolor = ds.Tables[1];
        //        if (dtcolor.Rows.Count > 0)
        //        {
        //            DataView dv = new DataView(dtcolor);
        //            chkColorName.DataSource = dv;
        //            chkColorName.DataTextField = "color_name";
        //            chkColorName.DataValueField = "color_id";
        //            chkColorName.DataBind();
        //        }
        //        else
        //        {
        //            chkColorName.DataSource = null;
        //            chkColorName.DataBind();
        //        }

        //        // Bind Materials
        //        DataTable dtmaterial = ds.Tables[2];
        //        if (dtmaterial.Rows.Count > 0)
        //        {
        //            DataView dv = new DataView(dtmaterial);
        //            chkMaterialName.DataSource = dv;
        //            chkMaterialName.DataTextField = "material_name";
        //            chkMaterialName.DataValueField = "material_id";
        //            chkMaterialName.DataBind();
        //        }
        //        else
        //        {
        //            chkMaterialName.DataSource = null;
        //            chkMaterialName.DataBind();
        //        }
        //    }

        //}
        protected void chkColorName_CheckedChanged(object sender, EventArgs e)
        {
            List<string> ColorList = new List<string>();
            foreach (RepeaterItem row in rptColor.Items)
            {
                CheckBox cb = (CheckBox)row.FindControl("chkColorName");

                if (cb.Checked)
                {
                    ColorList.Add(cb.Attributes["ColorName"]);
                }

            }
            if (ColorList.Count > 0)
            {
                ViewState["ColorIDs"] = String.Join(",", ColorList.ToArray());
            }
            else
            {
                ViewState["ColorIDs"] = null;
            }

            BindFilteredProducts(1, false);
        }

        protected void chkColorName_mob_CheckedChanged(object sender, EventArgs e)
        {
            List<string> ColorList = new List<string>();
            foreach (RepeaterItem row in rptColor_mob.Items)
            {
                CheckBox cb = (CheckBox)row.FindControl("chkColorName");

                if (cb.Checked)
                {
                    ColorList.Add(cb.Attributes["ColorName"]);
                }

            }
            if (ColorList.Count > 0)
            {
                ViewState["ColorIDs"] = String.Join(",", ColorList.ToArray());
            }
            else
            {
                ViewState["ColorIDs"] = null;
            }

            BindFilteredProducts(1, false);
        }


        protected void chkMaterialName_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> MaterialList = new List<string>();
            foreach (ListItem item in chkMaterialName.Items)
            {
                if (item.Selected)
                {
                    // If the item is selected, add the value to the list.
                    MaterialList.Add(item.Value);
                }
                else
                {
                    // Item is not selected, do something else.
                }
            }
            if (MaterialList.Count > 0)
            {
                ViewState["MaterialIDs"] = String.Join(",", MaterialList.ToArray());
            }
            else
            {
                ViewState["MaterialIDs"] = null;
            }

            //flag = true;
            BindFilteredProducts(1, false);
        }

        protected void chkMaterialName_mob_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> MaterialList = new List<string>();
            foreach (ListItem item in chkMaterialName_mob.Items)
            {
                if (item.Selected)
                {
                    // If the item is selected, add the value to the list.
                    MaterialList.Add(item.Value);
                }
                else
                {
                    // Item is not selected, do something else.
                }
            }
            if (MaterialList.Count > 0)
            {
                ViewState["MaterialIDs"] = String.Join(",", MaterialList.ToArray());
            }
            else
            {
                ViewState["MaterialIDs"] = null;
            }

            //flag = true;
            BindFilteredProducts(1, false);
        }

        protected void rbtPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["OrderBy"] = rbtPrice.SelectedValue;
            BindFilteredProducts(1, false);
        }
        protected void rbtPrice_mob_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["OrderBy"] = rbtPrice_mob.SelectedValue;
            BindFilteredProducts(1, false);
        }
        protected void rbtGendertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ViewState["gender"] = rbtGendertype.SelectedValue;
            BindFilteredProducts(1, false);
            LoadMenu();
        }
        protected void rbtGendertype_mob_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ViewState["gender"] = rbtGendertype.SelectedValue;
            BindFilteredProducts(1, false);
            LoadMenu();
        }
        #endregion

        #region products paging
        //private void PopulatePager(long recordCount, int currentPage)
        //{
        //    List<ListItem> pages = new List<ListItem>();
        //    int startIndex, endIndex;
        //    int pagerSpan = 5;

        //    //Calculate the Start and End Index of pages to be displayed.
        //    double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        //    int pageCount = (int)Math.Ceiling(dblPageCount);
        //    startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        //    endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        //    if (currentPage > pagerSpan % 2)
        //    {
        //        if (currentPage == 2)
        //        {
        //            endIndex = 5;
        //        }
        //        else
        //        {
        //            endIndex = currentPage + 2;
        //        }
        //    }
        //    else
        //    {
        //        endIndex = (pagerSpan - currentPage) + 1;
        //    }

        //    if (endIndex - (pagerSpan - 1) > startIndex)
        //    {
        //        startIndex = endIndex - (pagerSpan - 1);
        //    }

        //    if (endIndex > pageCount)
        //    {
        //        endIndex = pageCount;
        //        startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        //    }

        //    //Add the First Page Button.
        //    if (currentPage > 1)
        //    {
        //        pages.Add(new ListItem("First", "1"));
        //    }

        //    //Add the Previous Button.
        //    if (currentPage > 1)
        //    {
        //        pages.Add(new ListItem("<", (currentPage - 1).ToString()));
        //    }

        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        //    }

        //    //Add the Next Button.
        //    if (currentPage < pageCount)
        //    {
        //        pages.Add(new ListItem(">", (currentPage + 1).ToString()));
        //    }

        //    //Add the Last Button.
        //    if (currentPage != pageCount)
        //    {
        //        pages.Add(new ListItem("Last", pageCount.ToString()));
        //    }
        //    lblPageCount.Text = "Page " + currentPage + " of " + recordCount;

        //    if (recordCount < PageSize)
        //    {
        //        divPaging.Visible = false;
        //    }
        //    else
        //    {
        //        divPaging.Visible = true;
        //        rptPager.DataSource = pages;
        //        rptPager.DataBind();
        //    }
        //}

        //protected void Page_Changed(object sender, EventArgs e)
        //{
        //    Flag = Convert.ToInt32(ViewState["flag"]);
        //    int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        //    ViewState["pageindex"] = Convert.ToInt32(pageIndex);
        //    //this.BindSubDetailsProducts(Flag, pageIndex);
        //    BindFilteredProducts(pageIndex, false);
        //}
        #endregion

        //#region latest product
        //private void BindLatestProduct()
        //{
        //    product_handler productHandler = new product_handler();
        //    DataSet dsProducts = productHandler.get_home_product_details(0, 3);
        //    if (dsProducts != null && dsProducts.Tables.Count > 0)
        //    {
        //        DataTable dt = dsProducts.Tables[0];
        //        rptLatestProduct.DataSource = dt;
        //        rptLatestProduct.DataBind();
        //    }
        //}

        //protected void rptLatestProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    decimal discount;
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HtmlGenericControl spOldPrice = e.Item.FindControl("spOldPrice") as HtmlGenericControl;
        //        HtmlGenericControl divdiscount = e.Item.FindControl("divdiscount") as HtmlGenericControl;
        //        HiddenField Hdiscount = (HiddenField)e.Item.FindControl("hfieldDiscount");
        //        discount = Convert.ToDecimal(Hdiscount.Value);

        //        if (discount == 0)
        //        {
        //            divdiscount.Visible = false;
        //            spOldPrice.Visible = false;
        //        }
        //        else
        //        {
        //            divdiscount.Visible = true;
        //            spOldPrice.Visible = true;
        //        }
        //    }
        //}
        //#endregion

        #region Filter
        private void BindFilteredProducts(int pageIndex, bool isPageLoad)
        {
            Int32 categoryID = ViewState["Category"] == null ? 0 : Convert.ToInt32(ViewState["Category"].ToString());

            if (ViewState["Category"] == null && Request.QueryString["mnid"] != null)
            {
                categoryID = Convert.ToInt32(Request.QueryString["mnid"]);
            };

            Int32 subCategoryID = ViewState["SubCategory"] == null ? 0 : Convert.ToInt32(ViewState["SubCategory"].ToString());
            if (ViewState["SubCategory"] == null && Request.QueryString["smnid"] != null)
            {
                subCategoryID = Convert.ToInt32(Request.QueryString["smnid"]);
                LoadSubCategory(subCategoryID);
            };

            if (ViewState["Category"] == null && Request.QueryString["bultgift"] != null)
            {
                categoryID = Convert.ToInt32(Request.QueryString["bultgift"]);
            };

            bool IsSales = false, IsExclusive = false, IsBestSeller = false, IsBulkGift = false;
            Int32 childCategoryID = ViewState["ChildCategory"] == null ? 0 : Convert.ToInt32(ViewState["ChildCategory"].ToString());
            Int64 productTypeID = ViewState["ProductTypeID"] == null ? 0 : Convert.ToInt64(ViewState["ProductTypeID"].ToString());
            string Orderby = "";
            string colorIDs = string.Empty;
            string materialIDs = string.Empty;
            string ProductFilterText = string.Empty;
            string productname = null;

            if (Request.QueryString["s"] != null)
            {
                productname = Request.QueryString["s"];
                string searchevent = @"wigzo(""track"", ""search"", """ + productname + @""");";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "searchevent", searchevent, true);
            }
            if (ViewState["MinPrice"] != null && ViewState["MaxPrice"] != null)
            {
                minValue = Convert.ToDecimal(ViewState["MinPrice"].ToString());
                maxValue = Convert.ToDecimal(ViewState["MaxPrice"].ToString());
            }

            if (ViewState["ColorIDs"] != null)
            {
                colorIDs = ViewState["ColorIDs"].ToString();
            }

            if (ViewState["MaterialIDs"] != null)
            {
                materialIDs = ViewState["MaterialIDs"].ToString();
            }
            if (ViewState["OrderBy"] != null)
            {
                Orderby = ViewState["OrderBy"].ToString();
            }

            if (rbtGendertype.SelectedValue != "")
                gid = Convert.ToByte(rbtGendertype.SelectedValue);

            if (Request.QueryString["sales"] != null)
            {
                IsSales = true;
                ProductFilterText = "Sales";
            }
            else if (Request.QueryString["exclusive"] != null)
            {
                IsExclusive = true;
                ProductFilterText = "Exclusive";
            }
            else if (Request.QueryString["bestseller"] != null)
            {
                IsBestSeller = true;
                ProductFilterText = "Best Seller";
            }
            else if (Request.QueryString["bultgift"] != null)
            {
                IsBulkGift = true;
                ProductFilterText = "Bult Gift";
            }
            else
                ProductFilterText = "Shop";

            if (ViewState["pageindex"] != null)
            {
                PageIndex = Convert.ToInt32(ViewState["pageindex"].ToString());
            }

            hfCategory.Value = categoryID.ToString();
            hfSubCategory.Value = subCategoryID.ToString();
            hfColorIDs.Value = colorIDs;
            hfMaterialIDs.Value = materialIDs;
            hfOrderBy.Value = Orderby;
            hfGnr.Value = (gid.HasValue ? gid.ToString() : "");
            product_handler filterProductsHandler = new product_handler();
            DataSet ds = filterProductsHandler.filter_productlist(categoryID, subCategoryID, childCategoryID, productTypeID, minValue, maxValue, colorIDs, materialIDs, productname,
                                                                    IsSales, IsExclusive, IsBestSeller, gid, pageIndex, PageSize, Orderby, RecordCount);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    totcount = Convert.ToInt32(dt.Rows[0]["TotalRec"]);
                    //lblMenu1.Text = dt.Rows[0]["menu_name"].ToString();
                    //lblMenu2.Text = dt.Rows[0]["menu_name"].ToString();
                    //lblSubMenu.Text = dt.Rows[0]["sub_menu_name"].ToString();

                    if (Request.QueryString["mnid"] != null && Request.QueryString["smnid"] != null)
                    {
                        lblMenuHead.Text = dt.Rows[0]["sub_menu_name"].ToString();
                        lblMenuHeadRight.Text = dt.Rows[0]["menu_name"].ToString() + " / " + dt.Rows[0]["sub_menu_name"].ToString();
                    }
                    else if (Request.QueryString["mnid"] != null && Request.QueryString["smnid"] == null)
                    {
                        lblMenuHead.Text = dt.Rows[0]["menu_name"].ToString();
                        lblMenuHeadRight.Text = dt.Rows[0]["menu_name"].ToString();
                    }
                    else
                    {
                        lblMenuHead.Text = ProductFilterText;
                        lblMenuHeadRight.Text = ProductFilterText;
                    }

                    //divPageCount.Visible = true;
                    //ulpaging.Visible = true;
                    lblMsg.Visible = false;
                    lblMsg_mob.Visible = false;

                    if (totcount <= PageSize)
                        hlLoadMore.Visible = false;
                    else
                        hlLoadMore.Visible = true;

                    rptproduct.DataSource = dt;
                    rptproduct.DataBind();

                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "No products found.";

                    lblMsg_mob.Visible = true;
                    lblMsg_mob.Text = "No products found.";

                    hlLoadMore.Visible = false;
                    rptproduct.DataSource = null;
                    rptproduct.DataBind();
                }

                if (isPageLoad)
                {
                    // Bind Colors
                    DataTable dtcolor = ds.Tables[1];
                    if (dtcolor.Rows.Count > 0)
                    {
                        rptColor.DataSource = dtcolor;
                        rptColor.DataBind();

                        rptColor_mob.DataSource = dtcolor;
                        rptColor_mob.DataBind();
                    }
                    else
                    {
                        rptColor.DataSource = null;
                        rptColor.DataBind();

                        rptColor_mob.DataSource = null;
                        rptColor_mob.DataBind();
                    }

                    // Bind Materials
                    DataTable dtmaterial = ds.Tables[2];
                    if (dtmaterial.Rows.Count > 0)
                    {
                        chkMaterialName.DataSource = dtmaterial;
                        chkMaterialName.DataTextField = "material_name";
                        chkMaterialName.DataValueField = "material_id";
                        chkMaterialName.DataBind();

                        chkMaterialName_mob.DataSource = dtmaterial;
                        chkMaterialName_mob.DataTextField = "material_name";
                        chkMaterialName_mob.DataValueField = "material_id";
                        chkMaterialName_mob.DataBind();
                    }
                    else
                    {
                        chkMaterialName.Items.Clear();
                        chkMaterialName.DataSource = null;
                        chkMaterialName.DataBind();

                        chkMaterialName_mob.Items.Clear();
                        chkMaterialName_mob.DataSource = null;
                        chkMaterialName_mob.DataBind();
                    }
                }
                if (isPageLoad)
                {
                    DataTable dtnavigation;
                    if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
                    {
                        if (mnId != 0 && smnId != 0)
                        {
                            dtnavigation = ds.Tables[4];
                            naviName = dtnavigation.Rows[0]["sub_menu_name"].ToString().ToLower();
                            hlSubcategory.Text = "› " + dt.Rows[0]["sub_menu_name"].ToString();

                            hlSubcategory_mob.Text = "› " + dt.Rows[0]["sub_menu_name"].ToString();

                            rpt_naviCategory.DataSource = ds.Tables[3];
                            rpt_naviCategory.DataBind();

                            rpt_naviCategory_mob.DataSource = ds.Tables[3];
                            rpt_naviCategory_mob.DataBind();
                        }
                        else if (mnId != 0 && smnId == 0)
                        {
                            dtnavigation = ds.Tables[3];
                            naviName = dtnavigation.Rows[0]["menu_name"].ToString().ToLower();
                            hlSubcategory.Text = "";
                            hlSubcategory_mob.Text = "";

                            rpt_naviCategory.DataSource = dtnavigation;
                            rpt_naviCategory.DataBind();

                            rpt_naviCategory_mob.DataSource = dtnavigation;
                            rpt_naviCategory_mob.DataBind();
                        }

                    }
                }
            }
            else
            {
                Response.Redirect("~/coming-soon");
            }

            if (status == 1)
            {
                ViewState["SubCategory"] = null;
            }
            if (status == 2)
            {
                ViewState["ChildCategory"] = null;
            }
            if (status == 3)
            {
                ViewState["ProductTypeID"] = null;
            }
            //if (totcount > 0)
            //{
            //    ulpaging.Visible = true;
            //    divPageCount.Visible = true;
            //}
            //this.PopulatePager(totcount, pageIndex);
        }
        #endregion

        protected void btnGo_Click(object sender, EventArgs e)
        {
            amountFrom.Value = amountFrom.Value;
            amountTo.Value = amountTo.Value;

            amountFrom_mob.Value = amountFrom_mob.Value;
            amountTo_mob.Value = amountTo_mob.Value;

            minValue = Convert.ToDecimal(amountFrom.Value);
            maxValue = Convert.ToDecimal(amountTo.Value);

            minValue = Convert.ToDecimal(amountFrom_mob.Value);
            maxValue = Convert.ToDecimal(amountTo_mob.Value);

            ViewState["MinPrice"] = minValue;
            ViewState["MaxPrice"] = maxValue;

            BindFilteredProducts(1, false);
        }


        #region Add To Cart...

        //protected void rptproduct_itemcommand(object source, RepeaterCommandEventArgs e)
        //{
        //if (e.CommandName == "addtocart")
        //{
        //    Int64 product_id = Convert.ToInt64(e.CommandArgument.ToString());
        //    ViewState["ProId"] = product_id.ToString();

        //    bool result = this.AddToShoppingCart();
        //    if (result)
        //    {
        //        System.Web.UI.HtmlControls.HtmlGenericControl cartDiv = (HtmlGenericControl)this.Master.FindControl("divCart");
        //        cartDiv.Attributes.Add("class", "speed-in");

        //        UpdatePanel cartPanel = (UpdatePanel)Master.FindControl("upCart");
        //        cartPanel.Update();

        //        //if (Request.QueryString["cproid"] != null)
        //        //{
        //        //    Response.Redirect("../viewcart.aspx");
        //        //}
        //        //else
        //        //{
        //        //    Response.Redirect("../view-cart");
        //        //}
        //    }
        //}
        //if (e.CommandName == "wishlist")
        //{
        //    product_id = Convert.ToInt64(e.CommandArgument.ToString());
        //    if (Session["CustomerLoginDetails"] != null)
        //    {
        //        wishlist_handler wishlistHandler = new wishlist_handler();
        //        BusinessEntities.wishlist wishlist = new BusinessEntities.wishlist();

        //        //wishlist.WishlistId=
        //        wishlist.product_id = product_id;
        //        wishlist.email_id = Session["CustomerLoginDetails"].ToString();

        //        bool resultReview = wishlistHandler.InsertWishList(wishlist);
        //        if (resultReview == true)
        //        {
        //            lblMessage.Text = "adding item to wishlist Successfully";
        //            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('adding item to wishlist Successfully')", true);
        //            SendEmailadmin(product_id);
        //            Response.Redirect("~/wishlist.aspx");

        //        }
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Please login before add item in Wish list";
        //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please login before add item in Wish list.')", true);
        //    }
        //}
        //}
        private void AddWishlist()
        {
            int product_id = Convert.ToInt32(ViewState["product_id"]);
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
                    lblMessage.Text = "adding item to wishlist Successfully";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('adding item to wishlist Successfully')", true);
                    SendEmailadmin(product_id);
                    Response.Redirect("~/wishlist.aspx");

                }
            }
            else
            {
                lblMessage.Text = "Please login before add item in Wish list";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please login before add item in Wish list.')", true);
            }
        }
        private bool AddToShoppingCart()
        {
           Int64 product_id = 0;
            //HiddenField product_id = (HiddenField)e.Item.FindControl("hfieldproductid");
            if (ViewState["ProId"] != null)
            {
                product_id = Convert.ToInt64(ViewState["ProId"]);

                product_handler productHandler = new product_handler();
                DataSet dsProducts = productHandler.get_product_details(product_id);
                if (dsProducts != null && dsProducts.Tables.Count > 0)
                {
                    DataTable dt = dsProducts.Tables[0];
                    ViewState["ProductDetails"] = dt;

                }
            }

            Boolean blnMatch = false;
            DataTable dtCart = (DataTable)Session["Cart"];
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
                    DataTable dt = (DataTable)ViewState["ProductDetails"];
                    drCart["product_id"] = product_id;
                    drCart["product_name"] = dt.Rows[0]["product_name"].ToString();
                    if (Session["procust"] != null)
                    {
                        drCart["thumb_image"] = "images/OrderImages/" + Session["procust"].ToString();
                    }
                    else
                    {
                        drCart["thumb_image"] = "images/Product/Thumb/" + dt.Rows[0]["thumb_image"].ToString();
                    }
                    drCart["menu_name"] = dt.Rows[0]["menu_name"].ToString();
                    drCart["sub_menu_name"] = dt.Rows[0]["sub_menu_name"].ToString();
                    drCart["child_name"] = dt.Rows[0]["child_name"].ToString();
                    drCart["weight"] = dt.Rows[0]["weight"].ToString();
                    drCart["gendertype"] = dt.Rows[0]["gendertype"].ToString();

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
                    drCart["quantity"] = 1;
                    drCart["Total"] = Convert.ToInt32(1) * TotalPrice;
                    dtCart.Rows.Add(drCart);
                }
                Session["Cart"] = dtCart;
            }
            return true;
        }

        #endregion

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
            //dv.RowFilter = "menu_id = 1 AND is_active=1";        // Kalpesh: Don't change this menu_id = 1 (This is Live database ID)
            dv.RowFilter = "menu_id = 2013 AND is_active=1";
            rptMenu1.DataSource = dv;
            rptMenu1.DataBind();

            rptMenu1_mob.DataSource = dv;
            rptMenu1_mob.DataBind();

            //dv.RowFilter = "menu_id = 2002 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2002 (This is Live database ID)
            dv.RowFilter = "menu_id = 2014 AND is_active=1";
            rptMenu2.DataSource = dv;
            rptMenu2.DataBind();

            rptMenu2_mob.DataSource = dv;
            rptMenu2_mob.DataBind();

            //dv.RowFilter = "menu_id = 2005 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2005 (This is Live database ID)
            dv.RowFilter = "menu_id = 2015 AND is_active=1";
            rptMenu5.DataSource = dv;
            rptMenu5.DataBind();

            rptMenu5_mob.DataSource = dv;
            rptMenu5_mob.DataBind();

            //dv.RowFilter = "menu_id = 2006 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2006 (This is Live database ID)
            dv.RowFilter = "menu_id = 2016 AND is_active=1";
            rptMenu6.DataSource = dv;
            rptMenu6.DataBind();

            rptMenu6_mob.DataSource = dv;
            rptMenu6_mob.DataBind();

            //dv.RowFilter = "menu_id = 1002 AND is_active=1";        // Kalpesh: Don't change this menu_id = 1002 (This is Live database ID)
            dv.RowFilter = "menu_id = 2017 AND is_active=1";
            rptMenu3.DataSource = dt;
            rptMenu3.DataBind();
            rptMenu3_mob.DataSource = dt;
            rptMenu3_mob.DataBind();

            //dv.RowFilter = "menu_id = 2004 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2004 (This is Live database ID)
            dv.RowFilter = "menu_id = 2018 AND is_active=1";
            rptMenu4.DataSource = dt;
            rptMenu4.DataBind();
            rptMenu4_mob.DataSource = dt;
            rptMenu4_mob.DataBind();


            //dv.RowFilter = "menu_id = 2006 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2006 (This is Live database ID)
            dv.RowFilter = "menu_id = 2019 AND is_active=1";
            rptMenu7.DataSource = dv;
            rptMenu7.DataBind();

            rptMenu7_mob.DataSource = dv;
            rptMenu7_mob.DataBind();


            dv.RowFilter = "menu_id = 2004 AND is_active=1";
            rptCollaborations.DataSource = dv;
            rptCollaborations.DataBind();

            rptMenu7_mobColl.DataSource = dv;
            rptMenu7_mobColl.DataBind();


            //dv.RowFilter = "menu_id = 4 AND is_active=1";
            //rptMenu4.DataSource = dt;
            //rptMenu4.DataBind();

            //dv.RowFilter = "menu_id = 3 AND is_active=1";
            //rptMenu3.DataSource = dt;
            //rptMenu3.DataBind();
        }
        private void LoadSubCategory(int sub_menu_id)
        {
            menu_handler subCategoryHandler = new menu_handler();
            DataSet dssubcategory = subCategoryHandler.get_menu_sub(0, sub_menu_id, null);
            if (dssubcategory != null && dssubcategory.Tables.Count > 0)
            {
                DataTable dt = dssubcategory.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblDecsHeader.Text = dt.Rows[0]["desc_header"].ToString();
                    lblDecsFooter.Text = dt.Rows[0]["desc_footer"].ToString();
                }
            }
        }

        protected void rptproduct_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //long smid = 0;
            //if (e.CommandName == "allproduct")
            //{
            //    product_id = Convert.ToInt64(e.CommandArgument.ToString());
            //    product_handler productHandler = new product_handler();
            //    DataSet dsProducts = productHandler.get_product_details(product_id);
            //    if (dsProducts != null && dsProducts.Tables.Count > 0)
            //    {
            //        DataTable dt = dsProducts.Tables[0];

            //        ViewState["ProductDetails"] = dt;
            //        lblProductName.Text = dt.Rows[0]["product_name"].ToString();
            //        //lblProductName2.Text = dt.Rows[0]["product_name"].ToString();

            //        //lblDiscPrice.Text = dt.Rows[0]["price"].ToString();
            //        lblSalePrice.Text = dt.Rows[0]["sale_price"].ToString();
            //        lblSortDesc.Text = dt.Rows[0]["short_description"].ToString();
            //        smid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

            //        DataTable dtCart = (DataTable)Session["Cart"];
            //        long matchid = 0;
            //        long proid = 0;
            //        int stock = 0;
            //        matchid = Convert.ToInt64(dt.Rows[0]["product_id"].ToString());

            //        if (dtCart != null && dtCart.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dtCart.Rows.Count; i++)
            //            {
            //                proid = Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString());
            //                if (matchid == proid)
            //                {
            //                    btnAddToCart.Enabled = true;
            //                    lblStock.Visible = false;
            //                    //btnbuynow.Enabled = true;
            //                    break;
            //                }
            //                else
            //                {
            //                    stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
            //                    if (stock <= 0)
            //                    {

            //                        btnAddToCart.Enabled = false;
            //                        btnAddToCart.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
            //                        //btnbuynow.CssClass = "btn btn--lg btn--black";
            //                        //btnCustomBug.Enabled = false;
            //                        lblStock.Visible = true;
            //                        lblStock.Text = "Out of Stock";
            //                        // divaleadyadd.Visible = false;
            //                    }
            //                    else
            //                    {

            //                        if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
            //                        {
            //                            //btnCustomBug.Enabled = false;
            //                        }
            //                        else
            //                        {
            //                            //btnCustomBug.Enabled = true;
            //                        }
            //                        btnAddToCart.Enabled = true;
            //                        //btnbuynow.Enabled = true;
            //                        lblStock.Visible = true;
            //                        lblStock.Text = "In Stock";
            //                        // divaleadyadd.Visible = false;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            stock = Convert.ToInt32(dt.Rows[0]["quantity"]);
            //            if (stock <= 0)
            //            {

            //                btnAddToCart.Enabled = false;
            //                btnAddToCart.CssClass = "btn--lg btn--grey font-weight--reguler text-white";
            //                //btnbuynow.Enabled = false;
            //                // btnCustomBug.Enabled = false;
            //                lblStock.Visible = true;
            //                // divaleadyadd.Visible = false;
            //            }
            //            else
            //            {
            //                if (smid == 2017) // for the sub menu "The Native Batua" only  for local sub menu id = 1013
            //                {
            //                    //btnCustomBug.Enabled = false;
            //                }
            //                else
            //                {
            //                    // btnCustomBug.Enabled = true;
            //                }

            //                btnAddToCart.Enabled = true;
            //                // btnbuynow.Enabled = true;

            //                lblStock.Visible = true;
            //                lblStock.Text = "In Stock";
                            
            //                // divaleadyadd.Visible = false;

            //            }
            //        }
            //        DataTable dtimgZoom = dsProducts.Tables[1];
            //        if (dtimgZoom.Rows.Count > 0)
            //        {
            //            rptimgZoomLarge.Visible = true;
            //            rptimgZoomLarge.DataSource = dtimgZoom;
            //            rptimgZoomLarge.DataBind();
            //        }
            //        this.BindReviews(product_id);
            //    }

            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
            //}
            
        }
        // Added by Chandni 18-Jan-2020
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
                    //ratingShow.CurrentRating = Convert.ToInt16(Math.Ceiling(average));
                    //lblReview.Text = average.ToString("0.00") + " | " + dt.Rows.Count + " Customer Review(s)";
                    //litReviewHead.Text = "View Review";
                }
                else
                {
                    //ratingShow.Visible = false;
                   // litReviewHead.Text = "No Review";
                }
            }
        }

        protected void lbtnwishlist_Click(object sender, EventArgs e)
        {
            AddWishlist();
        }
        private void DynamicLabel()
        {
            //CMS details for verious Page
            pagelabel_handler pagelabelHandler = new pagelabel_handler();
            DataSet ds = pagelabelHandler.get_pagelabel(null);
            Session["PageLabel"] = ds.Tables[0];    //This is DataTable
        }
        private void getText()
        {
            try
            {
                DataTable dt = (DataTable)Session["PageLabel"];
                if (dt.Rows.Count > 0)
                {
                    lblCouponCode.Text = dt.Select("label_name='Coupon Code'")[0]["label_value"].ToString();
                    lblOffer.Text = dt.Select("label_name='Offer'")[0]["label_value"].ToString();
                }
            }
            catch (Exception)
            {
                //If any error then ignore
            }
        }

        

        //protected void lbtnAddCart_Click(object sender, EventArgs e)
        //{
        //    AddToShoppingCart();
        //    Response.Redirect("~/cart.aspx?pid=" + product_id.ToString());
        //}




        // Added by Chandni 18-Jan-2020
    }
}