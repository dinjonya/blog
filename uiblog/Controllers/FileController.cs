using System;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using uiblog;

public class FileController : Controller
{
    public IActionResult Index()
    {
        

        Object result = ApiHelper.GetWebApi<ApiResultModel>(
                                        Program.Config.BlogUi["GetAllPosts"].Uri,
                                        Program.Config.BlogUi["GetAllPosts"].UriPath).Value.Data;
            
        var pageModel = JObject.Parse(JsonConvert.SerializeObject(result)).ToObject<Index_Model>();
        return View(pageModel);
    }
}