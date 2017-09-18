using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CorePlugs20.Models;
using CorePlugs20.OdinMongo;
using CorePlugs20.OdinNet;
using CorePlugs20.OdinSecurity;
using CorePlugs20.OdinString;
using CorePlugs20.PlugConfig;
using CorePlugs20.TimeHelper;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CorePlugs20.ApiFilter
{
    /// <summary>
    /// 身份认证拦截器
    /// </summary>
    public class ApiAuthenFilterAttribute : ActionFilterAttribute
    {
        private static Dictionary<string,string> authenInfo = null;
        private PlugModel model = PlugHelper.ReadPlugConfig();
        private static ConfigModel config;
        private static MongoHelper mongo = null;
        private string cvalue = null;
        //构造函数
        public ApiAuthenFilterAttribute(ConfigModel _config,string _authenCollection,Dictionary<string,string> _authenInfo)
        {
            config = _config;
            authenInfo = _authenInfo;
            mongo = new MongoHelper(config.MongoConfig.ConnectionString,config.MongoConfig.DataBase);
            cvalue = config.MongoConfig[_authenCollection].CollectionNameValue;
        }
        
        
        
        private void AuthenExecuting(ActionExecutingContext context)
        {
            if(config.AuthenServer.AuthenEnable)
            {
                string errorInfo = null;
                InvokeRecordModel invokeRecordModel = new InvokeRecordModel();
                string controllerName = context.ActionDescriptor.RouteValues["controller"];
                string actionName = context.ActionDescriptor.RouteValues["action"];
                invokeRecordModel.ControllerName = controllerName;
                invokeRecordModel.ActionName = actionName;
                invokeRecordModel.InvokeMethod = context.HttpContext.Request.Method;
                invokeRecordModel.InvokeId = context.HttpContext.Request.Headers["Key"];
                invokeRecordModel.InvokeSecret = context.HttpContext.Request.Headers["Secret"];
                invokeRecordModel.UserSource = context.HttpContext.Request.Headers["PrjName"];
                invokeRecordModel.InvokeTime = UnixTimeHelper.FromDateTime(DateTime.Now).ToString();
                invokeRecordModel.Headers = JsonConvert.SerializeObject(context.HttpContext.Request.Headers);
                invokeRecordModel.RequestIp = context.HttpContext.GetUserIp();
                var requestIp = invokeRecordModel.RequestIp;
                //无需验证拦截器
                if(NoFilterHelper.IssureFilter<NoAuthenFilterAttribute>(context))
                {
                    invokeRecordModel.SetReturnValue(JsonConvert.SerializeObject(context.Result));
                    ApiHelper.PostAsync(model.ApiInvokeRecord.InvokeMethod.Uri,model.ApiInvokeRecord.InvokeMethod.MethodName,invokeRecordModel,authenInfo);
                    //ReturnSendRecord(context,invokeRecordModel);
                    return;
                }
                if(config.AuthenServer.NoAuthenIp.Enable)
                {
                    if(!IpAuthen(requestIp))
                    {
                        errorInfo=$"来自【 {requestIp} 】的请求没有通过Authen认证 IP验证未通过";
                        System.Console.WriteLine($"来自【 {requestIp} 】的请求没有通过Authen IP验证");
                        //发送并处理拦截错误
                        ReturnSendErrorRecord(context,errorInfo,invokeRecordModel);
                    }
                }
                //请求认证
                AuthenResultEnum authenResult = RequestAuthen(context,requestIp,ref errorInfo);
                invokeRecordModel.ReturnValue = $"ApiAuthenFilter {authenResult.ToString()}";
                //判断是否成功
                if(authenResult == AuthenResultEnum.Success)
                    return;
                //发送并返回拦截错误
                ReturnSendErrorRecord(context,errorInfo,invokeRecordModel);
                
            }
        }

        //Ip认证
        private bool IpAuthen(string ip)
        {
            return config.AuthenServer.NoAuthenIp.Ips.Contains(ip);
        }

        //发送并返回认证错误
        private void ReturnSendErrorRecord(ActionExecutingContext context,string errorInfo,InvokeRecordModel invokeRecordModel)
        {
            var result = AuthenResultError(context,errorInfo);
            invokeRecordModel.SetReturnValue(JsonConvert.SerializeObject(result));
            ApiHelper.PostAsync(model.ApiInvokeRecord.InvokeMethod.Uri, model.ApiInvokeRecord.InvokeMethod.MethodName, invokeRecordModel, authenInfo);
            context.Result=result;
        }

        private void ReturnSendRecord(ActionExecutingContext context,InvokeRecordModel invokeRecordModel)
        {
            var result = AuthenResultError(context,invokeRecordModel);
            invokeRecordModel.SetReturnValue(JsonConvert.SerializeObject(result));
            ApiHelper.PostAsync(model.ApiInvokeRecord.InvokeMethod.Uri, model.ApiInvokeRecord.InvokeMethod.MethodName, invokeRecordModel, authenInfo);
            context.Result=result;
        }

        //失败处理
        private ContentResult AuthenResultError(ActionExecutingContext context,string errorInfo)
        {
            ApiResultModel resultModel = new ApiResultModel();
            resultModel.Value = new ResultData { Status=false, Data=errorInfo };
            resultModel.ResultTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,ms");
            var result = new ContentResult
            {
                ContentType="application/json",
                Content=JsonConvert.SerializeObject(resultModel),
                StatusCode=context.HttpContext.Response.StatusCode
            };
            return result;
        }

        private ContentResult AuthenResultError(ActionExecutingContext context,InvokeRecordModel invokeRecord)
        {
            ApiResultModel resultModel = new ApiResultModel();
            resultModel.Value = new ResultData { Status=false, Data=invokeRecord };
            resultModel.ResultTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,ms");
            var result = new ContentResult
            {
                ContentType="application/json",
                Content=JsonConvert.SerializeObject(resultModel),
                StatusCode=context.HttpContext.Response.StatusCode
            };
            return result;
        }

        //请求认证
        private AuthenResultEnum RequestAuthen(ActionExecutingContext context,string requestIp,ref string errorInfo)
        {
            bool flag = false;
            string key = context.HttpContext.Request.Headers["Key"];
            string secret = context.HttpContext.Request.Headers["Secret"];
            var filter = Builders<BsonDocument>.Filter.Eq("Invoker", key);
            var mongoResult = mongo.SelectBsonModel(cvalue,filter);
            if(mongoResult!=null)
            {
                var mongoSalt = mongoResult.GetValue("Sale").AsString;
                var mongoPwd = mongoResult.GetValue("Key").AsString;
                var userPwd = (secret+mongoSalt).StringToMd5ToLower();
                flag = userPwd ==  mongoPwd;
                //flag = mongoSalt ==  mongoPwd;    //错误测试代码
                if(!flag)
                {
                    errorInfo=$"来自【 {requestIp} 】的请求没有通过Authen认证,Key:{key} Secret:{secret} 查验未通过";
                    System.Console.WriteLine($"来自【 {requestIp} 】的请求没有通过Authen认证");
                    System.Console.WriteLine($"Key:{key}");
                    System.Console.WriteLine($"secret:{secret}");
                    System.Console.WriteLine("查验未通过");
                    return AuthenResultEnum.SecretError;
                }
                errorInfo="";
                return AuthenResultEnum.Success;
            }
            else
            {
                errorInfo=$"来自【 {requestIp} 】的请求没有通过Authen认证,Key:{key} Secret:{secret} 查找不存在";
                System.Console.WriteLine($"来自【 {requestIp} 】的请求没有通过Authen认证");
                System.Console.WriteLine($"Key:{key}");
                System.Console.WriteLine($"Secret:{secret}");
                System.Console.WriteLine("查找不存在");
                return AuthenResultEnum.NotFind;
            }
        }


        private void AuthenExecuted(ActionExecutedContext context)
        {
            if(config.AuthenServer.AuthenEnable)
            {
                if(NoFilterHelper.IssureFilter<NoAuthenFilterAttribute>(context))
                {
                    base.OnActionExecuted(context);
                    return;
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AuthenExecuting(context);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            AuthenExecuted(context);
            base.OnActionExecuted(context);
        }
    }
    public enum AuthenResultEnum
    {
        NotFind = -1,
        SecretError,
        Success
    }
}