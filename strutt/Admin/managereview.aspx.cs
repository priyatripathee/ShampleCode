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
    public partial class managereview : System.Web.UI.Page
    {
        long ReviewID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindProductReview();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
        private void BindProductReview()
        {
            review_handler reviewHandler = new review_handler();
            DataSet ds = reviewHandler.get_product_review(ReviewID, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                grdProductReview.DataSource = dt;
                grdProductReview.DataBind();
            }
            else
            {
                grdProductReview.DataSource = null;
                grdProductReview.DataBind();
            }
        }
        protected void grdProductReview_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "review_id", "tbl_product_review");
                    this.BindProductReview();
                    grdProductReview.Focus();
                }
                else
                {
                    bool status = Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "review_id", "tbl_product_review");
                    this.BindProductReview();
                    grdProductReview.Focus();
                }
            }
        }
        protected void grdProductReview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //check if is in edit mode
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    HiddenField hfieldEditRating = (HiddenField)e.Row.FindControl("hfieldRating");
                    DropDownList ddlSubCategories = (DropDownList)e.Row.FindControl("ddlEditRating");
                    //Bind subcategories data to dropdownlist
                    ddlSubCategories.SelectedValue = hfieldEditRating.Value;
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
        protected void grdProductReview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ReviewID = Convert.ToInt32(grdProductReview.DataKeys[e.RowIndex].Values["review_id"].ToString());

            review_handler reviewHandler = new review_handler();
            bool delete = reviewHandler.delete_product_review(ReviewID);
            if (delete)
            {
                this.BindProductReview();
                grdProductReview.Focus();
            }
        }

        protected void grdProductReview_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProductReview.EditIndex = e.NewEditIndex;
            BindProductReview();
        }

        protected void grdProductReview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = grdProductReview.Rows[e.RowIndex];

            long revId = Convert.ToInt64(grdProductReview.DataKeys[e.RowIndex].Value.ToString());
            TextBox EditUserName = (TextBox)row.FindControl("txtEditUName");
            TextBox EditTitleName = (TextBox)row.FindControl("txtEditTitleName");
            TextBox EditReviewName = (TextBox)row.FindControl("txtEditReviewName");
            DropDownList ddlEditRating = (DropDownList)row.FindControl("ddlEditRating");



            review_handler reviewHandler = new review_handler();
            BusinessEntities.review review = new BusinessEntities.review();
            review.review_id = revId;
            review.user_name = EditUserName.Text;
            review.rating = Convert.ToInt64(ddlEditRating.SelectedValue);
            review.title = EditTitleName.Text;
            review.reviews = EditReviewName.Text;
            bool resultReview = reviewHandler.update_reviews(review);
            if (resultReview == true)
            {
                grdProductReview.EditIndex = -1;
                BindProductReview();
            }
        }

        protected void grdProductReview_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProductReview.EditIndex = -1;
            BindProductReview();
        }
    }
}