using System.Collections.Generic;
using System.Linq;
using Authen.Models;
using BlogModels.MongoKv;
using CorePlugs20.ApiFilter;
using CorePlugs20.Models;
using CorePlugs20.OdinMongo;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Authen.Controllers
{
    [Route("api1.0/[controller]")]

    public class AuthenTokenController : Controller
    {
        private static MongoHelper mongo = new MongoHelper(Program.Config.MongoConfig.ConnectionString,Program.Config.MongoConfig.DataBase);
        private string cvalue = Program.Config.MongoConfig[MongoCollectionEnum.AUTHENCONFIGCOLLECTION].CollectionNameValue;
        
        AuthenEntities db;
        public AuthenTokenController(AuthenEntities _db)
        {
            db = _db;
        }


        /*
        接口路径: api1.0/AuthenToken/{AuthenKey}
        请求方式: Get
        输入参数: {AuthenKey}  请求的Key
        接口返回: Json格式
        {
            Status = true,   //业务成功         false为业务失败  Data返回错误信息
            Data = InvokerAuthen_DbModel对象 
            
        }
        接口简介: 请求认证查询接口 传递需要查询的Key 返回查询结果
        */
        
        [HttpGet("{AuthenKey}")]
        public ResultData GetAuthenInfo(string AuthenKey)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Invoker", AuthenKey);
            var mongoResult = mongo.SelectBsonModel(cvalue,filter);
            //mongo查询不存在
            if(mongoResult==null)
            {
                //数据库查询 并存储于mongo中
                var dbModel = db.InvokerAuthens.Where(ia=>ia.Invoker==AuthenKey).SingleOrDefault();
                if(dbModel == null)
                    return new ResultData{ Status = false,Data="Key查找不存在" };
                else
                {
                    mongo.AddModel(cvalue,dbModel);
                    return new ResultData { Status = true,Data = dbModel };
                }
            }
            InvokerAuthen_DbModel model = mongo.ConvertMongoObjectToObject<InvokerAuthen_DbModel>(mongoResult,new List<string>{"_id"});
            return new ResultData { Status = true,Data = model };
        }
        
    }
}