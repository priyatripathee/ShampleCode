using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BLL;
using System.Data;
using BusinessEntities;
using DAL;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Web.Script.Services;

namespace strutt.Admin
{
    /// <summary>
    /// Summary description for ServiceProductFeed
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceProductFeed : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public XmlDocument products()
        {
            rss rssObj = new rss();
            rssObj.channel = new Channel();
            Item item;
            //int i = 0;

            customer_handler customerdetailall = new customer_handler();
            DataSet ds = new DataSet();
            ds = customerdetailall.get_customer_detail_all(0);

            if (ds != null && ds.Tables.Count > 0)
            {
                rssObj.version = "2.0";
                rssObj.channel.title = "The Strutt Store";
                rssObj.channel.link = "https://thestruttstore.com";
                rssObj.channel.description = "We at Strutt Handcraft all our products from the finest materials.";
                rssObj.channel.items = new List<Item>();
                //rssObj.channel.items = new Item[ds.Tables[0].Rows.Count];


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    item = new Item();
                    //item.ns.Add("chrec", "a");
                    item.id = row["product_id"].ToString();                               // Product Id
                    item.title = row["product_name"].ToString();                          // Product Title
                    //item.title = row["menu_name"].ToString();                             // Category Name
                    item.description = row["full_description"].ToString();                // Product Description
                    item.link = row["ProductLink"].ToString();                            // Product Link
                    item.image_link = row["Image1"].ToString();                           // Image Link
                    item.brand = "Strutt";                                                // Brand Name
                    item.condition = "New";                                               // Condition
                    item.availability = row["in_stock"].ToString();                       // Available
                    item.Sales_price = row["sale_price"].ToString();  // Sale Price // added on 28-01-2021 as per client request
                    item.price = Convert.ToDouble(row["Price"]).ToString("0.00") + " INR"; // Product Price

                    item.shipping = new Shipping();

                    item.shipping.country = "IN";                                         // Shipping Country
                    item.shipping.service = "Standard";                                   // Shipping Service         --
                    if (Convert.ToDouble(row["Price"]) <= 750)
                        item.shipping.price = "99 INR";                                   // Shipping Price           --
                    else
                        item.shipping.price = "0 INR";                                    // Shipping Price           --

                    item.google_product_category = row["product_type_name"].ToString();  // Google Product Category  --
                    item.custom_label_0 = "Made In India";                                // Custom Label             --

                    rssObj.channel.items.Add(item);
                    //rssObj.channel.items[i] = item;
                    //i++;
                    //XElement nodeToRemove = items;

                    //nodeToRemove.AddBeforeSelf(nodeToRemove.Elements());
                    //nodeToRemove.Remove();
                }
                XmlDocument doc = new XmlDocument();
                XmlSerializer oXmlSerializer = new XmlSerializer(typeof(rss));
                using (var sww = new System.IO.StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        //ns.Add("g", "http://base.google.com/ns/1.0");

                        //oXmlSerializer.Serialize(writer, rssObj, ns);
                        //return  sww.ToString();

                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("g", "http://base.google.com/ns/1.0");
                        oXmlSerializer.Serialize(writer, rssObj, ns);
                        //XmlNodeList nodes = doc.GetElementsByTagName("items");
                        //foreach (XmlNode node in nodes)
                        //{
                        //    node.RemoveAll();
                        //}
                        doc.LoadXml(sww.ToString());


                        // if found....

                        return doc;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public XmlDocument FacebookFeed()

        {
            Fbrss rssObj = new Fbrss();
            rssObj.Fbchannel = new FbChannel();
            FbItem Fbitem;
            atom atom;
            //int i = 0;
            var xml = "<Entity> <WatchList><Match ID=\"1\"><MatchDetails><Reason>asdasd</Reason></MatchDetails></Match></WatchList></Entity>";
            customer_handler customerdetailall = new customer_handler();
            DataSet ds = new DataSet();
            ds = customerdetailall.get_customer_detail_all(0);

            if (ds != null && ds.Tables.Count > 0)
            {
                rssObj.version = "2.0";
                rssObj.Fbchannel.title = "The Strutt Store";
                rssObj.Fbchannel.link = "https://thestruttstore.com";
                rssObj.Fbchannel.description = "We at Strutt Handcraft all our products from the finest materials.";
                //rssObj.Fbchannel.atom = "https://www.mydealsshop.foo/pages/test-feed";


                //rssObj.Fbchannel.atom = "href=\"https://www.mydealsshop.foo/pages/test-feed\" ref=\"self\" type=\"application/rss+xml" ;

                rssObj.Fbchannel.Fbitems = new List<FbItem>();
                //rssObj.channel.items = new Item[ds.Tables[0].Rows.Count];


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Fbitem = new FbItem();
                    Fbitem.item_group_id = "";                                              // item_group_id
                    Fbitem.gtin = "";                                                       // item_group_id
                    Fbitem.google_product_category = row["product_type_name"].ToString();  // Google Product Category  --      
                    Fbitem.id = row["product_id"].ToString();                               // Product Id
                    Fbitem.title = row["product_name"].ToString();                          // Product Title
                    Fbitem.description = row["full_description"].ToString();                // Product Description
                    Fbitem.link = row["ProductLink"].ToString();                            // Product Link
                    Fbitem.image_link = row["Image1"].ToString();                           // Image Link
                    Fbitem.Sales_Price = row["sale_price"].ToString();                           // Sales Price
                    Fbitem.additional_image_link = row["Image2"].ToString();
                    Fbitem.additional_image_link2 = row["Image3"].ToString();
                    Fbitem.color = row["color_name"].ToString();

                    Fbitem.additional_variant_attribute = new FBadditional_variant_attribute();
                    Fbitem.additional_variant_attribute = new FBadditional_variant_attribute();
                    Fbitem.additional_variant_attribute.label = "";
                    Fbitem.additional_variant_attribute.value = "";

                    Fbitem.brand = "Strutt";                                                // Brand Name
                    Fbitem.condition = "New";                                               // Condition
                    Fbitem.availability = row["in_stock"].ToString();                       // Available
                   // Fbitem.price = Convert.ToDouble(row["sale_price"]).ToString("0.00") + " INR"; // added on 27-01-2021 as per client request
                   Fbitem.price = Convert.ToDouble(row["Price"]).ToString("0.00") + " INR"; // Product Price


                    rssObj.Fbchannel.Fbitems.Add(Fbitem);
                    //rssObj.channel.items[i] = item;
                    //i++;
                    //XElement nodeToRemove = items;

                    //nodeToRemove.AddBeforeSelf(nodeToRemove.Elements());
                    //nodeToRemove.Remove();
                }
                XmlDocument doc = new XmlDocument();

                XmlNode rootNode = doc.CreateElement("users");
                XmlNode userNode = doc.CreateElement("user");
                XmlAttribute attribute = doc.CreateAttribute("age");
                attribute.Value = "42";
                userNode.Attributes.Append(attribute);
                userNode.InnerText = "John Doe";
                rootNode.AppendChild(userNode);

                XmlSerializer oXmlSerializer = new XmlSerializer(typeof(Fbrss));
                using (var sww = new System.IO.StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        //ns.Add("g", "http://base.google.com/ns/1.0");

                        //oXmlSerializer.Serialize(writer, rssObj, ns);
                        //return  sww.ToString();

                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("g", "http://base.google.com/ns/1.0");
                        ns.Add("atom", "http://www.w3.org/2005/Atom");
                        oXmlSerializer.Serialize(writer, rssObj, ns);
                        //XmlNodeList nodes = doc.GetElementsByTagName("items");
                        //foreach (XmlNode node in nodes)
                        //{
                        //    node.RemoveAll();
                        //}
                        doc.LoadXml(sww.ToString());
                        // if found....

                        return doc;
                    }
                }
            }
            else
            {
                return null;
            }
        }

    }
}
