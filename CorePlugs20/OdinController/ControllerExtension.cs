using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CorePlugs20.OdinController
{
    
    /// <summary>
    /// 扩展System.Mvc.Controller
    /// </summary>
    public static class ControllerExtension
    {
        public static XmlResult Xml(this Controller request, object obj) { return Xml(obj, null, null, XmlRequestBehavior.DenyGet); }
        public static XmlResult Xml(this Controller request, object obj, XmlRequestBehavior behavior) { return Xml(obj, null, null, behavior); }
        public static XmlResult Xml(this Controller request, object obj, Encoding contentEncoding, XmlRequestBehavior behavior) { return Xml(obj, null, contentEncoding, behavior); }
        public static XmlResult Xml(this Controller request, object obj, string contentType, Encoding contentEncoding, XmlRequestBehavior behavior) { return Xml(obj, contentType, contentEncoding, behavior); }
        internal static XmlResult Xml(object data, string contentType, Encoding contentEncoding, XmlRequestBehavior behavior) { return new XmlResult() { ContentEncoding = contentEncoding, ContentType = contentType, Data = data, XmlRequestBehavior = behavior }; }
        
        
        public static ToXmlResult ToXml(this Controller request, string xmlStr) { return ToXml(xmlStr, null, null, XmlRequestBehavior.DenyGet); }
        public static ToXmlResult ToXml(this Controller request, string xmlStr, XmlRequestBehavior behavior) { return ToXml(xmlStr, null, null, behavior); }
        public static ToXmlResult ToXml(this Controller request, string xmlStr, Encoding contentEncoding, XmlRequestBehavior behavior) { return ToXml(xmlStr, null, contentEncoding, behavior); }
        public static ToXmlResult ToXml(this Controller request, string xmlStr, string contentType, Encoding contentEncoding, XmlRequestBehavior behavior) { return ToXml(xmlStr, contentType, contentEncoding, behavior); }
        internal static ToXmlResult ToXml(string xmlStr, string contentType, Encoding contentEncoding, XmlRequestBehavior behavior) { return new ToXmlResult() { ContentEncoding = contentEncoding, ContentType = contentType, XmlContent = xmlStr, XmlRequestBehavior = behavior }; }
    }
}