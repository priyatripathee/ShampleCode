using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mail;
using System.Text;
using System.Configuration;

namespace strutt
{
    public partial class contact_us : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            try
            {
                //MailMessage mail = new MailMessage();
                //mail.BodyFormat = MailFormat.Html;
                //mail.Priority = MailPriority.High;
                //mail.BodyEncoding = Encoding.UTF8;
                //mail.To = txtEmail.Text;
                ////mail.Bcc = "dhananjay@shoptosurprise.com, shruti@shoptosurprise.com";
                //mail.Bcc = System.Configuration.ConfigurationManager.AppSettings["emailBcc"];
                //mail.From = txtEmail.Text;
                //mail.Subject = "Contact details";
                string msgbody1;
                msgbody1 = "<tr><td>";
                msgbody1 += "<table border='0' cellpadding='0' cellspacing='0' style='width: 86%;'>";
                msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'>";
                msgbody1 += "Name</td>";
                msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtname.Text + "</td></tr>";

                msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'> Email Id</td>";
                msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtEmail.Text + "</td></tr>";

                msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'> Mobile</td>";
                msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtMobile.Text + "</td></tr>";

                msgbody1 += "<tr><td style='width: 40%; padding-left: 10px; font-weight: bold; font-size: 11px; color: black; background-color: #C0C0C0; text-align: left; border-right: #626262 1px solid; border-top: #626262 1px solid; border-left: #626262 1px solid; border-bottom: #626262 1px solid;'> Message</td>";
                msgbody1 += "<td style='width: 100%; border-right: #626262 1px solid; border-top: #626262 1px solid; padding-left: 10px; border-left: #626262 1px solid; border-bottom: #626262 1px solid; height: 25px; text-align: left; font-family:Verdana;font-size:8pt;color:#5B5B5B;text-align:left;line-height:18px;'>" + txtQuery.Text + "</td></tr>";

                msgbody1 += "</table></td></tr>";
                msgbody1 += "<tr><td style='width: 30%'></td><td style='width: 25%'></td><td style='width: 33%'></td></tr></table></td></tr>";
                msgbody1 += "</table>";
                msgbody1 += "</td></tr>";
                //mail.Body = msgbody1;


                //string smtpEmail = "happiness@shoptosurprise.com";
                //string smtpPassword = "visheshkhosla1";
                //string smtpEmail = "happiness@shoptosurprise.com";
                //string smtpPassword = "vishu0894";
                //string smtpAddress = "smtp.gmail.com";
                //string smtpPort = "465";
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpAddress);
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", smtpPort);
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", smtpEmail);
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", smtpPassword);
                //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "True");
                //SmtpMail.Send(mail);

                DAL.Utility.sendEmail("STRUTT - Contact details", msgbody1, "", "", ConfigurationManager.AppSettings["contactEmail"], null);

                lblMsg.Text = "Your contact has been submited successfully.";
                txtname.Text = "";
                txtEmail.Text = "";
                txtMobile.Text = "";
                txtQuery.Text = "";
            }
            catch (Exception ex)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Query failed. Please send again.";
            }
        }

        protected void btnClear_Click1(object sender, EventArgs e)
        {

            txtname.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtQuery.Text = "";
        }
    }
}