using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BusinessEntities;
using System.Web.Mail;
using System.Net;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mime;

namespace DAL
{
    public static class Utility
    {
        public static bool change_update_status(int id, bool status, string col, string key, string tblName)
        {
            bool retVal = false;
            SqlConnection con = new SqlConnection(Connection.ConnstruttDB);
            SqlCommand cmd = new SqlCommand("pr_update_status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@ColumnName", col);
                cmd.Parameters.AddWithValue("@KeyID", key);
                cmd.Parameters.AddWithValue("@TableName", tblName);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string generatecode()
        {
            string numbers = "1234567890";
            string characters = numbers;
            characters += numbers;
            int length = 10;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            string M = otp;
            return M;
        }


        public static string generatecouponcode()
        {
            string numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string characters = numbers;
            characters += numbers;
            int length = 4;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            string M = otp;
            return M;
        }

        public static string SendSms(string Number, String Message)
        {
            //string Number = "918123456789";
            if (string.IsNullOrEmpty(Number))
                return "Mobile number is blank.";

            Message = Uri.EscapeDataString(Message);

            string apikey = "gAaLwF1nlK4-uR43Qgwn0B0oj1lJprMp8cFYUe9UdV";
            string SenderName = "STRUTT";
            string URL = "https://api.textlocal.in/send/?";
            string PostData = "apikey=" + apikey + "&sender=" + SenderName + "&numbers=" + Number + "&message=" + Message;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            req.Method = "POST";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = byte1.Length;
            Stream newStream = req.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();
                return results;
            }
            catch (WebException wex)
            {
                //Response.Write("SOMETHING WENT AWRY!\nStatus: " + wex.Status + "Message: " + wex.Message + "");
                return wex.Status.ToString(); ;
            }
        }
        // private static bool SendMailSmpt(string ToAdd, string Subject, string MsgBody, string CCAdd, int? OrderId)

      

        //public static bool SendMailSmpt(string ToAdd, string subject, string emailMsg, string CCAdd, int? OrderId)
        //{
        //    try
        //    {
        //        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
        //        SmtpClient sc = new SmtpClient();
        //        m.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"].ToString(), "Chandni");
        //        m.To.Add(new MailAddress(ToAdd, ""));
        //        if (CCAdd != null && CCAdd.Trim() != "")
        //            m.CC.Add(new MailAddress(CCAdd));
        //        if (ConfigurationManager.AppSettings["BCCEmail"] != "")
        //            m.CC.Add(new MailAddress(ConfigurationManager.AppSettings["BCCEmail"]));
        //        m.Subject = subject;
        //        m.IsBodyHtml = true;
        //        m.Body = emailMsg;
        //        AlternateView av = AlternateView.CreateAlternateViewFromString(emailMsg, null, MediaTypeNames.Text.Html);
        //        m.AlternateViews.Add(av);
        //        sc.Host = "smtp.gmail.com";
        //        sc.Port = 587;
        //        sc.Credentials = new
        //          System.Net.NetworkCredential("kbp384@gmail.com", "kbp@12384");
        //        sc.EnableSsl = true;
        //        sc.Send(m);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
               
        //        return false;
        //    }
        //}
        public static bool sendEmail(string subject, string emailMsg, string Head1, string Head2, string ToEmailAddress, string CcEmailAddress, string BccEmail = null)
        {
            string FromEmailAddress = "connect@thestruttstore.com";

            //string FromEmailAddress = "happiness@shoptosurprise.com";
            try
            {
                MailMessage mail = new MailMessage();
                mail.BodyFormat = MailFormat.Html;
                mail.Priority = MailPriority.High;
                mail.BodyEncoding = Encoding.UTF8;
                mail.To = ToEmailAddress;
                if (!string.IsNullOrEmpty(CcEmailAddress))
                    mail.Cc = CcEmailAddress;
                mail.Bcc = System.Configuration.ConfigurationManager.AppSettings["emailBcc"];// "connect@thestruttstore.com";

                if (BccEmail != null)
                    mail.Bcc += ", " + BccEmail;

                mail.From = FromEmailAddress;
                mail.Subject = subject;

                if (subject.StartsWith("STRUTT - Signup Successful"))
                    //mail.Body = EmailHeaderSignup() + emailMsg + EmailFooter();
                    mail.Body = EmailHeaderSignup() + emailMsg;
                else if (subject.StartsWith("Error"))
                    mail.Body = emailMsg;
                else
                   // mail.Body = EmailHeader(Head1, Head2) + emailMsg + EmailFooter();
                    mail.Body = EmailHeader(Head1, Head2) + emailMsg;

                string smtpEmail = FromEmailAddress;
                //string smtpPassword = "vishu0894";
                //string smtpPassword = "Ironman0894@c272";
                string smtpPassword = "hit19@0894ST";
                string smtpaddress = "smtp.gmail.com";
                string smtpPort = "465";

                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpaddress);
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", smtpPort);
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", smtpEmail);
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", smtpPassword);
                mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "True");

