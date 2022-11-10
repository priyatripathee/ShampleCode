using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessEntities
{
    //[XmlRoot("rss", Namespace = "http://base.google.com/ns/1.0")]
    public class rss
    {
        [XmlAttribute]
        public string version { get; set; }
        public Channel channel { get; set; }
    }
    public class Channel
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        //public Item[] items { get; set; }
        [XmlElement("item")]
        public List<Item> items { get; set; }
    }

    [XmlRoot("g", Namespace = "http://base.google.com/ns/1.0")]
    public class Item
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string image_link { get; set; }
        public string brand { get; set; }
        public string condition { get; set; }
        public string availability { get; set; }
        public string price { get; set; }
        public string Sales_price { get; set; }
        public Shipping shipping { get; set; }
        public string google_product_category { get; set; }
        public string custom_label_0 { get; set; }
    }
    public class Shipping
    {
        public string country { get; set; }
        public string service { get; set; }
        public string price { get; set; }
    }


}
