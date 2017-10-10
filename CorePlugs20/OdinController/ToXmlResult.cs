using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorePlugs20.OdinController
{
    public class ToXmlResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the System.Web.Mvc.XmlResult class
        /// 初始化
        /// </summary>         
        public ToXmlResult() { }
        /// <summary>
        /// Encoding
        /// 编码格式
        /// </summary>
        public Encoding ContentEncoding { get; set; }
        /// <summary>
        /// Gets or sets the type of the content.
        /// 获取或设置返回内容的类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Gets or sets the data
        /// 获取或设置内容
        /// </summary>
        public string XmlContent { get; set; }
        /// <summary>
        /// Gets or sets a value that indicates whether HTTP GET requests from the client
        /// 获取或设置一个值,指示是否HTTP GET请求从客户端
        /// </summary>
        public XmlRequestBehavior XmlRequestBehavior { get; set; }
        /// <summary>
        /// Enables processing of the result of an action method by a custom type that
        /// 处理结果
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            if (context == null) { throw new ArgumentNullException("context"); }
            HttpRequest request = context.HttpContext.Request;
            if (XmlRequestBehavior == XmlRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("XmlRequest_GetNotAllowed");
            }
            HttpResponse response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/xml";
            if (this.ContentEncoding != null)
                response.WriteAsync(XmlContent,this.ContentEncoding).GetAwaiter();
            else
                response.WriteAsync(XmlContent).GetAwaiter();
        }
    }

}