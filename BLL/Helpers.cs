using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Helpers
    {
        //public static string getUrl(string UID, string key, int section)
        //{
        //    string str = string.Empty;
        //    key = ReplaceSpecilCharcterToNames(key);
        //    switch (section)
        //    {
        //        case 0:
        //            str = "/product" + "/" + UID + "-" + key;
        //            break;
        //        case 1:
        //            str = "/about-us" + "/" + key;
        //            break;
        //        case 2:
        //            str = UID + "-" + key + "-" + section;
        //            break;
        //    }
        //    return str.ToLower();
        //}

        //public static string ChildCategoryURL(object category, object subcategory, object childcategory, object ProductId)
        public static string getUrl(string category, string subcategory, string childcategory, string key, int section)
        {
            string str = string.Empty;
            key = ReplaceSpecilCharcterToNames(key);
            switch (section)
            {
                case 1:
                    str = category + "/" + subcategory + "/" + childcategory + "/" + key;
                    break;
            }
            return str.ToLower();
        }


        public static string ReplaceSpecilCharcterToNames(string special)
        {
            special = special.ToString();
            special = special.Replace(" ", "-");
            special = special.Replace("&", "-");
            special = special.Replace("--", "-");
            special = special.Replace("---", "-");
            special = special.Replace("----", "-");
            special = special.Replace("-----", "-");
            special = special.Replace("----", "-");
            special = special.Replace("---", "-");
            special = special.Replace("--", "-");
            special = special.Trim();
            special = special.Trim('-');

            return special.Replace(" ", "-");
        }



        public static string GetUrlCustomizeProduct(object category, object subcategory, object childcategory, object ProductName, object productId)
        {
            string strcategory = category.ToString();


            //#category
            strcategory = strcategory.Trim();
            strcategory = strcategory.Trim('-');

            strcategory = strcategory.ToLower();
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strcategory = strcategory.Replace("c#", "C-Sharp");
            strcategory = strcategory.Replace("vb.net", "VB-Net");
            strcategory = strcategory.Replace("asp.net", "Asp-Net");
            strcategory = strcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strcategory.Contains(strChar))
                {
                    strcategory = strcategory.Replace(strChar, string.Empty);
                }
            }
            strcategory = strcategory.Replace(" ", "-");
            strcategory = strcategory.Replace("&", "-");
            strcategory = strcategory.Replace("--", "-");
            strcategory = strcategory.Replace("---", "-");
            strcategory = strcategory.Replace("----", "-");
            strcategory = strcategory.Replace("-----", "-");
            strcategory = strcategory.Replace("----", "-");
            strcategory = strcategory.Replace("---", "-");
            strcategory = strcategory.Replace("--", "-");
            strcategory = strcategory.Trim();
            strcategory = strcategory.Trim('-');


            //subcategory
            string strsubcategory = subcategory.ToString();
            strsubcategory = strsubcategory.Trim();
            strsubcategory = strsubcategory.Trim('-');

            strsubcategory = strsubcategory.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strsubcategory = strsubcategory.Replace("c#", "C-Sharp");
            strsubcategory = strsubcategory.Replace("vb.net", "VB-Net");
            strsubcategory = strsubcategory.Replace("asp.net", "Asp-Net");
            strsubcategory = strsubcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strsubcategory.Contains(strChar))
                {
                    strsubcategory = strsubcategory.Replace(strChar, string.Empty);
                }
            }
            strsubcategory = strsubcategory.Replace(" ", "-");
            strsubcategory = strsubcategory.Replace("&", "-");
            strsubcategory = strsubcategory.Replace("--", "-");
            strsubcategory = strsubcategory.Replace("---", "-");
            strsubcategory = strsubcategory.Replace("----", "-");
            strsubcategory = strsubcategory.Replace("-----", "-");
            strsubcategory = strsubcategory.Replace("----", "-");
            strsubcategory = strsubcategory.Replace("---", "-");
            strsubcategory = strsubcategory.Replace("--", "-");
            strsubcategory = strsubcategory.Trim();
            strsubcategory = strsubcategory.Trim('-');

            //childcategory
            string strchildcategory = childcategory.ToString();
            strchildcategory = strchildcategory.Trim();
            strchildcategory = strchildcategory.Trim('-');

            strchildcategory = strchildcategory.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strchildcategory = strchildcategory.Replace("c#", "C-Sharp");
            strchildcategory = strchildcategory.Replace("vb.net", "VB-Net");
            strchildcategory = strchildcategory.Replace("asp.net", "Asp-Net");
            strchildcategory = strchildcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strchildcategory.Contains(strChar))
                {
                    strchildcategory = strchildcategory.Replace(strChar, string.Empty);
                }
            }
            strchildcategory = strchildcategory.Replace(" ", "-");
            strchildcategory = strchildcategory.Replace("&", "-");
            strchildcategory = strchildcategory.Replace("--", "-");
            strchildcategory = strchildcategory.Replace("---", "-");
            strchildcategory = strchildcategory.Replace("----", "-");
            strchildcategory = strchildcategory.Replace("-----", "-");
            strchildcategory = strchildcategory.Replace("----", "-");
            strchildcategory = strchildcategory.Replace("---", "-");
            strchildcategory = strchildcategory.Replace("--", "-");
            strchildcategory = strchildcategory.Trim();
            strchildcategory = strchildcategory.Trim('-');


            //productname
            string strproductname = ProductName.ToString();
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');

            strproductname = strproductname.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strproductname = strproductname.Replace("c#", "C-Sharp");
            strproductname = strproductname.Replace("vb.net", "VB-Net");
            strproductname = strproductname.Replace("asp.net", "Asp-Net");
            strproductname = strproductname.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strproductname.Contains(strChar))
                {
                    strproductname = strproductname.Replace(strChar, string.Empty);
                }
            }
            strproductname = strproductname.Replace(" ", "-");
            strproductname = strproductname.Replace("&", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("-----", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');


            strcategory = "/" + strcategory + "/" + strsubcategory + "/" + strchildcategory + "/" + strproductname + "-" + productId + ".html";

            return strcategory;
        }

        public static string GetUrlProduct(object category, object subcategory, object ProductName, object productId)
        {
            string strcategory = category.ToString();


            //#category
            strcategory = strcategory.Trim();
            strcategory = strcategory.Trim('-');

            strcategory = strcategory.ToLower();
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strcategory = strcategory.Replace("c#", "C-Sharp");
            strcategory = strcategory.Replace("vb.net", "VB-Net");
            strcategory = strcategory.Replace("asp.net", "Asp-Net");
            strcategory = strcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strcategory.Contains(strChar))
                {
                    strcategory = strcategory.Replace(strChar, string.Empty);
                }
            }
            strcategory = strcategory.Replace(" ", "-");
            strcategory = strcategory.Replace("&", "-");
            strcategory = strcategory.Replace("--", "-");
            strcategory = strcategory.Replace("---", "-");
            strcategory = strcategory.Replace("----", "-");
            strcategory = strcategory.Replace("-----", "-");
            strcategory = strcategory.Replace("----", "-");
            strcategory = strcategory.Replace("---", "-");
            strcategory = strcategory.Replace("--", "-");
            strcategory = strcategory.Trim();
            strcategory = strcategory.Trim('-');


            //subcategory
            string strsubcategory = subcategory.ToString();
            strsubcategory = strsubcategory.Trim();
            strsubcategory = strsubcategory.Trim('-');

            strsubcategory = strsubcategory.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strsubcategory = strsubcategory.Replace("c#", "C-Sharp");
            strsubcategory = strsubcategory.Replace("vb.net", "VB-Net");
            strsubcategory = strsubcategory.Replace("asp.net", "Asp-Net");
            strsubcategory = strsubcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strsubcategory.Contains(strChar))
                {
                    strsubcategory = strsubcategory.Replace(strChar, string.Empty);
                }
            }
            strsubcategory = strsubcategory.Replace(" ", "-");
            strsubcategory = strsubcategory.Replace("&", "-");
            strsubcategory = strsubcategory.Replace("--", "-");
            strsubcategory = strsubcategory.Replace("---", "-");
            strsubcategory = strsubcategory.Replace("----", "-");
            strsubcategory = strsubcategory.Replace("-----", "-");
            strsubcategory = strsubcategory.Replace("----", "-");
            strsubcategory = strsubcategory.Replace("---", "-");
            strsubcategory = strsubcategory.Replace("--", "-");
            strsubcategory = strsubcategory.Trim();
            strsubcategory = strsubcategory.Trim('-');




            //productname
            string strproductname = ProductName.ToString();
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');

            strproductname = strproductname.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strproductname = strproductname.Replace("c#", "C-Sharp");
            strproductname = strproductname.Replace("vb.net", "VB-Net");
            strproductname = strproductname.Replace("asp.net", "Asp-Net");
            strproductname = strproductname.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strproductname.Contains(strChar))
                {
                    strproductname = strproductname.Replace(strChar, string.Empty);
                }
            }
            strproductname = strproductname.Replace(" ", "-");
            strproductname = strproductname.Replace("&", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("-----", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');


            strcategory = "/" + strcategory + "/" + strsubcategory + "/" + strproductname + "-" + productId + ".html";

            return strcategory;
        }

        public static string GetUrlGiftProduct(object category, object brandname, object ProductName, object productId)
        {
            string strcategory = category.ToString();


            //#category
            strcategory = strcategory.Trim();
            strcategory = strcategory.Trim('-');

            strcategory = strcategory.ToLower();
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strcategory = strcategory.Replace("c#", "C-Sharp");
            strcategory = strcategory.Replace("vb.net", "VB-Net");
            strcategory = strcategory.Replace("asp.net", "Asp-Net");
            strcategory = strcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strcategory.Contains(strChar))
                {
                    strcategory = strcategory.Replace(strChar, string.Empty);
                }
            }
            strcategory = strcategory.Replace(" ", "-");
            strcategory = strcategory.Replace("&", "-");
            strcategory = strcategory.Replace("--", "-");
            strcategory = strcategory.Replace("---", "-");
            strcategory = strcategory.Replace("----", "-");
            strcategory = strcategory.Replace("-----", "-");
            strcategory = strcategory.Replace("----", "-");
            strcategory = strcategory.Replace("---", "-");
            strcategory = strcategory.Replace("--", "-");
            strcategory = strcategory.Trim();
            strcategory = strcategory.Trim('-');


            //subcategory
            string strbrandname = brandname.ToString();
            strbrandname = strbrandname.Trim();
            strbrandname = strbrandname.Trim('-');

            strbrandname = strbrandname.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strbrandname = strbrandname.Replace("c#", "C-Sharp");
            strbrandname = strbrandname.Replace("vb.net", "VB-Net");
            strbrandname = strbrandname.Replace("asp.net", "Asp-Net");
            strbrandname = strbrandname.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strbrandname.Contains(strChar))
                {
                    strbrandname = strbrandname.Replace(strChar, string.Empty);
                }
            }
            strbrandname = strbrandname.Replace(" ", "-");
            strbrandname = strbrandname.Replace("&", "-");
            strbrandname = strbrandname.Replace("--", "-");
            strbrandname = strbrandname.Replace("---", "-");
            strbrandname = strbrandname.Replace("----", "-");
            strbrandname = strbrandname.Replace("-----", "-");
            strbrandname = strbrandname.Replace("----", "-");
            strbrandname = strbrandname.Replace("---", "-");
            strbrandname = strbrandname.Replace("--", "-");
            strbrandname = strbrandname.Trim();
            strbrandname = strbrandname.Trim('-');


            //productname
            string strproductname = ProductName.ToString();
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');

            strproductname = strproductname.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strproductname = strproductname.Replace("c#", "C-Sharp");
            strproductname = strproductname.Replace("vb.net", "VB-Net");
            strproductname = strproductname.Replace("asp.net", "Asp-Net");
            strproductname = strproductname.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strproductname.Contains(strChar))
                {
                    strproductname = strproductname.Replace(strChar, string.Empty);
                }
            }
            strproductname = strproductname.Replace(" ", "-");
            strproductname = strproductname.Replace("&", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("-----", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');


            strcategory = "/" + strcategory + "/" + strbrandname + "/" + strproductname + "-" + productId + ".html";

            return strcategory;
        }

        public static string GetUrlGreetingCrd(object category, object subcategory, object childcategory, object ProductName, object productId)
        {
            string strcategory = category.ToString();


            //#category
            strcategory = strcategory.Trim();
            strcategory = strcategory.Trim('-');

            strcategory = strcategory.ToLower();
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strcategory = strcategory.Replace("c#", "C-Sharp");
            strcategory = strcategory.Replace("vb.net", "VB-Net");
            strcategory = strcategory.Replace("asp.net", "Asp-Net");
            strcategory = strcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strcategory.Contains(strChar))
                {
                    strcategory = strcategory.Replace(strChar, string.Empty);
                }
            }
            strcategory = strcategory.Replace(" ", "-");
            strcategory = strcategory.Replace("&", "-");
            strcategory = strcategory.Replace("--", "-");
            strcategory = strcategory.Replace("---", "-");
            strcategory = strcategory.Replace("----", "-");
            strcategory = strcategory.Replace("-----", "-");
            strcategory = strcategory.Replace("----", "-");
            strcategory = strcategory.Replace("---", "-");
            strcategory = strcategory.Replace("--", "-");
            strcategory = strcategory.Trim();
            strcategory = strcategory.Trim('-');


            //subcategory
            string strsubcategory = subcategory.ToString();
            strsubcategory = strsubcategory.Trim();
            strsubcategory = strsubcategory.Trim('-');

            strsubcategory = strsubcategory.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strsubcategory = strsubcategory.Replace("c#", "C-Sharp");
            strsubcategory = strsubcategory.Replace("vb.net", "VB-Net");
            strsubcategory = strsubcategory.Replace("asp.net", "Asp-Net");
            strsubcategory = strsubcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strsubcategory.Contains(strChar))
                {
                    strsubcategory = strsubcategory.Replace(strChar, string.Empty);
                }
            }
            strsubcategory = strsubcategory.Replace(" ", "-");
            strsubcategory = strsubcategory.Replace("&", "-");
            strsubcategory = strsubcategory.Replace("--", "-");
            strsubcategory = strsubcategory.Replace("---", "-");
            strsubcategory = strsubcategory.Replace("----", "-");
            strsubcategory = strsubcategory.Replace("-----", "-");
            strsubcategory = strsubcategory.Replace("----", "-");
            strsubcategory = strsubcategory.Replace("---", "-");
            strsubcategory = strsubcategory.Replace("--", "-");
            strsubcategory = strsubcategory.Trim();
            strsubcategory = strsubcategory.Trim('-');

            //childcategory
            string strchildcategory = childcategory.ToString();
            strchildcategory = strchildcategory.Trim();
            strchildcategory = strchildcategory.Trim('-');

            strchildcategory = strchildcategory.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strchildcategory = strchildcategory.Replace("c#", "C-Sharp");
            strchildcategory = strchildcategory.Replace("vb.net", "VB-Net");
            strchildcategory = strchildcategory.Replace("asp.net", "Asp-Net");
            strchildcategory = strchildcategory.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strchildcategory.Contains(strChar))
                {
                    strchildcategory = strchildcategory.Replace(strChar, string.Empty);
                }
            }
            strchildcategory = strchildcategory.Replace(" ", "-");
            strchildcategory = strchildcategory.Replace("&", "-");
            strchildcategory = strchildcategory.Replace("--", "-");
            strchildcategory = strchildcategory.Replace("---", "-");
            strchildcategory = strchildcategory.Replace("----", "-");
            strchildcategory = strchildcategory.Replace("-----", "-");
            strchildcategory = strchildcategory.Replace("----", "-");
            strchildcategory = strchildcategory.Replace("---", "-");
            strchildcategory = strchildcategory.Replace("--", "-");
            strchildcategory = strchildcategory.Trim();
            strchildcategory = strchildcategory.Trim('-');


            //productname
            string strproductname = ProductName.ToString();
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');

            strproductname = strproductname.ToLower();
            //char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strproductname = strproductname.Replace("c#", "C-Sharp");
            strproductname = strproductname.Replace("vb.net", "VB-Net");
            strproductname = strproductname.Replace("asp.net", "Asp-Net");
            strproductname = strproductname.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strproductname.Contains(strChar))
                {
                    strproductname = strproductname.Replace(strChar, string.Empty);
                }
            }
            strproductname = strproductname.Replace(" ", "-");
            strproductname = strproductname.Replace("&", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("-----", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');


            strcategory = "/" + strcategory + "/" + strsubcategory + "/" + strchildcategory + "-" + strproductname + "-" + productId + ".html";

            return strcategory;
        }

        public static string GetUrlRelationship(object ProductName, object productId)
        {
            //productname
            string strproductname = ProductName.ToString();
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');

            strproductname = strproductname.ToLower();
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strproductname = strproductname.Replace("c#", "C-Sharp");
            strproductname = strproductname.Replace("vb.net", "VB-Net");
            strproductname = strproductname.Replace("asp.net", "Asp-Net");
            strproductname = strproductname.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strproductname.Contains(strChar))
                {
                    strproductname = strproductname.Replace(strChar, string.Empty);
                }
            }
            strproductname = strproductname.Replace(" ", "-");
            strproductname = strproductname.Replace("&", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("-----", "-");
            strproductname = strproductname.Replace("----", "-");
            strproductname = strproductname.Replace("---", "-");
            strproductname = strproductname.Replace("--", "-");
            strproductname = strproductname.Trim();
            strproductname = strproductname.Trim('-');


            strproductname = "/" + strproductname + "-" + productId + ".html";

            return strproductname;
        }

        public static string GetUrltraking(object shipvia, object shipid, object platform)
        {
            if (shipid == null)
                return "";


            if (platform  != null)
            {
                return "http://pickrr.com/tracking/#?tracking_id=" + shipid;
            }
            else
            {
                if (shipvia.Equals("Delhivery"))
                {
                    return "https://www.delhivery.com/track/package/" + shipid;
                }
                else if (shipvia.Equals("FedEx"))
                {
                    return "https://www.fedex.com/apps/fedextrack/index.html?tracknumbers=" + shipid + "&cntry_code=in";
                }
                else if (shipvia.Equals("DTDC"))
                {
                    return "http://track.dtdc.com/ctbs-tracking/customerInterface.tr?submitName=showCITrackingDetails&cType=Consignment&cnNo=" + shipid;
                }

                else if (shipvia.Equals("Ecom Express"))
                {
                    return "https://ecomexpress.in/tracking/?awb_field=" + shipid;
                }
                else if (shipvia.Equals("Express Bees"))
                {
                    return "http://www.xpressbees.com/track-shipment.aspx";
                }
                else if (shipvia.Equals("Gati"))
                {
                    return "https://www.gati.com/track-by-docket/";
                }
                else if (shipvia.Equals("Blue Dart"))
                {
                    return "https://www.bluedart.com/tracking";
                }

                else
                {
                    return "";
                }
            }
        }
    }
}
