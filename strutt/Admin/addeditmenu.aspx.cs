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
    public partial class addeditmenu : System.Web.UI.Page
    {
        Int32 menuID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindMenu();
                if (Request.QueryString["Id"] != null)
                {

                }
                if (Session["Role"].ToString() == "Admin" )
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindMenu()
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    grdMenu.DataSource = dt;
                    grdMenu.DataBind();
                }
                else
                {
                    grdMenu.DataSource = null;
                    grdMenu.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (ViewState["menuID"] != null)
            {
                menuID = Convert.ToInt32(ViewState["menuID"].ToString());
            }

            menu_handler menuHandler = new menu_handler();
            int result = menuHandler.insert_update_menu(menuID, txtMenuName.Text, txtMenuURL.Text, txtMetaTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text);
            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtMenuName.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {
                if (ViewState["menuID"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtMenuName.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtMenuName.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.BindMenu();
            }
            ViewState["menuID"] = null;
            btnSubmit.Text = "Submit";
            txtMenuName.Text = string.Empty;
            txtMenuURL.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["menuID"] = null;
            btnSubmit.Text = "Submit";
            txtMenuName.Text = string.Empty;
            txtMenuURL.Text = string.Empty;
            this.BindMenu();
        }

        protected void grdMenu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            menuID = Convert.ToInt32(grdMenu.DataKeys[e.RowIndex].Values["menu_id"].ToString());
            string name = grdMenu.DataKeys[e.RowIndex].Values["menu_name"].ToString();
            menu_handler menuHandler = new menu_handler();
            bool delete = menuHandler.delete_menu(menuID);
            if (delete)
            {
                this.BindMenu();
               lblMsg.ForeColor = System.Drawing.Color.Red;
              lblMsg.Text = name + " " + helper_data.getMessage("msgDeleteSuccessfully");
                grdMenu.Focus();
            }
        }

        protected void grdMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                menuID = Convert.ToInt32(e.CommandArgument);
                menu_handler menuHandler = new menu_handler();
                DataSet ds = menuHandler.get_menu(menuID);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtMenuName.Focus();
                        ViewState["menuID"] = menuID;
                        txtMenuName.Text = dt.Rows[0]["menu_name"].ToString();
                        txtMenuURL.Text = dt.Rows[0]["menu_url"].ToString();
                        txtMetaTitle.Text = dt.Rows[0]["meta_title"].ToString();
                        txtMetaKeyword.Text = dt.Rows[0]["meta_keywords"].ToString();
                        txtMetaDescription.Text = dt.Rows[0]["meta_description"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "menu_id", "tbl_menu");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "menu_id", "tbl_menu");
            }
            this.BindMenu();
        }

        protected void grdMenu_RowDataBound(object sender, GridViewRowEventArgs e)
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

        
    }
}