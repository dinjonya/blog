using System;
using System.Collections.Generic;
using System.Reflection;
using CorePlugs20.Models;
using CorePlugs20.OdinNet;
using CorePlugs20.OdinString;
using CorePlugs20.PlugConfig;
using CorePlugs20.TimeHelper;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CorePlugs20.ApiFilter
{
    public class ApiFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
        private PlugModel model = PlugHelper.ReadPlugConfig();
        private static Dictionary<string,string> authenInfo = null;
        
        public ApiFilterAttribute(Dictionary<string,string> _authenInfo)
        {
            authenInfo = _authenInfo;
        }
        //返回记录封装
        private void ResultEncapsulates(ActionExecutedContext context)
        {
            if(JObject.Parse(JsonConvert.SerializeObject(context.Result)).GetValue("Value")!=null)
            {
                ApiResultModel resultModel = new ApiResultModel();
                resultModel.Value = JObject.Parse(JsonConvert.SerializeObject(context.Result)).GetValue("Value").ToObject<ResultData>();
                resultModel.ResultTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,ms");
                var result = new ContentResult
                {
                    ContentType="application/json",
                    Content=JsonConvert.SerializeObject(resultModel),
                    StatusCode=context.HttpContext.Response.StatusCode
                };
                context.Result=result;
            }
        }

        //api调用记录 调用前
        private void ApiInvokerRecordExecuting(ActionExecutingContext context)
        {
            if(model.ApiInvokeRecord.Used)
            {
                InvokeRecordModel invokeRecordModel = new InvokeRecordModel();
                string controllerName = context.ActionDescriptor.RouteValues["controller"];
                string actionName = context.ActionDescriptor.RouteValues["action"];
                invokeRecordModel.ControllerName = controllerName;
                invokeRecordModel.ActionName = actionName;
                invokeRecordModel.InvokeMethod = context.HttpContext.Request.Method;
                invokeRecordModel.RequestIp = context.HttpContext.GetUserIp();
                invokeRecordModel.InvokeId = context.HttpContext.Request.Headers["Key"];
                invokeRecordModel.InvokeSecret = context.HttpContext.Request.Headers["Secret"];
                invokeRecordModel.InvokeTime = UnixTimeHelper.FromDateTime(DateTime.Now).ToString();
                invokeRecordModel.UserSource = context.HttpContext.Request.Headers["PrjName"];
                invokeRecordModel.Headers = JsonConvert.SerializeObject(context.HttpContext.Request.Headers);
                if(context.HttpContext.Request.Method=="GET")
                {
                    invokeRecordModel.SetValue(controllerName, actionName, invokeRecordModel.UserSource, context.HttpContext.Request.QueryString.ToString());
                }
                else //if(context.HttpContext.Request.Method=="POST")
                {
                    string strParams = ApiHelper.GetStringFromRequestBody(context.HttpContext.Request.Body);
                    invokeRecordModel.SetValue(controllerName, actionName, invokeRecordModel.UserSource, strParams);         
                    context.RouteData.Values.Add("paramStr",strParams);
                }
                context.RouteData.Values.Add("invokeRecordModel",invokeRecordModel);
            }
        }

        //api调用记录 调用后 
        private void ApiInvokerRecordExecuted(ActionExecutedContext context)
        {
            if(model.ApiInvokeRecord.Used)
            {
                InvokeRecordModel callRecordModel = context.RouteData.Values["invokeRecordModel"] as InvokeRecordModel;
                if(context.Result!=null)
                {
                    callRecordModel.SetReturnValue(JsonConvert.SerializeObject(context.Result));
                }
                ApiHelper.PostAsync(model.ApiInvokeRecord.InvokeMethod.Uri,model.ApiInvokeRecord.InvokeMethod.MethodName,callRecordModel,authenInfo);
            }
        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(NoFilterHelper.IssureFilter<NoFilterAttribute>(context))
            {
                base.OnActionExecuting(context);
                return;
            }
            ApiInvokerRecordExecuting(context);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(!NoFilterHelper.IssureFilter<NoEncapsulatesAttribute>(context))
                ResultEncapsulates(context);
            if(NoFilterHelper.IssureFilter<NoFilterAttribute>(context))
            {
                base.OnActionExecuted(context);
                return;
            }
            ApiInvokerRecordExecuted(context);
            base.OnActionExecuted(context);
        }
    }
}