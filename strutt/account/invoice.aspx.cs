using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

namespace strutt.account
{
    public partial class invoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["id"] != null)
                {
                    GetOrder(Convert.ToInt64(BusinessEntities.security.Decryptdata(Request.QueryString["id"])));
                }
                //else
                //{
                //    Response.Redirect("~/Admin/orderstatus.aspx");
                //}
            }
        }

        private void GetOrder(long ordId)
        {
            order_handler orderHandler = new order_handler();
            DataTable dt = new DataTable();
            dt = orderHandler.get_order_search(ordId, null).Tables[0];
            string AddressOther = null;
            //string ordPrifix = "STR10001";
            string CurrentState = "Uttar Pradesh";
            double GstRate = 18.0f, GstValue;

            if (dt != null && dt.Rows.Count > 0)
            {
                //if (Session["Role"].ToString().Equals("Developer") && !dt.Rows[0]["email_id"].ToString().Equals(Session["CustomerLoginDetails"].ToString()))
                //{
                //    Response.Redirect(Request.UrlReferrer.ToString());
                //    return;
                //}

                litBillEmail.Text = dt.Rows[0]["email_id"].ToString();
                litBillName.Text = dt.Rows[0]["customer_name"].ToString();
                litBillPhone.Text = dt.Rows[0]["contact_number"].ToString();
                litShipName.Text = dt.Rows[0]["user_name"].ToString();
                litShipAddress.Text = dt.Rows[0]["address"].ToString();
                if (dt.Rows[0]["land_mark"].ToString().Length > 0)
                    AddressOther = dt.Rows[0]["land_mark"].ToString() + ", ";
                AddressOther += dt.Rows[0]["city"].ToString() + ", ";
                AddressOther += dt.Rows[0]["state"].ToString() + ", ";
                AddressOther += dt.Rows[0]["pin_code"].ToString() + ", ";
                AddressOther += "IN";

                litOrderId.Text = dt.Rows[0]["order_id"].ToString();
                litPaymentType.Text = dt.Rows[0]["Payment"].ToString();
                litInvoiceNo.Text = dt.Rows[0]["invoice_id"].ToString();
                litInvoiceDate.Text = dt.Rows[0]["OrdDate"].ToString();

                litDiscount.Text = dt.Rows[0]["discount_price"].ToString();
                litShippingCharge.Text = dt.Rows[0]["shipping_price"].ToString();

                int totalQty = (from DataRow dr in dt.AsEnumerable() select (Convert.ToInt32(dr["quantity"]))).Sum();
                litOrderQtyTotal.Text = totalQty.ToString();

                double totalAmount = (from DataRow dr in dt.AsEnumerable() select (Convert.ToInt32(dr["quantity"]) * Convert.ToSingle(dr["unit_price"]))).Sum();
                litOrderAmountTotal.Text = totalAmount.ToString("0.00");

                double GrandTotal=Convert.ToSingle(dt.Rows[0]["total_price"]);
                litGrandTotal.Text = GrandTotal.ToString("0.00");

                GstValue = GrandTotal * GstRate / (100 + GstRate);
                if (dt.Rows[0]["state"].ToString().Equals(CurrentState))
                {
                    litCgst9.Text = (GstValue / 2).ToString("0.00");
                    litSgst9.Text = (GstValue / 2).ToString("0.00");
                    litIgst18.Text = "-";
                }
                else
                {
                    litCgst9.Text = "-";
                    litSgst9.Text = "-";
                    litIgst18.Text = GstValue.ToString("0.00");
                }
                if (dt.Rows.Count == 1)
                    litGrossWeight.Text = dt.Rows[0]["weight"].ToString();
                else
                    litGrossWeight.Text = "N/A";

                litGrandTotalInWord.Text = NumbersToWords(Convert.ToInt32(GrandTotal));

                rptOrderDet.DataSource = dt;
                rptOrderDet.DataBind();
            }
            else
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }

            admin_data_handler companyhandler = new admin_data_handler();
            DataTable dtcompany = new DataTable();
            dtcompany = companyhandler.get_companydetails(1);
            if (dtcompany != null && dtcompany.Rows.Count > 0)
            {
                litCompanyName.Text = dtcompany.Rows[0]["CompanyName"].ToString();
                litAdd1.Text = dtcompany.Rows[0]["Address1"].ToString();
                litAdd2.Text = dtcompany.Rows[0]["Address2"].ToString();
                litwebsite.Text = dtcompany.Rows[0]["Website"].ToString();
                litemail.Text = dtcompany.Rows[0]["Email"].ToString();
                litphn1.Text = dtcompany.Rows[0]["Phone1"].ToString();
                litPan.Text = dtcompany.Rows[0]["Pannumber"].ToString();
                litGST.Text = dtcompany.Rows[0]["GST"].ToString();
            }

        }

        public static string NumbersToWords(int inputNumber)
        {
            int inputNo = inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }

    }
}