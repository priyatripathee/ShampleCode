using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace strutt
{
    public partial class testEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_send_Click(object sender, EventArgs e)
        {
            //System.Net.Configuration.SmtpSection smtpSection = (System.Net.Configuration.SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage mm = new MailMessage("noreply@tenagents.com", txt_mail.Text.Trim()))
            {
                mm.Subject = "New Mail";
                mm.Body = txt_msg.Text;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.settlespain.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential networkCred = new System.Net.NetworkCredential("noreply@tenagents.com","7jrm5WsJTcAf49h");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCred;
                smtp.Port = 587;

                smtp.Send(mm);
            }
        }
    }
}