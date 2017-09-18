using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CorePlugs20.Models
{
    public class InvokeRecordModel
    {
        public void SetValue(string controllerName, string actionName,string source,string inputParams=null)
        {
            ControllerName = controllerName;
            ActionName = actionName;
            InputParams = inputParams;
            UserSource = source;
        }
        public void SetValue(Controller controller,string source,Dictionary<string,string> inputParams=null)
        {
            ControllerName = controller.RouteData.Values["Controller"].ToString();
            ActionName = controller.RouteData.Values["Action"].ToString();
            InputParams = JsonConvert.SerializeObject(inputParams);
            UserSource = source;
        }
        public void SetValue(Controller controller,string strParam)
        {
            JObject obj = JObject.Parse(strParam);
            ControllerName = controller.RouteData.Values["Controller"].ToString();
            ActionName = controller.RouteData.Values["Action"].ToString();
            InputParams = strParam;
            UserSource = obj.GetValue("Source")==null?"null":obj.GetValue("Source").ToString();
        }

        public void SetReturnValue(string returnValue)
        {
            ReturnValue = returnValue;
        }

        /// <summary>
        /// ControllerName
        /// </summary>
        /// <returns></returns>
        public string ControllerName { get; set; } = "";

        /// <summary>
        /// ActionName
        /// </summary>
        /// <returns></returns>
        public string ActionName { get; set; } = "";

        public string Headers { get; set; } = "";

        /// <summary>
        /// 入参
        /// </summary>
        /// <returns></returns>
        public string InputParams { get; set; } = "";

        /// <summary>
        /// 返回值
        /// </summary>
        /// <returns></returns>
        public string ReturnValue { get; set; } = null;

        /// <summary>
        /// 调用源
        /// </summary>
        /// <returns></returns>
        public string UserSource { get; set; } = "";

        public string RequestIp { get; set; } = "";

        /// <summary>
        /// 调用方式
        /// </summary>
        /// <returns></returns>
        public string InvokeMethod { get; set; }

        /// <summary>
        /// 接口调用者
        /// </summary>
        /// <returns></returns>
        public string InvokeId { get; set; }

        /// <summary>
        /// 调用者秘钥
        /// </summary>
        /// <returns></returns>
        public string InvokeSecret { get; set; }

        /// <summary>
        /// 调用时间
        /// </summary>
        /// <returns></returns>
        public string InvokeTime { get; set; }
    }
}