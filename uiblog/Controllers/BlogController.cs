using System;
using System.Collections.Generic;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace uiblog.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index(int? id)
        {
            id = id==null?1:id;
            Object result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetPosts"].Uri,
                                        Program.Config.BlogUi["GetPosts"].UriPath+id).Value.Data;
            var pageModel = JObject.Parse(JsonConvert.SerializeObject(result)).ToObject<Index_Model>();
            return View(pageModel);
        }
    }
}