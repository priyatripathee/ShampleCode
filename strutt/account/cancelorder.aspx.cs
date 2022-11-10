using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using BusinessEntities;
using System.Configuration;
using strutt.api;
using api;

namespace strutt.account
{
    public partial class cancelorder : System.Web.UI.Page
    {
        long ordId = 0;
        string Email = "";
        string msgbody = "";
        string ordPrifix = "STR10001";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["success"] != null && Request.QueryString["success"] == "1")
                {
                    lblMsg.Text = "Cancelled Successfully.";
                    tblreason.Visible = false;
                }

                if (Session["CustomerLoginDetails"] != null)
                {
                    Email = Session["CustomerLoginDetails"].ToString();
                    if (Request.QueryString["ordid"] != null)
                    {
                        tblreason.Visible = true;
                        //try
                        //{
                        //    //ordId = Convert.ToInt64(Request.QueryString["ordid"]);
                        //}
                        //catch (Exception ex)
                        //{
                        //    ex.GetBaseException();
                        //    Response.Redirect("/");
                        //}
                    }
                    else
                    {
                        bindOrderDetails(Email);
                        lblOrderCancel.Text = "The Cancel order option will only be active before the order is dispatched.";
                        tblreason.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("~/login.aspx");
                }

            }
        }
        private void bindOrderDetails(string Email)
        {
            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();

            ds = orderHandler.get_order_return(Email, 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //string cancel = dt.Rows[0]["order_status"].ToString();
                    //if (cancel != "Cancelled")
                    //{
                    //    tblreason.Visible = true;
                    //    rptOrderCancel.DataSource = dt;
                    //    rptOrderCancel.DataBind();
                    //}
                    //else
                    //{
                    //    lblOrderCancel.ForeColor = System.Drawing.Color.Red;
                    //    lblOrderCancel.Text = "This product is already Cancelled.";
                    //    tblreason.Visible = false;
                    rptOrderCancel.DataSource = dt;
                    rptOrderCancel.DataBind();
                    //}
                }
                else
                {
                    lblOrderCancel.Text = "The Cancel order option will only be active before the order is dispatched.";
                    tblreason.Visible = false;
                }
            }
        }

        protected void btnConfirmCancellation_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["ordid"] != null)
            {
                order_handler orderHandler = new order_handler();
                order Order = new order();
                ordId = Convert.ToInt64(Request.QueryString["ordid"]);

                DataSet ds = new DataSet();
                ds = orderHandler.get_order_search(ordId, null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    
                        ViewState["manifest_link"] = ds.Tables[0].Rows[0]["manifest_link"].ToString();
                        ViewState["order_status"] = ds.Tables[0].Rows[0]["order_status"].ToString();
                        ViewState["ship_id"] = ds.Tables[0].Rows[0]["ship_id"].ToString();

                        string pickkrmanifest = ViewState["manifest_link"].ToString();
                        string pickkrorderstatus = ViewState["order_status"].ToString();
                        string ship_id = ViewState["ship_id"].ToString();
                        PickerApiService pikrapi = new PickerApiService();

                        if (!string.IsNullOrEmpty(pickkrmanifest))
                        {
                            if (pickkrorderstatus != "Dispatched")
                            {
                                bool cancelpikkr = pikrapi.cancelorder_api(ship_id);
                            }
                        }
                   
                }

                Order.order_id = ordId;
                Order.reason_for_cancellation = ddlreasoncancel.SelectedItem.Text;
                Order.Comments = txtComments.Text;
                Order.order_status = "Cancelled";

                bool result = orderHandler.update_cancel_product(Order);

                bindCancelMail(ordId);
                Utility.SendSmsOrderCancel(ViewState["contact_number"].ToString(), ViewState["product_name"].ToString(), ViewState["order_id"].ToString());

                Response.Redirect("cancelorder.aspx?success=1");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("orderhistory.aspx", false);
        }

        private void bindCancelMail(long OrderId)
        {
            //int OrderId;
            string paymentType = "", customername = "", contactnumber = "", address = "", productname = "";
            string imgThumb, ProductUrl;
            string CustEmail = Session["CustomerLoginDetails"].ToString();

            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();

            //DataTable dtOrderDet = orderHandler.get_order_status_track(orderDetailId, 0).Tables[0];
            //OrderId = Convert.ToInt32(dtOrderDet.Rows[0]["order_id"]);

            ds = orderHandler.get_order_search(OrderId, null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                double TotalPrice = Convert.ToDouble(dt.Rows[0]["total_price"].ToString());

                // string TrackUrl = BLL.Helpers.GetUrltraking(dt.Rows[0]["ship_via"], dt.Rows[0]["ship_id"]);
                paymentType = dt.Rows[0]["payment_through"].ToString();
                customername = dt.Rows[0]["customer_name"].ToString();
                contactnumber = dt.Rows[0]["contact_number"].ToString();
                address = dt.Rows[0]["address"].ToString() + " " + dt.Rows[0]["city"].ToString() + " " + dt.Rows[0]["state"].ToString() + " " + dt.Rows[0]["pin_code"].ToString();
                productname = dt.Rows[0]["product_name"].ToString();


                // Below three viewstate used for sending SMS.
                ViewState["product_name"] = productname;
                ViewState["contact_number"] = contactnumber;
                ViewState["order_id"] = OrderId;

                msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                   <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + dt.Rows[0]["customer_name"].ToString() + @", </p>
												  <p style='color:#777;line-height:150%;font-size:16px;margin:0;text-align: justify'>Your Order No: <b>" + ordPrifix + OrderId.ToString() + @" </b> has been processed for a return initiation.Please allow us 24 hours to process the same. Our delivery partner will get in touch with you for the reverse pickup. Please do keep the product and the packaging handy.
                                                    </p>
                                                  <p style='color:#777;line-height:150%;font-size:16px;margin:0;text-align: justify;margin-top: 15px'>Once we receive the original product, we will need your Bank details/ Wallet details to make an online refund transfer. You can reply on this mail or a new email on connect@thestruttstore.com with the order ID and the banking details.The complete process will take 7 to 10 days to complete
                                                    </p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0;text-align: justify;margin-top: 15px'>Sad to see you go but hope you will come back and shop with us again.
                                                    </p>
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double ItemAmount = Convert.ToDouble(dt.Rows[i]["quantity"]) * Convert.ToDouble(dt.Rows[i]["sale_price"]);
                    imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dt.Rows[i]["thumb_image"].ToString().Replace(" ", "%20");
                    ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dt.Rows[i]["product_id"].ToString();


                    msgbody += @"<tr style ='width:100%;border-top-width:1px;border-top-color:#e5e5e5;border-top-style:solid'>
                                                                 <td style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-top:15px'>
                                                                 <table style='border-spacing:0;border-collapse:collapse'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                                    <img src='" + imgThumb + @"' align='left' width='60' height='60' style='margin-right:15px;border-radius:8px;border:1px solid #e5e5e5' class='CToWUd' />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;width:100%'>
                                                                                    <span style='font-size:16px;font-weight:600;line-height:1.4;color:#555'> " + dt.Rows[i]["product_name"].ToString() + @" * " + dt.Rows[i]["quantity"].ToString() + @"</span><br />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;white-space:nowrap'>
                                                                                    <p style ='color:#777;line-height:150%;font-size:16px;margin:0'>
                                                                        <span style ='font-size:16px'> Rate <strike style ='font-size:16px;color:#555'> Rs. " + (dt.Rows[i]["Price"].ToString() + @"</strike></span>
                                                                    </p>
                                                                                    <p style ='color:#555;line-height:150%;font-size:16px;font-weight:600;margin:0 0 0 15px' align='right'>
                                                                                   Rs." + (Convert.ToInt32(dt.Rows[i]["quantity"]) * Convert.ToSingle(dt.Rows[i]["sale_price"])).ToString() + @"
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

                DAL.Utility.sendEmail("STRUTT - Order No " + ordPrifix + OrderId + " Cancelled", msgbody,
                    "Your Return has been initiated!", "YOUR ORDER: " + ordPrifix + OrderId + " has been accepted for return and refund", CustEmail, ConfigurationManager.AppSettings["emailCc"]);
            }
        }

    }
}
