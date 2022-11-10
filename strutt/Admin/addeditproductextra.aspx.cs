using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using BusinessEntities;
using System.IO;



namespace strutt.Admin
{
    public partial class addeditproductextra : System.Web.UI.Page
    {
        long ProductImageExtraId = 0;
        Int32 productid = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                this.ExtraImage();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindExtraimage()
        {

            //product_handler ProductHandler = new product_handler();
            //DataSet ds = ProductHandler.get_product_image_extra(0);
            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    DataTable dt = ds.Tables[0];
            //    if (dt.Rows.Count > 0)
            //    {
            //        grdextraimage.DataSource = dt;
            //        grdextraimage.DataBind();
            //    }
            //    else
            //    {
            //        grdextraimage.DataSource = null;
            //        grdextraimage.DataBind();
            //    }
            //}
        }

         private void ExtraImage()
         {
            //if (FileUpload1.HasFile == false)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "<script>alert('No File Uploaded.')</script>", false);
            //}
            //else
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                 {
                    string LargeNoImage = "noImage.jpg";
                    string returnMessage = string.Empty;
                    string filename = Path.GetFileName(postedFile.FileName);
                    string contentType = postedFile.ContentType;
                    string strbannerUploadTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                    LargeNoImage = FileUpload1 + "_" + strbannerUploadTime;
                    using (Stream fs = postedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            product_handler productHandler = new product_handler();
                         productextraimage ExtraImage = new productextraimage();
                        BusinessEntities.productextraimage extraimageData = new BusinessEntities.productextraimage();
            
                       // extraimageData.product_image_extra_id = ProductImageExtraId;
                        extraimageData.product_id = productid;
                        extraimageData.thumb_image = LargeNoImage;
                        extraimageData.is_active =chkIsactive.Checked;
                        extraimageData.image_order = 0;
                        extraimageData.created_by = null;


                        int result = productHandler.insert_update_product_image_extra(extraimageData);
                        if (result > 0)
                        {
                            lblMsg.Text = "Updated Successfully.";
                        }
                        else
                        {
                            lblMsg.Text = "Fail.";
                        }
                        this.BindExtraimage();
            }
               lblMsg.Text = returnMessage;
            ViewState["ProductImageExtraId"] = null;
            btnSubmit.Text = "Submit";
            txtimageorder.Text = string.Empty;
            imgLarge.ImageUrl = "images/noImage.jpg";
                   
                }
            }
        }




        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ExtraImage();
        }

    }

       

    }