                SmtpMail.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
            return true;
        }

        //public static string EmailHeader(string Head1, string Head2)
        //{
        //    return @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
        //        <html xmlns='http://www.w3.org/1999/xhtml'>
        //        <head>
        //        <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
        //        <title>The Strutt Store</title>
        //        </head>
        //        <body>
        //        <table width='750' border='0' align='center' cellpadding='0' cellspacing='0' style='border:1px solid #eee;'>
        //            <tr><td valign='top' style='padding:0px; margin:0px;'><img src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/header.jpg'/></td></tr>
        //            <tr><td valign='top' style='padding:0px; margin:0px; background-image:url(" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/banner.jpg); height:365px; width:750px;'>
        //                <span style='display: block;font-size: 55px;font-family: arial;color: #fff;margin: 93px 0 0 25px;'>" + Head1 + @"</span>
        //                <span style='display: block;font-size: 18px;font-family: arial;color: #fff;margin-left: 25px; width:380px;'>" + Head2 + @"</span></td></tr>";
        //}
        //public static string EmailFooter()
        //{
        //    return @"<tr><td style='background:lightgrey;padding:10px'><table style='width: 100 %; border - spacing:0; border - collapse:collapse'>
        //      <tr>
        //        <td style='text-align:left;'><img src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/follow-us.jpg'/></td>
        //        <td style='text-align:right;width: 10px;'><a href='https://www.facebook.com/theSTRUTTstore/'><img style='height:33px;' src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/fb.jpg'/></a></td>
        //        <td style='text-align:right;width: 10px;'><a href='https://www.instagram.com/thestrutt/'><img style='height:33px;' src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/insta.jpg'/></a></td>
        //        <td style='text-align:right;width: 10px'><a href='#'><img src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/blog.jpg'/></a></td>
        //      </tr>
        //      <tr>
        //        <td colspan='4'  style='text-align:center; font-size:14px; font-family:Arial, Helvetica, sans-serif;'><br />
        //        <p>Please add connect@thestruttstore.com to your address book. Having trouble viewing this email? View in Web Browser. C 272, 1st Floor, Sector 10, Noida - 201301, Uttar Pradesh, India.
        //        </p><br /><br /></td></tr>
        //      <tr><td colspan='4' style='text-align:center; font-size:14px; font-family:Arial, Helvetica, sans-serif;'><p>All Rights Reserved, Copyright @ 2021, Strutt</p></td></tr>
        //    </table>
        //    </td></tr></table>";
        //}

