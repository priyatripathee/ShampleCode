using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using System.Collections.Specialized;

namespace strutt
{
    public partial class ResponseHandler : System.Web.UI.Page
    {
        string WorkingKey = "m8tawcoeplb5b738s8ewpwcmh55dkd0e";

        protected void Page_Load(object sender, EventArgs e)
        {
            CCACrypto func = new CCACrypto();
            string encResponse = func.Decrypt(Request.Form["encResponse"], WorkingKey);
            //Response.Write(encResponse);


            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();

                    Params.Add(Key, Value);
                }
            }

            string strVerify = func.verifychecksum(Params["Merchant_Id"].ToString(), Params["Order_Id"].ToString(), Params["Amount"].ToString(), Params["AuthDesc"].ToString(), WorkingKey, Params["Checksum"].ToString());

            for (int i = 0; i < Params.Count; i++)
            {
                Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
            }
            Response.Write("VerifyChecksum = " + strVerify + "<br/>");
        }
    }
}