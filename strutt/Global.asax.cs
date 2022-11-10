using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Data;
using System.Text.RegularExpressions;
using DAL;
using BLL;

namespace strutt
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            //Exception ex = Server.GetLastError();
            //DAL.Utility.EmailError(ex, Request, User.Identity.Name);
            //Response.Redirect("~/ErrorPage.aspx");
        }

        void Session_Start(object sender, EventArgs e)
        {
            //Session.Remove("Cart");
            //Session.Abandon();

            // Code that runs when a new session is started
            if (Session["Cart"] == null)
            {
                DataTable dtCart = new DataTable("Cart");
                dtCart.Columns.Add("menu_name", typeof(string));
                dtCart.Columns.Add("sub_menu_name", typeof(string));
                dtCart.Columns.Add("child_name", typeof(string));
                dtCart.Columns.Add("product_id", typeof(Int64));
                dtCart.Columns.Add("product_name", typeof(string));
                dtCart.Columns.Add("thumb_image", typeof(string));
                dtCart.Columns.Add("weight", typeof(string));
                dtCart.Columns.Add("size", typeof(string));
                dtCart.Columns.Add("color_name", typeof(string));
                dtCart.Columns.Add("sale_price", typeof(decimal));
                dtCart.Columns.Add("discount", typeof(decimal));
                dtCart.Columns.Add("coupon_discount", typeof(decimal));
                dtCart.Columns.Add("quantity", typeof(Int32));
                dtCart.Columns.Add("Total", typeof(decimal));
                dtCart.Columns.Add("shipping_price", typeof(decimal));
                dtCart.Columns.Add("gendertype", typeof(byte));
                dtCart.Columns.Add("custom_bag_param", typeof(string));
                dtCart.Columns.Add("custom_bag_price", typeof(decimal));
                dtCart.Columns.Add("x_point", typeof(float));
                dtCart.Columns.Add("y_point", typeof(float));
                dtCart.Columns["quantity"].DefaultValue = 1;
                dtCart.Columns["product_id"].Unique = true;
                Session["Cart"] = dtCart;
            }

            

            //System.IO.File.AppendAllText(Server.MapPath("~/App_Data/log.txt"), "\n\n" + Request.Url.ToString());
            //string redirectUrl;

            //if (!Request.IsLocal && !Request.IsSecureConnection)
            //{
            //    redirectUrl = Request.Url.ToString().Replace("http:", "https:");
            //    Response.Redirect(redirectUrl, false);
            //    HttpContext.Current.ApplicationInstance.CompleteRequest();
            //}

            //if (!Request.IsLocal && Request.Url.ToString().ToLower().Contains("www"))
            //{
            //    redirectUrl = Request.Url.ToString().Replace("www.", "");
            //    if (!Request.IsSecureConnection)
            //        redirectUrl = redirectUrl.Replace("http:", "https:");

            //    Response.Redirect(redirectUrl, false);
            //    HttpContext.Current.ApplicationInstance.CompleteRequest();
            //}

            //System.IO.File.AppendAllText(Server.MapPath("~/App_Data/log.txt"), redirectUrl);
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
        void Application_BeginRequest(object sender, EventArgs e)
        {
            // Code that runs on every request

            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://www.thestruttstore.com"))
                Response.RedirectPermanent(Request.Url.ToString().ToLower().Replace("http://www.thestruttstore.com", "https://thestruttstore.com"), true);

            else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://thestruttstore.com"))
                Response.RedirectPermanent(Request.Url.ToString().ToLower().Replace("http://thestruttstore.com", "https://thestruttstore.com"), true);

            else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://thestruttstore.com/Default.aspx"))
                Response.RedirectPermanent(Request.Url.ToString().ToLower().Replace("https://thestruttstore.com/Default.aspx", "https://thestruttstore.com"), true);

            else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://www.thestruttstore.com"))
                Response.RedirectPermanent(Request.Url.ToString().ToLower().Replace("https://www.thestruttstore.com", "https://thestruttstore.com"), true);

        }

    }
}
