using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BusinessEntities;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mail;
using System.Text;

namespace strutt.account
{
    public partial class leavecomplaint : System.Web.UI.Page
    {
        string msgbody = "";
        string Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CustomerLoginDetails"] != null)
                {
                    Email = Session["CustomerLoginDetails"].ToString();
                }
                else
                {
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["siteUrl"]);
                    Response.Redirect("~/login.aspx");
                }
                //if (Session["CustomerLoginDetails"] != null)
                //{
                //    Email = Session["CustomerLoginDetails"].ToString();
                //}
                //else
                //{
                //    Response.Redirect("~/login.aspx");
                //}
            }
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            order Order = new order();
            Order.raise_id = 0;
            Order.login_id = Session["CustomerLoginDetails"].ToString();
            Order.user_name = txtName.Text;
            Order.contact_number = txtMobileNo.Text;
            Order.email_id = txtEmail.Text;
            Order.complain_type = txtComplainType.Text;
            Order.details = txtQuery.Text;

            order_handler orderHandler = new order_handler();
            bool result = orderHandler.insert_raise_complain(Order);
            if (result)
            {
                //try
                //{
                //    MailMessage mail = new MailMessage();
                //    mail.BodyFormat = MailFormat.Html;
                //    mail.Priority = MailPriority.High;
                //    mail.BodyEncoding = Encoding.UTF8;
                //    mail.To = txtEmail.Text;
                //    //mail.Cc = "dhananjay@shoptosurprise.com";
                //    mail.Bcc = "dhananjay@shoptosurprise.com";
                //    mail.From = txtEmail.Text;
                //    mail.Subject = "Your complaint details";
                //    string msgbody1 = "<table border='0' cellpadding='0' cellspacing='0' style='width: 86%;'>";
                //    msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'>";
                //    msgbody1 += "User Name</td>";
                //    msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtName.Text + "</td></tr>";

                //    msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'>Login Email Id</td>";
                //    msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + Session["CustomerLoginDetails"].ToString() + "</td></tr>";

                //    msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'>Mobile No</td>";
                //    msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtMobileNo.Text + "</td></tr>";

                //    msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'> Email Id</td>";
                //    msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtEmail.Text + "</td></tr>";

                //    msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'> Order Id</td>";
                //    msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtComplainType.Text + "</td></tr>";

                //    msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'> Query</td>";
                //    msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtQuery.Text + "</td></tr>";

                //    msgbody1 += "</table></td></tr>";
                //    msgbody1 += "<tr><td style='width: 30%'></td><td style='width: 25%'></td><td style='width: 33%'></td></tr></table></td></tr>";
                //    msgbody1 += "</table>";
                //    mail.Body = msgbody1;


                //    string smtpEmail = "happiness@shoptosurprise.com";
                //    string smtpPassword = "vishu0894";
                //    string smtpAddress = "smtp.gmail.com";
                //    string smtpPort = "465";
                //    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
                //    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpAddress);
                //    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", smtpPort);
                //    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                //    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", smtpEmail);
                //    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", smtpPassword);
                //    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "True");
                //    SmtpMail.Send(mail);
                //}
                //catch (Exception ex)
                //{
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //    lblMsg.Text = "query failed. Please send again";
                //}

               
                txtName.Text = "";
                txtEmail.Text = "";
                txtMobileNo.Text = "";
                txtComplainType.Text = "";
                txtQuery.Text = "";
                
            }
            lblMsg.Text = "Your raise complaint has been submited successfully.";
        }
    }
}