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
using System.Data.OleDb;


namespace strutt.Admin
{
    public partial class ImportProduct : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack )
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void BindProduct()
        {
            string lsFileName = "ImportProduct";
            product_handler productHandler = new product_handler();
            DataSet dsProduct = productHandler.pr_get_product_details(0);

            gvdProduct.DataSource = dsProduct;
            gvdProduct.ShowHeader = true;
            gvdProduct.DataBind();

            string lsPath = Server.MapPath("\\Admin\\images\\") + lsFileName + ".xls";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvdProduct.RenderControl(htw);

            if (File.Exists(lsPath))
            {
                File.Delete(lsPath);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(lsPath, true))
            {
                file.Write(sw.ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.BindProduct();
            //divImportbtn.Visible = true;
            gvdProduct.Visible = false;

            //ProductImport();

        }

        protected void btnimport_Click(object sender, EventArgs e)
        {
            ProductImport();
        }

        private void ProductImport()
        {
            String lsFilePath, lsFileName, lsconnStr;
            int liFailRow = 0, liInsertRow = 0, liUpdateRow = 0;
            string lsImagePath;

            lsFileName = "ImportProduct";
            DataTable loStock = null;

            int product_id = 0;
            byte? gendertype = null;
            int orderby = 0, isExclusive = 0;
            byte success = 0;
            string stsize = null, stweight = null, stmetakeywords = null, stmetatitle = null, msg = "";  //shortdescription = null,stspecification = null,

            if (flFile.HasFile)
            {
                try
                {
                    //lsFileName = Path.GetFileNameWithoutExtension(flFile.FileName);
                    // Get file extanstion in lsImagePath variable
                    lsImagePath = flFile.FileName.Substring(flFile.FileName.LastIndexOf('.')).ToLower();
                    lsFilePath = Server.MapPath("~\\Admin\\images\\") + lsFileName + lsImagePath;

                    // Read excel File into DataTable
                    if (lsImagePath == ".xlsx")
                        lsconnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + lsFilePath + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;\"";
                    else if (lsImagePath == ".xls")
                        lsconnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + lsFilePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";
                    else
                    {
                        lblMessage.Text = "Invalid file for import data. Allow only .xlsx.";
                        lblMessage.CssClass = "msg-err";
                        return;
                    }

                    flFile.SaveAs(lsFilePath);
                    OleDbConnection conn = new OleDbConnection(lsconnStr);
                    string strSQL = "SELECT * FROM [" + lsFileName + "$]";
                    OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    loStock = new DataTable();
                    da.Fill(loStock);

                    foreach (DataRow row in loStock.Rows)
                    {
                        product_id = 0;

                        if (row["product_id"] != DBNull.Value)
                            product_id = Convert.ToInt32(row["product_id"]);

                        if (row["menu_name"] == DBNull.Value || string.IsNullOrEmpty(row["menu_name"].ToString()))
                        {
                            msg += string.Format("Product: {0} menu_name not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["sub_menu_name"] == DBNull.Value || string.IsNullOrEmpty(row["sub_menu_name"].ToString()))
                        {
                            msg += string.Format("Product: {0} sub_menu_name not found. ", product_id);
                            liFailRow++;
                            continue;
                        }

                        if (row["product_name"] == DBNull.Value || string.IsNullOrEmpty(row["product_name"].ToString()))
                        {
                            msg += string.Format("Product: {0} product_name not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["material_name"] == DBNull.Value || string.IsNullOrEmpty(row["material_name"].ToString()))
                        {
                            msg += string.Format("Product: {0} material_name not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["quantity"] == DBNull.Value || string.IsNullOrEmpty(row["quantity"].ToString()))
                        {
                            msg += string.Format("Product: {0} quantity not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        //if (row["full_description"] == DBNull.Value || string.IsNullOrEmpty(row["full_description"].ToString()))
                        //{
                        //    msg += string.Format("Product: {0} full_description not found. ", product_id);
                        //    liFailRow++;
                        //    continue;
                        //}
                        if (row["size"] == DBNull.Value || string.IsNullOrEmpty(row["size"].ToString()))
                        {
                            msg += string.Format("Product: {0} size not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["weight"] == DBNull.Value || string.IsNullOrEmpty(row["weight"].ToString()))
                        {
                            msg += string.Format("Product: {0} weight not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["color_name"] == DBNull.Value || string.IsNullOrEmpty(row["color_name"].ToString()))
                        {
                            msg += string.Format("Product: {0} color_name not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["price"] == DBNull.Value || string.IsNullOrEmpty(row["price"].ToString()))
                        {
                            msg += string.Format("Product: {0} price not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["discount"] == DBNull.Value || string.IsNullOrEmpty(row["discount"].ToString()))
                        {
                            msg += string.Format("Product: {0} discount not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["sale_price"] == DBNull.Value || string.IsNullOrEmpty(row["sale_price"].ToString()))
                        {
                            msg += string.Format("Product: {0} sale_price not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        if (row["is_exclusive"] == DBNull.Value || string.IsNullOrEmpty(row["is_exclusive"].ToString()))
                        {
                            msg += string.Format("Product: {0} is_exclusive not found. ", product_id);
                            liFailRow++;
                            continue;
                        }
                        //if (row["gendertype"] == DBNull.Value || string.IsNullOrEmpty(row["gendertype"].ToString()))
                        //{
                        //    msg += string.Format("Product: {0} gendertype not found. ", product_id);
                        //    liFailRow++;
                        //    continue;
                        //}
                        if (row["gendertype"] != DBNull.Value && !string.IsNullOrEmpty(row["gendertype"].ToString()))
                            gendertype = Convert.ToByte(row["gendertype"]);

                        //if (row["short_description"] != DBNull.Value && !string.IsNullOrEmpty(row["short_description"].ToString()))
                        //    shortdescription = Convert.ToString(row["short_description"]);

                        //if (row["full_description"] != DBNull.Value && !string.IsNullOrEmpty(row["full_description"].ToString()))
                        //    fulldescription = Convert.ToString(row["full_description"]);

                        //if (row["specification"] != DBNull.Value && !string.IsNullOrEmpty(row["specification"].ToString()))
                        //    stspecification = Convert.ToString(row["specification"]);

                      
                        if (row["size"] != DBNull.Value && !string.IsNullOrEmpty(row["size"].ToString()))
                            stsize = Convert.ToString(row["size"]);

                        if (row["weight"] != DBNull.Value && !string.IsNullOrEmpty(row["weight"].ToString()))
                            stweight = Convert.ToString(row["weight"]);

                        if (row["meta_keywords"] != DBNull.Value && !string.IsNullOrEmpty(row["meta_keywords"].ToString()))
                            stmetakeywords = Convert.ToString(row["meta_keywords"]);

                        if (row["meta_title"] != DBNull.Value && !string.IsNullOrEmpty(row["meta_title"].ToString()))
                            stmetatitle = Convert.ToString(row["meta_title"]);

                        //if (row["meta_description"] != DBNull.Value && !string.IsNullOrEmpty(row["meta_description"].ToString()))
                        //    stmetadescription = Convert.ToString(row["meta_description"]);

                        //if (row["orderby"] != DBNull.Value && !string.IsNullOrEmpty(row["orderby"].ToString()))
                        //    orderby = Convert.ToInt16(row["orderby"]);

                        if (row["orderby"] != DBNull.Value)
                            orderby = Convert.ToInt32(row["orderby"]);

                        //if (row["features"] != DBNull.Value && !string.IsNullOrEmpty(row["features"].ToString()))
                        //    stfeatures = Convert.ToString(row["features"]);

                        //if (row["updated_by"] != DBNull.Value && !string.IsNullOrEmpty(row["updated_by"].ToString()))
                        //   stupdated_by = Convert.ToString(row["updated_by"]);


                        product_handler productHandler = new product_handler();
                        bool dsProduct = productHandler.insert_update_productimport(product_id, row["menu_name"].ToString(), row["sub_menu_name"].ToString(),
                            row["color_name"].ToString(), row["material_name"].ToString(), row["product_name"].ToString(), stsize, stweight, 
                            Convert.ToSingle(row["price"]), Convert.ToSingle(row["discount"]), Convert.ToSingle(row["sale_price"]),
                            Convert.ToInt32(row["quantity"]), Convert.ToBoolean(row["in_stock"]), Convert.ToBoolean(row["in_wishlist"]), Convert.ToBoolean(row["is_deal"]),
                            Convert.ToBoolean(row["is_latest"]), Convert.ToBoolean(row["is_default"]), Convert.ToBoolean(row["is_active"]),
                            stmetakeywords, stmetatitle, Convert.ToInt32(orderby), row["updated_by"].ToString(), Convert.ToBoolean(row["is_best_seller"]),
                            Convert.ToBoolean(row["is_exclusive"]), gendertype, ref success);

                        if (success == 1)
                        {
                            if (row["product_id"] != DBNull.Value)
                                liUpdateRow++;
                            else
                                liInsertRow++;
                        }
                        else
                            liFailRow++;
                    }

                    lblMessage.Text = "File import Successfully.</br>";
                    lblMessage.Text += string.Format("New inserted row:{0}</br>Updated row:{1}</br>Failed row:{2}</br>", liInsertRow, liUpdateRow, liFailRow);
                    lblMessage.Text += msg;

                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("External table is not in the expected format."))
                    {
                        lblMessage.Text = "Invalid Excel file. First save as your excel file in Standard excel format(.xls or .xlsx) than try again.";
                        lblMessage.CssClass = "msg-err";
                    }
                    else
                    {
                        lblMessage.Text = product_id.ToString() + "Error in Import: " + ex.Message;
                        lblMessage.CssClass = "msg-err";
                    }
                }
                finally
                {
                    loStock = null;
                }
            }
            else
            {
                lblMessage.Text = "Please select Excel file for import data.";
                lblMessage.CssClass = "msg-err";

            }
        }

    }
}