        public static string EmailHeader(string Head1, string Head2)
        {
            //string siteurl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/assets/images/logo/logo.png";
            return @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                <title>The Strutt Store</title>
                </head>
                <body>
        <div style='margin: 0 auto;text-align: -webkit-center;' id='mydiv1'>
        <table style = 'height:100 %!important; width: 100 % !important; border - spacing:0; border - collapse:collapse;' >
        <tbody>
          <tr>
              <td style = 'font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif' > 
                 <table style = 'width:100%;border-spacing:0;border-collapse:collapse;margin:40px 0 20px'>
                          <tbody>
                              <tr>
                                  <td style = 'font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif' >
                                       <center>
                                           <table style = 'width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto' >
                                                <tbody>
                                                    <tr>
                                                        <td style = 'font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif' >
                                                             <table style = 'width:100%;border-spacing:0;border-collapse:collapse' >
                                                                  <tbody>
                                                                      <tr>
                                                                          <td style = 'font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif' >
                                                                               <img  src = '" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/assets/images/logo/logo.png" + @"'  alt = 'thestruttstore' width = '180' class='CToWUd' />
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
        }
        public static string EmailFooter()
        {

            return @"<div style='margin: 0 auto;text-align: -webkit-center;' id='mydiv2'><table style='border-spacing:0;border-collapse:collapse;background-color: lightgrey'>
                        <tbody>
                            <tr>
                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif ,background-color: lightgrey;'>
                                    <center>
                                        <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                            <tbody>
                                                <tr>
                                                    <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                        <center>
                                                    <table style='border-spacing:0;border-collapse:collapse'>
                                                            <tbody>
                                                                <tr><td><table width='100%' border='0'>
              <tr>
                <td width='43%' style='text-align:right;'><img src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/follow-us.png'/></td>
                <td width='15%'><a href='https://www.facebook.com/theSTRUTTstore/'><img style='height:33px;' src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/fb.png'/></a></td>
                <td width='13%'><a href='https://www.instagram.com/thestrutt/'><img style='height:33px;' src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/insta.png'/></a></td>
                <td width='29%'><a href='#'><img src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/images/blog.png'/></a></td>
              </tr>
              <tr>
                <td td colspan='4' style='text-align:center; font-size:14px; font-family:Arial, Helvetica, sans-serif;'><br />
                <p>Please add connect@thestruttstore.com to your<br /> address book. Having trouble viewing this email? <br />View in Web Browser. Creative Tinsel pvt. Ltd,<br /> Sector 6, Noida, U.P. 201301
                </p><br /></td></tr>
              <tr><td colspan='4' style='text-align:center; font-size:14px; font-family:Arial, Helvetica, sans-serif;'><p>All Rights Reserved, Copyright @ 2018, Strutt</p></td></tr>
            </table>
            </td></tr>
                                                            </tbody>
                                                            </center>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </center>
                                </td>
                            </tr>
                        </tbody>
                    </table></div>";
        }
        public static string EmailHeaderSignup()
        {
            string siteurl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/assets/images/logo/logo.png";
            return @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                <title>The Strutt Store</title>
                </head>
                <body>
        <table style = 'height:100 %!important; width: 100 % !important; border - spacing:0; border - collapse:collapse' >
        <tbody>
          <tr>
              <td style = 'font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif' > 
                 <table style = 'width:100%;border-spacing:0;border-collapse:collapse;margin:40px 0 20px'>
                          <tbody>
                              <tr>
                                  <td style = 'font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif' >
                                       <center>
                                           <table style = 'width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto' >
                                                <tbody>
                                                    <tr>
                                                        <td style = 'font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif' >
                                                             <table style = 'width:100%;border-spacing:0;border-collapse:collapse' >
                                                                  <tbody>
                                                                      <tr>
                                                                          <td style = 'font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif' >
                                                                               <img  src = '" + siteurl + @"'  alt = 'thestruttstore' width = '180' class='CToWUd' />
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
                   
        }

        public static void SendSignupMail(string Username, string SignupEmail, string SignupPasswrod, string ToEmailAddress)
        {
            string msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + Username + @"</p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Welcome to thestruttstore.com.</p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>You'll be the first to get the inside scoop on <span style='color:#7CD5F3;'>New Products,</span> special offers and more.</br>
												    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Your Login Information</p></br>

                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Email : " + SignupEmail + @"</p>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Password : " + SignupPasswrod + @"</p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Here's a little gift for you from our little Genie at thestruttstore.com, Get <span style='color:#7CD5F3;'>20% </span> off: USE CODE : <span style='color:#7CD5F3;'>EARLYBIRD20</span></p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Select the products of your choice & add them to your shopping cart, Enter your unique coupon code during your checkout Process</p>
                                                    <h3 style='color:#777;line-height:150%;font-size:16px;margin:0;color:#7CD5F3;'>Terms & Conditions</h3>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>1. Coupons cant be clubbed with any other offer.</p>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>2. For a single transaction only one coupon can be used.</p>
                                                    <h3 style='color:#777;line-height:150%;font-size:16px;margin:0;color:#7CD5F3;'>Happy Shopping !!</h3>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";
            DAL.Utility.sendEmail("STRUTT - Signup Successful", msgbody, "", "", ToEmailAddress, System.Configuration.ConfigurationManager.AppSettings["emailCc"]);
        }

