using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CCA.Util;

namespace strutt
{
    public partial class ccavRequestHandler : System.Web.UI.Page
    {
        //CCACrypto chkSum = new CCACrypto();
        //string WorkingKey = "m8tawcoeplb5b738s8ewpwcmh55dkd0e";

        //CCACrypto ccaCrypto = new CCACrypto();
        //string workingKey = "m8tawcoeplb5b738s8ewpwcmh55dkd0e";
        //string ccaRequest = "";
        public string strEncRequest = "";
        public string strAccessCode = "";// put the access key in the quotes provided here.
        //string MerchantId = "M_raj35034_35034";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strEncRequest = Request.QueryString["request"];
                strAccessCode = Request.QueryString["access_code"];

                //Merchant_Id.Value = MerchantId;

                //string billingPageHeading;
                //lblOrderId.Text = "1123";
                //lblAmount.Text = "50";
                //lblMerchantId.Text = "M_raj35034_35034";
                //lblRedirectUrl.Text = "SubmitData.aspx";
                //lblCustomerName.Text = "shahid";
                //lblCustAddr.Text = "E-123, Noida";
                //lblCustCountry.Text = "India";
                //lblCustPhone.Text = "9540844995";
                //lblCustEmail.Text = "shahidit.net@gmail.com";
                //lblCustState.Text = "UP";
                //lblCustCity.Text = "Noida";
                //lblZipCode.Text = "201301";
                //lblCustNotes.Text = "note test";
                //lblDelCustName.Text = "shahid";
                //lblDelCustAddr.Text = "E-12, Noida";
                //lblDelCustCntry.Text = "India";
                //lblDelCustTel.Text = "9540844995";
                //lblDelCustState.Text = "UP";
                //lblDelCustCity.Text = "Noida";
                //lblDelZipCode.Text = "201301";
                //lblMerchantParam.Text = "mts";
                //lblPayType.Text = "ING_N";
                //billingPageHeading = "ABCDEFG" != null ? "123456" : "";

                //string Res = chkSum.getchecksum(lblMerchantId.Text, lblOrderId.Text, lblAmount.Text, lblRedirectUrl.Text, WorkingKey);

                //string ToEncrypt = "Order_Id=" + lblOrderId.Text + "&Amount=" + lblAmount.Text + "&Merchant_Id=" + lblMerchantId.Text + "&Redirect_Url=" + lblRedirectUrl.Text +
                //    "&Checksum=" + Res + "&billing_cust_name=" + lblCustomerName.Text + "&billing_cust_address=" + lblCustAddr.Text + "&billing_cust_country=" + lblCustCountry.Text +
                //    "&billing_cust_tel=" + lblCustPhone.Text + "&billing_cust_email=" + lblCustEmail.Text + "&billing_cust_state=" + lblCustState.Text +
                //    "&billing_cust_city=" + lblCustCity.Text + "&billing_zip_code=" + lblZipCode.Text + "&billing_cust_notes=" + lblCustNotes.Text +
                //    "&delivery_cust_name=" + lblDelCustName.Text + "&delivery_cust_address=" + lblDelCustAddr.Text + "&delivery_cust_country=" + lblDelCustCntry.Text +
                //    "&delivery_cust_tel=" + lblDelCustTel.Text + "&delivery_cust_state=" + lblDelCustState.Text + "&delivery_cust_city=" + lblDelCustCity.Text +
                //    "&delivery_zip_code=" + lblDelZipCode.Text + "&Merchant_Param=" + lblMerchantParam.Text + "&billingPageHeading=" + billingPageHeading + "&payType=" + lblPayType.Text;


                //string Encrypted;


                //Encrypted = chkSum.Encrypt(ToEncrypt, WorkingKey);

                //Merchant_Id.Value = lblMerchantId.Text;
                //encRequest.Value = Encrypted;

                //Response.Redirect("http://www.ccavenue.com/shopzone/cc_details.jsp");
            }
        }
    }
}