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

namespace strutt.Admin
{
    public partial class managesize : System.Web.UI.Page
    {
        long sizeID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                this.BindProductSize();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
        private void BindProductSize()
        {
            product_handler productHandler = new product_handler();
            DataSet ds = productHandler.get_product_size(sizeID, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                grdProductSize.DataSource = dt;
                grdProductSize.DataBind();
            }
            else
            {
                grdProductSize.DataSource = null;
                grdProductSize.DataBind();
            }
        }
        protected void grdProductSize_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //MaterialID = Convert.ToInt32(e.CommandArgument);
                //MaterialHandler materialHandler = new MaterialHandler();
                //DataSet ds = materialHandler.GetMaterials(MaterialID);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    DataTable dt = ds.Tables[0];
                //    txtMaterialName.Text = dt.Rows[0]["MaterialName"].ToString();
                //    btnSubmit.Text = "Update";
                //}
            }
            else if (e.CommandName == "Update")
            {

            }
            else if (e.CommandName == "Cancel")
            {

            }
            else
            {
                if (e.CommandName == "Active")
                {
                    bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "size_id", "tbl_product_size");
                    this.BindProductSize();
                    grdProductSize.Focus();
                }
                else
                {
                    bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "size_id", "tbl_product_size");
                    this.BindProductSize();
                    grdProductSize.Focus();
                }
            }
        }
        protected void grdProductSize_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //check if is in edit mode
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    //HiddenField hfieldEditRating = (HiddenField)e.Row.FindControl("hfieldRating");
                    //DropDownList ddlSubCategories = (DropDownList)e.Row.FindControl("ddlEditRating");
                    ////Bind subcategories data to dropdownlist
                    //ddlSubCategories.SelectedValue = hfieldEditRating.Value;
                }
                else
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
        protected void grdProductSize_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            sizeID = Convert.ToInt64(grdProductSize.DataKeys[e.RowIndex].Values["size_id"].ToString());

            product_handler productHandler = new product_handler();
            bool delete = productHandler.delete_product_size(sizeID);
            if (delete)
            {
                this.BindProductSize();
                grdProductSize.Focus();
            }
        }

        protected void grdProductSize_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProductSize.EditIndex = e.NewEditIndex;
            BindProductSize();
        }

        protected void grdProductSize_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = grdProductSize.Rows[e.RowIndex];

            long sizeId = Convert.ToInt64(grdProductSize.DataKeys[e.RowIndex].Value.ToString());

            HiddenField hfieldEditProductId = (HiddenField)row.FindControl("hfieldproductId");
            long proId = Convert.ToInt64(hfieldEditProductId.Value);
            TextBox EditSize = (TextBox)row.FindControl("txtEditSize");
            TextBox EditPrice = (TextBox)row.FindControl("txtEditPrice");
            TextBox EditDiscount = (TextBox)row.FindControl("txtEditDiscount");
            TextBox EditSalePrice = (TextBox)row.FindControl("txtEditSalePrice");

            product_handler productHandler = new product_handler();
            int result = productHandler.insert_update_product_size(sizeId, proId, EditSize.Text, EditPrice.Text, EditDiscount.Text, EditSalePrice.Text);
            if (result > 0)
            {
                grdProductSize.EditIndex = -1;
                BindProductSize();
            }
        }

        protected void grdProductSize_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProductSize.EditIndex = -1;
            BindProductSize();
        }
    }
}