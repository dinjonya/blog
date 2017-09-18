using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace CorePlugs20.OdinNet
{
    public class NetHelper
    {
        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public static List<string> GetAddressIP()
        {
            List<string> address = new List<string>();
            ///获取本地的IP地址
            string AddressIP = string.Empty;            
            foreach (IPAddress _IPAddress in Dns.GetHostEntryAsync(Dns.GetHostName()).Result.AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    address.Add(AddressIP);
                }
            }
            return address;
        }
    }
    public static class HttpContextExtension
    {
        public static string GetUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}