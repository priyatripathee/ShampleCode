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
using DAL;
using System.Xml;
using System.Xml.Serialization;



namespace strutt.Admin
{
    public partial class customerdetail : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] == null)
                Response.Redirect("../account/Login.aspx");

            if (!IsPostBack)
            {
                lbl_lastmonth.Text = Session["lastMonth"].ToString();
                lbl_curentmonth.Text = Session["currentMonth"].ToString();
                this.bindcustomerdetail();
                if (Session["Role"].ToString() == "Admin")
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.bindcustomerdetail();

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
           // txtemail.Text = "";
            this.filedownlord();
        }

        private void filedownlord()
        {
            customer_handler customerdetailall = new customer_handler();
            DataSet ds = new DataSet();
            ds = customerdetailall.get_customer_detail_all(0);
            grdcustomerdetails.DataSource = ds;
            grdcustomerdetails.ShowHeader = true;
            grdcustomerdetails.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=CustomerOrder_" + DateTime.Now.ToShortDateString() + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sWriter);
            System.Web.UI.HtmlControls.HtmlForm hForm = new System.Web.UI.HtmlControls.HtmlForm();
            grdcustomerdetails.Parent.Controls.Add(hForm);
            hForm.Attributes["runat"] = "server";
            hForm.Controls.Add(grdcustomerdetails);
            hForm.RenderControl(hTextWriter);
            Response.Write(sWriter.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
        }

        private void bindcustomerdetail()
        {
            //DateTime? Fromdate = null;
            //DateTime? Todate = null;
            //string Statusorder = null;
            //string email = null;
            //if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
            //{
            //    Fromdate = Convert.ToDateTime(txtfromdate.Text);
            //    Todate = Convert.ToDateTime(txttodate.Text + " 23:59:59");
            //}
            //if (!string.IsNullOrEmpty(txtemail.Text))
            //{
            //    email = txtemail.Text;
            //}
            //if (ddlStatusOrder.SelectedItem.Text != "All")
            //{
            //    Statusorder = ddlStatusOrder.SelectedItem.Text;
            //}
            customer_handler customerdetailall = new customer_handler();
            DataSet ds = new DataSet();
            ds = customerdetailall.get_customer_detail_all(0);

            if (ds != null && ds.Tables.Count > 0)
            {

                DataTable dt = ds.Tables[0];
                lbl_total_records.Text = "Total " + dt.Rows.Count + " recods";
                grdcustomerdetails.DataSource = dt;
                grdcustomerdetails.DataBind();
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "data not founds this date. Please search other date.";
                grdcustomerdetails.DataSource = null;
                grdcustomerdetails.DataBind();
            }
        }

        //private string convertXml()
        //{
        //    customer_handler customer = new customer_handler();
        //    DataSet ds = new DataSet();
        //    ds = customer.get_customer_detail_all(0);
        //    DataTable dt = ds.Tables[1];
            
        //    List<channel> cha = new List<channel>();
        //    channel ch;
            
        //    foreach(DataRow row in dt.Rows)
        //    {
        //        ch = new channel();
                
        //        ch.title = row["product_type_name"].ToString();
        //        ch.link = row["product_type_url"].ToString();
        //        ch.description = row["full_description"].ToString();
                    
               

        //    }
             
            

        //    XmlSerializer oXmlSerializer = new XmlSerializer(typeof(Patient));
        //    using (var sww = new System.IO.StringWriter())
        //    {
        //        using (XmlWriter writer = XmlWriter.Create(sww))
        //        {
        //            oXmlSerializer.Serialize(writer, dt); //TODO: Check 3rd parameter
        //            return sww.ToString();
        //        }
        //    }
        //}

      
    }
}