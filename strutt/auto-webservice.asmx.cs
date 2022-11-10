using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BLL;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Data;
using System.Configuration;

namespace strutt
{
    /// <summary>
    /// Summary description for auto_webservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class auto_webservice : System.Web.Services.WebService
    {

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public static List<string> GetAutoCompleteData(string prefix)
        //{
        //    List<string> result = new List<string>();
        //    menu_handler categoryHandler = new menu_handler();
        //    SqlDataReader dr = categoryHandler.Get_AutoSearch(prefix, 0);
        //    while (dr.Read())
        //    {
        //        result.Add(string.Format("{0}", dr["Name"]));
        //    }
        //    return result;
        //}


        [WebMethod]
        public string[] GetCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            DataTable dt = GetRecords(prefixText);
            List<string> items = new List<string>(count);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strName = dt.Rows[i][0].ToString();
                items.Add(strName);
            }
            return items.ToArray();
        }

        protected DataTable GetRecords(string strName)
        {
            string strConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("T_Get_AutoSearch", con);
                cmd.Parameters.Add(new SqlParameter("@SearchText", strName));
                cmd.Parameters.Add(new SqlParameter("@Flag", "0"));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return dt;
        }
    }
}
