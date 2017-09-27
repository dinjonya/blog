using System.Collections.Generic;
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
    [Route("Authen/api1.0/[controller]")]

    public class ConfigController : Controller
    {
        private static MongoHelper mongo = new MongoHelper(Program.Config.MongoConfig.ConnectionString,Program.Config.MongoConfig.DataBase);
        private string cvalue = Program.Config.MongoConfig[MongoCollectionEnum.BLOGCONFIGCOLLECTION].CollectionNameValue;
        
        
        /*
        接口简介: 获取整体配置信息 无需拦截器过滤拦截 配置信息存储于mongodb缓存中
        接口路径: Authen/api1.0/Config
        请求方式: Get
        输入参数: 无
        接口返回: Json格式
        {
            Status = true,   //业务成功         false为业务失败  Data返回错误信息
            Data = ConfigModel对象 
            
        }
        
        
        */
        [HttpGet]
        public ResultData Get()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("EnvironmentName", "online");
            var bsonResult = mongo.SelectBsonModel(cvalue,filter);
            if(bsonResult == null)
            {
                mongo.AddModel(cvalue,Program.Config);
                bsonResult = mongo.SelectBsonModel(cvalue,filter);
            }
            var result =  mongo.ConvertMongoObjectToObject<ConfigModel>(bsonResult,new List<string>{ "_id" });
            return new ResultData { Status = true, Data = result };
        }

        /*
        接口简介: 删除mongodb的缓存数据 reload 重新加载配置文件并存储
        接口路径: Authen/api1.0/Config/Reload
        请求方式: Get
        输入参数: 无        
        接口返回: Json格式
        ConfigModel对象
        */
        [Route("reload")]
        [HttpGet]
        public ResultData GetConfigReload()
        {
            mongo.ClearCollection(cvalue);
            return Get();
        }
    }
}