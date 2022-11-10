
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
    public partial class coupon : System.Web.UI.Page
    {
        Int32 couponId = 0;
        string CreatedBy = "admin";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindMenu();
                this.BindCoupon();
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
                    ddlMenu.Items.Insert(0, new ListItem("select menu", "0"));
                    ddlSubMenu.Items.Insert(0, new ListItem("select sub menu", "0"));
                    ddlChildMenu.Items.Insert(0, new ListItem("select child menu", "0"));
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
            ddlChildMenu.Items.Clear();
            ddlChildMenu.Items.Insert(0, new ListItem("select child menu", "0"));

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
                    ddlSubMenu.Items.Insert(0, new ListItem("select sub menu", "0"));
                }
                else
                {
                    ddlSubMenu.DataSource = null;
                    ddlSubMenu.DataBind();
                    ddlSubMenu.Items.Insert(0, "no data");
                }
            }
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMenu.SelectedValue == "select menu")
            {
                ddlSubMenu.Items.Clear();
                ddlSubMenu.Items.Insert(0, new ListItem("select sub menu", "0"));

                ddlChildMenu.Items.Clear();
                ddlChildMenu.Items.Insert(0, new ListItem("select child menu", "0"));
            }
            else
            {
                bindSubMenu(Convert.ToInt32(ddlMenu.SelectedValue));
            }
        }

        protected void ddlSubMenu_SelectIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubMenu.SelectedValue == "0")
            {
                ddlChildMenu.Items.Clear();
                ddlChildMenu.Items.Insert(0, new ListItem("select child menu", "0"));
            }
            else
            {
                bindChildMenu(Convert.ToInt32(ddlSubMenu.SelectedValue));
            }
        }


        private void bindChildMenu(int subid)
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_child(subid, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlChildMenu.DataSource = dt;
                    ddlChildMenu.DataTextField = "child_name";
                    ddlChildMenu.DataValueField = "child_menu_id";
                    ddlChildMenu.DataBind();
                    ddlChildMenu.Items.Insert(0, new ListItem("select child menu", "0"));
                }
                else
                {
                    ddlChildMenu.DataSource = null;
                    ddlChildMenu.DataBind();
                    ddlChildMenu.Items.Insert(0, "no data");
                }
            }
        }


        private void BindCoupon()
        {
            tools_handler toolsHandler = new tools_handler();
            DataSet ds = toolsHandler.get_coupon(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    gvCoupon.DataSource = dt;
                    gvCoupon.DataBind();
                }
                else
                {
                    gvCoupon.DataSource = null;
                    gvCoupon.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["couponId"] != null)
            {
                couponId = Convert.ToInt32(ViewState["couponId"].ToString());
            }

            if (Session["AdminUserID"] != null)
            {
                CreatedBy = Session["AdminUserID"].ToString();
            }

            byte? GenderCode = null;
            if (ddlgendertype.SelectedValue != "0")
                GenderCode = Convert.ToByte(ddlgendertype.SelectedValue);

            tools_handler toolsHandler = new tools_handler();
            int result = toolsHandler.insert_update_coupon(couponId, Convert.ToInt32(ddlSubMenu.SelectedValue), ddlMenu.SelectedItem.Text, Convert.ToInt32(ddlSubMenu.SelectedValue), ddlSubMenu.SelectedItem.Text, Convert.ToInt32(ddlChildMenu.SelectedValue), ddlChildMenu.SelectedItem.Text, txtCouponCode.Text, Convert.ToInt32(txtprice.Text), "", "", CreatedBy, CreatedBy, GenderCode);
            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtCouponCode.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {
                if (ViewState["couponId"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtCouponCode.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtCouponCode.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.BindCoupon();
            }
            ViewState["couponId"] = null;
            btnSubmit.Text = "Submit";
            txtCouponCode.Text = string.Empty;
            txtprice.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["couponId"] = null;
            btnSubmit.Text = "Submit";
            txtCouponCode.Text = string.Empty;
            txtprice.Text = string.Empty;
            ddlgendertype.Items.Clear();
            ddlMenu.ClearSelection();
            ddlSubMenu.Items.Clear();
            ddlSubMenu.Items.Insert(0, new ListItem("select sub menu", "0"));
            ddlChildMenu.Items.Clear();
            ddlChildMenu.Items.Insert(0, new ListItem("select child menu", "0"));
            this.BindCoupon();
        }

        protected void gvCoupon_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            couponId = Convert.ToInt32(gvCoupon.DataKeys[e.RowIndex].Values["coupon_code_id"].ToString());
            string name = gvCoupon.DataKeys[e.RowIndex].Values["coupon_code"].ToString();
            tools_handler toolsHandler = new tools_handler();
            bool delete = toolsHandler.delete_coupon(couponId);
            if (delete)
            {
                this.BindCoupon();
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = name + " " + helper_data.getMessage("msgDeleteSuccessfully");
                gvCoupon.Focus();
            }
        }

        protected void gvCoupon_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                couponId = Convert.ToInt32(e.CommandArgument);
                tools_handler toolsHandler = new tools_handler();
                DataSet ds = toolsHandler.get_coupon(couponId);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtCouponCode.Focus();
                        ViewState["couponId"] = couponId;
                        BindMenu();
                        ddlMenu.SelectedValue = dt.Rows[0]["menu_id"].ToString();
                        bindSubMenu(Convert.ToInt32(ddlMenu.SelectedValue));
                        ddlSubMenu.SelectedValue = dt.Rows[0]["sub_menu_id"].ToString();
                        bindChildMenu(Convert.ToInt32(ddlSubMenu.SelectedValue));
                        ddlChildMenu.SelectedValue = dt.Rows[0]["child_menu_id"].ToString();
                        ddlgendertype.SelectedValue = dt.Rows[0]["gendertype"].ToString();
                        txtCouponCode.Text = dt.Rows[0]["coupon_code"].ToString();
                        txtprice.Text = dt.Rows[0]["price"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "coupon_code_id", "tbl_coupon");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "coupon_code_id", "tbl_coupon");
            }
            this.BindCoupon();
        }

        protected void gvCoupon_RowDataBound(object sender, GridViewRowEventArgs e)
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