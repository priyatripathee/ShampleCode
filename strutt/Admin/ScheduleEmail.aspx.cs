using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace strutt.Admin
{
    public partial class ScheduleEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["AdminUserID"] == null)
                //    Response.Redirect("../account/Login.aspx");

             //   this.JobRunNotifyEmail();
              // this.SendAbandonedcartemail();
                //if (Session["Role"].ToString() == "Admin")
                //{
                //    Response.Redirect("Dashboard.aspx");
                //}
                //TestEmail();
            }
        }
        //public bool TestEmail()
        //{
        //    string msgbody = @"<tr> <td> Testing Email 30-March-2021</td></tr>
        //        ";
        //    DAL.Utility.sendEmail("STRUTT - Schedule Email", msgbody, null, null,"patelchandni026@gmail.com", null);
        //    return true;
        //}
        #region Abandoned cart and failed payment email 


        public bool SendAbandonedcartemail()
        {
            try
            {
                string msgbody = "";
                string emailsubject = "";
                string imgThumb, ProductUrl;
                long Orderid, Emailcount;
                string ContactNumber;
                string Emailid;

                order_handler orderHandler = new order_handler();
                DataSet ds = new DataSet();

                ds = orderHandler.get_abandonedcart();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow rowTempOrder in ds.Tables[0].Rows)
                    {
                        Orderid = Convert.ToInt64(rowTempOrder["order_id"]);
                        ContactNumber = rowTempOrder["contact_number"].ToString();
                        Emailcount = Convert.ToInt64(rowTempOrder["Sendemail_count"]);
                        Emailid = rowTempOrder["email_id"].ToString();

                        //DataSet dsorder = new DataSet();
                        //dsorder = orderHandler.get_order_search(Orderid, null);
                        product_handler productHandler = new product_handler();
                        DataSet dsProduct = new DataSet();

                        dsProduct = productHandler.get_product_details(Convert.ToInt64(rowTempOrder["product_id"]));

                    if (dsProduct != null && dsProduct.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtProduct = dsProduct.Tables[0];

                            if (Emailcount == 4 || Emailcount == 5)
                            {
                                msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                      <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + Session["CutomerName"] + @"</p></br>
                                                   <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Don't worry, we got your back.
Your current payment method could not be processed and the amount was not deducted. You can try again or try a different payment method. 
We reserved your items for you for the next few minutes and look forward to confirming your order and sending them off to you soon.
We look forward to you becoming a part of the STRUTT family. 
                                                   
                                                    </p>
												  
                                                    <table style='width:100%;border-spacing:0;border-collapse:collapse;margin-top:20px'>
                                                        <tbody>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                    <table style='border-spacing:0;border-collapse:collapse;margin-top:19px'>
                                                                        <tbody>
                                                                            <tr>
 <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;border-radius:4px' align='center' bgcolor='#000000'><a href='https://thestruttstore.com/' style='font-size:16px;text-decoration:none;display:block;color:#fff;padding:20px 25px' target='_blank' data=data -=- saferedirecturl='https://thestruttstore.com/'> Visit our store </a></td>
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
                            }
                            if (Emailcount == 6 || Emailcount == 7 || Emailcount == 8)
                            {
                                msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                       <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + Session["CutomerName"] + @"</p></br>
                                                   <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Don't worry, we got your back.
Your current payment method could not be processed and the amount was not deducted. You can try again or try a different payment method. 
We reserved your items for you for the next few minutes and look forward to confirming your order and sending them off to you soon.
We look forward to you becoming a part of the STRUTT family. 
                                                   
                                                    </p>
												  
                                                    <table style='width:100%;border-spacing:0;border-collapse:collapse;margin-top:20px'>
                                                        <tbody>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                    <table style='border-spacing:0;border-collapse:collapse;margin-top:19px'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;border-radius:4px' align='center' bgcolor='#000000'><a href='https://thestruttstore.com/' style='font-size:16px;text-decoration:none;display:block;color:#fff;padding:20px 25px' target='_blank' data=data -=- saferedirecturl='https://thestruttstore.com/'> Visit our store </a></td>
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
                               
                            }
                            for (int i = 0; i < dtProduct.Rows.Count; i++)
                            {
                                //ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dtProduct.Rows[i]["product_id"].ToString();
                                double ItemAmount = Convert.ToDouble(dtProduct.Rows[i]["quantity"]) * Convert.ToDouble(dtProduct.Rows[i]["sale_price"]);
                               // double ItemAmount =Convert.ToDouble(dtProduct.Rows[i]["sale_price"]);
                                imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dtProduct.Rows[i]["thumb_image"].ToString().Replace(" ", "%20");
                                ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dtProduct.Rows[i]["product_id"].ToString();
                                msgbody += @"<tr style ='width:100%;border-top-width:1px;border-top-color:#e5e5e5;border-top-style:solid'>
                                                                 <td style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-top:15px'>
                                                                 <table style='border-spacing:0;border-collapse:collapse'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                                    <img src='" + imgThumb + @"' align='left' width='60' height='60' style='margin-right:15px;border-radius:8px;border:1px solid #e5e5e5' class='CToWUd' />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;width:100%'>
                                                                                    <span style='font-size:16px;font-weight:600;line-height:1.4;color:#555'><a target='_blank' style='color:#FB641B' href='" + ProductUrl + "'>" + dtProduct.Rows[i]["product_name"].ToString() + @"</a></span><br />
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
                            
                            if (Emailcount == 4 || Emailcount == 5)
                            {
                                emailsubject = "Finch, your cart is wondering where you went!";
                                bool emailsent = DAL.Utility.sendEmail(emailsubject, msgbody, null, null, Emailid, null);
                                if (emailsent)
                                {
                                    orderHandler.update_temp_customer_email_count(Orderid, Emailcount);
                                }
                            }
                            if (Emailcount == 6 || Emailcount == 7 || Emailcount == 8)
                            {
                                emailsubject = "Your Payment attempt did not go through";
                                bool emailsent = DAL.Utility.sendEmail(emailsubject, msgbody, null, null, Emailid, null);
                                if (emailsent)
                                {
                                    orderHandler.update_temp_customer_email_count(Orderid, Emailcount);

                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        #endregion


        public bool JobRunNotifyEmail()
        {
            try
            {
                LogRunScheduler();
                order_handler orderHandler = new order_handler();
                DataSet ds = new DataSet();
                int cartcount = 0;
                int paymentfailcount = 0;
                DateTime CurrentDate;
                CurrentDate = DateTime.Now;
                ds = orderHandler.get_abandonedcart();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    var cartrow = from row in dt.AsEnumerable()
                                  where row.Field<string>("customer_medium") == "Cart Abandoned"
                                  select row;

                    var paymentfailrow = from row in dt.AsEnumerable()
                                         where row.Field<string>("customer_medium") == "Failed Payment"
                                         select row;

                    cartcount = cartrow.Count<DataRow>();

                    paymentfailcount = paymentfailrow.Count<DataRow>();

                }
                string msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi, </p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>The Strutt- <b>Scheduler Run successfully.</b></p></br></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Scheduler Run Time: <b>" + CurrentDate + @"<b></p></br></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Cart Abandoned Customer Count: <b>" + cartcount + @"<b></p></br></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Failed Payment Customer Count: <b>" + paymentfailcount + @"<b></p></br>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";
                bool emailsent = DAL.Utility.sendEmail("Email Send Scheduler Run successfully!", msgbody, null, null, null , null);
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }


        #region Log file
        public static void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = System.Web.HttpContext.Current.Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        public static void LogRunScheduler()
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += string.Format("Scheduler Run Time: {0}", message);
            message += Environment.NewLine;
            string path = System.Web.HttpContext.Current.Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
        #endregion
    }
}