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
    public partial class material : System.Web.UI.Page
    {
        Int32 matrialId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindMaterial();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindMaterial()
        {
            tools_handler toolsHandler = new tools_handler();
            DataSet ds = toolsHandler.get_material(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    grdMaterial.DataSource = dt;
                    grdMaterial.DataBind();
                }
                else
                {
                    grdMaterial.DataSource = null;
                    grdMaterial.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (ViewState["MatrialId"] != null)
            {
                matrialId = Convert.ToInt32(ViewState["MatrialId"].ToString());
            }

            tools_handler toolsHandler = new tools_handler();

            int result = toolsHandler.insert_update_material(matrialId, txtMaterialName.Text);

            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtMaterialName.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {

                if (ViewState["MatrialId"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtMaterialName.Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtMaterialName.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.BindMaterial();
            }
            ViewState["MatrialId"] = null;
            btnSubmit.Text = "Submit";
            txtMaterialName.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["MatrialId"] = null;
            btnSubmit.Text = "Submit";
            txtMaterialName.Text = string.Empty;
            this.BindMaterial();
        }

        protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            matrialId = Convert.ToInt32(grdMaterial.DataKeys[e.RowIndex].Values["material_id"].ToString());
            string colorName = grdMaterial.DataKeys[e.RowIndex].Values["material_name"].ToString();
            tools_handler toolsHandler = new tools_handler();
            bool delete = toolsHandler.delete_material(matrialId);
            if (delete)
            {
                this.BindMaterial();
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = colorName + " " + helper_data.getMessage("msgDeleteSuccessfully");
                grdMaterial.Focus();
            }
        }

        protected void grdMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                matrialId = Convert.ToInt32(e.CommandArgument);
                tools_handler toolsHandler = new tools_handler();
                DataSet ds = toolsHandler.get_material(matrialId);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtMaterialName.Focus();
                        ViewState["MatrialId"] = matrialId;
                        txtMaterialName.Text = dt.Rows[0]["material_name"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "material_id", "tbl_material");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "material_id", "tbl_material");
            }
            this.BindMaterial();
        }

        protected void grdMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
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