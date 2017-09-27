using System.Collections.Generic;
using System.Linq;
using apiblog.Models;
using BlogModels.MongoKv;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.OdinMongo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace apiblog.Controllers
{
    [Route("apiblog/api1.0/[controller]")]
    public class BlogController : Controller
    {
        private static MongoHelper mongo = new MongoHelper(Program.Config.MongoConfig.ConnectionString,Program.Config.MongoConfig.DataBase);
        private string cvalue = Program.Config.MongoConfig[MongoCollectionEnum.BLOGDATACOLLECTION].CollectionNameValue;
        private IHostingEnvironment hostingEnv;
        BlogEntities db;
        public BlogController(BlogEntities _db,IHostingEnvironment env)
        {
            db = _db;
            hostingEnv = env;
        }

        /*
        接口简介: 获取blog title 接口
        接口路径: apiblog/api1.0/Blog/GetBlogTitle
        请求方式: Get
        输入参数: 无
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data = "BlogTitle"     // 成功返回blogtitle 失败返回错误信息
        }
        */
        
        [EnableCors("AllowSpecificOrigin")]
        [Route("GetBlogTitle")]
        [HttpGet]
        public ResultData GetBlogTitle()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Key", "Blog");
            var mongoResult = mongo.SelectBsonModel(cvalue,filter);
            if(mongoResult == null)
            {
                var dbModel = db.BlogConfigs.SingleOrDefault();
                if(dbModel == null)
                    return new ResultData{ Status = false,Data="BlogConfig查询不存在，页面无法显示,0xGetBlogTitle01" };
                BlogConfig_Model configModel = new BlogConfig_Model
                {
                    BlogTitle = dbModel.BlogTitle,
                    BlogAboutMe = dbModel.AboutMe
                };
                mongo.RemoveModel(cvalue,filter);
                mongo.AddModel(cvalue,configModel);
                return new ResultData{ Status = true,Data=configModel.BlogTitle };
            }
            else
            {
                BlogConfig_Model configModel = mongo.ConvertMongoObjectToObject<BlogConfig_Model>(mongoResult,new List<string>{"_id"});
                return new ResultData{ Status = true,Data=configModel.BlogTitle };
            }
        }


        /*
        接口简介: 获取当前页面Pv 以及 网站 Uv 的接口
        接口路径: apiblog/api1.0/Blog/GetPvUv
        请求方式: Get
        输入参数: 无
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data = new { uv = uv,pv = pv }     // 成功返回pv和uv的信息 失败返回错误信息
        }
        */
        
        [EnableCors("AllowSpecificOrigin")]
        [Route("GetPvUv/{VisitPage}")]
        [HttpGet]
        public ResultData GetPvUv(string VisitPage)
        {
            var pvModel =  db.PV.Where(pView=>pView.VisitPage==VisitPage).Single();
            int pv = pvModel.VisitCount;
            int uv = db.UV.Where(uView=>uView.VisitPath==VisitPage).Count();
            return new ResultData{ Status = true,Data=new { uv = uv,pv = pv } };
        }
    }
}