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
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace strutt
{
    public partial class success_XpressLeanResponse : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.BuyEvent();


                string encryptedText = Request.Form["encrypted_payload"];

                string secretKey = ConfigurationManager.AppSettings["XpressLanesecretkey"].ToString();
                secretKey = secretKey.Substring(0, 16);

                string decryptedText = OpenSSLDecrypt(encryptedText, secretKey);



                var CustomerOrderDetails = JsonConvert.DeserializeObject<XpressLaneResponse>(decryptedText);

                Guid customer_id = Guid.NewGuid();
                bool result = false;
                customer_handler customerHandler = new customer_handler();
                order_handler orderHandler = new order_handler();

                DataSet DS = customerHandler.check_isexistxpresslanecustomer(CustomerOrderDetails.email);
                if (DS != null && DS.Tables.Count > 0)
                {
                    DataTable dt = DS.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        customer_id = Guid.Parse(dt.Rows[0]["customer_id"].ToString());
                    }
                }
                result = customerHandler.insert_update_Xprsslane_customer(customer_id, CustomerOrderDetails.email);
                receiver_address receiverAddress = new receiver_address();

                receiverAddress.customer_details_id = 0;
                receiverAddress.customer_id = Guid.Parse(customer_id.ToString());
                receiverAddress.full_name = CustomerOrderDetails.billing_firstname + (string.IsNullOrEmpty(CustomerOrderDetails.billing_lastname) ? "" : " " + CustomerOrderDetails.billing_lastname);
                receiverAddress.contact_number = CustomerOrderDetails.billing_telephone;
                receiverAddress.email_id = CustomerOrderDetails.email;
                receiverAddress.address = CustomerOrderDetails.billing_street_1;
                receiverAddress.land_mark = CustomerOrderDetails.billing_street_2;
                receiverAddress.city = CustomerOrderDetails.billing_city;
                receiverAddress.state = CustomerOrderDetails.billing_region;
                receiverAddress.pin_code = CustomerOrderDetails.billing_postcode;




                receiver_address_handler receiverAddressHandler = new receiver_address_handler();
                result = receiverAddressHandler.insert_customer_address(receiverAddress);

                DAL.order_data lodc = new DAL.order_data();
                Guid merchantorder_id;
                merchantorder_id = Guid.Parse(CustomerOrderDetails.merchantorderid);
                DataSet DSOrder = lodc.check_order(merchantorder_id);
                Session["OrderNumber"] = merchantorder_id;

                if (DSOrder != null && DSOrder.Tables.Count > 0)
                {
                    DataTable dt = DSOrder.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        BusinessEntities.order orderDetails = new order();
                        for (int i = 0; i < CustomerOrderDetails.orderitems.Count; i++)
                        {
                            orderDetails.order_id = Convert.ToInt64(dt.Rows[0]["order_id"].ToString());
                            orderDetails.customer_id = Guid.Parse(customer_id.ToString());
                            orderDetails.state = CustomerOrderDetails.shipping_region;
                            orderDetails.city = CustomerOrderDetails.shipping_city;
                            orderDetails.address = CustomerOrderDetails.shipping_street_1;
                            orderDetails.pin_code = CustomerOrderDetails.shipping_postcode;
                            orderDetails.contact_number = CustomerOrderDetails.shipping_telephone;
                            orderDetails.user_name = CustomerOrderDetails.shipping_firstname;

                        }
                        //Addedby Hetal Patel on 20-10-2020
                        Session["OrderID"] = orderDetails.order_id;
                        OrderApi(Convert.ToInt32(orderDetails.order_id), CustomerOrderDetails.email, CustomerOrderDetails.billing_telephone, CustomerOrderDetails.billing_firstname, Convert.ToDecimal(CustomerOrderDetails.totalAmount));
                        //Addedby Hetal Patel on 20-10-2020

                        result = lodc.update_order_Xpresslanecustomer(orderDetails);
                        UpdateOrderStatus(CustomerOrderDetails.xpresslane_payment_status, decryptedText, CustomerOrderDetails.paymentmode);

                       
                        
                        SendEmailConfirmOrder("Xpresslane-Online", Convert.ToInt32(Session["OrderID"]));
                        Utility.SendSmsOnlinePaymentConfirmation(Convert.ToString(orderDetails.contact_number), Convert.ToString(CustomerOrderDetails.billing_firstname), Convert.ToString(Session["OrderID"]));
                    }
                }

            }
        }
        private void UpdateOrderStatus(string paymentStatus, string Response, string Paymentmode)
        {
            order_handler orderHandler = new order_handler();
            order Order = new order();
            Order.XpressMerchantorder_id = Guid.Parse(Session["OrderNumber"].ToString());
            Order.order_id = Convert.ToInt32(Session["OrderID"].ToString());
            long orderid = Convert.ToInt64(Order.order_id);
            if (Paymentmode == "COD")
            {
                Paymentmode = "X-COD";
            }
            else
            {
                Paymentmode = "X-Online";
            }

            Order.PaymentThrough = Paymentmode;
            if (paymentStatus.Equals("SUCCESS"))
            {
                Order.order_status = "Confirmed";
                Order.Flag = 1;                     // 1: conformed
            }
            else
            {
                Order.order_status = paymentStatus;
                Order.Flag = 5;                     // 1: Failed
            }
            Order.payment_response = Response;
            orderHandler.update_order_status(Order);
            orderHandler.update_order_PaymentType(Convert.ToInt32(Order.order_id), Order.PaymentThrough);
        }

        public static string OpenSSLDecrypt(string encrypted, string passphrase)
        {
            //get the key bytes (not sure if UTF8 or ASCII should be used here doesn't matter if no extended chars in passphrase)
            var key = Encoding.UTF8.GetBytes(passphrase);
            Console.WriteLine(key.Length);
            //pad key out to 32 bytes (256bits) if its too short

            //setup an empty iv
            var iv = new byte[16];

            //get the encrypted data and decrypt
            byte[] encryptedBytes = Convert.FromBase64String(encrypted);
            return DecryptStringFromBytesAes(encryptedBytes.Skip(16).ToArray(), key, encryptedBytes.Take(16).ToArray());
        }
        static string DecryptStringFromBytesAes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            // Create a RijndaelManaged object
            // with the specified key and IV.
            aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7, KeySize = 128, BlockSize = 128, Key = key, IV = iv };

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                        srDecrypt.Close();
                    }
                }
            }

            return plaintext;
        }

        private void BuyEvent()
        {
            string ProductIds = "";
            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart != null && dtCart.Rows.Count > 0)
            {
                foreach (DataRow row in dtCart.Rows)
                    ProductIds += @"""" + row["product_id"] + @""",";

                string buyevent = @"wigzo (""track"", ""buy"", [" + ProductIds.Remove(ProductIds.Length - 1) + @"]);";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "buyevent", buyevent, true);
            }

        }
        private void OrderApi(int order_id, string emailid, string mobile, string username, decimal total)
        {

            string orderapi = @"wigzo(""track"", ""order"", {orderId:""" + order_id + @""", email: """ + emailid + @""", phone: """ + mobile + @""", fullName: """
                + username + @""", totalOrderValue: """ + total + @"""); ";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "orderapi", orderapi, true);
        }

        public bool SendEmailConfirmOrder(string paymentType, int OrderNumber)
        {
            string ordPrifix = "STR10001";
            string PaymentMehtod = paymentType;
            string msgbody = "";

            
            string imgThumb, ProductUrl;
            double TotalPrice = 0;

            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();
            double ShippingCharge = 0;

            ds = orderHandler.get_order_search(OrderNumber, null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                TotalPrice = Convert.ToDouble(dt.Rows[0]["price"].ToString());       // Actual price (Total price + shipping - all discount)

                ViewState["CustomerName"] = dt.Rows[0]["customer_name"];
                ViewState["ContactNumber"] = dt.Rows[0]["contact_number"].ToString();

                msgbody = @"<tr><td valign='top' style='padding:40px; margin:0px;'>
                         <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>Hi " + dt.Rows[0]["customer_name"].ToString() + @"</p>
                         <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>Thank you for shopping at thestruttstore.com.</p>
                         <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>We have received your order and will get started on handcrafting it right away. 
                            Once your order has been processed and is on its way, we will send you a dispatch confirmation with your tracking details and expected delivery date.</p>
                         <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>Please have a look at your order summary in brief below.</p>
                        </td></tr>
                      <tr>
                        <td valign='top' style='padding:0px 113px; margin:0px;'>
                        <table width='100%' border='0'>
                      <tr>
                        <td colspan='2' style='background:#7cd5f3; height:50px; color:#fff; font-size:18px; text-align:center; vertical-align:middle; text-transform:uppercase; font-family:Arial, Helvetica, sans-serif;'>Order Summary</td>
                        </tr>
                      <tr>
                        <td width='50%' style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:left; text-transform:uppercase; padding:10px;'>Order ID</td>
                        <td width='50%' style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:right; text-transform:uppercase; padding:10px;'>" + ordPrifix + OrderNumber.ToString() + @"</td>
                      </tr>
                      <tr>
                        <td style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:left; text-transform:uppercase; padding:10px;'>Purchase Date</td>
                        <td style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:right; text-transform:uppercase; padding:10px;'>" + dt.Rows[0]["OrdDate"].ToString() + @"</td>
                      </tr>
                      <tr>
                        <td style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:left; text-transform:uppercase; padding:10px;'>Payment method</td>
                        <td style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:right; text-transform:uppercase; padding:10px;'>" + PaymentMehtod + @"</td>
                      </tr>
                      <tr>
                        <td style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:left; text-transform:uppercase; padding:10px;'>Billing Details</td>
                        <td style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:right; text-transform:uppercase; padding:10px;'>Delivery Address</td>
                      </tr>
                      <tr>
                        <td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; text-align:left; text-transform:uppercase; padding:10px;'>"
                                        + dt.Rows[0]["customer_name"].ToString() + "<br/>" + "Email: " + dt.Rows[0]["email_id"].ToString() + "<br/>" + "Phone: " + dt.Rows[0]["contact_number"].ToString() + @"</td>
                        <td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; text-align:right; text-transform:uppercase; padding:10px;'>" + dt.Rows[0]["user_name"].ToString() + "<br/>"
                                        + dt.Rows[0]["address"].ToString() + (string.IsNullOrEmpty(dt.Rows[0]["land_mark"].ToString()) ? "" : ", " + dt.Rows[0]["land_mark"].ToString())
                                        + ", " + dt.Rows[0]["city"].ToString() + ", " + dt.Rows[0]["state"].ToString() + ". " + dt.Rows[0]["pin_code"].ToString() + @"</td>
                      </tr>
                      <tr>
                        <td colspan='2' style='border-top:1px solid #7CD5F3'>&nbsp;</td>
                      </tr>
                    
                      <tr>
                        <td colspan='2'><table width='100%' border='0'>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double ItemAmount = Convert.ToDouble(dt.Rows[i]["quantity"]) * Convert.ToDouble(dt.Rows[i]["sale_price"]);
                    imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dt.Rows[i]["thumb_image"].ToString().Replace(" ", "%20");
                    ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dt.Rows[i]["product_id"].ToString();

                    msgbody += @"<tr>
                            <td width='17%'><img style='height:90px;' src='" + imgThumb + @"'/></td>
                            <td width='52%' style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:center'><a target='_blank' style='color:#FB641B' href='" + ProductUrl + "'>" + dt.Rows[i]["product_name"].ToString() + @"</a></td>
                            <td width='13%'  style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:center'>" + dt.Rows[i]["quantity"].ToString() + @"</td>
                            <td width='18%'  style='font-family:Arial, Helvetica, sans-serif; font-size:15px; text-align:right'>Rs. " + ItemAmount.ToString("0.00") + @"</td>
                    </tr>";
                }

                msgbody += @"
                          <tr>
                            <td colspan='4' style='border-top:1px solid #7CD5F3'>&nbsp;</td>
                            </tr>
                          <tr>
                            <td colspan='3' style='font-family:Arial, Helvetica, sans-serif ; font-size:15px; text-align:right;'>Grand Total</td>
                            <td style='font-family:Arial, Helvetica, sans-serif ; font-size:15px; text-align:right;'>Rs. " + TotalPrice.ToString("0.00") + @"</td>
                          </tr>
                        </table></td>
                        </tr>
                      <tr>
                        <td colspan='2'>&nbsp;</td>
                        </tr>
                    </table></td></tr>";

                DAL.Utility.sendEmail("STRUTT - Order No " + " " + ordPrifix + OrderNumber.ToString() + " has been placed successfully.", msgbody,
                    "HURRAY!!", "PURCHASE CONFIRMATION", dt.Rows[0]["email_id"].ToString(), ConfigurationManager.AppSettings["emailCc"],
                    ConfigurationManager.AppSettings["emailBccMarketing"]);
            }
            return true;
        }
    }
}
