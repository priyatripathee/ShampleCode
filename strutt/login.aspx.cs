
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using CCA.Util;
using BusinessEntities;
using System.Net;
using System.IO;
using System.Web.Mail;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace strutt
{
    public partial class login : System.Web.UI.Page
    {
        //private static string PrevUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                    ViewState["PrevUrl"] = Request.UrlReferrer.PathAndQuery;

                if(ViewState["PrevUrl"].ToString().Contains("forgotpassword") ||
                    ViewState["PrevUrl"].ToString().Contains("resetpassword"))
                {
                    ViewState["PrevUrl"] = null;
                }

                if (Request.QueryString["unsb"] != null)
                {
                    long unsub = Convert.ToInt64(Request.QueryString["unsb"].ToString());
                }
                else if (Request.QueryString["type"] != null && Request.QueryString["type"].Equals("lo"))
                {
                    LogOut();
                }
                if (Session["CustomerLoginDetails"] != null)
                {
                    Response.Redirect("~/account/orderhistory.aspx");
                }
            }


        }
        #region login/logout
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
                        Response.Redirect("~/category.aspx");
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


        protected void LogOut()
        {


            Session.Clear();
            Session.Abandon();
            Response.Redirect("/", false);
        }
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


    }

}