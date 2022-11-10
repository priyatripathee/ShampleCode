using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Connection
    {
        public static string ConnstruttDB
        {
            get
            {               
                try
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
                }
                catch (Exception ce)
                {
                    throw new ApplicationException("Unable to get DB Connection string from Config File. Contact Administrator" + ce);
                }
            }
        }
    }
}
