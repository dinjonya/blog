using System;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace uiblog.Controllers
{
    public class DateController : Controller
    {
        public IActionResult Post(int? q1,int? q2)
        {
            if(q1==null && q2==null)
                return RedirectPermanent("/Home/Index"); 
            Object result = null;
            if(q2==null)
                result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetPostsByDate"].Uri,
                                        Program.Config.BlogUi["GetPostsByDate"].UriPath+q1).Value.Data;
            else
                result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetPostsByDate"].Uri,
                                        Program.Config.BlogUi["GetPostsByDate"].UriPath+q1+"/"+q2).Value.Data;
            var pageModel = JObject.Parse(JsonConvert.SerializeObject(result)).ToObject<DatePosts_Model>();
            return View(pageModel);
        }
    }
}