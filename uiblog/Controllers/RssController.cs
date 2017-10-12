using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.OdinController;
using CorePlugs20.OdinRss;
using CorePlugs20.OdinString;
using CorePlugs20.TimeHelper;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace uiblog.Controllers
{
    public class RssController : Controller
    {
        public ActionResult Feed(string type)
        {
            Object dbResult = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetPostByRss"].Uri,
                                        Program.Config.BlogUi["GetPostByRss"].UriPath).Value.Data;
            var rssModel = JObject.Parse(JsonConvert.SerializeObject(dbResult)).ToObject<Index_Model>();

            var rss = Program.Config.Rss;
            AtomModel atomModel = new AtomModel
            {
                RssTitle = rss.RssTitle,
                AlternateLink = rss.AlternateLink,
                SelfLink = rss.SelfLink,
                Tag = new AtomTag
                {
                    Id = rss.AtomTag.Id,
                    Domain = rss.AtomTag.Domain
                },
                SubTitle = rss.SubTitle,
                AuthorName = rss.AuthorName,
                AuthorEmail = rss.AuthorEmail,
                Entries = new List<RssModel>()
                // {
                //     new RssModel { Title = "EntryTitle1", Link = "EntryLink1", Published = "Published1", Id = "EntryId1", Summary = "EntrySummary1", Contents = "EntryContents1" },
                //     new RssModel { Title = "EntryTitle2", Link = "EntryLink2", Published = "Published2", Id = "EntryId2", Summary = "EntrySummary2", Contents = "EntryContents2" },
                //     new RssModel { Title = "EntryTitle3", Link = "EntryLink3", Published = "Published3", Id = "EntryId3", Summary = "EntrySummary3", Contents = "EntryContents3" },
                //     new RssModel { Title = "EntryTitle4", Link = "EntryLink4", Published = "Published4", Id = "EntryId4", Summary = "EntrySummary4", Contents = "EntryContents4" },
                //     new RssModel { Title = "EntryTitle5", Link = "EntryLink5", Published = "Published5", Id = "EntryId5", Summary = "EntrySummary5", Contents = "EntryContents5" },
                //     new RssModel { Title = "EntryTitle6", Link = "EntryLink6", Published = "Published6", Id = "EntryId6", Summary = "EntrySummary6", Contents = "EntryContents6" }
                // }
            };
            foreach (var post in rssModel.Posts)
            {
                atomModel.Entries.Add(new RssModel()
                    {
                        Title = post.PostTitle,
                        Link = rss.AlternateLink+"blog/post/"+post.Id,
                        Published = UnixTimeHelper.FromUnixTime(Convert.ToInt64(post.PostTime)).ToString("yyyy-MM-dd HH:mm:ss"),
                        Id = post.Id.ToString(),
                        Summary = post.PostDesc,
                        Contents = post.PostContent
                    });
            }
            XDocument xdoc = RssHelper.BuildRSS(atomModel);
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