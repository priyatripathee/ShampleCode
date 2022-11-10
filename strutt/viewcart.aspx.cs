using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

namespace strutt
{
    public partial class viewcart : System.Web.UI.Page
    {
        long matchid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //// check is secure connection used
                //if (!Request.IsSecureConnection)
                //{
                //    // redirect visitor to SSL connection
                //    Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
                //}

                //if (Session["CustomerLoginDetails"] != null)
                    offer.Style["display"] = "block";
                //else
                //    offer.Style["display"] = "none";

                Session["couponCodeName"] = null;
                Session["couponcodeprice"] = null;
                AddToShoppingCart();
            }
        }

        private void AddToShoppingCart()
        {
            //Session["customize"] = null;
            //Session["procust"] = null;
            Session["discount"] = null;
            try
            {
                DataTable dtCart = (DataTable)Session["Cart"];
                lblCartCount.Text = dtCart.Rows.Count.ToString();
                Session["cartCount"] = lblCartCount.Text;
                if (dtCart != null && dtCart.Rows.Count > 0)
                {
                    Session["product_id"] = Convert.ToInt64(dtCart.Rows[0]["product_id"].ToString());

                    divCartContinue.Visible = false;
                    divCart.Visible = true;
                    btnCheckout.Visible = true;
                    //SubTotal Amount
                    Decimal amount = (from DataRow drCart in dtCart.AsEnumerable()
                                      where drCart.RowState != DataRowState.Deleted
                                      select (Convert.ToDecimal(drCart["Total"]))).Sum();
                    lblSubTotal.Text = String.Format("{0:0.00}", amount);

                    //Tax Amount
                    Decimal taxPercentage = 18;
                    Decimal TaxAmount = GetTaxAmount(amount, taxPercentage);

                    lblTaxAmount.Text = String.Format("{0:0.00}", TaxAmount);

                    //Total Amount
                   // lblTotalAmount.Text = String.Format("{0:0.00}", (amount - TaxAmount));

                    // Shipping Charges
                    if (amount > 750)
                    {
                        lblShipping.Text = "0.00";
                        lblShippingEstimate.Text = "0.00";
                    }
                    else
                    {
                        lblShipping.Text = String.Format("{0:0.00}", 50);
                        lblShippingEstimate.Text = "50.00";
                    }                    
                    Decimal CouponAmount = 0;
                    lblCouponApply.Text = "";

                    //Grand Total
                    Decimal totalAmount = (from DataRow drCart in dtCart.AsEnumerable()
                                     where drCart.RowState != DataRowState.Deleted
                                     select (Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["quantity"]))).Sum();

                    decimal totalDiscount = (from DataRow drCart in dtCart.AsEnumerable()
                                        where drCart.RowState != DataRowState.Deleted
                                        select ((Convert.ToDecimal(drCart["sale_price"]) * Convert.ToDecimal(drCart["discount"]) / 100) * Convert.ToDecimal(drCart["quantity"]))).Sum();

                    if (totalDiscount == 0)
                    {
                        if (Session["couponcodeprice"] != null)
                        {
                            decimal coupcode = Convert.ToDecimal(Session["couponcodeprice"].ToString());
                            CouponAmount = GetCouponAmount(amount, coupcode);
                            Decimal totalApply = (amount + Convert.ToDecimal(lblShipping.Text));
                            lblCouponApply.Text = String.Format("Rs. {0:0.00}", totalApply);

                            Session["codecoupon"] = CouponAmount;
                            //lblCouponCode.Visible = true;
                        }
                        else
                        {
                            lblCouponApply.Text = "";
                            Session["codecoupon"] = null;
                            //lblCouponCode.Visible = false;
                            Session["couponcodeprice"] = null;
                        }                        
                    }
                    else
                    {
                        Session["discount"] = dtCart.Rows[0]["discount"].ToString();
                        lblMsgCoupon.Text = "Coupon codes are not valid for products on sale.";
                        Session["codecoupon"] = null;
                    }

                    lblDiscount.Text = String.Format("{0:0.00}", totalDiscount + CouponAmount);
                    lblTotalAmount.Text = String.Format("{0:0.00}", totalAmount);

                    Decimal grandTotal = totalAmount - (totalDiscount + CouponAmount) + Convert.ToDecimal(lblShipping.Text);
                    lblGrandTotal.Text = String.Format("{0:0.00}", grandTotal);

                    dlCart.DataSource = dtCart;
                    dlCart.DataBind();
                }
                else
                {
                    Session["state"] = null;
                    Session["city"] = null;
                    divCartContinue.Visible = true;
                    divCart.Visible = false;
                    dlCart.Visible = false;
                    lblTotalAmount.Text = "0.00";
                    lblTaxAmount.Text = "0.00";
                    lblSubTotal.Text = "0.00";
                    lblDiscount.Text = "0.00";
                    lblShipping.Text = "0.00";
                    lblGrandTotal.Text = "0.00";
                    btnCheckout.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("PageNotFound.aspx", false);
            }
        }

        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }

        protected string GetDiscountDetails(Decimal discount)
        {
            string discountDetails = string.Empty;
            if (discount > 0)
            {
                discountDetails = String.Format("{0:0}% discount", discount);
            }
            return discountDetails;
        }

        private Decimal GetTaxAmount(Decimal subTotal, Decimal taxPercentage)
        {
            Decimal IA = (subTotal * 100) / (100 + taxPercentage);
            Decimal TA = (taxPercentage / 100) * IA;
            return TA;
        }

        private Decimal GetCouponAmount(Decimal subTotal, Decimal coupcode)
        {
            //Decimal CA = (subTotal * 100) / (100 + coupcode);
            //Decimal TCA = (coupcode / 100) * CA;
            //return TCA;

            //Decimal CA = (subTotal * 100) / (100 + coupcode);
            //Decimal TCA = (coupcode / 100) * CA;
            //return TCA;

            Decimal CA = subTotal - (subTotal * coupcode) / 100;
            Decimal TCA = subTotal - CA;
            return TCA;
        }

        protected void dlCart_ItemDataCommond(object source, DataListCommandEventArgs e)
        {

        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            //Response.Redirect("proceed-to-payment");
            Response.Redirect("proceedtopayment.aspx");   
        }

        #region apply coupon code

        protected void lbtnApply_Click(object sender, EventArgs e)
        {
            long proId = Convert.ToInt64(Session["product_id"]);
            long catid = 0;
            long subcatid = 0;

            product_handler productHandler = new product_handler();
            DataSet ds = productHandler.get_search_productId(proId);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    catid = Convert.ToInt64(dt.Rows[0]["menu_id"].ToString());
                    subcatid = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());
                }
            }
            DataSet dscpn = new DataSet();
            dscpn = productHandler.get_coupon_code(txtCouponCode.Text, catid, subcatid, 0);
            if (dscpn != null && dscpn.Tables.Count > 0)
            {
                DataTable dt = dscpn.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    long cId = Convert.ToInt64(dt.Rows[0]["menu_id"].ToString());
                    long SbId = Convert.ToInt64(dt.Rows[0]["sub_menu_id"].ToString());

                    string SndEmail = dt.Rows[0]["sender_email"].ToString();
                    string RecEmail = dt.Rows[0]["reciever_email"].ToString();

                    Session["couponCodeName"] = txtCouponCode.Text;


                    if (RecEmail != null || RecEmail != "")///////////////////////refer friends
                    {
                        Session["sndemail"] = SndEmail.ToString();
                        Session["recemail"] = RecEmail.ToString();
                    }
                    else
                    {
                        Session["sndemail"] = null;
                        Session["recemail"] = null;
                    }


                    if (cId == catid && SbId == subcatid)   ///////////caregory wise coupon code
                    {
                        lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                        lblMsgCoupon.Text = "apply coupon code";
                        Session["couponcodeprice"] = dt.Rows[0]["price"].ToString();
                        AddToShoppingCart();
                    }
                    else if (cId == 0 && SbId == 0)     ////////////all product used coupon code
                    {
                        lblMsgCoupon.ForeColor = System.Drawing.Color.Green;
                        lblMsgCoupon.Text = "apply coupon code";
                        Session["couponcodeprice"] = dt.Rows[0]["price"].ToString();
                        AddToShoppingCart();
                    }
                    else
                    {
                        lblCouponApply.Text = "";
                        lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                        lblMsgCoupon.Text = "Coupon Code is Invalid.";
                        Session["couponcodeprice"] = null;
                        AddToShoppingCart();
                    }
                }
                else
                {
                    lblCouponApply.Text = "";
                    lblMsgCoupon.ForeColor = System.Drawing.Color.Red;
                    lblMsgCoupon.Text = "Coupon Code is Invalid.";
                    Session["couponcodeprice"] = null;
                    AddToShoppingCart();
                }
            }
        }

        #endregion


        protected void dlCart_ItemDataCommond(object source, RepeaterCommandEventArgs e)
        {
            int qty = 1;
            int txtqty = 1;
            string proname = "";
            Session["discount"] = null;
            if (e.CommandName == "Update")
            {
                //DropDownList ddlQuantity = (DropDownList)e.Item.FindControl("ddlQuantity");

                TextBox quntity = (TextBox)e.Item.FindControl("txtquantity");
                if (string.IsNullOrEmpty(quntity.Text) || Convert.ToInt16(quntity.Text) <= 0)
                {
                    lblQtyMsg.Text = "Please enter valid Quantity.";
                    quntity.Text = "1";
                    //return;
                }

                int ProductID = Convert.ToInt32(e.CommandArgument);
                DataTable dtCart = (DataTable)Session["Cart"];
                foreach (DataRow row in dtCart.Rows)
                {
                    if (int.Parse(row["product_id"].ToString()) == ProductID)
                    {
                        row["quantity"] = Convert.ToInt32(quntity.Text);

                        Decimal Price = Convert.ToDecimal(row["sale_price"]);
                        Decimal discount = Convert.ToDecimal(row["discount"]);

                        Decimal UnitPriceOnDiscount = discount > 0 ? Price - (Price * discount / 100) : Price;

                        product_handler productHandler = new product_handler();
                        DataSet dsqty = productHandler.get_search_quantity(ProductID);
                        if (dsqty != null && dsqty.Tables.Count > 0)
                        {
                            DataTable dtqty = dsqty.Tables[0];
                            if (dtqty.Rows.Count > 0)
                            {
                                qty = Convert.ToInt32(dtqty.Rows[0]["quantity"].ToString());
                                txtqty = Convert.ToInt32(quntity.Text);
                                proname = dtqty.Rows[0]["product_name"].ToString();

                                if (qty >= txtqty)
                                {
                                    row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
                                    Session["Cart"] = dtCart;
                                    AddToShoppingCart();
                                    lblQtyMsg.Text = "";
                                    break;
                                }
                                else
                                {
                                    row["quantity"] = qty;
                                    row["Total"] = Convert.ToInt32(row["quantity"]) * UnitPriceOnDiscount;
                                    Session["Cart"] = dtCart;
                                    AddToShoppingCart();
                                    lblQtyMsg.Text = "Hi, only " + qty + " quantity of " + proname + " is available at this time.";
                                    break;
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }

            }
            else if (e.CommandName == "Remove")
            {
                if (Session["Cart"] != null)
                {
                    DataTable dt = (DataTable)Session["Cart"];
                    dt.Rows.RemoveAt(e.Item.ItemIndex);
                    Session["Cart"] = dt;
                    AddToShoppingCart();
                    //if (Session["Cart"] != null)
                    //{
                    //    if (((DataTable)Session["Cart"]).Rows.Count <= 0)
                    //    {
                    //        lblTotalAmount.Text = "0.00";
                    //        lblTaxAmount.Text = "0.00";
                    //        lblSubTotal.Text = "0.00";
                    //        lblDiscount.Text = "0.00";
                    //        lblShipping.Text = "0.00";
                    //        lblGrandTotal.Text = "0.00";
                    //        btnCheckout.Visible = false;
                    //    }
                    //}
                }
            }
        }
        
    }
}