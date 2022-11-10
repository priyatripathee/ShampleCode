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
    public partial class childmenu : System.Web.UI.Page
    {
        Int32 childMenuID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindMenu();
                this.BindChildMenu();
                if (Session["Role"].ToString() == "Admin")
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
                    ddlMenu.DataSource = dt;
                    ddlMenu.DataTextField = "menu_name";
                    ddlMenu.DataValueField = "menu_id";
                    ddlMenu.DataBind();
                    ddlMenu.Items.Insert(0, "select menu");
                    ddlSubMenu.Items.Insert(0, "select sub menu");
                }
                else
                {
                    ddlMenu.DataSource = null;
                    ddlMenu.DataBind();
                }
            }
        }

        private void bindSubMenu(int subid)
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_sub(subid, 0,null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlSubMenu.DataSource = dt;
                    ddlSubMenu.DataTextField = "sub_menu_name";
                    ddlSubMenu.DataValueField = "sub_menu_id";
                    ddlSubMenu.DataBind();
                    ddlSubMenu.Items.Insert(0, "select sub menu");
                }
                else
                {
                    ddlSubMenu.Items.Clear();
                    ddlSubMenu.Items.Insert(0, "select sub menu");
                }
            }
            else
            {
                ddlSubMenu.Items.Clear();
                ddlSubMenu.Items.Insert(0, "select sub menu");
            }
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMenu.SelectedValue == "select menu")
            {
                ddlSubMenu.Items.Clear();
                ddlSubMenu.Items.Insert(0, "select sub menu");
            }
            else
            {
                bindSubMenu(Convert.ToInt32(ddlMenu.SelectedValue));
            }
        }

        private void BindChildMenu()
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_child(0, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    gvChildMenu.DataSource = dt;
                    gvChildMenu.DataBind();
                }
                else
                {
                    gvChildMenu.DataSource = null;
                    gvChildMenu.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["childMenuID"] != null)
            {
                childMenuID = Convert.ToInt32(ViewState["childMenuID"].ToString());
            }

            menu_handler menuHandler = new menu_handler();
            int result = menuHandler.insert_update_menu_child(childMenuID, Convert.ToInt32(ddlSubMenu.SelectedValue), txtChildMenuName.Text, txtChildMenuURL.Text);
            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtChildMenuName.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {
                if (ViewState["childMenuID"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtChildMenuName.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtChildMenuName.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.BindChildMenu();
            }
            ViewState["childMenuID"] = null;
            btnSubmit.Text = "Submit";
            txtChildMenuName.Text = string.Empty;
            txtChildMenuURL.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["childMenuID"] = null;
            btnSubmit.Text = "Submit";
            txtChildMenuName.Text = string.Empty;
            txtChildMenuURL.Text = string.Empty;
            this.BindChildMenu();
        }

        protected void gvChildMenu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            childMenuID = Convert.ToInt32(gvChildMenu.DataKeys[e.RowIndex].Values["child_menu_id"].ToString());
            string name = gvChildMenu.DataKeys[e.RowIndex].Values["child_name"].ToString();
            menu_handler menuHandler = new menu_handler();
            bool delete = menuHandler.delete_menu_child(childMenuID);
            if (delete)
            {
                this.BindChildMenu();
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = name + " " + helper_data.getMessage("msgDeleteSuccessfully");
                gvChildMenu.Focus();
            }
        }

        protected void gvChildMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                childMenuID = Convert.ToInt32(e.CommandArgument);
                menu_handler menuHandler = new menu_handler();
                DataSet ds = menuHandler.get_menu_child(0, childMenuID);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtChildMenuName.Focus();
                        ViewState["childMenuID"] = childMenuID;
                        ddlMenu.SelectedValue = dt.Rows[0]["menu_id"].ToString();
                        bindSubMenu(Convert.ToInt32(ddlMenu.SelectedValue));
                        ddlSubMenu.SelectedValue = dt.Rows[0]["sub_menu_id"].ToString();
                        txtChildMenuName.Text = dt.Rows[0]["child_name"].ToString();
                        txtChildMenuURL.Text = dt.Rows[0]["child_menu_url"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "child_menu_id", "tbl_menu_child");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "child_menu_id", "tbl_menu_child");
            }
            this.BindChildMenu();
        }

        protected void gvChildMenu_RowDataBound(object sender, GridViewRowEventArgs e)
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