        public static void SendUpdateWishListItem(string Productname)
        {
            string msgbody = @"<tr><td>
                <table border='0' cellpadding='0' cellspacing='0' style='width: 86%;'>
          <td valign='top' style='padding:40px; margin:0px;'>
            <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>Hi Admin</p>
                <tr><td></td>
               
                <td>" + Productname + " item added in Wishlist, which quantity is zero(0)." + @"</td></tr>
                </table></td></tr>
                <tr><td></td></tr></table></td></tr>
                </table>
                </td></tr>";

            DAL.Utility.sendEmail("Wishlist Item Quantity is Zero ", msgbody, null, null, System.Configuration.ConfigurationManager.AppSettings["emailCc"], null);
        }
        //public static void SendForgotPasswordMail(string Username, string Password, string ToEmailAddress)
        //{


        //    string msgbody = @"<tr><td>
        //        <table border='0' cellpadding='0' cellspacing='0' style='width: 86%;'>
        //        <tr><td></td>
        //        <td>" + "Username: " + Username + "<br /><br />Your password is: <b>" + Password + "</b><br /><br /> "
        //              + @"<br /><br />ThankYou !</td></tr>
        //        </table></td></tr>
        //        <tr><td></td></tr></table></td></tr>
        //        </table>
        //        </td></tr>";

        //    DAL.Utility.sendEmail("STRUTT - Password Recovery", msgbody, "Password recovery", "Your password has been recovered", ToEmailAddress, null);
        //}
        public static void SendForgotPasswordMail(string Username, string ActiveLink, string ToEmailAddress)
        {
            string msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + Username + @"</p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>You recently requested to reset your password for your account. Click the link below to reset it.</p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Your password Link : <b><a href=" + ActiveLink + @">Reset Password </a></b></p></br>
												    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>if you did not initiate this request, please contact us immediately at connect@thestruttstore.com .</p></br>
                                                    <h3 style='color:#777;line-height:150%;font-size:16px;margin:0;color:#7CD5F3;'>Thank you!!</h3>
                                                    <h3 style='color:#777;line-height:150%;font-size:16px;margin:0'><b>The Strutt Team.</b></h3>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";
            DAL.Utility.sendEmail("STRUTT - Reset Password", msgbody, null, null, ToEmailAddress, null);
        }
        public static void SendPendingOrder(string ToEmailAddress)
        {
            //string imgThumb, ProductUrl;
            string msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi Admin</p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Did you face any issue(s) while trying to place an order? Don't worry we have saved the details you've filled in - you're just a couple of clicks away! Click on the button below to review and confirm the order.</p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>" + "Your pending order not found." + @"</p></br>
												    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>" + "Did you face any issue(s) while trying to place an order? Don't worry we have saved the details you've filled in - you're just a couple of clicks away! Click on the button below to review and confirm the order." + @"</p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";
            DAL.Utility.sendEmail("Your pending order not found. ", msgbody, null, null, ToEmailAddress, null);
        }

