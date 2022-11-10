using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Xml;
using BusinessEntities;

namespace strutt.Admin
{
    public partial class addeditproduct : System.Web.UI.Page
    {
        private Int64 ProductID
        {
            get
            {
                if (ViewState["ProductID"] != null)
                {
                    return Convert.ToInt64(ViewState["ProductID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value == 0)
                {
                    ViewState["ProductID"] = null;
                }
                else
                {
                    ViewState["ProductID"] = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                this.GetCategory();
                this.BindColors();
                this.BindMaterials();
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                //  this.ExtraImageSave();
                if (Request.QueryString["prodid"] != null)
                {
                    ProductID = Convert.ToInt64(Request.QueryString["prodid"].ToString());
                    this.GetProductDetails(ProductID);
                    divExtraImage.Visible = true;
                    this.GetExtraimage(ProductID);
                }
                else
                    divExtraImage.Visible = false;
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
            //txtFullDescription.Text = "abc";

        }

        #region Control Methods 

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveProductDetails();
            //this.ExtraImageSave();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["prodid"] != null)
            {
                Response.Redirect("manageproduct.aspx?menu_id=" + ddlCategory.SelectedItem.Value);
            }
            else
            {
                Response.Redirect("manageproduct.aspx");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            foreach(DataList li in dlImages.Items )
            {
                string fileName = string.Empty;
                Int64 ProductImageId = Convert.ToInt64(Request.QueryString["prodid"]);
                product_handler productHandler = new product_handler();
                bool result = productHandler.delete_product_image(ProductImageId, ref fileName);
                if (result)
                {
                    // Remove images from directory
                    string pathThumb = Server.MapPath("~/images/Product/Thumb/" + fileName);
                    FileInfo fileThumb = new FileInfo(pathThumb);
                    if (fileThumb.Exists)//check file exsit or not
                    {
                        fileThumb.Delete();
                    }

                    string pathLarge = Server.MapPath("~/images/Product/Large/" + fileName);
                    FileInfo fileLarge = new FileInfo(pathLarge);
                    if (fileThumb.Exists)//check file exsit or not
                    {
                        fileLarge.Delete();
                    }

                    this.GetProductDetails(ProductID);
                }

                //Image imagepath = li.FindControl("img1") as Image;
                //string path = Server.MapPath("~/images/Product/Thumb/" + fileName);
                //if(System.IO.File.Exists(path))
                //{
                //    System.IO.File.Delete(path);

                //}
            }
        }

        protected void dlImages_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                string fileName = string.Empty;
                Int64 ProductImageId = Convert.ToInt64(e.CommandArgument.ToString());
                product_handler productHandler = new product_handler();
                bool result = productHandler.delete_product_image(ProductImageId, ref fileName);
                if (result)
                {
                    // Remove images from directory
                    string pathThumb = Server.MapPath("~/images/Product/Thumb/" + fileName);
                    FileInfo fileThumb = new FileInfo(pathThumb);
                    if (fileThumb.Exists)//check file exsit or not
                    {
                        fileThumb.Delete();
                    }

                    string pathLarge = Server.MapPath("~/images/Product/Large/" + fileName);
                    FileInfo fileLarge = new FileInfo(pathLarge);
                    if (fileThumb.Exists)//check file exsit or not
                    {
                        fileLarge.Delete();
                    }

                    this.GetProductDetails(ProductID);
                }
            }

            if (e.CommandName == "Active")
            {
                product_handler productHandler = new product_handler();
                Int64 ProductImageId = Convert.ToInt64(e.CommandArgument.ToString());
                bool status = productHandler.assign_default_product_image(ProductImageId);
                this.GetProductDetails(ProductID);
            }
            
        }
        //Remove all image 23-09-2020 by divyesh gandhi
        protected void Delete(object sender, EventArgs e)
        {
            Label lbl1 = new Label();
            product_handler productHandler = new product_handler();
            DataSet dsProduct = productHandler.get_product_details(Convert.ToInt32(Request.QueryString["prodid"]));
            if (dsProduct != null && dsProduct.Tables.Count > 0)
            {
                DataTable dtImage = dsProduct.Tables[1];

                foreach (DataRow  row in dtImage.Rows)
                {
                    string fileName = string.Empty;
                    Int64 ProductImageId = Convert.ToInt32(row["product_image_id"]);

                    bool result = productHandler.delete_product_image(ProductImageId, ref fileName);
                    if (result)
                    {
                        // Remove images from directory
                        string pathThumb = Server.MapPath("~/images/Product/Thumb/" + fileName);
                        FileInfo fileThumb = new FileInfo(pathThumb);
                        if (fileThumb.Exists)//check file exsit or not
                        {
                            fileThumb.Delete();
                        }

                        string pathLarge = Server.MapPath("~/images/Product/Large/" + fileName);
                        FileInfo fileLarge = new FileInfo(pathLarge);
                        if (fileThumb.Exists)//check file exsit or not
                        {
                            fileLarge.Delete();
                        }
                    }
                }

                        this.GetProductDetails(ProductID);
            }

        
            }


        protected void dlExtraimage_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                string fileName = string.Empty;
                Int32 Productimageextraid = Convert.ToInt32(e.CommandArgument.ToString());
                product_handler productHandler = new product_handler();
                bool result = productHandler.delete_product_image_extra(Productimageextraid, ref fileName);
                if (result)
                {
                    // Remove images from directory
                    string pathThumb = Server.MapPath("~/images/ExtraImage/" + fileName);
                    FileInfo fileThumb = new FileInfo(pathThumb);
                    if (fileThumb.Exists)//check file exsit or not
                    {
                        fileThumb.Delete();
                    }

                    this.GetExtraimage(ProductID);
                }
            }

            if (e.CommandName == "Active")
            {
                product_handler productHandler = new product_handler();
                Int64 Productimageextraid = Convert.ToInt64(e.CommandArgument.ToString());
                bool status = productHandler.assign_default_product_image(Productimageextraid);
                this.GetExtraimage(ProductID);
            }
        }

        #endregion

        #region Custom Methods 

        private void BindColors()
        {
            tools_handler toolHandler = new tools_handler();
            DataSet ds = toolHandler.get_color(0,1);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ddlColor.DataSource = dt;
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0,new ListItem( "select","0"));
                }
            }

        }

        private void BindMaterials()
        {
            tools_handler toolHandler = new tools_handler();
            DataSet ds = toolHandler.get_material(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                ddlMaterial.DataSource = dt;
                ddlMaterial.DataBind();
                ddlMaterial.Items.Insert(0, new ListItem("Please select", "0"));
            }
        }
        #endregion

        #region ddl Custom Methods 

        private void GetCategory()
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlCategory.DataSource = ds.Tables[0];
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Please select", "0"));
            }
        }

        private void BindSubCategory(Int32 categoryId)
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_sub(categoryId, 0, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlsubCategory.DataSource = ds.Tables[0];
                ddlsubCategory.DataBind();
                ddlsubCategory.Items.Insert(0, new ListItem("Please select", "0"));
            }
            else
            {
                ddlsubCategory.Items.Clear();
                ddlsubCategory.Items.Insert(0, new ListItem("Please select", "0"));
            }
        }

        private void BindChildCategory(Int32 subCatID)
        {
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_child(subCatID, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlChildCategory.DataSource = ds.Tables[0];
                ddlChildCategory.DataBind();
                ddlChildCategory.Items.Insert(0, new ListItem("Please select", "0"));
            }
            else
            {
                ddlChildCategory.Items.Clear();
                ddlChildCategory.Items.Insert(0, new ListItem("Please select", "0"));
            }
        }

        private void BindProductType(Int32 childCatID)
        {
            product_handler productHandler = new product_handler();
            DataSet ds = productHandler.get_product_type(0, childCatID);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlProductType.DataSource = ds.Tables[0];
                ddlProductType.DataBind();
                ddlProductType.Items.Insert(0, new ListItem("Please select", "0"));
            }
            else
            {
                ddlProductType.Items.Clear();
                ddlProductType.Items.Insert(0, new ListItem("Please select", "0"));
            }
        }


        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "0")
            {
                ddlsubCategory.Items.Clear();
                ddlsubCategory.Items.Insert(0, new ListItem("Please select", "0"));

                ddlChildCategory.Items.Clear();
                ddlChildCategory.Items.Insert(0, new ListItem("Please select", "0"));

                ddlProductType.Items.Clear();
                ddlProductType.Items.Insert(0, new ListItem("Please select", "0"));
            }
            else
            {
                BindSubCategory(Convert.ToInt32(ddlCategory.SelectedValue));
            }
        }

        protected void ddlsubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsubCategory.SelectedValue == "0")
            {
                ddlChildCategory.Items.Clear();
                ddlChildCategory.Items.Insert(0, new ListItem("Please select", "0"));

                ddlProductType.Items.Clear();
                ddlProductType.Items.Insert(0, new ListItem("Please select", "0"));
            }
            else
            {
                BindChildCategory(Convert.ToInt32(ddlsubCategory.SelectedValue));
            }
        }

        protected void ddlChildCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChildCategory.SelectedValue == "0")
            {
                ddlProductType.Items.Clear();
                ddlProductType.Items.Insert(0, new ListItem("Please select", "0"));
            }
            else
            {
                BindProductType(Convert.ToInt32(ddlChildCategory.SelectedValue));
            }
        }
        protected void ddlgendertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlgendertype.SelectedValue == "0")
            {
                ddlProductType.Items.Clear();
                ddlProductType.Items.Insert(0, new ListItem("Please select", "0"));
            }
            else
            {
                BindProductType(Convert.ToInt32(ddlgendertype.SelectedValue));
            }
        }

        #endregion

        #region Save Custom Methods 
        private void SaveProductDetails()
        {
            String strCustomMessage = String.Empty;
            Boolean status = false;
            XmlDocument xdProductData = new XmlDocument();
            XmlDocument xdProductImages = new XmlDocument();

            //try
            //{
            DataSet dsProduct = new DataSet();

            DataTable dtProduct = this.ProductsData();
            dsProduct = new DataSet();
            dsProduct.Tables.Add(dtProduct);
            xdProductData.LoadXml(dtProduct.DataSet.GetXml());

            DataTable dtProductImages = this.ProductImagesData();
            dsProduct = new DataSet();
            dsProduct.Tables.Add(dtProductImages);
            xdProductImages.LoadXml(dtProductImages.DataSet.GetXml());

            product_handler productHandler = new product_handler();
            status = productHandler.insert_update_product_details(xdProductData.InnerXml, xdProductImages.InnerXml, ref strCustomMessage);

            if (status)
            {
                if (Request.QueryString["prodid"] != null)
                {
                    Response.Redirect("addeditproduct.aspx?prodid=" + ProductID);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Product has been submited successfully.');", true);
                    txtProductName.Text = "";
                    txtquantity.Text = "";
                    chkIsExclusive.Checked = false;
                    chkIsBestSeller.Checked = false;
                    txtFullDescription.Text = "";
                    txtFeatures.Text = "";
                    ddlgendertype.SelectedValue = "";
                    txtMetaKeyword.Text = "";
                    txtMetaTitle.Text = "";
                    txtMetaDescription.Text = "";
                    txtSize.Text = "";
                    txtWeight.Text = "";
                    txtPrice.Text = "";
                    txtDiscount.Text = "0";
                    txtSalePrice.Text = "";
                    txtOrderby.Text = "";
                    UploadImages.Dispose();
                    Response.Redirect("manageproduct.aspx", false);
                }
            }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private DataTable TableProduct()
        {
            DataTable dtProduct = new DataTable();

            dtProduct.Columns.Add("product_id", System.Type.GetType("System.Int64"));
            dtProduct.Columns.Add("menu_id", System.Type.GetType("System.Int32"));
            dtProduct.Columns.Add("sub_menu_id", System.Type.GetType("System.Int32"));
            dtProduct.Columns.Add("child_menu_id", System.Type.GetType("System.Int32"));
            dtProduct.Columns.Add("product_type_id", System.Type.GetType("System.Int64"));
            dtProduct.Columns.Add("color_id", System.Type.GetType("System.Int32"));
            dtProduct.Columns.Add("material_id", System.Type.GetType("System.Int32"));
            dtProduct.Columns.Add("product_name", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("full_description", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("features", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("gendertype", System.Type.GetType("System.Byte"));
            dtProduct.Columns.Add("size", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("weight", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("price", System.Type.GetType("System.Decimal"));
            dtProduct.Columns.Add("discount", System.Type.GetType("System.Decimal"));
            dtProduct.Columns.Add("sale_price", System.Type.GetType("System.Decimal"));
            dtProduct.Columns.Add("quantity", System.Type.GetType("System.Int32"));
            dtProduct.Columns.Add("is_exclusive", System.Type.GetType("System.Boolean"));
            dtProduct.Columns.Add("is_best_seller", System.Type.GetType("System.Boolean"));
            dtProduct.Columns.Add("meta_keywords", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("meta_title", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("meta_description", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("orderby", System.Type.GetType("System.Int32"));
            dtProduct.Columns.Add("created_by", System.Type.GetType("System.String"));
            dtProduct.Columns.Add("updated_by", System.Type.GetType("System.String"));

            return dtProduct;
        }

        private DataTable TableImages()
        {
            DataTable dtImage = new DataTable();

            dtImage.Columns.Add("product_image_id", System.Type.GetType("System.Int64"));
            dtImage.Columns.Add("thumb_image", System.Type.GetType("System.String"));
            dtImage.Columns.Add("large_image", System.Type.GetType("System.String"));
            dtImage.Columns.Add("created_by", System.Type.GetType("System.String"));
            dtImage.Columns.Add("updated_by", System.Type.GetType("System.String"));

            return dtImage;
        }

        private DataTable ProductImagesData()
        {
            DataTable dtImages = this.TableImages();
            DataRow drImages;
            HttpFileCollection fileCollection = Request.Files;
            string[] arr1 = fileCollection.AllKeys;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadImages = fileCollection[i];
                string thumbImage = string.Empty;
                string largeImage = string.Empty;
                if (uploadImages.ContentLength > 0 || uploadImages.ContentLength != 0)
                {

                    this.ManageImageFormat(uploadImages, ref thumbImage, ref largeImage);

                    drImages = dtImages.NewRow();
                    drImages["product_image_id"] = 0;
                    drImages["thumb_image"] = thumbImage;
                    drImages["large_image"] = largeImage;
                    drImages["created_by"] = Session["AdminUserID"].ToString();
                    drImages["updated_by"] = Session["AdminUserID"].ToString();
                    //drImages["created_by"] = "admin";
                    //drImages["updated_by"] = "admin";
                    dtImages.Rows.Add(drImages);
                }
            }


            dtImages.AcceptChanges();
            return dtImages;
        }

        
        private DataTable ProductsData()
        {
            object lGenderType = DBNull.Value;
            object IColor = DBNull.Value;
            DataTable dtDetails = this.TableProduct();
            DataRow drDetails;

            if (!ddlgendertype.SelectedValue.Equals("0"))
                lGenderType = Convert.ToByte(ddlgendertype.SelectedValue);

            drDetails = dtDetails.NewRow();
            drDetails["product_id"] = ProductID == 0 ? 0 : ProductID;
            drDetails["menu_id"] = Convert.ToInt64(ddlCategory.SelectedItem.Value);
            drDetails["sub_menu_id"] = Convert.ToInt64(Request.Form[ddlsubCategory.UniqueID]);
            drDetails["child_menu_id"] = Convert.ToInt64(Request.Form[ddlChildCategory.UniqueID]);
            drDetails["product_type_id"] = Convert.ToInt64(Request.Form[ddlProductType.UniqueID]);
               drDetails["color_id"] = Convert.ToInt64(ddlColor.SelectedValue);
            drDetails["material_id"] = Convert.ToInt32(ddlMaterial.SelectedItem.Value);
            drDetails["product_name"] = txtProductName.Text;
            drDetails["full_description"] = txtFullDescription.Text;
            drDetails["features"] = txtFeatures.Text;
            drDetails["gendertype"] = lGenderType;
            drDetails["size"] = txtSize.Text;
            drDetails["weight"] = txtWeight.Text;
            drDetails["price"] = Convert.ToDecimal(txtPrice.Text);
            drDetails["discount"] = Convert.ToDecimal(txtDiscount.Text);
            drDetails["sale_price"] = Convert.ToDecimal(txtSalePrice.Text);
            drDetails["quantity"] = Convert.ToInt32(txtquantity.Text);
            drDetails["is_exclusive"] = Convert.ToBoolean(chkIsExclusive.Checked);
            drDetails["is_best_seller"] = Convert.ToBoolean(chkIsBestSeller.Checked);
            drDetails["meta_keywords"] = txtMetaKeyword.Text;
            drDetails["meta_title"] = txtMetaTitle.Text;
            drDetails["meta_description"] = txtMetaDescription.Text;
            if (string.IsNullOrEmpty(txtOrderby.Text))
                drDetails["orderby"] = 0;
            else
                drDetails["orderby"] = txtOrderby.Text;
            drDetails["created_by"] = Session["AdminUserID"].ToString();
            //drDetails["created_by"] = "admin";


            dtDetails.Rows.Add(drDetails);
            dtDetails.AcceptChanges();
            return dtDetails;
        }

        private void ManageImageFormat(HttpPostedFile UploadImage, ref string thumbImage, ref string largeImage)
        {
            if (UploadImage.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(UploadImage.FileName);
                string ext = System.IO.Path.GetExtension(UploadImage.FileName);
                string strThumbTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                Bitmap src = Bitmap.FromStream(UploadImage.InputStream) as Bitmap;

               // Bitmap thumb = ResizeBitmap(src, 353, 470);
                string thumbName = string.Format("{0}{1}_{2}{3}", Server.MapPath("~/images/Product/Thumb/"), fileName, strThumbTime, ext);
                src.Save(thumbName);
                thumbImage = string.Format("{0}_{1}{2}", fileName, strThumbTime, ext);

               // Bitmap large = ResizeBitmap(src, 900, 1200);
                string LargeName = string.Format("{0}{1}_{2}{3}", Server.MapPath("~/images/Product/Large/"), fileName, strThumbTime, ext);
                src.Save(LargeName);
                largeImage = string.Format("{0}_{1}{2}", fileName, strThumbTime, ext);
            }
        }

        private Bitmap ResizeBitmap(Bitmap src, int newWidth, int newHeight)
        {
            Bitmap result = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
            {
                g.DrawImage(src, 0, 0, newWidth, newHeight);
            }
            return result;
        }

        private void GetProductDetails(Int64 productId)
        {
            btnSubmit.Text = "Update";
            product_handler productHandler = new product_handler();
            DataSet dsProduct = productHandler.get_product_details(productId);
            if (dsProduct != null && dsProduct.Tables.Count > 0)
            {
                DataRow row = dsProduct.Tables[0].Rows[0];
                Int32 categoryId = Convert.ToInt32(row["menu_id"].ToString());
                Int32 subCategoryId = Convert.ToInt32(row["sub_menu_id"].ToString());
                Int32 childCategoryId = Convert.ToInt32(row["child_menu_id"].ToString());

                ddlCategory.SelectedValue = row["menu_id"].ToString();
                this.BindSubCategory(categoryId);

                ddlsubCategory.SelectedValue = row["sub_menu_id"].ToString();
                this.BindChildCategory(subCategoryId);

                ddlChildCategory.SelectedValue = row["child_menu_id"].ToString();
                this.BindProductType(childCategoryId);

                ddlProductType.SelectedValue = row["product_type_id"].ToString();

                ddlColor.Text = row["color_id"].ToString();
                ddlMaterial.SelectedValue = row["material_id"].ToString();
                txtProductName.Text = row["product_name"].ToString();
                txtquantity.Text = row["quantity"].ToString();

                if (row["is_exclusive"] != DBNull.Value)
                    chkIsExclusive.Checked = Convert.ToBoolean(row["is_exclusive"]);
                else
                    chkIsExclusive.Checked = false;

                if (row["is_best_seller"] != DBNull.Value)
                    chkIsBestSeller.Checked = Convert.ToBoolean(row["is_best_seller"]);
                else
                    chkIsBestSeller.Checked = false;

                txtFullDescription.Text = row["full_description"].ToString();
                txtFeatures.Text = row["features"].ToString();
                if (row["gendertype"] != DBNull.Value)
                {
                    ddlgendertype.SelectedValue = row["gendertype"].ToString();
                }
                txtMetaKeyword.Text = row["meta_keywords"].ToString();
                txtMetaTitle.Text = row["meta_title"].ToString();
                txtMetaDescription.Text = row["meta_description"].ToString();
                txtSize.Text = row["size"].ToString();
                txtWeight.Text = row["weight"].ToString();
                txtPrice.Text = row["price"].ToString();
                txtDiscount.Text = row["discount"].ToString();
                txtSalePrice.Text = row["sale_price"].ToString();

                lblCustomImg.Text = row["custom_Image"].ToString();
                imgCustomeImage.ImageUrl = "~/images/customImages/" + row["custom_Image"].ToString();

                if (row["orderby"] != DBNull.Value)
                {
                    txtOrderby.Text = row["orderby"].ToString();
                }
                DataTable dtImage = dsProduct.Tables[1];
                dlImages.DataSource = dtImage;
                dlImages.DataBind();
            }
        }

        #region Extra Images
        protected void uploadFile_Click(object sender, EventArgs e)
        {
            
            if (FileUpload1.HasFile)
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    //string fileName = Path.GetFileName(postedFile.FileName);

                    product_handler productHandler = new product_handler();
                    productextraimage ExtraImageSave = new productextraimage();
                    BusinessEntities.productextraimage extraimageData = new BusinessEntities.productextraimage();

                    string dbFileName = "noImage.jpg", fileExt;
                    fileExt = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.')).ToLower();
                    dbFileName = ViewState["ProductID"] + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + fileExt;
                    postedFile.SaveAs(Server.MapPath("~/images/ExtraImage/") + dbFileName);

                    extraimageData.product_id = Convert.ToInt32(ViewState["ProductID"]);
                    extraimageData.thumb_image = dbFileName;
                    extraimageData.is_active = true;
                    extraimageData.image_order = 0;
                    extraimageData.created_by = Session["AdminUserID"].ToString();

                    int result = productHandler.insert_update_product_image_extra(extraimageData);
                    if (result > 0)
                    {
                        lblExmsg.Text = "Updated Successfully.";
                    }
                    else
                    {
                        lblExmsg.Text = "Updated Fail.";
                    }
                }
               
            }
        }
        private void GetExtraimage(long ProductID)
        {
            product_handler productHandler = new product_handler();
            DataSet dsProduct = productHandler.get_product_image_extra(ProductID);
            if (dsProduct != null && dsProduct.Tables.Count > 0)
            {
                DataTable dt = dsProduct.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dlExtraimage.DataSource = dt;
                    dlExtraimage.DataBind();
                }
                else
                {
                    dlExtraimage.DataSource = null;
                    dlExtraimage.DataBind();
                }
            }
        }
        #endregion

        #region Custome Images
        protected void btnCustome_Click(object sender, EventArgs e)
        {
            if (FU_CustomImg.HasFile)
            {
                foreach (HttpPostedFile postedFile in FU_CustomImg.PostedFiles)
                {
                    product_handler productHandler = new product_handler();
                    string dbFileName = "noImage.jpg", fileExt;
                    fileExt = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.')).ToLower();
                    dbFileName = ViewState["ProductID"] + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + fileExt;
                    postedFile.SaveAs(Server.MapPath("~/images/customImages/") + dbFileName);

                    ProductID = Convert.ToInt32(ViewState["ProductID"]);

                    int result = productHandler.update_custome_image(ProductID, dbFileName);
                    if (result > 0)
                    {
                        lblMsg_Custome.ForeColor = System.Drawing.Color.Green;
                        lblMsg_Custome.Text = "Updated Successfully.";

                        Response.Redirect("addeditproduct.aspx?prodid=" + ProductID);
                    }
                    else
                    {
                        lblMsg_Custome.ForeColor = System.Drawing.Color.Red;
                        lblMsg_Custome.Text = "Updated Fail.";
                    }
                }
            }
        }
        #endregion
    }
    #endregion
}