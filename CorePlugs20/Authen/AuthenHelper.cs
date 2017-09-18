using System.Collections.Generic;
using System.IO;
using CorePlugs20.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CorePlugs20.Authen
{
    public class AuthenHelper
    {
		/// <summary>
		/// 获取Authen身份认证信息  配置文件格式如下 使用 PrjName、Key以及Secret
		/// <para>"Authen":</para> 
		/// <para>{</para> 
		/// <para>   "PrjName":"your Project Name",</para> 
		/// <para>   "Key":"your key",</para> 
		/// <para>   "Secret":"you secret"</para> 
		/// <para>}</para> 
		/// </summary>
		/// <param name="configFileName">配置文件名称 默认为appsettings.json</param>
		/// <returns>返回用于在请求时headers中添加的Dictionary&lt;string,string&gt;集合</returns>
		public static Dictionary<string,string> GetAuthenInfo(string configFileName="appsettings.json")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFileName, optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            string projectName = configuration.GetSection("Authen").GetSection("PrjName").Value;
            string key = configuration.GetSection("Authen").GetSection("Key").Value;
            string secret = configuration.GetSection("Authen").GetSection("Secret").Value;
            AuthenModel authenModel = new AuthenModel { PrjName = projectName, Key = key, Secret = secret };
            Dictionary<string,string> dic = JsonConvert.DeserializeObject<Dictionary<string,string>>(JsonConvert.SerializeObject(authenModel));
            return dic;
        }
    }
}