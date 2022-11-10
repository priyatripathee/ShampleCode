using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;
using System.Data;
using BusinessEntities;
using DAL;

namespace strutt.Admin
{
    public partial class manageUser : System.Web.UI.Page
    {
        Int32 adminId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack )
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                //bindUser();
                txtuname.Text = "";
                txtpass.Text = "";
                bindgrd();  
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
        

        private void bindgrd()
        {
            admin_data_handler admin_data = new admin_data_handler();
            DataTable dt = admin_data.get_user_type(null );
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                    gvUser.DataSource = dt;
                    gvUser.DataBind();
                }
                else
                {
                    gvUser .DataSource = null;
                    gvUser.DataBind();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strpassword = security.Encryptdata(txtpass.Text);

            if (ViewState["admin_id"] != null)
            {
                adminId = Convert.ToInt32(ViewState["admin_id"].ToString());
            }
            
            admin_data_handler adminHandeler = new admin_data_handler();
            string LargeNoImage = "noImage.jpg";
            string strbannerUploadTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            if (Upload_Images.HasFile )
            {
                string fileName = Path.GetFileNameWithoutExtension(Upload_Images.FileName);
                string ext = System.IO.Path.GetExtension(Upload_Images.FileName);
                Upload_Images.SaveAs(Server.MapPath("~/Admin/images/") + fileName + "_" + strbannerUploadTime + ext);
                Upload_Images.SaveAs(Server.MapPath("~/Admin/images/") + Upload_Images.FileName );
                LargeNoImage = fileName + "_" + strbannerUploadTime + ext;
            }
            
            //string lsImgPath = null;
            //if (Upload_Images.HasFile)
            //{
            //    lsImgPath = DateTime.Now.Ticks.ToString() + Upload_Images.FileName.Substring(Upload_Images.FileName.LastIndexOf('.')).ToLower();
            //    Upload_Images.SaveAs(Server.MapPath("~/images/") + lsImgPath);
            //    Lib.Common.makeThumbnail(Server.MapPath("~/download/gallery/") + lsImgPath,
            //        Server.MapPath("~/download/gallery/") + "_" + lsImgPath, 120, 80, true, Lib.Common.SizeOption.GenerateHeight);
            //}

            long  result = adminHandeler.insert_update_admin(adminId, txtuname.Text, strpassword , txtfname.Text, txtlname.Text, Convert.ToInt32(chkIsActive.Checked), Session["AdminUserID"].ToString(), LargeNoImage , Convert.ToInt32(ddluser.SelectedValue));
            if (result == -1)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Sorry, " + txtuname.Text + " " + helper_data.getMessage("msgAlreadyExist");
                return;
            }
            if (result > 0)
            {
                if (ViewState["admin_id"] != null)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtuname .Text + " " + helper_data.getMessage("msgUpdatedSuccessfully");
                    lblMsg.Visible = true;
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = txtuname.Text + " " + helper_data.getMessage("msgSavedSuccessfully");
                }
                this.bindgrd();
            }
            ViewState["admin_id"] = null;
            btnSubmit.Text = "Submit";
            txtuname .Text = string.Empty;
            Response.Redirect("manageUser.aspx");
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
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

                Label lbltype = (Label)e.Row.FindControl("lbltype");
                if (lbltype.Text == "1")
                {
                    lbltype.Text = "super Admin";
                }
                if (lbltype.Text == "2")
                {
                    lbltype.Text = "Admin";
                }
                if (lbltype.Text == "3")
                {
                    lbltype.Text = "Oprator";
                }

                //HiddenField hf = (HiddenField)e.Row.FindControl("lblisActive");
                //if(lblisActive.Value == "1")
                //{
                //    chkIsActive.Checked = true;
                //}
                //else
                //{
                //    chkIsActive.Checked = false;
                //}
            }
        }

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                Int32 admin_Id = Convert.ToInt32(e.CommandArgument);
                admin_data_handler adminHandler = new admin_data_handler();
                DataSet ds = adminHandler.get_admin(admin_Id);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ViewState["admin_id"] = admin_Id;
                        txtuname.Focus();
                        txtuname.Text = dt.Rows[0]["user_name"].ToString();
                        txtfname.Text= dt.Rows[0]["first_name"].ToString();
                        txtlname.Text = dt.Rows[0]["last_name"].ToString();
                        txtpass.Text = dt.Rows[0]["password"].ToString();
                        ddluser.SelectedValue = dt.Rows[0]["user_type"].ToString();
                        if (dt.Rows[0]["is_active"] == DBNull.Value || Convert.ToBoolean(dt.Rows[0]["is_active"]) == false)
                            chkIsActive.Checked = false ;
                        else 
                            chkIsActive.Checked = true ;
                        lblLargeImg.Text = dt.Rows[0]["profile_image"].ToString();
                        imgLarge.ImageUrl = "~/Admin/images/" + dt.Rows[0]["profile_image"].ToString();
                        txtpass.Visible = true;
                        btnSubmit.Text = "Update";
                    }
                }
            }
            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "admin_id", "tbl_admin");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "admin_id", "tbl_admin");
            }
            this.bindgrd();
        }

        protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string returnMessage = string.Empty;
            string imageName = string.Empty;

            Int32  adminId = Convert.ToInt32(gvUser.DataKeys[e.RowIndex].Values["admin_id"].ToString());
            string userName = gvUser.DataKeys[e.RowIndex].Values["user_name"].ToString();
            //banner_handler bannerHandler = new banner_handler();
            admin_data_handler adminHandler = new admin_data_handler();

            bool delete = adminHandler.delete_superadmin(adminId);
            if (delete)
            {
                string imagepath = Server.MapPath("~//Admin/images//" + imageName);
                FileInfo file = new FileInfo(imagepath);
                if (file.Exists)
                {
                    file.Delete();
                }

                this.bindgrd();
            }

            lblMsg.Text = returnMessage;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["admin_id"] = null;
            btnSubmit.Text = "Submit";
            Response.Redirect("manageUser.aspx"); 
        }
    }
}