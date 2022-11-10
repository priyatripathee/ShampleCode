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
    public partial class submenu : System.Web.UI.Page
    {
        Int32 subMenuID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserID"] == null)
                    Response.Redirect("../account/Login.aspx");

                this.BindMenu();
                this.BindSubMenu();
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
                }
                else
                {
                    ddlMenu.DataSource = null;
                    ddlMenu.DataBind();
                }
            }
        }


        private void BindSubMenu()
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_sub(0, 0,null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    gvSubMenu.DataSource = dt;
                    gvSubMenu.DataBind();
                }
                else
                {

                    gvSubMenu.DataSource = null;
                    gvSubMenu.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["subMenuID"] != null)
            {
                subMenuID = Convert.ToInt32(ViewState["subMenuID"].ToString());
            }

            byte? lGenderType = null;

            menu_handler menuHandler = new menu_handler();
            if (!ddlgendertype.SelectedValue.Equals("0"))
                lGenderType = Convert.ToByte(ddlgendertype.SelectedValue);


            short? lsOrderby = null;
            if (!string.IsNullOrEmpty(txtOrderBy.Text))
            {
                lsOrderby = Convert.ToInt16(txtOrderBy.Text);
            }


            int result = menuHandler.insert_update_menu_sub(subMenuID, Convert.ToInt32(ddlMenu.SelectedValue), txtSubMenuName.Text, txtSubMenuURL.Text,
                lGenderType, lsOrderby, chkIsNew.Checked, txtDescHeader.Text, txtDescFooter.Text, txtMetaTitle.Text, txtMetaKeywork.Text, txtMetaDescription.Text);

            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtSubMenuName.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {
                if (ViewState["subMenuID"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtSubMenuName.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtSubMenuName.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.BindSubMenu();
            }
            ViewState["subMenuID"] = null;
            btnSubmit.Text = "Submit";
            txtSubMenuName.Text = string.Empty;
            txtSubMenuURL.Text = string.Empty;
            txtOrderBy.Text = string.Empty;
            txtDescHeader.Text = string.Empty;
            txtDescFooter.Text = string.Empty;
            chkIsNew.Checked = false;
            ddlgendertype.SelectedValue = "0";
            txtMetaTitle.Text = "";
            txtMetaKeywork.Text = "";
            txtMetaDescription.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["subMenuID"] = null;
            btnSubmit.Text = "Submit";
            txtSubMenuName.Text = string.Empty;
            txtSubMenuURL.Text = string.Empty;
            txtOrderBy.Text = string.Empty;
            ddlgendertype.SelectedValue = "0";
            txtDescHeader.Text = string.Empty;
            txtDescFooter.Text = string.Empty;
            chkIsNew.Checked = false;
            txtMetaTitle.Text = "";
            txtMetaKeywork.Text = "";
            txtMetaDescription.Text = "";
            this.BindSubMenu();
        }

        protected void gvSubMenu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            subMenuID = Convert.ToInt32(gvSubMenu.DataKeys[e.RowIndex].Values["sub_menu_id"].ToString());
            string name = gvSubMenu.DataKeys[e.RowIndex].Values["sub_menu_name"].ToString();
            menu_handler menuHandler = new menu_handler();
            bool delete = menuHandler.delete_menu_sub(subMenuID);
            if (delete)
            {
                this.BindSubMenu();
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = name + " " + helper_data.getMessage("msgDeleteSuccessfully");
                gvSubMenu.Focus();
            }
        }

        protected void gvSubMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                subMenuID = Convert.ToInt32(e.CommandArgument);
                menu_handler menuHandler = new menu_handler();
                DataSet ds = menuHandler.get_menu_sub(0, subMenuID,null);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtSubMenuName.Focus();
                        ViewState["subMenuID"] = subMenuID;
                        ddlMenu.SelectedValue = dt.Rows[0]["menu_id"].ToString();
                        txtSubMenuName.Text = dt.Rows[0]["sub_menu_name"].ToString();
                        txtSubMenuURL.Text = dt.Rows[0]["sub_menu_url"].ToString();
                        txtDescHeader.Text = dt.Rows[0]["desc_header"].ToString();
                        txtDescFooter.Text = dt.Rows[0]["desc_footer"].ToString();
                        txtMetaTitle.Text = dt.Rows[0]["meta_title"].ToString();
                        txtMetaKeywork.Text = dt.Rows[0]["meta_keywords"].ToString();
                        txtMetaDescription.Text = dt.Rows[0]["meta_description"].ToString();
                        chkIsNew.Checked = Convert.ToBoolean(dt.Rows[0]["is_new"].ToString());

                        if (dt.Rows[0]["gendertype"] != DBNull.Value)
                            ddlgendertype.SelectedValue = dt.Rows[0]["gendertype"].ToString();
                        txtOrderBy.Text = dt.Rows[0]["orderby"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }

               // ClientScript.RegisterStartupScript(this.GetType(), "alert", "CreateFormatTextbox();", true);
                ScriptManager.RegisterStartupScript(Page, GetType(), "CreateFormatTextbox", "<script>CreateFormatTextbox()</script>", false);
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "sub_menu_id", "tbl_menu_sub");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "sub_menu_id", "tbl_menu_sub");
            }
            this.BindSubMenu();
        }

        protected void gvSubMenu_RowDataBound(object sender, GridViewRowEventArgs e)
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