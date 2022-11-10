using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using BusinessEntities;
using System.IO;
using CCA.Util;
using System.Configuration;

namespace strutt.Admin
{
    public partial class temporder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindTempOrder();
                txttodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtfromdate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
            
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindTempOrder();



            // DAL.Utility.SendPendingOrder();
        }
        private void BindTempOrder()
        {
            DateTime? Fromdate = null;
            DateTime? Todate = null;
            if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
            {
                Fromdate = Convert.ToDateTime(txtfromdate.Text);
                Todate = Convert.ToDateTime(txttodate.Text + " 23:59:59");
            }
            order_handler temporderHandler = new order_handler();
            DataSet ds = temporderHandler.get_temp_order(null, Fromdate, Todate);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    grdTempOrder.DataSource = dt;
                    grdTempOrder.DataBind();
                }
                else
                {
                    grdTempOrder.DataSource = null;
                    grdTempOrder.DataBind();
                }
            }
        }

        protected void grdPartialOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SendEmail")
            {
                int ordId = Convert.ToInt32(e.CommandArgument);
                SendPartialOrder(ordId);
            }
        }
        public bool SendPartialOrder(int ordId)
        {
            string msgbody = "";

            string imgThumb, ProductUrl;

            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();

            ds = orderHandler.get_temp_partialorder(ordId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dt.Rows[0]["product_id"].ToString();
                ViewState["order_id"] = dt.Rows[0]["order_id"];
                ViewState["ContactNumber"] = dt.Rows[0]["contact_number"].ToString();

                msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                   <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + dt.Rows[0]["name"].ToString() + @"
                                                    </p>
												  <p style='color:#777;line-height:150%;font-size:16px;margin:0'>You left an item in your cart.
                                                    </p></br>
                                                  <p style='color:#777;line-height:150%;font-size:16px;margin:0'>you added an item to your shopping cart and haven't completed your purchase. You can complete it now while it's still available.
                                                    </p>
                                                    <table style='width:100%;border-spacing:0;border-collapse:collapse;margin-top:20px'>
                                                        <tbody>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;line-height:0em'> &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                    <table style='border-spacing:0;border-collapse:collapse;margin-top:19px'>
                                                                        <tbody>
                                                                            <tr>
                                                                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>Items in your cart OR <a target='_blank' style='color:#0089ff8c' href='" + ProductUrl + "'>" + @" Visit our store</a></td>
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
                                                    <h3 style='font-weight:normal;font-size:20px;margin:0 0 25px'> Complete your purchase </h3>
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
                                                                                    <span style='font-size:16px;font-weight:600;line-height:1.4;color:#555'> <a target='_blank' style='color:#FB641B' href='" + ProductUrl + "'>" + dt.Rows[i]["product_name"].ToString() + @"</a></span><br />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;white-space:nowrap'>
                                                                                    
                                                                                    <p style ='color:#555;line-height:150%;font-size:16px;font-weight:600;margin:0 0 0 15px' align='right'>
                                                                                  Rs. " + ItemAmount.ToString("0.00") + @"
                                                                                    </p>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>";
                }
                msgbody += @"</tbody>
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
                DAL.Utility.sendEmail("Your cart is feeling abandoned!. ", msgbody, null, null, (dt.Rows[0]["email_id"].ToString()), null);
            }
            return true;
        }
    }
}