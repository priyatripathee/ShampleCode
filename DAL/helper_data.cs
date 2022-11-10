using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using System.Web;

namespace DAL
{
    public class helper_data
    {
        public static string getMessage(string key)
        {
            
            XPathDocument doc = new XPathDocument(HttpContext.Current.Server.MapPath("/images/Settings/SiteMessage.xml"));
            XPathNavigator nav = doc.CreateNavigator();
            XPathExpression expr1;
            XPathExpression expr2;
            expr1 = nav.Compile("/Message/MessageNode/key");
            expr2 = nav.Compile("/Message/MessageNode/msg");
            XPathNodeIterator iterator1 = nav.Select(expr1);
            XPathNodeIterator iterator2 = nav.Select(expr2);
            while (iterator1.MoveNext())
            {
                iterator2.MoveNext();
                XPathNavigator nav1 = iterator1.Current.Clone();
                XPathNavigator nav2 = iterator2.Current.Clone();
                if (nav1.Value.Trim() == key.Trim())
                {
                    return nav2.Value.Trim();
                }
            }
            return "";
        }
    }
}
