using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CorePlugs20.Authen;
using CorePlugs20.Models;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace uiblog
{
    public class Program
    {
        public static Dictionary<string,string> AuthenInfo = AuthenHelper.GetAuthenInfo();
        private static  IConfigurationRoot mainConfig = null;
        private static ConfigModel config = null;
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
            ApiResultModel apiResult = null;
            apiResult = ApiHelper.GetWebApi<ApiResultModel>("http://0.0.0.0:16060/","Authen/api1.0/Config",AuthenInfo);
            if(!apiResult.Value.Status)
            {
                System.Console.WriteLine("配置服务器无法返回配置信息，当前服务无法启动");
            }
            string valueStr= JsonConvert.SerializeObject(apiResult.Value.Data);
            config = JsonConvert.DeserializeObject<ConfigModel>(valueStr);
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
                .UseUrls(Config.BlogUi.BlogUiUrls[0])
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
