using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogPlugs.Configs;
using CorePlugs20.Authen;
using CorePlugs20.Files;
using CorePlugs20.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Authen
{
    public class Program
    {
        public static Dictionary<string,string> AuthenInfo = AuthenHelper.GetAuthenInfo();
        private static  IConfigurationRoot mainConfig = null;
        public static ConfigModel Config { get{ return GetConfig(); } }
        public static ConfigModel GetConfig()
        {
            return ConfigHelper.GetConfig(Directory.GetCurrentDirectory()+FileHelper.DirectorySeparatorChar+"Configs","blog.json");
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
                .UseUrls(Config.AuthenServer.AuthenServerUrls[0])
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}
