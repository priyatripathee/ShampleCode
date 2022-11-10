using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using BusinessEntities;
namespace strutt
{
    public partial class sitemap : System.Web.UI.Page
    {
        public string GenderType { get; set; }
        byte? gid = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMenu();
        }
        private void LoadMenu()
        {
            menu_handler lordmenuHandler = new menu_handler();
            DataTable dt = lordmenuHandler.get_menu_sub(0, 0, gid).Tables[0];

            DataColumn colGender = new DataColumn("gender", typeof(string));
            //if (Request.QueryString["gid"] != null && Request.QueryString["gid"].Equals("1"))
            //    colGender.DefaultValue = "men";
            //else
            //    colGender.DefaultValue = "women";

            colGender.DefaultValue = GenderType;
            dt.Columns.Add(colGender);

            DataView dv = dt.DefaultView;
            dv.RowFilter = "menu_id = 1 AND is_active=1";        // Kalpesh: Don't change this menu_id = 1 (This is Live database ID)
            rptMenu1.DataSource = dv;
            rptMenu1.DataBind();

            dv.RowFilter = "menu_id = 2002 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2002 (This is Live database ID)
            rptMenu2.DataSource = dv;
            rptMenu2.DataBind();

            dv.RowFilter = "menu_id = 2005 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2005 (This is Live database ID)
            rptMenu5.DataSource = dv;
            rptMenu5.DataBind();

            dv.RowFilter = "menu_id = 2006 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2006 (This is Live database ID)
            rptMenu6.DataSource = dv;
            rptMenu6.DataBind();

            dv.RowFilter = "menu_id = 1002 AND is_active=1";        // Kalpesh: Don't change this menu_id = 1002 (This is Live database ID)
            rptMenu3.DataSource = dt;
            rptMenu3.DataBind();

            dv.RowFilter = "menu_id = 2004 AND is_active=1";        // Kalpesh: Don't change this menu_id = 2004 (This is Live database ID)
            rptMenu4.DataSource = dt;
            rptMenu4.DataBind();

            //dv.RowFilter = "menu_id = 4 AND is_active=1";
            //rptMenu4.DataSource = dt;
            //rptMenu4.DataBind();

            //dv.RowFilter = "menu_id = 3 AND is_active=1";
            //rptMenu3.DataSource = dt;
            //rptMenu3.DataBind();
        }
    }
}