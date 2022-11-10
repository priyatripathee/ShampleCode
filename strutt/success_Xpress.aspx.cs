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
    public partial class success_Xpress : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                this.BuyEvent();

                //OrderApi(Convert.ToInt32(Session["orderid"]), Session["CustomerLoginDetails"].ToString(), Session["Contact_Number"].ToString(), Session["User_Name"].ToString(), Convert.ToDecimal(ViewState["salPrice"]));

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

                        result = lodc.update_order_Xpresslanecustomer(orderDetails);
                        UpdateOrderStatus(CustomerOrderDetails.xpresslane_payment_status, CustomerOrderDetails.paymentmode);
                    }
                }

            }
        }
        private void UpdateOrderStatus(string paymentStatus, string paymentResponse)
        {
            order_handler orderHandler = new order_handler();
            order Order = new order();
            Order.XpressMerchantorder_id = Guid.Parse(Session["OrderNumber"].ToString());
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
            Order.payment_response = paymentResponse;
            orderHandler.update_order_status(Order);
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

    }
}
