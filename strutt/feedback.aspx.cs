using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using BusinessEntities;
using DAL;

namespace strutt
{
    public partial class feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["customerDetailsId"] == null)
                Response.Redirect("~/");

            if (!IsPostBack)
            {
                if (!btnSubmit.Enabled)
                    Response.Redirect("~/");

                if (!string.IsNullOrEmpty(Request.QueryString["rate"]))
                {
                    ViewState["Ratting"] = Convert.ToByte(Request.QueryString["rate"]);
                    switch (Convert.ToByte(ViewState["Ratting"]))
                    {
                        case 1:
                            lbtnRate1_Click(lbtnRate1, null);
                            break;
                        case 2:
                            lbtnRate2_Click(lbtnRate2, null);
                            break;
                        case 3:
                            lbtnRate3_Click(lbtnRate3, null);
                            break;
                        case 4:
                            lbtnRate4_Click(lbtnRate4, null);
                            break;
                        case 5:
                            lbtnRate5_Click(lbtnRate5, null);
                            break;
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string selectedCategory = "";
            if (CheckBox1.Checked) selectedCategory += CheckBox1.Text + ", ";
            if (CheckBox2.Checked) selectedCategory += CheckBox2.Text + ", ";
            if (CheckBox3.Checked) selectedCategory += CheckBox3.Text + ", ";
            if (CheckBox4.Checked) selectedCategory += CheckBox4.Text + ", ";
            if (CheckBox5.Checked) selectedCategory += CheckBox5.Text + ", ";
            if (!string.IsNullOrEmpty(selectedCategory))
                selectedCategory = selectedCategory.Remove(selectedCategory.Length - 2);
            
            feedback_data_handler feedbackHandler = new feedback_data_handler();
            int result = feedbackHandler.insert_feedback(Guid.Parse(Session["customerDetailsId"].ToString()), Convert.ToByte(ViewState["Ratting"]), 
                selectedCategory, txtSuggestion.Text);

            if (result > 0)
            {
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "Thanks for taking out time to help us improve.";
                btnSubmit.Enabled = false;
            }
        }

        protected void lbtnRate1_Click(object sender, EventArgs e)
        {
            ViewState["Ratting"] = 1;
            litRateTitle.Text = "Very Poor";
            lbtnRate1.CssClass = "rate_smiley rate1 active";
            lbtnRate2.CssClass = "rate_smiley rate2 false";
            lbtnRate3.CssClass = "rate_smiley rate3 false";
            lbtnRate4.CssClass = "rate_smiley rate4 false";
            lbtnRate5.CssClass = "rate_smiley rate5 false";
        }
        protected void lbtnRate2_Click(object sender, EventArgs e)
        {
            ViewState["Ratting"] = 2;
            litRateTitle.Text = "Poor";
            lbtnRate2.CssClass = "rate_smiley rate2 active";
            lbtnRate1.CssClass = "rate_smiley rate1 false";
            lbtnRate3.CssClass = "rate_smiley rate3 false";
            lbtnRate4.CssClass = "rate_smiley rate4 false";
            lbtnRate5.CssClass = "rate_smiley rate5 false";
        }
        protected void lbtnRate3_Click(object sender, EventArgs e)
        {
            ViewState["Ratting"] = 3;
            litRateTitle.Text = "Fair";
            lbtnRate3.CssClass = "rate_smiley rate3 active";
            lbtnRate1.CssClass = "rate_smiley rate1 false";
            lbtnRate2.CssClass = "rate_smiley rate2 false";
            lbtnRate4.CssClass = "rate_smiley rate4 false";
            lbtnRate5.CssClass = "rate_smiley rate5 false";
        }
        protected void lbtnRate4_Click(object sender, EventArgs e)
        {
            ViewState["Ratting"] = 4;
            litRateTitle.Text = "Good";
            lbtnRate4.CssClass = "rate_smiley rate4 active";
            lbtnRate1.CssClass = "rate_smiley rate1 false";
            lbtnRate2.CssClass = "rate_smiley rate2 false";
            lbtnRate3.CssClass = "rate_smiley rate3 false";
            lbtnRate5.CssClass = "rate_smiley rate5 false";
        }
        protected void lbtnRate5_Click(object sender, EventArgs e)
        {
            ViewState["Ratting"] = 5;
            litRateTitle.Text = "Very Good";
            lbtnRate5.CssClass = "rate_smiley rate5 active";
            lbtnRate1.CssClass = "rate_smiley rate1 false";
            lbtnRate2.CssClass = "rate_smiley rate2 false";
            lbtnRate3.CssClass = "rate_smiley rate3 false";
            lbtnRate4.CssClass = "rate_smiley rate4 false";
        }

    }
}