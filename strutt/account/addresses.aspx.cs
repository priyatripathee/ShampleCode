using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using BLL;
using BusinessEntities;
using DAL;

namespace strutt.account
{
    public partial class addresses : System.Web.UI.Page
    {
        Guid customerId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                if (Session["CustomerLoginDetails"] != null)
                {
                    hfcustomeremailid.Value = Session["CustomerLoginDetails"].ToString();
                    hfcustomerloginid.Value = Session["customerDetailsId"].ToString();
                    customerId = Guid.Parse(Session["customerDetailsId"].ToString());
                    bindData(customerId);
                    this.bindState();
                }
                else
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }
        }

        private void bindData(Guid customerId)
        {
            customer_handler customerHandler = new customer_handler();
            DataSet ds = customerHandler.get_customer_address(customerId);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dlistcustomeraddress.DataSource = dt;
                    dlistcustomeraddress.DataBind();
                }
                else
                {
                    dlistcustomeraddress.DataSource = null;
                    dlistcustomeraddress.DataBind();
                }
            }
        }

        #region ---------- insert/delete customer addresses details--------------

        [WebMethod]
        public static bool Insertcustomerdetails(Guid customerId, string fullName, string emailId, string contactNumber, string address,string landMark, string city, string state, string pinCode)
        {
            customer_handler customerHandler = new customer_handler();
            customer Customer = new customer();
            Customer.customer_id = customerId;
            Customer.full_name = fullName;
            Customer.email_id = emailId;
            Customer.contact_number = contactNumber;
            Customer.address = address;
            Customer.land_mark = landMark;
            Customer.city = city;
            Customer.state = state;
            Customer.pin_code = pinCode;
            bool InsertData = customerHandler.insert_customer_details(Customer);
            return InsertData;
        }

        [WebMethod]
        public static bool DeleteCustomer(long customerId)
        {
            bool delete;
            customer_handler customerHandler = new customer_handler();
            delete = customerHandler.delete_customer_address(customerId);
            return delete;
        }

        #endregion


        protected void ddlReceiverState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReceiverState.SelectedValue == "Select State")
            {
                ddlReceiverCity.Items.Clear();
                ddlReceiverCity.Items.Insert(0, "Select City");
            }
            else
            {
                string statenamne = ddlReceiverState.SelectedValue;
                this.BindCity(statenamne);
            }
        }
        private void BindCity(string statename)
        {
            pincode_handler pincodeHandler = new pincode_handler();
            DataSet ds = pincodeHandler.get_pincode_search(statename, "", "", 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlReceiverCity.DataSource = ds.Tables[0];
                ddlReceiverCity.DataBind();
                ddlReceiverCity.Items.Insert(0, "Select City");
            }
            else
            {
                ddlReceiverCity.Items.Clear();
                ddlReceiverCity.Items.Insert(0, "Select City");
            }
        }
        private void bindState()
        {
            pincode_handler pincodeHandler = new pincode_handler();
            DataSet ds = pincodeHandler.get_pincode_search("", "", "", 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlReceiverState.DataSource = dt;
                    ddlReceiverState.DataBind();
                    ddlReceiverState.Items.Insert(0, "Select State");
                    ddlReceiverCity.Items.Insert(0, "Select City");
                }
                else
                {
                    ddlReceiverState.Items.Clear();
                    ddlReceiverState.Items.Insert(0, "Select State");
                    ddlReceiverCity.Items.Clear();
                    ddlReceiverCity.Items.Insert(0, "Select City");
                }
            }
        }
    }
}