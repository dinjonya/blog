using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CorePlugs20.Authen;
using CorePlugs20.Models;
using CorePlugs20.OdinString;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace apiblog
{
    public class Program
    {
        private static  IConfigurationRoot mainConfig = null;
        private static ConfigModel config = null;
        public static Dictionary<string,string> AuthenInfo = AuthenHelper.GetAuthenInfo();
        public static ConfigModel Config 
        { 
            get
            { 
                if(config==null)
                    GetConfig();
                return config; 
            } 
        }
        public static void GetConfig()
        {
            GetAuthen();
            ApiResultModel apiResult = ApiHelper.GetWebApi<ApiResultModel>("http://0.0.0.0:16060/","api1.0/Config",AuthenInfo);
            if(!apiResult.Value.Status)
            {
                System.Console.WriteLine("配置服务器无法返回配置信息，当前服务无法启动");
            }
            string valueStr= JsonConvert.SerializeObject(apiResult.Value.Data);
            config = JsonConvert.DeserializeObject<ConfigModel>(valueStr);
        }
        public static void GetAuthen()
        {
            AuthenInfo = AuthenHelper.GetAuthenInfo();
        }

        public static void Main(string[] args)
        {
            mainConfig = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(mainConfig)
                .UseKestrel()
                .UseIISIntegration()
                .UseUrls(Config.ApiBlog.ApiBlogUrls[0])
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
