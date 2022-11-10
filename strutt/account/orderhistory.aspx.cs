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

namespace strutt.account
{
    public partial class orderhistory : System.Web.UI.Page
    {
        string Email = "";
        long orderstatusId = 0;
        string statusName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CustomerLoginDetails"] != null)
                {
                    if (Session["loginType"] == null || Session["loginType"].ToString() == "guest")
                    {
                        Session["CustomerLoginDetails"] = null;
                        Response.Redirect("~/login.aspx");
                    }
                    //if (Session["loginType"].ToString() != "register")
                    //{
                    //    Session["CustomerLoginDetails"] = null;
                    //    Response.Redirect("~/login.aspx");
                    //}

                    Email = Session["CustomerLoginDetails"].ToString();
                    bindLastOrderStatus(Email);
                }
                else
                {
                    Response.Redirect("~/login.aspx");
                }
            }
        }
        private void bindLastOrderStatus(string Email)
        {
            Guid customerId = Guid.Empty;
            string orderStatus = "";

            customer_handler customerdetailsHandler = new customer_handler();
            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();
            ds = customerdetailsHandler.get_customer_login_details(Email);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    customerId = Guid.Parse(dt.Rows[0]["customer_id"].ToString());
                    DataSet dscust = orderHandler.get_order_status(null, null, null,null,null , null, null, customerId, 3, true);
                    if (dscust != null && dscust.Tables.Count > 0)
                    {
                        DataTable dtcust = dscust.Tables[0];
                        if (dtcust.Rows.Count > 0)
                        {
                           
                            
                            orderStatus = dtcust.Rows[0]["order_status"].ToString();
                            rptLastOrderPlaceStaus.DataSource = dtcust;
                            rptLastOrderPlaceStaus.DataBind();
                        }
                        else
                        {
                            lblLastOrder.Text = "0 orders placed in past 6 months";
                        }
                    }

                }
            }
        }

        protected void rptLastOrderPlaceStaus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfieldorderstatus = (HiddenField)e.Item.FindControl("hfieldorderstatusId");
                orderstatusId = Convert.ToInt64(hfieldorderstatus.Value);
                Image imgOrdStatus = (Image)e.Item.FindControl("imgorderStatus");

                Label OrderStatus = (Label)e.Item.FindControl("lblOrderStatus");
                //Label OrderId = (Label)e.Item.FindControl("lblOrderId");
                //Label TotalAmount = (Label)e.Item.FindControl("lblAmount");

                Label StatusMsg = (Label)e.Item.FindControl("lblStatusMsg");

                Button btnProReturn = (Button)e.Item.FindControl("btnReturn");
                Button btnCancel = (Button)e.Item.FindControl("btncancel");
                //LinkButton btnProInvoice = (LinkButton)e.Item.FindControl("btnInvoice");
                HyperLink btnProInvoice = (HyperLink)e.Item.FindControl("ibtnInvoice");
                LinkButton btnProTrackOrder = (LinkButton)e.Item.FindControl("btntrackorder");
                LinkButton btnpickertrackorder = (LinkButton)e.Item.FindControl("btnpickertrackorder");
                order_handler orderHandler = new order_handler();
                DataSet dstack = orderHandler.get_order_status_track(orderstatusId, 0);
                if (dstack != null && dstack.Tables.Count > 0)
                {
                    DataTable dttack = dstack.Tables[0];
                    if (dttack.Rows.Count > 0)
                    {
                        statusName = dttack.Rows[0]["order_status"].ToString();


                        if (statusName == "Pending for Admin")
                        {
                            btnProReturn.Enabled = true;
                            OrderStatus.Text = "Order Placed";
                            imgOrdStatus.ImageUrl = "~/images/step1.jpg";

                        }

                        else if (statusName == "inprogress")
                        {
                            btnProReturn.Enabled = true;
                            OrderStatus.Text = "Confirmed";
                            imgOrdStatus.ImageUrl = "~/images/step1.jpg";
                        }
                        else if (statusName == "Confirmed")
                        {
                            btnProReturn.Enabled = true;
                            OrderStatus.Text = "Confirmed";
                            imgOrdStatus.ImageUrl = "~/images/step1.jpg";
                        }
                        else if (statusName == "Packed")
                        {
                            btnProReturn.Enabled = true;
                            btnCancel.Enabled = true;
                            OrderStatus.Text = "Packed on";
                            imgOrdStatus.ImageUrl = "~/images/step2.jpg";
                        }
                        else if (statusName == "Dispatched")
                        {

                            btnProReturn.Enabled = true;
                            btnCancel.Enabled = false;
                            //ProReturn.BackColor = System.Drawing.ColorTranslator.FromHtml("#6A5750");
                            btnProReturn.CssClass = "btnreturnfalse";
                            OrderStatus.Text = "Dispatched on";
                            imgOrdStatus.ImageUrl = "~/images/step3.jpg";
                        }
                        else if (statusName == "Delivered")
                        {
                            btnProReturn.Enabled = true;
                            btnCancel.Enabled = false;
                            btnProTrackOrder.Enabled = false;
                            btnpickertrackorder.Enabled = false;
                            //  btnProReturn.CssClass = "btnreturnfalse";
                            OrderStatus.ForeColor = System.Drawing.Color.Green;
                            OrderStatus.Text = "Delivered on";
                            imgOrdStatus.ImageUrl = "~/images/step4.jpg";
                            StatusMsg.ForeColor = System.Drawing.Color.Green;
                            StatusMsg.Text = "Delivered Successfully!";
                        }
                        else if (statusName == "Failed")
                        {
                            btnProReturn.Enabled = true;
                            OrderStatus.ForeColor = System.Drawing.Color.Red;
                            OrderStatus.Text = "Failed on";
                            btnProReturn.CssClass = "btnreturnfalse";
                            //StatusMsg.ForeColor = System.Drawing.Color.Red;
                            StatusMsg.Text = "Your item has been failed! Please try again.";
                        }
                        else if (statusName == "Cancelled")
                        {
                            btnProReturn.Enabled = false;
                            btnProReturn.CssClass = "btnreturnfalse";

                            btnProInvoice.Enabled = false;
                            //btnProInvoice.CssClass = "btnreturnfalse";

                            btnCancel.Enabled = false;
                            btnCancel.CssClass = "btnreturnfalse";

                            OrderStatus.ForeColor = System.Drawing.Color.Red;
                            OrderStatus.Text = "Cancelled";

                            StatusMsg.ForeColor = System.Drawing.Color.Red;
                            StatusMsg.Text = "Your item has been cancelled.";

                        }
                        else if (statusName == "RTO")
                        {
                            btnProReturn.Enabled = false;
                            OrderStatus.Text = "RTO";
                            StatusMsg.Text = "Your item has been undelivered.";
                        }
                        //else
                        //{ }
                    }
                }
            }
        }

        protected void rptLastOrderPlaceStaus_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "return")
            {
                long OrdId = Convert.ToInt64(e.CommandArgument);
                //HiddenField ProductId = (HiddenField)e.Item.FindControl("hfilesProductId");
                //long ProId = Convert.ToInt64(ProductId.Value);
                Response.Redirect("cancelorder.aspx?ordid=" + OrdId);
            }

            if (e.CommandName == "cancel")
            {
                long OrdId = Convert.ToInt64(e.CommandArgument);
                //HiddenField ProductId = (HiddenField)e.Item.FindControl("hfCancelProductId");
                //long ProId = Convert.ToInt64(ProductId.Value);

                Response.Redirect("cancelorder.aspx?ordid=" + OrdId);


            }
            if (e.CommandName == "trackorder")
            {
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                Session["ship_via"] = arg[0];
                Session["ship_id"] = arg[1];
                string TrackUrl = BLL.Helpers.GetUrltraking(Session["ship_via"], Session["ship_id"], null);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(" + TrackUrl + "');", true);
               Response.Write("<script>");
               Response.Write("window.open(" + TrackUrl + " ,'_blank')");
                Response.Write("</script>");
               // Response.Redirect(TrackUrl);
            }

            if (e.CommandName == "pickertrackorder")
            {
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                Session["ship_via"] = arg[0];
                Session["ship_id"] = arg[1];
                string TrackUrl = BLL.Helpers.GetUrltraking(Session["ship_via"], Session["ship_id"], "Picker");

               // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(" + TrackUrl + "');", true);
               Response.Write("<script>");
                Response.Write("window.open(" + TrackUrl + " ,'_blank')");
               Response.Write("</script>");
                //Response.Redirect(TrackUrl);
            }
            //if (e.CommandName == "invoice")
            //{

            //}
        }

        

    }
}