        public static void SendPartialOrder(string ToEmailAddress)
        {
            //string imgThumb, ProductUrl;
            string msgbody = @"<tr><td>
                <table border='0' cellpadding='0' cellspacing='0' style='width: 86%;'>
          <td valign='top' style='padding:40px; margin:0px;'>
            <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>Hi Admin</p>
            <p style='font-family:Arial, Helvetica, sans-serif; font-size:16px;'>Did you face any issue(s) while trying to place an order? Don't worry we have saved the details you've filled in - you're just a couple of clicks away! Click on the button below to review and confirm the order.</p>
                <tr><td></td>
                <td>" + "Your cart is feeling abandoned!" + @"</td></tr>
                </table></td></tr>
                <tr><td></td></tr></table></td></tr>
                </table>
                </td></tr>";
            DAL.Utility.sendEmail("Your cart is feeling abandoned!. ", msgbody, null, null, ToEmailAddress, null);


        }
        public static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
        public static bool EmailError(Exception ex, System.Web.HttpRequest Request, string UserName)
        {
            //string FromEmailAddress = "connect@thestruttstore.com";
            //System.Net.Mail.MailMessage loMail = new System.Net.Mail.MailMessage();
            //System.Net.Mail.SmtpClient loSmtp = new System.Net.Mail.SmtpClient();
            try
            {
                StringBuilder loSb = new StringBuilder();
                //loSb.Append(EmailHeader());
                string subject = "Error -" + DateTime.Now.ToString("dd - MMM - yyyy hh: mm: ss");
                string msgbody = @"<p>Error raise from TheStrutt.</p>
                        <b>Date Time: </b>" + DateTime.Now.ToString() + "<br />" +
                        "<b>Location: </b>" + Request.Url.ToString() + "<br />" +
                        "<b>Message: </b>" + ex.Message + "<br />" +
                        "<b>Trace: </b>" + ex.StackTrace.ToString() + "<br />" +
                        "<b> Inner Messsag: </b>" + ex.InnerException.Message.ToString() + " <br/>" +
                        "<b>Inner Trace: </b>" + ex.InnerException.StackTrace.ToString() + "<br />" +
                        "<b> Username: </b>" + UserName + "<br />" +
                        "<b> IP: </b> " + Request.UserHostAddress.ToString() + "<br />" +
                        "<b>DNS: </b>" + Request.UserHostName.ToString() + "<br />" +
                        "<b>Browser: </b>" + Request.Browser.Type.ToString() + "<br />" +
                        "<b>Agent: </b>" + Request.UserAgent.ToString() + "<br />";
                //loSb.Append(@"<p>Error raise from Tenant Verify.</p>");
                //loSb.Append(@"<b>Date Time: </b>" + DateTime.Now.ToString() + "<br />");
                //loSb.Append(@"<b>Location: </b>" + Request.Url.ToString() + "<br />");
                //loSb.Append(@"<b>Message: </b>" + ex.Message + "<br />");
                //loSb.Append(@"<b>Trace: </b>" + ex.StackTrace.ToString() + "<br />");
                //if (ex.InnerException != null)
                //{
                //    loSb.Append(@"<b>Inner Messsag: </b>" + ex.InnerException.Message.ToString() + "<br />");
                //    loSb.Append(@"<b>Inner Trace: </b>" + ex.InnerException.StackTrace.ToString() + "<br />");
                //}
                //loSb.Append(@"<b>Username: </b>" + UserName + "<br />");
                //loSb.Append(@"<b>IP: </b>" + Request.UserHostAddress.ToString() + "<br />");
                //loSb.Append(@"<b>DNS: </b>" + Request.UserHostName.ToString() + "<br />");
                //loSb.Append(@"<b>Browser: </b>" + Request.Browser.Type.ToString() + "<br />");
                //loSb.Append(@"<b>Browser Version: </b>" + Request.Browser.Version.ToString() + "<br />");
                //loSb.Append(@"<b>Agent: </b>" + Request.UserAgent.ToString() + "<br />");
                ////loSb.Append(EmailSignature());
                //loMail.From = new System.Net.Mail.MailAddress(FromEmailAddress, "Strutt Verify");
                //loMail.To.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                //loMail.Subject = "Error - Strutt Verify – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                //loMail.IsBodyHtml = true;
                //System.Net.Mail.AlternateView av = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Convert.ToString(loSb), null, MediaTypeNames.Text.Html);
                //loMail.AlternateViews.Add(av);
                //loSmtp.Send(loMail);
                bool emailsent = DAL.Utility.sendEmail(subject, msgbody, null, null, (ConfigurationManager.AppSettings["BCCEmail"]), null);
                return true;
            }
            catch (Exception err)
            {
                LogError(err);
                return false;
            }
        }

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
    }
}
