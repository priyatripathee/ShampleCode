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
    public partial class color : System.Web.UI.Page
    {
        Int32 colorId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindColor();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindColor()
        {
            tools_handler toolsHandler = new tools_handler();
            DataSet ds = toolsHandler.get_color(0,1);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    grdColor.DataSource = dt;
                    grdColor.DataBind();
                }
                else
                {
                    grdColor.DataSource = null;
                    grdColor.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            if (ViewState["ColorId"] != null)
            {
                colorId = Convert.ToInt32(ViewState["ColorId"].ToString());
            }

            tools_handler toolsHandler = new tools_handler();

            int result = toolsHandler.insert_update_color(colorId, txtColorName.Text, txtColorCode.Text);

            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtColorName.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {

                if (ViewState["ColorId"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtColorName.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtColorName.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.BindColor();
            }
            ViewState["ColorId"] = null;
            btnSubmit.Text = "Submit";
            txtColorName.Text = string.Empty;
            txtColorCode.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["ColorId"] = null;
            btnSubmit.Text = "Submit";
            txtColorName.Text = string.Empty;
            txtColorCode.Text = string.Empty;
            this.BindColor();
        }

        protected void grdColor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 colorid = Convert.ToInt32(grdColor.DataKeys[e.RowIndex].Values["color_id"].ToString());
            string colorName = grdColor.DataKeys[e.RowIndex].Values["color_name"].ToString();
            tools_handler toolsHandler = new tools_handler();
            bool delete = toolsHandler.delete_color(colorid);
            if (delete)
            {
                this.BindColor();
                lblMsg.Text = colorName + " " + helper_data.getMessage("msgDeleteSuccessfully");
                grdColor.Focus();
            }
        }

        protected void grdColor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                Int32 colorId = Convert.ToInt32(e.CommandArgument);
                tools_handler toolsHandler = new tools_handler();
                DataSet ds = toolsHandler.get_color(colorId,1);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtColorName.Focus();
                        ViewState["ColorId"] = colorId;
                        txtColorName.Text = dt.Rows[0]["color_name"].ToString();
                        txtColorCode.Text = dt.Rows[0]["color_code"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "color_id", "tbl_color");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "color_id", "tbl_color");
            }
            this.BindColor();
        }

        protected void grdColor_RowDataBound(object sender, GridViewRowEventArgs e)
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