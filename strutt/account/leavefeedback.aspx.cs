using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BusinessEntities;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data.SqlClient;

namespace strutt.account
{
    public partial class leavefeedback : System.Web.UI.Page
    {
        string Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Btn_LeaveFeedback.Enabled = false;
            }
            if (Session["CustomerLoginDetails"] != null)
            {
                Email = Session["CustomerLoginDetails"].ToString();
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();
            ds = orderHandler.get_order_search(Convert.ToInt32(txtOrderNo.Text),null);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows[0]["email_id"].ToString().Equals(Session["CustomerLoginDetails"].ToString()))
                {
                    txtProductName.Text = dt.Rows[0]["product_name"].ToString();
                    txtOrderDate.Text = dt.Rows[0]["OrdDate"].ToString();
                    txtDeliveredDate.Text = dt.Rows[0]["DLDate"].ToString();

                    lblMsg.Visible = false;
                    Btn_LeaveFeedback.Enabled = true;

                    if (txtDeliveredDate.Text == "")
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "your product hasn't been delivered. you can post feedback once you receive the product.";
                        Btn_LeaveFeedback.Enabled = false;
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Data not found in your order history.";
                    Btn_LeaveFeedback.Enabled = false;
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Data not found.";
                Btn_LeaveFeedback.Enabled = false;
            }
        }

        protected void Btn_LeaveFeedback_Click(object sender, EventArgs e)
        {
            bool result = false;
            order order = new order();
            if (ViewState["LeaveFeedbackID"] != null)
                order.LeaveFeedbackId = Convert.ToInt64(ViewState["LeaveFeedbackID"]);

            order.order_id = Convert.ToInt32(txtOrderNo.Text);
            order.order_date = txtOrderDate.Text;
            order.product_name = txtProductName.Text;
            order.email_id = Session["CustomerLoginDetails"].ToString();
            order.rating = ratingControl.CurrentRating;
            order.ItemArrived = Convert.ToBoolean(rbtnListArrived.SelectedValue);
            order.ItemDescribed = Convert.ToBoolean(rbtnListDescribed.SelectedValue);
            order.DepartureOnTime = Convert.ToBoolean(rbtnTime.SelectedValue);
            order.FeedbackComment = txtMessage.Text;

            order_handler orderHandler = new order_handler();
            result = orderHandler.insert_LeaveFeedback(order);
            if (result)
            {
                lblValMes.Visible = true;
                lblValMes.Text = "Your Feedback Sent Successfully.";
            }
            else
            {
                lblValMes.Visible = true;
                lblValMes.Text = "Feedback Sent Failure. Please send us email.";
            }
        }
    }
}