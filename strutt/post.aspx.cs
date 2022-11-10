using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;

namespace strutt
{
    public partial class post : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            RazorpaySubmit();
            RazorpayLoadData();
        }

        private void RazorpayLoadData()
        {
            string key = "rzp_test_1UXyCE31U2snnE";
            int totalAmount;

            if (Session["CustomerData"] != null)
            {
                if (Session["CustomerLoginDetails"].ToString().Equals("kalpesh013@gmail.com") || Session["CustomerLoginDetails"].ToString().Equals("vishesh@thestruttstore.com"))
                    totalAmount = 100;
                else
                    totalAmount = Convert.ToInt32((Convert.ToSingle("10") * 100));

                //data - order_id = ""<%=orderId%>""

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
                            data-theme.color=""#F37254""></script> ", key, totalAmount, "Payment of Tesing", "Chandni", Session["CustomerLoginDetails"].ToString(), "9979730046");

                litRazor.Text = jsRequest;
            }


        }

        private void RazorpaySubmit()
        {
            string key = "rzp_test_1UXyCE31U2snnE";
            string secret = "fiLv1bBe4vWFMFquNXnmjmvN";
            RazorpayClient client = new RazorpayClient(key, secret);

        }
    }
}