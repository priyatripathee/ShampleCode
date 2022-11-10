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
    public partial class orderstatus : System.Web.UI.Page
    {
        long ordId = 0;
        string Email = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ordid"] != null)
                {
                    ordId = Convert.ToInt64(Request.QueryString["ordid"].ToString());
                    bindOrderStatus(ordId);
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
        }
        private void bindOrderStatus(long ordId)
        {
            order_handler orderHandler = new order_handler();
            DataSet dstack = orderHandler.get_order_search(ordId,null);
            if (dstack != null && dstack.Tables.Count > 0)
            {
                DataTable dtord = dstack.Tables[0];
                if (dtord.Rows.Count > 0)
                {
                    string status = dtord.Rows[0]["order_status"].ToString();
                    string conDate = dtord.Rows[0]["CnfDate"].ToString();
                    string pkdDate = dtord.Rows[0]["PkdDate"].ToString();
                    string DisDate = dtord.Rows[0]["DPDate"].ToString();
                    string DelDate = dtord.Rows[0]["DLDate"].ToString();

                    lblOrdId.Text = dtord.Rows[0]["order_id"].ToString();
                    lblOrderDate.Text = dtord.Rows[0]["OrdDate"].ToString();

                    if (status == "Confirmed" && conDate != "")
                    {
                        trconfirm.Visible = true;
                        lblConfirm.Text = "Confirmed";
                        lblConfirmDate.Text = dtord.Rows[0]["CnfDate"].ToString();
                    }
                    else
                    {
                        lblConfirmDate.Text = "...";
                    }


                    if (status == "Packed" && pkdDate != "")
                    {
                        trconfirm.Visible = true;
                        lblConfirm.Text = "Confirmed";
                        lblConfirmDate.Text = dtord.Rows[0]["CnfDate"].ToString();

                        trpacked.Visible = true;
                        lblPacked.Text = "Packed";
                        lblPacketDate.Text = dtord.Rows[0]["PkdDate"].ToString();
                    }
                    else
                    {
                        lblPacketDate.Text = "...";

                    }

                    if (status == "Dispatched" && DisDate != "")
                    {
                        trconfirm.Visible = true;
                        lblConfirm.Text = "Confirmed";
                        lblConfirmDate.Text = dtord.Rows[0]["CnfDate"].ToString();

                        trpacked.Visible = true;
                        lblPacked.Text = "Packed";
                        lblPacketDate.Text = dtord.Rows[0]["PkdDate"].ToString();

                        trdispatch.Visible = true;
                        lblDispatch.Text = "Dispatched";
                        lblDispatchDate.Text = dtord.Rows[0]["DPDate"].ToString();
                    }
                    else
                    {
                        lblDispatchDate.Text = "...";
                    }

                    if (status == "Delivered" && DelDate != "")
                    {
                        trconfirm.Visible = true;
                        lblConfirm.Text = "Confirmed";
                        lblConfirmDate.Text = dtord.Rows[0]["CnfDate"].ToString();

                        trpacked.Visible = true;
                        lblPacked.Text = "Packed";
                        lblPacketDate.Text = dtord.Rows[0]["PkdDate"].ToString();

                        trdispatch.Visible = true;
                        lblDispatch.Text = "Dispatched";
                        lblDispatchDate.Text = dtord.Rows[0]["DPDate"].ToString();

                        trdeliver.Visible = true;
                        lblDelivered.Text = "Delivered";
                        lblDeliveredDate.Text = dtord.Rows[0]["DLDate"].ToString();
                    }
                    else
                    {
                        lblDeliveredDate.Text = "...";
                    }

                    if (status == "Cancelled")
                    {
                        trcancel.Visible = true;
                        lblCancel.Text = "Cancelled";
                        lblCancelDate.Text = dtord.Rows[0]["CanDate"].ToString();
                    }
                    if (status == "RTO")
                    {
                        trcancel.Visible = true;
                        lblCancel.Text = "RTO";
                        lblCancelDate.Text = "N/A";
                    }
                    else
                    {
                        lblCancelDate.Text = "...";
                    }
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("orderhistory.aspx", false);
        }
    }
}