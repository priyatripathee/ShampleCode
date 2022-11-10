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
    public partial class pagelabel : System.Web.UI.Page
    {
        Int32 pagelableID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.BindPageLabel();
                if (Request.QueryString["Id"] != null)
                {

                }
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindPageLabel()
        {
            pagelabel_handler pagelabelHandler = new pagelabel_handler();
            DataSet ds = pagelabelHandler.get_pagelabel(null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    gvPageLabel.DataSource = dt;
                    gvPageLabel.DataBind();
                }
                else
                {
                    gvPageLabel.DataSource = null;
                    gvPageLabel.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["PageLabelID"] != null)
            {
                pagelableID = Convert.ToInt32(ViewState["PageLabelID"].ToString());
            }
            else
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please select record for Edit. Insert not allow.";
                return;
            }

           pagelabel_handler  pagelabelHandler = new pagelabel_handler();
            int result = pagelabelHandler.insert_update_pagelabel(pagelableID,txtpagename.Text.Trim(), txtlabelname.Text.Trim(), txtlabelvalue.Text.Trim(), DateTime.Now,Convert.ToString(Session["AdminUserID"]));
            //if (result == -1)
            //{
            //    lblMsg.ForeColor = System.Drawing.Color.Red;
            //   // lblMsg.Text = "Sorry, " + txtMenuName.Text + " " + helper_data.getMessage("msgAlreadyExist");
            //    return;
            //}
            if (result > 0)
            {
                if (ViewState["PageLabelID"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Updated Successfully.";
                }
                this.BindPageLabel();
            }
            ViewState["PageLabelID"] = null;
            btnSubmit.Text = "Submit";
            txtpagename.Text = string.Empty;
            txtlabelname.Text = string.Empty;
            txtlabelvalue.Text = string.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["PageLabelID"] = null;
            btnSubmit.Text = "Submit";
            txtpagename.Text = string.Empty;
            txtlabelname.Text = string.Empty;
            txtlabelvalue.Text = string.Empty;
            this.BindPageLabel();
        }
        protected void gvPageLabel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                pagelableID = Convert.ToInt32(e.CommandArgument);
                pagelabel_handler pagelabelHandler = new pagelabel_handler();
                DataSet ds = pagelabelHandler.get_pagelabel(pagelableID);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtpagename.Focus();
                        ViewState["PageLabelID"] = pagelableID;
                        txtpagename.Text = dt.Rows[0]["page_name"].ToString();
                        txtlabelname.Text = dt.Rows[0]["label_name"].ToString();
                        txtlabelvalue.Text = dt.Rows[0]["label_value"].ToString();
                        btnSubmit.Text = "Update";
                    }
                }
            }

            this.BindPageLabel();
        }

        protected void gvPageLabel_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 pagelableId = Convert.ToInt32(gvPageLabel.DataKeys[e.RowIndex].Values["label_id"].ToString());
            string name = gvPageLabel.DataKeys[e.RowIndex].Values["page_name"].ToString();
            pagelabel_handler pageHandler = new pagelabel_handler();
            bool delete = pageHandler.delete_pagelabel(pagelableId);
            if (delete)
            {
                this.BindPageLabel();
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Delete Successfully";
                gvPageLabel.Focus();
            }
        }
    }
}