using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Configuration;

namespace strutt
{
    public partial class cart : System.Web.UI.Page
    {
        string fxCountCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Testing purpose. To write log in text file
            UtilityLog.LogFile = Server.MapPath("~/App_Data/log.txt");
            lblError.Visible = false;

            if (Session["CustomerLoginDetails"] != null)
            {
                if (Session["CutomerName"] != null)
                {
                    lblLoginName.Text = string.Format("Welcome, {0}", Session["CutomerName"].ToString());
                }
            }
            //else
            //{
            //    string url = HttpContext.Current.Request.Url.PathAndQuery;
            //    Response.Redirect("Login.aspx?url=" + url);
            //}
            if (!IsPostBack)
            {
                if (Request.QueryString["proid"] != null)
                {
                    short status = Common.AddToShoppingCart(Convert.ToInt64(Request.QueryString["proid"].ToString()));
                    if (status == 1)
                    {
                        lblError.Text = "Unexptected error during add to cart. Please contact STRUTT team.";
                        lblError.Visible = true;
                        return;
                    }
                    else if (status == 2)
                    {
                        lblError.Text = "Your selected item is out of stock. Please try later.";
                        lblError.Visible = true;
                        return;
                    }
                }
                if (Request.UrlReferrer == null)
                {
                    //Response.Redirect("~/Login.aspx");
                    Response.Redirect("/");
                }
                getText();
            }
            Page.LoadComplete += new EventHandler(Page_LoadComplete);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["couponCodeName"] != null)
            {
                txtCouponCode.Text = Session["couponCodeName"].ToString();
                lbtnApply_Click(lbtnApply, null);
            }
            UpdateShoppingCart();
            this.Wigzoaddtocart();
        }

        #region Cart

        protected void dlCart_ItemDataCommond(object source, RepeaterCommandEventArgs e)
        {
            int qty = 1;
            int txtqty = 1;
            string proname = "";
            lblQtyMsg.Visible = false;

            if (e.CommandName == "add" || e.CommandName == "minus")
            {
                TextBox txtQty = (TextBox)e.Item.FindControl("txtquantity");
                HiddenField hfMenuName = e.Item.FindControl("hfMenuName") as HiddenField;
                //if (hfMenuName.Value == "Bulk Gifting")
                //{
                //    if (e.CommandName == "add")
                //        txtQty.Text = (Convert.ToInt16(txtQty.Text) + 1).ToString();
                //    else
                //        txtQty.Text = (Convert.ToInt16(txtQty.Text) - 1).ToString();
                //}
                //else
                //{
                //    if (e.CommandName == "add")
                //        txtQty.Text = (Convert.ToInt16(txtQty.Text) + 1).ToString();
                //    else
                //        txtQty.Text = (Convert.ToInt16(txtQty.Text) - 1).ToString();
                //}

                if (e.CommandName == "add")
                {
                    txtQty.Text = (Convert.ToInt16(txtQty.Text) + 1).ToString();
                }
                else
                {
                    txtQty.Text = (Convert.ToInt16(txtQty.Text) - 1).ToString();
                }
                //ViewState["Qty"] = txtQty.Text;

                if (Convert.ToInt32(txtQty.Text) <= 0)
                {
                    txtQty.Text = "1";
                    lblQtyMsg.Text = "Hi, Quantity value must be more then zero.";
                    lblQtyMsg.Visible = true;
                    return;
                }

                int ProductID = Convert.ToInt32(e.CommandArgument);
                DataTable dtCart = (DataTable)Session["Cart"];
                foreach (DataRow row in dtCart.Rows)
                {
                    Session["qty"] = Convert.ToInt32(txtQty.Text);
                    if (int.Parse(row["product_id"].ToString()) == ProductID)
                    {

                        row["quantity"] = Convert.ToInt32(txtQty.Text);

                        Decimal Price = Convert.ToDecimal(row["sale_price"]);
                        Decimal discount = Convert.ToDecimal(row["discount"]);
                        Decimal UnitPriceOnDiscount = discount > 0 ? Price - (Price * discount / 100) : Price;
                        product_handler productsHandler = new product_handler();
                        DataSet dsqty = productsHandler.get_search_quantity(ProductID);
                        if (dsqty != null && dsqty.Tables.Count > 0)
                        {
                            DataTable dtqty = dsqty.Tables[0];
                            if (dtqty.Rows.Count > 0)
                            {
                                qty = Convert.ToInt32(dtqty.Rows[0]["quantity"].ToString());
                                txtqty = Convert.ToInt32(txtQty.Text);
                                proname = dtqty.Rows[0]["product_name"].ToString();

                                if (qty >= txtqty)
                                {
                                    row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
                                    row["custom_bag_price"] = Convert.ToInt32(row["quantity"]) * Convert.ToSingle(row["custom_bag_price"]);
                                    //row["custom_bag_price"] = txtqty * 250;
                                    row["x_point"] = row["x_point"] == DBNull.Value ? 0 : Convert.ToSingle(row["x_point"]);
                                    row["y_point"] = row["y_point"] == DBNull.Value ? 0 : Convert.ToSingle(row["y_point"]);
                                    row["thumb_image"] = row["thumb_image"];
                                    Session["Cart"] = dtCart;
                                    //UpdateShoppingCart();
                                    lblQtyMsg.Text = "";
                                    break;
                                }
                                else
                                {
                                    if (qty < 0)
                                        row["quantity"] = 1;
                                    else
                                        row["quantity"] = qty;
                                    row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
                                    //row["custom_bag_price"] = Convert.ToInt32(row["quantity"]) * Convert.ToSingle(row["custom_bag_price"]);
                                    row["custom_bag_price"] = txtqty * 250;
                                    row["x_point"] = row["x_point"] == DBNull.Value ? 0 : Convert.ToSingle(row["x_point"]);
                                    row["y_point"] = row["y_point"] == DBNull.Value ? 0 : Convert.ToSingle(row["y_point"]);
                                    row["thumb_image"] = row["thumb_image"];
                                    Session["Cart"] = dtCart;
                                    //UpdateShoppingCart();
                                    lblQtyMsg.Text = "Hi, only <b>" + qty + "</b> quantity of <b>" + proname + "</b> is available at this time.";
                                    lblQtyMsg.Visible = true;
                                    break;
                                }
                            }
                            else
                            {

                            }
                        }
                        UpdateShoppingCart();
                    }
                }

            }
            else if (e.CommandName == "Remove")
            {
                if (Session["Cart"] != null)
                {
                    DataTable dt = (DataTable)Session["Cart"];
                    if (dt.Rows.Count > 0)
                        dt.Rows.RemoveAt(e.Item.ItemIndex);

                    Session["Cart"] = dt;
                    UpdateShoppingCart();
                }
            }

            //divCart.Attributes.Add("class", "speed-in");
        }

        protected void dlCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //HiddenField hfMenuName = e.Item.FindControl("hfMenuName") as HiddenField;
            //TextBox txtquantity = (TextBox)e.Item.FindControl("txtquantity");
            //LinkButton btnMin = (LinkButton)e.Item.FindControl("btnMin");
            //LinkButton btnAdd = (LinkButton)e.Item.FindControl("btnAdd");

            //if (hfMenuName.Value == "Bulk Gifting")
            //{
            //    txtquantity.Visible = false;
            //    btnMin.Visible = false;
            //    btnAdd.Visible = false;
            //}
            //else 
            //{
            //    txtquantity.Visible = true;
            //    btnMin.Visible = true;
            //    btnAdd.Visible = true;
            //}
            

            HiddenField hf = e.Item.FindControl("hfImgUrl") as HiddenField;
            if (hf != null)
            {
                string val = hf.Value;
                Image img = e.Item.FindControl("Image1") as Image;
                img.ImageUrl = "~/images/Product/Thumb/" + val.Substring(val.LastIndexOf('/'));
                //img.ImageUrl = "~/image" + val + ".jpg";
            }
        }

        private void UpdateShoppingCart()
        {
            string ProductIds = "";
            DataTable dtCart = (DataTable)Session["Cart"];
            if (dtCart != null && dtCart.Rows.Count > 0)
            {
                ViewState["proId"] = dtCart.Rows[0]["product_id"].ToString();
                //ViewState["proName"] = dtCart.Rows[0]["product_name"].ToString();
                //ViewState["proQty"] = dtCart.Rows[0]["quantity"].ToString();
                ViewState["salPrice"] = dtCart.Rows[0]["sale_price"].ToString();
                //ViewState["thumbImg"] = dtCart.Rows[0]["thumb_image"].ToString();

                lblCartCount.Text = dtCart.Rows.Count.ToString();



                ////SubTotal Amount
                //Decimal amount = (from DataRow drCart in dtCart.AsEnumerable()
                //                  where drCart.RowState != DataRowState.Deleted
                //                  select (Convert.ToDecimal(drCart["Total"]))).Sum();



                //if (amount > 750)
                //    lblShipping.Text = "0.00";
                //else
                //    lblShipping.Text = String.Format("{0:0.00}", System.Configuration.ConfigurationManager.AppSettings["shippingcharge"]);

                //Decimal totalAmount = (from DataRow drCart in dtCart.AsEnumerable()
                //                       where drCart.RowState != DataRowState.Deleted
                //                       select (Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();
                //lblTotalPrice.Text = String.Format("{0:0.00}", totalAmount);

                //decimal totalDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                //                         where drCart.RowState != DataRowState.Deleted
                //                         select ((Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["discount"]) / 100) * Convert.ToDecimal(drCart["quantity"]))).Sum();
                //// totalDiscount = totalDiscount + CouponAmount;
                //// lblDiscount.Text = String.Format("{0:0.00}", totalDiscount);
                //ViewState["Discount"] = totalDiscount;
                //ViewState["ShippingCharge"] = lblShipping.Text;

                //decimal totalCustomeBagCharge = (from DataRow drCart in dtCart.AsEnumerable()
                //                                 where drCart.RowState != DataRowState.Deleted
                //                                 select (Convert.ToDecimal(drCart["custom_bag_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();

                //Decimal grandTotal = totalAmount - totalDiscount + Convert.ToDecimal(lblShipping.Text) + totalCustomeBagCharge;
                //lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
                //ViewState["grdTotal"] = lblGrandTotal.Text.ToString();

                //string mnName = dtCart.Rows[0]["menu_name"].ToString();
                string mnName = "";
                int QtyBlk = 0;
                decimal slPrice;
                foreach (DataRow row in dtCart.Rows)
                {
                    mnName = row["menu_name"].ToString();
                    slPrice = Convert.ToDecimal(row["sale_price"].ToString());

                    if (mnName == "Bulk Gifting")
                    {
                        if (Session["Qty_Bulk"] != null)
                        {
                            //row["quantity"] = "1";
                        }
                        else
                        {
                            Session["Qty_Bulk"] = "5";
                            QtyBlk = 5;
                            // ViewState["Qty_Bulk"] = "5";
                            row["quantity"] = "5";
                            row["total"] = slPrice * QtyBlk;
                        }
                    }
                    else
                    {
                        //row["quantity"] = "1";
                    }
                }


                Decimal totalAmount = (from DataRow drCart in dtCart.AsEnumerable()
                                       where drCart.RowState != DataRowState.Deleted
                                       select (Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();
                lblTotalPrice.Text = String.Format("{0:0.00}", totalAmount);

                decimal salesPrice = (from DataRow drCart in dtCart.AsEnumerable()
                                      where drCart.RowState != DataRowState.Deleted
                                      select (Convert.ToDecimal(drCart["sale_price"]))).Sum();

                decimal DiscountPer = 0;

                //// We will implement in future. Some category exclude from Qty based discount
                //string ExcludeCategory = ConfigurationManager.AppSettings["excludeCategoryFromDiscount"];
                //if (ExcludeCategory.Split(',').Where(r => r == "2").ToList().Count == 0)
                //{
                //}
                //Commnet becaouse client said: Kalpesh-29-May-2020
                //// On Product get 20 % and 30 % offer for 3/4 Days
                //if (TotalQty == 2)
                //{
                //    DiscountPer = Convert.ToDecimal(ConfigurationManager.AppSettings["disconton2qty"]);
                //    lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                //    lblMsgCoupon.Text = "Congratulations ! You get " + DiscountPer.ToString() + "% Discount.";
                //    txtCouponCode.Enabled = false;
                //    lbtnApply.Enabled = false;
                //}
                //else if (TotalQty > 2)
                //{
                //    DiscountPer = Convert.ToDecimal(ConfigurationManager.AppSettings["disconton3qty"]);
                //    lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                //    lblMsgCoupon.Text = "Congratulations ! You get " + DiscountPer.ToString() + "% Discount.";
                //    txtCouponCode.Enabled = false;
                //    lbtnApply.Enabled = false;
                //}

                decimal TotalDiscount = 0;
                //if (TotalQty == 1)
                //{
                decimal FlatDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                                        where drCart.RowState != DataRowState.Deleted
                                        select ((Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["discount"]) / 100) * Convert.ToDecimal(drCart["quantity"]))).Sum();

               
               decimal couponDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                                              where drCart.RowState != DataRowState.Deleted
                                              select (Convert.ToDecimal(drCart["coupon_discount"]))).Sum();
                
                // TotalDiscount = couponDiscount;
                TotalDiscount = FlatDiscount + couponDiscount;
                //  lblQtyCount.Text = TotalQty.ToString();
                //}
                //else
                //{
                //    TotalDiscount = (totalAmount * DiscountPer) / 100;
                //}
                ViewState["salPrice"] = salesPrice;

                lblDiscount.Text = String.Format("{0:0.00}", TotalDiscount);
                Session["CouponAmount"] = TotalDiscount;


                decimal totalCustomeBagCharge = (from DataRow drCart in dtCart.AsEnumerable()
                                                 where drCart.RowState != DataRowState.Deleted
                                                 select (Convert.ToDecimal(drCart["custom_bag_price"]))).Sum();
                // lblCustomBagCharge.Text = String.Format("{0:0.00}", totalCustomeBagCharge);

                if (totalAmount - TotalDiscount > 750)
                    lblShipping.Text = "0.00";
                else
                    lblShipping.Text = String.Format("{0:0.00}", Convert.ToDecimal(ConfigurationManager.AppSettings["shippingcharge"]));


                ViewState["ShippingCharge"] = lblShipping.Text;


                Decimal grandTotal = totalAmount - TotalDiscount + Convert.ToDecimal(lblShipping.Text) + totalCustomeBagCharge;


                lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);
                Session["grandTotal"] = grandTotal;

                //btnPlaceOrder.Text = String.Format("Pay Rs.{0:0.00}", grandTotal);
                Session["grdTotal"] = lblGrandTotal.Text.ToString();
                Session["grandTotal"] = lblGrandTotal.Text.ToString();
                //Session["CouponAmount"] = TotalDiscount;

                int product_id = Convert.ToInt32(ViewState["proId"].ToString());

                foreach (DataRow row in dtCart.Rows)
                {
                    ProductIds += "'" + row["product_id"] + "',";
                    if (int.Parse(row["product_id"].ToString()) == product_id)
                    {
                        row["shipping_price"] = ViewState["ShippingCharge"].ToString();
                    }
                }

                rptCartPc.DataSource = dtCart;
                rptCartPc.DataBind();

                string FacebookPixelCode = @"
                fbq('track', 'AddToCart', {
                content_ids: [" + ProductIds.Remove(ProductIds.Length - 1) + @"],
                content_type: 'product',
                value: " + lblGrandTotal.Text + @",
                currency: 'INR'
                });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "FacebookPixelCode", FacebookPixelCode, true);

            }
            else if (dtCart != null && dtCart.Rows.Count == 0)
            {
                lblQtyMsg.Text = "Cart empty. Please click on Continue Shopping to add items.";
                lblQtyMsg.Visible = true;

                rptCartPc.DataSource = dtCart;
                rptCartPc.DataBind();

                lblTotalPrice.Text = "0.00";
                lblShipping.Text = "0.00";
                // lblDiscount.Text = "0";
                lblGrandTotal.Text = "0.00";
                Session["couponcodeprice"] = null;
                Session["couponCodeName"] = null;
            }

        }


        #endregion

        #region Xpresslane

        protected void btnXpresslane_Click(object sender, EventArgs e)
        {
            UtilityLog.WriteToLog("btnXpresslane_Click");

            if (InsertOrder("Xpresslane"))
            {
                UtilityLog.WriteToLog("btnXpresslane_Click  ___1");

                Session["XpressLanemerchantcarturl"] = HttpContext.Current.Request.RawUrl;


                UtilityLog.WriteToLog("btnXpresslane_Click  ___2");

                Response.Redirect("TestXpressLean.aspx");

                UtilityLog.WriteToLog("btnXpresslane_Click  ___3");
            }


        }
        //Insert Order for Xpresslane
        private bool InsertOrder(string PaymentType)
        {
            int qty = 0;
            int txtqty = 1;
            int qtycount = 0;

            try
            {

                string generatecode = Utility.generatecode();
                customer_handler customerHandler = new customer_handler();
                Guid customer_id = Guid.NewGuid();

                UtilityLog.WriteToLog("InsertOrder ______1");

                DataSet ds = customerHandler.get_customer_login_details(ConfigurationManager.AppSettings["XpressLaneuseremailid"].ToString());

                if (ds != null && ds.Tables.Count > 0)
                {
                    customer_id = Guid.Parse(ds.Tables[0].Rows[0]["customer_id"].ToString());
                    Session["CustomerLoginDetails"] = ds.Tables[0].Rows[0]["email_id"].ToString();
                }

                order_handler orderHandler = new order_handler();
                BusinessEntities.order Order = new BusinessEntities.order();
                Order.order_number = Guid.NewGuid();
                //Order.order_number = customer_id;
                Order.customer_id = customer_id;
                UtilityLog.WriteToLog("InsertOrder ______2");

                Order.user_name = "";
                Order.contact_number = "";
                Order.email_id = "";
                Order.address = "";
                Order.land_mark = "";
                Order.city = "";
                Order.state = "";
                Order.pin_code = "";
                Order.message = "";
                Order.PaymentThrough = PaymentType;
                Order.discount_price = "0";     // Include only Coupon discount
                Order.coupon_code = "";

                if (Session["CouponAmount"] != null)
                {
                    Order.discount_price = Session["CouponAmount"].ToString();
                }


                if (Session["couponCodeName"] != null)
                    Order.coupon_code = Session["couponCodeName"].ToString();


                Order.TotalPrice = Session["grdTotal"].ToString();
                Order.sale_price = ViewState["salPrice"].ToString();

                Order.price = Session["grdTotal"].ToString();
                Order.shipping_price = ViewState["ShippingCharge"].ToString();
                UtilityLog.WriteToLog("InsertOrder ______3");

                long resultOrder = orderHandler.insert_order(Order);
                if (resultOrder > 0)
                {
                    Order.order_id = Convert.ToInt32(resultOrder);
                    Session["OrderNumber"] = Order.order_id;
                    Session["OrderNumberGUID"] = Order.order_number;
                    DataTable dtCart = (DataTable)Session["Cart"];

                    decimal totalAmount = (from DataRow drCart in dtCart.AsEnumerable()
                                           where drCart.RowState != DataRowState.Deleted
                                           select (Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();

                    if (dtCart != null && dtCart.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCart.Rows.Count; i++)
                        {
                            Order.product_id = Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString());
                            Order.quantity = Convert.ToInt32(dtCart.Rows[i]["quantity"].ToString());
                            Order.TotalPrice = totalAmount.ToString();

                            //if (Order.PaymentThrough != "COD")
                            //{
                            //    Order.order_status = "Failed";
                            //}
                            //else
                            //{
                            Order.order_status = "New Order";
                            //}

                            if (dtCart.Rows[i]["custom_bag_price"] != DBNull.Value)
                                Order.custom_bag_price = dtCart.Rows[i]["custom_bag_price"].ToString();
                            if (dtCart.Rows[i]["custom_bag_param"] != DBNull.Value)
                                Order.custom_bag_param = dtCart.Rows[i]["custom_bag_param"].ToString();
                            if (dtCart.Rows[i]["x_point"] != DBNull.Value)
                                Order.x_point = Convert.ToSingle(dtCart.Rows[i]["x_point"]);
                            if (dtCart.Rows[i]["y_point"] != DBNull.Value)
                                Order.y_point = Convert.ToSingle(dtCart.Rows[i]["y_point"]);

                            bool resultOrderDetails = orderHandler.insert_order_detail(Order);
                            if (resultOrderDetails)
                            {
                                // checking and updating stock and out of stock
                                product_handler productsHandler = new product_handler();
                                DataSet dsqty = productsHandler.get_search_quantity(Convert.ToInt64(dtCart.Rows[i]["product_id"].ToString()));
                                if (dsqty != null && dsqty.Tables.Count > 0)
                                {
                                    DataTable dtqty = dsqty.Tables[0];
                                    if (dtqty.Rows.Count > 0)
                                    {
                                        qty = Convert.ToInt32(dtqty.Rows[0]["quantity"].ToString());
                                        txtqty = Convert.ToInt32(dtCart.Rows[i]["quantity"].ToString());
                                        qtycount = qty - txtqty;
                                        Order.quantity = qtycount;

                                        if (qty == 0)
                                        {
                                            //pending redirect on product detail page.
                                        }

                                        if (qtycount <= 0)
                                        {
                                            Order.in_stock = false;
                                        }
                                        else
                                        {
                                            Order.in_stock = true;
                                        }

                                        bool updateQuantity = orderHandler.update_product_quantity(Order);
                                    }
                                }
                            }
                        }
                    }
                    Session["email"] = Session["CustomerLoginDetails"].ToString();
                    Session["amt"] = lblGrandTotal.Text;
                    Session["cell"] = "";
                    Session["orderid"] = Session["OrderNumber"].ToString();

                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                UtilityLog.WriteToLog("InsertOrder ______Error: " + ex.Message + "\n" + ex.StackTrace);
            }
            return false;
        }

        #endregion

        protected void lbtnApply_Click(object sender, EventArgs e)
        {

            byte CouponGenderType = 0;
            Decimal CouponAmount = 0, CouponPrice = 0; //Coupon %
            short countCouponAppliedProduct = 0;

            DataTable dtCart = (DataTable)Session["Cart"];
            product_handler productHandler = new product_handler();

            DataTable dtCoupon = null;
            dtCoupon = productHandler.get_coupon_code(txtCouponCode.Text, 0, 0, 0).Tables[0];
            if (dtCoupon.Rows.Count > 0 && dtCoupon.Rows[0]["price"] != DBNull.Value)
            {
                CouponPrice = Convert.ToDecimal(dtCoupon.Rows[0]["price"]);
                Session["couponcodeprice"] = CouponPrice;
                Session["couponCodeName"] = txtCouponCode.Text;

                fxCountCode = txtCouponCode.Text;
                if (fxCountCode == "CDG17Y" || fxCountCode == "BMP98A" || fxCountCode == "JPG2XA" || fxCountCode == "NKL12M" || fxCountCode == "XCV1VB" || fxCountCode == "GHL7YZ" || fxCountCode == "ASD85S" || fxCountCode == "JGH10B" || fxCountCode == "JHD1KJ" || fxCountCode == "CXY189")
                {
                    if (Session["CustomerLoginDetails"] != null)
                    {
                        string custLogEmailId = Session["CustomerLoginDetails"].ToString();

                        order_handler orderHandler = new order_handler();
                        DataSet ds = new DataSet();

                        ds = orderHandler.chk_CouonCodeByEmail(custLogEmailId, txtCouponCode.Text);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                                lblMsgCoupon.Text = "Coupon Code Already Used.";
                                Session["couponcodeprice"] = null;
                                return;
                            }
                            else
                            {
                                CouponPrice = Convert.ToDecimal(dtCoupon.Rows[0]["price"]);
                                Session["couponcodeprice"] = CouponPrice;
                                //Session["couponCodeName"] = txtCouponCode.Text;
                            }
                        }
                    }
                    else
                    {
                        lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                        lblMsgCoupon.Text = "Login/Register";
                        Session["couponcodeprice"] = null;
                        return;
                    }
                }
               
            }
            else
            {
                lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                lblMsgCoupon.Text = "Coupon Code is Invalid.";
                Session["couponcodeprice"] = null;
                return;
            }

            // Apply coupon discount Gender wise
            if (dtCoupon.Rows[0]["gendertype"] != DBNull.Value)
            {
                CouponGenderType = Convert.ToByte(dtCoupon.Rows[0]["gendertype"]);

                foreach (DataRow cartRow in dtCart.Rows)
                {
                    if (cartRow["discount"] != DBNull.Value && Convert.ToInt32(cartRow["discount"]) > 0)
                    {
                        // No need to count discount. Already available.
                    }
                    else if (dtCoupon.Rows[0]["menu_name"].ToString() != "select menu")
                    {
                        if (dtCoupon.Rows[0]["menu_name"].ToString() == cartRow["menu_name"].ToString())
                        {
                            if (cartRow["gendertype"] != DBNull.Value && cartRow["gendertype"].ToString() == dtCoupon.Rows[0]["gendertype"].ToString())
                            {
                                CouponAmount = GetCouponAmount(Convert.ToDecimal(cartRow["Total"]), CouponPrice);
                                cartRow["coupon_discount"] = CouponAmount;
                                countCouponAppliedProduct++;
                            }
                        }
                    }
                    else if (cartRow["gendertype"] != DBNull.Value && cartRow["gendertype"].ToString() == dtCoupon.Rows[0]["gendertype"].ToString())
                    {
                        CouponAmount = GetCouponAmount(Convert.ToDecimal(cartRow["Total"]), CouponPrice);
                        cartRow["coupon_discount"] = CouponAmount;
                        countCouponAppliedProduct++;
                    }
                }

            }
            else
            {
                foreach (DataRow cartRow in dtCart.Rows)
                {
                    if (cartRow["discount"] != DBNull.Value && Convert.ToInt32(cartRow["discount"]) > 0)
                    {
                        // No need to count discount. Already available.
                    }
                    else if (dtCoupon.Rows[0]["menu_name"].ToString() == "select menu")
                    {
                        CouponAmount = GetCouponAmount(Convert.ToDecimal(cartRow["Total"]), CouponPrice);
                        cartRow["coupon_discount"] = CouponAmount;
                        countCouponAppliedProduct++;
                    }
                    else if (dtCoupon.Rows[0]["menu_name"].ToString() != "select menu"
                        && dtCoupon.Rows[0]["menu_name"].ToString() == cartRow["menu_name"].ToString())
                    {
                        CouponAmount = GetCouponAmount(Convert.ToDecimal(cartRow["Total"]), CouponPrice);
                        cartRow["coupon_discount"] = CouponAmount;
                        countCouponAppliedProduct++;
                    }
                }

            }
            if (countCouponAppliedProduct > 0)
            {
                lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                lblMsgCoupon.Text = "Coupon code applied successfully on " + countCouponAppliedProduct.ToString() + " product(s).";
                Session["CouponAmount"] = (from DataRow drCart in dtCart.AsEnumerable()
                                           where drCart.RowState != DataRowState.Deleted
                                           select (Convert.ToDecimal(drCart["coupon_discount"]))).Sum();
            }
            else
            {
                lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                lblMsgCoupon.Text = "Oops !!! Apologies, This coupon is not valid on products which are already on Sale or belongs different Category!";
            }

            int TotalQty = 0;
            TotalQty = (from DataRow drCart in dtCart.AsEnumerable() select (Convert.ToInt32(drCart["quantity"]))).Sum();

            //AddToShoppingCart(TotalQty);
            UpdateShoppingCart();



        }

        private Decimal GetCouponAmount(Decimal amount, Decimal coupcode)
        {
            Decimal CA = amount - (amount * coupcode) / 100;
            Decimal TCA = amount - CA;
            return TCA;
        }


        private void Wigzoaddtocart()
        {
            string ReqUrl = Request.UrlReferrer.ToString();
            string addtocart = @"wigzo (""track"", ""addtocart"", """ + ReqUrl + @""");";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addtocart", addtocart, true);
        }
        private void getText()
        {
            try
            {
                DataTable dt = (DataTable)Session["PageLabel"];
                if (dt.Rows.Count > 0)
                {
                    lblSale.Text = dt.Select("label_name='Sale Text'")[0]["label_value"].ToString();
                    lblCode.Text = dt.Select("label_name='Code Coupon'")[0]["label_value"].ToString();
                    lblOffer.Text = dt.Select("label_name='Offer Coupon'")[0]["label_value"].ToString();
                }
            }
            catch (Exception)
            {
                //If any error then ignore
            }
        }

    }
}