using System.IO;
using CorePlugs20.Models;
using Microsoft.Extensions.Configuration;

namespace BlogPlugs.Configs
{
    public class ConfigHelper
    {
        public static T LoadConfig<T>(string configFullPath){
            T config = new ConfigurationBuilder().AddJsonFile(configFullPath).Build().Get<T>();
            return config;
        }
        public static ConfigModel GetConfig(string configPath,string configName)
        {
            foreach (var item in Directory.GetFiles(configPath))
            {
                if(item.EndsWith(configName))
                {
                    return ConfigHelper.LoadConfig<ConfigModel>(item);
                }
            }
            return null;
        }
    }
}