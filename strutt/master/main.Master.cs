using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using BLL;
using BusinessEntities;
using System.Net;
using System.IO;
using System.Web.Mail;
using System.Text;
using System.Configuration;

namespace strutt.master
{
    public partial class main : System.Web.UI.MasterPage
    {
        string msgbody = "";
        string thumbimg = "";
        string Email = "";
        byte? gid = null;
        public string GenderType { get; set; }
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
              
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["unsb"] != null)
                {
                    long unsub = Convert.ToInt64(Request.QueryString["unsb"].ToString());
                    //Unsubscribe(unsub);
                    LoadMenu();
                   
                }
            }
            Page.LoadComplete += new EventHandler(Page_LoadComplete);
            getText();
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            AddToShoppingCart();
            AddToWishlist();
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
            rptMenu1.DataSource = dv;
            rptMenu1.DataBind();

            rptMenu1s.DataSource = dv;
            rptMenu1s.DataBind();

            dv.RowFilter = "menu_id = 2014 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2002 (This is Live database ID)
            rptMenu2.DataSource = dv;
            rptMenu2.DataBind();

            rptMenu2s.DataSource = dv;
            rptMenu2s.DataBind();

            dv.RowFilter = "menu_id = 2015 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2005 (This is Live database ID)
            rptMenu5.DataSource = dv;
            rptMenu5.DataBind();

            rptMenu5s.DataSource = dv;
            rptMenu5s.DataBind();

            dv.RowFilter = "menu_id = 2016 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2006 (This is Live database ID)
            rptMenu6.DataSource = dv;
            rptMenu6.DataBind();

            rptMenu6s.DataSource = dv;
            rptMenu6s.DataBind();

            dv.RowFilter = "menu_id = 2017 AND is_active=1";        // Kalpesh: Don't change this menu_id = 1002 (This is Live database ID)
            rptMenu3.DataSource = dt;
            rptMenu3.DataBind();

            rptMenu3s.DataSource = dt;
            rptMenu3s.DataBind();

            dv.RowFilter = "menu_id = 2018 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2004 (This is Live database ID)
            rptMenu4.DataSource = dt;
            rptMenu4.DataBind();

            rptMenu4s.DataSource = dt;
            rptMenu4s.DataBind();


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

        //     //divCart.Attributes.Add("class", "speed-in");
        //}

        //protected void dlCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    HiddenField hf = e.Item.FindControl("hfImgUrl") as HiddenField;
        //    if (hf != null)
        //    {
        //        string val = hf.Value;
        //        Image img = e.Item.FindControl("Image1") as Image;
        //        img.ImageUrl = "~/images/Product/Thumb/" + val.Substring(val.LastIndexOf('/'));
        //        //img.ImageUrl = "~/image" + val + ".jpg";
        //    }
        //}

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
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        }

        private Decimal GetCouponAmount(Decimal amount, Decimal coupcode)
        {
            Decimal CA = amount - (amount * coupcode) / 100;
            Decimal TCA = amount - CA;
            return TCA;
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
        //    // dscpn = productHandler.get_coupon_code(txtCouponCode.Text, catid, subcatid, 0);
        //    if (dscpn != null && dscpn.Tables.Count > 0)
        //    {
        //        DataTable dt = dscpn.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            long cId = Convert.ToInt64(dt.Rows[0]["menu_id"].ToString());
        //            long SbId = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

        //            string SndEmail = dt.Rows[0]["sender_email"].ToString();
        //            string RecEmail = dt.Rows[0]["reciever_email"].ToString();

        //            //Session["couponCodeName"] = txtCouponCode.Text;


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
        //                // lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
        //                // lblMsgCoupon.Text = "apply coupon code";
        //                Session["couponcodeprice"] = dt.Rows[0]["price"].ToString();
        //                //AddToShoppingCart();
        //            }
        //            else if (cId == 0 && SbId == 0)         //all product used coupon code
        //            {
        //                // lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
        //                // lblMsgCoupon.Text = "apply coupon code";
        //                Session["couponcodeprice"] = dt.Rows[0]["price"].ToString();
        //                //AddToShoppingCart();
        //            }
        //            else
        //            {
        //                //lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
        //                // lblMsgCoupon.Text = "Coupon Code is Invalid.";
        //                Session["couponcodeprice"] = null;
        //                //AddToShoppingCart();
        //            }
        //        }
        //        else
        //        {
        //            //lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
        //            // lblMsgCoupon.Text = "Coupon Code is Invalid.";
        //            Session["couponcodeprice"] = null;
        //            //AddToShoppingCart();
        //        }

        //        AddToShoppingCart();
        //    }

        //    // divCart.Attributes.Add("class", "speed-in");
        //}

        #endregion




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

        //protected void btngo_Click(object sender, EventArgs e)
        //{
        //   if (!string.IsNullOrEmpty(txtname.Text))
        //   Response.Redirect("~/category.aspx?s=" + txtname.Text.Trim());
        //}

        #region login/logout
        //protected void lbtnLogin_Click(object sender, EventArgs e)
        //{
        //    customer_handler customerHandler = new customer_handler();
        //    security scty = new security();
        //    string strpassword = scty.Encryptdata(txtLoginPwd.Text);
        //    BusinessEntities.customer customerData = new BusinessEntities.customer();
        //    customerData.email_id = txtLoginEmail.Text.Trim();
        //    customerData.password = strpassword;

        //    if (Page.IsValid)
        //    {
        //        int retFlag = customerHandler.get_customer_login(customerData);
        //        if (retFlag == 0)
        //        {
        //            lblLoginMsg.Text = "Login Failed, invalid UserId/Password";
        //            txtLoginPwd.Focus();
        //        }
        //        else
        //        {
        //            string path = HttpContext.Current.Request.Url.AbsolutePath;
        //            string url = HttpContext.Current.Request.Url.PathAndQuery;

        //            Session["CustomerLoginDetails"] = txtLoginEmail.Text.Trim();
        //            DataSet ds = customerHandler.check_customer_loginid(txtLoginEmail.Text);
        //            if (ds != null && ds.Tables.Count > 0)
        //            {
        //                DataTable dt = ds.Tables[0];
        //                if (dt.Rows.Count > 0)
        //                {
        //                    Session["CustomerLoginId"] = dt.Rows[0]["customer_id"].ToString();
        //                    Session["CutomerName"] = dt.Rows[0]["customer_name"];
        //                    Session["customerDetailsId"] = dt.Rows[0]["customer_id"].ToString();
        //                    Session["loginType"] = "register";
        //                    GetTempShoppingCart();

        //                }
        //            }
        //            if (Request.QueryString["url"] == null)
        //                Response.Redirect("~/category.aspx");
        //            else
        //                Response.Redirect(Request.QueryString["url"]);
        //        }
        //    }
        //}
        //protected void btnSignUp_Click(object sender, EventArgs e)
        //{
        //    Guid customer_id = Guid.Empty;
        //    security scty = new security();
        //    customer_handler customerHandler = new customer_handler();
        //    customer Customer = new customer();

        //    string strpassword = scty.Encryptdata(txtsignoutPassword.Text);



        //    Customer.customer_name = txtsignoutUserName.Text;
        //    Customer.email_id = txtsignoutEmail.Text;
        //    Customer.password = strpassword;
        //    Customer.contact_number = txtsignoutMobile.Text;

        //    bool result = customerHandler.insert_update_customer_login_details(Customer, ref customer_id);

        //    if (result)
        //    {


        //        Session["CustomerEmail"] = txtsignoutEmail.Text.Trim();
        //        Session["UserName"] = txtsignoutUserName.Text.Trim();
        //        Session["Mobile"] = txtsignoutMobile.Text;

        //        //// 5: Identify API - On new user registration.
        //        //string identifyapi = @"wigzo(""identify"", { email: " + Session["CustomerEmail"].ToString() + ",phone: " + Session["Mobile"].ToString() + ",fullName: " + Session["UserName"].ToString() + @" }); ";
        //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "identifyapi", identifyapi, true);

        //        DataSet ds = customerHandler.check_customer_loginid(txtsignoutEmail.Text);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                Session["CustomerLoginId"] = dt.Rows[0]["customer_id"].ToString();
        //                Session["CutomerName"] = dt.Rows[0]["customer_name"];
        //                Session["loginType"] = "register";
        //            }
        //        }

        //        DAL.Utility.SendSignupMail(txtsignoutUserName.Text.Trim(), txtsignoutEmail.Text.Trim(), txtsignoutPassword.Text.Trim(), txtsignoutEmail.Text.Trim());

        //        if (Request.QueryString["url"] == null)
        //            Response.Redirect("~/category.aspx");
        //        else
        //            Response.Redirect(Request.QueryString["url"]);
        //    }
        //}


        //protected void LogOut()
        //{
        //    Session.Clear();
        //    Session.Abandon();
        //    Response.Redirect("/", false);
        //}
        #endregion
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
                    // hfpersonaName.Value = dt.Rows[0]["customer_name"].ToString(); Comment by Chandni
                    // hfpersonamobile.Value = dt.Rows[0]["contact_number"].ToString();Comment by Chandni
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
           // security scty = new security();
            customer_handler customerHandler = new customer_handler();
            BusinessEntities.customer customer = new BusinessEntities.customer();

            customer.email_id = Session["CustomerLoginDetails"].ToString();
            //customer.customer_name = hfpersonaName.Value;
            //customer.contact_number = hfpersonamobile.Value;

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
        //protected void btnFbGoogle_Click(object sender, EventArgs e)
        //{
        //    //security scty = new security();
        //    customer_handler customerHandler = new customer_handler();
        //    BusinessEntities.customer customer = new BusinessEntities.customer();

        //    customer.customer_name = hfpersonaName.Value;
        //    if (string.IsNullOrEmpty(hfreceiveremail.Value))
        //        customer.email_id = hffbid.Value;
        //    else
        //        customer.email_id = hfreceiveremail.Value;

        //    DataSet ds = customerHandler.check_guest_loginid(customer.email_id);
        //    bool result = false;
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            customer.customer_id = Guid.Parse(dt.Rows[0]["customer_id"].ToString());

        //            Session["CustomerLoginDetails"] = customer.email_id;
        //            Session["customerDetailsId"] = customer.customer_id.ToString();
        //            Session["loginType"] = hfLoginType.Value;
        //            bindDeliveryDetails(Session["CustomerLoginDetails"].ToString());
        //            BillingDetails();
        //        }
        //        else
        //        {
        //            Guid customer_id = Guid.NewGuid();
        //            customer.customer_id = customer_id;

        //            if (hfLoginType.Value.Equals("fb"))
        //                result = customerHandler.insert_update_facebook_customer(customer, ref customer_id);
        //            else if (hfLoginType.Value.Equals("google"))
        //                result = customerHandler.insert_update_google_customer(customer, ref customer_id);

        //            if (result)
        //            {
        //                Session["CustomerLoginDetails"] = customer.email_id;
        //                Session["customerDetailsId"] = customer_id.ToString();
        //                Session["loginType"] = hfLoginType.Value;

        //                bindDeliveryDetails(Session["CustomerLoginDetails"].ToString());
        //                BillingDetails();
        //            }
        //        }
        //    }

        //    if (Request.QueryString["url"] == null)
        //        Response.Redirect("~/category.aspx");
        //    else
        //        Response.Redirect(Request.QueryString["url"]);
        //}
        //protected void lbtnForgotPassword_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtLoginEmail.Text))
        //    {
        //        lblMsg.ForeColor = System.Drawing.Color.Red;
        //        lblMsg.Text = "Please enter email address.";
        //        return;
        //    }
        //    customer_handler customerHandler = new customer_handler();
        //    customer Customer = new customer();
        //    string loginType;

        //    DataSet ds = customerHandler.get_customer_login_details(txtLoginEmail.Text.Trim());

        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        foreach (DataRow item in ds.Tables[0].Rows)
        //        {
        //            // loginType = item["login_type"].ToString();
        //            loginType = ds.Tables[0].Rows[0]["login_type"].ToString();

        //            if (loginType == "register")
        //            {
        //                security scty = new security();
        //                string pwd = scty.Decryptdata(ds.Tables[0].Rows[0]["password"].ToString());

        //                DAL.Utility.SendForgotPasswordMail(txtLoginEmail.Text, pwd, ds.Tables[0].Rows[0]["email_id"].ToString());

        //                lblMsg.ForeColor = System.Drawing.Color.Green;
        //                lblMsg.Text = "Account detail sent you on your email address. Please verify.";
        //                return;
        //            }
        //        }

        //        loginType = ds.Tables[0].Rows[0]["login_type"].ToString();
        //        if (loginType == "fb")
        //        {
        //            lblMsg.ForeColor = System.Drawing.Color.Red;
        //            lblMsg.Text = "Your login type is Facebook user. Please login with Facebook account.";
        //        }
        //        else if (loginType == "google")
        //        {
        //            lblMsg.ForeColor = System.Drawing.Color.Red;
        //            lblMsg.Text = "Your login type is google user. Please login with Google account.";
        //        }
        //        else if (loginType == "guest")
        //        {
        //            lblMsg.ForeColor = System.Drawing.Color.Red;
        //            lblMsg.Text = "Your login type is guest user. Please register before login.";
        //        }
        //    }
        //}
        //private bool GetTempShoppingCart()
        //{
        //    Boolean blnMatch = false;
        //    DataTable dtCart = (DataTable)Session["Cart"];
        //    //Start: Added By Hetal Patel on 21-08-2020 For Live Site issue resolution
        //    if (dtCart == null)
        //    {
        //        DataTable dtCart1 = new DataTable("Cart");
        //        dtCart1.Columns.Add("menu_name", typeof(string));
        //        dtCart1.Columns.Add("sub_menu_name", typeof(string));
        //        dtCart1.Columns.Add("child_name", typeof(string));
        //        dtCart1.Columns.Add("product_id", typeof(Int64));
        //        dtCart1.Columns.Add("product_name", typeof(string));
        //        dtCart1.Columns.Add("thumb_image", typeof(string));
        //        dtCart1.Columns.Add("weight", typeof(string));
        //        dtCart1.Columns.Add("size", typeof(string));
        //        dtCart1.Columns.Add("color_name", typeof(string));
        //        dtCart1.Columns.Add("sale_price", typeof(decimal));
        //        dtCart1.Columns.Add("discount", typeof(decimal));
        //        dtCart1.Columns.Add("coupon_discount", typeof(decimal));
        //        dtCart1.Columns.Add("quantity", typeof(Int32));
        //        dtCart1.Columns.Add("Total", typeof(decimal));
        //        dtCart1.Columns.Add("shipping_price", typeof(decimal));
        //        dtCart1.Columns.Add("gendertype", typeof(byte));
        //        dtCart1.Columns.Add("custom_bag_param", typeof(string));
        //        dtCart1.Columns.Add("custom_bag_price", typeof(decimal));
        //        dtCart1.Columns.Add("x_point", typeof(float));
        //        dtCart1.Columns.Add("y_point", typeof(float));
        //        dtCart1.Columns["quantity"].DefaultValue = 1;
        //        dtCart1.Columns["product_id"].Unique = true;
        //        Session["Cart"] = dtCart1;
        //        dtCart = dtCart1;
        //    }
        //    //End: Added By Hetal Patel on 21-08-2020 For Live Site issue resolution
        //    temp_cart_handler productIDHandler = new temp_cart_handler();
        //    DataSet dsProductIds = productIDHandler.get_temp_customer_cart(txtLoginEmail.Text.Trim());
        //    int product_id;

        //    if (dsProductIds != null && dsProductIds.Tables.Count > 0)
        //    {
        //        DataTable dt = dsProductIds.Tables[0];

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            product_id = int.Parse(dt.Rows[i]["product_id"].ToString());
        //            BindProduct(product_id);
        //            foreach (DataRow row in dtCart.Rows)
        //            {
        //                if (int.Parse(row["product_id"].ToString()) == product_id)
        //                {
        //                    row["quantity"] = (int)row["quantity"] + 1;

        //                    Decimal price = Convert.ToDecimal(row["sale_price"]);
        //                    Decimal discount = Convert.ToDecimal(row["discount"]);
        //                    Decimal UnitPriceOnDiscount = discount > 0 ? price - (price * discount / 100) : price;

        //                    row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
        //                    Session["Cart"] = dtCart;
        //                    blnMatch = true;
        //                    break;
        //                }
        //            }

        //            if (!blnMatch)
        //            {
        //                if (ViewState["ProductDetails"] != null)
        //                {
        //                    DataRow drCart = dtCart.NewRow();
        //                    DataTable dtt = (DataTable)ViewState["ProductDetails"];
        //                    drCart["product_id"] = product_id;
        //                    drCart["product_name"] = dtt.Rows[0]["product_name"].ToString();
        //                    drCart["thumb_image"] = "images/Product/Thumb/" + dtt.Rows[0]["thumb_image"].ToString();
        //                    drCart["menu_name"] = dtt.Rows[0]["menu_name"].ToString();
        //                    drCart["sub_menu_name"] = dtt.Rows[0]["sub_menu_name"].ToString();
        //                    drCart["child_name"] = dtt.Rows[0]["child_name"].ToString();
        //                    drCart["weight"] = dtt.Rows[0]["weight"].ToString();
        //                    drCart["gendertype"] = dtt.Rows[0]["gendertype"];

        //                    drCart["size"] = dtt.Rows[0]["size"].ToString();

        //                    if (Session["size"] != null)
        //                    {
        //                        drCart["size"] = Session["size"].ToString();
        //                    }

        //                    drCart["color_name"] = dtt.Rows[0]["color_name"].ToString();
        //                    Decimal price = Convert.ToDecimal(dtt.Rows[0]["Price"].ToString());
        //                    Decimal discount = Convert.ToDecimal(dtt.Rows[0]["discount"].ToString());
        //                    Decimal TotalPrice = discount > 0 ? price - (price * discount / 100) : price;

        //                    Session["sale_discount"] = String.Format("{0:0.00}", (price - TotalPrice));

        //                    drCart["sale_price"] = price;
        //                    drCart["discount"] = discount;
        //                    drCart["coupon_discount"] = 0;
        //                    drCart["custom_bag_price"] = 0;
        //                    drCart["quantity"] = 1;
        //                    drCart["Total"] = Convert.ToInt32(1) * TotalPrice;
        //                    dtCart.Rows.Add(drCart);


        //                }
        //            }
        //        }
        //        Session["Cart"] = dtCart;
        //    }
        //    return true;
        //}
        //private void BindProduct(int product_id)
        //{
        //    product_handler productHandler = new product_handler();
        //    DataSet dsProducts = productHandler.get_product_details(product_id);


        //    if (dsProducts != null && dsProducts.Tables.Count > 0)
        //    {
        //        DataTable dt = dsProducts.Tables[0];

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            ViewState["ProductDetails"] = dt;
        //        }

        //        DataTable dtCart = (DataTable)Session["Cart"];


        //    }
        //}

        protected void lbtngo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            Response.Redirect("~/category.aspx?s=" + txtSearch.Text.Trim());
        }

        protected void lbtngop_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtname.Text))
                Response.Redirect("~/category.aspx?s=" + txtname.Text.Trim());
        }

        protected void rptCartPc_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
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
            catch(Exception )
            {
                Response.Redirect("/", false);
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
                        lblMarquee1.Text = dt.Select("label_name='Marquee1'")[0]["label_value"].ToString();
                        lblMarquee2.Text = dt.Select("label_name='Marquee2'")[0]["label_value"].ToString();
                    }
                }
                else
                { }
            }
            catch (Exception)
            {
                //If any error then ignore
            }
        }
    }
}