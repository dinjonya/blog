using System;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace uiblog.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index(int? q1)
        {
            Object result = null;
            if(q1==null)
                result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetCategories"].Uri,
                                        Program.Config.BlogUi["GetCategories"].UriPath).Value.Data;
            else
                result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetCategories"].Uri,
                                        Program.Config.BlogUi["GetCategories"].UriPath+q1).Value.Data;
            var pageModel = JObject.Parse(JsonConvert.SerializeObject(result)).ToObject<Category_Model>();
            return View(pageModel);
        }

        
        public IActionResult Post(int q1,int? q2)
        {
            Object result = null;
            if(q2==null)
                result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetPostsByCategory"].Uri,
                                        Program.Config.BlogUi["GetPostsByCategory"].UriPath+q1).Value.Data;
            else
                result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetPostsByCategory"].Uri,
                                        Program.Config.BlogUi["GetPostsByCategory"].UriPath+q1+"/"+q2).Value.Data;
            var pageModel = JObject.Parse(JsonConvert.SerializeObject(result)).ToObject<Index_Model>();
            return View(pageModel);
        }
    }
}