using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using BLL;
using BusinessEntities;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Mail;
using System.Collections.Specialized;
using CCA.Util;
using Razorpay.Api;
using System.Web.UI.HtmlControls;
using System.Web.Services;

namespace strutt
{
    public partial class proceedtopayment : System.Web.UI.Page
    {
        string msgbody = "";
        string thumbimg = "";
        long proid = 0;
        int qty = 0;
        int txtqty = 1;
        int qtycount = 0;
        string fxCountCode = "";
        //string ordPrifix = "STR";

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Testing purpose. To write log in text file
            //UtilityLog.LogFile = Server.MapPath("~/App_Data/log.txt");
            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart == null || dtCart.Rows.Count == 0)
                Response.Redirect("~/");
            if (!IsPostBack)
            {
                headReceiverAddress.Visible = false;
                if (Session["CustomerData"] == null)
                {
                    DataTable dtCustomerData = new DataTable("CustomerData");
                    dtCustomerData.Columns.Add("customer_id", typeof(Guid));
                    dtCustomerData.Columns.Add("full_name", typeof(string));
                    dtCustomerData.Columns.Add("Email", typeof(string));
                    dtCustomerData.Columns.Add("contact_number", typeof(string));

                    Session["CustomerData"] = dtCustomerData;
                }
                this.bindState();
               
                if (Session["CustomerLoginDetails"] != null)
                {
                    if (Convert.ToString(Session["loginType"]) == "register" || Convert.ToString(Session["loginType"]) == "fb" || Convert.ToString(Session["loginType"]) == "google")
                    {
                        txtguestemail.Text = Session["CustomerLoginDetails"].ToString();
                        bindDeliveryDetails(Session["CustomerLoginDetails"].ToString());
                        BillingDetails();
                        txtguestemail.Enabled = false;
                    }
                    //else
                    //{
                    //   // divAddNewAddress.Attributes.Add("class", "templates");   // Show
                    //    this.bindState();
                    //}
                    //lblCurrentLoginId.Text = "Signed in as " + Session["CustomerLoginDetails"].ToString();
                    btnChangeUserDetails.Visible = true;
                    divLogin.Style.Add("display", "none");
                    lblhead1.Text = "Signed in as " + Session["CustomerLoginDetails"].ToString();
                    //lblCurrentLoginId.Visible = true;
                    //btnChangeLogin.Visible = true;
                    //btnLogin.Visible = false;
                    // addAddress.Visible = true;
                    btnPlaceOrder.Enabled = true;
                    insertTempCart();
                }
                else
                {
                    btnChangeUserDetails.Visible = false;
                    divLogin.Style.Add("display", "block");
                    //lblCurrentLoginId.Text = "Login not found.";
                    lblhead1.Text = "";
                    //btnChangeLogin.Visible = false;
                    //btnLogin.Visible = true;
                    //addAddress.Disabled = true;
                    btnPlaceOrder.Enabled = false;
                }
                //if (Session["CustomerLoginDetails"] == null)
                //{
                //    // lbllogin.Text = "To continue process, please login or checkout as guest.";
                //}

                int TotalQty = 0;
                TotalQty = (from DataRow drCart in dtCart.AsEnumerable() select (Convert.ToInt32(drCart["quantity"]))).Sum();

                if (TotalQty >= 2)
                {
                    AddToShoppingCart(TotalQty);

                }
                else
                {
                    if (Session["couponCodeName"] != null)
                    {
                        txtCouponCode.Text = Session["couponCodeName"].ToString();
                        lbtnApply_Click(lbtnApply, null);
                    }
                    AddToShoppingCart(TotalQty);
                }
                SetActiveSteps();
                otpbox.Visible = false;
                if (Session["couponCodeName"] != null)
                {
                    txtCouponCode.Enabled = false;
                    txtCouponCode.Text = Session["couponCodeName"].ToString();
                    lbtnApply.Enabled = false;
                }
                getText();
                this.Checkout();
                txtreceivername.Focus();
            }
        }
        #endregion

        #region login
        protected void btnLogin_Click(object sender, EventArgs e)
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
                    lblLoginMsg.Text = "Login Failed, invalid UserId/Password.";
                    txtLoginPwd.Focus();
                }
                else
                {
                    //string path = HttpContext.Current.Request.Url.AbsolutePath;
                    //string url = HttpContext.Current.Request.Url.PathAndQuery;

                    Session["CustomerLoginDetails"] = txtLoginEmail.Text.Trim();
                    DataSet ds = customerHandler.check_customer_loginid(txtLoginEmail.Text);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            Session["CustomerLoginEmailId"] = dt.Rows[0]["email_id"].ToString();
                            Session["CustomerLoginId"] = dt.Rows[0]["customer_id"].ToString();
                            Session["CutomerName"] = dt.Rows[0]["customer_name"];
                            Session["customerDetailsId"] = dt.Rows[0]["customer_id"].ToString();
                            Session["loginType"] = "register";
                            GetTempShoppingCart();

                        }
                    }
                    // ABhay : 11-June-2021
                    //if (Request.QueryString["url"] != null)
                    //    Response.Redirect(Request.QueryString["url"]);

                    if (ViewState["PrevUrl"] != null)
                        Response.Redirect(ViewState["PrevUrl"].ToString());

                    else
                        Response.Redirect("~/proceedtopayment.aspx");
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

                            //drCart["custsom_image"] = dtt.Rows[0]["child_name"].ToString();


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

        #endregion

        #region Guest Details

        protected bool GuestUserCheck()
        {
            Guid customer_id = Guid.NewGuid();
            bool result = false;

            //security scty = new security();
            customer_handler customerHandler = new customer_handler();
            BusinessEntities.customer customer = new BusinessEntities.customer();

            //customer.full_name = txtGuestUserName.Text;
            customer.email_id = txtguestemail.Text;
            customer.customer_id = customer_id;

            DataSet ds = customerHandler.check_guest_loginid(txtguestemail.Text);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string loginType = dt.Rows[0]["login_type"].ToString();
                    Session["loginType"] = loginType;
                    customer_id = Guid.Parse(dt.Rows[0]["customer_id"].ToString());
                    customer.customer_id = customer_id;

                    if (loginType == "register")
                    {
                        lblMsgGuest.Text = "This email is already registered, please choose another one.";
                    }
                    else
                    {
                        //result = customerHandler.insert_update_guest_customer(customer, ref customer_id);
                        //if (result)
                        //{

                        result = true;
                        Session["CustomerLoginDetails"] = txtguestemail.Text;
                        Session["customerDetailsId"] = customer_id.ToString();

                        //bindDeliveryDetails(Session["CustomerLoginDetails"].ToString());
                        //BillingDetails();
                        //}
                    }
                }
                else
                {
                    result = customerHandler.insert_update_guest_customer(customer, ref customer_id);
                    if (result)
                    {
                        Session["CustomerLoginDetails"] = txtguestemail.Text;
                        //Session["cusDetailsId"] = customer_id.ToString();
                        Session["customerDetailsId"] = customer_id.ToString();

                        //bindDeliveryDetails(Session["CustomerLoginDetails"].ToString());
                        //BillingDetails();
                    }
                    else
                    {
                        lblMsgGuest.Text = "This email is already registered, please choose another one.";
                    }
                }

                if (result)
                {
                    //lblCurrentLoginId.Text = "Signed in as " + Session["CustomerLoginDetails"].ToString();
                    lblhead1.Text = "Signed in as " + Session["CustomerLoginDetails"].ToString();
                    //lblCurrentLoginId.Visible = true;
                    //btnChangeLogin.Visible = true;
                    //btnLogin.Visible = false;

                    lbllogin.Text = "";
                    //addAddress.Visible = true;
                    btnPlaceOrder.Enabled = true;
                    //Response.Redirect("proceedtopayment.aspx");
                    //divAddNewAddress.Attributes.Add("class", "templates"); 
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //SetActiveSteps();
            return false;
        }

        //protected void btnChangeLogin_Click(object sender, EventArgs e)
        //{
        //    Session.Remove("CustomerLoginDetails");
        //    Session.Remove("customerDetailsId");
        //    txtguestemail.Text = "";
        //    //lblreceiverName.Text = "";
        //    //lblreceiverAddress.Text = "";
        //    //lblreceiverCity.Text = "";
        //    //lblreceiverState.Text = "";
        //    //lblreceiverPincode.Text = "";
        //    //lblreceiverMobile.Text = "";

        //    lblCurrentLoginId.Text = "Login not found.";
        //    lblhead1.Text = "";
        //   lblhead2.Text = "";
        //    lblCurrentLoginId.Visible = true;
        //    btnChangeLogin.Visible = false;
        //    btnLogin.Visible = true;
        //  //divAddNewAddress.Attributes.Add("class", "templates hidden");   // Hide

        //    lbllogin.Text = "";
        //    //addAddress.Disabled = true;
        //    btnPlaceOrder.Enabled = false;

        //    //repReceiverAddress.DataSource = null;
        //    //repReceiverAddress.DataBind();

        //    txtguestemail.Focus();
        //    SetActiveSteps();
        //}

        #endregion

        #region Billing Details

        private void GetBillingDetails()
        {
            if (Session["CustomerData"] != null)
            {
                DataTable dtCustomerData = (DataTable)Session["CustomerData"];
                Session["customerDetailsId"] = dtCustomerData.Rows[0]["customer_id"].ToString();
                hfpersonaName.Value = dtCustomerData.Rows[0]["full_name"].ToString();
                hfpersonamobile.Value = dtCustomerData.Rows[0]["contact_number"].ToString();
                this.bindState();
               
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

                    // PanelLogin(); //close Pick The Most Convenient Option and open Billing Details.
                    this.GetBillingDetails();
                }
            }
            else
            {
                lblSignupMsg.Text = "This email/mobile is already registered, please choose another one.";
            }
            //Billing();   //close Billing Details and open Delivery Details.
            //PanelAddress();
            BindReceiverAddressData(Guid.Parse(Session["customerDetailsId"].ToString()));


        }



        #endregion

        #region Delivery Details

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
                }
            }
        }

        //protected void btnChangeOrderSummary_Click(object sender, EventArgs e)
        //{
        //    //  this.PanelPayment();
        //    this.AddToShoppingCart();
        //}

        #region state/city/pincode
        private void bindState()
        {
            pincode_handler pincodeHandler = new pincode_handler();
            DataSet ds = pincodeHandler.get_pincode_search("", "", "", 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlReceiverState.DataSource = dt;
                    ddlReceiverState.DataBind();
                    ddlReceiverState.Items.Insert(0, "Select State");
                    ddlReceiverCity.Items.Insert(0, "Select City");
                }
                else
                {
                    ddlReceiverState.Items.Clear();
                    ddlReceiverState.Items.Insert(0, "Select State");
                    ddlReceiverCity.Items.Clear();
                    ddlReceiverCity.Items.Insert(0, "Select City");
                }
            }
        }

        protected void ddlReceiverState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReceiverState.SelectedValue == "Select State")
            {
                ddlReceiverCity.Items.Clear(); 
                ddlReceiverCity.Items.Insert(0, "Select City");
            }
            else
            {
                string statenamne = ddlReceiverState.SelectedValue;
                this.BindCity(statenamne);
            }
        }

        private void BindCity(string statename)
        {
            pincode_handler pincodeHandler = new pincode_handler();
            DataSet ds = pincodeHandler.get_pincode_search(statename, "", "", 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlReceiverCity.DataSource = ds.Tables[0];
                ddlReceiverCity.DataBind();
                ddlReceiverCity.Items.Insert(0, "Select City");
            }
            else
            {
                ddlReceiverCity.Items.Clear();
                ddlReceiverCity.Items.Insert(0, "Select City");
            }
        }

        #endregion

        protected void btnProceedToPayment_Click(object sender, EventArgs e)  ////////////Delivery Details Button
        {
            if (Session["CustomerLoginDetails"] == null)
            {
                if (!GuestUserCheck())
                    return;
            }
            receiver_address receiverAddress = new receiver_address();

            receiverAddress.customer_details_id = 0;
            receiverAddress.customer_id = Guid.Parse(Session["customerDetailsId"].ToString());
            receiverAddress.full_name = txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : " " + txtreceiverlastname.Text);
            receiverAddress.contact_number = txtreceiverphone.Text;
            receiverAddress.email_id = Session["CustomerLoginDetails"].ToString();
            receiverAddress.address = txtreceiveraddress.Text;
            receiverAddress.land_mark = txtLandMark.Text;
            receiverAddress.city = ddlReceiverCity.SelectedValue;
            receiverAddress.state = ddlReceiverState.SelectedValue;
            receiverAddress.pin_code = txtReceiverPinCode.Text;

            receiver_address_handler receiverAddressHandler = new receiver_address_handler();
            //if (Session["customer_details_id"] != null)
            //{
            //    // this.PanelOrderSummary();  //close Delivery Details and open Order Summary.
            //    this.AddToShoppingCart();
            //}
            //else
            //{

            if (ViewState["Adrs_Added"] == null)
            {
                bool result = receiverAddressHandler.insert_customer_address(receiverAddress);

                if (Session["CutomerName"] == null || string.IsNullOrEmpty(Convert.ToString(Session["CutomerName"])))
                    Session["CutomerName"] = txtreceivername.Text;

                if (result)
                {
                    //  this.PanelOrderSummary();  //close Delivery Details and open Order Summary.
                    //this.AddToShoppingCart();
                }
            }
            //}
            //lblreceiverName.Text = txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : " " + txtreceiverlastname.Text);
            //lblreceiverAddress.Text = txtreceiveraddress.Text + (string.IsNullOrEmpty(txtLandMark.Text) ? "" : ", " + txtLandMark.Text);
            //lblreceiverCity.Text = ", " + ddlReceiverCity.SelectedValue;
            //lblreceiverState.Text = ddlReceiverState.SelectedValue;
            //lblreceiverPincode.Text = "- " + txtReceiverPinCode.Text;
            //lblreceiverMobile.Text = txtreceiverphone.Text;

            //lblhead2.Text = txtreceiveraddress.Text + ", " + ddlReceiverCity.SelectedValue + ", " + ddlReceiverState.SelectedValue + " - " + txtReceiverPinCode.Text;

            //divAddNewAddress.Attributes.Add("class", "templates");   // Hide

            if (Convert.ToString(Session["loginType"]) == "register")
            {
                DataSet dsReceiverAddress = receiverAddressHandler.get_customer_address(Guid.Parse(Session["customerDetailsId"].ToString()));
                //repReceiverAddress.DataSource = dsReceiverAddress.Tables[0];
                //repReceiverAddress.DataBind();
            }
            else
                updateTempCart();

            //This script can be execute only for new user. Right now execute for all user. (Just for tesing I have added without condition)

            string identifyapi = @"wigzo(""identify"", { email: """ + Session["CustomerLoginDetails"].ToString() + @""",phone: """
                + txtreceiverphone.Text + @""",fullName: """ + txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : txtreceiverlastname.Text) + @""" }); ";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "identifyapi", identifyapi, true);


            ViewState["steps"] = "2";
            collapseThree.Attributes.Add("class", "collapse show");
            collapseOne.Attributes.Add("class", "collapse");
            //SetActiveSteps();
            headingThree.Visible = true;
        }

        private void BindReceiverAddressData(Guid customer_id)   //Saved Addresses in Delivery Details
        {
            receiver_address_handler receiverAddressHandler = new receiver_address_handler();
            DataSet dsReceiverAddress = receiverAddressHandler.get_customer_address(customer_id);
            if (dsReceiverAddress.Tables[0].Rows.Count > 0)
            {
                DataTable dtrece = dsReceiverAddress.Tables[0];
               
                headReceiverAddress.Visible = true;
                repReceiverAddress.DataSource = dtrece;
                repReceiverAddress.DataBind();
               
                //lblreceiverName.Text = dtrece.Rows[0]["full_name"].ToString();
                //lblreceiverAddress.Text = dtrece.Rows[0]["address"].ToString();
                //lblreceiverCity.Text = ", " + dtrece.Rows[0]["city"].ToString();
                //lblreceiverState.Text = dtrece.Rows[0]["state"].ToString();
                //lblreceiverPincode.Text = "- " + dtrece.Rows[0]["pin_code"].ToString();
                //lblreceiverMobile.Text = dtrece.Rows[0]["contact_number"].ToString();

                //divAddNewAddress.Attributes.Add("class", "templates hidden");   // Hide

                // lblhead2.Text = lblreceiverAddress.Text + ", " + lblreceiverCity.Text + ", " + lblreceiverState.Text + " - " + lblreceiverPincode.Text;
            }
            else
            {

                headReceiverAddress.Visible = false; 
                repReceiverAddress.DataSource = null;
                repReceiverAddress.DataBind();

                //repReceiverAddress.DataSource = null;
                //repReceiverAddress.DataBind();

                //lblreceiverName.Text = "";
                //lblreceiverAddress.Text = "";
                //lblreceiverCity.Text = "";
                //lblreceiverState.Text = "";
                //lblreceiverPincode.Text = "";
                //lblreceiverMobile.Text = "";
                // divAddNewAddress.Attributes.Add("class", "templates");   // Show
                //  lblhead2.Text = "";

                // ClearAddress();
            }
            
        }

        //private void ClearAddress()
        //{
        //    txtreceivername.Text = string.Empty;
        //    txtreceiverlastname.Text = string.Empty;
        //    txtreceiverphone.Text = string.Empty;
        //    txtreceiveraddress.Text = string.Empty;
        //    txtLandMark.Text = string.Empty;
        //    ddlReceiverState.SelectedIndex = -1;
        //    ddlReceiverCity.SelectedIndex = -1;
        //    txtReceiverPinCode.Text = string.Empty;
        //    txtreceivermessage.Text = string.Empty;
        //}

        protected void repReceiverAddress_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == "Update")
            //{

            //}
            //else if (e.CommandName == "Remove")
            //{
            //    if (Session["Cart"] != null)
            //    {
            //        Int64 customer_details_id = Convert.ToInt64(e.CommandArgument.ToString());
            //        receiver_address_handler receiverAddressHandler = new receiver_address_handler();
            //        DataSet dsReceiverAddress = receiverAddressHandler.delete_save_customer_address(customer_details_id);
            //        BindReceiverAddressData(Guid.Parse(Session["customerDetailsId"].ToString()));
            //    }
            //    //SetActiveSteps();
            //}
            if (e.CommandName == "useThisAddress")
            {
                ViewState["Adrs_Added"] = "Old";
                Int64 customer_details_id = Convert.ToInt64(e.CommandArgument.ToString());
                receiver_address_handler receiverAddressHandler = new receiver_address_handler();
                DataSet dsReceiverAddress = receiverAddressHandler.get_save_customer_address(customer_details_id);
                if (dsReceiverAddress != null && dsReceiverAddress.Tables.Count > 0)
                {
                    DataTable dt = dsReceiverAddress.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //Session["customer_details_id"] = customer_details_id.ToString();
                        Session["email_id"] = dt.Rows[0]["email_id"].ToString();
                        string fullName = dt.Rows[0]["full_name"].ToString();
                        if (fullName.Contains(' '))
                        {
                            txtreceivername.Text = fullName.Split(' ').GetValue(0).ToString();
                            txtreceiverlastname.Text = fullName.Split(' ').GetValue(1).ToString();
                        }
                        else
                            txtreceivername.Text = fullName;

                        txtreceiverphone.Text = dt.Rows[0]["contact_number"].ToString();
                        txtreceiveraddress.Text = dt.Rows[0]["address"].ToString();
                        //txtreceiveraddress1.Text = dt.Rows[0]["Address2"].ToString();
                        txtLandMark.Text = dt.Rows[0]["land_mark"].ToString();

                        string statename = dt.Rows[0]["State"].ToString();
                       // string statename = dt.Rows[0]["state"].ToString();
                        ddlReceiverState.SelectedValue = statename.ToString();

                        BindCity(statename);


                        //string cityname = dt.Rows[0]["city"].ToString();
                        //ddlReceiverCity.SelectedValue = cityname.ToString();
                        //ddlReceiverCity.SelectedValue = dt.Rows[0]["city"].ToString();

                        //If city not match from old address then not set any value.
                        try
                        {
                            ddlReceiverCity.SelectedValue = dt.Rows[0]["city"].ToString(); }
                        catch { }

                        txtReceiverPinCode.Text = dt.Rows[0]["pin_code"].ToString();
                        //txtreceivermessage.Text = dt.Rows[0]["message"].ToString();

                        //this.AddToShoppingCart();

                        //lblreceiverName.Text = dt.Rows[0]["full_name"].ToString();
                        //lblreceiverAddress.Text = dt.Rows[0]["address"].ToString();
                        //lblreceiverCity.Text = ", " + dt.Rows[0]["city"].ToString();
                        //lblreceiverState.Text = dt.Rows[0]["state"].ToString();
                        //lblreceiverPincode.Text = "- " + dt.Rows[0]["pin_code"].ToString();
                        //lblreceiverMobile.Text = dt.Rows[0]["contact_number"].ToString();

                        //divAddNewAddress.Attributes.Add("class", "templates hidden");   // Hide

                        //lblhead2.Text = lblreceiverAddress.Text + ", " + lblreceiverCity.Text + ", " + lblreceiverState.Text + " - " + lblreceiverPincode.Text;
                    }
                }
                updateTempCart();
                //SetActiveSteps();
                //5: Identify API - On new user registration.

                //string identifyapi = @"wigzo(""identify"", { email: """ + Session["CustomerLoginDetails"].ToString() + @""",phone: """ 
                //    + txtreceiverphone.Text + @""",fullName: """ + txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : txtreceiverlastname.Text) + @""" }); ";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "identifyapi", identifyapi, true);
            }
            btnProceedToPayment.Focus();
        }  ////////// use this address button in Delivery Details

        #endregion

        #region Order summary

        /// <summary>
        /// Added strCustom condition by Hetal Patel on 04-03-2020 to disable COD Button
        /// </summary>
        /// <param name="TotalQty"></param>
        private void AddToShoppingCart(int TotalQty)  ///////////////bind product Details in Order Summary
        {
            string ProductIds = "";
            string StrCustom = "";
            //byte CouponGenderType = 0;
            //Session["CouponAmount"] = "0";
            lblQtyCount.Text = TotalQty.ToString(); //Updated Hetal
            DataTable dtCart = (DataTable)Session["Cart"];
            //////DataTable dtCoupon = null;
            if (dtCart != null && dtCart.Rows.Count > 0)
            {
                ViewState["FirstProId"] = dtCart.Rows[0]["product_id"].ToString();
                ViewState["proName"] = dtCart.Rows[0]["product_name"].ToString();
                ViewState["proQty"] = dtCart.Rows[0]["quantity"].ToString();
                // ViewState["Price"] = dtCart.Rows[0]["price"].ToString();

                ViewState["thumbImg"] = dtCart.Rows[0]["thumb_image"].ToString();
                lblCartCount.Text = dtCart.Rows.Count.ToString();
                StrCustom = dtCart.Rows[0]["custom_bag_param"].ToString();

                if (!String.IsNullOrEmpty(StrCustom) || StrCustom.ToString() != "")
                {

                    rbtnCashOnDelivery.Checked = false;

                    rbtnCashOnDelivery.Enabled = false;
                    rbtnDebitCreditCard.Checked = true;
                    divCod.Visible = true;

                }



                //SubTotal Amount
                Decimal amount = (from DataRow drCart in dtCart.AsEnumerable()
                                  where drCart.RowState != DataRowState.Deleted
                                  select (Convert.ToDecimal(drCart["Total"]))).Sum();

                Decimal totalAmount = (from DataRow drCart in dtCart.AsEnumerable()
                                       where drCart.RowState != DataRowState.Deleted
                                       select (Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();
                lblTotalPrice.Text = String.Format("{0:0.00}", totalAmount);

                decimal salesPrice = (from DataRow drCart in dtCart.AsEnumerable()
                                      where drCart.RowState != DataRowState.Deleted
                                      select (Convert.ToDecimal(drCart["sale_price"]))).Sum();
                //Chandni 24-Dec-2020
                //decimal DiscountPer = 0;

                //// We will implement in future. Some category exclude from Qty based discount
                //string ExcludeCategory = ConfigurationManager.AppSettings["excludeCategoryFromDiscount"];
                //if (ExcludeCategory.Split(',').Where(r => r == "2").ToList().Count == 0)
                //{
                //}
                //Commnet becaouse client said: Kalpesh-29-May-2020
                //// On Product get 20 % and 30 % offer for 3/4 Days
                //if (TotalQty == 2)
                //{
                //    DiscountPer = Convert.ToDecimal(ConfigurationManager.AppSettings["disconton2qty"]);
                //    lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                //    lblMsgCoupon.Text = "Congratulations ! You get " + DiscountPer.ToString() + "% Discount.";
                //    txtCouponCode.Enabled = false;
                //    lbtnApply.Enabled = false;
                //}
                //else if (TotalQty > 2)
                //{
                //    DiscountPer = Convert.ToDecimal(ConfigurationManager.AppSettings["disconton3qty"]);
                //    lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                //    lblMsgCoupon.Text = "Congratulations ! You get " + DiscountPer.ToString() + "% Discount.";
                //    txtCouponCode.Enabled = false;
                //    lbtnApply.Enabled = false;
                //}

                decimal TotalDiscount = 0;
                //if (TotalQty == 1)
                //{
                decimal FlatDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                                        where drCart.RowState != DataRowState.Deleted
                                        select ((Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["discount"]) / 100) * Convert.ToDecimal(drCart["quantity"]))).Sum();

                decimal couponDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                                          where drCart.RowState != DataRowState.Deleted
                                          select (Convert.ToDecimal(drCart["coupon_discount"]))).Sum();
                // TotalDiscount = couponDiscount;
                TotalDiscount = FlatDiscount + couponDiscount;
                //  lblQtyCount.Text = TotalQty.ToString();
                //}
                //else
                //{
                //    TotalDiscount = (totalAmount * DiscountPer) / 100;
                //}
                ViewState["salPrice"] = salesPrice;

                lblDiscount.Text = String.Format("{0:0.00}", TotalDiscount);
                Session["CouponAmount"] = TotalDiscount;


                decimal totalCustomeBagCharge = (from DataRow drCart in dtCart.AsEnumerable()
                                                 where drCart.RowState != DataRowState.Deleted
                                                 select (Convert.ToDecimal(drCart["custom_bag_price"]))).Sum();
                lblCustomBagCharge.Text = String.Format("{0:0.00}", totalCustomeBagCharge);

                if (totalAmount - TotalDiscount > 750)
                    lblShipping.Text = "0.00";
                else
                    lblShipping.Text = String.Format("{0:0.00}", Convert.ToDecimal(ConfigurationManager.AppSettings["shippingcharge"]));


                ViewState["ShippingCharge"] = lblShipping.Text;


                Decimal grandTotal = totalAmount - TotalDiscount + Convert.ToDecimal(lblShipping.Text) + totalCustomeBagCharge;

                // This is creating problen Session["onlinegrdTotal"] or Session["onlineDiscount"]
                //if (!rbtnCashOnDelivery.Checked)
                //{
                //    grandTotal = Convert.ToDecimal(Session["onlinegrdTotal"].ToString());
                //    TotalDiscount = Convert.ToDecimal(Session["onlineDiscount"].ToString());
                //}
                if (!rbtnCashOnDelivery.Checked)
                {  
                    if (Session["onlinegrdTotal"] != null)
                        grandTotal = Convert.ToDecimal(Session["onlinegrdTotal"].ToString());
                    if (Session["onlineDiscount"] != null)
                        TotalDiscount = Convert.ToDecimal(Session["onlineDiscount"].ToString());
                }

                lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
                Session["grandTotal"] = grandTotal;

                Int64 TotPrice = Convert.ToInt64(grandTotal);
                Int64 FixPrice = 5000;

                if (TotPrice < FixPrice)
                {
                    if (!String.IsNullOrEmpty(StrCustom) || StrCustom.ToString() != "")
                    {

                        rbtnCashOnDelivery.Checked = false;

                        rbtnCashOnDelivery.Enabled = false;
                        rbtnDebitCreditCard.Checked = true;
                        divCod.Visible = true;

                    }
                    else
                    {
                        //rbtnCashOnDelivery.Checked = true;
                        rbtnCashOnDelivery.Enabled = true;
                        rbtnCashOnDelivery.Checked = true;
                        //rbtnCashOnDelivery.Visible = true;
                        lblCODMsg.Visible = false;
                    }
                    
                }
                else
                {
                    rbtnCashOnDelivery.Enabled = false;
                    rbtnCashOnDelivery.Checked = false;
                    //rbtnCashOnDelivery.Visible = false;
                    lblCODMsg.Visible = true;
                }

                btnPlaceOrder.Text = String.Format("Pay Rs.{0:0.00}", grandTotal);
                Session["grdTotal"] = lblGrandTotal.Text.ToString();
                //Session["grandTotal"] = lblGrandTotal.Text.ToString();
                //Session["CouponAmount"] = lblDiscount.Text.ToString();


                int product_id = Convert.ToInt32(ViewState["FirstProId"].ToString());

                foreach (DataRow row in dtCart.Rows)
                {
                    ProductIds += "'" + row["product_id"] + "',";
                    if (int.Parse(row["product_id"].ToString()) == product_id)
                    {
                        //row["Total"] = lblGrandTotal.Text;
                        row["shipping_price"] = ViewState["ShippingCharge"].ToString();
                    }
                }


                // Chandni 6-April-2021 Comment
                //Decimal TempTotal;
                //TempTotal = Convert.ToDecimal(lblGrandTotal.Text);
                //Response.Write(TempTotal);
               // Response.End();
                //if (TempTotal >= 5000)
                //{

                //    rbtnCashOnDelivery.Enabled = false;
                //    rbtnDebitCreditCard.Checked = true;
                //    rbtnCashOnDelivery.Checked = false;
                //}
                // Chandni 6-April-2021

                dlCart.DataSource = dtCart;
                dlCart.DataBind();
                //lbl3OrderSummary.Text = "Total Amount Payable: Rs. " + lblGrandTotal.Text;

                string FacebookPixelCode = @"
                fbq('track', 'InitiateCheckout', {
                content_ids: [" + ProductIds.Remove(ProductIds.Length - 1) + @"],
                content_type: 'product',
                value: " + lblGrandTotal.Text + @",
                currency: 'INR'
                });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "FacebookPixelCode", FacebookPixelCode, true);
            }
            else if (dtCart != null && dtCart.Rows.Count == 0)
            {
                dlCart.DataSource = dtCart;
                dlCart.DataBind();

                //lbl3OrderSummary.Text = "Total Amount Payable: Rs. " + lblGrandTotal.Text;
                btnPlaceOrder.Text = "Pay";
                lblTotalPrice.Text = "0";
                lblShipping.Text = "0";
                lblDiscount.Text = "0";
                lblGrandTotal.Text = "0";
            }
        }

        private Decimal GetTaxAmount(Decimal subTotal, Decimal taxPercentage)
        {
            Decimal IA = (subTotal * 100) / (100 + taxPercentage);
            Decimal TA = (taxPercentage / 100) * IA;
            return TA;
        }

        protected string GetDiscountDetails(Decimal discountPrice)
        {
            string discountDetails = string.Empty;
            if (discountPrice > 0)
            {
                discountDetails = String.Format("{0:0}% discount", discountPrice);
            }
            return discountDetails;
        }

        #endregion

        #region POST Form

        public string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }

        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }

        #endregion

        #region payment methord

        public class RemotePost
        {
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();

            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public void Post()
            {
                System.Web.HttpContext.Current.Response.Clear();

                System.Web.HttpContext.Current.Response.Write("<html><head>");

                System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                System.Web.HttpContext.Current.Response.Write("</form>");
                System.Web.HttpContext.Current.Response.Write("</body></html>");

                System.Web.HttpContext.Current.Response.End();
            }
        }

        public string Generatetxnid()
        {
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
            string txnid1 = strHash.ToString().Substring(0, 20);
            return txnid1;
        }

        #endregion

        //#region Cart remove/update

        //protected void dlCart_ItemDataCommond(object source, RepeaterCommandEventArgs e)
        //{
        //    int qty = 1;
        //    int txtqty = 1;
        //    string proname = "";

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
        //            dt.Rows.RemoveAt(e.Item.ItemIndex);
        //            Session["Cart"] = dt;
        //            AddToShoppingCart();
        //        }
        //    }
        //}

        //#endregion

        //#region apply coupon code

        private Decimal GetCouponAmount(Decimal amount, Decimal coupcode)
        {
            Decimal CA = amount - (amount * coupcode) / 100;
            Decimal TCA = amount - CA;
            return TCA;
        }

        protected void lbtnApply_Click(object sender, EventArgs e)
        {

            byte CouponGenderType = 0;
            Decimal CouponAmount = 0, CouponPrice = 0; //Coupon %
            short countCouponAppliedProduct = 0;

            DataTable dtCart = (DataTable)Session["Cart"];
            product_handler productHandler = new product_handler();

            DataTable dtCoupon = null;
            dtCoupon = productHandler.get_coupon_code(txtCouponCode.Text, 0, 0, 0).Tables[0];
            if (dtCoupon.Rows.Count > 0 && dtCoupon.Rows[0]["price"] != DBNull.Value)
            {
                CouponPrice = Convert.ToDecimal(dtCoupon.Rows[0]["price"]);
                Session["couponcodeprice"] = CouponPrice;
                Session["couponCodeName"] = txtCouponCode.Text;

                fxCountCode = txtCouponCode.Text;
                if (fxCountCode == "CDG17Y" || fxCountCode == "BMP98A" || fxCountCode == "JPG2XA" || fxCountCode == "NKL12M" || fxCountCode == "XCV1VB" || fxCountCode == "GHL7YZ" || fxCountCode == "ASD85S" || fxCountCode == "JGH10B" || fxCountCode == "JHD1KJ" || fxCountCode == "CXY189")
                {
                    if (Session["CustomerLoginDetails"] != null)
                    {
                        string custLogEmailId = Session["CustomerLoginDetails"].ToString();

                        order_handler orderHandler = new order_handler();
                        DataSet ds = new DataSet();

                        ds = orderHandler.chk_CouonCodeByEmail(custLogEmailId, txtCouponCode.Text);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                                lblMsgCoupon.Text = "Coupon Code Already Used.";
                                Session["couponcodeprice"] = null;
                                return;
                            }
                            else
                            {
                                CouponPrice = Convert.ToDecimal(dtCoupon.Rows[0]["price"]);
                                Session["couponcodeprice"] = CouponPrice;
                                //Session["couponCodeName"] = txtCouponCode.Text;
                            }
                        }
                    }
                    else
                    {
                        lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                        lblMsgCoupon.Text = "Login/Register";
                        Session["couponcodeprice"] = null;
                        return;
                    }
                }

            }
            else
            {
                lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                lblMsgCoupon.Text = "Coupon Code is Invalid.";
                txtCouponCode.Focus();
                Session["couponcodeprice"] = null;
                return;
            }

            // Apply coupon discount Gender wise
            if (dtCoupon.Rows[0]["gendertype"] != DBNull.Value)
            {
                CouponGenderType = Convert.ToByte(dtCoupon.Rows[0]["gendertype"]);

                foreach (DataRow cartRow in dtCart.Rows)
                {
                    if (cartRow["discount"] != DBNull.Value && Convert.ToInt32(cartRow["discount"]) > 0)
                    {
                        // No need to count discount. Already available.
                    }
                    else if (dtCoupon.Rows[0]["menu_name"].ToString() != "select menu")
                    {
                        if (dtCoupon.Rows[0]["menu_name"].ToString() == cartRow["menu_name"].ToString())
                        {
                            if (cartRow["gendertype"] != DBNull.Value && cartRow["gendertype"].ToString() == dtCoupon.Rows[0]["gendertype"].ToString())
                            {
                                CouponAmount = GetCouponAmount(Convert.ToDecimal(cartRow["Total"]), CouponPrice);
                                cartRow["coupon_discount"] = CouponAmount;
                                countCouponAppliedProduct++;
                            }
                        }
                    }
                    else if (cartRow["gendertype"] != DBNull.Value && cartRow["gendertype"].ToString() == dtCoupon.Rows[0]["gendertype"].ToString())
                    {
                        CouponAmount = GetCouponAmount(Convert.ToDecimal(cartRow["Total"]), CouponPrice);
                        cartRow["coupon_discount"] = CouponAmount;
                        countCouponAppliedProduct++;
                    }
                }
                //lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                //lblMsgCoupon.Text = (CouponGenderType == 1 ? "Men" : "Women") + " Coupon code applied successfully on "+countCouponAppliedProduct.ToString()+" product(s).";
            }
            else
            {
                foreach (DataRow cartRow in dtCart.Rows)
                {
                    if (cartRow["discount"] != DBNull.Value && Convert.ToInt32(cartRow["discount"]) > 0)
                    {
                        // No need to count discount. Already available.
                    }
                    else if (dtCoupon.Rows[0]["menu_name"].ToString() == "select menu")
                    {
                        CouponAmount = GetCouponAmount(Convert.ToDecimal(cartRow["Total"]), CouponPrice);
                        cartRow["coupon_discount"] = CouponAmount;
                        countCouponAppliedProduct++;
                    }
                    else if (dtCoupon.Rows[0]["menu_name"].ToString() != "select menu"
                        && dtCoupon.Rows[0]["menu_name"].ToString() == cartRow["menu_name"].ToString())
                    {
                        CouponAmount = GetCouponAmount(Convert.ToDecimal(cartRow["Total"]), CouponPrice);
                        cartRow["coupon_discount"] = CouponAmount;
                        countCouponAppliedProduct++;
                    }
                }
                //lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                //lblMsgCoupon.Text = "Coupon code applied successfully on " + countCouponAppliedProduct.ToString() + " product(s).";
            }
            if (countCouponAppliedProduct > 0)
            {
                lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                lblMsgCoupon.Text = "Coupon code applied successfully on " + countCouponAppliedProduct.ToString() + " product(s).";
                Session["CouponAmount"] = (from DataRow drCart in dtCart.AsEnumerable()
                                           where drCart.RowState != DataRowState.Deleted
                                           select (Convert.ToDecimal(drCart["coupon_discount"]))).Sum();
            }
            else
            {
                lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                lblMsgCoupon.Text = "Oops !!! Apologies, This coupon is not valid on products which are already on Sale or belongs different Category!";
            }

            int TotalQty = 0;
            TotalQty = (from DataRow drCart in dtCart.AsEnumerable() select (Convert.ToInt32(drCart["quantity"]))).Sum();

            AddToShoppingCart(TotalQty);



            //if (ViewState["itemDiscount"] != null && Convert.ToInt32(ViewState["itemDiscount"]) > 0)
            //{
            //    lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
            //    lblMsgCoupon.Text = "Oops !!! Apologies, This coupon is not valid on products which are already on Sale!";
            //}

            //product_handler productHandler = new product_handler();
            //DataSet ds = productHandler.get_search_productId(proId);
            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    DataTable dt = ds.Tables[0];
            //    if (dt.Rows.Count > 0)
            //    {
            //        catid = Convert.ToInt64(dt.Rows[0]["menu_id"].ToString());
            //        subcatid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());
            //    }
            //}
            //DataSet dscpn = new DataSet();
            //dscpn = productHandler.get_coupon_code(txtCouponCode.Text, catid, subcatid, 0);

            //if (dscpn != null && dscpn.Tables.Count > 0)
            //{
            //    DataTable dt = dscpn.Tables[0];
            //    if (dt.Rows.Count > 0)
            //    {
            //        long cId = Convert.ToInt64(dt.Rows[0]["menu_id"].ToString());
            //        long SbId = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

            //        string SndEmail = dt.Rows[0]["sender_email"].ToString();
            //        string RecEmail = dt.Rows[0]["reciever_email"].ToString();

            //        Session["couponCodeName"] = txtCouponCode.Text;


            //        if (RecEmail != null || RecEmail != "")///////////////////////refer friends
            //        {
            //            Session["sndemail"] = SndEmail.ToString();
            //            Session["recemail"] = RecEmail.ToString();
            //        }
            //        else
            //        {
            //            Session["sndemail"] = null;
            //            Session["recemail"] = null;
            //        }

            //        if (dt.Rows[0]["gendertype"] != DBNull.Value)
            //            CouponGenderType = Convert.ToByte(dt.Rows[0]["gendertype"]);

            //        // Check Gender wise coupon
            //        DataTable dtCart = (DataTable)Session["Cart"];
            //        foreach (DataRow cartRow in dtCart.Rows)
            //        {
            //            if (cartRow["gendertype"] != DBNull.Value)
            //            {
            //                if (Convert.ToByte(cartRow["gendertype"]) == CouponGenderType)
            //                {
            //                    ValidGenderCoupon = true;
            //                    break;
            //                }
            //            }
            //        }
            //        if (CouponGenderType > 0 && !ValidGenderCoupon)
            //        {
            //            lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
            //            lblMsgCoupon.Text = "Oops !!! Apologies, This coupon is valid only for " + (CouponGenderType == 1 ? "Men" : "Women") + " products!";
            //            Session["couponcodeprice"] = null;
            //        }
            //        else if (cId == catid && SbId == subcatid)   ///////////caregory wise coupon code
            //        {
            //            lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
            //            lblMsgCoupon.Text = "Coupon code applied successfully.";
            //            Session["couponcodeprice"] = dt.Rows[0]["price"].ToString();
            //            //AddToShoppingCart();
            //        }
            //        else if (cId == 0 && SbId == 0)     ////////////all product used coupon code
            //        {
            //            lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
            //            lblMsgCoupon.Text = "Coupon code applied successfully.";
            //            Session["couponcodeprice"] = dt.Rows[0]["price"].ToString();
            //            //AddToShoppingCart();
            //        }
            //        else
            //        {
            //            lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
            //            lblMsgCoupon.Text = "Coupon Code is Invalid.";
            //            Session["couponcodeprice"] = null;
            //            //AddToShoppingCart();
            //        }

            //AddToShoppingCart();
            //}
            //else
            //{
            //    lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
            //    lblMsgCoupon.Text = "Coupon Code is Invalid.";
            //    Session["couponcodeprice"] = null;
            //    //AddToShoppingCart();
            //}

            //AddToShoppingCart();
            //}
        }

        //#endregion

        #region PayUmoney

        private void bindPayUmoneyPayment()
        {
            Session["payamount"] = lblGrandTotal.Text;
            Session["orderName"] = hfpersonaName.Value;

            if (ViewState["proName"] != null)
            {
                productinfo.Value = ViewState["proName"].ToString();
            }


            RemotePost myremotepost = new RemotePost();
            string key = "mZzMRv";
            string amount = lblGrandTotal.Text;
            string productInfo = productinfo.Value;
            string firstName = hfpersonaName.Value;
            string email = Session["CustomerLoginDetails"].ToString();
            string phone = hfpersonamobile.Value;
            string salt = "ZgF6zuS7";
            //posting all the parameters required for integration.

            if (Session["CustomerLoginDetails"].ToString().Equals("kalpesh013@gmail.com") || Session["CustomerLoginDetails"].ToString().Equals("faizan@carbonmedia.in"))
                amount = "1.00";

            myremotepost.Url = "https://secure.payu.in/_payment";
            myremotepost.Add("key", "mZzMRv");
            string txnid = Generatetxnid();
            myremotepost.Add("txnid", txnid);
            myremotepost.Add("amount", amount);
            myremotepost.Add("productinfo", productInfo);
            myremotepost.Add("firstname", firstName);
            myremotepost.Add("phone", phone);
            myremotepost.Add("email", email);

            myremotepost.Add("surl", System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/success.aspx");//Change the success url here depending upon the port number of your local system.
            myremotepost.Add("furl", System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/error.aspx");//Change the failure url here depending upon the port number of your local system.

            myremotepost.Add("service_provider", "payu_paisa");
            string hashString = key + "|" + txnid + "|" + amount + "|" + productInfo + "|" + firstName + "|" + email + "|||||||||||" + salt;
            string hash = Generatehash512(hashString);
            myremotepost.Add("hash", hash);

            myremotepost.Post();
        }

        #endregion


        #region Mobikwik - Unused

        //protected void submitMobikwik_Click(object sender, EventArgs e)
        //{
        //    bindProceedToPaymentforMobikwik();
        //}

        private void bindProceedToPaymentforMobikwik()
        {
            try
            {
                string generatecode = Utility.generatecode();
                //Guid customer_id = new Guid(Session["customerDetailsId"].ToString());
                //order_handler orderHandler = new order_handler();
                //BusinessEntities.order Order = new BusinessEntities.order();
                //Order.customer_id = customer_id;

                Guid customer_id = new Guid(Session["customerDetailsId"].ToString());
                order_handler orderHandler = new order_handler();
                BusinessEntities.order Order = new BusinessEntities.order();
                //Order.order_number = generatecode;
                Order.order_number = customer_id;
                Order.customer_id = customer_id;



                Order.user_name = txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : " " + txtreceiverlastname.Text);
                Order.contact_number = txtreceiverphone.Text;
                Order.email_id = Session["CustomerLoginDetails"].ToString();
                //Order.address = txtreceiveraddress.Text + " " + txtreceiveraddress1.Text;
                Order.address = txtreceiveraddress.Text;
                Order.land_mark = txtLandMark.Text;
                Order.city = Session["ReceiverCity"].ToString();
                Order.state = Session["ReceiverState"].ToString();
                Order.pin_code = Session["ReceiverPinCode"].ToString();
                //Order.message = txtreceivermessage.Text;
                Order.message = "";
                Order.PaymentThrough = "MobiKwik";
                if (Session["CouponAmount"] != null)
                {
                    //Order.coupon_price = Session["CouponAmount"].ToString();
                    // Order.discount_price = Session["CouponAmount"].ToString();
                    Order.discount_price = lblDiscount.Text;
                }
                //else if (Session["sale_discount"] != null)
                //{
                //    Order.discount_price = Session["sale_discount"].ToString();
                //}
                //else
                //{
                //    Order.coupon_price = "0";
                //}

                if (Session["couponCodeName"] != null)
                {
                    Order.coupon_code = Session["couponCodeName"].ToString();
                }
                else
                {
                    Order.coupon_code = "";
                }

                Order.TotalPrice = Session["grdTotal"].ToString();


                Order.sale_price = ViewState["salPrice"].ToString();
                //Order.price = ViewState["Price"].ToString();
                Order.price = Session["grdTotal"].ToString();

                Order.shipping_price = ViewState["ShippingCharge"].ToString();


                //long resultOrder = orderHandler.InsertUpdateOrder(Session);
                long resultOrder = orderHandler.insert_order(Order);
                if (resultOrder > 0)
                {
                    Order.order_id = Convert.ToInt32(resultOrder);
                    Session["OrderNumber"] = Order.order_id;

                    DataTable dtCart = (DataTable)Session["Cart"];
                    if (dtCart != null && dtCart.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCart.Rows.Count; i++)
                        {
                            Order.product_id = Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString());
                            Order.quantity = Convert.ToInt32(dtCart.Rows[i]["quantity"].ToString());
                            Order.TotalPrice = dtCart.Rows[i]["Total"].ToString();
                            Order.order_status = "Pending for Admin";

                            //bool resultOrderDetails = orderHandler.InsertOrderDetails(Order);
                            bool resultOrderDetails = orderHandler.insert_order_detail(Order);
                            if (resultOrderDetails)
                            {
                                // checking and updating stock and out of stock
                                product_handler productsHandler = new product_handler();
                                DataSet dsqty = productsHandler.get_search_quantity(Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString()));
                                if (dsqty != null && dsqty.Tables.Count > 0)
                                {
                                    DataTable dtqty = dsqty.Tables[0];
                                    if (dtqty.Rows.Count > 0)
                                    {
                                        qty = Convert.ToInt32(dtqty.Rows[0]["quantity"].ToString());
                                        txtqty = Convert.ToInt32(dtCart.Rows[i]["quantity"].ToString());
                                        qtycount = qty - txtqty;
                                        Order.quantity = qtycount;

                                        if (qty == 0)
                                        {
                                            //pending redirect on product detail page.
                                        }

                                        if (qtycount <= 0)
                                        {
                                            Order.in_stock = false;
                                        }
                                        else
                                        {
                                            Order.in_stock = true;
                                        }

                                        bool updateQuantity = orderHandler.update_product_quantity(Order);
                                    }
                                }
                            }
                        }
                    }
                    Session["email"] = Session["CustomerLoginDetails"].ToString();
                    Session["amt"] = lblGrandTotal.Text;
                    Session["cell"] = hfpersonamobile.Value;
                    //Session["orderid"] = "S2S" + Session["OrderNumber"].ToString();
                    Session["orderid"] = Session["OrderNumber"].ToString();

                    Response.Redirect("mobindex.aspx");
                    //SendEmailConfirmOrder("mobokwik"); Send this email from success.aspx page.
                }
            }
            catch (Exception ex)
            {
                throw ex;
                Response.Redirect("/");
            }
        }
        #endregion

        #region Payment

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(lblreceiverAddress.Text))
            //{
            //    lblCheckMsg.Visible = true;
            //    lblCheckMsg.Text = "Please select Delivery Address.";
            //    return;
            //}

            //if (txtOTPno.Visible == false && rbtnCashOnDelivery.Checked)
            //{
            //    GenerateOTPCode();
            //    otpbox.Visible = true;
            //    return;
            //}
            //else if (txtOTPno.Visible == true && rbtnCashOnDelivery.Checked)
            //{
            //    if (ViewState["OTP"].ToString() != txtOTPno.Text.Trim())
            //    {
            //        lblotpno.Text = "Your OTP invaild. Please enter corret OTP.";
            //        return;
            //    }
            //}
            //else if(txtOTPno.Visible == true && !rbtnCashOnDelivery.Checked)
            //{
            //    otpbox.Visible = false;
            //}

            if (rbtnCashOnDelivery.Checked)
            {
                //pincode_handler pincodeHandler = new pincode_handler();
                //if (!pincodeHandler.check_Pincode_COD(lblreceiverPincode.Text.Substring(2)))
                //{
                //    lblCheckMsg.Visible = true;
                //    lblCheckMsg.Text = "Cash on Delivery is not available at this Pincode number.";
                //    return;
                //}

                if (InsertOrder("COD"))
                {
                    deleteTempCart();
                    Utility.SendSmsCODConfirmation(txtreceiverphone.Text, txtreceivername.Text, Convert.ToString(Session["OrderNumber"]), Session["grdTotal"].ToString());
                    Response.Redirect("success.aspx?type=cod");
                }
            }
            else
            {
                //if (Session["CustomerLoginDetails"].ToString().Equals("kalpesh013@gmail.com") || Session["CustomerLoginDetails"].ToString().Equals("faizan@carbonmedia.in"))
                //{
                if (InsertOrder("Razorpay"))        // 28-08-2019 Added new Payment gatway.
                {
                    deleteTempCart();

                    //Payment method call from JavaScript
                    string jsRequest = @"$(function () { $('.razorpay-payment-button').click(); });";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pay", jsRequest, true);
                }
                //}
                //else
                //{

                //    if (InsertOrder("CCAvenue"))
                //    {
                //        deleteTempCart();
                //        SubmitCcAvenue();
                //    }
                //}

                //else if (rbtnUseWallet.Checked)
                //{
                //    if (InsertOrder("CCAvenue"))
                //    {
                //        deleteTempCart();
                //        SubmitCcAvenue();
                //    }
                //}
                //else
                //{
                //    if (InsertOrder("PayUMoney"))
                //    {
                //        deleteTempCart();
                //        bindPayUmoneyPayment();
                //    }
                //}

                // Logout if user type is Guest
                //customer_handler customerHandler = new customer_handler();
                //DataSet ds = customerHandler.check_guest_loginid(Convert.ToString(Session["CustomerLoginDetails"]));
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    if (ds.Tables[0].Rows[0]["login_type"].ToString() == "guest")
                //    {
                //        Session.Clear();
                //        Session.Abandon();
                //    }
                //}
            }

        }
        /// <summary>
        /// Added  if (Order.PaymentThrough != "COD") by Hetal Patel to change Order status
        /// </summary>
        /// <param name="PaymentType"></param>
        /// <returns></returns>
        private bool InsertOrder(string PaymentType)
        {
            string generatecode = Utility.generatecode();
            Guid customer_id = new Guid(Session["customerDetailsId"].ToString());
            order_handler orderHandler = new order_handler();
            BusinessEntities.order Order = new BusinessEntities.order();
            Order.order_number = Guid.NewGuid();
            //Order.order_number = customer_id; 
            Order.customer_id = customer_id;

            Order.user_name = txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : " " + txtreceiverlastname.Text);
            Order.contact_number = txtreceiverphone.Text;

            Session["User_Name"] = Order.user_name;
            Session["Contact_Number"] = Order.contact_number;
            Session["Sale_Price"] = Order.sale_price;

            Order.email_id = Session["CustomerLoginDetails"].ToString();
            Order.address = txtreceiveraddress.Text;
            Order.land_mark = txtLandMark.Text;
            Order.city = ddlReceiverCity.SelectedItem.Text;
            Order.state = ddlReceiverState.SelectedItem.Text;
            Order.pin_code = txtReceiverPinCode.Text;
            //Order.message = txtreceivermessage.Text;
            Order.message = "";
            Order.PaymentThrough = PaymentType;
            //Order.coupon_price = "0";
            Order.discount_price = "0";     // Include only Coupon discount

            if (Session["CouponAmount"] != null)
            {
                //Order.coupon_price = Session["CouponAmount"].ToString();
                //Order.discount_price = Session["CouponAmount"].ToString();
                Order.discount_price = lblDiscount.Text;
            }
            //if (Session["sale_discount"] != null)
            //    Order.discount_price = (Convert.ToSingle(Session["sale_discount"]) + Convert.ToSingle(Order.discount_price)).ToString("0.00"); ;

            if (Session["couponCodeName"] != null)
                Order.coupon_code = Session["couponCodeName"].ToString();
            else
                Order.coupon_code = "";

            Order.TotalPrice = Session["grdTotal"].ToString();
            Order.sale_price = ViewState["salPrice"].ToString();
            //Order.price = ViewState["Price"].ToString(); 
            Order.price = Session["grdTotal"].ToString();
            Order.shipping_price = ViewState["ShippingCharge"].ToString();

            long resultOrder = orderHandler.insert_order(Order);
            if (resultOrder > 0)
            {
                Order.order_id = Convert.ToInt32(resultOrder);
                Session["OrderNumber"] = Order.order_id;

                DataTable dtCart = (DataTable)Session["Cart"];
                if (dtCart != null && dtCart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCart.Rows.Count; i++)
                    {
                        Order.product_id = Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString());
                        Order.quantity = Convert.ToInt32(dtCart.Rows[i]["quantity"].ToString());
                        //Order.TotalPrice = dtCart.Rows[i]["Total"].ToString();
                        Order.TotalPrice = Session["grdTotal"].ToString();
                        //Order.order_status = "Pending for Admin";
                        if (Order.PaymentThrough != "COD")
                        {
                            Order.order_status = "Failed";
                        }
                        else
                        {
                            Order.order_status = "New Order";
                        }

                        if (dtCart.Rows[i]["custom_bag_price"] != DBNull.Value)
                            Order.custom_bag_price = dtCart.Rows[i]["custom_bag_price"].ToString();
                        if (dtCart.Rows[i]["custom_bag_param"] != DBNull.Value)
                            Order.custom_bag_param = dtCart.Rows[i]["custom_bag_param"].ToString();

                        if (Session["CustImgPath"] != null)
                        {
                            Order.thumb_image = Session["CustImgPath"].ToString();
                        }

                        if (dtCart.Rows[i]["x_point"] != DBNull.Value)
                            Order.x_point = Convert.ToSingle(dtCart.Rows[i]["x_point"]);
                        if (dtCart.Rows[i]["y_point"] != DBNull.Value)
                            Order.y_point = Convert.ToSingle(dtCart.Rows[i]["y_point"]);

                        bool resultOrderDetails = orderHandler.insert_order_detail(Order);
                        if (resultOrderDetails)
                        {
                            // checking and updating stock and out of stock
                            product_handler productsHandler = new product_handler();
                            DataSet dsqty = productsHandler.get_search_quantity(Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString()));
                            if (dsqty != null && dsqty.Tables.Count > 0)
                            {
                                DataTable dtqty = dsqty.Tables[0];
                                if (dtqty.Rows.Count > 0)
                                {
                                    qty = Convert.ToInt32(dtqty.Rows[0]["quantity"].ToString());
                                    txtqty = Convert.ToInt32(dtCart.Rows[i]["quantity"].ToString());
                                    qtycount = qty - txtqty;
                                    Order.quantity = qtycount;

                                    if (qty == 0)
                                    {
                                        //pending redirect on product detail page.
                                    }

                                    if (qtycount <= 0)
                                    {
                                        Order.in_stock = false;
                                    }
                                    else
                                    {
                                        Order.in_stock = true;
                                    }

                                    bool updateQuantity = orderHandler.update_product_quantity(Order);
                                }
                            }
                        }
                    }
                }
                Session["email"] = Session["CustomerLoginDetails"].ToString();
                Session["amt"] = lblGrandTotal.Text;
                Session["cell"] = hfpersonamobile.Value;
                Session["orderid"] = Session["OrderNumber"].ToString();

                return true;
            }
            return false;
        }
        #endregion

        //protected void btnUserDetails_Click(object sender, EventArgs e)
        //{
        //    if (Session["CustomerLoginDetails"] != null)
        //    {
        //        ViewState["steps"] = "2";
        //        ApplyActiveSteps();
        //    }
        //}

        //protected void btnDeliveryAddress_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(lblreceiverAddress.Text))
        //    {
        //        ViewState["steps"] = "3";
        //        ApplyActiveSteps();
        //    }
        //}



        #region CCAvenue
        private void SubmitCcAvenue()
        {
            CCACrypto chkSum = new CCACrypto();
            string requestUrl = "https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction";       //Live
            //string requestUrl = "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction";       //Testing

            string WorkingKey = "AA66DDF3F895AB3818C39AF133E3473D";//put in the 32bit alpha numeric key in the quotes provided here 	
            string MerchantId = "162060";
            string AccessCode = "AVJV77FD61BV39VJVB";
            string CountryName = "India";

            if (Session["CustomerData"] != null)
            {
                StringBuilder ToEncrypt = new StringBuilder();
                ToEncrypt.Append("tid=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                ToEncrypt.Append("&merchant_id=" + MerchantId);
                ToEncrypt.Append("&order_id=" + Session["orderid"]);
                if (Session["CustomerLoginDetails"].ToString().Equals("kalpesh013@gmail.com") || Session["CustomerLoginDetails"].ToString().Equals("vishesh@thestruttstore.com"))
                    ToEncrypt.Append("&amount=1.00");
                else
                    ToEncrypt.Append("&amount=" + lblGrandTotal.Text);

                ToEncrypt.Append("&currency=INR");

                //ToEncrypt.Append("&redirect_url=https://thestruttstore.com/ccavenue/ccavResponseHandler.aspx");
                //ToEncrypt.Append("&cancel_url=https://thestruttstore.com/ccavenue/ccavResponseHandler.aspx");
                ToEncrypt.Append("&redirect_url=" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/success.aspx");
                ToEncrypt.Append("&cancel_url=" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/error.aspx");

                ToEncrypt.Append("&billing_name=" + txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : " " + txtreceiverlastname.Text));
                ToEncrypt.Append("&billing_address=" + txtreceiveraddress.Text + (string.IsNullOrEmpty(txtLandMark.Text) ? "" : ", " + txtLandMark.Text));
                ToEncrypt.Append("&billing_city=" + ddlReceiverCity.SelectedItem.Text);
                ToEncrypt.Append("&billing_state=" + ddlReceiverState.SelectedItem.Text);
                ToEncrypt.Append("&billing_zip=" + txtReceiverPinCode.Text);
                ToEncrypt.Append("&billing_country=" + CountryName);
                ToEncrypt.Append("&billing_tel=" + txtreceiverphone.Text);
                ToEncrypt.Append("&billing_email=" + Session["CustomerLoginDetails"].ToString());

                ToEncrypt.Append("&delivery_name=" + txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : " " + txtreceiverlastname.Text));
                ToEncrypt.Append("&delivery_address=" + txtreceiveraddress.Text + (string.IsNullOrEmpty(txtLandMark.Text) ? "" : ", " + txtLandMark.Text));
                ToEncrypt.Append("&delivery_city=" + ddlReceiverCity.SelectedItem.Text);
                ToEncrypt.Append("&delivery_state=" + ddlReceiverState.SelectedItem.Text);
                ToEncrypt.Append("&delivery_zip=" + txtReceiverPinCode.Text);
                ToEncrypt.Append("&delivery_country=" + CountryName);
                ToEncrypt.Append("&delivery_tel=" + txtreceiverphone.Text);
                //ToEncrypt.Append("&merchant_param1=" + txtreceivermessage.Text);

                string Encrypted;
                Encrypted = chkSum.Encrypt(ToEncrypt.ToString(), WorkingKey);

                Response.Redirect(requestUrl + "&encRequest=" + Encrypted + "&access_code=" + AccessCode);
                //Response.Redirect("~/ccavRequestHandler.aspx?request=" + Encrypted + "&access_code=" + AccessCode);
            }
        }

        #endregion

        #region Razorpay
        // New 
        private string GetRazorpayID()
        {
            int totalAmount;
            if (Session["CustomerLoginDetails"].ToString().Equals("kalpesh013@gmail.com") || Session["CustomerLoginDetails"].ToString().Equals("vishesh@thestruttstore.com"))
                totalAmount = 200; // For testing purpose, It will deduct 2 Rs from card for above emails
            else
                totalAmount = Convert.ToInt32((Convert.ToSingle(lblGrandTotal.Text) * 100));

            //double TransfterAmountPer = Convert.ToDouble(ConfigurationManager.AppSettings["razorpay_transfer_per"]);
            string razorpayorder_id;
            string key = ConfigurationManager.AppSettings["razorpay_key"];
            string secret = ConfigurationManager.AppSettings["razorpay_secret"];
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            RazorpayClient rclient = new RazorpayClient(key, secret);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", totalAmount); // amount in the smallest currency unit
            options.Add("receipt", "order_rcptid_11");
            options.Add("currency", "INR");

            Dictionary<string, object> payment = new Dictionary<string, object>();
            payment.Add("capture", "automatic");

            Dictionary<string, object> capture_options = new Dictionary<string, object>();
            capture_options.Add("automatic_expiry_period", "12");
            capture_options.Add("manual_expiry_period", "7200");
            capture_options.Add("refund_speed", "optimum");

            payment.Add("capture_options", capture_options);
            options.Add("payment", payment);

            Order order = rclient.Order.Create(options);

            //Code for Split Amount API//
            //RazorpayClient rclient = new RazorpayClient(key, secret);

            //Dictionary<string, object> options = new Dictionary<string, object>();
            //options.Add("amount", totalAmount);
            //options.Add("currency", "INR");

            //Dictionary<string, object>[] optionTransfers = new Dictionary<string, object>[1];
            //optionTransfers[0] = new Dictionary<string, object>();
            //optionTransfers[0].Add("account", ConfigurationManager.AppSettings["razorpay_account"]);
            ////optionTransfers[0].Add("amount", Convert.ToInt32((TransfterAmountPer / 100) * totalAmount)); 
            //optionTransfers[0].Add("currency", "INR");

            //options.Add("transfers", optionTransfers);
            //Order order = rclient.Order.Create(options);
            //Code for Split Amount API//

            razorpayorder_id = order["id"];
            return razorpayorder_id;
        }
        private void RazorpaySubmit()
        {
            string key = ConfigurationManager.AppSettings["razorpay_key"];
            string secret = ConfigurationManager.AppSettings["razorpay_secret"];
            int totalAmount;

            string razorpayorder_id = GetRazorpayID();

            RazorpayClient client = new RazorpayClient(key, secret);

            if (Session["CustomerData"] != null)
            {
                StringBuilder reqStr = new StringBuilder();

                if (Session["CustomerLoginDetails"].ToString().Equals("kalpesh013@gmail.com") || Session["CustomerLoginDetails"].ToString().Equals("vishesh@thestruttstore.com"))
                    totalAmount = 200; // For testing purpose, It will deduct 2 Rs from card for above emails
                else
                    totalAmount = Convert.ToInt32((Convert.ToSingle(lblGrandTotal.Text) * 100));

                string jsRequest = string.Format(@"<script type=""text/javascript""
                        src=""https://checkout.razorpay.com/v1/checkout.js""
                            data-key=""{0}""
                            data-amount=""{1}""
                            data-name=""Razorpay""
                            data-description=""{2}""
                            data-image=""https://razorpay.com/favicon.png""
                            data-prefill.name=""{3}""
                            data-prefill.email=""{4}""
                            data-prefill.contact=""{5}""
                            data-order_id=""{6}""></script> ", key, totalAmount, "Payment of RazorPayOrderID: " + razorpayorder_id,
                            txtreceivername.Text + (string.IsNullOrEmpty(txtreceiverlastname.Text) ? "" : " " + txtreceiverlastname.Text),
                            Session["CustomerLoginDetails"].ToString(), txtreceiverphone.Text, razorpayorder_id);

                litRazor.Text = jsRequest;
            }
        }

        #endregion
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

        //                bindDeliveryDetails(Session["CustomerLoginDetails"].ToString());
        //                BillingDetails();
        //            }
        //        }
        //    }
        //}

        
        private void SetActiveSteps()
        {
            if (Session["CustomerLoginDetails"] == null || string.IsNullOrEmpty(txtreceiverphone.Text))
            {
                ViewState["steps"] = "1";
            }
            else
            {
                ViewState["steps"] = "2";
            }

            ApplyActiveSteps();
        }


        private void ApplyActiveSteps() 
        {

            if (Convert.ToString(ViewState["steps"]) == "1")          //User Details
            {
                //head1UserDetails.Attributes.Add("class", "customer-zone mb-30 active");
                //head3Payment.Attributes.Add("class", "customer-zone mb-30");

                //panel1UserDetails.Attributes.Add("class", "active");
                //// panel2DeliveryAdd.Attributes.Add("class", "padding-25 fragment");
                //panel3Payment.Attributes.Add("class", "");

                //btnChangeUserDetails.Visible = false;
                //btnChangeDeliveryAdd.Visible = false;
                //if (string.IsNullOrEmpty(lblreceiverAddress.Text))
                //    btnUseAddress2.Visible = false;
                //else
                //    btnUseAddress2.Visible = true;


            }

            else if (ViewState["steps"].ToString() == "2")     //Payment Methods
            {
                //head1UserDetails.Attributes.Add("class", "customer-zone mb-30");
                //head3Payment.Attributes.Add("class", "customer-zone mb-30 active");

                //panel1UserDetails.Attributes.Add("class", "");
                //panel3Payment.Attributes.Add("class", "active");

                //btnChangeUserDetails.Visible = true;
                // btnChangeDeliveryAdd.Visible = true;

                //RazorpaySubmit();

                //RazorpayLoadData();
                RazorpaySubmit();
}
        }

        protected void btnChangeUserDetails_Click(object sender, EventArgs e)
        {
            collapseOne.Attributes.Add("class", "collapse show");
        }

        protected void txtguestemail_OnTextChanged(object sender, EventArgs e)
        {
            insertTempCart();
        }

        private void insertTempCart()
        {
            try
            {
                if (string.IsNullOrEmpty(txtguestemail.Text) || !retxtguestemail.IsValid)
                    return;

                string city = string.Empty, state = string.Empty, name = string.Empty;
                if (ddlReceiverCity.SelectedIndex > 0) city = ddlReceiverCity.SelectedItem.Text;
                if (ddlReceiverState.SelectedIndex > 0) state = ddlReceiverState.SelectedItem.Text;
                if (string.IsNullOrEmpty(txtreceiverlastname.Text))
                    name = txtreceivername.Text;
                else
                    name = txtreceivername.Text + " " + txtreceiverlastname.Text;

                temp_cart_handler cartHandler = new temp_cart_handler();
                int temp_customer_Id = cartHandler.insert_temp_customer(txtguestemail.Text, name, txtreceiverphone.Text, txtreceiveraddress.Text, txtLandMark.Text, city, state, "India", txtReceiverPinCode.Text, false, "Cart Abandoned");

                DataTable dtCart = (DataTable)Session["Cart"];
                if (dtCart != null && dtCart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCart.Rows.Count; i++)
                    {
                        cartHandler.insert_temp_cart(temp_customer_Id, Convert.ToInt64(dtCart.Rows[i]["product_id"]), Convert.ToInt32(dtCart.Rows[i]["quantity"]));
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void updateTempCart()
        {
            try
            {
                string city = string.Empty, state = string.Empty, name = string.Empty;
                if (ddlReceiverCity.SelectedIndex > 0) city = ddlReceiverCity.SelectedItem.Text;
                if (ddlReceiverState.SelectedIndex > 0) state = ddlReceiverState.SelectedItem.Text;
                if (string.IsNullOrEmpty(txtreceiverlastname.Text))
                    name = txtreceivername.Text;
                else
                    name = txtreceivername.Text + " " + txtreceiverlastname.Text;

                temp_cart_handler cartHandler = new temp_cart_handler();
                int temp_customer_Id = cartHandler.update_temp_customer(txtguestemail.Text, name, txtreceiverphone.Text, txtreceiveraddress.Text, txtLandMark.Text, city, state, "India", txtReceiverPinCode.Text);

            }
            catch (Exception ex)
            {
               
                return;
            }
        }

        private void deleteTempCart()
        {
            temp_cart_handler cartHandler = new temp_cart_handler();
            cartHandler.delete_temp_cart_customer(txtguestemail.Text);
        }

        //protected void btnUseAddress2_Click(object sender, EventArgs e)
        //{
        //    ViewState["steps"] = "2";
        //    ApplyActiveSteps();
        //}

        private void GenerateOTPCode()
        {
            Random r = new Random();
            string OTP = r.Next(10000, 99999).ToString();

            //Send message of OTP
            Utility.SendSmsCodOtpSend(txtreceiverphone.Text, txtreceivername.Text, OTP);

            lblotpno.Text = "OTP sent to your mobile, Please enter here to complete order.";
            ViewState["OTP"] = OTP;

        }

        protected void rbtnDebitCreditCard_CheckedChanged(object sender, EventArgs e)
        {
            Decimal grandTotal = Convert.ToDecimal(Session["grandTotal"].ToString());
            Decimal OnlinePaymentPersantage = 0;
            OnlinePaymentPersantage = ((grandTotal * 5) / 100);
            grandTotal = grandTotal - OnlinePaymentPersantage;

            Decimal Discount = Convert.ToDecimal(Session["CouponAmount"].ToString());
            lblDiscount.Text = String.Format("{0:0.00}", Discount + OnlinePaymentPersantage);
            Session["onlineDiscount"] = lblDiscount.Text.ToString();

            btnPlaceOrder.Text = String.Format("Pay Rs.{0:0.00}", grandTotal);
            Session["onlinegrdTotal"] = lblGrandTotal.Text.ToString();
            lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
            Session["grdTotal"] = lblGrandTotal.Text.ToString();
            RazorpaySubmit();
        }

        protected void rbtnUseWallet_CheckedChanged(object sender, EventArgs e)
        {
            Decimal grandTotal = Convert.ToDecimal(Session["grandTotal"].ToString());
            Decimal OnlinePaymentPersantage = 0;
            OnlinePaymentPersantage = ((grandTotal * 5) / 100);
            grandTotal = grandTotal - OnlinePaymentPersantage;

            Decimal Discount = Convert.ToDecimal(Session["CouponAmount"].ToString());
            lblDiscount.Text = String.Format("{0:0.00}", Discount + OnlinePaymentPersantage);
            Session["onlineDiscount"] = lblDiscount.Text.ToString();

            btnPlaceOrder.Text = String.Format("Pay Rs.{0:0.00}", grandTotal);
            Session["onlinegrdTotal"] = lblGrandTotal.Text.ToString();
            lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
            Session["grdTotal"] = lblGrandTotal.Text.ToString();
            RazorpaySubmit();
        }

        protected void rbtnWallets_CheckedChanged(object sender, EventArgs e)
        {
            Decimal grandTotal = Convert.ToDecimal(Session["grandTotal"].ToString());
            Decimal OnlinePaymentPersantage = 0;
            OnlinePaymentPersantage = ((grandTotal * 5) / 100);
            grandTotal = grandTotal - OnlinePaymentPersantage;

            Decimal Discount = Convert.ToDecimal(Session["CouponAmount"].ToString());
            lblDiscount.Text = String.Format("{0:0.00}", Discount + OnlinePaymentPersantage);
            Session["onlineDiscount"] = lblDiscount.Text.ToString();

            btnPlaceOrder.Text = String.Format("Pay Rs.{0:0.00}", grandTotal);
            Session["onlinegrdTotal"] = lblGrandTotal.Text.ToString();
            lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
            Session["grdTotal"] = lblGrandTotal.Text.ToString();
            RazorpaySubmit();
        }

        protected void rbtnUpi_CheckedChanged(object sender, EventArgs e)
        {
            Decimal grandTotal = Convert.ToDecimal(Session["grandTotal"].ToString());
            Decimal OnlinePaymentPersantage = 0;
            OnlinePaymentPersantage = ((grandTotal * 5) / 100);
            grandTotal = grandTotal - OnlinePaymentPersantage;

            Decimal Discount = Convert.ToDecimal(Session["CouponAmount"].ToString());
            lblDiscount.Text = String.Format("{0:0.00}", Discount + OnlinePaymentPersantage);
            Session["onlineDiscount"] = lblDiscount.Text.ToString();

            btnPlaceOrder.Text = String.Format("Pay Rs.{0:0.00}", grandTotal);
            Session["onlinegrdTotal"] = lblGrandTotal.Text.ToString();
            lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
            Session["grdTotal"] = lblGrandTotal.Text.ToString();
            RazorpaySubmit();
        }

        protected void rbtnCashOnDelivery_CheckedChanged(object sender, EventArgs e)
        {
            Decimal grandTotal = Convert.ToDecimal(Session["grandTotal"].ToString());


            btnPlaceOrder.Text = String.Format("Pay Rs.{0:0.00}", grandTotal);
            Session["onlinegrdTotal"] = lblGrandTotal.Text.ToString();
            lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
            Session["grdTotal"] = lblGrandTotal.Text.ToString();
            lblDiscount.Text = Session["CouponAmount"].ToString();


        }

        private void Checkout()
        {
            string checkoutstarted = @"
                    wigzo(""track"", ""checkoutstarted"",""" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + Request.RawUrl + @""");";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "checkoutstarted", checkoutstarted, true);


        }
        private void getText()
        {
            try
            {
                DataTable dt = (DataTable)Session["PageLabel"];
                if (dt.Rows.Count > 0)
                {
                    lblSale.Text = dt.Select("label_name='Sale Text'")[0]["label_value"].ToString();
                    lblCode.Text = dt.Select("label_name='Code Coupon'")[0]["label_value"].ToString();
                    lblOffer.Text = dt.Select("label_name='Offer Coupon'")[0]["label_value"].ToString();
                }
            }
            catch (Exception)
            {
                //If any error then ignore
            }
        }

        #region google login
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
                Response.Redirect("~/proceedtopayment.aspx");
            else
                Response.Redirect(Request.QueryString["url"]);
        }

        #endregion end google login
    }
}