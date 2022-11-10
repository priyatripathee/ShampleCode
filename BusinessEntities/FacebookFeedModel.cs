using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BusinessEntities
{
    class FacebookFeedModel
    {
    }
    [XmlRoot("rss")]
    public class Fbrss
    {
        [XmlAttribute]
        public string version { get; set; }
        [XmlElement("Channel")]
        public FbChannel Fbchannel { get; set; }
    }
    public class FbChannel
    {
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        [XmlElement("atom:link")]
        public string atom { get; set; }
        //public Item[] items { get; set; }
        [XmlElement("item")]
        public List<FbItem> Fbitems { get; set; }
       
    }
    public class atom
    {
        public string href { get; set; }
        [XmlAttribute]
        public string rel { get; set; }
        [XmlAttribute]
        public string type { get; set; }
    }
    [XmlRoot("g", Namespace = "http://base.google.com/ns/1.0" )]
    public class FbItem
    {
        public string item_group_id { get; set; }
        public string gtin { get; set; }
        public string google_product_category { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string image_link { get; set; }
        [XmlElement(ElementName="additional_image_link", Namespace = "")]
        public string additional_image_link { get; set; }
        
        [XmlElement(ElementName = "additional_image_link2", Namespace = "")]
        public string additional_image_link2 { get; set; }
        [XmlElement("color", Namespace = "")]
        public string color { get; set; }
        [XmlElement("additional_variant_attribute", Namespace = "")]
        public FBadditional_variant_attribute additional_variant_attribute { get; set; }
        //[XmlElement("additional",Namespace = "")]
        //public Fbimage additional { get; set; }
        public string brand { get; set; }
        public string condition { get; set; }
        public string availability { get; set; }
        public string price { get; set; }
        public string Sales_Price { get; set; }
    }
    //[XmlRoot("", Namespace = "http://base.google.com/ns/1.0")]
    //[XmlType(TypeName = "additional")
    public class FBadditional_variant_attribute
    {
        public string label { get; set; }
        public string value { get; set; }
    }
}
