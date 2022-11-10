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
using System.Net;
using System.IO;
using System.Configuration;
using strutt.api;
using api;

namespace strutt.Admin
{
    public partial class orderstatus : System.Web.UI.Page
    {
        int ordId = 0;
        string ordPrifix = "STR10001";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                fillMonthRange();
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();

                if (Request.QueryString["order_id"] != null)
                {
                    ordId = Convert.ToInt32(Request.QueryString["order_id"].ToString());
                }

                if (Request.QueryString["fdate"] != null)
                    txtfromdate.Text = Request.QueryString["fdate"];
                else
                    txtfromdate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");

                if (Request.QueryString["tdate"] != null)
                    txtfromdate.Text = Request.QueryString["tdate"];
                else
                    txttodate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");

                if (Request.QueryString["status"] != null)
                    ddlStatusOrder.SelectedValue = Request.QueryString["status"];
                else
                    ddlStatusOrder.SelectedIndex = 0;

                txttodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtfromdate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.bindOrderStatus();

                Label lbl_role = new Label();
                lbl_role.Text = Session["Role"].ToString();

                if (lbl_role.Text == "Admin")
                {
                    {
                        btnDelete.Visible = false;
                        btnEdit.Visible = false;
                        this.bindOrderStatus();
                    }
                }
            }

        }

        private void bindOrderStatus()
        {
            Guid ordId = Guid.Empty;
            DateTime? Fromdate = null;
            DateTime? Todate = null;
            string Statusorder = null;
            string payment = null, payment2 = null ;
            string Customername = null;
            string Phoneno = null;
            if (ddlDateRange.SelectedValue.Equals("0"))
            {

                if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                {
                    Fromdate = Convert.ToDateTime(txtfromdate.Text);
                    Todate = Convert.ToDateTime(txttodate.Text + " 23:59:59");
                }

            }
            else
            {
                string dateRange = ddlDateRange.SelectedValue;
                Fromdate = Convert.ToDateTime(dateRange.Split('|').GetValue(0).ToString());
                Todate = Convert.ToDateTime(dateRange.Split('|').GetValue(1) + " 23:59:59");
            }
            if (!string.IsNullOrEmpty(txtSearchcustomername.Text))
            {
                Customername = (txtSearchcustomername.Text);
            }
            if (!string.IsNullOrEmpty(txtSerachphoneno.Text))
            {
                Phoneno = (txtSerachphoneno.Text);
            }
            if (ddlStatusOrder.SelectedItem.Text != "All Status")
            {
                Statusorder = ddlStatusOrder.SelectedItem.Text;
            }
            if(ddlPaymentType.SelectedItem.Text == "COD")
            {
                payment = ddlPaymentType.SelectedItem.Text;
            }
            else if (ddlPaymentType.SelectedItem.Text == "Online")
            {
                payment2 = ddlPaymentType.SelectedItem.Text;
            }
            

                //if (ddlStatusOrder.SelectedItem.Text != "Delivered")
                //{
                //    Statusorder = ddlStatusOrder.SelectedItem.Text;
                //}
                int isactive = 1;
            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();
            ds = orderHandler.get_order_status(Fromdate, Todate, Phoneno, Customername, Statusorder, payment,payment2  ,null, 4, !chkDeleteRecord.Checked);

            if (ds != null && ds.Tables.Count > 0)
            {

                lblMsg.Visible = true;
                DataTable dt = ds.Tables[0];
                lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                grdOrderStatus.DataSource = dt;
                grdOrderStatus.DataBind();
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "data not founds this date. Please search other date.";
                grdOrderStatus.DataSource = null;
                grdOrderStatus.DataBind();
            }
        }

        

        protected void grdOrderStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Date Range Done by divyesh gandhi -------------------
            Guid ordId = Guid.Empty;
            DateTime? Fromdate = null;
            DateTime? Todate = null;
            string Customername = null;
            string Phoneno = null;
            string Statusorder = null;
            
            if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
            {
                Fromdate = Convert.ToDateTime(txtfromdate.Text);
                Todate = Convert.ToDateTime(txttodate.Text + " 23:59:59");
            }
            if (!string.IsNullOrEmpty(txtSearchcustomername.Text))
            {
                Customername = (txtSearchcustomername.Text);
            } 
            if (!string.IsNullOrEmpty(txtSerachphoneno.Text))
            {
                Phoneno = (txtSerachphoneno.Text);
            }
            if (ddlStatusOrder.SelectedItem.Text != "All")
            {
                Statusorder = ddlStatusOrder.SelectedItem.Text;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    HiddenField hfieldOrderStatus = (HiddenField)e.Row.FindControl("hfieldOrderStatus");
                    DropDownList ddlOrderStatus = (DropDownList)e.Row.FindControl("ddlOrderStatus");
                    HiddenField hfieldshipvia = (HiddenField)e.Row.FindControl("hfieldshipvia");
                    DropDownList ddlshipvia = (DropDownList)e.Row.FindControl("ddlshipvia");
                    Label lblCustom = (Label)e.Row.FindControl("lblCustom");
                    order_handler orderHandler = new order_handler();
                    DataSet ds = new DataSet();
                    ds = orderHandler.get_order_status(Fromdate, Todate, null, null, null, null, null, ordId, 0, true);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        ddlOrderStatus.SelectedValue = hfieldOrderStatus.Value;
                        ddlshipvia.SelectedValue = hfieldshipvia.Value;

                    }
                    else
                    {
                        ddlOrderStatus.DataSource = null;
                        ddlOrderStatus.DataBind();
                        ddlshipvia.DataSource = null;
                        ddlshipvia.DataBind();
                    }
                }


                if (ViewState["Edit"] != null)
                {

                }
                else
                {
                    ImageButton lnk = (ImageButton)e.Row.FindControl("imgBtnActive");
                    //Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                    //if (lblstatus.Text == "False")
                    //{
                    //    lnk.CommandName = "Active";
                    //}
                    //if (lblstatus.Text == "True")
                    //{
                    //    lnk.CommandName = "Deactive";
                    //}
                }

                Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                try
                {
                    if (lblstatus.Text == "Cancelled")
                    {
                        //lblstatus.CssClass = "label label-danger";
                        lblstatus.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (lblstatus.Text == "Failed")
                    {
                        lblstatus.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (lblstatus.Text == "New Order")
                    {
                        lblstatus.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (lblstatus.Text == "RTO")
                    {
                        lblstatus.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (lblstatus.Text == "Returned")
                    {
                        lblstatus.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }
                LinkButton ibtnView = (LinkButton)e.Row.FindControl("ibtnView");
                LinkButton ingBtnEdit = (LinkButton)e.Row.FindControl("ingBtnEdit");
                LinkButton lnlbtnPicker = (LinkButton)e.Row.FindControl("lnlbtnPicker");
                LinkButton lnlbtnprint = (LinkButton)e.Row.FindControl("lnlbtnprint");
                LinkButton btnImgDelete = (LinkButton)e.Row.FindControl("btnImgDelete");
                LinkButton btnShow = (LinkButton)e.Row.FindControl("btnShow");
                if (Session["Role"].ToString() == "Admin")
                {
                    
                    ingBtnEdit.Visible = false;
                    lnlbtnPicker.Visible = false;
                    lnlbtnprint.Visible = false;
                    btnImgDelete.Visible = false;
                    btnShow.Visible = false;

                }
            }

        }

        protected void grdOrderStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ViewState["Edit"] = "Edit";

                //TaxID = Convert.ToInt32(e.CommandArgument);
                //TaxesHandler taxHandler = new TaxesHandler();
                //DataSet ds = taxHandler.GetTax(TaxID);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    DataTable dt = ds.Tables[0];
                //    txtTax.Text = dt.Rows[0]["Tax"].ToString();
                //    btnSubmit.Text = "Update";
                //}
            }
            else if (e.CommandName == "View")
            {
                Response.Redirect("~/Admin/searchorderdetails.aspx?id=" + e.CommandArgument
                    + "&fdate=" + txtfromdate.Text + "&tdate=" + txttodate.Text + "&status=" + ddlStatusOrder.SelectedItem.Text);
            }
            else if (e.CommandName == "Cancel")
            {
            }
            else if (e.CommandName == "Update")
            {
            }
            else if (e.CommandName == "Delete")
            {
            }
            else if (e.CommandName == "Picker")
            {
                order_handler orderHandler = new order_handler();
                PickerApiService pikrapi = new PickerApiService();
                int orderid = Convert.ToInt32(e.CommandArgument);
                string proname = "";
                Decimal TotalPrice = 0;

                DataSet ds = new DataSet();
                ds = orderHandler.get_order_search(orderid, null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Decimal Price = Convert.ToDecimal(dt.Rows[i]["sale_price"].ToString());
                            Decimal UnitPrice = Convert.ToInt64(dt.Rows[i]["quantity"].ToString()) * Price;

                            TotalPrice += UnitPrice;
                            Session["totAmount"] = TotalPrice.ToString();
                            proname += dt.Rows[i]["product_name"].ToString() + " , ";
                        }

                        proname = proname.Substring(0, proname.Length - 4);
                    }
                    int result = pikrapi.placeorder_api(orderid, proname, ds.Tables[0].Rows[0]["customer_name"].ToString(), ds.Tables[0].Rows[0]["email_id"].ToString(),
                        ds.Tables[0].Rows[0]["contact_number"].ToString(), ds.Tables[0].Rows[0]["pin_code"].ToString(), ds.Tables[0].Rows[0]["address"].ToString());

                    if (result == 0)
                    {
                        this.bindOrderStatus();
                    }
                    else
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        switch (result)
                        {
                            case 1:
                                lblMsg.Text = "Pickup is Not Avaiable for This Pincode";
                                break;

                            case 2:
                                lblMsg.Text = "Fail to Save at Picker Side";
                                break;

                            case 3:
                                lblMsg.Text = "Fail to Save In Database";
                                break;

                            default:
                                break;
                        }
                    }

                }

            }
            else if (e.CommandName == "Print")
            {
                string url = e.CommandArgument.ToString();
                Response.Redirect(url);
            }
            else if (e.CommandName == "EditCustomer")
            {
                //int OrderId = Convert.ToInt32(e.CommandArgument);
                // int  ordId = Convert.ToInt32(e.CommandArgument);
                //  ViewState["Edit"] = null;
                //txtid.Text = string.Empty;
                int ordId = Convert.ToInt32(e.CommandArgument);
                order_handler orderHandler = new order_handler();
                DataSet dsOrdMas = orderHandler.get_order_search(Convert.ToInt32(ordId), null);
                if (dsOrdMas != null && dsOrdMas.Tables.Count > 0)
                {
                    DataTable dt2 = dsOrdMas.Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        hfeditOrderId.Value = (e.CommandArgument.ToString());
                        //  txtid.Text= e.CommandArgument.ToString();
                        txtpopupphoneno.Text = dt2.Rows[0]["contact_number"].ToString();
                        txtaddress.Text = dt2.Rows[0]["address"].ToString();
                        txtcity.Text = dt2.Rows[0]["city"].ToString();
                        txtstate.Text = dt2.Rows[0]["state"].ToString();
                        txtpincode.Text = dt2.Rows[0]["pin_code"].ToString();
                        txtcustomer.Text = dt2.Rows[0]["customer_name"].ToString();
                        this.mp1.Show();
                    }
                    else
                    {
                        ViewState["Edit"] = null;
                        if (e.CommandName == "Active")
                        {
                            bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "order_id", "tbl_order");
                            this.bindOrderStatus();
                            grdOrderStatus.Focus();
                        }
                        else
                        {
                            bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "order_id", "tbl_order");
                            this.bindOrderStatus();
                            grdOrderStatus.Focus();
                        }

                    }
                }
            }
        }


        protected void grdOrderStatus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdOrderStatus.EditIndex = -1;
            bindOrderStatus();
        }

        protected void grdOrderStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ordId = Convert.ToInt32(grdOrderStatus.DataKeys[e.RowIndex].Values["order_id"].ToString());
            // string ordernumber = grdOrderStatus.DataKeys[e.RowIndex].Values["order_number"].ToString();
            order_handler orderHandler = new order_handler();
            bool delete = orderHandler.pr_delete_order(ordId);
            if (delete)
            {
                this.bindOrderStatus();
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = ordId + " " + DAL.helper_data.getMessage("msgDeleteSuccessfully");
                // grdOrderStatus.Focus();
            }

        }

        protected void grdOrderStatus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdOrderStatus.EditIndex = e.NewEditIndex;
            bindOrderStatus();
        }


        protected void grdOrderStatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string proname = "";
            int Flag = 0;
            Decimal TotalPrice = 0;
            order_handler orderHandler = new order_handler();
            ordId = Convert.ToInt32(grdOrderStatus.DataKeys[e.RowIndex].Value.ToString());

            string paymentType = "";

            if (ordId != 0)
            {
                ViewState["ordId"] = ordId;

                DataSet ds = new DataSet();
                ds = orderHandler.get_order_search(Convert.ToInt32(ViewState["ordId"].ToString()), null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Decimal Price = Convert.ToDecimal(dt.Rows[i]["sale_price"].ToString());
                            Decimal UnitPrice = Convert.ToInt64(dt.Rows[i]["quantity"].ToString()) * Price;

                            TotalPrice += UnitPrice;
                            Session["totAmount"] = TotalPrice.ToString();
                            proname += dt.Rows[i]["product_name"].ToString() + " and ";
                        }

                        proname = proname.Substring(0, proname.Length - 4);


                        ViewState["product_id"] = dt.Rows[0]["product_id"].ToString();
                        ViewState["customer_name"] = dt.Rows[0]["customer_name"].ToString();
                        ViewState["email_id"] = dt.Rows[0]["email_id"].ToString();
                        ViewState["address"] = dt.Rows[0]["address"].ToString() + " " + dt.Rows[0]["city"].ToString() + " " + dt.Rows[0]["state"].ToString() + " " + dt.Rows[0]["pin_code"].ToString();
                        ViewState["quantity"] = dt.Rows[0]["quantity"].ToString();
                        ViewState["sale_price"] = dt.Rows[0]["sale_price"].ToString();
                        ViewState["thumb_image"] = dt.Rows[0]["thumb_image"].ToString();
                        //ViewState["product_name"] = dt.Rows[0]["product_name"].ToString();

                        ViewState["product_name"] = proname.ToString();

                        ViewState["contact_number"] = dt.Rows[0]["contact_number"].ToString();

                        paymentType = dt.Rows[0]["payment_through"].ToString();
                        ViewState["manifest_link"] = dt.Rows[0]["manifest_link"].ToString();
                        ViewState["order_status"] = dt.Rows[0]["order_status"].ToString();
                        ViewState["ship_id"] = dt.Rows[0]["ship_id"].ToString();
                    }
                }
            }

            DropDownList orderStatus = (DropDownList)grdOrderStatus.Rows[e.RowIndex].FindControl("ddlOrderStatus");
            string OrderStatus = orderStatus.SelectedItem.Text;

            DropDownList shipvia = (DropDownList)grdOrderStatus.Rows[e.RowIndex].FindControl("ddlshipvia");
            string ShipVia = shipvia.SelectedItem.Text;


            TextBox txtEditship_id = (TextBox)grdOrderStatus.Rows[e.RowIndex].FindControl("txtEditShipId");

            ViewState["ship_id"] = txtEditship_id.Text;

            // TextBox txtEditFreight = (TextBox)grdOrderStatus.Rows[e.RowIndex].FindControl("txtEditFreight");
            // string Freight = "0.00";

            if (OrderStatus == "inprogress")
            {
                Flag = 0;
            }
            else if (OrderStatus == "Confirmed")
            {
                //this.bindConfirmMail();
                // Send Email when Confirmed as per Vishesh said on 20-Apr-2019
                success pageSuccess = new success();
                pageSuccess.SendEmailConfirmOrder(paymentType, ordId);

                Flag = 1;
            }
            else if (OrderStatus == "Packed")
            {
                this.bindSmsPacked();
                Flag = 2;
            }
            else if (OrderStatus == "Scheduled")
            {
                Flag = 8;
            }
            else if (OrderStatus == "Dispatched")
            {
                Flag = 3;
            }
            else if (OrderStatus == "Delivered")
            {
                Flag = 4;
            }
            else if (OrderStatus == "Failed")
            {
                Flag = 5;
            }
            else if (OrderStatus == "RTO")
            {
                Flag = 7;
            }
            //if (string.IsNullOrEmpty(txtEditFreight.Text))
            //{
            //    txtEditFreight.Text = Freight;
            //}

            string pickkrmanifest = ViewState["manifest_link"].ToString();
            string pickkrorderstatus = ViewState["order_status"].ToString();
            string ship_id = ViewState["ship_id"].ToString();
            PickerApiService pikrapi = new PickerApiService();
            if (OrderStatus == "Cancelled")
            {
                if (!string.IsNullOrEmpty(pickkrmanifest))
                {
                    if (pickkrorderstatus != "Dispatched")
                    {
                        bool cancelpikkr = pikrapi.cancelorder_api(ship_id);
                    }
                }
            }
            order Order = new order();

            Order.order_id = ordId;
            Order.ship_id = txtEditship_id.Text;
            // Order.freight = txtEditFreight.Text;
            Order.order_status = orderStatus.SelectedItem.Text;
            Order.ship_via = shipvia.SelectedItem.Text;
            Order.Flag = Flag;

            bool result = orderHandler.update_order_status(Order);
            if (result)
            {
                lblMsg.Text = "Updated Successfully.";
                grdOrderStatus.EditIndex = -1;
                bindOrderStatus();

                if (OrderStatus == "Dispatched")
                {
                    this.bindDispatchedMail();
                    Utility.SendSmsOrderDispatch(ViewState["contact_number"].ToString(), ViewState["customer_name"].ToString(), ViewState["ordId"].ToString(), ViewState["ship_id"].ToString(), ViewState["ordId"].ToString());
                }
                else if (OrderStatus == "Delivered")
                {
                    this.bindDeliveredMail();
                    Utility.SendSmsOrderConfirmation(ViewState["contact_number"].ToString(), ViewState["customer_name"].ToString(), ViewState["ordId"].ToString());
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txttodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtfromdate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
            txtSearchcustomername.Text = "";
            txtSerachphoneno.Text = "";
            //ddlStatusOrder.SelectedIndex = "All";
            ddlStatusOrder.SelectedItem.Text = "All";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            BusinessEntities.order orderDetails = new order();
            orderDetails.order_id = Convert.ToInt64(hfeditOrderId.Value);

            // orderDetails.order_id = Convert.ToInt32(txtid.Text);
            orderDetails.state = txtstate.Text;
            orderDetails.city = txtcity.Text;
            orderDetails.address = txtaddress.Text;
            orderDetails.pin_code = txtpincode.Text;
            orderDetails.contact_number = txtpopupphoneno.Text;
            orderDetails.user_name = txtcustomer.Text;
            DAL.order_data lodc = new DAL.order_data();
            lodc.update_order_customer(orderDetails);

            lblMsg.Text = "Updated Successfully.";
            this.bindOrderStatus();
        }



        #region ***********Mailer/SMS****************

        #region Confirm

        private void bindSmsPacked() /////////////received
        {
            string ordId = ordPrifix + ViewState["ordId"].ToString();

            ///Enter your details 
            //string Message = "Happiness Thank you so much for choosing us to deliver your Gifts. We have received your order S2S" + ViewState["ordId"].ToString() + " amounting to " + ViewState["sale_price"].ToString() + " and is being processed. You can expect delivery by 5-7 days. We will keep you posted with the progress.";
            string Message = "Happiness " + ViewState["customer_name"].ToString() + " Your order no " + ordId + " for " + ViewState["product_name"] + " has been packed . We will get back with the dispatch details shortly";
            string MobileNo = ViewState["contact_number"].ToString();

            Utility.SendSms(MobileNo, Message);
            //WebClient client = new WebClient();
            //string baseurl = "http://push3.maccesssmspush.com/servlet/com.aclwireless.pushconnectivity.listeners.TextListener?userId=stsalt&pass=stsalt&appid=stsalt&subappid=stsalt&contenttype=1&to=" + MobileNo + "&from=SGIFTS&text=" + Message + "&selfid=true&alert=1&dlrreq=true";
            //string baseurl = "http://push3.maccesssmspush.com/servlet/com.aclwireless.pushconnectivity.listeners.TextListener?userId=stsalt&pass=stsalt&appid=stsalt&subappid=stsalt&contenttype=1&to=" + MobileNo + "&from=SGIFTS&text=" + "Thank you for registering with us. We will get back to you shortly." + "&selfid=true&alert=1&dlrreq=true";
            //string baseurl = "http://push3.maccesssmspush.com/servlet/com.aclwireless.pushconnectivity.listeners.TextListener?userId=stsalt&pass=stsalt&appid=stsalt&subappid=stsalt&contenttype=1&to=" + MobileNo + "&from=DEMOAW&text=" + Message + "&selfid=true&alert=0&dlrreq=true";

            //Stream data = client.OpenRead(baseurl);
            //StreamReader reader = new StreamReader(data);
            //string ResponseID = reader.ReadToEnd();
            //data.Close();
            //reader.Close();
        }

        private void bindConfirmMail()
        {
            string msgbody = "";
            string ProId = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/product_details.aspx?relproid=" + ViewState["product_id"].ToString();
            string imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + ViewState["thumb_image"].ToString();
            string CustEmail = ViewState["email_id"].ToString();

            //MailMessage mail = new MailMessage();
            //mail.BodyFormat = MailFormat.Html;
            //mail.Priority = MailPriority.High;
            //mail.BodyEncoding = Encoding.UTF8;
            //mail.To = CustEmail.ToString();
            ////mail.Bcc = "dhananjay@shoptosurprise.com, shruti@shoptosurprise.com";
            //mail.Cc = ConfigurationManager.AppSettings["emailCc"];

            //mail.From = CustEmail.ToString();
            //mail.Subject = "Order No " + " " + "S2S" + ViewState["ordId"].ToString() + " " + "Confirmed";

            //msgbody = "<table width='750' border='1' bordercolor='#F2F2F2' cellspacing='0' cellpadding='0' style='border-collapse:collapse; background:#F2F2F2;'  align='center'>";
            //msgbody += "<tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='margin-top:30px;'>";
            //msgbody += "<tr><td width='30%' rowspan='2'><img src='" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/logo_mail.jpg' width='226' height='123' /></td><td width='70%' height='24'>&nbsp;</td></tr>";//logo
            //msgbody += "<tr><td bgcolor='#fd638f' style='font-family:Arial, Helvetica, sans-serif; font-size:18px; font-weight:normal; color:#fff; padding:20px 40px; line-height:28px;'><span style='font-size:30px;'>Hurray! </span><br />Your order #" + ordPrifix + ViewState["ordId"].ToString() + "  has been placed.</td></tr>";
            //msgbody += "</table></td></tr>";

            msgbody += "<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; color:#828282; padding:40px; line-height:22px;'>";
            msgbody += "<span style='font-size:24px; font-weight:normal; color:#fd638f;'>Dear, " + ViewState["customer_name"] + "</span><br /><br />Now this is what we call an excellent choice. A gift crazy enough to share tankers full of love & laughter. Get ready to spread faboulas smiles as we have already started processing your order (No. #" + ordPrifix + ViewState["ordId"].ToString() + ") 	<br /><br /> Take a look at the product you ordered & let us know in case there is any concern: </td></tr>";


            msgbody += "<tr><td bgcolor='#FFFFFF' style='padding:30px;'>";
            msgbody += "<span style='font-family:Arial, Helvetica, sans-serif; font-size:18px; font-weight:normal; text-transform:uppercase; color:#2d2d2d; line-height:22px;'>YOUR " + ViewState["product_name"].ToString() + " IS CONFIRMED</span><br /><br />";

            msgbody += "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table width='100%' border='1' bordercolor='#d3d3d3' cellspacing='0' cellpadding='0' style='border-collapse:collapse;'><tr>";
            msgbody += "<td height='38' bgcolor='#f2f2f2' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>Shipping address</td>";
            msgbody += "<td bgcolor='#f2f2f2' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>Billing address</td>";
            msgbody += "<td bgcolor='#f2f2f2' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>Net order amount</td>";
            msgbody += "</tr><tr>";

            msgbody += "<td width='230' valign='top'  style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:10px; line-height:22px;'>" + ViewState["address"].ToString() + "</td>";
            msgbody += "<td width='230' valign='top'  style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:10px; line-height:22px;'>" + ViewState["address"].ToString() + "</td>";
            msgbody += "<td align='right' valign='top' style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:10px; line-height:22px;'>" + ViewState["sale_price"].ToString() + "</td>";
            msgbody += "</tr></table></td></tr>";

            msgbody += "<tr><td><table width='100%' border='1' bordercolor='#d3d3d3' cellspacing='0' cellpadding='0' style='border-collapse:collapse;'><tr>";

            msgbody += "<td bgcolor='#f2f2f2' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>S.No</td>";
            msgbody += "<td bgcolor='#f2f2f2' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>product description</td>";
            msgbody += "<td height='38' bgcolor='#f2f2f2' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>QTY</td>";
            msgbody += "<td bgcolor='#f2f2f2' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>Price</td>";
            msgbody += "<td width='153' bgcolor='#f2f2f2' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>Amount</td></tr>";

            msgbody += "<tr><td width='55' valign='top' style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:10px; line-height:22px;'>1</td>";
            msgbody += "<td width='302' valign='top' style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:10px; line-height:22px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>";
            msgbody += "<tr><td width='60'><img src='" + imgThumb + " 'width='43' height='89' /></td>";//images here
            msgbody += "<td width='74%'><a href='" + ProId + "' style='color:#fd638f'>" + ViewState["product_name"].ToString() + "<br />" + ViewState["address"].ToString() + "</td>"; // prdouct desc
            msgbody += "</tr></table></td>";

            msgbody += "<td width='88' valign='top' style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:10px; line-height:22px;'>" + ViewState["quantity"].ToString() + "</td>";
            msgbody += "<td width='80' valign='top'  style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:10px; line-height:22px;'>" + ViewState["sale_price"].ToString() + "</td>";
            msgbody += "<td align='right' valign='top' style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:10px; line-height:22px;'>" + ViewState["sale_price"].ToString() + "</td>";
            msgbody += "</tr></table></td></tr></table></td></tr>";

            msgbody += "<tr><td bgcolor='#DADADA' style='padding:10px;' colspan='5'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr>";
            msgbody += "<td width='66%' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>Estimated Delivery Time : 3 - 4  days</td>";
            msgbody += "<td width='34%' style='padding-left:10px; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:normal; text-transform:uppercase; color:#2d2d2d;'>Net order amount : " + ViewState["sale_price"].ToString() + "</td>";
            msgbody += "</tr></table></td></tr>";

            //msgbody += "<tr><td style='font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; color:#828282; padding:40px; line-height:22px;'>";
            //msgbody += "<span style='font-size:16px; font-weight:normal; color:#fd638f;'>From the day you laid eyes on us, to being together today it has been a delightful journey. Come back soon coz we have already started missing you!</span></strong><br />";
            //msgbody += "For any help, feel free to call our customer  care at 9999394508 (10AM-6PM) or email us at <a href='mailto:" + System.Configuration.ConfigurationManager.AppSettings["contactEmail"] + "'>" + System.Configuration.ConfigurationManager.AppSettings["contactEmail"] + "</a><br />";
            //msgbody += "<br /> <span style='font-size:24px; font-weight:normal; color:#fd638f;'> Happy Surprising !! </span> <br />  " + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "</td>";
            //msgbody += "</tr></table>";

            //mail.Body = msgbody;

            //string smtpEmail = "happiness@shoptosurprise.com";
            //string smtpPassword = "vishu0894";
            //string smtpaddress = "smtp.gmail.com";
            //string smtpPort = "465";
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpaddress);
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", smtpPort);
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", smtpEmail);
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", smtpPassword);
            //mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "True");

            //SmtpMail.Send(mail);
            DAL.Utility.sendEmail("STRUTT - Order No " + " " + ordPrifix + ViewState["ordId"].ToString() + " " + "Confirmed", msgbody,
                "Hurray!", "Your order #" + ordPrifix + ViewState["ordId"].ToString() + "  has been placed.", CustEmail, ConfigurationManager.AppSettings["emailCc"]);
        }

        #endregion

        #region Dispatched
        
        private void bindDispatchedMail()
        {
            //string ProId = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/productdetails.aspx?proid=" + ViewState["product_id"].ToString();
            string msgbody = "";
            string imgThumb, ProductUrl;    //= System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/noimage.jpg";
            string CustEmail = ViewState["email_id"].ToString();
            string TrackUrl;
            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();

            ds = orderHandler.get_order_search(Convert.ToInt32(ViewState["ordId"].ToString()), null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                double TotalPrice = Convert.ToDouble(dt.Rows[0]["total_price"].ToString());       // Actual price (Total price + shipping - all discount)
                                                                                                  //double totalDiscount = Convert.ToDouble(dt.Rows[0]["discount_price"].ToString());

                //if (totalDiscount != 0)
                //{
                //    TotalPrice = TotalPrice - totalDiscount;
                //}

                //double ShippingCharge = 0;
                //if (dt.Rows[0]["shipping_price"] != null)
                //{
                //    ShippingCharge = Convert.ToDouble(dt.Rows[0]["shipping_price"]);
                //    TotalPrice = TotalPrice + ShippingCharge;
                //}

                ViewState["manifest_link"] = dt.Rows[0]["manifest_link"].ToString();
                string manifest_link = ViewState["manifest_link"].ToString();
                if (string.IsNullOrEmpty(manifest_link))
                {
                    TrackUrl = BLL.Helpers.GetUrltraking(dt.Rows[0]["ship_via"], dt.Rows[0]["ship_id"], null);
                }
                else
                {
                    TrackUrl = BLL.Helpers.GetUrltraking(dt.Rows[0]["ship_via"], dt.Rows[0]["ship_id"], "Picker");
                }
                msgbody = @"<table style='width: 100 %; border - spacing:0; border - collapse:collapse'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                   <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + dt.Rows[0]["customer_name"].ToString() + @", We are on our way home. That was fast, don't you think? We will be with you in 4-5 days.
                                                  Your order ID is <b>" + ordPrifix + ViewState["ordId"].ToString() + @".</b>
                                                    </p>
                                            <table style='width:100%;border-spacing:0;border-collapse:collapse;margin-top:20px'>
                                                        <tbody>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;line-height:0em'> &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                    <table style='border-spacing:0;border-collapse:collapse;float:left;margin-right:15px'>
                                                                        <tbody>
                                                                            <tr>";
                if (!string.IsNullOrEmpty(TrackUrl))
                {
                    msgbody += @" <td><p style ='color:#555;line-height:150%;font-size:16px;font-weight:600;margin:0 0 0 15px'>You can track us with Track ID: " + dt.Rows[0]["ship_id"].ToString()
                + " <a href='" + TrackUrl + @"' target='_blank'> Click here</a>.</p></td>";
                }
                msgbody += @"</tr>
                                                                        </tbody>
                                                                    </table>
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
                msgbody += @"<table style='width:100%;border-spacing:0;border-collapse:collapse'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <h3 style='font-weight:normal;font-size:20px;margin:0 0 25px'> Order summary </h3>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>";
                msgbody += @"<table style='width:100%;border-spacing:0;border-collapse:collapse'>

              <tbody>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double ItemAmount = Convert.ToDouble(dt.Rows[i]["quantity"]) * Convert.ToDouble(dt.Rows[i]["sale_price"]);
                    imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dt.Rows[i]["thumb_image"].ToString().Replace(" ", "%20");
                    ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dt.Rows[i]["product_id"].ToString();


                    msgbody += @"<tr style ='width:100%;border-top-width:1px;border-top-color:#e5e5e5;border-top-style:solid'>
                                                                 <td style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-top:15px'>
                                                                 <table style='border-spacing:0;border-collapse:collapse'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                                    <img src='" + imgThumb + @"' align='left' width='60' height='60' style='margin-right:15px;border-radius:8px;border:1px solid #e5e5e5' class='CToWUd' />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;width:100%'>
                                                                                    <span style='font-size:16px;font-weight:600;line-height:1.4;color:#555'> " + dt.Rows[i]["product_name"].ToString() + @" * " + dt.Rows[i]["quantity"].ToString() + @"</span><br />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;white-space:nowrap'>
                                                                                    <p style ='color:#777;line-height:150%;font-size:16px;margin:0'>
                                                                        <span style ='font-size:16px'> Rate <strike style ='font-size:16px;color:#555'> Rs. " + (dt.Rows[i]["Price"].ToString() + @"</strike></span>
                                                                    </p>
                                                                                    <p style ='color:#555;line-height:150%;font-size:16px;font-weight:600;margin:0 0 0 15px' align='right'>
                                                                                   Rs." + (Convert.ToInt32(dt.Rows[i]["quantity"]) * Convert.ToSingle(dt.Rows[i]["sale_price"])).ToString() + @"
                                                                                    </p>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>");
                }
                msgbody += @"</tbody>
                                                    </table>";
                msgbody += @"<table style ='width:100%;border-spacing:0;border-collapse:collapse;margin-top:20px;border-top-width:2px;border-top-color:#e5e5e5;border-top-style:solid'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:20px 0 0'>
                                                                                    <p style ='color:#777;line-height:1.2em;font-size:16px;margin:0'>
                                                                                        <span style='font-size:16px'>Total</span>
                                                                                    </p>
                                                                                </td>
                                                                                <td  style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:20px 0 0' align='right'>
                                                                                    <strong style ='font-size:24px;color:#555'>Rs." + TotalPrice.ToString("0.00") + @"</strong>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
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

                msgbody += @"<table>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:35px 0'>
                                <center>
                                    <table style ='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>

                                                    <p style ='color:#999;line-height:150%;font-size:14px;margin:0'> If you have any questions, reply to this email or contact us at<a href='mailto:connect@thestruttstore.com' style='font-size:14px;text-decoration:none;color:#000000' target='_blank'> connect@thestruttstore.com</a></p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";
                DAL.Utility.sendEmail("STRUTT - Order No " + ordPrifix + ViewState["ordId"].ToString() + " Dispatched", msgbody,
                    "We are on our way!", "YOUR ORDER: " + ordPrifix + ViewState["ordId"].ToString() + " HAS BEEN DISPATCHED", CustEmail, ConfigurationManager.AppSettings["emailCc"]);
            }
        }
        #endregion

        #region Deliverd
        private void bindDeliveredMail()
        {
            //string ProId = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/product_details.aspx?relproid=" + ViewState["product_id"].ToString();
            string msgbody = "";
            string imgThumb, ProductUrl;    // = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/noimage.jpg";
            string CustEmail = ViewState["email_id"].ToString();

            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();
            ds = orderHandler.get_order_search(Convert.ToInt32(ViewState["ordId"].ToString()), null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                double TotalPrice = Convert.ToDouble(dt.Rows[0]["total_price"].ToString());       // Actual price (Total price + shipping - all discount)

                //double CouponPrice = Convert.ToDouble(dt.Rows[0]["discount_price"].ToString());
                //if (CouponPrice != 0)
                //    TotalPrice = TotalPrice - CouponPrice;

                //double ShippingCharge = 0;
                //if (dt.Rows[0]["shipping_price"] != null)
                //{
                //    ShippingCharge = Convert.ToDouble(dt.Rows[0]["shipping_price"]);
                //    TotalPrice += ShippingCharge;
                //}

                msgbody = @"<table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-bottom:40px;border:0'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Hi " + dt.Rows[0]["customer_name"].ToString() + @"</p></br>
												    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Aren't you glad we got home safe? We are happy to be home too!</p></br>
                                                    <p style='color:#777;line-height:150%;font-size:16px;margin:0'>Thank you for shopping with us at thestruttstore.com and we hope you are as happy with your purchase as we are servicing your request. Come back soon and shop with us again.</p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>";
                msgbody += @"<table style='width:100%;border-spacing:0;border-collapse:collapse'>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;'>
                                <center>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                    <h3 style='font-weight:normal;font-size:20px;margin:0 0 25px'> Please have a look at your order summary in brief below: </h3>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>";
                msgbody += @"<table style='width:100%;border-spacing:0;border-collapse:collapse'>

              <tbody>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double ItemAmount = Convert.ToDouble(dt.Rows[i]["quantity"]) * Convert.ToDouble(dt.Rows[i]["sale_price"]);
                    imgThumb = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/Product/Thumb/" + dt.Rows[i]["thumb_image"].ToString().Replace(" ", "%20");
                    ProductUrl = System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + @"/productdetails.aspx?proid=" + dt.Rows[i]["product_id"].ToString();


                    msgbody += @"<tr style ='width:100%;border-top-width:1px;border-top-color:#e5e5e5;border-top-style:solid'>
                                                                 <td style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding-top:15px'>
                                                                 <table style='border-spacing:0;border-collapse:collapse'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>
                                                                                    <img src='" + imgThumb + @"' align='left' width='60' height='60' style='margin-right:15px;border-radius:8px;border:1px solid #e5e5e5' class='CToWUd' />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;width:100%'>
                                                                                    <span style='font-size:16px;font-weight:600;line-height:1.4;color:#555'> " + dt.Rows[i]["product_name"].ToString() + @" * " + dt.Rows[i]["quantity"].ToString() + @"</span><br />
                                                                                </td>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;white-space:nowrap'>
                                                                                    <p style ='color:#777;line-height:150%;font-size:16px;margin:0'>
                                                                        <span style ='font-size:16px'> Rate <strike style ='font-size:16px;color:#555'> Rs. " + (dt.Rows[i]["Price"].ToString() + @"</strike></span>
                                                                    </p>
                                                                                    <p style ='color:#555;line-height:150%;font-size:16px;font-weight:600;margin:0 0 0 15px' align='right'>
                                                                                   Rs." + (Convert.ToInt32(dt.Rows[i]["quantity"]) * Convert.ToSingle(dt.Rows[i]["sale_price"])).ToString() + @"
                                                                                    </p>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>");
                }
                msgbody += @"</tbody>
                                                    </table>";
                msgbody += @"<table style ='width:100%;border-spacing:0;border-collapse:collapse;margin-top:20px;border-top-width:2px;border-top-color:#e5e5e5;border-top-style:solid'>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:20px 0 0'>
                                                                                    <p style ='color:#777;line-height:1.2em;font-size:16px;margin:0'>
                                                                                        <span style='font-size:16px'>Grand Total</span>
                                                                                    </p>
                                                                                </td>
                                                                                <td  style ='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:20px 0 0' align='right'>
                                                                                    <strong style ='font-size:24px;color:#555'>Rs." + TotalPrice.ToString("0.00") + @"</strong>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
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
                msgbody += @"<table>
                    <tbody>
                        <tr>
                            <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif;padding:35px 0'>
                                <center>
                                    <table style ='width:560px;text-align:left;border-spacing:0;border-collapse:collapse;margin:0 auto'>
                                        <tbody>
                                            <tr>
                                                <td style='font-family:-apple-system,BlinkMacSystemFont,&quot;Segoe UI&quot;,&quot;Roboto&quot;,&quot;Oxygen&quot;,&quot;Ubuntu&quot;,&quot;Cantarell&quot;,&quot;Fira Sans&quot;,&quot;Droid Sans&quot;,&quot;Helvetica Neue&quot;,sans-serif'>

                                                    <p style ='color:#999;line-height:150%;font-size:14px;margin:0'> If you have any questions, reply to this email or contact us at<a href='mailto:connect@thestruttstore.com' style='font-size:14px;text-decoration:none;color:#000000' target='_blank'> connect@thestruttstore.com</a></p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>
               </td>
           </tr>
       </tbody>
   </table>";
            }

            DAL.Utility.sendEmail("STRUTT - Order No " + ordPrifix + ViewState["ordId"].ToString() + " " + "Delivered", msgbody,
                "FABULOUS!", "YOUR ORDER: " + ordPrifix + ViewState["ordId"].ToString() + " HAS BEEN DELIVERED", CustEmail, ConfigurationManager.AppSettings["emailCc"]);
        }
        #endregion

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindOrderStatus();
        }
        /// <summary>
        /// Update ds = orderHandler.get_order_status(Fromdate, Todate, Phoneno, Customername, Statusorder, null, 0,true);
        /// to ds = orderHandler.get_order_status(Fromdate, Todate, Phoneno, Customername, Statusorder, null, 4,true);
        /// by Hetal Patel on 04-03-2020
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DateTime? Fromdate = null;
            DateTime? Todate = null;
            string Statusorder = null;
            string payment = null , payment2 = null;
            string Customername = null;
            string Phoneno = null;
            if (ddlDateRange.SelectedValue.Equals("0"))
            {
                if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                {
                    Fromdate = Convert.ToDateTime(txtfromdate.Text);
                    Todate = Convert.ToDateTime(txttodate.Text + " 23:59:59");
                }
            }
            else
            {
                string dateRange = ddlDateRange.SelectedValue;
                Fromdate = Convert.ToDateTime(dateRange.Split('|').GetValue(0).ToString());
                Todate = Convert.ToDateTime(dateRange.Split('|').GetValue(1) + " 23:59:59");
            }
            if (!string.IsNullOrEmpty(txtSearchcustomername.Text))
                Customername = (txtSearchcustomername.Text);
            if (!string.IsNullOrEmpty(txtSerachphoneno.Text))
                Phoneno = (txtSerachphoneno.Text);
            if (ddlStatusOrder.SelectedItem.Text != "All Status")
                Statusorder = ddlStatusOrder.SelectedItem.Text;
            if (ddlPaymentType.SelectedItem.Text == "COD")
            {
                payment = ddlPaymentType.SelectedItem.Text;
            }
            else if (ddlPaymentType.SelectedItem.Text == "Online")
            {
                payment2 = ddlPaymentType.SelectedItem.Text;
            }
            order_handler orderHandler = new order_handler();
            DataSet ds = new DataSet();
            ds = orderHandler.get_order_export(Fromdate, Todate, Phoneno, Customername, Statusorder,payment , payment2 , null, true);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                this.ExportDataTable(ds.Tables[0]);
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            // txtorderdate.Text = string.Empty;
            lblMsg.Visible = false;
            this.bindOrderStatus();
        }

        private void ExportDataTable(DataTable dt)
        {
            string lsFileName = "Order_" + DateTime.Now.ToString("dd-MMM-yyyy-hhmmss");

            string attachment = "attachment; filename=" + lsFileName + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        protected void btnshipVia_Click(object sender ,EventArgs e)
        {
            string proname = "";
            int Flag = 0;
            Decimal TotalPrice = 0;
            order_handler orderHandler = new order_handler();
            string[] strArray = hdnOrderId.Value.Split('|');

            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = strArray[i].Trim();
                ordId = Convert.ToInt32(strArray[i]);

                string paymentType = "";

                if (ordId != 0)
                {
                    ViewState["ordId"] = ordId;

                    DataSet ds = new DataSet();
                    ds = orderHandler.get_order_search(Convert.ToInt32(ViewState["ordId"].ToString()), null);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                Decimal Price = Convert.ToDecimal(dt.Rows[j]["sale_price"].ToString());
                                Decimal UnitPrice = Convert.ToInt64(dt.Rows[j]["quantity"].ToString()) * Price;

                                TotalPrice += UnitPrice;
                                Session["totAmount"] = TotalPrice.ToString();
                                proname += dt.Rows[j]["product_name"].ToString() + " and ";
                            }

                            proname = proname.Substring(0, proname.Length - 4);


                            ViewState["product_id"] = dt.Rows[0]["product_id"].ToString();
                            ViewState["customer_name"] = dt.Rows[0]["customer_name"].ToString();
                            ViewState["email_id"] = dt.Rows[0]["email_id"].ToString();
                            ViewState["address"] = dt.Rows[0]["address"].ToString() + " " + dt.Rows[0]["city"].ToString() + " " + dt.Rows[0]["state"].ToString() + " " + dt.Rows[0]["pin_code"].ToString();
                            ViewState["quantity"] = dt.Rows[0]["quantity"].ToString();
                            ViewState["sale_price"] = dt.Rows[0]["sale_price"].ToString();
                            ViewState["thumb_image"] = dt.Rows[0]["thumb_image"].ToString();
                            //ViewState["product_name"] = dt.Rows[0]["product_name"].ToString();

                            ViewState["product_name"] = proname.ToString();

                            ViewState["contact_number"] = dt.Rows[0]["contact_number"].ToString();

                            paymentType = dt.Rows[0]["payment_through"].ToString();
                            ViewState["manifest_link"] = dt.Rows[0]["manifest_link"].ToString();
                            ViewState["order_status"] = dt.Rows[0]["order_status"].ToString();
                            ViewState["ship_id"] = dt.Rows[0]["ship_id"].ToString();
                        }
                    }
                }

                //string OrderStatus = ViewState["order_status"];
                string ShipVia = ddlshipvia.SelectedItem.Text;
                //ViewState["ship_id"] = txtEditShipId.Text;

                string pickkrmanifest = ViewState["manifest_link"].ToString();
                string pickkrorderstatus = ViewState["order_status"].ToString();
                string ship_id = ViewState["ship_id"].ToString();
                PickerApiService pikrapi = new PickerApiService();
                //if (OrderStatus == "Cancelled")
                //{
                //    if (!string.IsNullOrEmpty(pickkrmanifest))
                //    {
                //        if (pickkrorderstatus != "Dispatched")
                //        {
                //            bool cancelpikkr = pikrapi.cancelorder_api(ship_id);
                //        }
                //    }
                //}
                order Order = new order();

                Order.order_id = ordId;
                Order.ship_id = ViewState["ship_id"].ToString();
                Order.order_status = ViewState["order_status"].ToString();
                Order.ship_via = ShipVia;
                Order.Flag = Flag;

                bool result = orderHandler.update_order_status(Order);
                if (result)
                {
                    lblMsg.Text = "Updated Successfully.";
                    grdOrderStatus.EditIndex = -1;
                    bindOrderStatus();

                    //if (OrderStatus == "Dispatched")
                    //{
                    //    this.bindDispatchedMail();
                    //    Utility.SendSmsOrderDispatch(ViewState["contact_number"].ToString(), ViewState["customer_name"].ToString(), ViewState["ordId"].ToString(), ViewState["ship_id"].ToString(), ViewState["ordId"].ToString());
                    //}
                    //else if (OrderStatus == "Delivered")
                    //{
                    //    this.bindDeliveredMail();
                    //    Utility.SendSmsOrderConfirmation(ViewState["contact_number"].ToString(), ViewState["customer_name"].ToString(), ViewState["ordId"].ToString());
                    //}
                }
            }
        }

        /// <summary>
        /// Added by Hetal Patel on 06-03-202 for save multiple orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave1_Click(object sender, EventArgs e)
        {
            string proname = "";
            int Flag = 0;
            Decimal TotalPrice = 0;
            order_handler orderHandler = new order_handler();
            string[] strArray = hdnOrderId.Value.Split('|');

            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = strArray[i].Trim();
                ordId = Convert.ToInt32(strArray[i]);

                string paymentType = "";

                if (ordId != 0)
                {
                    ViewState["ordId"] = ordId;

                    DataSet ds = new DataSet();
                    ds = orderHandler.get_order_search(Convert.ToInt32(ViewState["ordId"].ToString()), null);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                Decimal Price = Convert.ToDecimal(dt.Rows[j]["sale_price"].ToString());
                                Decimal UnitPrice = Convert.ToInt64(dt.Rows[j]["quantity"].ToString()) * Price;

                                TotalPrice += UnitPrice;
                                Session["totAmount"] = TotalPrice.ToString();
                                proname += dt.Rows[j]["product_name"].ToString() + " and ";
                            }

                            proname = proname.Substring(0, proname.Length - 4);


                            ViewState["product_id"] = dt.Rows[0]["product_id"].ToString();
                            ViewState["customer_name"] = dt.Rows[0]["customer_name"].ToString();
                            ViewState["email_id"] = dt.Rows[0]["email_id"].ToString();
                            ViewState["address"] = dt.Rows[0]["address"].ToString() + " " + dt.Rows[0]["city"].ToString() + " " + dt.Rows[0]["state"].ToString() + " " + dt.Rows[0]["pin_code"].ToString();
                            ViewState["quantity"] = dt.Rows[0]["quantity"].ToString();
                            ViewState["sale_price"] = dt.Rows[0]["sale_price"].ToString();
                            ViewState["thumb_image"] = dt.Rows[0]["thumb_image"].ToString();
                            //ViewState["product_name"] = dt.Rows[0]["product_name"].ToString();

                            ViewState["product_name"] = proname.ToString();

                            ViewState["contact_number"] = dt.Rows[0]["contact_number"].ToString();

                            paymentType = dt.Rows[0]["payment_through"].ToString();
                            ViewState["manifest_link"] = dt.Rows[0]["manifest_link"].ToString();
                            ViewState["order_status"] = dt.Rows[0]["order_status"].ToString();
                            ViewState["ship_id"] = dt.Rows[0]["ship_id"].ToString();
                            ViewState["ShipVia"] = dt.Rows[0]["ship_via"].ToString();
                        }
                    }
                }

                string OrderStatus = ddorderstatus.SelectedItem.Text;
                //string ShipVia = ddlshipvia.SelectedItem.Text;
                //ViewState["ship_id"] = txtEditShipId.Text;

                if (OrderStatus == "inprogress")
                {
                    Flag = 0;
                }
                else if (OrderStatus == "Confirmed")
                {
                    success pageSuccess = new success();
                    pageSuccess.SendEmailConfirmOrder(paymentType, ordId);

                    Flag = 1;
                }
                else if (OrderStatus == "Packed")
                {
                    this.bindSmsPacked();
                    Flag = 2;
                }
                else if (OrderStatus == "Scheduled")
                {
                    Flag = 8;
                }
                else if (OrderStatus == "Dispatched")
                {
                    Flag = 3;
                }
                else if (OrderStatus == "Delivered")
                {
                    Flag = 4;
                }
                else if (OrderStatus == "Failed")
                {
                    Flag = 5;
                }
                else if (OrderStatus == "RTO")
                {
                    Flag = 7;
                }

                string pickkrmanifest = ViewState["manifest_link"].ToString();
                string pickkrorderstatus = ViewState["order_status"].ToString();
                string ship_id = ViewState["ship_id"].ToString();
                PickerApiService pikrapi = new PickerApiService();
                if (OrderStatus == "Cancelled")
                {
                    if (!string.IsNullOrEmpty(pickkrmanifest))
                    {
                        if (pickkrorderstatus != "Dispatched")
                        {
                            bool cancelpikkr = pikrapi.cancelorder_api(ship_id);
                        }
                    }
                }
                order Order = new order();

                Order.order_id = ordId;
                Order.ship_id = ViewState["ship_id"].ToString();
                Order.order_status = OrderStatus;
                Order.ship_via = ViewState["ShipVia"].ToString();
                Order.Flag = Flag;

                bool result = orderHandler.update_order_status(Order);
                if (result)
                {
                    lblMsg.Text = "Updated Successfully.";
                    grdOrderStatus.EditIndex = -1;
                    bindOrderStatus();

                    if (OrderStatus == "Dispatched")
                    {
                        this.bindDispatchedMail();
                        Utility.SendSmsOrderDispatch(ViewState["contact_number"].ToString(), ViewState["customer_name"].ToString(), ViewState["ordId"].ToString(), ViewState["ship_id"].ToString(), ViewState["ordId"].ToString());
                    }
                    else if (OrderStatus == "Delivered")
                    {
                        this.bindDeliveredMail();
                        Utility.SendSmsOrderConfirmation(ViewState["contact_number"].ToString(), ViewState["customer_name"].ToString(), ViewState["ordId"].ToString());
                    }
                }
            }

        }

        /// <summary>
        /// Added by Hetal Patel on 06-03-202 for Edit multiple orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strIDs = string.Empty;

            foreach (GridViewRow row in grdOrderStatus.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("chkselect");

                if (cb.Checked)
                {
                    ordId = Convert.ToInt32(grdOrderStatus.DataKeys[row.RowIndex].Values["order_id"].ToString());
                    strIDs += ordId + "|";
                    hdnOrderId.Value = strIDs.Remove(strIDs.LastIndexOf('|'));
                    this.mp2.Show();
                }

            }
            if (String.IsNullOrEmpty(hdnOrderId.Value))
            {
                string str = "Please select any order!";
                Response.Write("<script language=javascript>alert('" + str + "');</script>");
            }

        }

        /// <summary>
        /// Added by Hetal Patel on 07-03-202 for Delete multiple orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strIDs = string.Empty;
            order_handler orderHandler = new order_handler();

            foreach (GridViewRow row in grdOrderStatus.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("chkselect");

                if (cb.Checked)
                {
                    ordId = Convert.ToInt32(grdOrderStatus.DataKeys[row.RowIndex].Values["order_id"].ToString());
                    strIDs += ordId + "|";
                    hdndeleteId.Value = strIDs.Remove(strIDs.LastIndexOf('|'));

                    this.mp3.Show();
                }

            }
            if (String.IsNullOrEmpty(hdndeleteId.Value))
            {
                string str = "Please select any order!";
                Response.Write("<script language=javascript>alert('" + str + "');</script>");
            }
        }

        /// <summary>
        /// Added by Hetal Patel on 07-03-202 for Delete multiple orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnYes_Click(object sender, EventArgs e)
        {

            order_handler orderHandler = new order_handler();

            string[] strArray = hdndeleteId.Value.Split('|');

            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = strArray[i].Trim();
                ordId = Convert.ToInt32(strArray[i]);
                bool delete = orderHandler.pr_delete_order(ordId);
                if (delete)
                {
                    this.bindOrderStatus();

                }
            }
            string str = "Order deleted Successfully!";
            Response.Write("<script language=javascript>alert('" + str + "');</script>");
        }


        //by divyesh gandhi
        private void fillMonthRange()
        {
            

            DateTime firstDate;
            DateTime secondDate; 
            //ddlDateRange.Items.Add("select date range");

            firstDate = DateTime.Today;
            secondDate = DateTime.Now;
            ddlDateRange.Items.Add(new ListItem("Today (" + firstDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            firstDate = DateTime.Today.AddDays(-1);
            secondDate = DateTime.Today.AddDays(-1);
            ddlDateRange.Items.Add(new ListItem("Yesterday (" + firstDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            //for current week
            
            if (DateTime.Today.ToString("ddd") == "Sun")
            {
                firstDate = DateTime.Now;
                secondDate = firstDate.AddDays (6);
            }
            else
            {
                firstDate = DateTime.Today.AddDays((-1 * (int)(DateTime.Today.DayOfWeek)));
                secondDate = firstDate.AddDays(6);
            }
            
            ddlDateRange.Items.Add(new ListItem("Current Week (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));


            secondDate = DateTime.Today.AddDays((-1 * (int)(DateTime.Today.DayOfWeek)));
            firstDate = secondDate.AddDays(-6);
            ddlDateRange.Items.Add(new ListItem("Last Week (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));


            firstDate = new DateTime(secondDate.Year, secondDate.Month, 1);
            secondDate = DateTime.Now;
            ddlDateRange.Items.Add(new ListItem("Current Month (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            firstDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1);
            secondDate = new DateTime(firstDate.Year, firstDate.Month, DateTime.DaysInMonth(firstDate.Year, firstDate.Month));
            ddlDateRange.Items.Add(new ListItem("Last Month (" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            firstDate = DateTime.Now + TimeSpan.FromDays(-30);
            secondDate = DateTime.Now;
            ddlDateRange.Items.Add(new ListItem("Last 30 Days(" + firstDate.ToString("dd/MM/yy") + " - " + secondDate.ToString("dd/MM/yy") + ")",
                firstDate.ToString("yyyy/MM/dd") + "|" + secondDate.ToString("yyyy/MM/dd")));

            ListItem lst = new ListItem("Custom Data Range", "0");
            ddlDateRange.Items.Insert(ddlDateRange.Items.Count - 0, lst);

        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateRange.SelectedValue == "0")
            {
                pnl_customMonth.Visible = true;
                txtfromdate.Visible = true;
                txttodate.Visible = true;
            }

            else
            {
                pnl_customMonth.Visible = false;
            }
        }
    }
}
