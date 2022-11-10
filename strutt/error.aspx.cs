using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BLL;
using CCA.Util;

namespace strutt
{
    public partial class error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Response.Write(Request.Form);
                lblResponse.Text=Request.Form.ToString();

                if (Request.Form["encResp"] != null)
                    CCAvenueResponse();
                else if (Request.Form != null)
                    UpdateOrderStatus("Failed", Convert.ToString(Request.Form));
                else
                    UpdateOrderStatus("Failed", null);

            }
        }

        private bool CCAvenueResponse()
        {
            string workingKey = "AA66DDF3F895AB3818C39AF133E3473D";     //put in the 32bit alpha numeric key in the quotes provided here 	
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = null;
            string returnMsg = "Failed";

            encResponse = Request.Form["encResp"];
            if (string.IsNullOrEmpty(encResponse))
            {
                UpdateOrderStatus(returnMsg, null);
                return false;
            }
            else
            {
                encResponse = ccaCrypto.Decrypt(encResponse, workingKey);
                UpdateOrderStatus(returnMsg, encResponse);
                return true;
            }
        }

        private void UpdateOrderStatus(string paymentStatus, string paymentResponse)
        {
            order_handler orderHandler = new order_handler();
            order Order = new order();
            Order.order_id = Convert.ToInt32(Session["OrderNumber"].ToString());
            Order.order_status = paymentStatus;
            Order.Flag = 5;                     // 5: Fail
            Order.payment_response = paymentResponse;
            orderHandler.update_order_status(Order);
        }

    }
}