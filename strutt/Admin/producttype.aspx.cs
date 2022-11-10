

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
    public partial class producttype : System.Web.UI.Page
    {
        Int32 producttypeID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                this.BindMenu();
                this.BindProductType();
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
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
                    ddlChildMenu.Items.Insert(0, "select child menu");
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
                    ddlSubMenu.DataSource = null;
                    ddlSubMenu.DataBind();
                    ddlSubMenu.Items.Insert(0, "no data");
                }
            }
        }

        private void BindChildMenu(int childid)
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_child(childid, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlChildMenu.DataSource = dt;
                    ddlChildMenu.DataTextField = "child_name";
                    ddlChildMenu.DataValueField = "child_menu_id";
                    ddlChildMenu.DataBind();
                    ddlChildMenu.Items.Insert(0, "select child menu");
                }
                else
                {
                    ddlChildMenu.DataSource = null;
                    ddlChildMenu.DataBind();
                    ddlChildMenu.Items.Insert(0, "no data");
                }
            }
        }

        private void BindProductType()
        {
            product_handler productHandler = new product_handler();
            DataSet ds = productHandler.get_product_type(0, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    gvProductType.DataSource = dt;
                    gvProductType.DataBind();
                }
                else
                {
                    gvProductType.DataSource = null;
                    gvProductType.DataBind();
                }
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

        protected void ddlSubMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubMenu.SelectedValue == "select sub menu")
            {
                ddlChildMenu.Items.Clear();
                ddlChildMenu.Items.Insert(0, "select child menu");
            }
            else
            {
                BindChildMenu(Convert.ToInt32(ddlSubMenu.SelectedValue));
            }
        }

        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["producttypeID"] != null)
            {
                producttypeID = Convert.ToInt32(ViewState["producttypeID"].ToString());
            }

            product_handler productHandler = new product_handler();
            int result = productHandler.insert_update_product_type(producttypeID, Convert.ToInt32(ddlChildMenu.SelectedValue), txtProductType.Text, txtProductTypeUrl.Text);
            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtProductType.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {
                if (ViewState["producttypeID"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtProductType.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtProductType.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.BindProductType();
            }
            ViewState["producttypeID"] = null;
            btnSubmit.Text = "Submit";
            txtProductType.Text = string.Empty;
            txtProductTypeUrl.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["producttypeID"] = null;
            btnSubmit.Text = "Submit";
            txtProductType.Text = string.Empty;
            txtProductTypeUrl.Text = string.Empty;
            this.BindProductType();
        }

        protected void gvProductType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            producttypeID = Convert.ToInt32(gvProductType.DataKeys[e.RowIndex].Values["product_type_id"].ToString());
            string name = gvProductType.DataKeys[e.RowIndex].Values["product_type_name"].ToString();
            product_handler productHandler = new product_handler();
            bool delete = productHandler.delete_product_type(producttypeID);
            if (delete)
            {
                this.BindProductType();
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = name + " " + helper_data.getMessage("msgDeleteSuccessfully");
                gvProductType.Focus();
            }
        }

        protected void gvProductType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                producttypeID = Convert.ToInt32(e.CommandArgument);
                product_handler productHandler = new product_handler();
                DataSet ds = productHandler.get_product_type(producttypeID, 0);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtProductType.Focus();
                        ViewState["producttypeID"] = producttypeID;
                        ddlMenu.SelectedValue = dt.Rows[0]["menu_id"].ToString();
                        bindSubMenu(Convert.ToInt32(ddlMenu.SelectedValue));
                        ddlSubMenu.SelectedValue = dt.Rows[0]["sub_menu_id"].ToString();
                        BindChildMenu(Convert.ToInt32(ddlSubMenu.SelectedValue));
                        ddlChildMenu.SelectedValue = dt.Rows[0]["child_menu_id"].ToString();
                        txtProductType.Text = dt.Rows[0]["product_type_name"].ToString();
                        txtProductTypeUrl.Text = dt.Rows[0]["product_type_url"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "product_type_id", "tbl_product_type");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "product_type_id", "tbl_product_type");
            }
            this.BindProductType();
        }

        protected void gvProductType_RowDataBound(object sender, GridViewRowEventArgs e)
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