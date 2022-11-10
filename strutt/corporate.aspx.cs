using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace strutt
{
    public partial class corporate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLookbook();
            }
        }
        private void BindLookbook()
        {
            tools_handler toolsHandler = new tools_handler();
            DataSet ds = toolsHandler.get_corporate(0, 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    rpt_Corporate.DataSource = dt;
                    rpt_Corporate.DataBind();
                }
                else
                {
                    rpt_Corporate.DataSource = null;
                    rpt_Corporate.DataBind();
                }
            }
        }
    }
}