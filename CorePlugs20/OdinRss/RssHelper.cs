using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace CorePlugs20.OdinRss
{
    public class RssHelper
    {
        /// <summary>  
        /// 生成RSS,写出文件  
        /// <param name="AtomModel">atomModel</param>  
        /// <returns></returns>  
        public static XDocument BuildRSS(AtomModel atomModel)  
        {  
            XNamespace defaultNs = "http://www.w3.org/2005/Atom";
            var xDoc = new XDocument(
                new XElement(defaultNs+"feed", new XAttribute("xmlns", defaultNs),
                    new XElement(defaultNs+"title",atomModel.RssTitle),
                    new XElement(defaultNs+"subtitle", $"{atomModel.SubTitle}"),
                    new XElement(defaultNs+"link",new XAttribute("rel","alternate"),new XAttribute("type","text/html"),new XAttribute("href",atomModel.AlternateLink)),
                    new XElement(defaultNs+"link",new XAttribute("rel","self"),new XAttribute("type","application/atom+xml"),new XAttribute("href",atomModel.SelfLink)),
                    new XElement(defaultNs+"author", 
                        new XElement(defaultNs+"name",atomModel.AuthorName),
                        new XElement(defaultNs+"uri",atomModel.AlternateLink),
                        new XElement(defaultNs+"email",atomModel.AuthorEmail)
                    ),
                    new XElement(defaultNs+"id", $"tag:{atomModel.Tag.Domain},{DateTime.Now.ToString("yyyy-MM-dd")}:/blog//{atomModel.Tag.Id}"),
                    new XElement(defaultNs+"updated", $"{DateTime.Now.ToString("yyyy-MM-dd")}")
            ));
            foreach (var item in atomModel.Entries)
            {
                xDoc.Root.Add(
                    new XElement(defaultNs+"entry",
                        new XElement(defaultNs+"title",item.Title),
                        new XElement(defaultNs+"link",new XAttribute("rel","alternate"),new XAttribute("type","text/html"),new XAttribute("href",item.Link)),
                        new XElement(defaultNs+"id",$"tag:{atomModel.Tag.Domain},{DateTime.Now.ToString("yyyy")}:/blog//{atomModel.Tag.Id}.{item.Id}"),
                        new XElement(defaultNs+"published",item.Published),
                        new XElement(defaultNs+"updated",$"{DateTime.Now.ToString("yyyy-MM-dd")}"),
                        new XElement(defaultNs+"summary",item.Summary),
                        new XElement(defaultNs+"author",
                            new XElement(defaultNs+"name",atomModel.AuthorName),
                            new XElement(defaultNs+"uri",atomModel.AlternateLink),
                            new XElement(defaultNs+"email",atomModel.AuthorEmail)
                        ),
                        new XElement(defaultNs+"content",new XCData(item.Contents))
                    )
                );
            }
            return xDoc;  
        }  
    }
}