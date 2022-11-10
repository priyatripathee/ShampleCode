using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mail;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using BusinessEntities;
using BLL;
using CCA.Util;
using System.Configuration;

namespace strutt
{
    public partial class success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["OrderNumber"] == null)
                {
                    Response.Redirect("/");
                    return;
                }
                //Response.Write(Request.Form);
                lblResponse.Text = Request.Form.ToString();

                if (Request.Form["razorpay_payment_id"] != null)
                {
                    string paymentId = Request.Form["razorpay_payment_id"];
                    RazorPayResponse();
                    SendEmailConfirmOrder("Razorpay", Convert.ToInt32(Session["OrderNumber"]));
                    Utility.SendSmsOnlinePaymentConfirmation(Convert.ToString(ViewState["ContactNumber"]), Convert.ToString(ViewState["CustomerName"]), Convert.ToString(Session["OrderNumber"]));
                }
                else if (Request.Form["payuMoneyId"] != null)
                {
                    PayUResponse();
                    SendEmailConfirmOrder("PayUMoney", Convert.ToInt32(Session["OrderNumber"]));
                    Utility.SendSmsOnlinePaymentConfirmation(Convert.ToString(ViewState["ContactNumber"]), Convert.ToString(ViewState["CustomerName"]), Convert.ToString(Session["OrderNumber"]));
                }
                else if (Request.Form["encResp"] != null)
                {
                    if (CCAvenueResponse())
                    {
                        SendEmailConfirmOrder("CCAvenue", Convert.ToInt32(Session["OrderNumber"]));
                        Utility.SendSmsOnlinePaymentConfirmation(Convert.ToString(ViewState["ContactNumber"]), Convert.ToString(ViewState["CustomerName"]), Convert.ToString(Session["OrderNumber"]));
                    }
                    else
                    {
                        lblSuccessHead.InnerHtml = "Fail Transactions";
                        lblSuccessHead.Style.Add("color", "red");
                        lblsuccessMsg.Text = "YOUR ORDER IS FAIL. Please contact our support team for more clarification.";
                    }
                }
                //else if (Request.UrlReferrer.ToString().Contains("thestruttstore.com"))
                else if (Request.UrlReferrer.ToString().Contains(System.Configuration.ConfigurationManager.AppSettings["siteUrl"]))
                {
                    CodResponse();
                    SendEmailConfirmOrder("COD", Convert.ToInt32(Session["OrderNumber"]));
                    // SMS send from proceedtopayment.aspx page.
                }
                //else if (Request.UrlReferrer.ToString().Contains("www.mobikwik.com"))     Unused
                //{
                //    SendEmailConfirmOrder("mobokwik", Convert.ToInt32(Session["OrderNumber"]));
                //}
                GetTrackpurchases();
                GetAdsConversion();
                FacebookPixelPurchase();

                //GetProductReview();   //TODO: comment by Kalpesh 05-Aug-2020

                OrderApi(Convert.ToInt32(Session["orderid"]), Session["CustomerLoginDetails"].ToString(), Session["Contact_Number"].ToString(), Session["User_Name"].ToString(), Convert.ToDecimal(ViewState["salPrice"]));
                BuyEvent();
                this.clearDetails();

                //OrderApi(Convert.ToInt32(Session["orderid"]), Session["CustomerLoginDetails"].ToString(), Session["Contact_Number"].ToString(), Session["User_Name"].ToString(), Convert.ToDecimal(ViewState["salPrice"]));
                //BuyEvent();
                // Logout if user type is Guest
                customer_handler customerHandler = new customer_handler();
                DataSet ds = customerHandler.check_guest_loginid(Convert.ToString(Session["CustomerLoginDetails"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["login_type"].ToString() == "guest")
                    {
                        Session.Clear();
                        Session.Abandon();
                    }
                }
            }
        }
       
        //protected void btnContinueShopping_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("category.aspx");
        //}

        private void BuyEvent()
        {
            string ProductIds = "";
            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart != null && dtCart.Rows.Count > 0)
            {
                foreach (DataRow row in dtCart.Rows)
                    ProductIds += @"""" + row["product_id"] + @""",";

                //ProductIds += "'" + row["product_id"] + "',";
                string buyevent = @"
                    wigzo (""track"", ""buy"", [" + ProductIds.Remove(ProductIds.Length - 1) + @"]);";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "buyevent", buyevent, true);
            }

        }

        private void OrderApi(int order_id, string emailid, string mobile, string username, decimal total)
        {

            string orderapi = @"wigzo(""track"", ""order"", {orderId:""" + order_id + @""", email: """ + emailid + @""", phone: """ + mobile + @""", fullName: """
                + username + @""", totalOrderValue: """ + total + @"""}); ";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "orderapi", orderapi, true);
        }
        private void FacebookPixelPurchase()  // For Facebook Pixel Code
        {
            string ProductIds = "";

            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart != null && dtCart.Rows.Count > 0)
            {
                foreach (DataRow row in dtCart.Rows)
                    ProductIds += "'" + row["product_id"] + "',";

                string FacebookPixelCode = @"
                    fbq('track', 'Purchase', {
                    content_ids: [" + ProductIds.Remove(ProductIds.Length - 1) + @"],
                    content_type: 'product',
                    value: " + Session["grdTotal"].ToString() + @",
                    currency: 'INR'
                    });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "FacebookPixelCode", FacebookPixelCode, true);
            }
        }


        //TODO: comment by Kalpesh 05-Aug-2020
        //private void GetProductReview()  // For Product Review Code
        //{
        //    string ProductReviewCode = @"
        //    window.renderOptIn = function() {
        //            window.gapi.load('surveyoptin', function() {
        //                window.gapi.surveyoptin.render(
        //                {
        //                    ""merchant_id"": 119671648,
        //                    ""order_id"": """ + Session["OrderNumber"].ToString() + @""",
        //                    ""email"": """ + Session["CustomerLoginDetails"] + @""",
        //                    ""delivery_country"": ""In"",
        //                    ""estimated_delivery_date"": """ + DateTime.Now.AddDays(3).ToString("yyyy-MM-dd") + @"""
        //                });
        //            });
        //            }";

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ProductReviewCode", ProductReviewCode, true);
        //}

        private Decimal GetCouponAmount(Decimal amount, Decimal coupcode)
        {
            Decimal CA = amount - (amount * coupcode) / 100;
            Decimal TCA = amount - CA;
            return TCA;
        }

        private void clearDetails()
        {
            lblOrderNo.Text = Session["OrderNumber"].ToString();
            Session["cartCount"] = null;
            Session["couponcodeprice"] = null;
            Session["couponCodeName"] = null;
            Session["sale_discount"] = null;
            Session["OrderNumber"] = null;
            ((DataTable)Session["Cart"]).Rows.Clear();
        }

        private void GetTrackpurchases()  // For Track purchases
        {
            string shippingCharge = "0";

            //string ProductIds = "", Productname = "", quantity = "", price = "", brand = "", menuname = "", variant = "", listposition="";
            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart != null && dtCart.Rows.Count > 0)

            {
                if (dtCart.Rows[0]["shipping_price"] != DBNull.Value)
                {
                    shippingCharge = dtCart.Rows[0]["shipping_price"].ToString();
                }

                string Trackpurchasescode = @"
                    gtag('event', 'purchase', {
                    ""transaction_id"":""" + Session["orderid"] + @""",
                    ""affiliation"":""The Strutt Store"",
                    ""value"":" + Session["grdTotal"].ToString() + @",
                    ""currency"":""INR"",
					""tax"":0,
					""shipping"":" + shippingCharge + @",
					items:[";




                foreach (DataRow row in dtCart.Rows)
                {
                    Trackpurchasescode += "{";
                    Trackpurchasescode += @"""id"":""" + row["product_id"] + @""",";
                    Trackpurchasescode += @"""name"":""" + row["product_name"] + @""",";
                    Trackpurchasescode += @"""list_name"":""" + @""",";
                    Trackpurchasescode += @"""brand"":""" + "Strutt" + @""",";
                    Trackpurchasescode += @"""category"":""" + row["menu_name"] + @""",";
                    Trackpurchasescode += @"""variant"":""" + row["color_name"] + @""",";
                    Trackpurchasescode += @"""list_position"":""" + "1" + @""",";
                    Trackpurchasescode += @"""quantity"":""" + row["quantity"] + @""",";
                    Trackpurchasescode += @"""price"":""" + row["Total"] + @"""";
                    Trackpurchasescode += "},";
                }
                Trackpurchasescode = Trackpurchasescode.Remove(Trackpurchasescode.Length - 1);


                Trackpurchasescode += "]";
                Trackpurchasescode += "});";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Trackpurchasescode", Trackpurchasescode, true);
            }
        }

        private void GetAdsConversion()  // For Track purchases
        {
            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart != null && dtCart.Rows.Count > 0)
            {

                string AdsConversion = @"
                    gtag('event', 'conversion', {
                    ""send_to"":""AW-827193669/Nc28CNOn8YoBEMXyt4oD"",
                    ""currency"":""INR"",
                    ""transaction_id"":""" + Session["orderid"].ToString() + @""",
                    ""value"":""" + Session["grdTotal"].ToString() + @"""});";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AdsConversion", AdsConversion, true);
            }
        }

        private bool CCAvenueResponse()
        {
            string workingKey = "AA66DDF3F895AB3818C39AF133E3473D";     //put in the 32bit alpha numeric key in the quotes provided here 	
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = null;
            string returnMsg = "Failed";        // "Failed - No response from CCAvenue. [From success page]";
            bool returnValue = false;

            //urlRespose = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["trandata"];
            //if (string.IsNullOrEmpty(urlRespose))
            //    urlRespose = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["encResp"];

            encResponse = Request.Form["encResp"];

            if (string.IsNullOrEmpty(encResponse))
            {
                UpdateOrderStatus(returnMsg, null);
                return true;
            }
            else
                encResponse = ccaCrypto.Decrypt(encResponse, workingKey);

            string[] segments = encResponse.Split('&');
            string Key, Value;
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    Key = parts[0].Trim();
                    Value = parts[1].Trim();
                    //Params.Add(Key, Value);

                    if (Key.Equals("order_status"))
                    {
                        if (!string.IsNullOrEmpty(Value) && Value.ToLower().Equals("success"))
                        {
                            returnMsg = "success";
                            returnValue = true;
                        }
                        else
                            returnValue = false;

                        break;
                    }
                }
            }

            UpdateOrderStatus(returnMsg, encResponse);
            return returnValue;
        }

        private bool PayUResponse()
        {
            bool returnValue = true;
            UpdateOrderStatus("success", Request.Form.ToString());
            return returnValue;
        }

        private bool CodResponse()
        {
            bool returnValue = true;
            UpdateOrderStatus("success", "COD Success");
            return returnValue;
        }

        private bool RazorPayResponse()
        {
            bool returnValue = true;
            UpdateOrderStatus("success", Request.Form.ToString());
            return returnValue;
        }

        private void UpdateOrderStatus(string paymentStatus, string paymentResponse)
        {
            order_handler orderHandler = new order_handler();
            order Order = new order();
            Order.order_id = Convert.ToInt32(Session["OrderNumber"].ToString());
            long orderid = Convert.ToInt64(Order.order_id);
            if (paymentStatus.Equals("success"))
            {
                Order.order_status = "Confirmed";
                Order.Flag = 1;                     // 1: conformed
            }
            else
            {
                Order.order_status = paymentStatus;
                Order.Flag = 5;                     // 1: Failed
                insertTempCart(Convert.ToInt32(orderid));

            }
            Order.payment_response = paymentResponse;
            orderHandler.update_order_status(Order);
        }

        public bool SendEmailConfirmOrder(string paymentType, int OrderNumber)
        {
            string ordPrifix = "STR10001";

            string PaymentMehtod = null;
            string msgbody = "";

            switch (paymentType)
            {
                //case "mobokwik":
                //    PaymentMehtod = "Online - MobiKwik";
                //    break;

                case "Razorpay":
                    PaymentMehtod = "Online - Razorpay";
                    break;

                case "PayUMoney":
                    PaymentMehtod = "Online - PayUMoney";
                    break;

                case "CCAvenue":
                    PaymentMehtod = "Online - CCAvenue";
                    break;

                case "COD":
                    PaymentMehtod = "Cash on Delivery";
                    break;

                    //default:
                    //    PaymentMehtod = "Cash on Delivery";
                    //    break;
            }

            string imgThumb, ProductUrl;

            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();
            double TotalPrice = 0;
            ds = orderHandler.get_order_search(OrderNumber, null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtCart = ds.Tables[0];
                TotalPrice = Convert.ToDouble(dtCart.Rows[0]["total_price"].ToString());       // Actual price (Total price + shipping - all discount)
                ViewState["CustomerName"] = dtCart.Rows[0]["customer_name"];
                ViewState["ContactNumber"] = dtCart.Rows[0]["contact_number"].ToString();
                //double subtotal = Convert.ToDouble(dtCart.Rows[0]["quantity"]) * Convert.ToDouble(dtCart.Rows[0]["sale_price"]);
                string siteurl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/assets/images/logo/logo.png";
                //double discount = Convert.ToDouble(dt.Rows[0]["discount"].ToString());
                string vieworderUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/account/orderhistory.aspx";
                string invoiceurl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/account/invoice.aspx?id=" + BusinessEntities.security.Encryptdata(dtCart.Rows[0]["order_id"].ToString());
                msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <h2 style='font-weight:normal;font-size:24px;margin:0 0 10px'> Thank you for your purchase! </h2>
                                                   <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + dtCart.Rows[0]["customer_name"].ToString() + @", we're getting your order ready to be shipped. We will notify you when it has been sent.
                                                    Your order ID is <b>" + ordPrifix + OrderNumber.ToString() + @".</b>
                                                    </p>
                                                    <table style='width:100%;border-spacing:8px;border-collapse:collapse;margin-top:20px'>
                                                        <tbody>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;line-height:0em'> &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                    <table style='border-spacing:8px;margin-top:19px'>
                                                                        <tbody>
                                                                            <tr>
                                                                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;border-radius:4px' align='center' bgcolor='#000000'><a href='https://thestruttstore.com/account/orderhistory.aspx' style='font-size:16px;text-decoration:none;display:block;color:#fff;padding:20px 25px' target='_blank' data=data -=- saferedirecturl='https://thestruttstore.com/account/orderhistory.aspx'> View your order</a></td>
                                                                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;border-radius:4px' align='center' bgcolor='#D0D0D0'><a href='https://thestruttstore.com/' style='font-size:16px;text-decoration:none;display:block;color:#fff;padding:20px 25px' target='_blank' data=data -=- saferedirecturl='https://thestruttstore.com/'> Visit our store</a></td>
                                                                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;border-radius:4px' align='center' bgcolor='#000000'>
                                                                            <a href='" + invoiceurl + @"' style='font-size:16px;text-decoration:none;display:block;color:#fff;padding:20px 25px' target='_blank'> View Invoice</a></td>
                                                                             </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";
                msgbody += @"<table style='width:100%;border-spacing:0;border-collapse:collapse'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <h3 style='font-weight:normal;font-size:20px;margin:0 0 25px'> Order summary </h3>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>";
                msgbody += @"<table style='width:100%;border-spacing:0;border-collapse:collapse'>

              <tbody>";
                for (int i = 0; i < dtCart.Rows.Count; i++)
                {
                    double ItemAmount = Convert.ToDouble(dtCart.Rows[i]["quantity"]) * Convert.ToDouble(dtCart.Rows[i]["sale_price"]);
                    imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dtCart.Rows[i]["thumb_image"].ToString().Replace(" ", "%20");
                    ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dtCart.Rows[i]["product_id"].ToString();


                    msgbody += @"<tr style ='width:100%;border-top-width:1px;border-top-color:#e5e5e5;border-top-style:solid'>
                                                                 <td style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-top:15px'>
                                                                 <table style='border-spacing:0;border-collapse:collapse'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                                    <img src='" + imgThumb + @"' align='left' width='60' height='60' style='margin-right:15px;border-radius:8px;border:1px solid #e5e5e5' class='CToWUd' />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;width:100%'>
                                                                                    <span style='font-size:16px;font-weight:600;line-height:1.4;color:#555'> " + dtCart.Rows[i]["product_name"].ToString() + @" * " + dtCart.Rows[i]["quantity"].ToString() + @"</span><br />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;white-space:nowrap'>
                                                                                    <p style ='color:#777;line-height:150%;font-size:16px;margin:0'>
                                                                        <span style ='font-size:16px'> Rate <strike style ='font-size:16px;color:#555'> Rs. " + (dtCart.Rows[i]["Price"].ToString() + @"</strike></span>
                                                                    </p>
                                                                                    <p style ='color:#555;line-height:150%;font-size:16px;font-weight:600;margin:0 0 0 15px' align='right'>
                                                                                   Rs." + (Convert.ToInt32(dtCart.Rows[i]["quantity"]) * Convert.ToSingle(dtCart.Rows[i]["sale_price"])).ToString() + @"
                                                                                    </p>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>");
                }
                msgbody += @"</tbody>
                                                    </table>";


                msgbody += @"<table style ='width:100%;border-spacing:0;border-collapse:collapse;margin-top:20px;border-top-width:2px;border-top-color:#e5e5e5;border-top-style:solid'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:20px 0 0'>
                                                                                    <p style ='color:#777;line-height:1.2em;font-size:16px;margin:0'>
                                                                                        <span style='font-size:16px'>Grand Total</span>
                                                                                    </p>
                                                                                </td>
                                                                                <td  style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:20px 0 0' align='right'>
                                                                                    <strong style ='font-size:24px;color:#555'>Rs." + TotalPrice.ToString("0.00") + @"</strong>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";

                msgbody += @"<table>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:40px 0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <h3 style='font-weight:normal;font-size:20px;margin:0 0 25px'> Customer information</h3>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style ='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                     <table style='width:100%;border-spacing:0;border-collapse:collapse'>
                                                        <tbody>
                                                           <tr>
     <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;width:50%'>
         <h4 style='font-weight:500;font-size:16px;color:#555;margin:0 0 5px'> Customer Details</h4>
         <p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["user_name"].ToString() + @"<br />" + dtCart.Rows[0]["email_id"].ToString() + @"<br /></p>
         <p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["contact_number"].ToString() + @"<br />" + dtCart.Rows[0]["address"].ToString() + @"<br /></p>
         <p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["pin_code"].ToString() + @"<br /></p>
     </td>
 </tr>
                                                    </tbody>
                                                    </table>
<table style='width:100%;border-spacing:0;border-collapse:collapse'>
                                                        <tbody>
                                                           <tr>
     <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;width:50%'>
         <h4 style='font-weight:500;font-size:16px;color:#555;margin:0 0 5px'> Shipping address</h4>
         <p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["address"].ToString() + @"<br />" + dtCart.Rows[0]["state"].ToString() + @"<br /></p>
         <p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["city"].ToString() + @"</p>
         <p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["pin_code"].ToString() + @"</p>
     </td>
     <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;width:50%'>
         <h4 style='font-weight:500;font-size:16px;color:#555;margin:0 0 5px'> Billing address</h4>
         <p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["address"].ToString() + @"<br />" + dtCart.Rows[0]["state"].ToString() + @"<br /></p>
<p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["city"].ToString() + @"</p>
<p style='color:#777;line-height:150%;font-size:16px;margin:0'> " + dtCart.Rows[0]["pin_code"].ToString() + @"</p>
     </td>
 </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style ='width:100%;border-spacing:0;border-collapse:collapse'>
                                                        <tbody>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;width:50%'>
                                                                    <h4 style='font-weight:500;font-size:16px;color:#555;margin:0 0 5px'> Shipping method</h4>
                                                                    <p style ='color:#777;line-height:150%;font-size:16px;margin:0'> Generic Shipping</p>
                                                                </td>
                                                                <td style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;width:50%'>
                                                                    <h4 style='font-weight:500;font-size:16px;color:#555;margin:0 0 5px'> Payment method</h4>
                                                                    <p style ='color:#777;line-height:150%;font-size:16px;margin:0'>
                                                                        <span style ='font-size:16px'> " + PaymentMehtod + @" — <strong style ='font-size:16px;color:#555'> Rs. " + TotalPrice.ToString("0.00") + @"</strong></span>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";

                msgbody += @"<table>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:35px 0'>
                                <center>
                                    <table style ='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>

                                                    <p style ='color:#999;line-height:150%;font-size:14px;margin:0'> If you have any questions, reply to this email or contact us at<a href='mailto:connect@thestruttstore.com' style='font-size:14px;text-decoration:none;color:#000000' target='_blank'> connect@thestruttstore.com</a></p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>
               </td>
           </tr>
       </tbody>
   </table>";
                DAL.Utility.sendEmail("STRUTT - Order No " + " " + ordPrifix + OrderNumber.ToString() + " has been placed successfully.", msgbody,
                    "HURRAY!!", "PURCHASE CONFIRMATION", dtCart.Rows[0]["email_id"].ToString(), ConfigurationManager.AppSettings["emailCc"],
                    ConfigurationManager.AppSettings["emailBccMarketing"]);

            }
            return true;
        }
        //New Templeat
        private void insertTempCart(int OrderNumber)
        {
            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();

            ds = orderHandler.get_order_search(OrderNumber, null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string emailid = dt.Rows[0]["email_id"].ToString();
                string phn = dt.Rows[0]["contact_number"].ToString();
                    string add = dt.Rows[0]["address"].ToString();
                    string landmark = dt.Rows[0]["land_mark"].ToString();
                    string pincode = dt.Rows[0]["pin_code"].ToString();

                    if (string.IsNullOrEmpty(emailid))
                        return;

                    string city = string.Empty, state = string.Empty, name = string.Empty;
                    city = dt.Rows[0]["city"].ToString();
                    state = dt.Rows[0]["state"].ToString();
                    if (string.IsNullOrEmpty(dt.Rows[0]["user_name"].ToString()))
                        name = dt.Rows[0]["user_name"].ToString();

                    temp_cart_handler cartHandler = new temp_cart_handler();
                    int temp_customer_Id = cartHandler.insert_temp_customer(emailid, name, phn, add, landmark, city, state, "India", pincode, false, "Failed Payment");

                    DataTable dtCart = (DataTable)Session["Cart"];
                    if (dtCart != null && dtCart.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCart.Rows.Count; i++)
                        {
                            cartHandler.insert_temp_cart(temp_customer_Id, Convert.ToInt64(dtCart.Rows[i]["product_id"]), Convert.ToInt32(dtCart.Rows[i]["quantity"]));
                        }
                    }
                }
            }
        }
    }