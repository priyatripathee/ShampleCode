using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace strutt
{
    public partial class XpressLeanRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Post();
            }
            //if (!IsPostBack)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Post", "Post()", true);
            //}

        }
        #region Xpresslane


        protected void Post()
        {
            try
            {

                double GstRate = 18.0f, GstValue;
                string shippingcharge;
                DataTable dtCart = (DataTable)Session["Cart"];
                if (dtCart != null || dtCart.Rows.Count != 0)
                {
                    decimal amount = (from DataRow drCart in dtCart.AsEnumerable()
                                      where drCart.RowState != DataRowState.Deleted
                                      select (Convert.ToDecimal(drCart["Total"]))).Sum();

                    decimal totalAmount = Convert.ToDecimal(Session["grdTotal"]);

                    //(from DataRow drCart in dtCart.AsEnumerable()
                    //                   where drCart.RowState != DataRowState.Deleted
                    //                   select (Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();
                    Decimal total = (from DataRow drCart in dtCart.AsEnumerable()
                                           where drCart.RowState != DataRowState.Deleted
                                           select (Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();


                    decimal salesPrice = (from DataRow drCart in dtCart.AsEnumerable()
                                          where drCart.RowState != DataRowState.Deleted
                                          select (Convert.ToDecimal(drCart["sale_price"]))).Sum();

                    decimal FlatDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                                            where drCart.RowState != DataRowState.Deleted
                                            select ((Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["discount"]) / 100) * Convert.ToDecimal(drCart["quantity"]))).Sum();

                    decimal couponDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                                              where drCart.RowState != DataRowState.Deleted
                                              select (Convert.ToDecimal(drCart["coupon_discount"]))).Sum();

                    decimal totalCustomeBagCharge = (from DataRow drCart in dtCart.AsEnumerable()
                                                     where drCart.RowState != DataRowState.Deleted
                                                     select (Convert.ToDecimal(drCart["custom_bag_price"]))).Sum();


                    Decimal OnlinePaymentPersantage = 0;
                    OnlinePaymentPersantage = ((totalAmount * 5) / 100);


                    decimal TotalDiscount = 0;
                    TotalDiscount = FlatDiscount + couponDiscount + OnlinePaymentPersantage;




                    GstValue = Convert.ToDouble(totalAmount) * GstRate / (100 + GstRate);

                   
                    if (total - TotalDiscount > 750)
                        shippingcharge = "0.00";
                    else
                        shippingcharge = String.Format("{0:0.00}", Convert.ToDecimal(ConfigurationManager.AppSettings["shippingcharge"]));


                    ViewState["ShippingCharge"] = shippingcharge;


                    XpressLanePost obj = new XpressLanePost();
                    obj.merchantsuccessurl = ConfigurationManager.AppSettings["siteUrl"] + "/" + ConfigurationManager.AppSettings["XpressLanemerchantsuccessurl"].ToString();
                    obj.merchantcarturl = ConfigurationManager.AppSettings["siteUrl"] + Session["XpressLanemerchantcarturl"].ToString();
                    obj.merchantid = ConfigurationManager.AppSettings["XpressLanemerchantid"].ToString();
                    obj.secretkey = ConfigurationManager.AppSettings["XpressLanesecretkey"].ToString();

                    obj.merchantorderid = Session["OrderNumberGUID"].ToString();


                    var date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");

                    obj.orderdate = date.ToString();

                    Decimal grandTotal = salesPrice + Convert.ToDecimal(shippingcharge) - TotalDiscount + totalCustomeBagCharge;


                    Session["grandTotal"] = grandTotal;

                    obj.grandtotal = grandTotal.ToString();
                    Decimal shippingtotal = salesPrice - TotalDiscount;
                    if (ViewState["ShippingCharge"] == null || ViewState["ShippingCharge"].ToString() == "0.00")
                    {
                        obj.preshiptotal = totalAmount.ToString();
                    }
                    else
                    {

                        obj.preshiptotal = shippingtotal.ToString(); //(grandTotal - Convert.ToDecimal(ViewState["ShippingCharge"])).ToString();
                    }
                    if (Session["couponCodeName"] != null)
                    {
                        obj.coupon_code = Session["couponCodeName"].ToString();
                    }
                    else
                    {
                        obj.coupon_code = "";
                    }
                    //obj.cgst = (GstValue / 2).ToString("0.00");
                    obj.cgst = "0.00";
                    obj.coupondiscount = Convert.ToDecimal(couponDiscount);
                    obj.discount = Convert.ToDecimal(TotalDiscount);
                    obj.subtotal = shippingtotal.ToString();
                    obj.total = shippingtotal.ToString();
                    obj.currency = "INR";


                    OrderitemList Orderitem;

                    obj.orderitems = new List<OrderitemList>();

                    for (int i = 0; i < dtCart.Rows.Count; i++)
                    {
                        Orderitem = new OrderitemList();
                        Orderitem.productname = dtCart.Rows[i]["product_name"].ToString();
                        Orderitem.sku = dtCart.Rows[i]["sub_menu_name"].ToString();
                        Orderitem.productdescription = dtCart.Rows[i]["product_name"].ToString();
                        Orderitem.unitprice = dtCart.Rows[i]["sale_price"].ToString();
                        Orderitem.discountamount = 0;
                        Orderitem.discountunitprice = 0;
                        Orderitem.originalprice = dtCart.Rows[i]["sale_price"].ToString();
                        Orderitem.actualprice = dtCart.Rows[i]["sale_price"].ToString();
                        Orderitem.productimage = ConfigurationManager.AppSettings["siteUrl"] + "/" + dtCart.Rows[i]["thumb_image"].ToString();
                        //Orderitem.productimage = ConfigurationManager.AppSettings["XpressLaneproductURL"].ToString();
                        Orderitem.merchantproductid = Convert.ToInt32(dtCart.Rows[i]["product_id"]);
                        Orderitem.quantity = Convert.ToByte(dtCart.Rows[i]["quantity"]);
                        obj.orderitems.Add(Orderitem);
                    }

                    Shipping Shippingoptions;
                    obj.shippingoptions = new List<Shipping>();

                    Shippingoptions = new Shipping();
                    Shippingoptions.shippingcode = obj.coupon_code;
                    Shippingoptions.shippingname = obj.coupon_code;
                    Shippingoptions.shippingprice = shippingcharge;
                    obj.shippingoptions.Add(Shippingoptions);

                    JavaScriptSerializer jss = new JavaScriptSerializer();

                    // serialize into json string
                    var orderdata = jss.Serialize(obj);


                    string secretKey = obj.secretkey;
                    secretKey = secretKey.Substring(0, 16);

                    string encryptedText = OpenSSLEncrypt(orderdata, secretKey);


                    merchantid.Value = ConfigurationManager.AppSettings["XpressLanemerchantid"].ToString();
                    checksum.Value = encryptedText;

                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }


        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            //byte[] ret = new byte[first.Length + second.Length];
            //Array.Copy(first, 0, ret, 0, first.Length);
            //Array.Copy(second, 0, ret, first.Length, second.Length);

            byte[] ret = new byte[first.Length + second.Length];
            System.Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            System.Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
        public static string OpenSSLEncrypt(string unencrytptedText, string passphrase)
        {
            //get the key bytes (not sure if UTF8 or ASCII should be used here doesn't matter if no extended chars in passphrase)
            var key = Encoding.UTF8.GetBytes(passphrase);
            Random rnd = new Random();
            var iv = new Byte[16];
            rnd.NextBytes(iv);
            var aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7, KeySize = 128, BlockSize = 128, Key = key, IV = iv };


            using (var memoryStream = new MemoryStream())
            {
                using (
                    var cryptoStream = new CryptoStream(
                        memoryStream, aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV), CryptoStreamMode.Write))
                {
                    using (var streamWiter = new StreamWriter(cryptoStream))
                    {
                        streamWiter.Write(unencrytptedText);
                    }
                }
                return Convert.ToBase64String(Combine(iv, memoryStream.ToArray()));
            }
            //get the encrypted data and decrypt
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

        #endregion


    }
}