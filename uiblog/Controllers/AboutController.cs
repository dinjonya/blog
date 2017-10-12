using System;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace uiblog.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Me()
        {
            Object result = null;
            result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetAboutMe"].Uri,
                                        Program.Config.BlogUi["GetAboutMe"].UriPath).Value.Data;
            var pageModel = JObject.Parse(JsonConvert.SerializeObject(result)).ToObject<AboutMe_Model>();
            return View(pageModel);
        }
    }
}