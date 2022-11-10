using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Web.Services;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace strutt.Admin
{
    public partial class manageproduct : System.Web.UI.Page
    {
        private static int PageSize = 100;
        long productId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                ViewState["PageNo"] = 1;
                //ViewState["PageSize"] = ddlFindDisplay.SelectedValue;
                ViewState["TotalPage"] = 0;
                if (Request.QueryString["menu_id"] != null)
                {
                    ddlCategory.SelectedValue = Request.QueryString["menu_id"].ToString();
                }
                this.BindCategory();
                this.BindProduct();
                this.BindProductGird();
                //this.BindSubcategory();
                
                //ProductGird();
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
        #region Custom Methods

        private void BindCategory()
        {
            
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu(0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                ddlCategory.DataSource = dt.DefaultView;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "0")
            {
                ddlsubCategory.Items.Clear();
                ddlsubCategory.Items.Insert(0, new ListItem("Please select", "0"));

            }
            else
            {
                BindSubcategory(Convert.ToInt32(ddlCategory.SelectedValue));
            }
        }
        private void BindSubcategory(Int32 categoryId)
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
        private void BindProduct()
        {
            DataTable product = new DataTable();

            product.Columns.Add("quantity");
            product.Columns.Add("product_id");
            product.Columns.Add("gendertype");
            product.Columns.Add("product_name");
            product.Columns.Add("price");
            product.Columns.Add("discount");
            product.Columns.Add("sale_price");
            product.Columns.Add("menu_name");
            product.Columns.Add("sub_menu_name");
            product.Columns.Add("child_name");
            product.Columns.Add("is_active");
            product.Columns.Add("in_stock");
            product.Columns.Add("is_default");
            product.Columns.Add("is_latest");
            product.Columns.Add("is_best_seller");
            product.Columns.Add("is_exclusive");
            product.Columns.Add("created_date");
            product.Rows.Add();

            gvProduct.DataSource = product;
            gvProduct.DataBind();
        }


        #endregion

       
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("addeditproduct.aspx");
        }

        #region Web Methods

        [WebMethod]
        public static ArrayList SubCategory(int menu_id)
        {
            ArrayList list = new ArrayList();
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_sub(menu_id, 0,null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(new ListItem(dt.Rows[i]["sub_menu_name"].ToString(), dt.Rows[i]["sub_menu_id"].ToString()));
                    }
                }
            }

            return list;
        }

        [WebMethod]
        public static ArrayList ChildCategory(int menu_id, int sub_menu_id)
        {
            ArrayList list = new ArrayList();
            menu_handler menuHandler = new menu_handler();
            DataSet ds = menuHandler.get_menu_child(sub_menu_id, 0);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(new ListItem(dt.Rows[i]["child_name"].ToString(), dt.Rows[i]["child_menu_id"].ToString()));
                    }
                }
            }

            return list;
        }

        [WebMethod]
        public static ArrayList ProductType(int menu_id, int sub_menu_id, int child_menu_id)
        {
            ArrayList list = new ArrayList();
            product_handler productTypeHandler = new product_handler();
            DataSet ds = productTypeHandler.get_product_type(0, child_menu_id);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(new ListItem(dt.Rows[i]["product_type_name"].ToString(), dt.Rows[i]["product_type_id"].ToString()));
                    }
                }
            }

            return list;
        }


        [WebMethod]
        public static bool DeleteProduct(long ProductId)
        {
            string fileName = string.Empty;
            long Product_Id = ProductId;
            product_handler productHandler = new product_handler();
            bool result = productHandler.delete_proudct(Product_Id, ref fileName);
            if (result)
            {
                // Remove images from directory
                string pathThumb = HttpContext.Current.Server.MapPath("~/images/Product/Thumb/" + fileName);
                FileInfo fileThumb = new FileInfo(pathThumb);
                if (fileThumb.Exists)//check file exsit or not
                {
                    fileThumb.Delete();
                }

                string pathLarge = HttpContext.Current.Server.MapPath("~//images//Product//Large//" + fileName);
                FileInfo fileLarge = new FileInfo(pathLarge);
                if (fileThumb.Exists)//check file exsit or not
                {
                    fileLarge.Delete();
                }
            }
            return Product_Id > 0;
        }


        [WebMethod]
        public static bool IsActive(long ProductId, int Flag)
        {
            string query = "[pr_product_change_action]";
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", ProductId);
                    cmd.Parameters.AddWithValue("@Flag", Flag);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    return rowsAffected > 0;
                }
            }
        }

        [WebMethod]
        public static bool Stock(long ProductId, int Flag)
        {
            string query = "[pr_change_stock]";
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", ProductId);
                    cmd.Parameters.AddWithValue("@Flag", Flag);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    return rowsAffected > 0;
                }
            }
        }

        [WebMethod]
        public static string GetProduct(int Flag, string menu_id, string sub_menu_id, string child_menu_id, string product_type_id, long is_active, long in_stock, long is_default, long is_latest, long is_best_seller, int pageIndex)
        {
            string query = "[pr_get_search_product_details_admin]";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", Flag);
            cmd.Parameters.AddWithValue("@menu_id", menu_id);
            cmd.Parameters.AddWithValue("@sub_menu_id", sub_menu_id);
            cmd.Parameters.AddWithValue("@child_menu_id", child_menu_id);
            cmd.Parameters.AddWithValue("@product_type_id", product_type_id);
            cmd.Parameters.AddWithValue("@is_active", is_active);
            cmd.Parameters.AddWithValue("@in_stock", in_stock);
            cmd.Parameters.AddWithValue("@is_default", is_default);
            cmd.Parameters.AddWithValue("@is_latest", is_latest);
            cmd.Parameters.AddWithValue("@is_best_seller", is_best_seller);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            return GetData(cmd, pageIndex).GetXml();
        }

        [WebMethod]
        public static bool ChangeDefault(long ProductId, int Flag)
        {
            string query = "[pr_change_default]";
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", ProductId);
                    cmd.Parameters.AddWithValue("@Flag", Flag);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    return rowsAffected > 0;
                }
            }
        }


        [WebMethod]
        public static bool IsLatest(long ProductId, int Flag)
        {
            string query = "[pr_product_change_latest]";
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", ProductId);
                    cmd.Parameters.AddWithValue("@Flag", Flag);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    return rowsAffected > 0;
                }
            }
        }

        [WebMethod]
        public static bool IsBestSeller(long ProductId, int Flag)
        {
            string query = "[pr_product_change_best_seller]";
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", ProductId);
                    cmd.Parameters.AddWithValue("@Flag", Flag);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    return rowsAffected > 0;
                }
            }
        }

        [WebMethod]
        public static bool IsExclusive(long ProductId, int Flag)
        {
            string query = "[pr_product_change_is_exclusive]";
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_id", ProductId);
                    cmd.Parameters.AddWithValue("@Flag", Flag);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    return rowsAffected > 0;
                }
            }
        }

        private static DataSet GetData(SqlCommand cmd, int pageIndex)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds, "tbl_product");
                        DataTable dt = new DataTable("Pager");
                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();
                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["PageSize"] = PageSize;
                        dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                        ds.Tables.Add(dt);
                        return ds;
                    }
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["PageNo"] = 1;
            //ViewState["PageSize"] = ddlFindDisplay.SelectedValue;
            BindProductGird();
        }
        private void BindProductGird()
        {
            
            product_handler productselectHandler = new product_handler();
            
            int? productcategory = null, productsubcategory = null;
            bool? isActive = null, isStock= null, isLatest = null;
           
           
            if (!ddlCategory.SelectedValue.Equals("0"))
                productcategory = Convert.ToInt32(ddlCategory.SelectedValue);

            if (!ddlsubCategory.SelectedValue.Equals("0"))
                productsubcategory = Convert.ToInt32(ddlsubCategory.SelectedValue);
            if (!ddlIsActive.SelectedValue.Equals("-1"))
                isActive = Convert.ToBoolean(ddlIsActive.SelectedValue);
            if (!ddlStock.SelectedValue.Equals("-1"))
                isStock = Convert.ToBoolean(ddlStock.SelectedValue);
            if (!ddlIsLatest.SelectedValue.Equals("-1"))
                isLatest = Convert.ToBoolean(ddlIsLatest.SelectedValue);
            //// int liTotalRec = 0;
            ////// liTotalRec = Convert.ToInt16(loProduct[0].iTotalRec);
            //// ViewState["TotalPage"] = Math.Ceiling(Convert.ToSingle(liTotalRec / Convert.ToSingle(ViewState["PageSize"])));

            //// lblPageNo.Text = Convert.ToString(ViewState["PageNo"]) + "/" + Convert.ToString(ViewState["TotalPage"]);

            //// if (liTotalRec > Convert.ToInt16(ViewState["PageSize"]) * Convert.ToInt16(ViewState["PageNo"]))
            ////     lbNext.Enabled = lbLast.Enabled = true;
            //// else
            ////     lbNext.Enabled = lbLast.Enabled = false;

            //// if (Convert.ToInt16(ViewState["PageNo"]) == 1)
            ////     lbFirst.Enabled = lbPrev.Enabled = false;
            //// else
            ////     lbFirst.Enabled = lbPrev.Enabled = true;


            DataSet ds = productselectHandler.pr_product_serach_admin(null, productcategory, productsubcategory,
                isActive, isStock , isLatest , 1, PageSize);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                }
                else
                {
                    gvProduct.DataSource = null;
                    gvProduct.DataBind();
                }
            }
        }


        //private void ProductGird()
        //{
        //    product_handler productselectHandler = new product_handler();
        //    DataSet ds = productselectHandler.per_product_selectsall(null,null,0,0,0,0, chkIsActive.Checked,chkIsStock.Checked,true, chkIsLatest.Checked,true, 1, PageSize);
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            gvProduct.DataSource = dt;
        //            gvProduct.DataBind();
        //        }
        //        else
        //        {
        //            gvProduct.DataSource = null;
        //            gvProduct.DataBind();
        //        }
        //    }
        //}
        #endregion

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRecored")
            {
                Response.Redirect("~/Admin/addeditproduct.aspx?prodid=" + e.CommandArgument);
                productId = Convert.ToInt32(e.CommandArgument);
            }
            if (e.CommandName == "Stock")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "in_stock", "product_id", "tbl_product");
            }
            if (e.CommandName == "outStock")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "in_stock", "product_id", "tbl_product");
            }

            if (e.CommandName == "Active")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_active", "product_id", "tbl_product");
            }
            if (e.CommandName == "Deactive")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_active", "product_id", "tbl_product");
            }

            if (e.CommandName == "IsLatest")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), true, "is_latest", "product_id", "tbl_product");
            }
            if (e.CommandName == "notLatest")
            {
                bool status = BLL.Utility.change_update_status(Convert.ToInt32(e.CommandArgument), false, "is_latest", "product_id", "tbl_product");
            }
            
            this.BindProductGird();
            
        }

        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton lnk1 = (ImageButton)e.Row.FindControl("imgIsActive");
                Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                if (lblstatus.Text == "False")
                {
                    lnk1.CommandName = "Active";
                }
                if (lblstatus.Text == "True")
                {
                    lnk1.CommandName = "Deactive";
                }
                ImageButton lnk2 = (ImageButton)e.Row.FindControl("imgIsLatest");
                Label lblIsLatest = (Label)e.Row.FindControl("lblIsLatest");
                if (lblIsLatest.Text == "False")
                {
                    lnk2.CommandName = "IsLatest";
                }
                if (lblIsLatest.Text == "True")
                {
                    lnk2.CommandName = "notLatest";
                }
                ImageButton lnk3 = (ImageButton)e.Row.FindControl("imgIsStock");
                Label lblInStock = (Label)e.Row.FindControl("lblInStock");
                if (lblInStock.Text == "False")
                {
                    lnk3.CommandName = "Stock";
                }
                if (lblInStock.Text == "True")
                {
                    lnk3.CommandName = "outStock";
                }
            }
        }

        protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long productId = Convert.ToInt64(gvProduct.DataKeys[e.RowIndex].Values["product_id"].ToString());
            string fileName = string.Empty;
            product_handler productHandler = new product_handler();
            bool delete = productHandler.delete_proudct(productId,ref fileName);
            if (delete)
            {
                string imagepath = Server.MapPath("~//images/Product/Large//" + fileName);
                FileInfo file = new FileInfo(imagepath);
                if(file.Exists )
                {
                    file.Delete();
                }
                this.BindProductGird();
                gvProduct.Focus();
            }
        }

        protected void btnIsActive_Click(object sender, EventArgs e)
        {

        }





        //protected void lbNavButton_Click(object sender, EventArgs e)
        //{
        //    if (sender.Equals(lbFirst))
        //        ViewState["PageNo"] = 1;
        //    else if (sender.Equals(lbPrev))
        //        ViewState["PageNo"] = Convert.ToInt16(ViewState["PageNo"]) - 1;
        //    else if (sender.Equals(lbNext))
        //        ViewState["PageNo"] = Convert.ToInt16(ViewState["PageNo"]) + 1;
        //    else if (sender.Equals(lbLast))
        //        ViewState["PageNo"] = ViewState["TotalPage"];

        //    BindProductGird();
        //}


    }
}