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
    public partial class searchorderdetails : System.Web.UI.Page
    {
        int ordId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserID"] == null)
                    Response.Redirect("../account/Login.aspx");

                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                if (Request.QueryString["id"] != null)
                {
                    txtOrderNumber.Text = Request.QueryString["id"];
                    btnSearch_Click(btnSearch, null);
                }
                if (Session["Role"].ToString() == "Admin")
                {
                    btnSave.Visible = false;
                }
            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            double PayableAmt = 0;
            double TotalAmt = 0;
          
            order_handler orderHandler = new order_handler();
            DataSet dsOrdDet = new DataSet();
            dsOrdDet = orderHandler.get_order_search_orderdetail(Convert.ToInt32(txtOrderNumber.Text));
            if (dsOrdDet != null && dsOrdDet.Tables.Count > 0)
            {

                DataTable dt = dsOrdDet.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Visible = false;
                    rptSearchOrderDetails.Visible = true;
                    rptSearchOrderDetails.DataSource = dt;
                    rptSearchOrderDetails.DataBind();

                    lbldiscount.Text = dt.Rows[0]["discount"].ToString();
                    lblTotalPrice.Text = dt.Compute("Sum(total_price)", string.Empty).ToString();
                    lblCustomeBagChages.Text = dt.Compute("Sum(custom_bag_price)", string.Empty).ToString();

                    DataSet dsOrdMas = orderHandler.get_order_search_order(Convert.ToInt32(txtOrderNumber.Text));
                    if (dsOrdMas != null && dsOrdMas.Tables.Count > 0)
                    {
                        DataTable dt2 = dsOrdMas.Tables[0];
                        if (dt2.Rows.Count > 0)
                        {

                            //  tblOrderDetails.Visible = true;
                            // lblProductId.Text = dt.Rows[i]["ProductId"].ToString();

                            lblCustomerId.Text = dt2.Rows[0]["customer_id"].ToString();
                            lblEmail.Text = dt2.Rows[0]["email_id"].ToString();
                            lblContactNo.Text = dt2.Rows[0]["contact_number"].ToString();
                            lblAddress.Text = dt2.Rows[0]["address"].ToString();
                            lblMessage.Text = dt2.Rows[0]["Message"].ToString();
                            lblCity.Text = dt2.Rows[0]["city"].ToString();
                            lblState.Text = dt2.Rows[0]["state"].ToString();
                            lblPinCode.Text = dt2.Rows[0]["pin_code"].ToString();
                            lblCustomerName.Text = dt2.Rows[0]["customer_name"].ToString();
                            lblOrderId.Text = dt2.Rows[0]["order_id"].ToString();
                            lblOrder.Text = dt2.Rows[0]["order_id"].ToString();
                            lblPackedDate.Text = dt2.Rows[0]["PkdDate"].ToString();
                            lblOrderDate.Text = dt2.Rows[0]["OrdDate"].ToString();
                            lblOrderDateTime.Text = dt2.Rows[0]["OrdDate"].ToString();
                            lblConformedDate.Text = dt2.Rows[0]["CnfDate"].ToString();
                            //lblpaymentthrough.Text = dt2.Rows[0]["payment_through"].ToString();
                            ddlPayment.SelectedValue = dt2.Rows[0]["payment_through"].ToString();
                            lblReasonforCancellation.Text = dt2.Rows[0]["reason_for_cancellation"].ToString();
                            lblDispatchedDate.Text = dt2.Rows[0]["DPDate"].ToString();
                            lblDeliveredDate.Text = dt2.Rows[0]["DLDate"].ToString();
                            lblShipVia.Text = dt2.Rows[0]["ship_via"].ToString();
                            lblShipId.Text = dt2.Rows[0]["ship_id"].ToString();

                            if (dt2.Rows[0]["Freight"] == DBNull.Value)
                                lblFreight.Text = "0.00";
                            else
                                lblFreight.Text = dt2.Rows[0]["Freight"].ToString();

                            if (dt2.Rows[0]["shipping_price"] == DBNull.Value)
                                lblshippingprice.Text = "0.00";
                            else
                                lblshippingprice.Text = dt2.Rows[0]["shipping_price"].ToString();

                            lblOrdereStatus.Text = dt2.Rows[0]["order_status"].ToString();
                            lblCouponCode.Text = dt2.Rows[0]["coupon_code"].ToString();
                            //lblCheckOption.Text = dt2.Rows[0]["GuestStatus"].ToString();
                            //bool chkOption = Convert.ToBoolean(dt.Rows[0]["GuestStatus"].ToString());
                            //if (chkOption == false)
                            //{
                            //    lblCheckOption.Text = "Register User";
                            //}
                            //if (chkOption == true)
                            //{
                            //    lblCheckOption.Text = "Guest User";
                            //}
                            //tblOrderDetails.Visible = true;
                            //lblCouponCode.Text = dt.Rows[0]["coupon_code"].ToString();
                            // lblTotalPrice.Text = dt.Rows[0]["total_price"].ToString();
                        }
                    }

                    //lblTotalPrice.Text = (Convert.ToDouble(dt.Rows[0]["price"]) - Convert.ToDouble(lblshippingprice.Text)).ToString("0.00");
                    //lblTotalPrice.Text = (Convert.ToDouble(dt.Rows[0]["price"])).ToString("0.00");



                    PayableAmt = Convert.ToDouble(dt.Rows[0]["price"])
                                // - Convert.ToDouble(dt.Rows[0]["discount"])
                                //+ Convert.ToDouble(lblshippingprice.Text)
                                + Convert.ToDouble(lblFreight.Text);
                    lblPayableAmount.Text = PayableAmt.ToString("0.00");
                }
            }
            else
            {
                // tblOrderDetails.Visible = false;
                lblMsg.Visible = true;
                rptSearchOrderDetails.Visible = false;
                lblMsg.Text = "data not found.";
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["fdate"] != null)
                Response.Redirect("~/Admin/orderstatus.aspx?fdate=" + Request.QueryString["fdate"] + "&tdate=" + Request.QueryString["tdate"] + "&status=" + Request.QueryString["status"]);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            order_handler orderHandler = new order_handler();
            orderHandler.update_order_PaymentType(Convert.ToInt32(txtOrderNumber.Text), ddlPayment.SelectedValue);
        }
    }
}

