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
using DAL;

namespace strutt.Admin
{
    public partial class superadmin : System.Web.UI.Page
    {
        DataTable dt;
        int adId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                this.BindAdminData();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindAdminData()
        {
            admin_data_handler admindataHandler = new admin_data_handler();
            DataTable dt = admindataHandler.get_superadmin("");
            if (dt.Rows.Count > 0)
            {
                lbl_Total.Text = "Total " + dt.Rows.Count + " records";
                grdAdmin.DataSource = dt;
                grdAdmin.DataBind();
            }
            else
            {
                grdAdmin.DataSource = null;
                grdAdmin.DataBind();
            }
            txtUserId.Text = string.Empty;
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            ddlAdminRole.ClearSelection();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            admin_data_handler admindataHandler = new admin_data_handler();
            BusinessEntities.admin admin = new BusinessEntities.admin();
            //string strpassword = security.Encryptdata(txtPassword.Text);
            string strpassword = txtPassword.Text;
            if (ViewState["adId"] != null)
            {
                adId = Convert.ToInt32(ViewState["adId"].ToString());
            }

            admin.admin_id = adId;
            admin.user_name = txtUserId.Text.Trim();
            admin.password = strpassword;
            admin.first_name = "";
            admin.last_name = "";
            admin.permission = ddlAdminRole.SelectedItem.Text;
            admin.created_by = Session["AdminUserID"].ToString();

            long result = admindataHandler.insert_update_superadmin(admin);
            if (result > 0)
            {
                if (adId == 0)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtUserId.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                    BindAdminData();
                    grdAdmin.Focus();
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtUserId.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                    BindAdminData();
                    grdAdmin.Focus();
                    ViewState["adId"] = null;
                    btnSubmit.Text = "Submit";
                }
            }
            if (result == -2)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = txtUserId.Text + " " + helper_data.getMessage("msgAlreadyExist");
            }
        }
        protected void grdAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {

                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);

                HiddenField UserId = row.FindControl("hfieldUserName") as HiddenField;

                admin_data_handler admindataHandler = new admin_data_handler();
                DataTable dt = admindataHandler.get_superadmin(UserId.Value.ToString());
                if (dt.Rows.Count > 0)
                {
                    txtUserId.Focus();
                    ViewState["adId"] = dt.Rows[0]["admin_id"].ToString();
                    txtUserId.Text = dt.Rows[0]["user_name"].ToString();
                    ddlAdminRole.SelectedItem.Text = dt.Rows[0]["permission"].ToString();
                    btnSubmit.Text = "Update";
                }
            }
            else
            {
                if (e.CommandName == "Active")
                {
                    bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "admin_id", "tbl_admin");
                    this.BindAdminData();
                    grdAdmin.Focus();
                }
                else
                {
                    bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "admin_id", "tbl_admin");
                    this.BindAdminData();
                    grdAdmin.Focus();
                }
            }
        }

        protected void grdAdmin_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void grdAdmin_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            admin_data_handler admindataHandler = new admin_data_handler();

            adId = Convert.ToInt32(grdAdmin.DataKeys[e.RowIndex].Values["admin_id"].ToString());
            string mnName = grdAdmin.DataKeys[e.RowIndex].Values["user_name"].ToString();

            bool delete = admindataHandler.delete_superadmin(adId);
            BindAdminData();
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = mnName + " " + helper_data.getMessage("msgDeleteSuccessfully");
            grdAdmin.Focus();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserId.Text = "";
            BindAdminData();
            ViewState["adId"] = null;
            btnSubmit.Text = "Submit";
            grdAdmin.Focus();
        }
    }
}