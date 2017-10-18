using System;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace uiblog.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Article(int? q1)
        {
            if(q1==null)
                return RedirectPermanent("/Home/Index"); 
            Object result = null;
            result = ApiHelper.GetWebApi<ApiResultModel>(           
                                    Program.Config.BlogUi["GetPostArticle"].Uri,
                                    Program.Config.BlogUi["GetPostArticle"].UriPath+q1).Value.Data;
            var pageModel = JObject.Parse(JsonConvert.SerializeObject(result)).ToObject<Article_Model>();
            return View(pageModel);
        }
    }
}