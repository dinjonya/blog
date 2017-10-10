using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using CorePlugs20.OdinController;
using CorePlugs20.OdinRss;
using CorePlugs20.OdinString;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace uiblog.Controllers
{
    public class myxmlController : Controller
    {
        public ActionResult GetActionResult(string type)
        {
            AtomModel model = new AtomModel
            {
                RssTitle = "RssTitle",
                AlternateLink = "AlternateLink",
                SelfLink = "SelfLink",
                Tag = new AtomTag
                {
                    Id = "AtomTagId",
                    Domain = "Domain"
                },
                SubTitle = "SubTitle",
                AuthorName = "AuthorName",
                AuthorEmail = "AuthorEmail",
                Entries = new List<RssModel>
                {
                    new RssModel { Title = "EntryTitle1", Link = "EntryLink1", Published = "Published1", Id = "EntryId1", Summary = "EntrySummary1", Contents = "EntryContents1" },
                    new RssModel { Title = "EntryTitle2", Link = "EntryLink2", Published = "Published2", Id = "EntryId2", Summary = "EntrySummary2", Contents = "EntryContents2" },
                    new RssModel { Title = "EntryTitle3", Link = "EntryLink3", Published = "Published3", Id = "EntryId3", Summary = "EntrySummary3", Contents = "EntryContents3" },
                    new RssModel { Title = "EntryTitle4", Link = "EntryLink4", Published = "Published4", Id = "EntryId4", Summary = "EntrySummary4", Contents = "EntryContents4" },
                    new RssModel { Title = "EntryTitle5", Link = "EntryLink5", Published = "Published5", Id = "EntryId5", Summary = "EntrySummary5", Contents = "EntryContents5" },
                    new RssModel { Title = "EntryTitle6", Link = "EntryLink6", Published = "Published6", Id = "EntryId6", Summary = "EntrySummary6", Contents = "EntryContents6" }
                }
            };
            XDocument xdoc = RssHelper.BuildRSS(model);
            StringBuilder result = new StringBuilder();
            XmlWriterSettings xws = new XmlWriterSettings();
            using (XmlWriter xw = XmlWriter.Create(result, xws))
            {
                xdoc.WriteTo(xw);
            }
            string str = result.ToString();
            return this.ToXml(str,XmlRequestBehavior.AllowGet);
        }
    }
}