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

namespace strutt.Admin
{
    public partial class city : System.Web.UI.Page
    {
        Int32 cityID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.bindState();
                this.GetCity();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
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
                    ddlState.DataSource = dt;
                    ddlState.DataTextField = "state";
                    ddlState.DataValueField = "state_id";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "select state");
                }
                else
                {
                    ddlState.DataSource = null;
                    ddlState.DataBind();
                }

            }
        }
        private void GetCity()
        {
            city_handler cityHandler = new city_handler();
            DataSet ds = cityHandler.get_city(null);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    gvCity.DataSource = dt;
                    gvCity.DataBind();
                }
                else
                {
                    gvCity.DataSource = null;
                    gvCity.DataBind();
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["cityID"] = null;
            btnSubmit.Text = "Submit";
            txtcityName.Text = string.Empty;
            this.GetCity();
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlState.SelectedItem.Value.ToString() == "select state")
            {
                lbldropdown.Text = "Plese Select State";
                ddlState.Focus();
            }
            else
            { 

            if (ViewState["cityID"] != null)
            {
                cityID = Convert.ToInt32(ViewState["cityID"].ToString());
            }

            city_handler cityHandler = new city_handler();
             int result = cityHandler.insert_update_city(cityID, Convert.ToInt64(ddlState.SelectedValue),txtcityName.Text,chkIsActive.Checked);
            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtcityName.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {
                if (ViewState["cityID"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtcityName.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtcityName.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
               this.GetCity();
            }
            ViewState["cityID"] = null;
            btnSubmit.Text = "Submit";
            txtcityName.Text = string.Empty;
                lbldropdown.Text = string.Empty;
            }

        }
        protected void gvCity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton lnk = (ImageButton)e.Row.FindControl("imgBtnActive");
                Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                if (lblstatus.Text == "False")
                {
                    lnk.CommandName = "Active";
                }
                if (lblstatus.Text == "True")
                {
                    lnk.CommandName = "Deactive";
                }
            }
        }

        protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                cityID = Convert.ToInt32(e.CommandArgument);
                city_handler cityHandler = new city_handler();
                DataSet ds = cityHandler.get_city(cityID);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ddlState.Focus();
                        ViewState["cityID"] = cityID;
                        ddlState.SelectedValue = dt.Rows[0]["state_id"].ToString();
                        txtcityName.Text = dt.Rows[0]["city_name"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "city_id", "tbl_city");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "city_id", "tbl_city");
            }
            this.GetCity();
        }
    }
}