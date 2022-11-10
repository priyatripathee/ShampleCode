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

namespace strutt.Admin
{
    public partial class newsletter : System.Web.UI.Page
    {
        private Int64 NewsLetterID
        {
            get
            {
                if (ViewState["NewsLetterID"] != null)
                {
                    return Convert.ToInt32(ViewState["NewsLetterID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value == 0)
                {
                    ViewState["NewsLetterID"] = null;
                }
                else
                {
                    ViewState["NewsLetterID"] = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindNewsletter();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
        private void BindNewsletter()
        {
            newsletter_handler newsletterHandler = new newsletter_handler();
            DataSet ds = newsletterHandler.get_newsletter(0, "", 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                grdNewsLetter.DataSource = dt;
                grdNewsLetter.DataBind();
            }
            else
            {
                grdNewsLetter.DataSource = null;
                grdNewsLetter.DataBind();
            }
        }

        public string generatecode()
        {
            //string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
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

            string M = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/" + "newsletter" + "/" + txtEmail.Text + "?" + otp;
            return M;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String url = generatecode();

            bool result = false;
            BusinessEntities.newsletter NewsLetter = new BusinessEntities.newsletter();
            NewsLetter.news_letter_id = NewsLetterID == 0 ? 0 : NewsLetterID;
            NewsLetter.email = txtEmail.Text;
            NewsLetter.url = url.ToString();


            newsletter_handler newsletterHandler = new newsletter_handler();
            result = newsletterHandler.insert_update_newsletter(NewsLetter);
            if (result)
            {
                if (NewsLetterID == 0)
                {
                    lblMsg.Text = "saved successfully!";
                }
                else
                {
                    lblMsg.Text = "updated successfully!";
                    ViewState["NewsLetterID"] = null;
                    btnSubmit.Text = "Submit";
                }
                this.BindNewsletter();
            }
            txtEmail.Text = "";
        }

        protected void grdNewsLetter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                NewsLetterID = Convert.ToInt64(e.CommandArgument);
                string mailid = e.CommandArgument.ToString();
                newsletter_handler newsletterHandler = new newsletter_handler();
                DataSet ds = newsletterHandler.get_newsletter(NewsLetterID, "", 1);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    txtEmail.Text = dt.Rows[0]["Email"].ToString();
                    btnSubmit.Text = "Update";
                }
            }
            else
            {
                if (e.CommandName == "Active")
                {
                    bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "news_letter_id", "tbl_news_letter");
                    this.BindNewsletter();
                    grdNewsLetter.Focus();
                }
                else
                {
                    bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "news_letter_id", "tbl_news_letter");
                    this.BindNewsletter();
                    grdNewsLetter.Focus();
                }
            }
        }

        protected void grdNewsLetter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton lnk = (ImageButton)e.Row.FindControl("imgBtnActive");
                Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                if (lblstatus.Text == "False")
                {
                    lnk.CommandName = "Active";
                }
                if (lblstatus.Text == "True")
                {
                    lnk.CommandName = "Deactive";
                }
            }
        }

        protected void grdNewsLetter_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long newsid = Convert.ToInt64(grdNewsLetter.DataKeys[e.RowIndex].Values["news_letter_id"].ToString());

            newsletter_handler newsletterHandler = new newsletter_handler();
            bool delete = newsletterHandler.delete_newsletter(newsid);
            if (delete)
            {
                this.BindNewsletter();
                grdNewsLetter.Focus();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            this.BindNewsletter();
            ViewState["NewsLetterID"] = null;
            btnSubmit.Text = "Submit";
            grdNewsLetter.Focus();
        }
    }
}